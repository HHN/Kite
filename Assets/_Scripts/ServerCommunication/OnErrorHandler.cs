namespace Assets._Scripts.ServerCommunication
{
    /// <summary>
    /// Defines an interface for classes that handle error responses from server communication.
    /// Implementations of this interface provide custom logic for reacting to and processing
    /// unsuccessful server replies.
    /// </summary>
    public interface IOnErrorHandler
    {
        /// <summary>
        /// Called when an error response is received from the server.
        /// </summary>
        /// <param name="response">The <see cref="Response"/> object containing details about the error.</param>
        void OnError(Response response);
    }
}