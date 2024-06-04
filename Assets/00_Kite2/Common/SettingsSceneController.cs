using TMPro;
using UnityEngine;
using UnityEngine.UI;
using LeastSquares.Overtone;

public class SettingsSceneController : SceneController
{
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
    [SerializeField] private Button expertFeedbackButton;
    [SerializeField] private Button expertFeedbackInfoButton;
    [SerializeField] private AudioSource audioSource;
    
    [SerializeField] private TTSEngine engine;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.SETTINGS_SCENE);

        audioSource = GetComponent<AudioSource>();
        TextToSpeechService.Instance().SetAudioSource(audioSource);

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
        expertFeedbackButton.onClick.AddListener(delegate { OnExpertFeedbackButton(); });
        expertFeedbackInfoButton.onClick.AddListener(delegate { OnExpertFeedbackInfoButton(); });

        InitializeToggleDataCollectionButton();
        InitializeApplicationModeButton();
        InitializeToggleTextToSpeechButton();
    }

    public void OnAboutKiteButton()
    {
        TextToSpeechService.Instance().TextToSpeechReadLive("ueberKITE", engine);
        SceneLoader.LoadInfoScene();
    }

    public void OnAboutKiteInfoButton()
    {
        TextToSpeechService.Instance().TextToSpeechReadLive("infoSeiteInfo", engine);
        DisplayInfoMessage(InfoMessages.EXPLANATION_ABOUT_US_BUTTON);
    }

    public void OnToggleDataCollectionButton()
    {
        if (PrivacyAndConditionManager.Instance().IsDataCollectionAccepted())
        {
            PrivacyAndConditionManager.Instance().UnacceptDataCollection();
            DisplayInfoMessage(InfoMessages.STOPPED_DATA_COLLECTION);
        }
        else
        {
            PrivacyAndConditionManager.Instance().AcceptDataCollection();
            DisplayInfoMessage(InfoMessages.STARTED_DATA_COLLECTION);
        }
        InitializeToggleDataCollectionButton();
        if (PrivacyAndConditionManager.Instance().IsDataCollectionAccepted())
        {
            TextToSpeechService.Instance().TextToSpeechReadLive("startDatenaufzeichnung", engine);
        } else {
            TextToSpeechService.Instance().TextToSpeechReadLive("stopDatenaufzeichnung", engine);
        }
    }

    public void OnToggleDataCollectionInfoButton()
    {
        if (PrivacyAndConditionManager.Instance().IsDataCollectionAccepted())
        {
            TextToSpeechService.Instance().TextToSpeechReadLive("datenaufzeichnungAktiviert", engine);
            DisplayInfoMessage(InfoMessages.EXPLANATION_STOP_DATA_BUTTON);
        } 
        else
        {
            TextToSpeechService.Instance().TextToSpeechReadLive("datenaufzeichnungDeaktivieren", engine);
            DisplayInfoMessage(InfoMessages.EXPLANATION_COLLECT_DATA_BUTTON);
        }
    }

    public void OnDeleteCollectedDataButton()
    {
        TextToSpeechService.Instance().TextToSpeechReadLive("datenloeschungErfolgreichNotification", engine);
        AnalyticsServiceHandler.Instance().DeleteCollectedData();
        DisplayInfoMessage(InfoMessages.DELETED_DATA);
    }

    public void OnDeleteCollectedDataInfoButton()
    {
        TextToSpeechService.Instance().TextToSpeechReadLive("datenloeschungInfo", engine);
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
            toggleTextToSpeechButton.GetComponentInChildren<TextMeshProUGUI>().text = "TEXT NICHT VORLESEN";
        }
        else
        {
            toggleTextToSpeechButton.GetComponentInChildren<TextMeshProUGUI>().text = "TEXT VORLESEN";
        }
    }

    public void OnTermsOfUseButton()
    {
        //TextToSpeechService.Instance().TextToSpeech("");
        SceneLoader.LoadTermsOfUseScene();
    }

    public void OnTermsOfUseInfoButton()
    {
        TextToSpeechService.Instance().TextToSpeechReadLive("nutzungsbedingungenInfo", engine);
        DisplayInfoMessage(InfoMessages.EXPLANATION_TERMS_OF_USE_BUTTON);
    }

    public void OnDataPrivacyButton()
    {
        //TextToSpeechService.Instance().TextToSpeech("");
        SceneLoader.LoadPrivacyPolicyScene();
    }

    public void OnDataPrivacyInfoButton()
    {
        TextToSpeechService.Instance().TextToSpeechReadLive("datenschutzerklaerungInfo", engine);
        DisplayInfoMessage(InfoMessages.EXPLANATION_DATA_PRIVACY_BUTTON);
    }

    public void OnImprintButton()
    {
        //TextToSpeechService.Instance().TextToSpeech("");
        SceneLoader.LoadImprintScene();
    }

    public void OnImprintInfoButton()
    {
        TextToSpeechService.Instance().TextToSpeechReadLive("impressumInfo", engine);
        DisplayInfoMessage(InfoMessages.EXPLANATION_IMPRINT_BUTTON);
    }

    private void OnPlayerPrefsButton()
    {
        //TextToSpeechService.Instance().TextToSpeech("");
        SceneLoader.LoadPlayerPrefsScene();
    }

    private void OnPlayerPrefsInfoButton()
    {
        TextToSpeechService.Instance().TextToSpeechReadLive("playerPrefsInfo", engine);
        DisplayInfoMessage(InfoMessages.EXPLANATION_PLAYERPREFS_BUTTON);
    }

    private void OnToggleTextToSpeechButton()
    {
        if (TextToSpeechManager.Instance().IsTextToSpeechActivated())
        {
            TextToSpeechManager.Instance().DeactivateTextToSpeech();
            DisplayInfoMessage(InfoMessages.STOPPED_TOGGLETEXTTOSPEECH_BUTTON);
        }
        else
        {
            TextToSpeechManager.Instance().ActivateTextToSpeech();
            DisplayInfoMessage(InfoMessages.STARTED_TOGGLETEXTTOSPEECH_BUTTON);
        }
        InitializeToggleTextToSpeechButton();
        if (TextToSpeechManager.Instance().IsTextToSpeechActivated())
        {
            TextToSpeechService.Instance().TextToSpeechReadLive("textWirdVorgelesen", engine);
        } else {
            TextToSpeechService.Instance().TextToSpeechReadLive("textWirdNichtMehrVorgelesen", engine, true);
        }
        // If more settings are decired this should lead to an extra scene
    }

    private void OnToggleTextToSpeechInfoButton()
    {
        TextToSpeechService.Instance().TextToSpeechReadLive("textToSpeechInfo", engine);
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
        if (ApplicationModeManager.Instance().IsOfflineModeActive())
        {
            TextToSpeechService.Instance().TextToSpeechReadLive("offlineModus", engine);
        } else {
            TextToSpeechService.Instance().TextToSpeechReadLive("onlineModus", engine);
        }
    }

    private void OnApplicationModeInfoButton()
    {
        TextToSpeechService.Instance().TextToSpeechReadLive("onlineOfflineModusInfo", engine);
        DisplayInfoMessage(InfoMessages.EXPLANATION_APPLICATION_MODE_BUTTON);
    }

    private void OnExpertFeedbackButton()
    {
        SceneLoader.LoadExpertFeedbackScene();
    }

    private void OnExpertFeedbackInfoButton()
    {
        DisplayInfoMessage(InfoMessages.EXPLANATION_EXPERT_FEEDBACK_BUTTON);
    }
}
