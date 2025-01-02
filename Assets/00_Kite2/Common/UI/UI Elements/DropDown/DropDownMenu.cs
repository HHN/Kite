using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Common.UI.UI_Elements.DropDown
{
    public class DropDownMenu : MonoBehaviour
    {
        [SerializeField] private GameObject menuWrapper;
        [SerializeField] private Button menuButton;
        [SerializeField] private Image indicatorImage;
        [SerializeField] private Sprite spriteWhileOpen;
        [SerializeField] private Sprite spriteWhileClosed;
        [SerializeField] private bool isOpen;
        [SerializeField] private List<RectTransform> listOfObjectToUpdate;
        [SerializeField] private List<DropDownMenu> childMenus;

        // Start is called before the first frame update
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

            StartCoroutine(RebuildLayout());
        }

        private void OpenMenu()
        {
            indicatorImage.sprite = spriteWhileOpen;
            menuWrapper.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate(menuWrapper.GetComponent<RectTransform>());
            isOpen = true;
        }

        private void CloseMenu()
        {
            indicatorImage.sprite = spriteWhileClosed;
            menuWrapper.SetActive(false);
            LayoutRebuilder.ForceRebuildLayoutImmediate(menuWrapper.GetComponent<RectTransform>());
            isOpen = false;
        }

        public void AddLayoutToUpdateOnChange(RectTransform rect)
        {
            listOfObjectToUpdate.Add(rect);
        }

        public IEnumerator RebuildLayout()
        {
            yield return new WaitForEndOfFrame();

            if (childMenus != null)
            {
                foreach (DropDownMenu menu in childMenus)
                {
                    menu.RebuildLayout();
                }
            }

            if (listOfObjectToUpdate != null)
            {
                foreach (RectTransform rect in listOfObjectToUpdate)
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
                }
            }
        }

        public void AddChildMenu(DropDownMenu menu)
        {
            if (childMenus == null)
            {
                childMenus = new List<DropDownMenu>();
            }

            childMenus.Add(menu);
        }
    }
}