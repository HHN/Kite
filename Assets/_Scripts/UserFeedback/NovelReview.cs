using System;
using UnityEngine;

namespace Assets._Scripts.UserFeedback
{
    [Serializable]
    public class NovelReview
    {
        [SerializeField] private long reviewId;
        [SerializeField] private long novelId;
        [SerializeField] private string novelTitle;
        [SerializeField] private long rating;
        [SerializeField] private string reviewContent;

        public long ReviewId
        {
            get => reviewId;
            set => reviewId = value;
        }

        public long NovelId
        {
            get => novelId;
            set => novelId = value;
        }

        public string NovelTitle
        {
            get => novelTitle;
            set => novelTitle = value;
        }

        public long Rating
        {
            get => rating;
            set => rating = value;
        }

        public string ReviewContent
        {
            get => reviewContent;
            set => reviewContent = value;
        }
    }
}