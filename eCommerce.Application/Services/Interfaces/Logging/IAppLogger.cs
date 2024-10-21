namespace eCommerce.Application.Services.Interfaces.Logging
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public interface IAppLogger<TValue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void LogInformation(string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        void LogError(Exception ex ,string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void LogWarning(string message);
    }
}
