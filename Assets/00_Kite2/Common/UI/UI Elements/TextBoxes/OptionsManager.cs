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

    private string idA;
    private string idB;
    private string idC;
    private string idD;
    private string idE;

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

    public void Initialize(PlayNovelSceneController sceneController, List<VisualNovelEvent> options)
    {
        this.sceneController = sceneController;
        if (options.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }
        optionA.SetMessage(options[0].text);
        AnalyticsServiceHandler.Instance().AddChoiceToList(options[0].text);
        idA = options[0].onChoice;
        stringA = options[0].text;
        displayAfterSelectionA = options[0].show;

        if (options.Count == 1)
        {
            optionB.gameObject.SetActive(false);
            optionC.gameObject.SetActive(false);
            optionD.gameObject.SetActive(false);
            optionE.gameObject.SetActive(false);
            AnalyticsServiceHandler.Instance().AddedLastChoice();
            return;
        }
        optionB.SetMessage(options[1].text);
        AnalyticsServiceHandler.Instance().AddChoiceToList(options[1].text);
        idB = options[1].onChoice;
        stringB = options[1].text;
        displayAfterSelectionB = options[1].show;

        if (options.Count == 2)
        {
            optionC.gameObject.SetActive(false);
            optionD.gameObject.SetActive(false);
            optionE.gameObject.SetActive(false);
            AnalyticsServiceHandler.Instance().AddedLastChoice();
            return;
        }
        optionC.SetMessage(options[2].text);
        AnalyticsServiceHandler.Instance().AddChoiceToList(options[2].text);
        idC = options[2].onChoice;
        stringC = options[2].text;
        displayAfterSelectionC = options[2].show;

        if (options.Count == 3)
        {
            optionD.gameObject.SetActive(false);
            optionE.gameObject.SetActive(false);
            AnalyticsServiceHandler.Instance().AddedLastChoice();
            return;
        }
        optionD.SetMessage(options[3].text);
        AnalyticsServiceHandler.Instance().AddChoiceToList(options[3].text);
        idD = options[3].onChoice;
        stringD = options[3].text;
        displayAfterSelectionD = options[3].show;

        if (options.Count == 4)
        {
            optionE.gameObject.SetActive(false);
            AnalyticsServiceHandler.Instance().AddedLastChoice();
            return;
        }
        optionE.SetMessage(options[4].text);
        AnalyticsServiceHandler.Instance().AddChoiceToList(options[4].text);
        idE = options[4].onChoice;
        stringE = options[4].text;
        displayAfterSelectionE = options[4].show;
        AnalyticsServiceHandler.Instance().AddedLastChoice();
        return;
    }

    public void OnOptionA()
    {
        AnalyticsServiceHandler.Instance().SetChoiceId(0);
        Debug.Log(stringA);
        TextToSpeechService.Instance().TextToSpeech(idA, true);
        StartCoroutine(AfterSelection("Selected A", stringA, idA, displayAfterSelectionA));
    }

    public void OnOptionB()
    {
        AnalyticsServiceHandler.Instance().SetChoiceId(1);
        Debug.Log(stringB);
        TextToSpeechService.Instance().TextToSpeech(idB, true);
        StartCoroutine(AfterSelection("Selected B", stringB, idB, displayAfterSelectionB));
    }

    public void OnOptionC()
    {
        AnalyticsServiceHandler.Instance().SetChoiceId(2);
        Debug.Log(stringC);
        TextToSpeechService.Instance().TextToSpeech(idC, true);
        StartCoroutine(AfterSelection("Selected C", stringC, idC, displayAfterSelectionC));
    }

    public void OnOptionD()
    {
        AnalyticsServiceHandler.Instance().SetChoiceId(3);
        Debug.Log(stringD);
        TextToSpeechService.Instance().TextToSpeech(idD, true);
        StartCoroutine(AfterSelection("Selected D", stringD, idD, displayAfterSelectionD));
    }

    public void OnOptionE()
    {
        AnalyticsServiceHandler.Instance().SetChoiceId(4);
        Debug.Log(stringE);
        TextToSpeechService.Instance().TextToSpeech(idE, true);
        StartCoroutine(AfterSelection("Selected E", stringE, idE, displayAfterSelectionE));
    }

    public IEnumerator AfterSelection(string parameterName, string answer, string nextEventID, bool displayAfterSelection)
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
            sceneController.confirmArea2.gameObject.SetActive(true);
            sceneController.ShowAnswer(answer, displayAfterSelection);
            sceneController.SetWaitingForConfirmation(true);
            sceneController.SetNextEvent(nextEventID);

            if (!displayAfterSelection)
            {
                sceneController.OnConfirm();
            }
        } 
    }
}
