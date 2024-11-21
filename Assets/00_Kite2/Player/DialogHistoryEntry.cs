using System;
using UnityEngine;

namespace _00_Kite2.Player
{
    [Serializable]
    public class DialogHistoryEntry
    {
        [SerializeField] private long novelId;
        [SerializeField] private string dialog;
        [SerializeField] private string completion;
        [SerializeField] private string dateAndTime;

        public long GetNovelId() 
        { 
            return novelId; 
        }

        public void SetNovelId(long novelId)
        {
            this.novelId = novelId;
        }

        public string GetDialog() 
        { 
            return dialog; 
        }

        public void SetDialog(string dialog)
        {
            this.dialog = dialog;
        }

        public string GetDateAndTime()
        {
            return dateAndTime;
        }

        public void SetDateAndTime(string dateAndTime)
        {
            this.dateAndTime = dateAndTime;
        }

        public string GetCompletion() 
        { 
            return completion; 
        }

        public void SetCompletion(string completion)
        {
            this .completion = completion;
        }
    }
}
