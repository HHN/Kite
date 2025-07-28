using System;

namespace Assets._Scripts.ServerCommunication.RequestObjects
{
    /// <summary>
    /// Represents a request object for sending a prompt to a GPT (Generative Pre-trained Transformer) model.
    /// This object typically contains the text prompt that the AI should process or respond to.
    /// The class is <see cref="Serializable"/> to facilitate conversion to formats like JSON for server communication.
    /// </summary>
    [Serializable]
    public class GptRequest
    {
        public string prompt;
    }
}