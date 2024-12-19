using System;

namespace _00_Kite2.Server_Communication.Request_Objects
{
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