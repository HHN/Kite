using UnityEngine;
using UnityEngine.UI;
using LeastSquares.Overtone;

public class DatenschutzSceneController : SceneController
{
    [SerializeField] private Toggle dataCollectionToggle;
    [SerializeField] private Button toggleDataCollectionInfoButton;
    [SerializeField] private Button deleteCollectedDataButton;
    [SerializeField] private Button deleteCollectedDataInfoButton;
    [SerializeField] private Button resetAppButton;
    [SerializeField] private Button resetAppInfoButton;
    [SerializeField] private Button playerPrefsButton;
    [SerializeField] private Button playerPrefsInfoButton;
    [SerializeField] private Toggle applicationModeToggle;
    [SerializeField] private Button applicationModeInfoButton;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private RectTransform layout;
    [SerializeField] private RectTransform layout02;
    [SerializeField] private TTSEngine engine;
    [SerializeField] private DeleteUserDataConfirmation deleteUserDataConfirmDialogObject;
    [SerializeField] private GameObject deleteUserDataConfirmDialog;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.DATENSCHUTZ_SCENE);

        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout02);
        audioSource = GetComponent<AudioSource>();
        TextToSpeechService.Instance().SetAudioSource(audioSource);

        InitializeToggleDataCollectionButton();
        InitializeApplicationModeButton();

        dataCollectionToggle.onValueChanged.AddListener(delegate { OnToggleDataCollection(dataCollectionToggle); }); toggleDataCollectionInfoButton.onClick.AddListener(delegate { OnToggleDataCollectionInfoButton(); });
        deleteCollectedDataButton.onClick.AddListener(delegate { OnDeleteCollectedDataButton(); });
        deleteCollectedDataInfoButton.onClick.AddListener(delegate { OnDeleteCollectedDataInfoButton(); });
        resetAppButton.onClick.AddListener(delegate { OnResetAppButton(); });
        resetAppInfoButton.onClick.AddListener(delegate { OnResetAppInfoButton(); });
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
            TextToSpeechService.Instance().TextToSpeechReadLive(InfoMessages.STARTED_DATA_COLLECTION, engine);
        }
        else
        {
            PrivacyAndConditionManager.Instance().UnacceptDataCollection();
            DisplayInfoMessage(InfoMessages.STOPPED_DATA_COLLECTION);
            TextToSpeechService.Instance().TextToSpeechReadLive(InfoMessages.STOPPED_DATA_COLLECTION, engine);

        }
    }

    public void OnApplicationModeToggle(Toggle toggle)
    {
        if (toggle.isOn)
        {
            ApplicationModeManager.Instance().ActivateOnlineMode();
            DisplayInfoMessage(InfoMessages.SWITCHED_TO_ONLINE_MODE);
            TextToSpeechService.Instance().TextToSpeechReadLive(InfoMessages.SWITCHED_TO_ONLINE_MODE, engine);
        }
        else
        {
            ApplicationModeManager.Instance().ActivateOfflineMode();
            DisplayInfoMessage(InfoMessages.SWITCHED_TO_OFFLINE_MODE);
            TextToSpeechService.Instance().TextToSpeechReadLive(InfoMessages.SWITCHED_TO_OFFLINE_MODE, engine);
        }
    }

    public void OnToggleDataCollectionInfoButton()
    {
        if (PrivacyAndConditionManager.Instance().IsDataCollectionAccepted())
        {
            TextToSpeechService.Instance().TextToSpeechReadLive(InfoMessages.EXPLANATION_STOP_DATA_BUTTON, engine);
            DisplayInfoMessage(InfoMessages.EXPLANATION_STOP_DATA_BUTTON);
        }
        else
        {
            TextToSpeechService.Instance().TextToSpeechReadLive(InfoMessages.EXPLANATION_COLLECT_DATA_BUTTON, engine);
            DisplayInfoMessage(InfoMessages.EXPLANATION_COLLECT_DATA_BUTTON);
        }
    }

    public void OnDeleteCollectedDataButton()
    {
        if (!DestroyValidator.IsNullOrDestroyed(deleteUserDataConfirmDialogObject))
        {
            deleteUserDataConfirmDialogObject.CloseMessageBox();
        }
        if (DestroyValidator.IsNullOrDestroyed(canvas))
        {
            return;
        }
        deleteUserDataConfirmDialogObject = null;
        deleteUserDataConfirmDialogObject = Instantiate(deleteUserDataConfirmDialog, 
            canvas.transform).GetComponent<DeleteUserDataConfirmation>();
        deleteUserDataConfirmDialogObject.Initialize("delete");
        deleteUserDataConfirmDialogObject.Activate();
    }

    public void OnDeleteCollectedDataInfoButton()
    {
        TextToSpeechService.Instance().TextToSpeechReadLive(InfoMessages.EXPLANATION_DELETE_DATA_BUTTON, engine);
        DisplayInfoMessage(InfoMessages.EXPLANATION_DELETE_DATA_BUTTON);
    }

    public void OnResetAppButton()
    {
        if (!DestroyValidator.IsNullOrDestroyed(deleteUserDataConfirmDialogObject))
        {
            deleteUserDataConfirmDialogObject.CloseMessageBox();
        }
        if (DestroyValidator.IsNullOrDestroyed(canvas))
        {
            return;
        }
        deleteUserDataConfirmDialogObject = null;
        deleteUserDataConfirmDialogObject = Instantiate(deleteUserDataConfirmDialog,
            canvas.transform).GetComponent<DeleteUserDataConfirmation>();
        deleteUserDataConfirmDialogObject.Initialize("reset");
        deleteUserDataConfirmDialogObject.Activate();
    }

    public void OnResetAppInfoButton()
    {
        TextToSpeechService.Instance().TextToSpeechReadLive(InfoMessages.EXPLANATION_RESET_APP_BUTTON, engine);
        DisplayInfoMessage(InfoMessages.EXPLANATION_RESET_APP_BUTTON);
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
        TextToSpeechService.Instance().TextToSpeechReadLive(InfoMessages.EXPLANATION_PLAYERPREFS_BUTTON, engine);
        DisplayInfoMessage(InfoMessages.EXPLANATION_PLAYERPREFS_BUTTON);
    }


    private void OnApplicationModeInfoButton()
    {
        TextToSpeechService.Instance().TextToSpeechReadLive(InfoMessages.EXPLANATION_APPLICATION_MODE_BUTTON, engine);
        DisplayInfoMessage(InfoMessages.EXPLANATION_APPLICATION_MODE_BUTTON);
    }
}
