using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI_Elements.Messages
{
    public class LeaveNovelAndGoBackToMainMenuMessageBox : MonoBehaviour
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

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void OnCloseButton()
        {
            CloseMessageBox();
        }

        private void OnCancelButton()
        {
            CloseMessageBox();
        }

        private void OnConfirmButton()
        {
            SceneLoader.LoadMainMenuScene();
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