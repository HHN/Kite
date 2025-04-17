using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
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

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.PlayInstructionScene);

            backgroundColor = PlayManager.Instance().GetBackgroundColorOfVisualNovelToPlay();
            novelName.text = PlayManager.Instance().GetDisplayNameOfNovelToPlay();
            novelImage.color = backgroundColor;
            toggle.isOn = false;
            toggle2.isOn = false;

            playButton.onClick.AddListener(OnPlayButton);
            playButton2.onClick.AddListener(OnPlayButton);

            toggle.onValueChanged.AddListener((value) => SyncToggles(toggle, toggle2, value));
            toggle2.onValueChanged.AddListener((value) => SyncToggles(toggle2, toggle, value));

            FontSizeManager.Instance().UpdateAllTextComponents();

            SetColours();
        }

        private void SyncToggles(Toggle changedToggle, Toggle otherToggle, bool isOn)
        {
            if (_isSyncing) return; // Prevent recursive calls

            _isSyncing = true; // Start syncing
            otherToggle.isOn = isOn; // Update the other toggle to match the changed one
            _isSyncing = false; // End syncing
        }

        private void OnPlayButton()
        {
            if (toggle.isOn)
            {
                NeverShowAgain();
            }

            SceneLoader.LoadPlayNovelScene();
        }

        private void NeverShowAgain()
        {
            ShowPlayInstructionManager.Instance().SetShowInstruction(false);
        }

        public void OnBackButton()
        {
            string lastScene = SceneRouter.GetTargetSceneForBackButton();

            if (string.IsNullOrEmpty(lastScene))
            {
                SceneLoader.LoadMainMenuScene();
                return;
            }

            SceneLoader.LoadScene(lastScene);
        }

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