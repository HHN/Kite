using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

public class FeelingPanelController : MonoBehaviour
{
    public TextMeshProUGUI question;
    public TextMeshProUGUI feedback;
    public Button nervousButton;
    public Button fearfullButton;
    public Button encouragedButton;
    public Button annoyedButton;
    public Button skipButton;
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
        skipButton.onClick.AddListener(delegate { OnSkipButton(); });
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
            nervousButton.interactable = true;
            AnalyticsServiceHandler.Instance().AddFeelingToList("Nervous");
        }

        if (string.IsNullOrEmpty(fearfullFeedback))
        {
            fearfullButton.gameObject.SetActive(false);
        }
        else
        {
            fearfullButton.gameObject.SetActive(true);
            fearfullButton.interactable = true;
            AnalyticsServiceHandler.Instance().AddFeelingToList("Fearfull");
        }

        if (string.IsNullOrEmpty(encouragedFeedback))
        {
            encouragedButton.gameObject.SetActive(false);
        }
        else
        {
            encouragedButton.gameObject.SetActive(true);
            encouragedButton.interactable = true;
            AnalyticsServiceHandler.Instance().AddFeelingToList("Encouraged");
        }

        if (string.IsNullOrEmpty(annoyedFeedback))
        {
            annoyedButton.gameObject.SetActive(false);
        }
        else
        {
            annoyedButton.gameObject.SetActive(true);
            annoyedButton.interactable = true;
            AnalyticsServiceHandler.Instance().AddFeelingToList("Annoyed");
        }

        feedback.gameObject.SetActive(false);
    }

    public void CleanUp()
    {
        nervousFeedback = "";
        fearfullFeedback = "";
        encouragedFeedback = "";
        annoyedFeedback = "";
        SetNervousButtonText("NERVÖS");
        SetFearfullButtonText("ÄNGSTLICH");
        SetEncouragedButtonText("ERMUTIGT");
        SetAnnoyedButtonText("VERÄRGERT");
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
            return;
        }
        alreadyChosen = true;

        feedback.text = nervousFeedback;

        annoyedButton.gameObject.SetActive(false);
        fearfullButton.gameObject.SetActive(false);
        encouragedButton.gameObject.SetActive(false);
        feedback.gameObject.SetActive(true);

        nervousButton.interactable = false;
        this.skipButton.GetComponentInChildren<TextMeshProUGUI>().text = "WEITER";
        AnalyticsServiceHandler.Instance().SendPlayerFeeling(0);
    }

    public void OnFearfullButton()
    {
        if (alreadyChosen)
        {
            return;
        }
        alreadyChosen = true;

        feedback.text = fearfullFeedback;

        annoyedButton.gameObject.SetActive(false);
        nervousButton.gameObject.SetActive(false);
        encouragedButton.gameObject.SetActive(false);
        feedback.gameObject.SetActive(true);

        fearfullButton.interactable = false;
        this.skipButton.GetComponentInChildren<TextMeshProUGUI>().text = "WEITER";
        AnalyticsServiceHandler.Instance().SendPlayerFeeling(1);
    }

    public void OnEncouragedButton()
    {
        if (alreadyChosen)
        {
            return;
        }
        alreadyChosen = true;

        feedback.text = encouragedFeedback;

        annoyedButton.gameObject.SetActive(false);
        fearfullButton.gameObject.SetActive(false);
        nervousButton.gameObject.SetActive(false);
        feedback.gameObject.SetActive(true);

        encouragedButton.interactable = false;
        this.skipButton.GetComponentInChildren<TextMeshProUGUI>().text = "WEITER";
        AnalyticsServiceHandler.Instance().SendPlayerFeeling(2);
    }

    public void OnAnnoyedButton()
    {
        if (alreadyChosen)
        {
            return;
        }
        alreadyChosen = true;

        feedback.text = annoyedFeedback;

        nervousButton.gameObject.SetActive(false);
        fearfullButton.gameObject.SetActive(false);
        encouragedButton.gameObject.SetActive(false);
        feedback.gameObject.SetActive(true);

        annoyedButton.interactable = false;
        this.skipButton.GetComponentInChildren<TextMeshProUGUI>().text = "WEITER";
        AnalyticsServiceHandler.Instance().SendPlayerFeeling(3);
    }

    public void OnSkipButton()
    {
        this.gameObject.SetActive(false);
        this.controller.SetWaitingForConfirmation(true);
        this.controller.OnConfirm();
        AnalyticsServiceHandler.Instance().SendPlayerFeeling(4);
    }
}
