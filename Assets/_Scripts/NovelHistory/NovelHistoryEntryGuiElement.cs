using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Assets._Scripts.Managers;
using Assets._Scripts.Player;
using Assets._Scripts.UIElements.DropDown;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.NovelHistory
{
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

        private void Start()
        {
            copyDialogButton.onClick.AddListener(CopyDialog);
            copyFeedbackButton.onClick.AddListener(CopyFeedback);
            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        public void InitializeEntry(DialogHistoryEntry dialogHistoryEntry)
        {
            this.dialogHistoryEntry = dialogHistoryEntry;
            headButtonText.text = dialogHistoryEntry.GetDateAndTime();
            dialogText.text = dialogHistoryEntry.GetDialog();
            aiFeedbackText.text = dialogHistoryEntry.GetCompletion().Replace("#", "").Replace("*", "").Trim();
        }

        public void CloseAll()
        {
            dropdownContainer.SetMenuOpen(false);
            dropDownDialog.SetMenuOpen(false);
            dropDownAiFeedback.SetMenuOpen(false);
        }

        public void AddLayoutToUpdateOnChange(RectTransform rectTransform)
        {
            dropdownContainer.AddLayoutToUpdateOnChange(rectTransform);
            dropDownDialog.AddLayoutToUpdateOnChange(rectTransform);
            dropDownAiFeedback.AddLayoutToUpdateOnChange(rectTransform);
        }

        public void RebuildLayout()
        {
            dropDownDialog.RebuildLayout();
            dropDownAiFeedback.RebuildLayout();
            dropdownContainer.RebuildLayout();
        }

        public List<DropDownMenu> GetDropDownMenus()
        {
            return new List<DropDownMenu>()
            {
                dropdownContainer,
                dropDownDialog,
                dropDownAiFeedback
            };
        }

        public void SetVisualNovelColor(VisualNovelNames visualNovel)
        {
            Color color = FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovel);
            image.color = color;
            image01.color = color;
            image02.color = color;
            image03.color = color;
            image04.color = color;
            buttonText.color = color;
            buttonText02.color = color;
        }

        private void CopyDialog()
        {
            Debug.Log("KOPIEREN");
            string pattern = @"<\/?(b|i)>";
            string copyText = Regex.Replace(dialogText.text, pattern, string.Empty);
            GUIUtility.systemCopyBuffer = copyText.Replace("\n", "\n\n");
            StartCoroutine(ShowCopyPopup("Dialog"));
        }

        private void CopyFeedback()
        {
            Debug.Log("KOPIEREN");
            string pattern = @"<\/?(b|i)>";
            string copyText = Regex.Replace(aiFeedbackText.text, pattern, string.Empty);
            GUIUtility.systemCopyBuffer = copyText.Replace("\n", "\n\n");
            StartCoroutine(ShowCopyPopup("Feedback"));
        }

        private IEnumerator ShowCopyPopup(string whatWasCopied)
        {
            if (GameObjectManager.Instance().GetCopyNotification() == null)
            {
                Debug.Log("Kein GameObject mit dem Tag 'CopyNotification' gefunden.");
                yield break; // Beende die Coroutine, wenn das Objekt nicht gefunden wurde.
            }

            // Zugriff auf die TextMeshPro-Komponente im Popup
            TextMeshProUGUI textComponent = GameObjectManager.Instance().GetCopyNotification()
                .GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                if (whatWasCopied == "Feedback")
                {
                    // Setze den Text
                    textComponent.text = "Das Feedback wurde in die\r\nZwischenablage kopiert.";
                }

                if (whatWasCopied == "Dialog")
                {
                    // Setze den Text
                    textComponent.text = "Der Dialog wurde in die\r\nZwischenablage kopiert.";
                }
            }

            // Popup aktivieren
            GameObjectManager.Instance().GetCopyNotification().SetActive(true);

            // Warte die angegebene Zeit (z. B. 2 Sekunden)
            yield return new WaitForSeconds(2);

            // Popup ausblenden
            GameObjectManager.Instance().GetCopyNotification().SetActive(false);
        }
    }
}