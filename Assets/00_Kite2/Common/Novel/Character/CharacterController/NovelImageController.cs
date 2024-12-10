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
            // case Character.NONE: { return 0; }
            // case Character.PLAYER: { return 1; }
            // case Character.INTRO: { return 2; }
            // case Character.OUTRO: { return 3; }
            // case Character.INFO: { return 4; }
            // case Character.REPORTERIN: { return 5; }
            // case Character.VERMIETER: { return 6; }
            // case Character.VATER: { return 7; }
            // case Character.MUTTER: { return 8; }
            // case Character.BEKANNTER: { return 9; }
            // case Character.NOTARIN: { return 10; }
            // case Character.SACHBEARBEITER: { return 11; }
            
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
