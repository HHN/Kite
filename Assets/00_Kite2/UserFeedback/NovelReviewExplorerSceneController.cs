using System;
using System.Collections.Generic;
using _00_Kite2.Common;
using _00_Kite2.Common.Managers;
using _00_Kite2.Common.SceneManagement;
using _00_Kite2.Common.UI.UI_Elements.DropDown;
using _00_Kite2.Server_Communication;
using _00_Kite2.Server_Communication.Server_Calls;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.UserFeedback
{
    public class NovelReviewExplorerSceneController : SceneController, IOnSuccessHandler
    {
        [SerializeField] private GameObject novelReviewGuiPrefab;
        [SerializeField] private GameObject getNovelReviewServerCallPrefab;
        [SerializeField] private GameObject noReviewsFoundHint;
        [SerializeField] private GameObject searchBar;
        [SerializeField] private GameObject reviewsContainer;
        [SerializeField] private Button searchButton;
        [SerializeField] private TMP_InputField searchInputField;
        [SerializeField] private Sprite searchIcon;
        [SerializeField] private Sprite xIcon;
        [SerializeField] private List<NovelReviewGuiElement> reviews;

        // Start is called before the first frame update
        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.NOVEL_REVIEW_EXPLORER_SCENE);

            reviews = new List<NovelReviewGuiElement>();
            searchButton.onClick.AddListener(OnStopSearchButton);

            GetNovelReviewsServerCall call = Instantiate(getNovelReviewServerCallPrefab)
                .GetComponent<GetNovelReviewsServerCall>();
            call.sceneController = this;
            call.OnSuccessHandler = this;
            call.SendRequest();
        }

        public void OnSuccess(Response response)
        {
            if (response?.GetNovelReviews() == null || response?.GetNovelReviews().Count == 0)
            {
                searchBar.SetActive(false);
                noReviewsFoundHint.SetActive(true);
                return;
            }

            searchBar.SetActive(true);
            noReviewsFoundHint.SetActive(false);

            foreach (NovelReview review in response.GetNovelReviews())
            {
                NovelReviewGuiElement novelReviewGuiElement =
                    Instantiate(novelReviewGuiPrefab, reviewsContainer.transform)
                        .GetComponent<NovelReviewGuiElement>();

                novelReviewGuiElement.InitializeReview(review);
                reviews.Add(novelReviewGuiElement);
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

            foreach (NovelReviewGuiElement guiElement in reviews)
            {
                guiElement.gameObject.SetActive(true);
            }
        }

        private void Search(string value)
        {
            searchButton.image.sprite = xIcon;

            foreach (NovelReviewGuiElement guiElement in reviews)
            {
                guiElement.gameObject.SetActive(guiElement.GetNovelName()
                    .Contains(value, StringComparison.OrdinalIgnoreCase));
                guiElement.GetComponentInChildren<DropDownMenu>().SetMenuOpen(false);
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