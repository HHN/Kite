using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.OfflineAiFeedback
{
    /// <summary>
    /// Represents a serializable container for a list of <see cref="FeedbackNodeContainer"/> objects.
    /// This class is typically used to hold collections of AI feedback data,
    /// making it easy to serialize and deserialize multiple feedback nodes (e.g., for saving/loading).
    /// </summary>
    [Serializable]
    public class FeedbackNodeList
    {
        [SerializeField] public List<FeedbackNodeContainer> feedbackNodes;
    }
}