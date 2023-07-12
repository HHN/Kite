using System.Collections.Generic;
using UnityEngine;

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
        else if (novelEvent.name == "Intro" || novelEvent.name == "Outro" || novelEvent.name == "Info")
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
    }

    public void ShowPlayerAnswer(string message)
    {
        GameObject newMessageBox = Instantiate(blueMessagePrefab, this.transform);
        ChatMessageBox messageBox = newMessageBox.GetComponent<ChatMessageBox>();
        messageBox.SetMessage(message);
        guiContent.Add(newMessageBox);
    }
}
