using System;
using System.Collections.Generic;
using _00_Kite2.Common;
using _00_Kite2.Common.Managers;
using _00_Kite2.Common.SceneManagement;
using _00_Kite2.Server_Communication;
using _00_Kite2.Server_Communication.Server_Calls;
using _00_Kite2.UserFeedback;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.ExpertFeedback
{
    public class ExpertFeedbackSceneController : SceneController, IOnSuccessHandler
    {
        [SerializeField] private List<ExpertFeedbackGuiElementController> questions;
        [SerializeField] private TMP_InputField searchInputField;
        [SerializeField] private GameObject expertFeedbackGuiPrefab;
        [SerializeField] private GameObject getExpertFeedbackQuestionsServerCallPrefab;
        [SerializeField] private GameObject noQuestionsFoundHint;
        [SerializeField] private GameObject searchBar;
        [SerializeField] private GameObject questionsContainer;
        [SerializeField] private Button searchButton;
        [SerializeField] private Sprite searchIcon;
        [SerializeField] private Sprite xIcon;

        void Start()
        {
            BackStackManager.Instance().Push(SceneNames.EXPERT_FEEDBACK_SCENE);

            questions = new List<ExpertFeedbackGuiElementController>();
            searchButton.onClick.AddListener(OnStopSearchButton);

            FindExpertFeedbackQuestionServerCall call = Instantiate(getExpertFeedbackQuestionsServerCallPrefab).GetComponent<FindExpertFeedbackQuestionServerCall>();
            call.sceneController = this;
            call.OnSuccessHandler = this;
            call.userUuid = ExpertFeedbackQuestionManager.Instance.GetUUID();
            call.SendRequest();
        }

        public void OnSuccess(Response response)
        {
            if (response?.GetExpertFeedbackQuestions() == null || response?.GetExpertFeedbackQuestions().Count == 0)
            {
                searchBar.SetActive(false);
                noQuestionsFoundHint.SetActive(true);
                return;
            }
            searchBar.SetActive(true);
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

        private void StopSearch()
        {
            searchButton.image.sprite = searchIcon;
            searchInputField.text = string.Empty;

            foreach (ExpertFeedbackGuiElementController guiElement in questions)
            {
                guiElement.gameObject.SetActive(true);
            }
        }

        private void Search(string value)
        {
            searchButton.image.sprite = xIcon;

            foreach (ExpertFeedbackGuiElementController guiElement in questions)
            {
                guiElement.gameObject.SetActive(guiElement.GetNovelName().Contains(value, StringComparison.OrdinalIgnoreCase));
                guiElement.CloseAll();
            }
        }

        private void OnStopSearchButton()
        {
            if (string.IsNullOrEmpty(searchInputField.text))
            {
                return;
            }
            StopSearch();
        }
    }
}
