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

    // Neue Liste, die alle Optionen enthält
    private List<ChatMessageBox> allOptions;

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
    private ConversationContentGuiController _conversationContentGuiController;

    public void Initialize(PlayNovelSceneController sceneController, List<VisualNovelEvent> options)
    {
        this.sceneController = sceneController;
        _conversationContentGuiController = FindAnyObjectByType<ConversationContentGuiController>();

        // Initialisiere die Liste aller Optionen
        allOptions = new List<ChatMessageBox> { optionA, optionB, optionC, optionD, optionE };

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
        GameManager.Instance.calledFromReload = false;
        
        // Disable animations after the selection
        var animationFlagSingleton = AnimationFlagSingleton.Instance();
        if (animationFlagSingleton != null)
        {
            animationFlagSingleton.SetFlag(false);
        }
        else
        {
            Debug.LogWarning("AnimationFlagSingleton.Instance() returned null.");
        }

        // If already selected, exit the coroutine
        if (selected) { yield break; }
        selected = true; // Mark as selected to prevent repeated selections

        // Add the current path (selection) to the visual novel path history
        if (sceneController != null)
        {
            for (int i = 0; i < sceneController.NovelToPlay.novelEvents.Count; i++)
            {
                if (sceneController.NovelToPlay.novelEvents[i].text == answer)
                {
                    _conversationContentGuiController.VisualNovelEvents.Add(sceneController.NovelToPlay.novelEvents[i]);
                }
            }
            sceneController.AddPathToNovel(index);
        }
        else
        {
            Debug.LogWarning("sceneController is null. Cannot add path to novel.");
        }

        // Trigger the animation associated with the parameter
        Animator animator = GetComponent<Animator>();
        if (animator != null && !string.IsNullOrEmpty(parameterName))
        {
            animator.SetBool(parameterName, true);
        }
        else
        {
            if (animator == null)
            {
                Debug.LogWarning("Animator component not found on GameObject.");
            }
            if (string.IsNullOrEmpty(parameterName))
            {
                Debug.LogWarning("parameterName is null or empty.");
            }
        }

        // Play the selection audio feedback
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            if (selectedSound != null)
            {
                audio.clip = selectedSound;
                audio.Play();
            }
            else
            {
                Debug.LogWarning("selectedSound is null. Cannot play audio clip.");
            }
        }
        else
        {
            Debug.LogWarning("AudioSource component not found on GameObject.");
        }

        // Wait for the audio and animation to complete
        yield return new WaitForSeconds(0.5f);

        // Hide the current object after the selection
        gameObject.SetActive(false);

        // If the scene controller is set, proceed with further actions
        if (sceneController != null)
        {
            // Show confirmation areas
            if (sceneController.confirmArea != null && sceneController.confirmArea.gameObject != null)
            {
                sceneController.confirmArea.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("sceneController.confirmArea or its GameObject is null.");
            }

            if (sceneController.confirmArea2 != null && sceneController.confirmArea2.gameObject != null)
            {
                sceneController.confirmArea2.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("sceneController.confirmArea2 or its GameObject is null.");
            }

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
        else
        {
            Debug.LogWarning("sceneController is null.");
        }
    }
}
