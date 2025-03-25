using System;

namespace Assets._Scripts.ServerCommunication.RequestObjects
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