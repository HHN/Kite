using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public ChatMessageBox optionA;
    public ChatMessageBox optionB;
    public ChatMessageBox optionC;
    public ChatMessageBox optionD;
    public ChatMessageBox optionE;

    private long idA;
    private long idB;
    private long idC;
    private long idD;
    private long idE;

    private bool displayAfterSelectionA;
    private bool displayAfterSelectionB;
    private bool displayAfterSelectionC;
    private bool displayAfterSelectionD;
    private bool displayAfterSelectionE;

    private string stringA;
    private string stringB;
    private string stringC;
    private string stringD;
    private string stringE;

    private bool selected = false;

    public AudioClip selectedSound;

    private PlayNovelSceneController sceneController;
    private InitialTalkSceneController talkSceneController;

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
        displayAfterSelectionA = options[0].show;

        if (options.Count == 1)
        {
            optionB.gameObject.SetActive(false);
            optionC.gameObject.SetActive(false);
            optionD.gameObject.SetActive(false);
            optionE.gameObject.SetActive(false);
            return;
        }
        optionB.SetMessage(options[1].text);
        idB = options[1].onChoice;
        stringB = options[1].text;
        displayAfterSelectionB = options[1].show;

        if (options.Count == 2)
        {
            optionC.gameObject.SetActive(false);
            optionD.gameObject.SetActive(false);
            optionE.gameObject.SetActive(false);
            return;
        }
        optionC.SetMessage(options[2].text);
        idC = options[2].onChoice;
        stringC = options[2].text;
        displayAfterSelectionC = options[2].show;

        if (options.Count == 3)
        {
            optionD.gameObject.SetActive(false);
            optionE.gameObject.SetActive(false);
            return;
        }
        optionD.SetMessage(options[3].text);
        idD = options[3].onChoice;
        stringD = options[3].text;
        displayAfterSelectionD = options[3].show;

        if (options.Count == 4)
        {
            optionE.gameObject.SetActive(false);
            return;
        }
        optionE.SetMessage(options[4].text);
        idE = options[4].onChoice;
        stringE = options[4].text;
        displayAfterSelectionE = options[4].show;
        return;
    }

    public void Initialize(InitialTalkSceneController sceneController, List<VisualNovelEvent> options)
    {
        this.talkSceneController = sceneController;
        if (options.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }
        optionA.SetMessage(options[0].text);
        idA = options[0].onChoice;
        stringA = options[0].text;
        displayAfterSelectionA = options[0].show;

        if (options.Count == 1)
        {
            optionB.gameObject.SetActive(false);
            optionC.gameObject.SetActive(false);
            optionD.gameObject.SetActive(false);
            optionE.gameObject.SetActive(false);
            return;
        }
        optionB.SetMessage(options[1].text);
        idB = options[1].onChoice;
        stringB = options[1].text;
        displayAfterSelectionB = options[1].show;

        if (options.Count == 2)
        {
            optionC.gameObject.SetActive(false);
            optionD.gameObject.SetActive(false);
            optionE.gameObject.SetActive(false);
            return;
        }
        optionC.SetMessage(options[2].text);
        idC = options[2].onChoice;
        stringC = options[2].text;
        displayAfterSelectionC = options[2].show;

        if (options.Count == 3)
        {
            optionD.gameObject.SetActive(false);
            optionE.gameObject.SetActive(false);
            return;
        }
        optionD.SetMessage(options[3].text);
        idD = options[3].onChoice;
        stringD = options[3].text;
        displayAfterSelectionD = options[3].show;

        if (options.Count == 4)
        {
            optionE.gameObject.SetActive(false);
            return;
        }
        optionE.SetMessage(options[4].text);
        idE = options[4].onChoice;
        stringE = options[4].text;
        displayAfterSelectionE = options[4].show;
        return;
    }

    public void OnOptionA()
    {
        AnalyticsServiceHandler.Instance().SendPlayerChoice(0);
        StartCoroutine(AfterSelection("Selected A", stringA, idA, displayAfterSelectionA));
    }

    public void OnOptionB()
    {
        AnalyticsServiceHandler.Instance().SendPlayerChoice(1);
        StartCoroutine(AfterSelection("Selected B", stringB, idB, displayAfterSelectionB));
    }

    public void OnOptionC()
    {
        AnalyticsServiceHandler.Instance().SendPlayerChoice(2);
        StartCoroutine(AfterSelection("Selected C", stringC, idC, displayAfterSelectionC));
    }

    public void OnOptionD()
    {
        AnalyticsServiceHandler.Instance().SendPlayerChoice(3);
        StartCoroutine(AfterSelection("Selected D", stringD, idD, displayAfterSelectionD));
    }

    public void OnOptionE()
    {
        AnalyticsServiceHandler.Instance().SendPlayerChoice(4);
        StartCoroutine(AfterSelection("Selected E", stringE, idE, displayAfterSelectionE));
    }

    public IEnumerator AfterSelection(string parameterName, string answer, long nextEventID, bool displayAfterSelection)
    {
        if (selected) { yield break; }
        selected = true;

        GetComponent<Animator>().SetBool(parameterName, true);
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = selectedSound;
        audio.Play();
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        if(sceneController != null)
        {
            sceneController.confirmArea.gameObject.SetActive(true);
            sceneController.ShowAnswer(answer, displayAfterSelection);
            sceneController.SetWaitingForConfirmation(true);
            sceneController.SetNextEvent(nextEventID);

            if (!displayAfterSelection)
            {
                sceneController.OnConfirm();
            }
        } else if(talkSceneController != null) 
        {
            talkSceneController.confirmArea.gameObject.SetActive(true);
            talkSceneController.ShowAnswer(answer, displayAfterSelection);
            talkSceneController.SetWaitingForConfirmation(true);
            talkSceneController.SetNextEvent(nextEventID);

            if (!displayAfterSelection)
            {
                talkSceneController.OnConfirm();
            }
        }
        
    }
}
