namespace Assets._Scripts.Messages
{
    /// <summary>
    /// Provides a collection of error messages that can be used throughout the application
    /// to standardize error handling and improve maintainability.
    /// </summary>
    public abstract class ErrorMessages
    {
        public static readonly string NO_INTERNET = "Es gibt keine Verbindung zum Internet.";
        public static readonly string UNEXPECTED_SERVER_ERROR = "Ein unerwarteter Server-Fehler ist aufgetreten.";
    }
}
