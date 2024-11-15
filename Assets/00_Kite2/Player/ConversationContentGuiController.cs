using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _00_Kite2.Player;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.UI;

public class ConversationContentGuiController : MonoBehaviour
{
    [Header("Event Data")] [SerializeField]
    private List<VisualNovelEvent> content = new();

    [SerializeField] private List<GameObject> guiContent = new();
    [SerializeField] private List<VisualNovelEvent> visualNovelEvents = new();

    [Header("Message Prefabs")] [SerializeField]
    private GameObject blueMessagePrefab;

    [SerializeField] private GameObject greyMessagePrefab;
    [SerializeField] private GameObject turqouiseMessagePrefab;
    [SerializeField] private GameObject cottaMessagePrefab;
    [SerializeField] private GameObject blueMessagePrefabWithTrigger;
    [SerializeField] private GameObject optionsPrefab;

    [Header("UI Buttons")] [SerializeField]
    private GameObject askForFeelingsButton;

    [Header("Undo Choice UI")] [SerializeField]
    private UndoChoiceMessageBox undoChoiceMessageBoxObject;

    [SerializeField] private GameObject undoChoiceMessageBox;

    [Header("Choice Tracking")] private GameObject _lastBlueMessagePrefabWithTrigger; // To track the last added prefab
    private List<VisualNovelEvent> _options = new();

    private PlayNovelSceneController _sceneController;

    public List<VisualNovelEvent> Content
    {
        get => content;
        set => content = value;
    }
    
    public List<GameObject> GuiContent
    {
        get => guiContent;
        set => guiContent = value;
    }

    public List<VisualNovelEvent> VisualNovelEvents
    {
        get => visualNovelEvents;
        set => visualNovelEvents = value;
    }

    public void AddContent(VisualNovelEvent novelEvent, PlayNovelSceneController controller)
    {
        _sceneController = controller;

        HandleNewContent(novelEvent);
        content.Add(novelEvent);
    }

    private void HandleNewContent(VisualNovelEvent novelEvent)
    {
        VisualNovelEventType type = VisualNovelEventTypeHelper.ValueOf(novelEvent.eventType);

        switch (type)
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
                GameObject optionsObject = Instantiate(optionsPrefab, this.transform);
                OptionsManager prefab = optionsObject.GetComponent<OptionsManager>();
                prefab.Initialize(_sceneController, _options);

                _options = new List<VisualNovelEvent>();

                // Add the optionsObject to the guiContent list
                guiContent.Add(optionsObject);

                break;
            }
        }
    }

    private void ShowMessage(VisualNovelEvent novelEvent)
    {
        GameObject newMessageBox;
        if (novelEvent.character == CharacterTypeHelper.ToInt(Character.PLAYER))
        {
            newMessageBox = Instantiate(blueMessagePrefab, this.transform);
        }
        else if ((novelEvent.character != 0) &&
                 (novelEvent.character == CharacterTypeHelper.ToInt(Character.INTRO))
                 || novelEvent.character == CharacterTypeHelper.ToInt(Character.OUTRO)
                 || novelEvent.character == CharacterTypeHelper.ToInt(Character.INFO))
        {
            newMessageBox = Instantiate(cottaMessagePrefab, this.transform);
        }
        else
        {
            newMessageBox = Instantiate(greyMessagePrefab, this.transform);
        }

        ChatMessageBox messageBox = newMessageBox.GetComponent<ChatMessageBox>();
        messageBox.SetMessage(PlayNovelSceneController.ReplacePlaceholders(novelEvent.text,
            PlayManager.Instance().GetVisualNovelToPlay().GetGlobalVariables()));
        guiContent.Add(newMessageBox);
        PromptManager.Instance().AddFormattedLineToPrompt(CharacterTypeHelper.GetNameOfCharacter(novelEvent.character),
            PlayNovelSceneController.ReplacePlaceholders(novelEvent.text,
                PlayManager.Instance().GetVisualNovelToPlay().GetGlobalVariables()));
    }

    public void ShowPlayerAnswer(string message)
    {
        GameObject newMessageBox = Instantiate(blueMessagePrefabWithTrigger, this.transform);

        ChatMessageBox messageBox = newMessageBox.GetComponent<ChatMessageBox>();
        messageBox.SetMessage(message);
        guiContent.Add(newMessageBox);

        PromptManager.Instance().AddFormattedLineToPrompt(
            CharacterTypeHelper.GetNameOfCharacter(Character.PLAYER),
            PlayNovelSceneController.ReplacePlaceholders(message,
                PlayManager.Instance().GetVisualNovelToPlay().GetGlobalVariables()));

        // Deaktiviere den Button der vorherigen blueMessagePrefabWithTrigger (falls vorhanden)
        if (_lastBlueMessagePrefabWithTrigger != null)
        {
            DisableButtonClick(_lastBlueMessagePrefabWithTrigger);
        }

        // Setze die neue Nachrichtbox als die aktuell aktive
        _lastBlueMessagePrefabWithTrigger =
            newMessageBox; // Speichere die Referenz auf die letzte blueMessagePrefabWithTrigger

        // Stelle sicher, dass der Button der neuen Nachrichtbox aktiv ist
        EnableButtonClick(newMessageBox);
    }


    // Methode zum Deaktivieren des Button-Klicks, wobei das Prefab aktiv bleibt
    private void DisableButtonClick(GameObject messageBox)
    {
        if (messageBox.name.Contains(blueMessagePrefabWithTrigger
                .name)) // Pr�fe, ob es sich um das spezifische Prefab handelt
        {
            Button button = messageBox.GetComponent<Button>();
            if (button != null)
            {
                button.interactable = false; // Deaktiviere Interaktion, aber lasse den Button sichtbar
                button.onClick.RemoveListener(OnBlueMessageButtonClick);
            }
        }
    }

    // Methode zum Aktivieren des Button-Klicks f�r die neue Nachrichtbox
    private void EnableButtonClick(GameObject messageBox)
    {
        Button button = messageBox.GetComponent<Button>();
        if (button != null)
        {
            button.interactable = true; // Aktiviere Interaktion f�r den neuen Button
            button.onClick.AddListener(OnBlueMessageButtonClick);
        }
    }

    // Methode, um den Button-Click des blueMessagePrefabWithTrigger zu verwalten
    private void OnBlueMessageButtonClick()
    {
        if (!undoChoiceMessageBoxObject.IsNullOrDestroyed())
        {
            undoChoiceMessageBoxObject.CloseMessageBox();
        }

        if (_sceneController.canvas.IsNullOrDestroyed())
        {
            return;
        }

        undoChoiceMessageBoxObject = null;
        undoChoiceMessageBoxObject = Instantiate(undoChoiceMessageBox, _sceneController.canvas.transform)
            .GetComponent<UndoChoiceMessageBox>();
        undoChoiceMessageBoxObject.Activate();


        //lastBlueMessagePrefabWithTrigger.GetComponent<Animator>().SetTrigger("isBlueMessagePrefabWithTrigger");
    }

    public void ClearUIAfter(int index)
    {
        // �berpr�fe, ob der Index g�ltig ist
        if (index > guiContent.Count || index < 0)
        {
            return;
        }

        // L�sche alle UI-Elemente, die nach dem entsprechenden Index angezeigt wurden
        for (int i = guiContent.Count - 1; i >= index; i--)
        {
            if (guiContent[i] != null) // Pr�fe, ob das UI-Element nicht bereits null ist
            {
                Destroy(guiContent[i]); // L�scht das GameObject aus der Szene
            }

            guiContent.RemoveAt(i); // Entfernt das Element aus der Liste, selbst wenn es null ist
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
        
        GameManager.Instance.calledFromReload = true;

        // Durchlaufe jedes Event in visualNovelEvents und erstelle das entsprechende GUI-Element
        for (int i = 0; i < savedData.visualNovelEvents.Count; i++)
        {
            VisualNovelEvent visualNovelEvent = savedData.visualNovelEvents[i];

            GameObject newMessageBox;

            if (savedData.messageType[i].Contains("Blue Message Prefab With Trigger(Clone)"))
            {
                newMessageBox = Instantiate(blueMessagePrefabWithTrigger, this.transform);
            }

            // Bestimme den Typ des Prefabs basierend auf dem Charaktertyp und dem Ereignistyp
            else if (visualNovelEvent.character == CharacterTypeHelper.ToInt(Character.PLAYER))
            {
                newMessageBox = Instantiate(blueMessagePrefab, this.transform);
            }
            else if (visualNovelEvent.character == CharacterTypeHelper.ToInt(Character.INTRO) ||
                     visualNovelEvent.character == CharacterTypeHelper.ToInt(Character.OUTRO) ||
                     visualNovelEvent.character == CharacterTypeHelper.ToInt(Character.INFO))
            {
                newMessageBox = Instantiate(cottaMessagePrefab, this.transform);
            }
            else
            {
                newMessageBox = Instantiate(greyMessagePrefab, this.transform);
            }
            
            SetText(newMessageBox, visualNovelEvent.text);
        }
        
        // GameManager.Instance.calledFromReload = false;
        Debug.Log("Ende");
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
            chatMessageBox.textBox.text = ""; // Stelle sicher, dass der Text leer ist
            chatMessageBox.textBox.text = text;
        }
            
        // Aktiviere die MessageBox, nachdem der Text vollständig angezeigt wurde
        // messageBox.SetActive(true);
        
        // TypewriterCore typewriterCore = messageBox.GetComponentInChildren<TypewriterCore>();
        // typewriterCore.IsShowingText = true;
        // typewriterCore.SkipTypewriter();
    }
}