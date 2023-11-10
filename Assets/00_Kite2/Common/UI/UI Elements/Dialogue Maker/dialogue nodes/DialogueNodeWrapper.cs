using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueNodeWrapper : MonoBehaviour
{
    [SerializeField] private RectTransform dialogueNodeWrapperRectTransform;
    [SerializeField] private RectTransform dialogueNodeRectTransform;
    [SerializeField] private RectTransform contextMenuRectTransform;

    void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(dialogueNodeWrapperRectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(dialogueNodeRectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(contextMenuRectTransform);
    }
}
