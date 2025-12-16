using System.Collections;
using System.Collections.Generic;
using Assets._Scripts.Managers;
using Assets._Scripts._Mappings;
using Assets._Scripts.Messages;
using Assets._Scripts.Novel;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.ServerCommunication;
using Assets._Scripts.ServerCommunication.SceneMetrics;
using Assets._Scripts.UIElements;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    /// <summary>
    /// Controls the main menu scene of the application, managing user interactions
    /// and handling server communication responses specific to the main menu context.
    /// </summary>
    public class MainMenuSceneController : SceneController, IOnSuccessHandler
    {
        [SerializeField] private GameObject termsAndConditionPanel;
        [SerializeField] private Button continueTermsAndConditionsButton;
        [SerializeField] private CustomToggle termsOfUseToggle;
        [SerializeField] private CustomToggle dataPrivacyToggle;
        [SerializeField] private GameObject novelLoader;
        [SerializeField] private TMP_Text versionInfo;
        [SerializeField] private GameObject startButtonPanel;
        [SerializeField] private Button startButton;
        [SerializeField] private Button logoButton;
        [SerializeField] private Image background;

        private const int CompatibleServerVersionNumber = 10;

        private bool _startedNextFlow;
        private bool _skipVisualSetup;

        private void Awake()
        {
            TextToSpeechManager ttsManager = TextToSpeechManager.Instance;
            MappingManager mappingManager = MappingManager.Instance;
            
            InitializeScene();
            ApplyVolumeFromPrefs();
            SetupButtonListeners();

            var privacyManager = PrivacyAndConditionManager.Instance();
            bool alreadyAccepted = privacyManager.IsConditionsAccepted() && privacyManager.IsPrivacyTermsAccepted();
            
            GeneratedFeedbackManager.Instance.LoadFeedbacks();

            if (alreadyAccepted)
            {
                _skipVisualSetup = true;
                
                if (startButton != null)
                {
                    startButtonPanel.SetActive(true);
                    termsAndConditionPanel.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Initializes the main menu scene and its components when the scene starts.
        /// </summary>
        private void Start()
        {
            if (!PlayerPrefs.HasKey("StartScene") || PlayerPrefs.GetInt("StartScene") == 0)
            {
                PlayerPrefs.SetInt("StartScene", 1);
                PlayerPrefs.Save();
                StartCoroutine(SceneMetricsClient.Hit(SceneType.StartScene));
            }

            if (_skipVisualSetup) return;

            InitializeScene();
            HandleTermsAndConditions();

            if (versionInfo != null)
                versionInfo.text = Application.version;
        }

        /// <summary>
        /// Prepares and initializes required components for the scene.
        /// Ensures certain game objects persist across scenes,
        /// loads player preferences, and resets the back stack manager.
        /// </summary>
        private void InitializeScene()
        {
            if (novelLoader != null)
                DontDestroyOnLoad(novelLoader);

            PlayerDataManager.Instance().LoadAllPlayerPrefs();
            BackStackManager.Instance.Clear();
        }

        /// <summary>
        /// Sets the global volume according to stored PlayerPrefs.
        /// </summary>
        private void ApplyVolumeFromPrefs()
        {
            if (PlayerPrefs.GetInt("IsSoundEffectVolumeOn", 1) == 1)
                GlobalVolumeManager.Instance.SetGlobalVolume(PlayerPrefs.GetFloat("SavedSoundEffectVolume", 1));
            else
                GlobalVolumeManager.Instance.SetGlobalVolume(0);
        }

        /// <summary>
        /// Wire button events.
        /// </summary>
        private void SetupButtonListeners()
        {
            continueTermsAndConditionsButton.onClick.AddListener(OnContinueTermsAndConditionsButton);
            startButton.onClick.AddListener(OnStartButtonClicked);
            logoButton.onClick.AddListener(OnStartButtonClicked);
        }

        /// <summary>
        /// Show/hide panel depending on acceptance status (no forced hide when accepted,
        /// because we leave the panel visible during the transition).
        /// </summary>
        private void HandleTermsAndConditions()
        {
            var privacyManager = PrivacyAndConditionManager.Instance();

            if (privacyManager.IsConditionsAccepted() && privacyManager.IsPrivacyTermsAccepted())
            {
                if (startButton != null)
                {
                    startButtonPanel.gameObject.SetActive(true);
                    termsAndConditionPanel.SetActive(false);
                }
            }
            else
            {
                if (termsAndConditionPanel) termsAndConditionPanel.SetActive(true);
            }
        }

        /// <summary>
        /// Click on “Continue” in the legal panel.
        /// </summary>
        private void OnContinueTermsAndConditionsButton()
        {
            UpdateTermsAcceptance();
            ValidateTermsAndLoadScene();
        }
        
        /// <summary>
        /// Called when the Start button is clicked.
        /// </summary>
        private void OnStartButtonClicked()
        {
            Debug.Log("Start Button clicked");

            startButton.interactable = false;
            logoButton.interactable = false;

            LockLegalUi();

            if (ScreenFade.Instance)
                ScreenFade.Instance.FadeToBlackAndLoad(StartNextFlow);
            else
                StartNextFlow();
        }

        /// <summary>
        /// Updates the acceptance status via the PrivacyAndConditionManager.
        /// </summary>
        private void UpdateTermsAcceptance()
        {
            var privacyManager = PrivacyAndConditionManager.Instance();

            UpdateAcceptance(
                termsOfUseToggle.IsClicked(),
                privacyManager.AcceptConditionsOfUsage,
                privacyManager.UnacceptedConditionsOfUsage);

            UpdateAcceptance(
                dataPrivacyToggle.IsClicked(),
                privacyManager.AcceptTermsOfPrivacy,
                privacyManager.UnacceptedTermsOfPrivacy);
        }

        private void UpdateAcceptance(bool isAccepted, System.Action acceptAction, System.Action deniedAction)
        {
            if (isAccepted) acceptAction();
            else deniedAction();
        }

        /// <summary>
        /// Immediately after acceptance: Panel remains visible, interaction is blocked,
        /// and the scene change starts IMMEDIATELY (with fade, if available).
        /// </summary>
        private void ValidateTermsAndLoadScene()
        {
            bool acceptedTermsOfUse = termsOfUseToggle.IsClicked();
            bool acceptedDataPrivacyTerms = dataPrivacyToggle.IsClicked();

            if (acceptedTermsOfUse && acceptedDataPrivacyTerms)
            {
                LockLegalUi();

                if (ScreenFade.Instance)
                    ScreenFade.Instance.FadeToBlackAndLoad(StartNextFlow);
                else
                    StartNextFlow();
            }
        }

        /// <summary>
        /// Version check remains unchanged.
        /// </summary>
        public void OnSuccess(Response response)
        {
            if (response == null) return;

            if (response.GetVersion() != CompatibleServerVersionNumber)
            {
                DisplayInfoMessage(InfoMessages.UPDATE_AVAILABLE);
            }
        }

        /// <summary>
        /// Starts the transition to the actual game/next scene (only once).
        /// </summary>
        private void StartNextFlow()
        {
            if (_startedNextFlow) return;
            _startedNextFlow = true;

            StartCoroutine(WaitForNovelsToLoad());
        }

        /// <summary>
        /// Waits until the novels are loaded, then continues according to the settings.
        /// </summary>
        private IEnumerator WaitForNovelsToLoad()
        {
            MappingManager.allNovels = null;

            while (MappingManager.allNovels == null || MappingManager.allNovels.Count == 0)
            {
                MappingManager.allNovels = KiteNovelManager.Instance().GetAllKiteNovels();
                yield return new WaitForSeconds(0.5f);
            }

            if (!GameManager.Instance.SkipIntroNovel)
            {
                StartIntroNovel(MappingManager.allNovels);
            }
            else
            {
                SceneLoader.LoadFoundersBubbleScene();
            }
        }

        /// <summary>
        /// Searches for “Einstiegsnovel” and loads the PlayNovelScene with set metadata.
        /// </summary>
        private void StartIntroNovel(List<VisualNovel> allNovels)
        {
            if (!PlayerPrefs.HasKey("Einstiegsnovel") || PlayerPrefs.GetInt("Einstiegsnovel") == 0)
            {
                PlayerPrefs.SetInt("Einstiegsnovel", 1);
                PlayerPrefs.Save();
                StartCoroutine(SceneMetricsClient.Hit(SceneType.Einstiegsnovel));
            }

            foreach (var novel in allNovels)
            {
                if (novel.id == 13)
                {
                    GameManager.Instance.IsIntroNovelLoadedFromMainMenu = true;

                    PlayManager.Instance().SetVisualNovelToPlay(novel);

                    SceneLoader.LoadPlayNovelScene();
                    break;
                }
            }
        }

        /// <summary>
        /// Keep panel visible, but disable input (no double-clicking, no “clicking through”).
        /// </summary>
        private void LockLegalUi()
        {
            if (!termsAndConditionPanel) return;

            var cg = termsAndConditionPanel.GetComponent<CanvasGroup>();
            if (!cg) cg = termsAndConditionPanel.AddComponent<CanvasGroup>();

            cg.interactable = false;
            cg.blocksRaycasts = true;

            if (continueTermsAndConditionsButton)
                continueTermsAndConditionsButton.interactable = false;
        }

        /// <summary>
        /// Prevents the scene from becoming visible for one frame (Awake path for legal information that has already been accepted).
        /// </summary>
        private void HideSceneVisuals()
        {
            var objectOfTypeCanvas = FindObjectOfType<Canvas>();
            if (objectOfTypeCanvas) objectOfTypeCanvas.enabled = false;

            var cam = Camera.main;
            if (cam)
            {
                cam.clearFlags = CameraClearFlags.SolidColor;
                cam.backgroundColor = Color.black;
            }
        }
    }
}
