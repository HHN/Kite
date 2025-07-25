using System;
using UnityEngine;

namespace Assets._Scripts.OfflineAiFeedback
{
    /// <summary>
    /// Represents a data container for storing offline AI feedback related to a specific novel and player path.
    /// This class is designed to be serializable, allowing its instances to be saved and loaded.
    /// </summary>
    [Serializable]
    public class FeedbackNodeContainer
    {
        [SerializeField] public long novel;
        [SerializeField] public string path;
        [SerializeField] public string prompt;
        [SerializeField] public string completion;
    }
}