using _00_Kite2.Common.Managers;
using _00_Kite2.Common.Messages;
using _00_Kite2.Common.Utilities;
using _00_Kite2.Player;
using _00_Kite2.SaveNovelData;
using LeastSquares.Overtone;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _00_Kite2.Common.UI.UI_Elements.Buttons
{
    public class DeleteUserDataConfirmation : MonoBehaviour
    {
        [SerializeField] private TTSEngine engine;
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
            this._origin = origin;
            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        private void Start()
        {
            cancelButton.onClick.AddListener(OnCancelButton);
            confirmButton.onClick.AddListener(OnConfirmButton);

            if (_origin == "delete")
            {
                messageText.text = InfoMessages.DELETE_DATA_CONFIRMATION;
            }

            if (_origin == "reset")
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
            if (_origin == "delete")
            {
                TextToSpeechService.Instance().TextToSpeechReadLive(InfoMessages.DELETED_DATA, engine);
                PlayerDataManager.Instance().ClearRelevantUserdata();
                if (AnalyticsServiceHandler.Instance().IsAnalyticsInitialized())
                {
                    AnalyticsServiceHandler.Instance().DeleteCollectedData();
                }

                CloseMessageBox();
            }

            if (_origin == "reset")
            {
                SaveLoadManager.ClearAllSaveData();

                TextToSpeechService.Instance().TextToSpeechReadLive(InfoMessages.RESET_APP, engine);
                PlayerDataManager.Instance().ClearEverything();
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
                FavoritesManager.Instance().ClearFavorites();
                PlayRecordManager.Instance().ClearData();
                ShowPlayInstructionManager.Instance().SetShowInstruction(true);
                CloseMessageBox();
            }
        }

        private void OnCancelButton()
        {
            CloseMessageBox();
        }

        public void Activate()
        {
            this.gameObject.SetActive(true);
        }

        public void CloseMessageBox()
        {
            if (this.IsNullOrDestroyed() || this.gameObject.IsNullOrDestroyed())
            {
                return;
            }

            Destroy(this.gameObject);
        }
    }
}