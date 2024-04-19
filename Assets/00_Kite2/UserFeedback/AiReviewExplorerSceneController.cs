using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using UnityEngine.UI;

public class AiReviewExplorerSceneController : SceneController, OnSuccessHandler
{
    [SerializeField] private List<AiReviewGuiElement> reviews;
    [SerializeField] private TMP_InputField searchInputField;
    [SerializeField] private GameObject aiReviewGuiPrefab;
    [SerializeField] private GameObject GetAiReviewServerCallPrefab;
    [SerializeField] private GameObject NoReviewsFoundHint;
    [SerializeField] private GameObject SearchBar;
    [SerializeField] private GameObject reviewsContainer;
    [SerializeField] private Button searchButton;
    [SerializeField] private Sprite searchIcon;
    [SerializeField] private Sprite xIcon;

    // Start is called before the first frame update
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.AI_REVIEW_EXPLORER_SCENE);

        reviews = new List<AiReviewGuiElement>();
        searchButton.onClick.AddListener(delegate { OnStopSearchButton(); });

        GetAiReviewsServerCall call = Instantiate(GetAiReviewServerCallPrefab).GetComponent<GetAiReviewsServerCall>();
        call.sceneController = this;
        call.onSuccessHandler = this;
        call.SendRequest();
    }

    public void OnSuccess(Response response)
    {
        if (response?.GetAiReviews() == null || response?.GetAiReviews().Count == 0)
        {
            SearchBar.SetActive(false);
            NoReviewsFoundHint.SetActive(true);
            return;
        }
        SearchBar.SetActive(true);
        NoReviewsFoundHint.SetActive(false);

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

    public void StopSearch()
    {
        searchButton.image.sprite = searchIcon;
        searchInputField.text = string.Empty;

        foreach (AiReviewGuiElement guiElement in reviews)
        {
            guiElement.gameObject.SetActive(true);
        }
    }

    public void Search(string value)
    {
        searchButton.image.sprite = xIcon;

        foreach (AiReviewGuiElement guiElement in reviews)
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
