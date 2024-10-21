using eCommerce.Infrastructure.Middleware.ChineOfRepo.Interface;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Infrastructure.Middleware.ChineOfRepo.ConcreteClasses
{
    /// <summary>
    /// Handles all unhandled exceptions in the application.
    /// </summary>
    public class GeneralExceptionHandler : IExceptionHandler
    {
        /// <summary>
        /// Indicates that this handler can process any exception.
        /// </summary>
        /// <param name="exception">The exception to evaluate.</param>
        /// <returns>True, as this handler handles all exceptions.</returns>
        public bool CanHandle(Exception exception) => true;

        /// <summary>
        /// Responds with a 500 Internal Server Error status and the exception message.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <param name="exception">The exception being handled.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task HandleAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync($"An error occurred: {exception.Message}");
        }
    }
}
