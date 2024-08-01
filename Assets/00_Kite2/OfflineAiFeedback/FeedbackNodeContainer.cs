using System;
using UnityEngine;

[Serializable]
public class FeedbackNodeContainer
{
    [SerializeField] public long novel;
    [SerializeField] public string path;
    [SerializeField] public string prompt;
    [SerializeField] public string completion;
}
