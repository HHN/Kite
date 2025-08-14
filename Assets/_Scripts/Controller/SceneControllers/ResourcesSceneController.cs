using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    /// <summary>
    /// Manages the behavior and functionality specific to the Resources scene.
    /// Inherits from the SceneController base class to leverage core scene management capabilities.
    /// </summary>
    /// <remarks>
    /// This class can be used to define and implement scene-specific logic for the Resources scene.
    /// It can use methods like displaying error or information messages, which are inherited from the SceneController base class.
    /// </remarks>
    public class ResourcesSceneController : SceneController
    {
        [SerializeField] private RectTransform layout;

        [SerializeField] private Button bgaButton;
        [SerializeField] private Button hhnButton;
        [SerializeField] private Button kite2Button;

        /// <summary>
        /// Defines the initialization logic for the Resources scene.
        /// </summary>
        /// <remarks>
        /// This method is called when the scene is loaded and the associated GameObject is initialized.
        /// It sets up the scene layout, pushes the scene to the backstack for navigation tracking, and attaches event listeners to the UI buttons.
        /// </remarks>
        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.ResourcesScene);
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);

            bgaButton.onClick.AddListener(OnBgaButton);
            hhnButton.onClick.AddListener(OnHhnButton);
            kite2Button.onClick.AddListener(OnKite2Button);
        }

        /// <summary>
        /// Handles the click event for the BGA button in the Resources scene.
        /// </summary>
        /// <remarks>
        /// This method is invoked when the BGA button is clicked by the user. It opens the specified URL in the system's default web browser.
        /// </remarks>
        private void OnBgaButton()
        {
            Application.OpenURL("https://www.gruenderinnenagentur.de/home");
        }

        /// <summary>
        /// Opens the designated URL associated with the "HHN Button" in the default web browser.
        /// </summary>
        /// <remarks>
        /// This method is invoked when the HHN button is clicked by the user. It opens the specified URL in the system's default web browser.
        /// </remarks>
        private void OnHhnButton()
        {
            Application.OpenURL("https://www.hs-heilbronn.de/de/lab-sozioinformatik");
        }

        /// <summary>
        /// Handles the click event for the "Kite2" button.
        /// </summary>
        /// <remarks>
        /// This method is invoked when the Kite2 button is clicked by the user. It opens the specified URL in the system's default web browser.
        /// </remarks>
        private void OnKite2Button()
        {
            Application.OpenURL("https://kite2.de/");
        }
    }
}