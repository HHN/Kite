using System;
using System.Collections.Generic;
using _00_Kite2.Common;
using _00_Kite2.Common.Managers;
using _00_Kite2.Common.SceneManagement;
using _00_Kite2.Server_Communication;
using _00_Kite2.Server_Communication.Server_Calls;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.UserFeedback
{
    public class AiReviewExplorerSceneController : SceneController, IOnSuccessHandler
    {
        [SerializeField] private List<AiReviewGuiElement> reviews;
        [SerializeField] private TMP_InputField searchInputField;
        [SerializeField] private GameObject aiReviewGuiPrefab;
        [SerializeField] private GameObject getAiReviewServerCallPrefab;
        [SerializeField] private GameObject noReviewsFoundHint;
        [SerializeField] private GameObject searchBar;
        [SerializeField] private GameObject reviewsContainer;
        [SerializeField] private Button searchButton;
        [SerializeField] private Sprite searchIcon;
        [SerializeField] private Sprite xIcon;

        // Start is called before the first frame update
        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.AI_REVIEW_EXPLORER_SCENE);

            reviews = new List<AiReviewGuiElement>();
            searchButton.onClick.AddListener(OnStopSearchButton);

            GetAiReviewsServerCall call = Instantiate(getAiReviewServerCallPrefab)
                .GetComponent<GetAiReviewsServerCall>();
            call.sceneController = this;
            call.OnSuccessHandler = this;
            call.SendRequest();
        }

        public void OnSuccess(Response response)
        {
            if (response?.GetAiReviews() == null || response?.GetAiReviews().Count == 0)
            {
                searchBar.SetActive(false);
                noReviewsFoundHint.SetActive(true);
                return;
            }

            searchBar.SetActive(true);
            noReviewsFoundHint.SetActive(false);

            foreach (AiReview review in response.GetAiReviews())
            {
                AiReviewGuiElement aiReviewGuiElement =
                    Instantiate(aiReviewGuiPrefab, reviewsContainer.transform)
                        .GetComponent<AiReviewGuiElement>();

                aiReviewGuiElement.InitializeReview(review);
                reviews.Add(aiReviewGuiElement);
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

            foreach (AiReviewGuiElement guiElement in reviews)
            {
                guiElement.gameObject.SetActive(true);
            }
        }

        private void Search(string value)
        {
            searchButton.image.sprite = xIcon;

            foreach (AiReviewGuiElement guiElement in reviews)
            {
                guiElement.gameObject.SetActive(guiElement.GetNovelName()
                    .Contains(value, StringComparison.OrdinalIgnoreCase));
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