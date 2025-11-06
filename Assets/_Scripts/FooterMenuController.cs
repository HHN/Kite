using System.Collections.Generic;
using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets._Scripts
{
    [System.Serializable]
    public class FooterButton
    {
        public Button button;
        public TextMeshProUGUI textElement;
        public Sprite activeSprite;
        public Sprite inactiveSprite;
        public string sceneName;
    }
    
    /// <summary>
    /// Manages the behavior and visual state of the application's footer menu.
    /// It handles navigation between different main scenes and updates the
    /// appearance of the active menu button. It also responds to global footer
    /// activation/deactivation events.
    /// </summary>
    public class FooterMenuController : MonoBehaviour
    {
        [SerializeField] private List<FooterButton> footerButtons;
        [SerializeField] private string hexColor = "#B6BBC0";

        /// <summary>
        /// Initializes the FooterMenuController when the script instance is being loaded.
        /// Subscribes to the footer activation change event to dynamically handle changes
        /// in the activation state and update associated functionality.
        /// </summary>
        private void Awake()
        {
            FooterActivationManager.Instance().OnActivationChanged += HandleFooterActivationChanged;
        }

        /// <summary>
        /// Cleans up resources and unsubscribes from the footer activation change event when the FooterMenuController is destroyed.
        /// This ensures that the instance does not remain subscribed to events, preventing potential memory leaks or unintended behavior.
        /// </summary>
        private void OnDestroy()
        {
            FooterActivationManager.Instance().OnActivationChanged -= HandleFooterActivationChanged;
        }

        /// <summary>
        /// Called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// Checks the initial activation status of the footer and configures buttons accordingly.
        /// </summary>
        private void Start()
        {
            bool isActive = FooterActivationManager.Instance().IsActivated();

            if (isActive)
                SetupButtonsAndVisuals();
        }

        /// <summary>
        /// Handles changes to the activation state of the footer menu.
        /// Adjusts button visibility and functionality based on the provided activation state.
        /// </summary>
        /// <param name="newValue">The new activation state of the footer menu. True if the footer should be active; false otherwise.</param>
        private void HandleFooterActivationChanged(bool newValue)
        {
            SetButtonsActive(newValue);

            if (newValue)
                SetupButtonsAndVisuals();
            else
                RemoveButtonListeners();
        }

        /// <summary>
        /// Sets the active state of all buttons in the footer menu.
        /// Enables or disables the GameObject of each button based on the specified state.
        /// </summary>
        /// <param name="active">If true, all buttons are activated; if false, all buttons are deactivated.</param>
        private void SetButtonsActive(bool active)
        {
            foreach (var fb in footerButtons)
                fb.button.gameObject.SetActive(active);
        }

        /// <summary>
        /// Removes all click event listeners from the buttons in the footer menu.
        /// Ensures that button actions no longer respond to user inputs after being removed.
        /// </summary>
        private void RemoveButtonListeners()
        {
            foreach (var fb in footerButtons)
                fb.button.onClick.RemoveAllListeners();
        }

        /// <summary>
        /// Sets up the click listeners for all footer buttons and updates their initial visual states.
        /// </summary>
        private void SetupButtonsAndVisuals()
        {
            foreach (var fb in footerButtons)
            {
                fb.button.onClick.AddListener(() => OnFooterButtonClicked(fb));
                fb.button.image.sprite = fb.inactiveSprite;
                if (ColorUtility.TryParseHtmlString(hexColor, out Color color))
                    fb.textElement.color = color;
            }

            SetActiveButtonVisual();
        }

        /// <summary>
        /// Handles the footer button click event to navigate to the associated scene.
        /// Ensures that the active scene is not reloaded if it matches the button's associated scene.
        /// </summary>
        /// <param name="fb">The footer button data associated with the clicked button,
        /// including the scene name and visual elements for the button.</param>
        private void OnFooterButtonClicked(FooterButton fb)
        {
            if (fb.sceneName == SceneManager.GetActiveScene().name) return;

            SceneLoader.LoadScene(fb.sceneName);
        }

        /// <summary>
        /// Updates the visual state of all footer buttons based on the currently active scene.
        /// Highlights the button associated with the active scene, changing its sprite and text color,
        /// while reverting others to their default inactive appearance.
        /// </summary>
        private void SetActiveButtonVisual()
        {
            string currentScene = SceneManager.GetActiveScene().name;

            foreach (var fb in footerButtons)
            {
                bool isActive = fb.sceneName == currentScene;
                fb.button.image.sprite = isActive ? fb.activeSprite : fb.inactiveSprite;
                fb.textElement.color  = isActive ? Color.white : ColorUtility.TryParseHtmlString(hexColor, out var c) ? c : Color.gray;
            }
        }
    }
}
