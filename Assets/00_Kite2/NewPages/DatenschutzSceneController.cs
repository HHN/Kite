using _00_Kite2.Common;
using _00_Kite2.Common.Managers;
using _00_Kite2.Common.Messages;
using _00_Kite2.Common.SceneManagement;
using _00_Kite2.Common.UI.UI_Elements.Buttons;
using _00_Kite2.Common.Utilities;
using _00_Kite2.Player;
using LeastSquares.Overtone;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.NewPages
{
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

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.DATENSCHUTZ_SCENE);

            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout02);
            audioSource = GetComponent<AudioSource>();

            InitializeToggleDataCollectionButton();
            InitializeApplicationModeButton();

            dataCollectionToggle.onValueChanged.AddListener(delegate { OnToggleDataCollection(dataCollectionToggle); });
            toggleDataCollectionInfoButton.onClick.AddListener(OnToggleDataCollectionInfoButton);
            deleteCollectedDataButton.onClick.AddListener(OnDeleteCollectedDataButton);
            deleteCollectedDataInfoButton.onClick.AddListener(OnDeleteCollectedDataInfoButton);
            resetAppButton.onClick.AddListener(OnResetAppButton);
            resetAppInfoButton.onClick.AddListener(OnResetAppInfoButton);
            playerPrefsButton.onClick.AddListener(OnPlayerPrefsButton);
            playerPrefsInfoButton.onClick.AddListener(OnPlayerPrefsInfoButton);
            applicationModeToggle.onValueChanged.AddListener(delegate
            {
                OnApplicationModeToggle(applicationModeToggle);
            });
            applicationModeInfoButton.onClick.AddListener(OnApplicationModeInfoButton);
            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        private void OnToggleDataCollection(Toggle toggle)
        {
            if (toggle.isOn)
            {
                PrivacyAndConditionManager.Instance().AcceptDataCollection();
                DisplayInfoMessage(InfoMessages.STARTED_DATA_COLLECTION);
                StartCoroutine(TextToSpeechManager.Instance.Speak(InfoMessages.STARTED_DATA_COLLECTION));
            }
            else
            {
                PrivacyAndConditionManager.Instance().UnaccepedDataCollection();
                DisplayInfoMessage(InfoMessages.STOPPED_DATA_COLLECTION);
                StartCoroutine(TextToSpeechManager.Instance.Speak(InfoMessages.STOPPED_DATA_COLLECTION));
            }
        }

        private void OnApplicationModeToggle(Toggle toggle)
        {
            if (toggle.isOn)
            {
                ApplicationModeManager.Instance().ActivateOnlineMode();
                DisplayInfoMessage(InfoMessages.SWITCHED_TO_ONLINE_MODE);
                StartCoroutine(TextToSpeechManager.Instance.Speak(InfoMessages.SWITCHED_TO_ONLINE_MODE));
            }
            else
            {
                ApplicationModeManager.Instance().ActivateOfflineMode();
                DisplayInfoMessage(InfoMessages.SWITCHED_TO_OFFLINE_MODE);
                StartCoroutine(TextToSpeechManager.Instance.Speak(InfoMessages.SWITCHED_TO_OFFLINE_MODE));
            }
        }

        private void OnToggleDataCollectionInfoButton()
        {
            if (PrivacyAndConditionManager.Instance().IsDataCollectionAccepted())
            {
                StartCoroutine(TextToSpeechManager.Instance.Speak(InfoMessages.EXPLANATION_STOP_DATA_BUTTON));
                DisplayInfoMessage(InfoMessages.EXPLANATION_STOP_DATA_BUTTON);
            }
            else
            {
                StartCoroutine(TextToSpeechManager.Instance.Speak(InfoMessages.EXPLANATION_COLLECT_DATA_BUTTON));
                DisplayInfoMessage(InfoMessages.EXPLANATION_COLLECT_DATA_BUTTON);
            }
        }

        private void OnDeleteCollectedDataButton()
        {
            if (!deleteUserDataConfirmDialogObject.IsNullOrDestroyed())
            {
                deleteUserDataConfirmDialogObject.CloseMessageBox();
            }

            if (canvas.IsNullOrDestroyed())
            {
                return;
            }

            deleteUserDataConfirmDialogObject = null;
            deleteUserDataConfirmDialogObject = Instantiate(deleteUserDataConfirmDialog,
                canvas.transform).GetComponent<DeleteUserDataConfirmation>();
            deleteUserDataConfirmDialogObject.Initialize("delete");
            deleteUserDataConfirmDialogObject.Activate();
        }

        private void OnDeleteCollectedDataInfoButton()
        {
            StartCoroutine(TextToSpeechManager.Instance.Speak(InfoMessages.EXPLANATION_DELETE_DATA_BUTTON));
            DisplayInfoMessage(InfoMessages.EXPLANATION_DELETE_DATA_BUTTON);
        }

        private void OnResetAppButton()
        {
            if (!deleteUserDataConfirmDialogObject.IsNullOrDestroyed())
            {
                deleteUserDataConfirmDialogObject.CloseMessageBox();
            }

            if (canvas.IsNullOrDestroyed())
            {
                return;
            }

            deleteUserDataConfirmDialogObject = null;
            deleteUserDataConfirmDialogObject = Instantiate(deleteUserDataConfirmDialog,
                canvas.transform).GetComponent<DeleteUserDataConfirmation>();
            deleteUserDataConfirmDialogObject.Initialize("reset");
            deleteUserDataConfirmDialogObject.Activate();
        }

        private void OnResetAppInfoButton()
        {
            StartCoroutine(TextToSpeechManager.Instance.Speak(InfoMessages.EXPLANATION_RESET_APP_BUTTON));
            DisplayInfoMessage(InfoMessages.EXPLANATION_RESET_APP_BUTTON);
        }

        private void InitializeToggleDataCollectionButton()
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

        private void InitializeApplicationModeButton()
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
            SceneLoader.LoadPlayerPrefsScene();
        }

        private void OnPlayerPrefsInfoButton()
        {
            StartCoroutine(TextToSpeechManager.Instance.Speak(InfoMessages.EXPLANATION_PLAYERPREFS_BUTTON));
            DisplayInfoMessage(InfoMessages.EXPLANATION_PLAYERPREFS_BUTTON);
        }


        private void OnApplicationModeInfoButton()
        {
            StartCoroutine(TextToSpeechManager.Instance.Speak(InfoMessages.EXPLANATION_APPLICATION_MODE_BUTTON));
            DisplayInfoMessage(InfoMessages.EXPLANATION_APPLICATION_MODE_BUTTON);
        }
    }
}