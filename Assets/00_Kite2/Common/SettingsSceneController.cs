using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSceneController : SceneController
{
    [SerializeField] private RectTransform sublayout01;
    [SerializeField] private RectTransform sublayout02;
    [SerializeField] private RectTransform sublayout03;
    [SerializeField] private RectTransform sublayout04;
    [SerializeField] private RectTransform sublayout05;
    [SerializeField] private RectTransform sublayout06;
    [SerializeField] private RectTransform sublayout07;
    [SerializeField] private RectTransform sublayout08;
    [SerializeField] private RectTransform sublayout09;
    [SerializeField] private RectTransform layout;

    [SerializeField] private Button aboutKiteButton;
    [SerializeField] private Button aboutKiteInfoButton;
    [SerializeField] private Button toggleDataCollectionButton;
    [SerializeField] private Button toggleDataCollectionInfoButton;
    [SerializeField] private Button deleteCollectedDataButton;
    [SerializeField] private Button deleteCollectedDataInfoButton;
    [SerializeField] private Button termsOfUseButton;
    [SerializeField] private Button termsOfUseInfoButton;
    [SerializeField] private Button dataPrivacyButton;
    [SerializeField] private Button dataPrivacyInfoButton;
    [SerializeField] private Button imprintButton;
    [SerializeField] private Button imprintInfoButton;
    [SerializeField] private Button playerPrefsButton;
    [SerializeField] private Button playerPrefsInfoButton;
    [SerializeField] private Button toggleTextToSpeechButton;
    [SerializeField] private Button toggleTextToSpeechInfoButton;
    [SerializeField] private Button applicationModeButton;
    [SerializeField] private Button applicationModeInfoButton;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.SETTINGS_SCENE);

        aboutKiteButton.onClick.AddListener(delegate { OnAboutKiteButton(); });
        aboutKiteInfoButton.onClick.AddListener(delegate { OnAboutKiteInfoButton(); });
        toggleDataCollectionButton.onClick.AddListener(delegate { OnToggleDataCollectionButton(); });
        toggleDataCollectionInfoButton.onClick.AddListener(delegate { OnToggleDataCollectionInfoButton(); });
        deleteCollectedDataButton.onClick.AddListener(delegate { OnDeleteCollectedDataButton(); });
        deleteCollectedDataInfoButton.onClick.AddListener(delegate { OnDeleteCollectedDataInfoButton(); });
        termsOfUseButton.onClick.AddListener(delegate { OnTermsOfUseButton(); });
        termsOfUseInfoButton.onClick.AddListener(delegate { OnTermsOfUseInfoButton(); });
        dataPrivacyButton.onClick.AddListener(delegate { OnDataPrivacyButton(); });
        dataPrivacyInfoButton.onClick.AddListener(delegate { OnDataPrivacyInfoButton(); });
        imprintButton.onClick.AddListener(delegate { OnImprintButton(); });
        imprintInfoButton.onClick.AddListener(delegate { OnImprintInfoButton(); });
        playerPrefsButton.onClick.AddListener(delegate { OnPlayerPrefsButton(); });
        playerPrefsInfoButton.onClick.AddListener(delegate { OnPlayerPrefsInfoButton(); });
        toggleTextToSpeechButton.onClick.AddListener(delegate { OnToggleTextToSpeechButton(); });
        toggleTextToSpeechInfoButton.onClick.AddListener(delegate { OnToggleTextToSpeechInfoButton(); });
        applicationModeButton.onClick.AddListener(delegate { OnApplicationModeButton(); });
        applicationModeInfoButton.onClick.AddListener(delegate { OnApplicationModeInfoButton(); });

        InitializeToggleDataCollectionButton();
        InitializeApplicationModeButton();
        InitializeToggleTextToSpeechButton();
        LayoutRebuilder.ForceRebuildLayoutImmediate(sublayout01);
        LayoutRebuilder.ForceRebuildLayoutImmediate(sublayout02);
        LayoutRebuilder.ForceRebuildLayoutImmediate(sublayout03);
        LayoutRebuilder.ForceRebuildLayoutImmediate(sublayout04);
        LayoutRebuilder.ForceRebuildLayoutImmediate(sublayout05);
        LayoutRebuilder.ForceRebuildLayoutImmediate(sublayout06);
        LayoutRebuilder.ForceRebuildLayoutImmediate(sublayout07);
        LayoutRebuilder.ForceRebuildLayoutImmediate(sublayout08);
        LayoutRebuilder.ForceRebuildLayoutImmediate(sublayout09);
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
    }

    public void OnAboutKiteButton()
    {
        SceneLoader.LoadInfoScene();
    }

    public void OnAboutKiteInfoButton()
    {
        DisplayInfoMessage(InfoMessages.EXPLANATION_ABOUT_US_BUTTON);
    }

    public void OnToggleDataCollectionButton()
    {
        if (PrivacyAndConditionManager.Instance().IsDataCollectionAccepted())
        {
            PrivacyAndConditionManager.Instance().UnacceptDataCollection();
            DisplayInfoMessage(InfoMessages.STOPED_DATA_COLLECTION);
        }
        else
        {
            PrivacyAndConditionManager.Instance().AcceptDataCollection();
            DisplayInfoMessage(InfoMessages.STARTED_DATA_COLLECTION);
        }
        InitializeToggleDataCollectionButton();
    }

    public void OnToggleDataCollectionInfoButton()
    {
        if (PrivacyAndConditionManager.Instance().IsDataCollectionAccepted())
        {
            DisplayInfoMessage(InfoMessages.EXPLANATION_STOP_DATA_BUTTON);
        } 
        else
        {
            DisplayInfoMessage(InfoMessages.EXPLANATION_COLLECT_DATA_BUTTON);
        }
    }

    public void OnDeleteCollectedDataButton()
    {
        AnalyticsServiceHandler.Instance().DeleteCollectedData();
        DisplayInfoMessage(InfoMessages.DELETED_DATA);
    }

    public void OnDeleteCollectedDataInfoButton()
    {
        DisplayInfoMessage(InfoMessages.EXPLANATION_DELETE_DATA_BUTTON);
    }

    public void InitializeToggleDataCollectionButton()
    {
        if (PrivacyAndConditionManager.Instance().IsDataCollectionAccepted())
        {
            toggleDataCollectionButton.GetComponentInChildren<TextMeshProUGUI>().text = "DATEN ERFASSUNG STOPPEN";
        }
        else
        {
            toggleDataCollectionButton.GetComponentInChildren<TextMeshProUGUI>().text = "DATEN ERFASSEN";
        }
    }

    public void InitializeApplicationModeButton()
    {
        if (ApplicationModeManager.Instance().IsOfflineModeActive())
        {
            applicationModeButton.GetComponentInChildren<TextMeshProUGUI>().text = "ONLINE GEHEN";
        }
        else
        {
            applicationModeButton.GetComponentInChildren<TextMeshProUGUI>().text = "OFFLINE GEHEN";
        }
    }

    public void InitializeToggleTextToSpeechButton()
    {
        if (TextToSpeechManager.Instance().IsTextToSpeechActivated())
        {
            toggleTextToSpeechButton.GetComponentInChildren<TextMeshProUGUI>().text = "TEXT VORLESEN";
        }
        else
        {
            toggleTextToSpeechButton.GetComponentInChildren<TextMeshProUGUI>().text = "TEXT NICHT VORLESEN";
        }
    }

    public void OnTermsOfUseButton()
    {
        SceneLoader.LoadTermsOfUseScene();
    }

    public void OnTermsOfUseInfoButton()
    {
        DisplayInfoMessage(InfoMessages.EXPLANATION_TERMS_OF_USE_BUTTON);
    }

    public void OnDataPrivacyButton()
    {
        SceneLoader.LoadPrivacyPolicyScene();
    }

    public void OnDataPrivacyInfoButton()
    {
        DisplayInfoMessage(InfoMessages.EXPLANATION_DATA_PRIVACY_BUTTON);
    }

    public void OnImprintButton()
    {
        SceneLoader.LoadImprintScene();
    }

    public void OnImprintInfoButton()
    {
        DisplayInfoMessage(InfoMessages.EXPLANATION_IMPRINT_BUTTON);
    }

    private void OnPlayerPrefsButton()
    {
        SceneLoader.LoadPlayerPrefsScene();
    }

    private void OnPlayerPrefsInfoButton()
    {
        DisplayInfoMessage(InfoMessages.EXPLANATION_PLAYERPREFS_BUTTON);
    }

    private void OnToggleTextToSpeechButton()
    {
        if (TextToSpeechManager.Instance().IsTextToSpeechActivated())
        {
            TextToSpeechManager.Instance().DeactivateTextToSpeech();
            DisplayInfoMessage(InfoMessages.STARTED_TOGGLETEXTTOSPEECH_BUTTON);
        }
        else
        {
            TextToSpeechManager.Instance().ActivateTextToSpeech();
            DisplayInfoMessage(InfoMessages.STOPPED_TOGGLETEXTTOSPEECH_BUTTON);
        }
        InitializeToggleTextToSpeechButton();
        // If more settings are decired this should lead to an extra scene
    }

    private void OnToggleTextToSpeechInfoButton()
    {
        DisplayInfoMessage(InfoMessages.EXPLANATION_TEXTTOSPEECH_BUTTON);
    }

    private void OnApplicationModeButton()
    {
        if (ApplicationModeManager.Instance().IsOfflineModeActive())
        {
            ApplicationModeManager.Instance().ActivateOnlineMode();
            DisplayInfoMessage(InfoMessages.SWITCHED_TO_ONLINE_MODE);
        }
        else
        {
            ApplicationModeManager.Instance().ActivateOfflineMode();
            DisplayInfoMessage(InfoMessages.SWITCHED_TO_OFFLINE_MODE);
        }
        InitializeApplicationModeButton();
    }

    private void OnApplicationModeInfoButton()
    {
        DisplayInfoMessage(InfoMessages.EXPLANATION_APPLICATION_MODE_BUTTON);
    }
}
