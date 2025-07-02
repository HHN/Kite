using Assets._Scripts.Managers;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.Messages
{
    public class MessageBox : MonoBehaviour
    {
        [SerializeField] private AudioClip errorSound;
        [SerializeField] private AudioClip infoSound;
        [SerializeField] private bool isErrorMessage;
        
        public TextMeshProUGUI messageBoxHeadline;
        public TextMeshProUGUI messageBoxBody;
        public Button okButton;

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
            GlobalVolumeManager.Instance.PlaySound(isErrorMessage ? errorSound : infoSound);

            gameObject.SetActive(true);
        }

        private void OnOkButton()
        {
            CloseMessageBox();
        }

        public void CloseMessageBox()
        {
            if (this.IsNullOrDestroyed() || gameObject.IsNullOrDestroyed())
            {
                return;
            }

            Destroy(gameObject);
        }

        public void SetIsErrorMessage(bool isError)
        {
            isErrorMessage = isError;
        }
    }
}