using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets._Scripts._Mappings;
using Assets._Scripts.Controller.SceneControllers;
using UnityEngine;

namespace Assets._Scripts.Controller.CharacterController
{
    /// <summary>
    /// Represents an interaction mechanism for UI decoration elements in a scene.
    /// Classes implementing this interface should define how the interaction is executed,
    /// such as animating or manipulating the decoration within a specified container.
    /// </summary>
    public interface IDecorationInteraction
    {
        IEnumerator PlayInteraction(RectTransform container);
    }

    /// <summary>
    /// Defines a collectible interaction mechanism for managing UI decoration elements
    /// with specified prefabricated objects and container associations.
    /// This class serves to facilitate decoration, instantiation, and management during interactions.
    /// </summary>
    [Serializable]
    public class DecorationInteraction
    {
        public GameObject prefab;
        public GameObject container;
    }

    /// <summary>
    /// Manages and controls the novel-style image-based character interactions and events.
    /// Provides functionality for setting up character visuals, controlling character expressions,
    /// handling user interactions, and managing character states like talking animations or background settings.
    /// This class acts as a critical part in visual novel scene control, enabling dynamic character
    /// behavior within the context of a Unity-specific rendering environment.
    /// </summary>
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

        /// <summary>
        /// Initializes the character controllers for the characters in the specified visual novel.
        /// </summary>
        /// <param name="playNovelSceneController">
        /// The scene controller that contains the visual novel data, including characters to be initialized.
        /// </param>
        private void InitializeCharacters(PlayNovelSceneController playNovelSceneController)
        {
            var characters = playNovelSceneController.NovelToPlay.characters;
            characterControllers = new List<Kite2CharacterController>(new Kite2CharacterController[characters.Count]);

            foreach (var container in characterContainers)
            {
                string containerName = container.GetChild(0).name.Trim();

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

        /// <summary>
        /// Saves character data for the current novel if it has not yet been saved.
        /// Updates the save status of the novel accordingly.
        /// </summary>
        /// <param name="playNovelSceneController">
        /// The scene controller providing the novel data, which is checked against save statuses to determine if character data saving is required.
        /// </param>
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

        /// <summary>
        /// Handles a touch event by checking if it interacts with any decorations within specified coordinates.
        /// </summary>
        /// <param name="x"> The x-coordinate of the touch event. </param>
        /// <param name="y"> The y-coordinate of the touch event. </param>
        /// <returns>
        /// A boolean value indicating whether the touch event successfully interacted with a decoration.
        /// </returns>
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

        /// <summary>
        /// Sets the background image for the current scene in the visual novel.
        /// This method is responsible for updating the visual representation of the background within the novel's scene.
        /// </summary>
        public virtual void SetBackground() { }

        /// <summary>
        /// Sets up or initializes a character within the visual novel scene.
        /// This method is responsible for configuring character visuals,
        /// states, and other properties required for their interaction within the current scene.
        /// </summary>
        public virtual void SetCharacter() { }

        /// <summary>
        /// Removes and cleans up a character from the scene, ensuring all related resources
        /// and references are properly released or reset.
        /// </summary>
        public virtual void DestroyCharacter() { }

        /// <summary>
        /// Sets the face expression for a specific character identified by its character ID.
        /// </summary>
        /// <param name="characterId"> The unique identifier of the character whose face expression is to be updated. </param>
        /// <param name="expressionType"> The type of expression that should be applied to the character's face. </param>
        public void SetFaceExpression(int characterId, int expressionType)
        {
            var controller = characterControllers.FirstOrDefault(c => c != null && c.characterId == characterId);

            if (controller != null)
            {
                controller.SetFaceExpression(expressionType);
            }
            else
            {
                Debug.LogWarning($"SetFaceExpression failed: No controller found with characterId {characterId}");
            }
        }

        /// <summary>
        /// Initiates the talking animation for the main character within the visual novel scene.
        /// </summary>
        /// <remarks>
        /// This method triggers the `StartTalking` method of the primary character controller (if available),
        /// enabling the character's talking behavior. If the main character controller is not initialized, it performs no action.
        /// </remarks>
        public virtual void StartCharacterTalking()
        {
            if (novelKite2CharacterController == null)
            {
                return;
            }

            novelKite2CharacterController.StartTalking();
        }

        /// <summary>
        /// Stops the talking state of the active character within the visual novel scene.
        /// </summary>
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