using Microsoft.AspNetCore.Http;

namespace eCommerce.Infrastructure.Middleware.ChineOfRepo.Interface
{
    public interface IExceptionHandler
    {
        /// <summary>
        /// Determines if the handler can process the given exception.
        /// </summary>
        /// <param name="exception">The exception to evaluate.</param>
        /// <returns>True if the handler can process the exception; otherwise, false.</returns>
        bool CanHandle(Exception exception);

        /// <summary>
        /// Handles the exception and provides an appropriate response.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <param name="exception">The exception being handled.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task HandleAsync(HttpContext context, Exception exception);
    }
}
