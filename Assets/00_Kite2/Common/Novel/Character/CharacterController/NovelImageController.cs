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

    public void SetFaceExpression(int characterId, int expressionType)
    {
        /*case Character.NONE: { return 0; }
        case Character.PLAYER: { return 1; }
        case Character.INTRO: { return 2; }
        case Character.OUTRO: { return 3; }
        case Character.INFO: { return 4; }
        case Character.REPORTERIN: { return 5; }
        case Character.VERMIETER: { return 6; }
        case Character.VATER: { return 7; }
        case Character.MUTTER: { return 8; }
        case Character.BEKANNTER: { return 9; }
        case Character.NOTARIN: { return 10; }
        case Character.SACHBEARBEITER: { return 11; }*/
        if (characterController == null)
        {
            Debug.Log("characterController == null");
            return;
        }
        else if (characterId == 5)
        {
            Debug.Log("REPORTERIN");
            characterController.SetFaceExpression(expressionType);
        }
        else if (characterId == 6)
        {
            characterController.SetFaceExpression(expressionType);
        }
        else if (characterId == 8)
        {
            characterController.SetFaceExpression(expressionType);
        }
        else if (characterId == 9)
        {
            characterController.SetFaceExpression(expressionType);
        }
        else if (characterId == 10)
        {
            characterController.SetFaceExpression(expressionType);
        }
        else if (characterId == 11)
        {
            characterController.SetFaceExpression(expressionType);
        }
        else if (characterController2 == null)
        {
            return;
        }
        else if (characterId == 7)
        {
            characterController2.SetFaceExpression(expressionType);
        }
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
