using System;
using UnityEngine;

namespace _00_Kite2.UserFeedback
{
    [Serializable]
    public class NovelReview
    {
        [SerializeField] private long id;
        [SerializeField] private long novelId;
        [SerializeField] private string novelName;
        [SerializeField] private long rating;
        [SerializeField] private string reviewText;

        public void SetId(long id)
        {
            this.id = id;
        }

        public long GetId()
        {
            return id;
        }

        public void SetNovelId(long novelId)
        {
            this.novelId = novelId;
        }

        public long GetNovelId()
        {
            return novelId;
        }

        public void SetNovelName(string novelName)
        {
            this.novelName = novelName;
        }

        public string GetNovelName()
        {
            return novelName;
        }

        public void SetRating(long rating)
        {
            this.rating = rating;
        }

        public long GetRating()
        {
            return rating;
        }

        public void SetReviewText(string reviewText)
        {
            this.reviewText = reviewText;
        }

        public string GetReviewText()
        {
            return reviewText;
        }
    }
}