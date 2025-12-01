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

        private const int CompatibleServerVersionNumber = 10;

        // Guard: stellt sicher, dass der Weiter-Flow nur einmal gestartet wird
        private bool _startedNextFlow;

        // Wenn true, überspringt Start() das visuelle Setup (weil wir in Awake() schon weiterleiten)
        private bool _skipVisualSetup;

        // Läuft vor dem ersten Render-Frame: ideal, um bei bereits akzeptierten Legalinfos
        // direkt weiterzuleiten, ohne die Szene je sichtbar zu machen.
        private void Awake()
        {
            InitializeScene();
            ApplyVolumeFromPrefs();

            var privacyManager = PrivacyAndConditionManager.Instance();
            bool alreadyAccepted = privacyManager.IsConditionsAccepted() && privacyManager.IsPrivacyTermsAccepted();
            
            GeneratedFeedbackManager.Instance.LoadFeedbacks();

            if (alreadyAccepted)
            {
                // Szene unsichtbar halten und direkt weiterleiten
                HideSceneVisuals();

                _skipVisualSetup = true;

                // Optional: wenn ScreenFade bereits existiert, kann man auch so weiterleiten:
                // if (ScreenFade.Instance) ScreenFade.Instance.FadeToBlackAndLoad(StartNextFlow);
                // else StartNextFlow();
                StartNextFlow();
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

            // Bestehende Initialisierungen beibehalten
            TextToSpeechManager ttsManager = TextToSpeechManager.Instance;
            MappingManager mappingManager = MappingManager.Instance;

            InitializeScene();
            SetupButtonListeners();
            HandleTermsAndConditions();

            if (versionInfo != null)
                versionInfo.text = Application.version;

            // Falls exakt in diesem Moment schon akzeptiert wurde, direkt weiterleiten
            var privacyManager = PrivacyAndConditionManager.Instance();
            if (privacyManager.IsConditionsAccepted() && privacyManager.IsPrivacyTermsAccepted())
            {
                // Panel sichtbar lassen; sofort Szenenwechsel starten (mit Fade, wenn vorhanden)
                if (ScreenFade.Instance)
                    ScreenFade.Instance.FadeToBlackAndLoad(StartNextFlow);
                else
                    StartNextFlow();
            }
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
        /// Setzt die globale Lautstärke gemäß gespeicherten PlayerPrefs.
        /// </summary>
        private void ApplyVolumeFromPrefs()
        {
            if (PlayerPrefs.GetInt("IsSoundEffectVolumeOn", 1) == 1)
                GlobalVolumeManager.Instance.SetGlobalVolume(PlayerPrefs.GetFloat("SavedSoundEffectVolume", 1));
            else
                GlobalVolumeManager.Instance.SetGlobalVolume(0);
        }

        /// <summary>
        /// Button-Events verdrahten.
        /// </summary>
        private void SetupButtonListeners()
        {
            continueTermsAndConditionsButton.onClick.AddListener(OnContinueTermsAndConditionsButton);
        }

        /// <summary>
        /// Panel je nach Akzeptanzstatus ein-/ausblenden (kein Zwangs-Hide bei akzeptiert,
        /// weil wir beim Übergang das Panel sichtbar lassen).
        /// </summary>
        private void HandleTermsAndConditions()
        {
            var privacyManager = PrivacyAndConditionManager.Instance();

            if (privacyManager.IsConditionsAccepted() && privacyManager.IsPrivacyTermsAccepted())
            {
                // Panel kann sichtbar bleiben; der Wechsel wird in Start() angestoßen.
                // Kein SetActive(false) nötig.
            }
            else
            {
                if (termsAndConditionPanel) termsAndConditionPanel.SetActive(true);
            }
        }

        /// <summary>
        /// Klick auf „Weiter“ im Legal-Panel.
        /// </summary>
        private void OnContinueTermsAndConditionsButton()
        {
            UpdateTermsAcceptance();
            ValidateTermsAndLoadScene();
        }

        /// <summary>
        /// Schreibt den Akzeptanzstatus über den PrivacyAndConditionManager fort.
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
        /// Direkt nach dem Akzeptieren: Panel bleibt sichtbar, Interaktion wird gesperrt,
        /// und der Szenenwechsel startet SOFORT (mit Fade, wenn vorhanden).
        /// </summary>
        private void ValidateTermsAndLoadScene()
        {
            bool acceptedTermsOfUse = termsOfUseToggle.IsClicked();
            bool acceptedDataPrivacyTerms = dataPrivacyToggle.IsClicked();

            if (acceptedTermsOfUse && acceptedDataPrivacyTerms)
            {
                // Panel sichtbar lassen, aber Interaktion verhindern
                LockLegalUi();

                // Szenenwechsel jetzt sofort anstoßen
                if (ScreenFade.Instance)
                    ScreenFade.Instance.FadeToBlackAndLoad(StartNextFlow);
                else
                    StartNextFlow();
            }
        }

        /// <summary>
        /// Version-Check bleibt erhalten.
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
        /// Startet den Übergang zum eigentlichen Spiel/der nächsten Szene (nur einmal).
        /// </summary>
        private void StartNextFlow()
        {
            if (_startedNextFlow) return;
            _startedNextFlow = true;

            StartCoroutine(WaitForNovelsToLoad());
        }

        /// <summary>
        /// Wartet, bis die Novels geladen sind, und wechselt dann gemäß Einstellung weiter.
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
        /// Sucht „Einstiegsdialog“ und lädt die PlayNovelScene mit gesetzten Metadaten.
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
        /// Panel sichtbar lassen, aber Eingaben sperren (kein Doppelklick, kein „Durchklicken“).
        /// </summary>
        private void LockLegalUi()
        {
            if (!termsAndConditionPanel) return;

            var cg = termsAndConditionPanel.GetComponent<CanvasGroup>();
            if (!cg) cg = termsAndConditionPanel.AddComponent<CanvasGroup>();

            cg.interactable = false;
            cg.blocksRaycasts = true; // blockiert Eingaben unter dem Panel

            // zusätzlich den Continue-Button deaktivieren (optisch/semantisch)
            if (continueTermsAndConditionsButton)
                continueTermsAndConditionsButton.interactable = false;
        }

        /// <summary>
        /// Verhindert, dass die Szene einen Frame sichtbar wird (Awake-Pfad bei bereits akzeptierten Legalinfos).
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
