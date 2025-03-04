using System;
using UnityEngine;

namespace Assets._Scripts.UserFeedback
{
    [Serializable]
    public class DataObject
    {
        [SerializeField] private string prompt;
        [SerializeField] private string completion;
        [SerializeField] private long id;

        public string Prompt
        {
            get => prompt;
            set => prompt = value;
        }

        public string Completion
        {
            get => completion;
            set => completion = value;
        }

        public long Id
        {
            get => id;
            set => id = value;
        }
    }
}