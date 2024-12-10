using System;
using System.Collections.Generic;
using _00_Kite2.Common.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NovelReviewExplorerSceneController : SceneController, OnSuccessHandler
{
    [SerializeField] private GameObject novelReviewGuiPrefab;
    [SerializeField] private GameObject GetNovelReviewServerCallPrefab;
    [SerializeField] private GameObject NoReviewsFoundHint;
    [SerializeField] private GameObject SearchBar;
    [SerializeField] private GameObject reviewsContainer;
    [SerializeField] private Button searchButton;
    [SerializeField] private TMP_InputField searchInputField;
    [SerializeField] private Sprite searchIcon;
    [SerializeField] private Sprite xIcon;
    [SerializeField] private List<NovelReviewGuiElement> reviews;

    // Start is called before the first frame update
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.NOVEL_REVIEW_EXPLORER_SCENE);

        reviews = new List<NovelReviewGuiElement>();
        searchButton.onClick.AddListener(delegate { OnStopSearchButton(); });

        GetNovelReviewsServerCall call = Instantiate(GetNovelReviewServerCallPrefab).GetComponent<GetNovelReviewsServerCall>();
        call.sceneController = this;
        call.OnSuccessHandler = this;
        call.SendRequest();
    }

    public void OnSuccess(Response response)
    {
        if (response?.GetNovelReviews() == null || response?.GetNovelReviews().Count == 0)
        {
            SearchBar.SetActive(false);
            NoReviewsFoundHint.SetActive(true);
            return;
        }
        SearchBar.SetActive(true);
        NoReviewsFoundHint.SetActive(false);

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

    public void StopSearch()
    {
        searchButton.image.sprite = searchIcon;
        searchInputField.text = string.Empty;

        foreach (NovelReviewGuiElement guiElement in reviews)
        {
            guiElement.gameObject.SetActive(true);
        }
    }

    public void Search(string value)
    {
        searchButton.image.sprite = xIcon;

        foreach (NovelReviewGuiElement guiElement in reviews)
        {
            guiElement.gameObject.SetActive(guiElement.GetNovelName().Contains(value, StringComparison.OrdinalIgnoreCase));
            guiElement.GetComponentInChildren<DropDownMenu>().SetMenuOpen(false);
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
