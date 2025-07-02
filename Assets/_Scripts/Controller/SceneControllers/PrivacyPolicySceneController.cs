using Assets._Scripts.Managers;
using Assets._Scripts.Messages;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.UIElements.Buttons;
using Assets._Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;

//using LeastSquares.Overtone;

namespace Assets._Scripts.Controller.SceneControllers
{
    public class PrivacyPolicySceneController : SceneController
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

        private const string Origin = "reset";

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.PrivacyPolicyScene);

            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout02);
            audioSource = GetComponent<AudioSource>();

            InitializeApplicationModeButton();

            resetAppButton.onClick.AddListener(OnResetAppButton);
            resetAppInfoButton.onClick.AddListener(OnResetAppInfoButton);
            applicationModeToggle.onValueChanged.AddListener(delegate { OnApplicationModeToggle(applicationModeToggle); });
            applicationModeInfoButton.onClick.AddListener(OnApplicationModeInfoButton);
            FontSizeManager.Instance().UpdateAllTextComponents();
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
            deleteUserDataConfirmDialogObject = Instantiate(deleteUserDataConfirmDialog, canvas.transform).GetComponent<DeleteUserDataConfirmation>();
            deleteUserDataConfirmDialogObject.Initialize(Origin);
            deleteUserDataConfirmDialogObject.Activate();

            GameManager.Instance.canvas = canvas;
        }

        private void OnResetAppInfoButton()
        {
            StartCoroutine(TextToSpeechManager.Instance.Speak(InfoMessages.EXPLANATION_RESET_APP_BUTTON));
            DisplayInfoMessage(InfoMessages.EXPLANATION_RESET_APP_BUTTON);
        }


        private void InitializeApplicationModeButton()
        {
            applicationModeToggle.isOn = !ApplicationModeManager.Instance().IsOfflineModeActive();
        }


        private void OnApplicationModeInfoButton()
        {
            StartCoroutine(TextToSpeechManager.Instance.Speak(InfoMessages.EXPLANATION_APPLICATION_MODE_BUTTON));
            DisplayInfoMessage(InfoMessages.EXPLANATION_APPLICATION_MODE_BUTTON);
        }
    }
}