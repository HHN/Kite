using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeedbackSceneController : SceneController
{
    public TextMeshProUGUI feedbackText;

    private void Start()
    {
        VisualNovel novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();

        if (novelToPlay == null)
        {
            return;
        }
        if (string.IsNullOrEmpty(novelToPlay.feedback))
        {
            feedbackText.SetText("Leider ist kein Feedback für diese Novel verfügbar.");
            return;
        }
        feedbackText.SetText(novelToPlay.feedback);
    }

    public void OnMainMenuButton()
    {
        SceneLoader.LoadMainMenuScene();
    }
}
