using System;
using System.Collections.Generic;
using UnityEngine;

namespace _00_Kite2.OfflineAiFeedback
{
    [Serializable]
    public class FeedbackNodeList
    {
        [SerializeField] public List<FeedbackNodeContainer> feedbackNodes;
    }
}