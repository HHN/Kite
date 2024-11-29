using System.Collections.Generic;
using System;
using _00_Kite2.Common.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpertFeedbackSceneController : SceneController, OnSuccessHandler
{
    [SerializeField] private List<ExpertFeedbackGuiElementController> questions;
    [SerializeField] private TMP_InputField searchInputField;
    [SerializeField] private GameObject expertFeedbackGuiPrefab;
    [SerializeField] private GameObject getExpertFeedbackQuestionsServerCallPrefab;
    [SerializeField] private GameObject noQuestionsFoundHint;
    [SerializeField] private GameObject SearchBar;
    [SerializeField] private GameObject questionsContainer;
    [SerializeField] private Button searchButton;
    [SerializeField] private Sprite searchIcon;
    [SerializeField] private Sprite xIcon;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.EXPERT_FEEDBACK_SCENE);

        questions = new List<ExpertFeedbackGuiElementController>();
        searchButton.onClick.AddListener(delegate { OnStopSearchButton(); });

        FindExpertFeedbackQuestionServerCall call = Instantiate(getExpertFeedbackQuestionsServerCallPrefab).GetComponent<FindExpertFeedbackQuestionServerCall>();
        call.sceneController = this;
        call.onSuccessHandler = this;
        call.userUuid = ExpertFeedbackQuestionManager.Instance.GetUUID();
        call.SendRequest();
    }

    public void OnSuccess(Response response)
    {
        if (response?.GetExpertFeedbackQuestions() == null || response?.GetExpertFeedbackQuestions().Count == 0)
        {
            SearchBar.SetActive(false);
            noQuestionsFoundHint.SetActive(true);
            return;
        }
        SearchBar.SetActive(true);
        noQuestionsFoundHint.SetActive(false);

        foreach (ExpertFeedbackQuestion question in response.GetExpertFeedbackQuestions())
        {
            ExpertFeedbackGuiElementController expertFeedbackGuiElementController =
                Instantiate(expertFeedbackGuiPrefab, questionsContainer.transform)
                .GetComponent<ExpertFeedbackGuiElementController>();

            expertFeedbackGuiElementController.InitializeReview(question);
            questions.Add(expertFeedbackGuiElementController);
        }
    }

    public void OnSearchValueChanged()
    {
        if (string.IsNullOrEmpty(searchInputField.text))
        {
            StopSearch();
            return;
        }
        Search(searchInputField.text);
    }

    public void StopSearch()
    {
        searchButton.image.sprite = searchIcon;
        searchInputField.text = string.Empty;

        foreach (ExpertFeedbackGuiElementController guiElement in questions)
        {
            guiElement.gameObject.SetActive(true);
        }
    }

    public void Search(string value)
    {
        searchButton.image.sprite = xIcon;

        foreach (ExpertFeedbackGuiElementController guiElement in questions)
        {
            guiElement.gameObject.SetActive(guiElement.GetNovelName().Contains(value, StringComparison.OrdinalIgnoreCase));
            guiElement.CloseAll();
        }
    }

    public void OnStopSearchButton()
    {
        if (string.IsNullOrEmpty(searchInputField.text))
        {
            return;
        }
        StopSearch();
    }
}
