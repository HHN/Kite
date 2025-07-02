using Assets._Scripts.Managers;
using Assets._Scripts.Messages;
using Assets._Scripts.SaveNovelData;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.Buttons
{
    static class Origins
    {
        public const string Reset = "reset";
    }

    
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

        public void Initialize(string origin)
        {
            _origin = origin;
            FontSizeManager.Instance().UpdateAllTextComponents();
        }

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
                
                CloseMessageBox();
            }

            DialogHistoryManager.Instance().ClearList();
        }

        private void OnCancelButton()
        {
            CloseMessageBox();
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void CloseMessageBox()
        {
            if (this.IsNullOrDestroyed() || gameObject.IsNullOrDestroyed())
            {
                return;
            }

            GameManager.Instance.resetApp = true;
            GameManager.Instance.DisplayMessage(InfoMessages.RESET_APP);
            
            Destroy(gameObject);
        }
    }
}