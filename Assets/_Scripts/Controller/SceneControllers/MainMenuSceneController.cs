using System.Collections;
using System.Collections.Generic;
using Assets._Scripts.Managers;
using Assets._Scripts._Mappings;
using Assets._Scripts.Messages;
using Assets._Scripts.Novel;
using Assets._Scripts.Player;
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

        // Guard: ensures that the continuation flow is only started once
        private bool _startedNextFlow;

        // If true, Start() skips the visual setup (because we already forward in Awake())
        private bool _skipVisualSetup;

        /// <summary>
        /// Runs before the first render frame: ideal for forwarding directly when legal terms are already accepted,
        /// without ever making the scene visible.
        /// </summary>
        private void Awake()
        {
            TextToSpeechManager ttsManager = TextToSpeechManager.Instance;
            MappingManager mappingManager = MappingManager.Instance;

            InitializeScene();
            ApplyVolumeFromPrefs();
            SetupButtonListeners();

            var privacyManager = PrivacyAndConditionManager.Instance();
            bool alreadyAccepted = privacyManager.IsConditionsAccepted() && privacyManager.IsPrivacyTermsAccepted();

            if (alreadyAccepted)
            {
                // Szene unsichtbar halten und direkt weiterleiten
                // HideSceneVisuals();
                _skipVisualSetup = true;

                // Optional: if ScreenFade already exists, you can also forward it as follows:
                // if (ScreenFade.Instance) ScreenFade.Instance.FadeToBlackAndLoad(StartNextFlow);
                // else StartNextFlow();
                // StartNextFlow();
                
                if (startButton != null)
                {
                    startButtonPanel.SetActive(true);
                    // background.gameObject.SetActive(true);
                    
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

            // If it has already been accepted at this exact moment, forward it directly.
            // var privacyManager = PrivacyAndConditionManager.Instance();
            // if (privacyManager.IsConditionsAccepted() && privacyManager.IsPrivacyTermsAccepted())
            // {
            //     // Keep the panel visible; start scene change immediately (with fade, if available)
            //     if (ScreenFade.Instance)
            //         ScreenFade.Instance.FadeToBlackAndLoad(StartNextFlow);
            //     else
            //         StartNextFlow();
            // }
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
        /// Wiring button events.
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
                // Panel can remain visible; the change is triggered in Start().
                // No SetActive(false) necessary.
                if (startButton != null)
                {
                    startButtonPanel.gameObject.SetActive(true);
                    // background.gameObject.SetActive(true);
                    
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
            startButton.interactable = false;
            logoButton.interactable = false;

            // Keep the panel visible but prevent interaction
            LockLegalUi();

            // Initiate scene change immediately
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

        /// <summary>
        /// Updates the acceptance status and executes the appropriate action (accept or deny).
        /// </summary>
        /// <param name="isAccepted">A boolean indicating the final decision (true for accepted, false for denied).</param>
        /// <param name="acceptAction">The action/callback to execute if the status is accepted.</param>
        /// <param name="deniedAction">The action/callback to execute if the status is denied.</param>
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
                // Keep the panel visible but prevent interaction
                LockLegalUi();

                // Initiate scene change immediately
                if (ScreenFade.Instance)
                    ScreenFade.Instance.FadeToBlackAndLoad(StartNextFlow);
                else
                    StartNextFlow();
            }
        }

        /// <summary>
        /// The version check remains.
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
        /// Waits until the novels are loaded, then proceeds, according to settings.
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
        /// Searches for the Intro Novel and loads the PlayNovelScene with the necessary metadata.
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
        /// Keeps the panel visible but blocks input (prevents double-click or "click-through").
        /// </summary>
        private void LockLegalUi()
        {
            if (!termsAndConditionPanel) return;

            var cg = termsAndConditionPanel.GetComponent<CanvasGroup>();
            if (!cg) cg = termsAndConditionPanel.AddComponent<CanvasGroup>();

            cg.interactable = false;
            cg.blocksRaycasts = true;

            // additionally disable the Continue button (visually/semantically)
            if (continueTermsAndConditionsButton)
                continueTermsAndConditionsButton.interactable = false;
        }

        /// <summary>
        /// Prevents the scene from becoming visible for one frame (Awake the path when legal terms are already accepted).
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
