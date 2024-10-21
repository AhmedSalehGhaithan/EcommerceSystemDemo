using eCommerce.Application.Services.Interfaces.Logging;
using Microsoft.Extensions.Logging;

namespace eCommerce.Infrastructure.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="_logger"></param>
    public class SerilogLoggerAdapter<TValue>(ILogger<TValue> _logger) : IAppLogger<TValue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        public void LogError(Exception ex, string message)
            => _logger.LogError(ex, message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void LogInformation(string message)
            => _logger.LogInformation(message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void LogWarning(string message)
            => _logger.LogWarning(message);
    }
}
