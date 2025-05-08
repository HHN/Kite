using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting;

namespace Assets._Scripts.UIElements.DropDown
{
    [Preserve]
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

        private void Start()
        {
            if (menuButton != null)
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
            }
            else
            {
                CloseMenu();
            }
        }

        public void SetMenuOpen(bool setOpen)
        {
            if (setOpen)
                OpenMenu();
            else
                CloseMenu();
        }

        private void OnMenuButton()
        {
            if (isOpen)
                CloseMenu();
            else
                OpenMenu();

            StartCoroutine(RebuildLayout());
        }

        private void OpenMenu()
        {
            if (indicatorImage != null && spriteWhileOpen != null)
                indicatorImage.sprite = spriteWhileOpen;

            if (menuWrapper != null)
            {
                menuWrapper.SetActive(true);
                var rt = menuWrapper.GetComponent<RectTransform>();
                if (rt != null)
                    LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
            }

            isOpen = true;
        }

        private void CloseMenu()
        {
            if (indicatorImage != null && spriteWhileClosed != null)
                indicatorImage.sprite = spriteWhileClosed;

            if (menuWrapper != null)
            {
                menuWrapper.SetActive(false);
                var rt = menuWrapper.GetComponent<RectTransform>();
                if (rt != null)
                    LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
            }

            isOpen = false;
        }

        public void AddLayoutToUpdateOnChange(RectTransform rect)
        {
            if (rect == null) return;
            if (listOfObjectToUpdate == null)
                listOfObjectToUpdate = new List<RectTransform>();
            if (!listOfObjectToUpdate.Contains(rect))
                listOfObjectToUpdate.Add(rect);
        }

        public IEnumerator RebuildLayout()
        {
            yield return new WaitForEndOfFrame();

            // Update child menus first
            if (childMenus != null)
            {
                foreach (var menu in childMenus)
                {
                    if (menu != null)
                        StartCoroutine(menu.RebuildLayout());
                }
            }

            // Update registered RectTransforms
            if (listOfObjectToUpdate != null)
            {
                Canvas.ForceUpdateCanvases();
                foreach (var rect in listOfObjectToUpdate)
                {
                    if (rect != null)
                        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
                }
            }
        }

        public void AddChildMenu(DropDownMenu menu)
        {
            if (menu == null) return;
            if (childMenus == null)
                childMenus = new List<DropDownMenu>();
            if (!childMenus.Contains(menu))
                childMenus.Add(menu);
        }
    }
}
