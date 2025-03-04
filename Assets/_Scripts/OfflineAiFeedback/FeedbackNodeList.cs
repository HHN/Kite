using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.OfflineAiFeedback
{
    [Serializable]
    public class FeedbackNodeList
    {
        [SerializeField] public List<FeedbackNodeContainer> feedbackNodes;
    }
}