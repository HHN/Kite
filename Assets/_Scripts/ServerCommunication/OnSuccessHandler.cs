namespace Assets._Scripts.ServerCommunication
{
    /// <summary>
    /// Defines an interface for classes that handle successful responses from server communication.
    /// Implementations of this interface provide custom logic for reacting to and processing
    /// successful server replies.
    /// </summary>
    public interface IOnSuccessHandler
    {
        /// <summary>
        /// Called when a successful response is received from the server.
        /// </summary>
        /// <param name="response">The <see cref="Response"/> object containing the successful data.</param>
        void OnSuccess(Response response);
    }
}