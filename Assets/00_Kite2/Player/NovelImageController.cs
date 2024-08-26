using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovelImageController : MonoBehaviour
{

    protected RectTransform canvasRect = null;
    protected CharacterController characterController = null;
    protected CharacterController characterController2 = null;

    public void SetCanvasRect(RectTransform canvasRect)
    {
        this.canvasRect = canvasRect;
    }

    public virtual bool HandleTouchEvent(float x, float y, AudioSource audioSource)
    {
        return false;
    }

    public virtual void SetBackground()
    {
        
    }

    public virtual void SetCharacter()
    {
        
    }

    public virtual void DestroyCharacter()
    {

    }

    public void SetFaceExpression(int expressionType)
    {
        if (characterController == null)
        {
            return;
        }
        characterController.SetFaceExpression(expressionType);
    }

    public virtual void StartCharacterTalking()
    {
        if (characterController == null)
        {
            return;
        }
        characterController.StartTalking();
    }

    public virtual void StopCharacterTalking()
    {
        if (characterController == null)
        {
            return;
        }
        characterController.StopTalking();
    }
}
