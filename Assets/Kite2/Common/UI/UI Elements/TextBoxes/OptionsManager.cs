using System.Collections;
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
        StartCoroutine(AfterSelection("Selected A", stringA, idA));
    }

    public void OnOptionB()
    {
        StartCoroutine(AfterSelection("Selected B", stringB, idB));
    }

    public void OnOptionC()
    {
        StartCoroutine(AfterSelection("Selected C", stringC, idC));
    }

    public void OnOptionD()
    {
        StartCoroutine(AfterSelection("Selected D", stringD, idD));
    }

    public IEnumerator AfterSelection(string parameterName, string answer, long nextEventID)
    {
        if (selected) { yield break; }
        selected = true;

        GetComponent<Animator>().SetBool(parameterName, true);
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = selectedSound;
        audio.Play();
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        sceneController.ShowAnswer(answer);
        sceneController.isWaitingForConfirmation = true;
        sceneController.SetNextEvent(nextEventID);
    }
}
