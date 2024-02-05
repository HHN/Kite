using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationContentGuiController : MonoBehaviour
{
    public List<VisualNovelEvent> content = new List<VisualNovelEvent>();
    public List<GameObject> guiContent = new List<GameObject>();
    public List<VisualNovelEvent> options = new List<VisualNovelEvent>();

    public GameObject blueMessagePrefab;
    public GameObject greyMessagePrefab;
    public GameObject turqouiseMessagePrefab;
    public GameObject cottaMessagePrefab;
    public GameObject optionsPrefab;
    public GameObject askForFeelingsButton;
    public GameObject blueMessagePrefabWithTrigger;

    public void AddContent(VisualNovelEvent novelEvent, PlayNovelSceneController controller)
    {
        HandleNewContent(novelEvent, controller);
        content.Add(novelEvent);
    }

    private void HandleNewContent(VisualNovelEvent novelEvent, PlayNovelSceneController controller)
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
                    OptionsManager prefab = Instantiate(optionsPrefab, this.transform).GetComponent<OptionsManager>();
                    prefab.Initialize(controller, options);
                    options = new List<VisualNovelEvent>();
                    break;
                }
            case VisualNovelEventType.ASK_FOR_OPINION_EVENT:
                {
                    AskForFeelingsButton prefab = Instantiate(askForFeelingsButton, this.transform).GetComponent<AskForFeelingsButton>();
                    prefab.controller = controller;
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
        if (PlayManager.Instance().GetVisualNovelToPlay().nameOfMainCharacter == novelEvent.name)
        {
            newMessageBox = Instantiate(blueMessagePrefab, this.transform);
        }
        else if (novelEvent.name == "Intro" || novelEvent.name == "Outro" || novelEvent.name == "Info" || novelEvent.name == "info")
        {
            newMessageBox = Instantiate(cottaMessagePrefab, this.transform);
        }
        else
        {
            newMessageBox = Instantiate(greyMessagePrefab, this.transform);
        }
        ChatMessageBox messageBox = newMessageBox.GetComponent<ChatMessageBox>();
        messageBox.SetMessage(novelEvent.text);
        guiContent.Add(newMessageBox);
        PromptManager.Instance().AddLineToPrompt(novelEvent.name + ": " + novelEvent.text);
    }

    public void ShowPlayerAnswer(string message)
    {
        GameObject newMessageBox = Instantiate(blueMessagePrefabWithTrigger, this.transform);
        ChatMessageBox messageBox = newMessageBox.GetComponent<ChatMessageBox>();
        // Debug.Log("Message: " + message);
        messageBox.SetMessage(message);
        guiContent.Add(newMessageBox);
        PromptManager.Instance().AddLineToPrompt(
            PlayManager.Instance().GetVisualNovelToPlay().nameOfMainCharacter + ": " + message);
    }
}
