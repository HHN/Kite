using Assets._Scripts.Controller.SceneControllers;
using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.UIElements.Messages;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.SceneBase
{
    /// <summary>
    /// Manages the common header elements found in many scenes, such as the back button,
    /// legal information button, and settings button. It handles navigation logic
    /// and specific behaviors when a novel is being played.
    /// </summary>
    public class SceneHeader : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button legalInformationButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private GameObject warningMessageBox;
        [SerializeField] private LeaveNovelAndGoBackMessageBox warningMessageBoxObject;
        [SerializeField] private Canvas canvas;

        private PlayNovelSceneController _playNovelSceneController;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Finds the PlayNovelSceneController if present, manages the back button's visibility
        /// and interactivity based on whether an intro novel was just loaded, and sets up button listeners.
        /// </summary>
        private void Start()
        {
            GameObject persistentController = GameObject.Find("PlayNovelSceneController");
            if (persistentController != null)
            {
                _playNovelSceneController = persistentController.GetComponent<PlayNovelSceneController>();
            }
            
            backButton.gameObject.SetActive(true);

            if (GameManager.Instance.IsIntroNovelLoadedFromMainMenu)
            {
                backButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1, 0);
                backButton.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                backButton.interactable = false;

                // GameManager.Instance.IsIntroNovelLoadedFromMainMenu = false;
            }
            else
            {
                backButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                backButton.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1); 
                backButton.interactable = true;
            }

            backButton.onClick.AddListener(OnBackButton);
            legalInformationButton.onClick.AddListener(OnLegalInformationButton);
            settingsButton.onClick.AddListener(OnSettingsButton);
        }

        /// <summary>
        /// Handles the click event for the back button.
        /// Contains complex navigation logic based on the current scene and the back stack.
        /// It also handles pausing the novel if currently in a novel scene.
        /// </summary>
        private void OnBackButton()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            
            if (sceneName.Contains("PlayNovelScene") && _playNovelSceneController != null)
            {
                _playNovelSceneController.isPaused = true; // Pause the novel progression
            }

            if (!sceneName.Contains("PlayNovelScene"))
            {
                if (sceneName.Contains("KnowledgeScene"))
                {
                    KnowledgeSceneController knowledgeSceneController = FindAnyObjectByType<KnowledgeSceneController>();
                    knowledgeSceneController.NavigateScene();
                }
                else if (sceneName.Contains("FeedbackScene"))
                {
                    SceneLoader.LoadFoundersBubbleScene(); 
                    return;
                }
                else
                {
                    string lastScene = BackStackManager.Instance().Pop();
                    
                    // As long as the loaded scene is the active scene, load the next scene
                    while (!string.IsNullOrEmpty(lastScene) && lastScene == SceneManager.GetActiveScene().name)
                    {
                        lastScene = BackStackManager.Instance().Pop();
                    }

                    if (string.IsNullOrEmpty(lastScene))
                    {
                        Scene active = SceneManager.GetActiveScene();
                        bool isAdditiveSubScene = active.name != "PlayNovelScene" && active.isLoaded;
                        if (isAdditiveSubScene)
                        {
                            // If it's a sub-scene, unload it.
                            SceneManager.UnloadSceneAsync(active);

                            // Then activate PlayNovelScene if it's loaded.
                            Scene baseScene = SceneManager.GetSceneByName("PlayNovelScene");
                            if (baseScene.IsValid() && baseScene.isLoaded)
                            {
                                SceneManager.SetActiveScene(baseScene);
                            }
                            return;
                        }

                        SceneLoader.LoadFoundersBubbleScene(); 
                        return;
                    }
                    if (lastScene == "PlayNovelScene")
                    {
                        Debug.Log($"lastScene == PlayNovelScene: {_playNovelSceneController != null}");
                        if (_playNovelSceneController)
                        {
                            _playNovelSceneController.isPaused = false;
                            _playNovelSceneController.Continue();
                        }
                        
                        // If the last scene was PlayNovelScene, just make it active, don't reload.
                        Scene active = SceneManager.GetActiveScene();
                        if (active.name != "PlayNovelScene")
                        {
                            SceneManager.UnloadSceneAsync(active);
                        }

                        // Set PlayNovelScene as active.
                        var playNovel = SceneManager.GetSceneByName("PlayNovelScene");
                        if (playNovel.IsValid() && playNovel.isLoaded)
                        {
                            SceneManager.SetActiveScene(playNovel);
                        }
                        return;
                    }

#if UNITY_IOS
                    TextToSpeechManager.Instance.CancelSpeak();
#endif

                    SceneLoader.LoadScene(lastScene);
                }
            }

            HandleMessageBox(sceneName);
        }

        /// <summary>
        /// Instantiates and activates the warning message box for leaving a novel.
        /// Applies specific behavior if the novel is novel ID 13.
        /// </summary>
        /// <param name="sceneName">The name of the current active scene.</param>
        private void HandleMessageBox(string sceneName)
        {
            if (!warningMessageBox) return;
            
            if (!warningMessageBoxObject.IsNullOrDestroyed())
            {
                warningMessageBoxObject.CloseMessageBox();
            }

            if (canvas.IsNullOrDestroyed())
            {
                return;
            }

            warningMessageBoxObject = null;
            warningMessageBoxObject = Instantiate(warningMessageBox, canvas.transform).GetComponent<LeaveNovelAndGoBackMessageBox>();

            if (sceneName.Contains("PlayNovelScene") && _playNovelSceneController.NovelToPlay.id == 13)
            {
                warningMessageBoxObject.HandleButtons();
            }

            warningMessageBoxObject.Activate();
        }

        /// <summary>
        /// Handles the click event for the legal information button.
        /// Pauses the novel if active and loads the legal information scene.
        /// </summary>
        private void OnLegalInformationButton()
        {
            if (_playNovelSceneController != null)
            {
                _playNovelSceneController.isPaused = true;
            }
            
            SceneLoader.LoadLegalInformationScene();
        }

        /// <summary>
        /// Handles the click event for the settings button.
        /// Pauses the novel if active and loads the settings scene.
        /// </summary>
        private void OnSettingsButton()
        {
            if (_playNovelSceneController != null)
            {
                _playNovelSceneController.isPaused = true;
            }
            
            SceneLoader.LoadSettingsScene();
        }
    }
}