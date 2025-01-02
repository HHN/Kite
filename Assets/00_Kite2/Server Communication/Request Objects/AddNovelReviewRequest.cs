using System;

namespace _00_Kite2.Server_Communication.Request_Objects
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