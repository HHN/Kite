using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements
{
    /// <summary>
    /// Implements a custom toggle UI element. This component controls a visual indicator's
    /// active state based on whether a button is clicked, acting like a simple on/off switch.
    /// </summary>
    public class CustomToggle : MonoBehaviour
    {
        [SerializeField] private Button toggleButton;
        [SerializeField] private GameObject visualIndicator;
        [SerializeField] private bool isClicked;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Initializes the toggle to an "off" state by setting <see cref="isClicked"/> to false
        /// and deactivating the <see cref="visualIndicator"/>.
        /// </summary>
        private void Start()
        {
            isClicked = false;
            visualIndicator.SetActive(false);
        }

        /// <summary>
        /// This method should be called when the <see cref="toggleButton"/> is pressed.
        /// It inverts the <see cref="isClicked"/> state and updates the active state
        /// of the <see cref="visualIndicator"/> accordingly.
        /// </summary>
        public void OnButtonPressed()
        {
            isClicked = !isClicked;
            visualIndicator.SetActive(isClicked);
        }

        /// <summary>
        /// Returns the current state of the toggle.
        /// </summary>
        /// <returns>True if the toggle is "on" (clicked), false if "off" (not clicked).</returns>
        public bool IsClicked()
        {
            return isClicked;
        }
    }
}