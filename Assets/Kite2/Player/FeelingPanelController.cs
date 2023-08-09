using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FeelingPanelController : MonoBehaviour
{
    public TextMeshProUGUI question;
    public TextMeshProUGUI feedback;
    public Button nervousButton;
    public Button fearfullButton;
    public Button encouragedButton;
    public Button annoyedButton;
    public PlayNovelSceneController controller;

    private string nervousFeedback;
    private string fearfullFeedback;
    private string encouragedFeedback;
    private string annoyedFeedback;

    private bool alreadyChosen = false;

    void Start()
    {
        nervousButton.onClick.AddListener(delegate { OnNervousButton(); });
        fearfullButton.onClick.AddListener(delegate { OnFearfullButton(); });
        encouragedButton.onClick.AddListener(delegate { OnEncouragedButton(); });
        annoyedButton.onClick.AddListener(delegate { OnAnnoyedButton(); });
    }

    public void Initialize()
    {
        if (string.IsNullOrEmpty(nervousFeedback))
        {
            nervousButton.gameObject.SetActive(false);
        }
        else
        {
            nervousButton.gameObject.SetActive(true);
        }

        if (string.IsNullOrEmpty(fearfullFeedback))
        {
            fearfullButton.gameObject.SetActive(false);
        }
        else
        {
            fearfullButton.gameObject.SetActive(true);
        }

        if (string.IsNullOrEmpty(encouragedFeedback))
        {
            encouragedButton.gameObject.SetActive(false);
        }
        else
        {
            encouragedButton.gameObject.SetActive(true);
        }

        if (string.IsNullOrEmpty(annoyedFeedback))
        {
            annoyedButton.gameObject.SetActive(false);
        }
        else
        {
            annoyedButton.gameObject.SetActive(true);
        }

        feedback.gameObject.SetActive(false);
    }

    public void CleanUp()
    {
        nervousFeedback = "";
        fearfullFeedback = "";
        encouragedFeedback = "";
        annoyedFeedback = "";
        SetNervousButtonText("- Nervös");
        SetFearfullButtonText("- Ängstlich");
        SetEncouragedButtonText("- Ermutigt");
        SetAnnoyedButtonText("- Verärgert");
    }

    public void SetQuestion(string question)
    {
        this.question.text = question;
    }

    public void SetNervousButtonText(string text)
    {
        this.nervousButton.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public void SetFearfullButtonText(string text)
    {
        this.fearfullButton.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public void SetEncouragedButtonText(string text)
    {
        this.encouragedButton.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public void SetAnnoyedButtonText(string text)
    {
        this.annoyedButton.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public void SetNervousFeedback(string nervousFeedback)
    {
        this.nervousFeedback = nervousFeedback;
    }

    public void SetFearfullFeedback(string fearfullFeedback)
    {
        this.fearfullFeedback = fearfullFeedback;
    }

    public void SetEncouragedFeedback(string encouragedFeedback)
    {
        this.encouragedFeedback = encouragedFeedback;
    }

    public void SetAnnoyedFeedback(string annoyedFeedback)
    {
        this.annoyedFeedback = annoyedFeedback;
    }

    public void OnNervousButton()
    {
        if (alreadyChosen)
        {
            OnAlreadyChosen();
            return;
        }
        alreadyChosen = true;

        feedback.text = nervousFeedback;

        annoyedButton.gameObject.SetActive(false);
        fearfullButton.gameObject.SetActive(false);
        encouragedButton.gameObject.SetActive(false);
        feedback.gameObject.SetActive(true);
    }

    public void OnFearfullButton()
    {
        if (alreadyChosen)
        {
            OnAlreadyChosen();
            return;
        }
        alreadyChosen = true;

        feedback.text = fearfullFeedback;

        annoyedButton.gameObject.SetActive(false);
        nervousButton.gameObject.SetActive(false);
        encouragedButton.gameObject.SetActive(false);
        feedback.gameObject.SetActive(true);
    }

    public void OnEncouragedButton()
    {
        if (alreadyChosen)
        {
            OnAlreadyChosen();
            return;
        }
        alreadyChosen = true;

        feedback.text = encouragedFeedback;

        annoyedButton.gameObject.SetActive(false);
        fearfullButton.gameObject.SetActive(false);
        nervousButton.gameObject.SetActive(false);
        feedback.gameObject.SetActive(true);
    }

    public void OnAnnoyedButton()
    {
        if (alreadyChosen)
        {
            OnAlreadyChosen();
            return;
        }
        alreadyChosen = true;

        feedback.text = annoyedFeedback;

        nervousButton.gameObject.SetActive(false);
        fearfullButton.gameObject.SetActive(false);
        encouragedButton.gameObject.SetActive(false);
        feedback.gameObject.SetActive(true);
    }

    public void OnAlreadyChosen()
    {
        this.gameObject.SetActive(false);
        this.controller.isWaitingForConfirmation = true;
        this.controller.OnConfirm();
    }
}
