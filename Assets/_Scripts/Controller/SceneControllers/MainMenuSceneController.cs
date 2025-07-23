using System.Collections;
using System.Collections.Generic;
using Assets._Scripts.Managers;
using Assets._Scripts._Mappings;
using Assets._Scripts.Messages;
using Assets._Scripts.Novel;
using Assets._Scripts.Player;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.ServerCommunication;
using Assets._Scripts.ServerCommunication.ServerCalls;
using Assets._Scripts.UIElements;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Assets._Scripts.Controller.SceneControllers
{
    /// <summary>
    /// Controls the main menu scene of the application, managing user interactions
    /// and handling server communication responses specific to the main menu context.
    /// Inherits from the <see cref="SceneController"/> class and implements the
    /// <see cref="IOnSuccessHandler"/> interface.
    /// </summary>
    public class MainMenuSceneController : SceneController, IOnSuccessHandler
    {
        [SerializeField] private GameObject termsAndConditionPanel;
        [SerializeField] private Button continueTermsAndConditionsButton;
        [SerializeField] private CustomToggle termsOfUseToggle;
        [SerializeField] private CustomToggle dataPrivacyToggle;
        [SerializeField] private CustomToggle collectDataToggle;
        [SerializeField] private TextMeshProUGUI infoTextTermsAndConditions;
        [SerializeField] private GameObject getVersionServerCallPrefab;
        [SerializeField] private GameObject novelLoader;
        [SerializeField] private TMP_Text versionInfo;
        
        private const int CompatibleServerVersionNumber = 10;

        /// <summary>
        /// Initializes the main menu scene and its components when the scene starts.
        /// This includes initializing the necessary managers, setting up button listeners,
        /// handling terms and conditions, and setting application version information.
        /// It also configures global sound effect volume based on player preferences and
        /// conditionally starts loading novel data if required settings are accepted.
        /// </summary>
        private void Start()
        {
            // Initialize the TextToSpeechManager
            TextToSpeechManager ttsManager = TextToSpeechManager.Instance;

            MappingManager mappingManager = MappingManager.Instance;
            // Initialize the scene and set basic settings
            InitializeScene();

            // Set up button listeners to respond to user input
            SetupButtonListeners();

            // Handle terms and conditions and privacy policy
            HandleTermsAndConditions();

            // Get an instance of PrivacyManager to check the current status of privacy acceptance
            var privacyManager = PrivacyAndConditionManager.Instance();

            versionInfo.text = Application.version;

            if (PlayerPrefs.GetInt("IsSoundEffectVolumeOn", 1) == 1)
            {
                GlobalVolumeManager.Instance.SetGlobalVolume(PlayerPrefs.GetFloat("SavedSoundEffectVolume", 1));
            }
            else
            {
                GlobalVolumeManager.Instance.SetGlobalVolume(0);
            }

            // Check if terms and conditions and privacy policy have been accepted
            if (privacyManager.IsConditionsAccepted() && privacyManager.IsPrivacyTermsAccepted())
            {
                // Start a coroutine that waits for the novels to load
                StartCoroutine(WaitForNovelsToLoad());
            }
        }

        /// <summary>
        /// Prepares and initializes required components for the scene.
        /// Ensures certain game objects persist across scenes, starts analytics,
        /// loads player preferences, and resets the back stack manager.
        /// </summary>
        private void InitializeScene()
        {
            DontDestroyOnLoad(novelLoader);

            //AnalyticsServiceHandler.Instance().StartAnalytics();  //TODO: Replace with custom Analytics
            PlayerDataManager.Instance().LoadAllPlayerPrefs();
            BackStackManager.Instance().Clear();
        }

        /// <summary>
        /// Configures button click event listeners for the main menu scene.
        /// This involves assigning methods to handle user interactions with
        /// various UI buttons, initiating appropriate behaviors or navigation
        /// responses within the main menu context.
        /// </summary>
        private void SetupButtonListeners()
        {
            continueTermsAndConditionsButton.onClick.AddListener(OnContinueTermsAndConditionsButton);
        }

        /// <summary>
        /// Handles the verification and acceptance of terms and conditions as well as privacy policies.
        /// If the terms and conditions and privacy policies are accepted, the related UI panel is hidden,
        /// and additional actions such as enabling data collection and checking the application version
        /// are performed if their prerequisites are met.
        /// </summary>
        private void HandleTermsAndConditions()
        {
            var privacyManager = PrivacyAndConditionManager.Instance();

            if (privacyManager.IsConditionsAccepted() && privacyManager.IsPrivacyTermsAccepted())
            {
                termsAndConditionPanel.SetActive(false);

                if (privacyManager.IsDataCollectionAccepted())
                {
                    //AnalyticsServiceHandler.Instance().CollectData(); //TODO: Replace with custom Analytics
                }

                if (!ApplicationModeManager.Instance().IsOfflineModeActive())
                {
                    StartVersionCheck();
                }
            }
        }

        /// <summary>
        /// Initiates a version check with the server to ensure the application is compatible
        /// with the current server version. This method creates and configures an instance of
        /// the GetVersionServerCall, sets the necessary callbacks and controllers, and sends
        /// the request. The created server call object is marked to persist across scene loads.
        /// </summary>
        private void StartVersionCheck()
        {
            var call = Instantiate(getVersionServerCallPrefab).GetComponent<GetVersionServerCall>();
            call.sceneController = this;
            call.OnSuccessHandler = this;
            call.SendRequest();
            DontDestroyOnLoad(call.gameObject);
        }

        /// <summary>
        /// Handles the "Continue" button click event within the terms and conditions section of the main menu.
        /// This method updates the user's acceptance states for terms of use, privacy, and data collection
        /// and validates the overall acceptance before proceeding to load the appropriate scene or display
        /// the necessary messages based on the acceptance status.
        /// </summary>
        private void OnContinueTermsAndConditionsButton()
        {
            UpdateTermsAcceptance();
            ValidateTermsAndLoadScene();
        }

        /// <summary>
        /// Updates the acceptance state of terms and conditions based on the user's interaction
        /// with the corresponding toggles. This includes handling terms of use, data privacy,
        /// and optional data collection preferences. The method communicates the user's choices
        /// to the PrivacyAndConditionManager and invokes the appropriate actions, such as
        /// marking terms as accepted or unaccepted.
        /// </summary>
        private void UpdateTermsAcceptance()
        {
            var privacyManager = PrivacyAndConditionManager.Instance();

            UpdateAcceptance(termsOfUseToggle.IsClicked(),
                privacyManager.AcceptConditionsOfUsage,
                privacyManager.UnaccepedConditionsOfUsage);

            UpdateAcceptance(dataPrivacyToggle.IsClicked(),
                privacyManager.AcceptTermsOfPrivacy,
                privacyManager.UnaccepedTermsOfPrivacy);

            UpdateAcceptance(collectDataToggle.IsClicked(),
                privacyManager.AcceptDataCollection,
                privacyManager.UnaccepedDataCollection);
        }

        /// <summary>
        /// Updates the acceptance state based on the provided condition and triggers
        /// the corresponding action for acceptance or denial.
        /// </summary>
        /// <param name="isAccepted">A boolean value indicating whether the condition is accepted.</param>
        /// <param name="acceptAction">The action to execute when the condition is accepted.</param>
        /// <param name="deniedAction">The action to execute when the condition is denied.</param>
        private void UpdateAcceptance(bool isAccepted, System.Action acceptAction, System.Action deniedAction)
        {
            if (isAccepted)
            {
                acceptAction();
            }
            else
            {
                deniedAction();
            }
        }

        /// <summary>
        /// Validates whether the user has accepted the necessary terms of use and privacy conditions.
        /// If both conditions are accepted, initiates loading of novel data.
        /// Otherwise, displays a message indicating that the terms and conditions need to be accepted
        /// before proceeding further.
        /// </summary>
        private void ValidateTermsAndLoadScene()
        {
            bool acceptedTermsOfUse = termsOfUseToggle.IsClicked();
            bool acceptedDataPrivacyTerms = dataPrivacyToggle.IsClicked();

            if (acceptedTermsOfUse && acceptedDataPrivacyTerms)
            {
                StartCoroutine(WaitForNovelsToLoad());
            }
            else
            {
                infoTextTermsAndConditions.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Handles the success response received from the server specific to the main menu context.
        /// Validates the server version against the compatible version and displays an informational message
        /// if an update is available.
        /// </summary>
        /// <param name="response">The server response object containing relevant data for processing.</param>
        public void OnSuccess(Response response)
        {
            if (response == null) return;

            if (response.GetVersion() != CompatibleServerVersionNumber)
            {
                DisplayInfoMessage(InfoMessages.UPDATE_AVAILABLE);
            }
        }

        /// <summary>
        /// Waits for the list of visual novels to be completely loaded from the kite novel manager.
        /// This coroutine periodically checks if the novel data has been loaded and performs actions based
        /// on the application state. It will either start the introductory novel or load the designated
        /// scene depending on the user's preferences.
        /// </summary>
        /// <returns>A coroutine that keeps yielding until the novel data is available.</returns>
        private IEnumerator WaitForNovelsToLoad()
        {
            // Initialize an empty list for the visual novels
            List<VisualNovel> allNovels = null;

            // Wait for the novel data to be loaded
            while (allNovels == null || allNovels.Count == 0)
            {
                // Try to get the list of all available novels
                allNovels = KiteNovelManager.Instance().GetAllKiteNovels();
                yield return new WaitForSeconds(0.5f); // Wait half a second and check again
            }

            if (!GameManager.Instance.SkipIntroNovel)
            {
                // Call the method to start the introductory novel and pass the loaded novel list
                StartIntroNovel(allNovels);
            }
            else
            {
                SceneLoader.LoadFoundersBubbleScene();
            }
        }

        /// <summary>
        /// Identifies and starts the introductory novel from the provided list of loaded novels.
        /// If a novel with the title "Einstiegsdialog" is found, it is configured for play,
        /// applying its unique settings such as ID, color, and display name, and loads the corresponding play novel scene.
        /// </summary>
        /// <param name="allNovels">The list of all loaded visual novels to search for the introductory novel.</param>
        private static void StartIntroNovel(List<VisualNovel> allNovels)
        {
            // Iterate through the list of all loaded novels
            foreach (var novel in allNovels)
            {
                // Check if the current novel has the title "Einstiegsdialog" (introductory dialogue)
                if (novel.title == "Einstiegsdialog")
                {
                    GameManager.Instance.IsIntroNovelLoadedFromMainMenu = true;

                    // Convert the novel ID to the corresponding enum
                    VisualNovelNames novelNames = VisualNovelNamesHelper.ValueOf((int)novel.id);

                    PlayManager.Instance().SetVisualNovelToPlay(novel); // Set the novel to be played in the PlayManager          
                    PlayManager.Instance().SetColorOfVisualNovelToPlay(novel.novelColor); // Set the color for the novel
                    PlayManager.Instance().SetDisplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(novelNames)); // Set the display name for the novel
                    PlayManager.Instance().SetDesignationOfNovelToPlay(novel.designation);
                    
                    // Load the PlayNovelScene
                    SceneLoader.LoadPlayNovelScene();
                }
            }
        }
    }
}
