using UnityEngine;

namespace _00_Kite2.Common.Novel.Character.CharacterController
{
    public class NovelImageController : MonoBehaviour
    {
        protected RectTransform CanvasRect;
        protected CharacterController CharacterController = null;
        protected CharacterController CharacterController2 = null;

        public void SetCanvasRect(RectTransform canvasRect)
        {
            this.CanvasRect = canvasRect;
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
            Debug.Log("SetFaceExpression: characterId - " + characterId + " expressionType - " + expressionType);
            // case CharacterRole.NONE: { return 0; }
            // case CharacterRole.PLAYER: { return 1; }
            // case CharacterRole.INTRO: { return 2; }
            // case CharacterRole.OUTRO: { return 3; }
            // case CharacterRole.INFO: { return 4; }
            // case CharacterRole.REPORTERIN: { return 5; }
            // case CharacterRole.VERMIETER: { return 6; }
            // case CharacterRole.VATER: { return 7; }
            // case CharacterRole.MUTTER: { return 8; }
            // case CharacterRole.BEKANNTER: { return 9; }
            // case CharacterRole.NOTARIN: { return 10; }
            // case CharacterRole.SACHBEARBEITER: { return 11; }

            if (CharacterController == null)
            {
            }
            else if (characterId == 0)
            {
                CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 1)
            {
                CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 2)
            {
                CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 3)
            {
                CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 4)
            {
                CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 5)
            {
                CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 6)
            {
                CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 7)
            {
                CharacterController2.SetFaceExpression(expressionType);
            }
            else if (characterId == 8)
            {
                CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 9)
            {
                CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 10)
            {
                CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 11)
            {
                CharacterController.SetFaceExpression(expressionType);
            }
            else if (CharacterController2 == null)
            {
            }
        }

        public virtual void StartCharacterTalking()
        {
            if (CharacterController == null)
            {
                return;
            }

            CharacterController.StartTalking();
        }

        public virtual void StopCharacterTalking()
        {
            if (CharacterController == null)
            {
                return;
            }

            CharacterController.StopTalking();
        }
    }
}