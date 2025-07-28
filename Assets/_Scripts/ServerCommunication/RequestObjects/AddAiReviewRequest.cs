using System;

namespace Assets._Scripts.ServerCommunication.RequestObjects
{
    /// <summary>
    /// Represents a request object used to send AI review data to the server.
    /// This includes details about the novel, the AI prompt and feedback, and the user's review text.
    /// The class is <see cref="Serializable"/> to facilitate conversion to formats like JSON for server communication.
    /// </summary>
    [Serializable]
    public class AddAiReviewRequest
    {
        public long novelId;
        public string novelName;
        public string prompt;
        public string aiFeedback;
        public string reviewText;
    }
}