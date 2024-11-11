using eCommerce.Application.Services.Interfaces.Logging;
using eCommerce.Infrastructure.Middleware.ChineOfRepo.ConcreteClasses;
using eCommerce.Infrastructure.Middleware.ChineOfRepo.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public class ExceptionHandlingMiddleware
{
    /// <summary>
    /// The next middleware in the pipeline.
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// List of exception handlers for processing specific exceptions.
    /// </summary>
    private readonly List<IExceptionHandler> _handlers;

    /// <summary>
    /// Initializes a new instance of the ExceptionHandlingMiddleware.
    /// </summary>
    /// <param name="next">The next middleware to invoke.</param>
    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
        _handlers = new List<IExceptionHandler>
        {
            new UniqueConstraintHandler(),
            new CannotInsertNullHandler(),
            new ForeignKeyConstraintHandler(),
            new GeneralExceptionHandler()
        };
    }

    /// <summary>
    /// Invokes the next middleware and handles exceptions that occur.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var logger = context.RequestServices.GetRequiredService<IAppLogger<ExceptionHandlingMiddleware>>();
            context.Response.ContentType = "application/json";
            var handler = _handlers.FirstOrDefault(h => h.CanHandle(ex));
            if (handler != null)
            {
                logger.LogError(ex, "Sql exception");
                await handler.HandleAsync(context, ex);
            }
            else
            {
                logger.LogError(ex, "Related EFCore exception");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An unexpected error occurred.");
            }
        }
    }
}
