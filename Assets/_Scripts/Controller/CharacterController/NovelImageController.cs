using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets._Scripts._Mappings;
using Assets._Scripts.Controller.SceneControllers;
using UnityEngine;

namespace Assets._Scripts.Controller.CharacterController
{
    public interface IDecorationInteraction
    {
        IEnumerator PlayInteraction(RectTransform container);
    }

    [Serializable]
    public class DecorationInteraction
    {
        public GameObject prefab;
        public GameObject container;
    }

    public class NovelImageController : MonoBehaviour
    {
        public Kite2CharacterController novelKite2CharacterController;

        [Header("Character Setup")] [SerializeField]
        private List<Transform> characterContainers;

        [Header("Decoration Interactions")] [SerializeField]
        private DecorationInteraction[] decorations;

        public List<Kite2CharacterController> characterControllers = new();

        private void Start()
        {
            PlayNovelSceneController playNovelSceneController = FindObjectOfType<PlayNovelSceneController>();
            
            InitializeCharacters(playNovelSceneController);
            SaveCharacterDataIfNeeded(playNovelSceneController);
        }
        
        private void InitializeCharacters(PlayNovelSceneController playNovelSceneController)
        {
            var characters = playNovelSceneController.NovelToPlay.characters;
            characterControllers = new List<Kite2CharacterController>(new Kite2CharacterController[characters.Count]);

            foreach (var container in characterContainers)
            {
                string containerName = container.GetChild(0).name.Trim();

                // Suche den Index des ersten Charakters, dessen Name im Containernamen enthalten ist
                string matchedCharacter = characters.FirstOrDefault(c => !string.IsNullOrWhiteSpace(c) && containerName.IndexOf(c.Trim(), StringComparison.OrdinalIgnoreCase) >= 0);

                int matchIndex = characters.IndexOf(matchedCharacter);


                if (matchIndex == -1)
                {
                    Debug.LogWarning($"Container '{containerName}' does not match any character in NovelToPlay.characters.");
                    continue;
                }
                
                Kite2CharacterController controller = container.GetComponentInChildren<Kite2CharacterController>();

                if (controller != null)
                {
                    characterControllers[matchIndex] = controller;
                    
                    controller.SetSkinSprite();
                    controller.SetHandSprite();
                    controller.SetClotheSprite();
                    controller.SetHairSprite();
                    controller.SetGlassesSprite();
                    
                    controller.characterId = MappingManager.MapCharacter(matchedCharacter);
                }
                else
                {
                    Debug.LogWarning("Character container does not contain a Kite2CharacterController.");
                }
            }
        }
        
        private void SaveCharacterDataIfNeeded(PlayNovelSceneController playNovelSceneController)
        {
            int novelId = (int)playNovelSceneController.NovelToPlay.id;

            foreach (var status in GameManager.Instance.NovelSaveStatusList)
            {
                if (int.TryParse(status.novelId, out int id) && id == novelId)
                {
                    if (!status.isSaved)
                    {
                        var data = new CharacterData();

                        if (characterControllers.Count > 0)
                        {
                            var c0 = characterControllers[0];
                            data.skinIndex = c0.skinIndex;
                            data.handIndex = new HandSpriteIndex
                            {
                                colorIndex = c0.handIndex?[0] ?? 0,
                                spriteIndex = c0.handIndex?[1] ?? 0
                            };
                            data.clotheIndex = c0.clotheIndex;
                            data.hairIndex = c0.hairIndex;
                            data.glassIndex = c0.glassIndex;
                        }

                        if (characterControllers.Count > 1)
                        {
                            var c1 = characterControllers[1];
                            data.skinIndex2 = c1.skinIndex;
                            data.handIndex2 = new HandSpriteIndex
                            {
                                colorIndex = c1.handIndex?[0] ?? 0,
                                spriteIndex = c1.handIndex?[1] ?? 0
                            };
                            data.clotheIndex2 = c1.clotheIndex;
                            data.hairIndex2 = c1.hairIndex;
                            data.glassIndex2 = c1.glassIndex;
                        }

                        GameManager.Instance.AddCharacterData(novelId, data);
                    }
                    else
                    {
                        GameManager.Instance.CheckAndSetAllNovelsStatus();
                    }

                    break;
                }
            }
        }

        public void SetCanvasRect(RectTransform canvasRect)
        {
        }

        public bool HandleTouchEvent(float x, float y)
        {
            // Check if animations are allowed to proceed, return false if disabled
            if (!AnimationFlagSingleton.Instance().GetFlag()) return false;

            foreach (var decoration in decorations)
            {
                if (decoration?.container == null || decoration.prefab == null)
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

        public virtual void SetBackground() { }

        public virtual void SetCharacter() { }

        public virtual void DestroyCharacter() { }

        public void SetFaceExpression(int characterId, int expressionType)
        {
            var controller = characterControllers
                .FirstOrDefault(c => c != null && c.characterId == characterId);

            if (controller != null)
            {
                controller.SetFaceExpression(expressionType);
            }
            else
            {
                Debug.LogWarning($"SetFaceExpression failed: No controller found with characterId {characterId}");
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