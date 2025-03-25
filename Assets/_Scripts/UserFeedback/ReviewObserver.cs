using System;
using UnityEngine;

namespace Assets._Scripts.UserFeedback
{
    [Serializable]
    public class ReviewObserver
    {
        [SerializeField] private long observerID;
        [SerializeField] private string observerEmail;

        public void SetId(long id)
        {
            this.observerID = id;
        }

        public long GetId()
        {
            return observerID;
        }

        public void SetEmail(string email)
        {
            observerEmail = email;
        }

        public string GetEmail()
        {
            return observerEmail;
        }
    }
}