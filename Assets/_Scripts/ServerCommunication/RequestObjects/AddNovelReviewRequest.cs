using System;

namespace Assets._Scripts.Server_Communication.Request_Objects
{
    [Serializable]
    public class AddNovelReviewRequest
    {
        public long novelId;
        public string novelName;
        public long rating;
        public string reviewText;
    }
}