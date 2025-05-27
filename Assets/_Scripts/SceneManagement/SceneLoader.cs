using System.Collections.Generic;
using Assets._Scripts.Controller.SceneControllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Scripts.SceneManagement
{
    public abstract class SceneLoader
    {
        
        private const string NovelBaseScene = "PlayNovelScene";
        
        private const string DontDestroySceneName = "DontDestroyOnLoad";
        
        // Szenen, bei deren Laden wir die PlayNovelScene vorher vollständig schließen
        private static readonly HashSet<string> SingleLoadExceptions = new HashSet<string>
        {
            "FeedBackScene",
            "FoundersBubbleScene"
        };
        
        public static void LoadMainMenuScene()
        {
            LoadScene(SceneNames.MainMenuScene);
        }

        public static void LoadPlayNovelScene()
        {
            // Überprüfen, ob ein Speicherstand für die nächste Szene existiert
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
        
        // Controller stoppen, wie gehabt...
        // GameObject oldController = GameObject.Find("Controller");
        //     if (oldController != null)
        // {
        //     var sc = oldController.GetComponent<SceneController>();
        //     if (sc != null) sc.OnStop();
        // }
        
        public static void LoadScene(string sceneName)
    {
        // ----------------------------------------------------------
        // 1) Sonderfälle: Feedback oder FoundersBubble
        //    -> PlayNovelScene beenden und dann SINGLE-Mode laden
        // ----------------------------------------------------------
        if (SingleLoadExceptions.Contains(sceneName))
        {
            // Wenn noch geladen, unloaden
            if (SceneManager.GetSceneByName(NovelBaseScene).isLoaded)
            {
                SceneManager.UnloadSceneAsync(NovelBaseScene);
            }
            // Danach ganz normal laden (ersetzt alle anderen Szenen)
            SceneManager.LoadScene(sceneName);
            return;
        }

        // ----------------------------------------------------------
        // 2) Prüfen, ob wir uns noch im Novel-Modus befinden
        //    (PlayNovelScene ist persistent im Hintergrund)
        // ----------------------------------------------------------
        bool inNovelMode = SceneManager.GetSceneByName(NovelBaseScene).isLoaded;

        // a) Wenn PlayNovelScene nicht geladen oder wir direkt zurück zu ihr wollen:
        if (!inNovelMode || sceneName == NovelBaseScene)
        {
            SceneManager.LoadScene(sceneName);
            return;
        }

        // ----------------------------------------------------------
        // 3) Novel-Modus & keine Ausnahme:
        //    - unload aller Subs (außer PlayNovelScene & DontDestroy)
        //    - load der neuen Sub-Szene additiv
        // ----------------------------------------------------------
        // 3a) Alte Sub-Szenen ermitteln und entladen
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var s = SceneManager.GetSceneAt(i);
            if (!s.isLoaded) continue;
            if (s.name == NovelBaseScene)       continue;
            if (s.name == DontDestroySceneName) continue;
            SceneManager.UnloadSceneAsync(s);
        }

        // 3b) Neue Sub-Szene additiv laden
        var loadOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        loadOp.completed += _ =>
        {
            var newlyLoaded = SceneManager.GetSceneByName(sceneName);
            if (newlyLoaded.IsValid() && newlyLoaded.isLoaded)
            {
                SceneManager.SetActiveScene(newlyLoaded);
            }
        };
    }
        
            
        }

    }