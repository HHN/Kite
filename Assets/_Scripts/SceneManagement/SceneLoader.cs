using System.Collections.Generic;
using Assets._Scripts.Controller.SceneControllers;
using Assets._Scripts.Managers;
using Assets._Scripts.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Scripts.SceneManagement
{
    /// <summary>
    /// Provides static utility methods for loading and managing scenes within the game.
    /// It implements a scene loading strategy that distinguishes between full scene changes
    /// and additive loading for sub-scenes within a persistent base scene (like the novel gameplay scene).
    /// </summary>
    public abstract class SceneLoader
    {
        private const string NovelBaseScene = "PlayNovelScene";

        /// <summary>
        /// Scenes that should NOT be additive transitions
        /// </summary>
        private static readonly HashSet<string> SingleLoadExceptions = new()
        {
            SceneNames.FeedbackScene,
            SceneNames.FoundersBubbleScene,
        };

        /// <summary>
        /// Stores the name of the currently loaded additive sub-scene.
        /// It is updated in <see cref="LoadScene"/> and must be reset manually if
        /// a sub-scene is unloaded outside this class (e.g., via a back button).
        /// Null if no sub-scene is currently loaded.
        /// </summary>
        private static string _currentSubScene;

        /// <summary>
        /// Loads the Main Menu scene. It also pushes the scene name onto the BackStackManager.
        /// </summary>
        public static void LoadMainMenuScene()
        {
            BackStackManager.Instance.Push(SceneNames.MainMenuScene);
            LoadScene(SceneNames.MainMenuScene);
        }

        /// <summary>
        /// Loads the main Play Novel scene. This typically clears the back stack,
        /// updates novel statuses, and sets up the base scene for novel gameplay.
        /// </summary>
        public static void LoadPlayNovelScene()
        {
            BackStackManager.Instance.Clear();
            GameManager.Instance.CheckAndSetAllNovelsStatus();

            BackStackManager.Instance.Push(SceneNames.PlayNovelScene);

            LoadScene(SceneNames.PlayNovelScene);
        }

        /// <summary>
        /// Loads the Feedback scene.
        /// </summary>
        public static void LoadFeedbackScene()
        {
            LoadScene(SceneNames.FeedbackScene);
        }

        /// <summary>
        /// Loads the Settings scene.
        /// </summary>
        public static void LoadSettingsScene()
        {
            LoadScene(SceneNames.SettingsScene);
        }

        /// <summary>
        /// Loads the Founders Bubble scene (likely a specific UI or gameplay module).
        /// </summary>
        public static void LoadFoundersBubbleScene()
        {
            LoadScene(SceneNames.FoundersBubbleScene);
        }

        /// <summary>
        /// Loads the Play Instruction scene.
        /// </summary>
        public static void LoadPlayInstructionScene()
        {
            LoadScene(SceneNames.PlayInstructionScene);
        }

        /// <summary>
        /// Loads the Novel History scene.
        /// </summary>
        public static void LoadNovelHistoryScene()
        {
            LoadScene(SceneNames.NovelHistoryScene);
        }

        /// <summary>
        /// Loads the Legal Information scene.
        /// </summary>
        public static void LoadLegalInformationScene()
        {
            LoadScene(SceneNames.LegalInformationScene);
        }

        /// <summary>
        /// Loads the Privacy Policy scene.
        /// </summary>
        public static void LoadPrivacyPolicyScene()
        {
            LoadScene(SceneNames.PrivacyPolicyScene);
        }

        /// <summary>
        /// Loads the Legal Notice scene.
        /// </summary>
        public static void LoadLegalNoticeScene()
        {
            LoadScene(SceneNames.LegalNoticeScene);
        }

        /// <summary>
        /// Loads the Terms of Use scene.
        /// </summary>
        public static void LoadTermsOfUseScene()
        {
            LoadScene(SceneNames.TermsOfUseScene);
        }

        /// <summary>
        /// Loads the Knowledge scene.
        /// </summary>
        public static void LoadKnowledgeScene()
        {
            LoadScene(SceneNames.KnowledgeScene);
        }

        /// <summary>
        /// =========================================================================
        /// The central scene loading logic:
        /// =========================================================================
        /// This method orchestrates the loading of scenes, differentiating between
        /// full scene replacements and additive loading for sub-scenes within the novel base.
        /// </summary>
        /// <param name="sceneName">The name of the scene to be loaded.</param>
        public static void LoadScene(string sceneName)
        {
            Scene activeScene = SceneManager.GetActiveScene();
            if (activeScene.name == sceneName || _currentSubScene == sceneName)
            {
                LogManager.Info($"[SceneLoader] Scene '{sceneName}' ist bereits aktiv. Laden abgebrochen.");
                return;
            }
            
            // ────────────────────────────────────────────────────────────────────────────
            // a) Has _currentSubScene already been unloaded externally? Then reset:
            // ────────────────────────────────────────────────────────────────────────────
            if (_currentSubScene != null && !SceneManager.GetSceneByName(_currentSubScene).isLoaded)
            {
                _currentSubScene = null;
            }

            // ----------------------------------------------------------
            // 1) Special cases: Feedback or FoundersBubble → Single-Mode Load
            //    (PlayNovelScene will be unloaded, no additive logic applies)
            // ----------------------------------------------------------
            if (SingleLoadExceptions.Contains(sceneName))
            {
                // If the base novel scene is loaded, unload it.
                if (SceneManager.GetSceneByName(NovelBaseScene).isLoaded)
                {
                    SceneManager.UnloadSceneAsync(NovelBaseScene);
                }

                // Perform a standard single-mode scene load.
                SceneManager.LoadScene(sceneName);
                // After a single load, it's crucial to clear all preserved controllers and reset sub-scene flags.
                ClearPreservedControllers();
                _currentSubScene = null;
                return;
            }

            // ----------------------------------------------------------
            // 2) Check if we are currently in "Novel Mode"
            //    (PlayNovelScene is persistent in the background)
            // ----------------------------------------------------------
            bool inNovelMode = SceneManager.GetSceneByName(NovelBaseScene).isLoaded;

            // a) If PlayNovelScene is not loaded, or if we are explicitly loading back to it:
            if (!inNovelMode || sceneName == NovelBaseScene)
            {
                // Release all previously preserved controllers.
                ClearPreservedControllers();
                // Reset the sub-scene state as we are leaving the additive novel mode.
                _currentSubScene = null;

                // Load the scene in single mode, replacing everything.
                SceneManager.LoadScene(sceneName);
                return;
            }

            // ----------------------------------------------------------
            // 3) Novel Mode & not an exception:
            //    Here, we swap out the currently active sub-scene.
            // ----------------------------------------------------------

            // 3a) On the first additive load, preserve the base controllers.
            // This ensures core controllers in the PlayNovelScene persist across sub-scene changes.
            if (!_controllersPreserved)
            {
                PreserveBaseControllers();
                _controllersPreserved = true;
            }

            // 3b) If no sub-scene is currently loaded (first time loading an additive sub-scene):
            if (_currentSubScene == null)
            {
                var firstOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                if (firstOp != null)
                {
                    firstOp.completed += _ =>
                    {
                        Scene newlyLoaded = SceneManager.GetSceneByName(sceneName);
                        if (newlyLoaded.IsValid() && newlyLoaded.isLoaded)
                        {
                            // 1. Activate the newly loaded additive scene.
                            SceneManager.SetActiveScene(newlyLoaded);
                            // 2. Remember its name for future unloading/control.
                            _currentSubScene = sceneName;
                        }
                    };
                    return;
                }
            }

            // 3c) If a sub-scene is already open, we swap it out:
            //     → Load the new scene first, set it as active, and only then unload the old one.
            {
                var op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                if (op != null)
                {
                    op.completed += _ =>
                    {
                        // 1. Find the new scene and set it as active.
                        Scene newScene = SceneManager.GetSceneByName(sceneName);
                        if (newScene.IsValid() && newScene.isLoaded)
                        {
                            SceneManager.SetActiveScene(newScene);
                            // 2. Now, and only now, unload the old sub-scene (if it still exists and is loaded).
                            if (!string.IsNullOrEmpty(_currentSubScene))
                            {
                                Scene oldScene = SceneManager.GetSceneByName(_currentSubScene);
                                if (oldScene.IsValid() && oldScene.isLoaded)
                                {
                                    SceneManager.UnloadSceneAsync(oldScene);
                                }
                            }

                            // 3. And finally, store the name of the new sub-scene.
                            _currentSubScene = sceneName;
                        }
                    };
                }
            }
        }

        // =========================================================================
        // (Optional) Preserve Base-Controller Logic
        // =========================================================================
        private static readonly List<GameObject> PreservedControllers = new List<GameObject>();

        private static bool _controllersPreserved;

        /// <summary>
        /// Identifies and preserves essential <see cref="SceneController"/> GameObjects
        /// from the <see cref="NovelBaseScene"/> by marking them with <see cref="Object.DontDestroyOnLoad"/>.
        /// This ensures they remain active when additive scenes are loaded.
        /// </summary>
        private static void PreserveBaseControllers()
        {
            Scene baseScene = SceneManager.GetSceneByName(NovelBaseScene);
            if (!baseScene.isLoaded) return; // Only proceed if the base scene is actually loaded.

            // Iterate through all root GameObjects in the base scene.
            foreach (var root in baseScene.GetRootGameObjects())
            {
                // Find all SceneController components (including inactive ones) in children.
                var controllers = root.GetComponentsInChildren<SceneController>(true);
                foreach (var sc in controllers)
                {
                    var go = sc.gameObject;
                    // If the GameObject is not already in the preserved list, add it and mark for persistence.
                    if (!PreservedControllers.Contains(go))
                    {
                        Object.DontDestroyOnLoad(go);
                        PreservedControllers.Add(go);
                    }
                }
            }
        }

        /// <summary>
        /// Destroys all GameObjects that were previously marked to be preserved
        /// (using <see cref="Object.DontDestroyOnLoad"/>) and clears the internal tracking list.
        /// This is typically called when leaving the "Novel Mode" to clean up persistent objects.
        /// </summary>
        private static void ClearPreservedControllers()
        {
            foreach (var go in PreservedControllers)
            {
                if (go != null) // Check if the GameObject still exists before destroying.
                    Object.Destroy(go);
            }

            PreservedControllers.Clear(); // Clear the list of tracked preserved GameObjects.
            _controllersPreserved = false; // Reset the flag.
        }
    }
}