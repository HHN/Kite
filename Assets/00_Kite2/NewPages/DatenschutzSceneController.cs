using UnityEngine;
using UnityEngine.UI;

public class DatenschutzSceneController : SceneController
{
    [SerializeField] private Toggle dataCollectionToggle;
    [SerializeField] private Button toggleDataCollectionInfoButton;
    [SerializeField] private Button deleteCollectedDataButton;
    [SerializeField] private Button deleteCollectedDataInfoButton;
    [SerializeField] private Button playerPrefsButton;
    [SerializeField] private Button playerPrefsInfoButton;
    [SerializeField] private Toggle applicationModeToggle;
    [SerializeField] private Button applicationModeInfoButton;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.DATENSCHUTZ_SCENE);

        audioSource = GetComponent<AudioSource>();
        TextToSpeechService.Instance().SetAudioSource(audioSource);

        InitializeToggleDataCollectionButton();
        InitializeApplicationModeButton();

        dataCollectionToggle.onValueChanged.AddListener(delegate { OnToggleDataCollection(dataCollectionToggle); }); toggleDataCollectionInfoButton.onClick.AddListener(delegate { OnToggleDataCollectionInfoButton(); });
        deleteCollectedDataButton.onClick.AddListener(delegate { OnDeleteCollectedDataButton(); });
        deleteCollectedDataInfoButton.onClick.AddListener(delegate { OnDeleteCollectedDataInfoButton(); });
        playerPrefsButton.onClick.AddListener(delegate { OnPlayerPrefsButton(); });
        playerPrefsInfoButton.onClick.AddListener(delegate { OnPlayerPrefsInfoButton(); });
        applicationModeToggle.onValueChanged.AddListener(delegate { OnApplicationModeToggle(applicationModeToggle); });
        applicationModeInfoButton.onClick.AddListener(delegate { OnApplicationModeInfoButton(); });
    }
    public void OnToggleDataCollection(Toggle toggle)
    {
        if (toggle.isOn)
        {
            PrivacyAndConditionManager.Instance().AcceptDataCollection();
            DisplayInfoMessage(InfoMessages.STARTED_DATA_COLLECTION);
            TextToSpeechService.Instance().TextToSpeech("startDatenaufzeichnung");
        }
        else
        {
            PrivacyAndConditionManager.Instance().UnacceptDataCollection();
            DisplayInfoMessage(InfoMessages.STOPED_DATA_COLLECTION);
            TextToSpeechService.Instance().TextToSpeech("stopDatenaufzeichnung");

        }
    }

    public void OnApplicationModeToggle(Toggle toggle)
    {
        if (toggle.isOn)
        {
            ApplicationModeManager.Instance().ActivateOnlineMode();
            DisplayInfoMessage(InfoMessages.SWITCHED_TO_ONLINE_MODE);
            TextToSpeechService.Instance().TextToSpeech("onlineModus");
        }
        else
        {
            ApplicationModeManager.Instance().ActivateOfflineMode();
            DisplayInfoMessage(InfoMessages.SWITCHED_TO_OFFLINE_MODE);
            TextToSpeechService.Instance().TextToSpeech("offlineModus");
        }
    }

    public void OnToggleDataCollectionInfoButton()
    {
        if (PrivacyAndConditionManager.Instance().IsDataCollectionAccepted())
        {
            TextToSpeechService.Instance().TextToSpeech("datenaufzeichnungAktiviert");
            DisplayInfoMessage(InfoMessages.EXPLANATION_STOP_DATA_BUTTON);
        }
        else
        {
            TextToSpeechService.Instance().TextToSpeech("datenaufzeichnungDeaktivieren");
            DisplayInfoMessage(InfoMessages.EXPLANATION_COLLECT_DATA_BUTTON);
        }
    }

    public void OnDeleteCollectedDataButton()
    {
        TextToSpeechService.Instance().TextToSpeech("datenloeschungErfolgreichNotification");
        AnalyticsServiceHandler.Instance().DeleteCollectedData();
        DisplayInfoMessage(InfoMessages.DELETED_DATA);
    }

    public void OnDeleteCollectedDataInfoButton()
    {
        TextToSpeechService.Instance().TextToSpeech("datenloeschungInfo");
        DisplayInfoMessage(InfoMessages.EXPLANATION_DELETE_DATA_BUTTON);
    }

    public void InitializeToggleDataCollectionButton()
    {
        if (PrivacyAndConditionManager.Instance().IsDataCollectionAccepted())
        {
            dataCollectionToggle.isOn = true;
        }
        else
        {
            dataCollectionToggle.isOn = false;
        }
    }

    public void InitializeApplicationModeButton()
    {
        if (ApplicationModeManager.Instance().IsOfflineModeActive())
        {
            applicationModeToggle.isOn = false;
        }
        else
        {
            applicationModeToggle.isOn = true;
        }
    }

    private void OnPlayerPrefsButton()
    {
        //TextToSpeechService.Instance().TextToSpeech("");
        SceneLoader.LoadPlayerPrefsScene();
    }

    private void OnPlayerPrefsInfoButton()
    {
        TextToSpeechService.Instance().TextToSpeech("playerPrefsInfo");
        DisplayInfoMessage(InfoMessages.EXPLANATION_PLAYERPREFS_BUTTON);
    }


    private void OnApplicationModeInfoButton()
    {
        TextToSpeechService.Instance().TextToSpeech("onlineOfflineModusInfo");
        DisplayInfoMessage(InfoMessages.EXPLANATION_APPLICATION_MODE_BUTTON);
    }
}
