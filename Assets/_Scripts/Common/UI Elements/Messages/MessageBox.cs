using Assets._Scripts.Common.Managers;
using Assets._Scripts.Common.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Common.UI.UI_Elements.Messages
{
    public class MessageBox : MonoBehaviour
    {
        public TextMeshProUGUI messageBoxHeadline;
        public TextMeshProUGUI messageBoxBody;
        public Button okButton;
        [SerializeField] private AudioClip errorSound;
        [SerializeField] private AudioClip infoSound;
        [SerializeField] private bool isErrorMessage;

        private void Start()
        {
            okButton.onClick.AddListener(OnOkButton);
            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        public void SetHeadline(string headline)
        {
            messageBoxHeadline.text = headline;
        }

        public void SetBody(string headline)
        {
            messageBoxBody.text = headline;
        }

        public void Activate()
        {
            if (this.isErrorMessage)
            {
                GlobalVolumeManager.Instance.PlaySound(errorSound);
            }
            else
            {
                GlobalVolumeManager.Instance.PlaySound(infoSound);
            }

            this.gameObject.SetActive(true);
        }

        private void OnOkButton()
        {
            this.CloseMessageBox();
        }

        public void CloseMessageBox()
        {
            if (this.IsNullOrDestroyed() || this.gameObject.IsNullOrDestroyed())
            {
                return;
            }

            Destroy(this.gameObject);
        }

        public void SetIsErrorMessage(bool isErrorMessage)
        {
            this.isErrorMessage = isErrorMessage;
        }
    }
}