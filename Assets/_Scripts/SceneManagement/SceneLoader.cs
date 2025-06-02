using System.Collections.Generic;
using Assets._Scripts.Controller.SceneControllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Scripts.SceneManagement
{
    public abstract class SceneLoader
    {
        private const string NovelBaseScene       = "PlayNovelScene";
        private const string DontDestroySceneName = "DontDestroyOnLoad";

        // Szenen, die KEIN additiver Wechsel sein sollen
        private static readonly HashSet<string> SingleLoadExceptions = new HashSet<string>
        {
            "FeedBackScene",
            "FoundersBubbleScene"
        };

        // Merkt sich den Namen der aktuell geladenen Sub-Szene (oder null, wenn keine).
        // Wird in LoadScene() aktualisiert, muss aber auch zurückgesetzt werden, wenn Ihr 
        // die Sub-Szene außerhalb von LoadScene entladet (z.B. per Back-Button).
        private static string _currentSubScene = null;

        public static void LoadMainMenuScene()
        {
            LoadScene(SceneNames.MainMenuScene);
        }

        public static void LoadPlayNovelScene()
        {
            // Überprüfen, ob ein Speicherstand existiert…
            GameManager.Instance.CheckAndSetAllNovelsStatus();
            LoadScene(SceneNames.PlayNovelScene);
        }

        public static void LoadFeedbackScene()
        {
            LoadScene(SceneNames.FeedbackScene);
        }

        public static void LoadSettingsScene()
        {
            LoadScene(SceneNames.SettingsScene);
        }

        public static void LoadFoundersBubbleScene()
        {
            LoadScene(SceneNames.FoundersBubbleScene);
        }

        public static void LoadPlayInstructionScene()
        {
            LoadScene(SceneNames.PlayInstructionScene);
        }

        public static void LoadNovelHistoryScene()
        {
            LoadScene(SceneNames.NovelHistoryScene);
        }

        public static void LoadResourcesScene()
        {
            LoadScene(SceneNames.ResourcesScene);
        }

        public static void LoadAccessibilityScene()
        {
            LoadScene(SceneNames.AccessibilityScene);
        }

        public static void LoadLegalInformationScene()
        {
            LoadScene(SceneNames.LegalInformationScene);
        }

        public static void LoadPrivacyPolicyScene()
        {
            LoadScene(SceneNames.PrivacyPolicyScene);
        }

        public static void LoadLegalNoticeScene()
        {
            LoadScene(SceneNames.LegalNoticeScene);
        }

        public static void LoadTermsOfUseScene()
        {
            LoadScene(SceneNames.TermsOfUseScene);
        }

        public static void LoadBookmarkedNovelsScene()
        {
            LoadScene(SceneNames.BookmarkedNovelsScene);
        }

        public static void LoadSoundSettingsScene()
        {
            LoadScene(SceneNames.SoundSettingsScene);
        }

        public static void LoadKnowledgeScene()
        {
            LoadScene(SceneNames.KnowledgeScene);
        }

        // =========================================================================
        // Die zentrale Ladelogik:
        // =========================================================================
        public static void LoadScene(string sceneName)
        {
            // ────────────────────────────────────────────────────────────────────────────
            // a) Wurde _currentSubScene extern bereits entladen? Dann zurücksetzen:
            // ────────────────────────────────────────────────────────────────────────────
            if (_currentSubScene != null && !SceneManager.GetSceneByName(_currentSubScene).isLoaded)
            {
                _currentSubScene = null;
            }

            // ----------------------------------------------------------
            // 1) Sonderfälle: Feedback oder FoundersBubble → Single-Mode
            //    (PlayNovelScene wird entladen, keine additive Logik)
            // ----------------------------------------------------------
            if (SingleLoadExceptions.Contains(sceneName))
            {
                // Wenn Base-Szene noch geladen ist, entladen
                if (SceneManager.GetSceneByName(NovelBaseScene).isLoaded)
                {
                    SceneManager.UnloadSceneAsync(NovelBaseScene);
                }
                // Normales Single-Laden
                SceneManager.LoadScene(sceneName);
                // Nach Single-Load müssen wir unbedingt alle Flags zurücksetzen:
                ClearPreservedControllers();
                _currentSubScene = null;
                return;
            }

            // ----------------------------------------------------------
            // 2) Prüfen, ob wir uns noch im Novel-Mode befinden
            //    (PlayNovelScene ist persistent im Hintergrund)
            // ----------------------------------------------------------
            bool inNovelMode = SceneManager.GetSceneByName(NovelBaseScene).isLoaded;

            // a) Wenn PlayNovelScene nicht geladen oder explizit zurück zu ihr:
            if (!inNovelMode || sceneName == NovelBaseScene)
            {
                // Alle Persistent-Controller freigeben
                ClearPreservedControllers();
                // Kein Sub-Scene-State mehr
                _currentSubScene = null;

                // Single-Mode-Laden
                SceneManager.LoadScene(sceneName);
                return;
            }

            // ----------------------------------------------------------
            // 3) Novel-Mode & keine Ausnahme:
            //    Hier tauschen wir die jeweils aktive Sub-Szene aus.
            // ----------------------------------------------------------

            // 3a) Beim ersten Additive-Load speichern wir die Base-Controller
            if (!controllersPreserved)
            {
                PreserveBaseControllers();
                controllersPreserved = true;
            }

            // 3b) Wenn noch keine Sub-Szene geladen ist (erstes Mal):
            if (_currentSubScene == null)
            {
                var firstOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                firstOp.completed += _ =>
                {
                    Scene newlyLoaded = SceneManager.GetSceneByName(sceneName);
                    if (newlyLoaded.IsValid() && newlyLoaded.isLoaded)
                    {
                        // 1. Neue additiv geladene Szene aktivieren
                        SceneManager.SetActiveScene(newlyLoaded);
                        // 2. Den Namen merken, damit wir sie später entladen/kontrollieren können
                        _currentSubScene = sceneName;
                    }
                };
                return;
            }

            // 3c) Wenn bereits eine Sub-Szene offen ist, tauschen wir sie aus
            //     → Lade zuerst die neue, setze sie als aktiv, und erst dann entlade ich die alte.
            {
                var op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                op.completed += _ =>
                {
                    // 1. Neue Szene gefunden und aktiv setzen
                    Scene newScene = SceneManager.GetSceneByName(sceneName);
                    if (newScene.IsValid() && newScene.isLoaded)
                    {
                        SceneManager.SetActiveScene(newScene);
                        // 2. Jetzt erst die alte Sub-Szene entladen (falls sie noch existiert)
                        if (!string.IsNullOrEmpty(_currentSubScene))
                        {
                            Scene oldScene = SceneManager.GetSceneByName(_currentSubScene);
                            if (oldScene.IsValid() && oldScene.isLoaded)
                            {
                                SceneManager.UnloadSceneAsync(oldScene);
                            }
                        }
                        // 3. Und nun den neuen Namen speichern
                        _currentSubScene = sceneName;
                    }
                };
                return;
            }
        }

        // =========================================================================
        // (Optional) Base-Controller persistieren
        // =========================================================================
        private static readonly List<GameObject> PreservedControllers  = new List<GameObject>();
        internal static bool controllersPreserved = false;

        private static void PreserveBaseControllers()
        {
            Scene baseScene = SceneManager.GetSceneByName(NovelBaseScene);
            if (!baseScene.isLoaded) return;

            foreach (var root in baseScene.GetRootGameObjects())
            {
                var controllers = root.GetComponentsInChildren<SceneController>(true);
                foreach (var sc in controllers)
                {
                    var go = sc.gameObject;
                    if (!PreservedControllers.Contains(go))
                    {
                        Object.DontDestroyOnLoad(go);
                        PreservedControllers.Add(go);
                    }
                }
            }
        }

        private static void ClearPreservedControllers()
        {
            foreach (var go in PreservedControllers)
            {
                if (go != null)
                    Object.Destroy(go);
            }
            PreservedControllers.Clear();
            controllersPreserved = false;
        }
    }
}
