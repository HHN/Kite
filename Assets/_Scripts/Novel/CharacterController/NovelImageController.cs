using UnityEngine;

namespace Assets._Scripts.Novel.CharacterController
{
    public class NovelImageController : MonoBehaviour
    {
        protected RectTransform CanvasRect;
        public Kite2CharacterController novelKite2CharacterController;
        public Kite2CharacterController novelKite2CharacterController2;

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
            // case CharacterRole.INVESTOR: { return 9; }
            // case CharacterRole.NOTARIN: { return 10; }
            // case CharacterRole.SACHBEARBEITER: { return 11; }

            if (novelKite2CharacterController == null)
            {
                // Debug.Log("novelKite2CharacterController is null");
            }
            else if (characterId == 0)
            {
                novelKite2CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 1)
            {
                novelKite2CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 2)
            {
                novelKite2CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 3)
            {
                novelKite2CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 4)
            {
                novelKite2CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 5)
            {
                novelKite2CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 6)
            {
                novelKite2CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 7)
            {
                novelKite2CharacterController2.SetFaceExpression(expressionType);
            }
            else if (characterId == 8)
            {
                novelKite2CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 9)
            {
                novelKite2CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 10)
            {
                novelKite2CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 11)
            {
                novelKite2CharacterController.SetFaceExpression(expressionType);
            }
            else if (characterId == 12)
            {
                novelKite2CharacterController.SetFaceExpression(expressionType);
            }
            else if (novelKite2CharacterController2 == null)
            {
            }
        }

        public virtual void StartCharacterTalking()
        {
            if (novelKite2CharacterController == null)
            {
                return;
            }

            novelKite2CharacterController.StartTalking();
        }

        public virtual void StopCharacterTalking()
        {
            if (novelKite2CharacterController == null)
            {
                return;
            }

            novelKite2CharacterController.StopTalking();
        }
    }
}