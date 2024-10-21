using eCommerce.Infrastructure.Middleware.ChineOfRepo.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Middleware.ChineOfRepo.ConcreteClasses
{
    /// <summary>
    /// Handles exceptions for null insert violations in database operations.
    /// </summary>
    public class CannotInsertNullHandler : IExceptionHandler
    {
        /// <summary>
        /// Checks if the exception is due to a null insert (SQL error 515).
        /// </summary>
        /// <param name="exception">The exception to evaluate.</param>
        /// <returns>True if the handler can process the exception; otherwise, false.</returns>
        public bool CanHandle(Exception exception)
            => exception is DbUpdateException dbUpdateEx && dbUpdateEx.InnerException is SqlException sqlEx && sqlEx.Number == 515;

        /// <summary>
        /// Responds with a 400 Bad Request status for null insert violations.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <param name="exception">The exception being handled.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task HandleAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Can't insert null.");
        }
    }
}
