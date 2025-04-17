using System.Collections;
using Assets._Scripts.Managers;
using Assets._Scripts.UIElements.Props;
using UnityEngine;
using UnityEngine.UI;

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

    public class NewNovelImageController : MonoBehaviour
    {
        protected RectTransform CanvasRect;
        public Kite2CharacterController novelKite2CharacterController;
        public Kite2CharacterController novelKite2CharacterController2;

        [Header("Character Setup")] [SerializeField]
        private Transform characterContainer;

        [SerializeField] private GameObject characterPrefab;

        [Header("Decoration Interactions")] [SerializeField]
        private DecorationInteraction[] decorations;

        private void Start()
        {
            novelKite2CharacterController = characterContainer.GetComponentInChildren<Kite2CharacterController>();
            if (novelKite2CharacterController == null && characterPrefab != null)
            {
                GameObject go = Instantiate(characterPrefab, characterContainer);
                novelKite2CharacterController = go.GetComponent<Kite2CharacterController>();
            }

            if (novelKite2CharacterController != null)
            {
                novelKite2CharacterController.SetSkinSprite();
                novelKite2CharacterController.SetHandSprite(); // Bei HonorarNovel: IndexOutOfBoundsException
                novelKite2CharacterController.SetClotheSprite();
                novelKite2CharacterController.SetHairSprite();

                // HandSpriteIndex handSpriteIndex = new HandSpriteIndex
                // {
                //     colorIndex = novelKite2CharacterController.handIndex[0],
                //     spriteIndex = novelKite2CharacterController.handIndex[1],
                // };

                // foreach (var novelStatus in GameManager.Instance.NovelSaveStatusList)
                // {
                //     // Wenn das Novel die gesuchte ID hat, setze den isSaved Wert
                //     int.TryParse(novelStatus.novelId, out int number);
                //     if (number == 10)
                //     {
                //         if (!novelStatus.isSaved)
                //         {
                //             GameManager.Instance.AddCharacterData(
                //                 10, // Schlüssel für den Eintrag
                //                 new CharacterData
                //                 {
                //                     skinIndex = novelKite2CharacterController.skinIndex,
                //                     handIndex = handSpriteIndex,
                //                     clotheIndex = novelKite2CharacterController.clotheIndex,
                //                     hairIndex = novelKite2CharacterController.hairIndex
                //                 }
                //             );
                //         }
                //
                //         break; // Keine Notwendigkeit mehr weiterzusuchen
                //     }
                // }
            }
        }

        public void SetCanvasRect(RectTransform canvasRect)
        {
            this.CanvasRect = canvasRect;
        }

        public bool HandleTouchEvent(float x, float y)
        {
            Debug.Log($"HandleTouchEvent called with x: {x}, y: {y}");
            // Check if animations are allowed to proceed, return false if disabled
            if (AnimationFlagSingleton.Instance().GetFlag() == false)
            {
                return false;
            }

            foreach (var decoration in decorations)
            {
                Debug.Log($"decoration.name: {decoration.container.name}");
                if (decoration.container.name.Contains("Glass"))
                {
                    // Get the RectTransforms of the objects to detect touch within their bounds
                    RectTransform decoGlassRectTransform = decoration.container.GetComponent<RectTransform>();
                    
                    // Get the world corners of the glass decoration container
                    Vector3[] cornersDecoDesk = new Vector3[4];
                    decoGlassRectTransform.GetWorldCorners(cornersDecoDesk);
                    Vector3 bottomLeftDecoDesk = cornersDecoDesk[0];
                    Vector3 topRightDecoDesk = cornersDecoDesk[2];
                    
                    // Check if the touch coordinates are within the glass decoration bounds
                    if (x >= bottomLeftDecoDesk.x && x <= topRightDecoDesk.x &&
                        y >= bottomLeftDecoDesk.y && y <= topRightDecoDesk.y)
                    {
                        StartCoroutine(decoration.prefab.GetComponent<WaterGlass>().PlayInteraction(decoGlassRectTransform)); // Trigger the glass interaction
                        return true;
                    }
                }
                else if (decoration.container.name.Contains("Plant"))
                {
                    // Get the RectTransforms of the objects to detect touch within their bounds
                    RectTransform decoPlantRectTransform = decoration.container.GetComponent<RectTransform>();

                    // Get the world corners of the plant decoration container
                    Vector3[] cornersDecoBackground = new Vector3[4];
                    decoPlantRectTransform.GetWorldCorners(cornersDecoBackground);
                    Vector3 bottomLeftDecoBackground = cornersDecoBackground[0];
                    Vector3 topRightDecoBackground = cornersDecoBackground[2];



                    // Check if the touch coordinates are within the plant decoration bounds
                    if (x >= bottomLeftDecoBackground.x && x <= topRightDecoBackground.x &&
                        y >= bottomLeftDecoBackground.y && y <= topRightDecoBackground.y)
                    {
                        StartCoroutine(decoration.prefab.GetComponent<Plant>().PlayInteraction(decoPlantRectTransform)); // Trigger the plant interaction
                        return true;
                    }
                }
                else if (decoration.container.name.Contains("Vase"))
                {
                    // Get the RectTransforms of the objects to detect touch within their bounds
                    RectTransform decoVaseRectTransform = decoration.container.GetComponent<RectTransform>();

                    // Get the world corners of the vase decoration container
                    Vector3[] cornersDecoVase = new Vector3[4];
                    decoVaseRectTransform.GetWorldCorners(cornersDecoVase);
                    Vector3 bottomLeftDecoVase = cornersDecoVase[0];
                    Vector3 topRightDecoVase = cornersDecoVase[2];

                    // Check if the touch coordinates are within the vase decoration bounds
                    if (x >= bottomLeftDecoVase.x && x <= topRightDecoVase.x &&
                        y >= bottomLeftDecoVase.y && y <= topRightDecoVase.y)
                    {
                        StartCoroutine(decoration.prefab.GetComponent<Plant>().PlayInteraction(decoVaseRectTransform));
                        return true;
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