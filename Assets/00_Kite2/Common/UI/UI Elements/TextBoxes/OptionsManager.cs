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
        StartCoroutine(AfterSelection("Selected A", stringA, idA, displayAfterSelectionA, 0));
    }

    public void OnOptionB()
    {
        AnalyticsServiceHandler.Instance().SetChoiceId(1);
        StartCoroutine(AfterSelection("Selected B", stringB, idB, displayAfterSelectionB, 1));
    }

    public void OnOptionC()
    {
        AnalyticsServiceHandler.Instance().SetChoiceId(2);
        StartCoroutine(AfterSelection("Selected C", stringC, idC, displayAfterSelectionC, 2));
    }

    public void OnOptionD()
    {
        AnalyticsServiceHandler.Instance().SetChoiceId(3);
        StartCoroutine(AfterSelection("Selected D", stringD, idD, displayAfterSelectionD, 3));
    }

    public void OnOptionE()
    {
        AnalyticsServiceHandler.Instance().SetChoiceId(4);
        StartCoroutine(AfterSelection("Selected E", stringE, idE, displayAfterSelectionE, 4));
    }

    public IEnumerator AfterSelection(string parameterName, string answer, string nextEventID, bool displayAfterSelection, int index)
    {
        // Disable animations after the selection
        AnimationFlagSingleton.Instance().SetFlag(false);

        // If already selected, exit the coroutine
        if (selected) { yield break; }
        selected = true; // Mark as selected to prevent repeated selections

        // Add the current path (selection) to the visual novel path history
        sceneController.AddPathToNovel(index);

        // Trigger the animation associated with the parameter
        GetComponent<Animator>().SetBool(parameterName, true);

        // Play the selection audio feedback
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = selectedSound;
        audio.Play();

        // Wait for the audio and animation to complete
        yield return new WaitForSeconds(1f);

        // Hide the current object after the selection
        gameObject.SetActive(false);

        // If the scene controller is set, proceed with further actions
        if (sceneController != null)
        {
            // Show confirmation areas
            sceneController.confirmArea.gameObject.SetActive(true);
            sceneController.confirmArea2.gameObject.SetActive(true);

            // Display the selected answer and handle post-selection behavior
            sceneController.ShowAnswer(answer, displayAfterSelection);
            sceneController.SetWaitingForConfirmation(true); // Indicate that a confirmation is expected
            sceneController.SetNextEvent(nextEventID); // Set the next event ID for the scene

            // Automatically confirm if post-selection display is not required
            if (!displayAfterSelection)
            {
                sceneController.OnConfirm(); // Trigger confirmation if no further display is needed
            }
        }
    }

}
