using System.Collections;
using System.Collections.Generic;
using Assets._Scripts.Controller.SceneControllers;
using UnityEngine;

namespace Assets._Scripts.Controller.CharacterController
{
    public interface IDecorationInteraction
    {
        IEnumerator PlayInteraction(RectTransform container);
    }

    [System.Serializable]
    public class DecorationInteraction
    {
        public GameObject prefab;
        public GameObject container;
    }

    public class NovelImageController : MonoBehaviour
    {
        private RectTransform _canvasRect;
        public Kite2CharacterController novelKite2CharacterController;
        public Kite2CharacterController novelKite2CharacterController2;

        [Header("Character Setup")] [SerializeField]
        private List<Transform> characterContainers;

        [Header("Decoration Interactions")] [SerializeField]
        private DecorationInteraction[] decorations;

        public List<Kite2CharacterController> characterControllers;

        private void Start()
        {
            PlayNovelSceneController playNovelSceneController = FindObjectOfType<PlayNovelSceneController>();
            characterControllers = new List<Kite2CharacterController>();

            foreach (var characterContainer in characterContainers)
            {
                Kite2CharacterController controller = characterContainer.GetComponentInChildren<Kite2CharacterController>();

                if (controller != null)
                {
                    characterControllers.Add(controller);
                    
                    controller.SetSkinSprite();
                    controller.SetHandSprite();
                    controller.SetClotheSprite();
                    controller.SetHairSprite();
                    controller.SetGlassesSprite(); 
                }
                else
                {
                    Debug.LogWarning("Character container not found");
                }
            }

            foreach (var novelStatus in GameManager.Instance.NovelSaveStatusList)
            {
                int novelId = (int)playNovelSceneController.NovelToPlay.id;
                int.TryParse(novelStatus.novelId, out int number);
                
                if (number == novelId)
                {
                    if (!novelStatus.isSaved)
                    {
                        CharacterData characterData = new CharacterData();

                        // Befülle dynamisch die Werte
                        if (characterControllers.Count > 0)
                        {
                            characterData.skinIndex = characterControllers[0].skinIndex;
                            characterData.handIndex = new HandSpriteIndex
                            {
                                colorIndex = characterControllers[0].handIndex[0],
                                spriteIndex = characterControllers[0].handIndex[1],
                            };
                            characterData.clotheIndex = characterControllers[0].clotheIndex;
                            characterData.hairIndex = characterControllers[0].hairIndex;
                            characterData.glassIndex = characterControllers[0].glassIndex;
                        }

                        if (characterControllers.Count > 1)
                        {
                            characterData.skinIndex2 = characterControllers[1].skinIndex;
                            characterData.clotheIndex2 = characterControllers[1].clotheIndex;
                            characterData.hairIndex2 = characterControllers[1].hairIndex;
                            characterData.glassIndex2 = characterControllers[1].glassIndex;
                        }

                        GameManager.Instance.AddCharacterData(novelId, characterData);
                    }

                    break;
                }
            }
        }

        public void SetCanvasRect(RectTransform canvasRect)
        {
            _canvasRect = canvasRect;
        }

        public bool HandleTouchEvent(float x, float y)
        {
            // Check if animations are allowed to proceed, return false if disabled
            if (AnimationFlagSingleton.Instance().GetFlag() == false)
            {
                return false;
            }

            foreach (var decoration in decorations)
            {
                if (decoration?.container == null || decoration?.prefab == null)
                {
                    continue;
                }

                RectTransform containerRect = decoration.container.GetComponent<RectTransform>();
                if (containerRect == null)
                {
                    continue;
                }

                Vector3[] corners = new Vector3[4];
                containerRect.GetWorldCorners(corners);
                Vector3 bottomLeft = corners[0];
                Vector3 topRight = corners[2];

                if (x >= bottomLeft.x && x <= topRight.x && y >= bottomLeft.y && y <= topRight.y)
                {
                    // Versuche ein IDecorationInteraction Interface auf dem Prefab zu finden
                    IDecorationInteraction interaction = decoration.prefab.GetComponent<IDecorationInteraction>();

                    if (interaction != null)
                    {
                        StartCoroutine(interaction.PlayInteraction(containerRect));
                        return true;
                    }
                    else
                    {
                        Debug.LogWarning($"Prefab {decoration.prefab.name} has no IDecorationInteraction component.");
                    }
                }
            }

            // Return false if the touch is outside both bounds
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
            else
                switch (characterId)
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