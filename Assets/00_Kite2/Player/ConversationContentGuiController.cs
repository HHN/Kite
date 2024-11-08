using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationContentGuiController : MonoBehaviour
{
    [Header("Event Data")]
    [SerializeField] private List<VisualNovelEvent> content = new List<VisualNovelEvent>();
    [SerializeField] private List<GameObject> guiContent = new List<GameObject>();
    [SerializeField] private List<VisualNovelEvent> options = new List<VisualNovelEvent>();

    [Header("Message Prefabs")]
    [SerializeField] private GameObject blueMessagePrefab;
    [SerializeField] private GameObject greyMessagePrefab;
    [SerializeField] private GameObject turqouiseMessagePrefab;
    [SerializeField] private GameObject cottaMessagePrefab;
    [SerializeField] private GameObject blueMessagePrefabWithTrigger;
    [SerializeField] private GameObject optionsPrefab;

    [Header("UI Buttons")]
    [SerializeField] private GameObject askForFeelingsButton;

    [Header("Undo Choice UI")]
    [SerializeField] private UndoChoiceMessageBox undoChoiceMessageBoxObject;
    [SerializeField] private GameObject undoChoiceMessageBox;

    [Header("Choice Tracking")]
    private GameObject lastBlueMessagePrefabWithTrigger; // To track the last added prefab

    PlayNovelSceneController sceneController;

    public void AddContent(VisualNovelEvent novelEvent, PlayNovelSceneController controller)
    {
        sceneController = controller;

        HandleNewContent(novelEvent);
        content.Add(novelEvent);
    }

    private void HandleNewContent(VisualNovelEvent novelEvent)
    {
        VisualNovelEventType type = VisualNovelEventTypeHelper.ValueOf(novelEvent.eventType);

        switch (type)
        {
            case VisualNovelEventType.SHOW_MESSAGE_EVENT :
                {
                    ShowMessage(novelEvent);
                    break;
                }
            case VisualNovelEventType.ADD_CHOICE_EVENT:
                {
                    options.Add(novelEvent);
                    break;
                }
            case VisualNovelEventType.SHOW_CHOICES_EVENT:
                {

                    // Instantiate the options prefab and add it to the guiContent list
                    GameObject optionsObject = Instantiate(optionsPrefab, this.transform);
                    OptionsManager prefab = optionsObject.GetComponent<OptionsManager>();
                    prefab.Initialize(sceneController, options);

                    options = new List<VisualNovelEvent>();

                    // Add the optionsObject to the guiContent list
                    guiContent.Add(optionsObject);

                    break;
                }
            default:
                {
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
        messageBox.SetMessage(PlayNovelSceneController.ReplacePlaceholders(novelEvent.text, PlayManager.Instance().GetVisualNovelToPlay().GetGlobalVariables())); // TODO: Add replace
        guiContent.Add(newMessageBox);
        PromptManager.Instance().AddFormattedLineToPrompt(CharacterTypeHelper.GetNameOfCharacter(novelEvent.character), PlayNovelSceneController.ReplacePlaceholders(novelEvent.text, PlayManager.Instance().GetVisualNovelToPlay().GetGlobalVariables()));
    }

    public void ShowPlayerAnswer(string message)
    {
        GameObject newMessageBox = Instantiate(blueMessagePrefabWithTrigger, this.transform);

        ChatMessageBox messageBox = newMessageBox.GetComponent<ChatMessageBox>();
        messageBox.SetMessage(message);
        guiContent.Add(newMessageBox);

        PromptManager.Instance().AddFormattedLineToPrompt(
            CharacterTypeHelper.GetNameOfCharacter(Character.PLAYER),
            PlayNovelSceneController.ReplacePlaceholders(message, PlayManager.Instance().GetVisualNovelToPlay().GetGlobalVariables()));

        // Deaktiviere den Button der vorherigen blueMessagePrefabWithTrigger (falls vorhanden)
        if (lastBlueMessagePrefabWithTrigger != null)
        {
            DisableButtonClick(lastBlueMessagePrefabWithTrigger);
        }

        // Setze die neue Nachrichtbox als die aktuell aktive
        lastBlueMessagePrefabWithTrigger = newMessageBox; // Speichere die Referenz auf die letzte blueMessagePrefabWithTrigger

        // Stelle sicher, dass der Button der neuen Nachrichtbox aktiv ist
        EnableButtonClick(newMessageBox);
    }


    // Methode zum Deaktivieren des Button-Klicks, wobei das Prefab aktiv bleibt
    private void DisableButtonClick(GameObject messageBox)
    {
        if (messageBox.name.Contains(blueMessagePrefabWithTrigger.name))  // Prüfe, ob es sich um das spezifische Prefab handelt
        {
            Button button = messageBox.GetComponent<Button>();
            if (button != null)
            {
                button.interactable = false;  // Deaktiviere Interaktion, aber lasse den Button sichtbar
                button.onClick.RemoveListener(OnBlueMessageButtonClick);
            }
        }
    }

    // Methode zum Aktivieren des Button-Klicks für die neue Nachrichtbox
    private void EnableButtonClick(GameObject messageBox)
    {
        Button button = messageBox.GetComponent<Button>();
        if (button != null)
        {
            button.interactable = true;  // Aktiviere Interaktion für den neuen Button
            button.onClick.AddListener(OnBlueMessageButtonClick);
        }
    }

    // Methode, um den Button-Click des blueMessagePrefabWithTrigger zu verwalten
    public void OnBlueMessageButtonClick()
    {
        if (!DestroyValidator.IsNullOrDestroyed(undoChoiceMessageBoxObject))
        {
            undoChoiceMessageBoxObject.CloseMessageBox();
        }

        if (DestroyValidator.IsNullOrDestroyed(sceneController.canvas))
        {
            return;
        }

        undoChoiceMessageBoxObject = null;
        undoChoiceMessageBoxObject = Instantiate(undoChoiceMessageBox, sceneController.canvas.transform).GetComponent<UndoChoiceMessageBox>();
        undoChoiceMessageBoxObject.Activate();


        //lastBlueMessagePrefabWithTrigger.GetComponent<Animator>().SetTrigger("isBlueMessagePrefabWithTrigger");
    }

    public void ClearUIAfter(int index)
    {
        // Überprüfe, ob der Index gültig ist
        if (index > guiContent.Count || index < 0)
        {
            return;
        }

        // Lösche alle UI-Elemente, die nach dem entsprechenden Index angezeigt wurden
        for (int i = guiContent.Count - 1; i >= index; i--)
        {
            if (guiContent[i] != null)  // Prüfe, ob das UI-Element nicht bereits null ist
            {
                Destroy(guiContent[i]);  // Löscht das GameObject aus der Szene
            }
            guiContent.RemoveAt(i);  // Entfernt das Element aus der Liste, selbst wenn es null ist
        }
    }

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

    public List<VisualNovelEvent> Options
    {
        get => options;
        set => options = value;
    }
}
