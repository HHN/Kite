using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovelImageController : MonoBehaviour
{
    public virtual bool HandleTouchEvent(float x, float y, AudioSource audioSource)
    {
        return false;
    }

    public virtual void SetVisualElements(RectTransform canvasRect)
    {
        Debug.Log("TEST");
    }
}
