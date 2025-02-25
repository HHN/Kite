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

        public void SetPrompt(string prompt)
        {
            this.prompt = prompt;
        }

        public string GetPrompt()
        {
            return this.prompt;
        }

        public void SetCompletion(string completion)
        {
            this.completion = completion;
        }

        public string GetCompletion()
        {
            return this.completion;
        }

        public void SetId(long id)
        {
            this.id = id;
        }

        public long GetId()
        {
            return this.id;
        }
    }
}