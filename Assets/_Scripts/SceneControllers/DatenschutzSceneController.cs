using Assets._Scripts.Managers;
using Assets._Scripts.Messages;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.UI_Elements.Buttons;
using Assets._Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;

//using LeastSquares.Overtone;

namespace Assets._Scripts.SceneControllers
{
    public class DatenschutzSceneController : SceneController
    {
        [SerializeField] private Button resetAppButton;
        [SerializeField] private Button resetAppInfoButton;
        [SerializeField] private Toggle applicationModeToggle;
        [SerializeField] private Button applicationModeInfoButton;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private RectTransform layout;
        [SerializeField] private RectTransform layout02;
        //[SerializeField] private TTSEngine engine;
        [SerializeField] private DeleteUserDataConfirmation deleteUserDataConfirmDialogObject;
        [SerializeField] private GameObject deleteUserDataConfirmDialog;

        private const string Delete = "delete";
        private const string Origin = "reset";

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.DatenschutzScene);

            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout02);
            audioSource = GetComponent<AudioSource>();

            InitializeApplicationModeButton();

            resetAppButton.onClick.AddListener(OnResetAppButton);
            resetAppInfoButton.onClick.AddListener(OnResetAppInfoButton);
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

            if (DestroyValidator.IsNullOrDestroyed(canvas))
            {
                return;
            }

            deleteUserDataConfirmDialogObject = null;
            deleteUserDataConfirmDialogObject = Instantiate(deleteUserDataConfirmDialog,
                canvas.transform).GetComponent<DeleteUserDataConfirmation>();
            deleteUserDataConfirmDialogObject.Initialize(Delete);
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

            if (DestroyValidator.IsNullOrDestroyed(canvas))
            {
                return;
            }

            deleteUserDataConfirmDialogObject = null;
            deleteUserDataConfirmDialogObject = Instantiate(deleteUserDataConfirmDialog,
                canvas.transform).GetComponent<DeleteUserDataConfirmation>();
            deleteUserDataConfirmDialogObject.Initialize(Origin);
            deleteUserDataConfirmDialogObject.Activate();
        }

        private void OnResetAppInfoButton()
        {
            StartCoroutine(TextToSpeechManager.Instance.Speak(InfoMessages.EXPLANATION_RESET_APP_BUTTON));
            DisplayInfoMessage(InfoMessages.EXPLANATION_RESET_APP_BUTTON);
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