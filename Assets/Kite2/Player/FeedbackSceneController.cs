using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeedbackSceneController : SceneController, OnSuccessHandler
{
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI novelTitle;
    public GameObject gptServercallPrefab;
    public FavoriteButton favoriteButton;
    private VisualNovel novelToPlay;

    private void Start()
    {
        novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();

        if (novelToPlay == null)
        {
            return;
        }
        novelTitle.SetText(novelToPlay.title);
        favoriteButton.novel = novelToPlay;
        favoriteButton.Init();
        if (string.IsNullOrEmpty(novelToPlay.feedback))
        {
            feedbackText.SetText("Bitte warten, Feedback wird geladen...");
            GetCompletionServerCall call = Instantiate(gptServercallPrefab).GetComponent<GetCompletionServerCall>();
            call.sceneController = this;
            call.onSuccessHandler = this;
            call.prompt = PromptManager.Instance().GetPrompt();
            call.SendRequest();
            return;
        }
        feedbackText.SetText(novelToPlay.feedback);
    }

    public void OnMainMenuButton()
    {
        SceneLoader.LoadMainMenuScene();
    }

    public void OnSuccess(Response response)
    {
        feedbackText.SetText(response.completion);
    }
}
