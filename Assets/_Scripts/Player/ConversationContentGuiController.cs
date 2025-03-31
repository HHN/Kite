using System.Collections.Generic;
using System.Linq;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.SaveNovelData;
using Assets._Scripts.UI_Elements.TextBoxes;
using Assets._Scripts.UndoChoice;
using Assets._Scripts.Utilities;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Animator._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Typewriter.Built_in;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Player
{
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

        [Header("UI Buttons")] [SerializeField]
        private GameObject askForFeelingsButton;

        [Header("Undo Choice UI")] [SerializeField]
        private UndoChoiceMessageBox undoChoiceMessageBoxObject;

        [SerializeField] private GameObject undoChoiceMessageBox;

        [Header("Choice Tracking")]
        private GameObject _lastBlueMessagePrefabWithTrigger; // To track the last added prefab

        private List<VisualNovelEvent> _options = new();

        private PlayNovelSceneController _sceneController;

        public List<VisualNovelEvent> Content { get => content; set => content = value; }
        public List<GameObject> GuiContent { get => guiContent; set => guiContent = value; }
        public List<VisualNovelEvent> VisualNovelEvents { get => visualNovelEvents; set => visualNovelEvents = value; }

        private void Start()
        {
            _sceneController = FindObjectOfType<PlayNovelSceneController>();
        }

        public void AddContent(VisualNovelEvent novelEvent, PlayNovelSceneController controller)
        {
            HandleNewContent(novelEvent);
            content.Add(novelEvent);
        }

        private void HandleNewContent(VisualNovelEvent novelEvent)
        {
            switch (VisualNovelEventTypeHelper.ValueOf(novelEvent.eventType))
            {
                case VisualNovelEventType.SHOW_MESSAGE_EVENT:
                {
                    visualNovelEvents.Add(novelEvent);
                    ShowMessage(novelEvent);
                    break;
                }
                case VisualNovelEventType.ADD_CHOICE_EVENT:
                {
                    _options.Add(novelEvent);
                    break;
                }
                case VisualNovelEventType.SHOW_CHOICES_EVENT:
                {
                    // Instantiate the options prefab and add it to the guiContent list
                    GameObject optionsObject = Instantiate(optionsPrefab, transform);
                    optionsObject.GetComponent<OptionsManager>().Initialize(_sceneController, _options);

                    _options = new List<VisualNovelEvent>();

                    guiContent.Add(optionsObject);

                    break;
                }
            }
        }

        private void ShowMessage(VisualNovelEvent novelEvent)
        {
            var newMessageBox = GetMessagePrefab(novelEvent);

            ChatMessageBox messageBox = newMessageBox.GetComponent<ChatMessageBox>();
            messageBox.SetMessage(PlayNovelSceneController.ReplacePlaceholders(novelEvent.text,
                PlayManager.Instance().GetVisualNovelToPlay().GetGlobalVariables()));
            
            guiContent.Add(newMessageBox);
            AddFormattedPromptLine(novelEvent.character, novelEvent.text);
        }

        public void ShowPlayerAnswer(string message)
        {
            GameObject newMessageBox = Instantiate(blueMessagePrefabWithTrigger, this.transform);

            ChatMessageBox messageBox = newMessageBox.GetComponent<ChatMessageBox>();
            messageBox.SetMessage(message);
            guiContent.Add(newMessageBox);

            AddFormattedPromptLine(CharacterTypeHelper.ToInt(CharacterRole.Player), message);

            // Deaktiviere den Button der vorherigen blueMessagePrefabWithTrigger (falls vorhanden)
            if (_lastBlueMessagePrefabWithTrigger != null)
            {
                SetButtonState(_lastBlueMessagePrefabWithTrigger, false);
            }

            // Setze die neue MessageBox als die aktuell aktive
            _lastBlueMessagePrefabWithTrigger =
                newMessageBox; // Speichere die Referenz auf die letzte blueMessagePrefabWithTrigger

            // Stelle sicher, dass der Button der neuen MessageBox aktiv ist
            SetButtonState(newMessageBox, true);
        }

        // Methode zum Deaktivieren des Button-Klicks, wobei das Prefab aktiv bleibt
        private void SetButtonState(GameObject messageBox, bool enable)
        {
            // Pr�fe, ob es sich um das spezifische Prefab handelt
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

        // Methode, um den Button-Click des blueMessagePrefabWithTrigger zu verwalten
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
            undoChoiceMessageBoxObject = Instantiate(undoChoiceMessageBox, _sceneController.canvas.transform)
                .GetComponent<UndoChoiceMessageBox>();
            undoChoiceMessageBoxObject.Activate();
        }

        public void ClearUIAfter(int index, int count)
        {
            int optionsToChooseFromCount = 0;

            // Überprüfe, ob der Index gültig ist
            if (index > guiContent.Count || index < 0) return;

            int guiIndex = (count != 0) ? index - count : index;

            // Lösche alle UI-Elemente, die nach dem entsprechenden Index angezeigt wurden
            for (int i = guiContent.Count - 1; i > guiIndex; i--)
            {
                if (guiContent[i] != null) // Pr�fe, ob das UI-Element nicht bereits null ist
                {
                    if (guiContent[i].name.Contains("OptionsToChooseFrom")) optionsToChooseFromCount++;

                    Destroy(guiContent[i]); // Löscht das GameObject aus der Szene
                }

                guiContent.RemoveAt(i); // Entfernt das Element aus der Liste, selbst wenn es null ist
            }

            if (count != 0) optionsToChooseFromCount = count;

            // Lösche alle UI-Elemente, die nach dem entsprechenden Index angezeigt wurden
            for (int i = visualNovelEvents.Count - 1; i > index - optionsToChooseFromCount; i--)
            {
                visualNovelEvents.RemoveAt(i); // Entfernt das Element aus der Liste, selbst wenn es null ist
            }
        }

        public void ReconstructGuiContent(NovelSaveData savedData)
        {
            // Entferne alle aktuellen GUI-Elemente in der Szene
            foreach (var guiElement in guiContent.Where(guiElement => guiElement != null))
            {
                Destroy(guiElement);
            }

            guiContent.Clear();

            content = savedData.content;
            visualNovelEvents = savedData.visualNovelEvents;

            // Durchlaufe jedes Event in visualNovelEvents und erstelle das entsprechende GUI-Element
            for (int i = 0; i <= savedData.visualNovelEvents.Count - 1; i++)
            {
                VisualNovelEvent visualNovelEvent = savedData.visualNovelEvents[i];

                if (visualNovelEvent.id.Contains("OptionsLabel") && visualNovelEvent.character == 0)
                {
                    visualNovelEvent.character = 1;
                }
                
                AddFormattedPromptLine(visualNovelEvent.character, visualNovelEvent.text);

                GameObject newMessageBox;

                if (savedData.messageType[i].Contains("Blue Message Prefab With Trigger(Clone)"))
                {
                    newMessageBox = Instantiate(blueMessagePrefabWithTrigger, transform);
                    newMessageBox.SetActive(false);

                    // Deaktiviere den Button der vorherigen blueMessagePrefabWithTrigger (falls vorhanden)
                    if (_lastBlueMessagePrefabWithTrigger != null)
                    {
                        SetButtonState(_lastBlueMessagePrefabWithTrigger, false);
                    }

                    // Setze die neue MessageBox als die aktuell aktive
                    _lastBlueMessagePrefabWithTrigger =
                        newMessageBox; // Speichere die Referenz auf die letzte blueMessagePrefabWithTrigger

                    // Stelle sicher, dass der Button der neuen MessageBox aktiv ist
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

        private void SetText(GameObject messageBox, string text)
        {
            // Setze das MessageBox-GameObject zunächst auf unsichtbar
            messageBox.SetActive(false);
            guiContent.Add(messageBox);

            // Setze den Text in der MessageBox und starte den Typewriter-Effekt
            ChatMessageBox chatMessageBox = messageBox.GetComponent<ChatMessageBox>();
            if (messageBox != null)
            {
                chatMessageBox.GetComponentInChildren<TAnimCore>().SetText(text);
                chatMessageBox.GetComponentInChildren<TypewriterByCharacter>().useTypeWriter = false;
            }
        }
        
        private static void AddFormattedPromptLine(int character, string message)
        {
            PromptManager.Instance().AddFormattedLineToPrompt(
                CharacterTypeHelper.GetNameOfCharacter(character),
                PlayNovelSceneController.ReplacePlaceholders(message,
                    PlayManager.Instance().GetVisualNovelToPlay().GetGlobalVariables()));
        }
        
        private GameObject GetMessagePrefab(VisualNovelEvent novelEvent)
        {
            GameObject newMessageBox;
            if (novelEvent.character == CharacterTypeHelper.ToInt(CharacterRole.Player))
            {
                newMessageBox = Instantiate(blueMessagePrefab, transform);
            }
            else if (novelEvent.character != 0 &&
                     novelEvent.character == CharacterTypeHelper.ToInt(CharacterRole.Intro) || 
                     novelEvent.character == CharacterTypeHelper.ToInt(CharacterRole.Outro) || 
                     novelEvent.character == CharacterTypeHelper.ToInt(CharacterRole.Info))
            {
                newMessageBox = Instantiate(cottaMessagePrefab, transform);
            }
            else
            {
                newMessageBox = Instantiate(greyMessagePrefab, transform);
            }

            return newMessageBox;
        }
    }
}