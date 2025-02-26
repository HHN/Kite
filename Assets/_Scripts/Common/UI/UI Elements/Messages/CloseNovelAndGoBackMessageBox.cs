using Assets._Scripts.Common.Managers;
using Assets._Scripts.Common.SceneManagement;
using Assets._Scripts.Common.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Common.UI.UI_Elements.Messages
{
    public class CloseNovelAndGoBackMessageBox : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI messageBoxHeadline;
        [SerializeField] private TextMeshProUGUI messageBoxBody;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button cancelButton;
        [SerializeField] private Button confirmButton;

        private void Start()
        {
            closeButton.onClick.AddListener(OnCloseButton);
            cancelButton.onClick.AddListener(OnCancelButton);
            confirmButton.onClick.AddListener(OnConfirmButton);
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
            this.gameObject.SetActive(true);
        }

        public void OnCloseButton()
        {
            this.CloseMessageBox();
        }

        private void OnCancelButton()
        {
            this.CloseMessageBox();
        }

        private void OnConfirmButton()
        {
            SceneLoader.LoadMainMenuScene();
        }

        private void CloseMessageBox()
        {
            if (this.IsNullOrDestroyed() || this.gameObject.IsNullOrDestroyed())
            {
                return;
            }

            Destroy(this.gameObject);
        }
    }
}