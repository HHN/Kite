using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

// Required for [Preserve] attribute.

namespace Assets._Scripts.UIElements
{
    /// <summary>
    /// Manages a UI dropdown menu, allowing it to be opened or closed,
    /// and dynamically updates the layout of specified RectTransforms.
    /// The [Preserve] attribute ensures that this class and its members are not stripped
    /// by code stripping, which can be important for UI elements referenced dynamically.
    /// </summary>
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

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Adds a listener to the menu button and initializes the menu to its default state.
        /// </summary>
        private void Start()
        {
            if (menuButton != null)
            {
                menuButton.onClick.AddListener(OnMenuButton);
            }
            InitiateMenu();
        }

        /// <summary>
        /// Initializes the menu's visual state (open or closed) based on the <see cref="isOpen"/> variable.
        /// </summary>
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

        /// <summary>
        /// Callback function for the menu button click event.
        /// Toggles the menu's open/close state and then initiates a layout rebuild.
        /// </summary>
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

        /// <summary>
        /// Opens the dropdown menu: activates its wrapper, updates the indicator image,
        /// and forces an immediate layout rebuild of the wrapper.
        /// </summary>
        private void OpenMenu()
        {
            if (indicatorImage != null && spriteWhileOpen != null)
            {
                indicatorImage.sprite = spriteWhileOpen;
            }

            if (menuWrapper != null)
            {
                menuWrapper.SetActive(true);
                var rt = menuWrapper.GetComponent<RectTransform>();
                if (rt != null)
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
                }
            }

            isOpen = true;
        }

        /// <summary>
        /// Closes the dropdown menu: deactivates its wrapper, updates the indicator image,
        /// and forces an immediate layout rebuild of the wrapper.
        /// </summary>
        private void CloseMenu()
        {
            if (indicatorImage != null && spriteWhileClosed != null)
            {
                indicatorImage.sprite = spriteWhileClosed;
            }

            if (menuWrapper != null)
            {
                menuWrapper.SetActive(false);
                var rt = menuWrapper.GetComponent<RectTransform>();
                if (rt != null)
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
                }
            }

            isOpen = false;
        }

        /// <summary>
        /// Adds a <see cref="RectTransform"/> to the list of objects whose layout should be
        /// updated when the menu's state changes. This is useful for parent layouts
        /// that need to adjust when the dropdown expands or collapses.
        /// </summary>
        /// <param name="rect">The <see cref="RectTransform"/> to add to the update list.</param>
        public void AddLayoutToUpdateOnChange(RectTransform rect)
        {
            if (rect == null) return;
            if (listOfObjectToUpdate == null) listOfObjectToUpdate = new List<RectTransform>();
            if (!listOfObjectToUpdate.Contains(rect)) listOfObjectToUpdate.Add(rect);
        }

        /// <summary>
        /// Coroutine to rebuild the UI layout of affected elements.
        /// It waits for the end of the frame to ensure all UI elements have updated their states,
        /// then rebuilds the layouts of child menus and any registered RectTransforms.
        /// </summary>
        /// <returns>An IEnumerator for coroutine execution.</returns>
        public IEnumerator RebuildLayout()
        {
            yield return new WaitForEndOfFrame();

            if (childMenus != null)
            {
                foreach (var menu in childMenus)
                {
                    if (menu != null)
                    {
                        StartCoroutine(menu.RebuildLayout());
                    }
                }
            }

            if (listOfObjectToUpdate != null)
            {
                Canvas.ForceUpdateCanvases();
                foreach (var rect in listOfObjectToUpdate)
                {
                    if (rect != null)
                    {
                        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
                    }
                }
            }
        }
    }
}