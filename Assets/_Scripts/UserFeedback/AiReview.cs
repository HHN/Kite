using System;
using UnityEngine;

namespace Assets._Scripts.UserFeedback
{
    [Serializable]
    public class AiReview
    {
        [SerializeField] private long id;
        [SerializeField] private long novelId;
        [SerializeField] private string novelName;
        [SerializeField] private string prompt;
        [SerializeField] private string aiFeedback;
        [SerializeField] private string reviewText;

        public long Id
        {
            get => id;
            set => id = value;
        }

        public long NovelId
        {
            get => novelId;
            set => novelId = value;
        }

        public string NovelName
        {
            get => novelName;
            set => novelName = value;
        }

        public string Prompt
        {
            get => prompt;
            set => prompt = value;
        }

        public string AiFeedback
        {
            get => aiFeedback;
            set => aiFeedback = value;
        }

        public string ReviewText
        {
            get => reviewText;
            set => reviewText = value;
        }
    }
}