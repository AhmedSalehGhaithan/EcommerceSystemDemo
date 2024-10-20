namespace eCommerce.Application.Exceptions
{
    /// <summary>
    /// Exception thrown when an item is not found in the application.
    /// </summary>
    /// <param name="message">The error message describing the exception.</param>
    public class ItemNotFoundException(string message) : Exception(message)
    {
    }
}
