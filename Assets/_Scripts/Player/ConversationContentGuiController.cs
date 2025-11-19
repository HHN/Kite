using System.Collections.Generic;
using System.Linq;
using Assets._Scripts._Mappings;
using Assets._Scripts.Controller.SceneControllers;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.SaveNovelData;
using Assets._Scripts.UIElements.TextBoxes;
using Assets._Scripts.UndoChoice;
using Assets._Scripts.Utilities;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Animator._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Typewriter.Built_in;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Player
{
    /// <summary>
    /// Controls the display and interaction of conversational content within the visual novel's UI.
    /// It manages message boxes, choices, and handles the dynamic construction and reconstruction
    /// of the conversation flow.
    /// </summary>
    public class ConversationContentGuiController : MonoBehaviour
    {
        [Header("Event Data")]
        [SerializeField] private List<VisualNovelEvent> content = new();
        [SerializeField] private List<GameObject> guiContent = new();
        [SerializeField] private List<VisualNovelEvent> visualNovelEvents = new();

        [Header("Message Prefabs")]
        [SerializeField] private GameObject blueMessagePrefab;
        [SerializeField] private GameObject greyMessagePrefab;
        [SerializeField] private GameObject turquoiseMessagePrefab;
        [SerializeField] private GameObject cottaMessagePrefab;
        [SerializeField] private GameObject blueMessagePrefabWithTrigger;
        [SerializeField] private GameObject optionsPrefab;

        [Header("UI Buttons")] 
        [SerializeField] private GameObject askForFeelingsButton;

        [Header("Undo Choice UI")] 
        [SerializeField] private UndoChoiceMessageBox undoChoiceMessageBoxObject;
        [SerializeField] private GameObject undoChoiceMessageBox;

        [Header("Choice Tracking")]
        private GameObject _lastBlueMessagePrefabWithTrigger;

        private List<VisualNovelEvent> _options = new();

        private PlayNovelSceneController _sceneController;

        public List<VisualNovelEvent> Content { get => content; set => content = value; }
        public List<GameObject> GuiContent { get => guiContent; set => guiContent = value; }
        public List<VisualNovelEvent> VisualNovelEvents { get => visualNovelEvents; set => visualNovelEvents = value; }

        /// <summary>
        /// Initializes the controller by finding a reference to the <see cref="PlayNovelSceneController"/>
        /// in the scene.
        /// </summary>
        private void Start()
        {
            _sceneController = FindObjectOfType<PlayNovelSceneController>();
        }

        /// <summary>
        /// Adds a new <see cref="VisualNovelEvent"/> to the conversation content and handles its display.
        /// </summary>
        /// <param name="novelEvent">The <see cref="VisualNovelEvent"/> to add and display.</param>
        /// <param name="controller">A reference to the <see cref="PlayNovelSceneController"/> managing the novel.</param>
        public void AddContent(VisualNovelEvent novelEvent, PlayNovelSceneController controller)
        {
            HandleNewContent(novelEvent);
            content.Add(novelEvent);
        }

        /// <summary>
        /// Processes a new <see cref="VisualNovelEvent"/> based on its <see cref="VisualNovelEvent.eventType"/>.
        /// This method dispatches events to appropriate handling functions like displaying messages or choices.
        /// </summary>
        /// <param name="novelEvent">The <see cref="VisualNovelEvent"/> to handle.</param>
        private void HandleNewContent(VisualNovelEvent novelEvent)
        {
            switch (VisualNovelEventTypeHelper.ValueOf(novelEvent.eventType))
            {
                case VisualNovelEventType.ShowMessageEvent:
                {
                    visualNovelEvents.Add(novelEvent);
                    ShowMessage(novelEvent);
                    break;
                }
                case VisualNovelEventType.AddChoiceEvent:
                {
                    _options.Add(novelEvent);
                    break;
                }
                case VisualNovelEventType.ShowChoicesEvent:
                {
                    GameObject optionsObject = Instantiate(optionsPrefab, transform);
                    optionsObject.GetComponent<OptionsManager>().Initialize(_sceneController, _options);

                    _options = new List<VisualNovelEvent>();

                    guiContent.Add(optionsObject);

                    break;
                }
            }
        }

        /// <summary>
        /// Displays a message box for the given <see cref="VisualNovelEvent"/>.
        /// It selects the appropriate prefab based on character type and sets the message text,
        /// then adds the message box to the GUI content.
        /// </summary>
        /// <param name="novelEvent">The <see cref="VisualNovelEvent"/> containing message details.</param>
        private void ShowMessage(VisualNovelEvent novelEvent)
        {
            var newMessageBox = GetMessagePrefab(novelEvent);

            ChatMessageBox messageBox = newMessageBox.GetComponent<ChatMessageBox>();
            messageBox.SetMessage(PlayNovelSceneController.ReplacePlaceholders(novelEvent.text, PlayManager.Instance().GetVisualNovelToPlay().GetGlobalVariables()));

            guiContent.Add(newMessageBox);

            AddFormattedPromptLine(MappingManager.MapCharacterToString(novelEvent.character), novelEvent.text);
        }

        /// <summary>
        /// Displays the player's answer in a blue message box with a trigger button.
        /// It also manages the interactability of previous player answer buttons.
        /// </summary>
        /// <param name="message">The message string representing the player's answer.</param>
        public void ShowPlayerAnswer(string message)
        {
            GameObject newMessageBox = Instantiate(blueMessagePrefabWithTrigger, this.transform);

            ChatMessageBox messageBox = newMessageBox.GetComponent<ChatMessageBox>();
            messageBox.SetMessage(message);
            guiContent.Add(newMessageBox);

            AddFormattedPromptLine(MappingManager.MapCharacterToString(1), message);

            if (_lastBlueMessagePrefabWithTrigger != null)
            {
                SetButtonState(_lastBlueMessagePrefabWithTrigger, false);
            }

            _lastBlueMessagePrefabWithTrigger = newMessageBox;

            SetButtonState(newMessageBox, true);
        }

        /// <summary>
        /// Sets the interactable state of a button component found on a message box GameObject.
        /// It also manages adding or removing the click listener.
        /// </summary>
        /// <param name="messageBox">The GameObject of the message box.</param>
        /// <param name="enable">True to enable the button, false to disable it.</param>
        private void SetButtonState(GameObject messageBox, bool enable)
        {
            if (messageBox.name.Contains(blueMessagePrefabWithTrigger.name))
            {
                Button button = messageBox.GetComponent<Button>();
                if (button != null)
                {
                    button.interactable = enable;
                    button.onClick.RemoveAllListeners();
                    if (enable) button.onClick.AddListener(OnBlueMessageButtonClick); 
                }
            }
        }

        /// <summary>
        /// Handles the click event on a blue message button.
        /// It opens or re-opens the Undo Choice message box.
        /// </summary>
        private void OnBlueMessageButtonClick()
        {
            if (!undoChoiceMessageBoxObject.IsNullOrDestroyed())
            {
                undoChoiceMessageBoxObject.CloseMessageBox();
            }

            if (DestroyValidator.IsNullOrDestroyed(_sceneController.canvas))
            {
                return;
            }

            undoChoiceMessageBoxObject = null;
            undoChoiceMessageBoxObject = Instantiate(undoChoiceMessageBox, _sceneController.canvas.transform).GetComponent<UndoChoiceMessageBox>();
            undoChoiceMessageBoxObject.Activate();
        }

        /// <summary>
        /// Clears a specified range of UI elements and corresponding <see cref="VisualNovelEvent"/>s
        /// from the conversation history, typically used for undoing choices.
        /// </summary>
        /// <param name="index">The starting index (inclusive) from which to clear the GUI content.</param>
        /// <param name="count">The number of items to clear, or 0 to clear up to the end.</param>
        public void ClearUIAfter(int index, int count)
        {
            int optionsToChooseFromCount = 0;

            if (index > guiContent.Count || index < 0) return;

            int guiIndex = (count != 0) ? index - count : index;

            for (int i = guiContent.Count - 1; i > guiIndex; i--)
            {
                if (guiContent[i] != null)
                {
                    if (guiContent[i].name.Contains("OptionsToChooseFrom")) optionsToChooseFromCount++;

                    Destroy(guiContent[i]);
                }

                guiContent.RemoveAt(i);
            }

            if (count != 0) optionsToChooseFromCount = count;

            for (int i = visualNovelEvents.Count - 1; i > index - optionsToChooseFromCount; i--)
            {
                visualNovelEvents.RemoveAt(i);
            }
        }

        /// <summary>
        /// Reconstructs the entire GUI conversation content based on saved data.
        /// This method is used when loading a saved game state.
        /// </summary>
        /// <param name="savedData">The <see cref="NovelSaveData"/> containing the saved conversation history.</param>
        public void ReconstructGuiContent(NovelSaveData savedData)
        {
            foreach (var guiElement in guiContent.Where(guiElement => guiElement != null))
            {
                Destroy(guiElement);
            }

            guiContent.Clear();

            content = savedData.content;
            visualNovelEvents = savedData.visualNovelEvents;

            for (int i = 0; i <= savedData.visualNovelEvents.Count - 1; i++)
            {
                VisualNovelEvent visualNovelEvent = savedData.visualNovelEvents[i];

                if (visualNovelEvent.id.Contains("OptionsLabel") && visualNovelEvent.character == 0)
                {
                    visualNovelEvent.character = 1;
                }

                AddFormattedPromptLine(MappingManager.MapCharacterToString(visualNovelEvent.character), visualNovelEvent.text);

                GameObject newMessageBox;

                if (savedData.messageType[i].Contains("Blue Message Prefab With Trigger(Clone)"))
                {
                    newMessageBox = Instantiate(blueMessagePrefabWithTrigger, transform);
                    newMessageBox.SetActive(false); // Initially set inactive.

                    if (_lastBlueMessagePrefabWithTrigger != null)
                    {
                        SetButtonState(_lastBlueMessagePrefabWithTrigger, false);
                    }

                    _lastBlueMessagePrefabWithTrigger = newMessageBox;
                    SetButtonState(newMessageBox, true);
                }
                else
                {
                    newMessageBox = GetMessagePrefab(visualNovelEvent);
                    newMessageBox.SetActive(true);
                }

                SetText(newMessageBox, visualNovelEvent.text);
            }

            _sceneController.ScrollToBottom();
        }

        /// <summary>
        /// Sets the text content for a given message box GameObject.
        /// It also prepares the message box for potential typewriter effects if needed.
        /// </summary>
        /// <param name="messageBox">The GameObject of the message box.</param>
        /// <param name="text">The string text to set.</param>
        private void SetText(GameObject messageBox, string text)
        {
            messageBox.SetActive(false); // Set inactive initially to prevent flickering during text setup.
            guiContent.Add(messageBox); // Add to the list of GUI content.

            ChatMessageBox chatMessageBox = messageBox.GetComponent<ChatMessageBox>();
            if (messageBox != null) // Redundant check, as messageBox is the input parameter.
            {
                // Set text using Text Animator's TAnimCore and disable the typewriter effect initially.
                chatMessageBox.GetComponentInChildren<TAnimCore>().SetText(text);
                chatMessageBox.GetComponentInChildren<TypewriterByCharacter>().useTypeWriter = false;
            }
        }

        /// <summary>
        /// Adds a formatted line to the global prompt history maintained by the <see cref="PromptManager"/>.
        /// Placeholders in the message text are replaced with their actual values.
        /// </summary>
        /// <param name="character">The string representation of the character (e.g., "Player", "Info").</param>
        /// <param name="message">The message text to add to the prompt history.</param>
        private static void AddFormattedPromptLine(string character, string message)
        {
            PromptManager.Instance().AddFormattedLineToPrompt(character, PlayNovelSceneController.ReplacePlaceholders(message, PlayManager.Instance().GetVisualNovelToPlay().GetGlobalVariables()));
        }

        /// <summary>
        /// Determines and instantiates the correct message box prefab based on the character type
        /// defined in the <see cref="VisualNovelEvent"/>.
        /// </summary>
        /// <param name="novelEvent">The <see cref="VisualNovelEvent"/> to get the prefab for.</param>
        /// <returns>A new GameObject instance of the appropriate message box prefab.</returns>
        private GameObject GetMessagePrefab(VisualNovelEvent novelEvent)
        {
            GameObject newMessageBox;

            if (novelEvent.character == 1)
            {
                newMessageBox = Instantiate(blueMessagePrefab, transform);
            }
            else if (novelEvent.character != 0 && // 'None'
                     novelEvent.character == 2 || // 'Intro' 
                     novelEvent.character == 3 || // 'Outro'
                     novelEvent.character == 4)   // 'Info' 
            {
                newMessageBox = Instantiate(cottaMessagePrefab, transform);
            }
            else // Default case for other characters.
            {
                newMessageBox = Instantiate(greyMessagePrefab, transform);
            }

            return newMessageBox;
        }
    }
}