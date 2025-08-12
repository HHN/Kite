using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    /// <summary>
    /// Handles the behavior and logic specifically for the instructional scenes within the game.
    /// </summary>
    /// <remarks>
    /// This controller inherits from the <see cref="SceneController"/> class and is designed to manage
    /// user interactions, messages, and other functionality specific to instructional scenes.
    /// </remarks>
    public class PlayInstructionSceneController : SceneController
    {
        [SerializeField] private Image novelImage;
        [SerializeField] private Image textBoxImage;
        [SerializeField] private TextMeshProUGUI novelName;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private Color backgroundColor;

        [SerializeField] private Button playButton;
        [SerializeField] private Button playButton2;
        [SerializeField] private Button backButton;
        [SerializeField] private Toggle toggle;
        [SerializeField] private Toggle toggle2;

        [SerializeField] private Image playButtonImage1;
        [SerializeField] private Image playButtonImage2;
        [SerializeField] private Image checkBoxImage1;
        [SerializeField] private Image checkBoxImage2;
        [SerializeField] private Image headerImage;

        private bool _isSyncing;

        /// <summary>
        /// Called when the PlayInstructionScene is initialized.
        /// This method sets up various properties and behaviors specific to the instructional scene.
        /// </summary>
        /// <remarks>
        /// This includes updating UI elements, setting colors and states, and configuring button and toggle listeners.
        /// It integrates with the <see cref="BackStackManager"/> to track scene history
        /// and with <see cref="PlayManager"/> to retrieve relevant information about the visual novel.
        /// </remarks>
        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.PlayInstructionScene);
            
            backgroundColor = PlayManager.Instance().GetColorOfVisualNovelToPlay();
            novelName.text = PlayManager.Instance().GetDesignationOfNovelToPlay();
            novelImage.color = backgroundColor;
            toggle.isOn = false;
            toggle2.isOn = false;

            playButton.onClick.AddListener(OnPlayButton);
            playButton2.onClick.AddListener(OnPlayButton);

            toggle.onValueChanged.AddListener((value) => SyncToggles(toggle2, value));
            toggle2.onValueChanged.AddListener((value) => SyncToggles(toggle, value));

            FontSizeManager.Instance().UpdateAllTextComponents();

            SetColours();
        }

        /// <summary>
        /// Synchronizes the state of two toggles to ensure they have matching values.
        /// Prevents recursive calls during the synchronization process.
        /// </summary>
        /// <param name="otherToggle">The toggle whose state needs to be updated to match the current toggle.</param>
        /// <param name="isOn">The current state of the toggle triggering the synchronization.</param>
        private void SyncToggles(Toggle otherToggle, bool isOn)
        {
            if (_isSyncing) return; // Prevent recursive calls

            _isSyncing = true; // Start syncing
            otherToggle.isOn = isOn; // Update the other toggle to match the changed one
            _isSyncing = false; // End syncing
        }

        /// <summary>
        /// This method manages toggling behaviors when the "never show again" toggle is set
        /// and transitions the current scene to the play novel scene using the <see cref="SceneLoader.LoadPlayNovelScene"/> method.
        /// </summary>
        private void OnPlayButton()
        {
            if (toggle.isOn)
            {
                ShowPlayInstructionManager.Instance().SetShowInstruction(false);
            }

            SceneLoader.LoadPlayNovelScene();
        }

        /// <summary>
        /// This method retrieves a color instance from <see cref="NovelColorManager"/> and applies it
        /// to the designated elements, including button images, checkbox images, and the header image.
        /// Ensures consistent theming based on the active visual novel.
        /// </summary>
        private void SetColours()
        {
            playButtonImage1.color = NovelColorManager.Instance().GetColor();
            playButtonImage2.color = NovelColorManager.Instance().GetColor();
            checkBoxImage1.color = NovelColorManager.Instance().GetColor();
            checkBoxImage2.color = NovelColorManager.Instance().GetColor();
            headerImage.color = NovelColorManager.Instance().GetColor();
        }
    }
}