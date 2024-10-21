﻿using eCommerce.Infrastructure.Middleware.ChineOfRepo.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Middleware.ChineOfRepo.ConcreteClasses
{
    /// <summary>
    /// Handles unique constraint violations in database operations.
    /// </summary>
    public class UniqueConstraintHandler : IExceptionHandler
    {
        /// <summary>
        /// Determines if the exception is a unique constraint violation (SQL error 2627).
        /// </summary>
        /// <param name="exception">The exception to evaluate.</param>
        /// <returns>True if the handler can process the exception; otherwise, false.</returns>
        public bool CanHandle(Exception exception)
            => exception is DbUpdateException dbUpdateEx && dbUpdateEx.InnerException is SqlException sqlEx && sqlEx.Number == 2627;

        /// <summary>
        /// Responds with a 409 Conflict status for unique constraint violations.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <param name="exception">The exception being handled.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task HandleAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await context.Response.WriteAsync("Unique constraint violation.");
        }
    }
}