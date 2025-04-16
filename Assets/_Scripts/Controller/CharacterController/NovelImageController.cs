using UnityEngine;

namespace Assets._Scripts.Controller.CharacterController
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

            if (novelKite2CharacterController == null)
            {
            }
            else switch (characterId)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    novelKite2CharacterController.SetFaceExpression(expressionType);
                    break;
                case 7:
                    novelKite2CharacterController2.SetFaceExpression(expressionType);
                    break;
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                    novelKite2CharacterController.SetFaceExpression(expressionType);
                    break;
                default:
                {
                    if (novelKite2CharacterController2 == null)
                    {
                    }

                    break;
                }
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