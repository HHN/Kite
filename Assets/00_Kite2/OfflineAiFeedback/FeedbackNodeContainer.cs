using System;
using UnityEngine;

namespace _00_Kite2.OfflineAiFeedback
{
    [Serializable]
    public class FeedbackNodeContainer
    {
        [SerializeField] public long novel;
        [SerializeField] public string path;
        [SerializeField] public string prompt;
        [SerializeField] public string completion;
    }
}