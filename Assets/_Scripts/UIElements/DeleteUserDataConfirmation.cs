using Assets._Scripts.Managers;
using Assets._Scripts.Messages;
using Assets._Scripts.SaveNovelData;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements
{
    /// <summary>
    /// Provides constant string identifiers for different origins of the delete user data confirmation.
    /// This helps to distinguish why the confirmation dialog was opened.
    /// </summary>
    static class Origins
    {
        public const string Reset = "reset";
    }
    
    /// <summary>
    /// Manages the behavior of a UI confirmation dialog for deleting user data.
    /// This dialog allows users to confirm or cancel actions like resetting application data.
    /// </summary>
    public class DeleteUserDataConfirmation : MonoBehaviour
    {
        [SerializeField] private Button cancelButton;
        [SerializeField] private Button confirmButton;
        [SerializeField] private GameObject background;
        [SerializeField] private GameObject backgroundLeave;
        [SerializeField] private GameObject textCancel;
        [SerializeField] private GameObject person;
        [SerializeField] private GameObject uiContainer;
        [SerializeField] private TextMeshProUGUI messageText;

        private string _origin;

        /// <summary>
        /// Initializes the confirmation dialog with a specific origin.
        /// This method should be called immediately after instantiating the dialog.
        /// </summary>
        /// <param name="origin">A string indicating why the dialog was opened (e.g., <see cref="Origins.Reset"/>).</param>
        public void Initialize(string origin)
        {
            _origin = origin;
            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        /// <summary>
        /// Called when the script instance is being loaded.
        /// This method sets up button listeners and initializes the message text and image sizes.
        /// </summary>
        private void Start()
        {
            cancelButton.onClick.AddListener(OnCancelButton);
            confirmButton.onClick.AddListener(OnConfirmButton);
            
            if (_origin == Origins.Reset)
            {
                messageText.text = InfoMessages.RESET_APP_CONFIRMATION;
            }

            AdjustImageSize();
            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        /// <summary>
        /// Adjusts the size of the 'person' image to ensure it doesn't exceed the width of the background.
        /// This prevents UI elements from overflowing.
        /// </summary>
        private void AdjustImageSize()
        {
            RectTransform backgroundRect = background.GetComponent<RectTransform>();
            RectTransform personRect = person.GetComponent<RectTransform>();
            
            float backgroundWidth = backgroundRect.rect.width;

            if (personRect.rect.width > backgroundWidth)
            {
                personRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, backgroundWidth);
                personRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, backgroundWidth);
            }
        }

        /// <summary>
        /// Handles the action when the confirmation button is clicked.
        /// If the origin is "reset", it clears all saved game data, player preferences,
        /// and displays a confirmation message before closing the dialog.
        /// </summary>
        private void OnConfirmButton()
        {
            if (_origin == Origins.Reset)
            {
                SaveLoadManager.ClearAllSaveData();
                
                StartCoroutine(TextToSpeechManager.Instance.Speak(InfoMessages.RESET_APP));
                PlayerDataManager.Instance().ClearEverything();
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
                FavoritesManager.Instance().ClearFavorites();
                PlayRecordManager.Instance().ClearData();
                ShowPlayInstructionManager.Instance().SetShowInstruction(true);
                
                GameManager.Instance.resetApp = true;
                
                CloseMessageBox();
            }

            DialogHistoryManager.Instance().ClearList();
        }

        /// <summary>
        /// Handles the action when the cancel button is clicked.
        /// It simply closes the message box.
        /// </summary>
        private void OnCancelButton()
        {
            GameManager.Instance.resetApp = false;
            
            CloseMessageBox();
        }

        /// <summary>
        /// Activates (makes visible) the confirmation dialog GameObject.
        /// </summary>
        public void Activate()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Closes and destroys the confirmation dialog message box.
        /// It also triggers a global message display via the GameManager.
        /// </summary>
        public void CloseMessageBox()
        {
            if (this.IsNullOrDestroyed() || gameObject.IsNullOrDestroyed())
            {
                return;
            }
            
            if (GameManager.Instance.resetApp) GameManager.Instance.DisplayMessage(InfoMessages.RESET_APP);
            
            Destroy(gameObject);
        }
    }
}