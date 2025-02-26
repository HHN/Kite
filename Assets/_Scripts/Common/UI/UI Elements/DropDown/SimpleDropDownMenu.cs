using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Common.UI.UI_Elements.DropDown
{
    public class SimpleDropDownMenu : MonoBehaviour
    {
        [SerializeField] private GameObject menuWrapper;
        [SerializeField] private Button menuButton;
        [SerializeField] private Image indicatorImage;
        [SerializeField] private Sprite spriteWhileOpen;
        [SerializeField] private Sprite spriteWhileClosed;
        [SerializeField] private bool isOpen;

        private void Start()
        {
            menuButton.onClick.AddListener(OnMenuButton);
            InitiateMenu();
        }

        public bool IsOpen()
        {
            return isOpen;
        }

        private void InitiateMenu()
        {
            if (isOpen)
            {
                OpenMenu();
                return;
            }

            CloseMenu();
        }

        public void SetMenuOpen(bool setOpen)
        {
            if (setOpen)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }
        }

        private void OnMenuButton()
        {
            if (isOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }

        private void OpenMenu()
        {
            indicatorImage.sprite = spriteWhileOpen;
            menuWrapper.SetActive(true);
            isOpen = true;
        }

        private void CloseMenu()
        {
            indicatorImage.sprite = spriteWhileClosed;
            menuWrapper.SetActive(false);
            isOpen = false;
        }
    }
}