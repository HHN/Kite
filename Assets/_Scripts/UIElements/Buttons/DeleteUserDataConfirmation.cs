using Assets._Scripts.Managers;
using Assets._Scripts.Messages;
using Assets._Scripts.SaveNovelData;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//using LeastSquares.Overtone;

namespace Assets._Scripts.UIElements.Buttons
{
    public class DeleteUserDataConfirmation : MonoBehaviour
    {
        //[SerializeField] private TTSEngine engine;
        [SerializeField] private Button cancelButton;
        [SerializeField] private Button confirmButton;
        [SerializeField] private GameObject background;
        [SerializeField] private GameObject backgroundLeave;
        [SerializeField] private GameObject textCancel;
        [SerializeField] private GameObject person;
        [SerializeField] private GameObject uiContainer;
        [SerializeField] private TextMeshProUGUI messageText;

        private const string Delete = "delete";
        private const string Reset = "reset";

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

            if (_origin == Delete)
            {
                messageText.text = InfoMessages.DELETE_DATA_CONFIRMATION;
            }
            
            if (_origin == Reset)
            {
                messageText.text = InfoMessages.RESET_APP_CONFIRMATION;
            }

            AdjustImageSize();
            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        private void AdjustImageSize()
        {
            float backgroundWidth = background.GetComponent<RectTransform>().rect.width;

            if (person.GetComponent<RectTransform>().rect.width > backgroundWidth)
            {
                person.GetComponent<RectTransform>()
                    .SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, backgroundWidth);
                person.GetComponent<RectTransform>()
                    .SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, backgroundWidth);
            }
        }

        private void OnConfirmButton()
        {
            if (_origin == Delete)
            {
                StartCoroutine(TextToSpeechManager.Instance.Speak(InfoMessages.DELETED_DATA));
                PlayerDataManager.Instance().ClearRelevantUserdata();
                if (AnalyticsServiceHandler.Instance().IsAnalyticsInitialized())
                {
                    AnalyticsServiceHandler.Instance().DeleteCollectedData();
                }

                CloseMessageBox();
            }

            if (_origin == Reset)
            {
                SaveLoadManager.ClearAllSaveData();

                StartCoroutine(TextToSpeechManager.Instance.Speak(InfoMessages.RESET_APP));
                PlayerDataManager.Instance().ClearEverything();
                UnityEngine.PlayerPrefs.DeleteAll();
                UnityEngine.PlayerPrefs.Save();
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

            Destroy(gameObject);
        }
    }
}