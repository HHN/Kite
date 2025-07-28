using System;

namespace Assets._Scripts.ServerCommunication.RequestObjects
{
    /// <summary>
    /// Represents a request object used to delete an observer from the server.
    /// This typically contains the unique identifier of the observer to be removed.
    /// The class is <see cref="Serializable"/> to facilitate conversion to formats like JSON for server communication.
    /// </summary>
    [Serializable]
    public class DeleteObserverRequest
    {
        public long id;
    }
}