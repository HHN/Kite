using _00_Kite2.Common.Managers;
using _00_Kite2.Common.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Common.UI.UI_Elements.Messages
{
    public class MessageBox : MonoBehaviour
    {
        public TextMeshProUGUI messageBoxHeadline;
        public TextMeshProUGUI messageBoxBody;
        public Button okButton;
        [SerializeField] private AudioSource errorSound;
        [SerializeField] private AudioSource infoSound;
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
                this.errorSound.Play();
            }
            else
            {
                this.infoSound.Play();
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