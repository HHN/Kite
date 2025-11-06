using System.Runtime.InteropServices;
using System.Collections;
using System.Text.RegularExpressions;
using Assets._Scripts.Managers;
using Assets._Scripts.Player;
using Assets._Scripts.UIElements.DropDown;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Novel
{
    /// <summary>
    /// Represents a single entry in the novel's history GUI, displaying dialog, AI feedback, and providing copy functionality.
    /// </summary>
    public class NovelHistoryEntryGuiElement : MonoBehaviour
    {
        [SerializeField] private DropDownMenu dropdownContainer;
        [SerializeField] private DropDownMenu dropDownDialog;
        [SerializeField] private DropDownMenu dropDownAiFeedback;
        [SerializeField] private TextMeshProUGUI headButtonText;
        [SerializeField] private TextMeshProUGUI dialogText;
        [SerializeField] private TextMeshProUGUI aiFeedbackText;
        [SerializeField] private DialogHistoryEntry dialogHistoryEntry;
        [SerializeField] private Image image;
        [SerializeField] private Image image01;
        [SerializeField] private Image image02;
        [SerializeField] private Image image03;
        [SerializeField] private Image image04;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private TextMeshProUGUI buttonText02;
        [SerializeField] private Button copyDialogButton;
        [SerializeField] private Button copyFeedbackButton;
        private GameObject _copyNotificationContainer;

#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void CopyTextToClipboard(string text);
#endif

        /// <summary>
        /// Initializes the component by adding listeners to copy buttons and updating all text components.
        /// </summary>
        private void Start()
        {
            copyDialogButton.onClick.AddListener(CopyDialog);
            copyFeedbackButton.onClick.AddListener(CopyFeedback);
            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        /// <summary>
        /// Initializes the GUI entry with data from a <see cref="DialogHistoryEntry"/>.
        /// Sets the head button text to the entry's date and time, the dialog text with character designations replaced,
        /// and the AI feedback text after cleaning up formatting characters.
        /// </summary>
        /// <param name="dialogHistoryEntry">The <see cref="DialogHistoryEntry"/> containing the data for this GUI element.</param>
        public void InitializeEntry(DialogHistoryEntry dialogHistoryEntry)
        {
            this.dialogHistoryEntry = dialogHistoryEntry;
            headButtonText.text = dialogHistoryEntry.GetDateAndTime();
            dialogText.text = dialogHistoryEntry.GetDialogWithReplacedCharacterDesignation();
            aiFeedbackText.text = dialogHistoryEntry.GetCompletion().Replace("#", "").Replace("*", "").Trim();
        }

        /// <summary>
        /// Adds a <see cref="RectTransform"/> to the dropdown containers for layout updates.
        /// This ensures the layout is recalculated when the content of the dropdowns changes.
        /// </summary>
        /// <param name="rectTransform">The <see cref="RectTransform"/> to add to the layout update list.</param>
        public void AddLayoutToUpdateOnChange(RectTransform rectTransform)
        {
            dropdownContainer.AddLayoutToUpdateOnChange(rectTransform);
            dropDownDialog.AddLayoutToUpdateOnChange(rectTransform);
            dropDownAiFeedback.AddLayoutToUpdateOnChange(rectTransform);
        }

        /// <summary>
        /// Sets the visual color of various UI elements within this history entry.
        /// Applies the given color to multiple Image and TextMeshProUGUI components.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> to apply to the visual elements.</param>
        public void SetVisualNovelColor(Color color)
        {
            image.color = color;
            image01.color = color;
            image02.color = color;
            image03.color = color;
            image04.color = color;
            buttonText.color = color;
            buttonText02.color = color;
        }

        /// <summary>
        /// Copies the dialog text to the system clipboard.
        /// Removes HTML bold/italic tags and replaces single newlines with double newlines for better formatting.
        /// Triggers a popup notification after copying.
        /// </summary>
        private void CopyDialog()
        {
            LogManager.Info("KOPIEREN");
            string pattern = @"<\/?(b|i)>"; // Regex to match <b>, <i>, </b>, </i> tags
            string copyText = Regex.Replace(dialogText.text, pattern, string.Empty);
            copyText = copyText.Replace("\n", "\n\n"); // Replace single newlines with double for better readability when pasted

#if UNITY_WEBGL && !UNITY_EDITOR
            CopyTextToClipboard(copyText);
#else
            GUIUtility.systemCopyBuffer = copyText;
#endif

            StartCoroutine(ShowCopyPopup("Dialog"));
        }

        /// <summary>
        /// Copies the AI feedback text to the system clipboard.
        /// Removes HTML bold/italic tags and replaces single newlines with double newlines for better formatting.
        /// Triggers a popup notification after copying.
        /// </summary>
        private void CopyFeedback()
        {
            LogManager.Info("KOPIEREN");
            string pattern = @"<\/?(b|i)>"; // Regex to match <b>, <i>, </b>, </i> tags
            string copyText = Regex.Replace(aiFeedbackText.text, pattern, string.Empty);
            copyText = copyText.Replace("\n", "\n\n"); // Replace single newlines with double for better readability when pasted

#if UNITY_WEBGL && !UNITY_EDITOR
            CopyTextToClipboard(copyText);
#else
            GUIUtility.systemCopyBuffer = copyText;
#endif

            StartCoroutine(ShowCopyPopup("Feedback"));
        }

        /// <summary>
        /// Displays a temporary popup notification indicating what was copied to the clipboard.
        /// The popup shows for a specified duration (e.g., 2 seconds) and then hides itself.
        /// </summary>
        /// <param name="whatWasCopied">A string indicating whether "Dialog" or "Feedback" was copied, used to set the popup text.</param>
        /// <returns>An IEnumerator for the coroutine.</returns>
        private IEnumerator ShowCopyPopup(string whatWasCopied)
        {
            if (GameObjectManager.Instance().GetCopyNotification() == null)
            {
                LogManager.Info("Kein GameObject mit dem Tag 'CopyNotification' gefunden.");
                yield break; // Exit the coroutine if the object is not found.
            }

            // Access the TextMeshPro component in the popup
            TextMeshProUGUI textComponent = GameObjectManager.Instance().GetCopyNotification()
                .GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                if (whatWasCopied == "Feedback")
                {
                    // Setze den Text
                    textComponent.text = "Das Feedback wurde in die\r\nZwischenablage kopiert.";
                }
                else if (whatWasCopied == "Dialog")
                {
                    // Setze den Text
                    textComponent.text = "Der Dialog wurde in die\r\nZwischenablage kopiert.";

                }
            }

            // Activate the popup
            GameObjectManager.Instance().GetCopyNotification().SetActive(true);

            // Wait for the specified time (e.g., 2 seconds)
            yield return new WaitForSeconds(2);

            // Hide the popup
            GameObjectManager.Instance().GetCopyNotification().SetActive(false);
        }
    }
}