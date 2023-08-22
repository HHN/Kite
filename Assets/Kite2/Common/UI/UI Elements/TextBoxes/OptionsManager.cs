using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public ChatMessageBox optionA;
    public ChatMessageBox optionB;
    public ChatMessageBox optionC;
    public ChatMessageBox optionD;

    private long idA;
    private long idB;
    private long idC;
    private long idD;

    private string stringA;
    private string stringB;
    private string stringC;
    private string stringD;

    private PlayNovelSceneController sceneController;

    public void Initialize(PlayNovelSceneController sceneController, List<VisualNovelEvent> options)
    {
        this.sceneController = sceneController;

        if (options.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }
        optionA.SetMessage(options[0].text);
        idA = options[0].onChoice;
        stringA = options[0].text;
        
        if (options.Count == 1)
        {
            optionB.gameObject.SetActive(false);
            optionC.gameObject.SetActive(false);
            optionD.gameObject.SetActive(false);
            return;
        }
        optionB.SetMessage(options[1].text);
        idB = options[1].onChoice;
        stringB = options[1].text;

        if (options.Count == 2)
        {
            optionC.gameObject.SetActive(false);
            optionD.gameObject.SetActive(false);
            return;
        }
        optionC.SetMessage(options[2].text);
        idC = options[2].onChoice;
        stringC = options[2].text;

        if (options.Count == 3)
        {
            optionD.gameObject.SetActive(false);
            return;
        }
        optionD.SetMessage(options[3].text);
        idD = options[3].onChoice;
        stringD = options[3].text;
        return;
    }

    public void OnOptionA()
    {
        gameObject.SetActive(false);
        sceneController.ShowAnswer(stringA);
        sceneController.isWaitingForConfirmation = true;
        sceneController.SetNextEvent(idA);
    }

    public void OnOptionB()
    {
        gameObject.SetActive(false);
        sceneController.ShowAnswer(stringB);
        sceneController.isWaitingForConfirmation = true;
        sceneController.SetNextEvent(idB);
    }

    public void OnOptionC()
    {
        gameObject.SetActive(false);
        sceneController.ShowAnswer(stringC);
        sceneController.isWaitingForConfirmation = true;
        sceneController.SetNextEvent(idC);
    }

    public void OnOptionD()
    {
        gameObject.SetActive(false);
        sceneController.ShowAnswer(stringD);
        sceneController.isWaitingForConfirmation = true;
        sceneController.SetNextEvent(idD);
    }
}
