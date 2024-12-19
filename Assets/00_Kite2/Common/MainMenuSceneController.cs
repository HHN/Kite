using System.Collections;
using System.Collections.Generic;
using _00_Kite2.Common.Managers;
using _00_Kite2.Common.Messages;
using _00_Kite2.Common.Novel;
using _00_Kite2.Common.SceneManagement;
using _00_Kite2.Common.UI.UI_Elements.Toggle;
using _00_Kite2.Player;
using _00_Kite2.Server_Communication;
using _00_Kite2.Server_Communication.Server_Calls;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Common
{
    public class MainMenuSceneController : SceneController, IOnSuccessHandler
    {
        [SerializeField] private Button novelPlayerButton;
        [SerializeField] private GameObject buttonSoundPrefab;
        [SerializeField] private GameObject termsAndConditionPanel;
        [SerializeField] private Button continueTermsAndConditionsButton;
        [SerializeField] private CustomToggle termsOfUseToggle;
        [SerializeField] private CustomToggle dataPrivacyToggle;
        [SerializeField] private CustomToggle collectDataToggle;
        [SerializeField] private TextMeshProUGUI infoTextTermsAndConditions;
        [SerializeField] private AudioSource kiteAudioLogo;
        [SerializeField] private GameObject getVersionServerCallPrefab;
        [SerializeField] private GameObject novelLoader;
        private static int COMPATIBLE_SERVER_VERSION_NUMBER = 10;

        private void Start()
        {
            // Initialisiere den TextToSpeechManager
            TextToSpeechManager ttsManager = TextToSpeechManager.Instance;
            // Initialisiere die Szene und setze grundlegende Einstellungen
            InitializeScene();

            // Richte die Button-Listener ein, um auf Benutzereingaben zu reagieren
            SetupButtonListeners();

            // Behandle die Nutzungsbedingungen und Datenschutzrichtlinien
            HandleTermsAndConditions();

            // Hole die Instanz des PrivacyManagers, um den aktuellen Status der Datenschutzakzeptanz zu überprüfen
            var privacyManager = PrivacyAndConditionManager.Instance();

            // Überprüfe, ob die Nutzungsbedingungen und Datenschutzrichtlinien akzeptiert wurden
            if (privacyManager.IsConditionsAccepted() && privacyManager.IsPrivacyTermsAccepted())
            {
                // Starte eine Coroutine, die darauf wartet, dass die Novels geladen sind
                StartCoroutine(WaitForNovelsToLoad());
            }
        }

        private void InitializeScene()
        {
            DontDestroyOnLoad(novelLoader);
            AnalyticsServiceHandler.Instance().StartAnalytics();
            PlayerDataManager.Instance().LoadAllPlayerPrefs();
            BackStackManager.Instance().Clear();
            SceneMemoryManager.Instance().ClearMemory();
        }

        private void SetupButtonListeners()
        {
            //novelPlayerButton.onClick.AddListener(OnNovelPlayerButton);
            continueTermsAndConditionsButton.onClick.AddListener(OnContinueTermsAndConditionsButton);
        }

        private void HandleTermsAndConditions()
        {
            var privacyManager = PrivacyAndConditionManager.Instance();

            if (privacyManager.IsConditionsAccepted() && privacyManager.IsPrivacyTermsAccepted())
            {
                termsAndConditionPanel.SetActive(false);
                kiteAudioLogo.Play();

                if (privacyManager.IsDataCollectionAccepted())
                {
                    AnalyticsServiceHandler.Instance().CollectData();
                }

                if (!ApplicationModeManager.Instance().IsOfflineModeActive())
                {
                    StartVersionCheck();
                }
            }
        }

        private void StartVersionCheck()
        {
            var call = Instantiate(getVersionServerCallPrefab).GetComponent<GetVersionServerCall>();
            call.sceneController = this;
            call.OnSuccessHandler = this;
            call.SendRequest();
            DontDestroyOnLoad(call.gameObject);
        }

        public void OnNovelPlayerButton()
        {
            var analytics = AnalyticsServiceHandler.Instance();
            analytics.SendMainMenuStatistics();
            analytics.SetFromWhereIsNovelSelected("KITE NOVELS");

            // Instantiate the sound prefab and assign it to a variable
            GameObject buttonSound = Instantiate(buttonSoundPrefab);
            DontDestroyOnLoad(buttonSound); // Correct usage of 
        }

        public void OnSettingsButton()
        {
            SceneLoader.LoadSettingsScene();
        }

        private void OnContinueTermsAndConditionsButton()
        {
            UpdateTermsAcceptance();
            ValidateTermsAndLoadScene();
        }

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

        public void OnSuccess(Response response)
        {
            if (response == null) return;

            if (response.GetVersion() != COMPATIBLE_SERVER_VERSION_NUMBER)
            {
                DisplayInfoMessage(InfoMessages.UPDATE_AVAILABLE);
            }
        }

        // Coroutine that waits for the novels to load
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

        // Method to start the introductory novel
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

                    PlayManager.Instance()
                        .SetVisualNovelToPlay(novel); // Set the novel to be played in the PlayManager          
                    PlayManager.Instance()
                        .SetForegroundColorOfVisualNovelToPlay(
                            FoundersBubbleMetaInformation
                                .GetForegroundColorOfNovel(novelNames)); // Set the foreground color for the novel
                    PlayManager.Instance()
                        .SetBackgroundColorOfVisualNovelToPlay(
                            FoundersBubbleMetaInformation
                                .GetBackgroundColorOfNovel(novelNames)); // Set the background color for the novel
                    PlayManager.Instance()
                        .SetDisplayNameOfNovelToPlay(
                            FoundersBubbleMetaInformation
                                .GetDisplayNameOfNovelToPlay(novelNames)); // Set the display name for the novel

                    // Load the PlayNovelScene
                    SceneLoader.LoadPlayNovelScene();
                }
            }
        }
    }
}