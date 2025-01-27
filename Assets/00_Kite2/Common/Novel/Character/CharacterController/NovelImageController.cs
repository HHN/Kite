using UnityEngine;
using UnityEngine.Serialization;

namespace _00_Kite2.Common.Novel.Character.CharacterController
{
    public class NovelImageController : MonoBehaviour
    {
        protected RectTransform CanvasRect;
        public CharacterController novelCharacterController;
        public CharacterController novelCharacterController2;

        public void SetCanvasRect(RectTransform canvasRect)
        {
            this.CanvasRect = canvasRect;
        }

        public virtual bool HandleTouchEvent(float x, float y)
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

            if (novelCharacterController == null)
            {
            }
            else if (characterId == 0)
            {
                novelCharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 1)
            {
                novelCharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 2)
            {
                novelCharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 3)
            {
                novelCharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 4)
            {
                novelCharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 5)
            {
                novelCharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 6)
            {
                novelCharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 7)
            {
                novelCharacterController2.SetFaceExpression(expressionType);
            }
            else if (characterId == 8)
            {
                novelCharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 9)
            {
                novelCharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 10)
            {
                novelCharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 11)
            {
                novelCharacterController.SetFaceExpression(expressionType);
            }
            else if (novelCharacterController2 == null)
            {
            }
        }

        public virtual void StartCharacterTalking()
        {
            if (novelCharacterController == null)
            {
                return;
            }

            novelCharacterController.StartTalking();
        }

        public virtual void StopCharacterTalking()
        {
            if (novelCharacterController == null)
            {
                return;
            }

            novelCharacterController.StopTalking();
        }
    }
}