using Assets._Scripts.Controller.SceneControllers;
using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.UIElements.Messages;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements
{
    /// <summary>
    /// Manages the common header elements found in many scenes, such as the back button,
    /// legal information button, and settings button. It handles navigation logic
    /// and specific behaviors when a novel is being played.
    /// </summary>
    public class SceneHeader : MonoBehaviour
    {
        private const int IntroNovelId = 13;
        private const float VisibleAlpha = 1f;
        private const float InvisibleAlpha = 0f;
        
        [SerializeField] private Button backButton;
        [SerializeField] private Button legalInformationButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private GameObject warningMessageBox;
        [SerializeField] private LeaveNovelAndGoBackMessageBox warningMessageBoxObject;
        [SerializeField] private Canvas canvas;
        
        private PlayNovelSceneController _playNovelSceneController;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Initializes the controller reference and sets up button listeners.
        /// </summary>
        private void Start()
        {
            InitializePlayNovelController();
            SetupBackButton();
            SetupButtonListeners();
        }

        /// <summary>
        /// Initializes the PlayNovelSceneController by locating it within the scene.
        /// If the controller is found, it assigns the reference to enable interaction
        /// and functionality related to playing a novel within the scene.
        /// </summary> 
        private void InitializePlayNovelController()
        {
            GameObject persistentController = GameObject.Find("PlayNovelSceneController");
            if (persistentController != null)
            {
                _playNovelSceneController = persistentController.GetComponent<PlayNovelSceneController>();
            }
        }

        /// <summary>
        /// Configures the initial state of the back button based on game context.
        /// The button is set to be active in the hierarchy and its visibility is toggled
        /// based on whether the intro novel was loaded directly from the main menu.
        /// </summary>
        private void SetupBackButton()
        {
            backButton.gameObject.SetActive(true);
            bool shouldShowBackButton = !GameManager.Instance.IsIntroNovelLoadedFromMainMenu;
            ToggleBackButtonVisibility(shouldShowBackButton);
        }

        /// <summary>
        /// Sets up the event listeners for all interactive buttons in the header.
        /// </summary>
        private void SetupButtonListeners()
        {
            backButton.onClick.AddListener(OnBackButton);
            legalInformationButton.onClick.AddListener(OnLegalInformationButton);
            settingsButton.onClick.AddListener(OnSettingsButton);
        }

        /// <summary>
        /// Handles the click event for the back button.
        /// Orchestrates navigation logic based on the current scene context.
        /// </summary>
        private void OnBackButton()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            
            HandleNovelPause(currentSceneName);

            if (IsPlayNovelScene(currentSceneName))
            {
                ShowLeaveNovelWarning(currentSceneName);
            }
            else
            {
                HandleNonNovelSceneNavigation(currentSceneName);
            }
        }

        /// <summary>
        /// Pauses the novel gameplay when the user navigates away from the play novel scene.
        /// Ensures the novel's state is retained for resumption.
        /// </summary>
        /// <param name="sceneName">The name of the current scene being handled.</param>
        private void HandleNovelPause(string sceneName)
        {
            if (IsPlayNovelScene(sceneName) && _playNovelSceneController != null)
            {
                _playNovelSceneController.isPaused = true;
            }
        }

        /// <summary>
        /// Handles navigation logic for non-novel scenes based on the provided scene name.
        /// Determines the scene type and performs the appropriate navigation actions, such as
        /// invoking specialized controllers or loading specific scenes.
        /// </summary>
        /// <param name="sceneName">The name of the scene to handle navigation for.</param>
        private void HandleNonNovelSceneNavigation(string sceneName)
        {
            if (sceneName.Contains(SceneNames.KnowledgeScene))
            {
                KnowledgeSceneController knowledgeSceneController = FindAnyObjectByType<KnowledgeSceneController>();
                knowledgeSceneController.NavigateScene();
                return;
            }

            if (sceneName.Contains(SceneNames.FeedbackScene))
            {
                SceneLoader.LoadFoundersBubbleScene();
                return;
            }

            HandleBackStackNavigation();
        }

        /// <summary>
        /// Handles navigation logic by retrieving the last scene from the back stack
        /// and loading it. If the back stack is empty, it executes logic to handle
        /// the empty state. Special handling is performed for returning from specific
        /// scenes, such as the Play Novel scene. Text-to-speech functionality is
        /// canceled as needed before navigating to the target scene.
        /// </summary>
        private void HandleBackStackNavigation()
        {
            string lastScene = GetValidLastSceneFromBackStack();

            if (string.IsNullOrEmpty(lastScene))
            {
                HandleEmptyBackStack();
                return;
            }

            if (lastScene == SceneNames.PlayNovelScene)
            {
                HandlePlayNovelSceneReturn();
                return;
            }

            CancelTextToSpeechIfNeeded();
            SceneLoader.LoadScene(lastScene);
        }

        /// <summary>
        /// Retrieves the last valid scene name from the back stack that does not match the current active scene.
        /// This ensures that navigation does not loop back to the current scene.
        /// </summary>
        /// <returns>A string representing the name of the last valid scene in the back stack, or an empty string if no valid scene exists.</returns>
        private string GetValidLastSceneFromBackStack()
        {
            string lastScene = BackStackManager.Instance.Pop();
            string currentSceneName = SceneManager.GetActiveScene().name;

            while (!string.IsNullOrEmpty(lastScene) && lastScene == currentSceneName)
            {
                lastScene = BackStackManager.Instance.Pop();
            }

            return lastScene;
        }

        /// <summary>
        /// Handles the navigation logic when the back stack is empty. Determines the active scene's status
        /// and either manages additive sub-scenes or loads the default Founders Bubble Scene.
        /// </summary>
        private void HandleEmptyBackStack()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            bool isAdditiveSubScene = activeScene.name != SceneNames.PlayNovelScene && activeScene.isLoaded;

            if (isAdditiveSubScene)
            {
                HandleAdditiveSubScene(activeScene);
            }
            else
            {
                SceneLoader.LoadFoundersBubbleScene();
            }
        }

        /// <summary>
        /// Handles the unloading of an additive sub-scene and switches the active scene back
        /// to a specified base scene if it is valid and loaded.
        /// </summary>
        /// <param name="activeScene">The currently active sub-scene that needs to be unloaded.</param>
        private void HandleAdditiveSubScene(Scene activeScene)
        {
            SceneManager.UnloadSceneAsync(activeScene);

            Scene baseScene = SceneManager.GetSceneByName(SceneNames.PlayNovelScene);
            if (baseScene.IsValid() && baseScene.isLoaded)
            {
                SceneManager.SetActiveScene(baseScene);
            }
        }

        /// <summary>
        /// Handles behavior when returning to the Play Novel Scene. Resumes the novel play state if it was paused,
        /// resets the introduction state for the novel if necessary, and ensures the Play Novel Scene is properly activated.
        /// </summary>
        private void HandlePlayNovelSceneReturn()
        {
            if (_playNovelSceneController)
            {
                _playNovelSceneController.isPaused = false;
                _playNovelSceneController.Continue();
            }

            ResetIntroNovelStateIfNeeded();
            ActivatePlayNovelScene();
        }

        /// <summary>
        /// Resets the state of the introductory novel if it is currently being played.
        /// Marks the introductory novel as loaded from the main menu and hides the back button.
        /// </summary>
        private void ResetIntroNovelStateIfNeeded()
        {
            if (_playNovelSceneController?.NovelToPlay.id == IntroNovelId)
            {
                GameManager.Instance.IsIntroNovelLoadedFromMainMenu = true;
                ToggleBackButtonVisibility(false);
            }
        }

        /// <summary>
        /// Activates and sets the "Play Novel" scene as the active scene.
        /// If the active scene is not the "Play Novel" scene, it unloads the current scene.
        /// Ensures that the "Play Novel" scene is valid and loaded before activating it.
        /// </summary>
        private void ActivatePlayNovelScene()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            if (activeScene.name != SceneNames.PlayNovelScene)
            {
                SceneManager.UnloadSceneAsync(activeScene);
            }

            Scene playNovelScene = SceneManager.GetSceneByName(SceneNames.PlayNovelScene);
            if (playNovelScene.IsValid() && playNovelScene.isLoaded)
            {
                SceneManager.SetActiveScene(playNovelScene);
            }
        }
        
        /// <summary>
        /// Cancels any currently active Text-to-Speech operation via the <see cref="TextToSpeechManager"/>.
        /// This is typically done before a scene transition or when navigating away from a dialogue screen 
        /// to stop audio playback and prevent interruptions.
        /// </summary>
        private void CancelTextToSpeechIfNeeded()
        {
            TextToSpeechManager.Instance.CancelSpeak();
        }

        /// <summary>
        /// Instantiates and activates the warning message box for leaving a novel.
        /// Applies specific behavior if the novel is the intro novel.
        /// </summary>
        /// <param name="sceneName">The name of the current active scene.</param>
        private void ShowLeaveNovelWarning(string sceneName)
        {
            if (!warningMessageBox || canvas.IsNullOrDestroyed()) return;

            CloseExistingMessageBox();
            CreateAndConfigureMessageBox(sceneName);
        }

        /// <summary>
        /// Closes the currently existing warning message box, if one exists and has not been destroyed.
        /// This operation ensures the message box reference is cleared, allowing for future reinitialization.
        /// </summary>
        private void CloseExistingMessageBox()
        {
            if (!warningMessageBoxObject.IsNullOrDestroyed())
            {
                warningMessageBoxObject.CloseMessageBox();
            }
            warningMessageBoxObject = null;
        }

        /// <summary>
        /// Creates and configures a message box for handling warnings displayed when attempting
        /// to leave a novel. Configures the message box behavior based on scene and novel type.
        /// </summary>
        /// <param name="sceneName">The name of the scene where the message box will be displayed.</param>
        private void CreateAndConfigureMessageBox(string sceneName)
        {
            warningMessageBoxObject = Instantiate(warningMessageBox, canvas.transform)
                .GetComponent<LeaveNovelAndGoBackMessageBox>();

            if (IsPlayNovelScene(sceneName) && IsIntroNovel())
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
            PauseNovelIfActive();
            ResetIntroNovelFlag();
            SceneLoader.LoadLegalInformationScene();
        }

        /// <summary>
        /// Handles the click event for the settings button.
        /// Pauses the novel if active and loads the settings scene.
        /// </summary>
        private void OnSettingsButton()
        {
            PauseNovelIfActive();
            ResetIntroNovelFlag();
            SceneLoader.LoadSettingsScene();
        }

        /// <summary>
        /// Pauses the currently active novel if the PlayNovelSceneController is available.
        /// Sets the isPaused property of the controller to true to reflect the paused state.
        /// </summary>
        private void PauseNovelIfActive()
        {
            if (_playNovelSceneController != null)
            {
                _playNovelSceneController.isPaused = true;
            }
        }

        /// <summary>
        /// Resets the flag indicating whether the introduction novel was loaded
        /// from the main menu. This ensures proper state management for later
        /// scene transitions and avoids reloading unnecessary content.
        /// </summary>
        private void ResetIntroNovelFlag()
        {
            if (GameManager.Instance.IsIntroNovelLoadedFromMainMenu)
            {
                GameManager.Instance.IsIntroNovelLoadedFromMainMenu = false;
            }
        }

        /// <summary>
        /// Adjusts the visibility and interactivity of the back button.
        /// </summary>
        /// <param name="isVisible">Determines whether the back button should be visible and interactable.</param>
        private void ToggleBackButtonVisibility(bool isVisible)
        {
            float alpha = isVisible ? VisibleAlpha : InvisibleAlpha;
            Color buttonColor = new Color(1, 1, 1, alpha);

            backButton.GetComponentInChildren<TextMeshProUGUI>().color = buttonColor;
            backButton.GetComponentInChildren<Image>().color = buttonColor;
            backButton.interactable = isVisible;
        }

        /// <summary>
        /// Determines if the given scene name corresponds to a play novel scene.
        /// </summary>
        /// <param name="sceneName">The name of the scene to evaluate.</param>
        /// <returns>True if the scene is a play novel scene; otherwise, false.</returns>
        private bool IsPlayNovelScene(string sceneName) => sceneName.Contains(SceneNames.PlayNovelScene);

        /// <summary>
        /// Determines if the currently selected novel is the introductory novel
        /// by comparing its ID with the predefined introductory novel ID.
        /// </summary>
        /// <returns>True if the currently selected novel matches the introductory novel ID; otherwise, false.</returns>
        private bool IsIntroNovel() => _playNovelSceneController?.NovelToPlay.id == IntroNovelId;
    }
}