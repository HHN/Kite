using System.Collections.Generic;
using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    /// <summary>
    /// Controls the behavior and initialization logic of the Legal Information Scene in the application.
    /// Extends the functionality of the base SceneController class.
    /// </summary>
    /// <remarks>
    /// This controller handles version information display, UI font size updates, and navigation management.
    /// </remarks>
    public class LegalInformationSceneController : SceneController
    {
        [SerializeField] private Button impressumButton;
        [SerializeField] private Button nutzungsbedingungenButton;
        [SerializeField] private Button datenschutzButton; 
        [SerializeField] private TMP_Text versionInfo;
        
        private Dictionary<Button, System.Action> _buttonActions;

        /// <summary>
        /// Initializes the Legal Information Scene by configuring UI elements,
        /// setting up navigation management, and updating font sizes.
        /// </summary>
        /// <remarks>
        /// This method is executed when the scene starts. It pushes the current scene
        /// onto the back stack for navigation purposes, updates the font sizes across
        /// all UI elements, initializes button actions, adds listeners to buttons, and
        /// sets the version information text using the application's version.
        /// </remarks>
        public void Start()
        {
            BackStackManager.Instance().Push(SceneNames.LegalInformationScene);
            
            FontSizeManager.Instance().UpdateAllTextComponents();
            
            InitializeButtonActions();
            AddButtonListeners();
            versionInfo.text = "Version: " + Application.version;
        }

        /// <summary>
        /// Configures button-to-action mappings for the legal information scene.
        /// </summary>
        /// <remarks>
        /// This method initializes a dictionary that associates UI buttons with their respective actions.
        /// Each button within the legal information scene is linked to a specific callback method,
        /// enabling navigation to related scenes when buttons are interacted with.
        /// </remarks>
        private void InitializeButtonActions()
        {
            _buttonActions = new Dictionary<Button, System.Action>
            {
                { impressumButton, OnImpressumButton },
                { nutzungsbedingungenButton, OnNutzungsbedingungenButton },
                { datenschutzButton, OnDatenschutzButton },
            };
        }

        /// <summary>
        /// Adds event listeners to UI buttons by associating them with their corresponding actions.
        /// </summary>
        /// <remarks>
        /// This method iterates through the dictionary of buttons and actions, and for each button,
        /// assigns a listener to the button's click event. The assigned listener executes the associated action.
        /// </remarks>
        private void AddButtonListeners()
        {
            foreach (var buttonAction in _buttonActions)
            {
                buttonAction.Key.onClick.AddListener(() => buttonAction.Value.Invoke());
            }
        }

        /// <summary>
        /// Executes logic associated with the Impressum (Legal Notice) button.
        /// </summary>
        /// <remarks>
        /// Loads the Legal Notice scene using the SceneLoader utility. This method is
        /// intended to be associated with the Impressum button and is triggered when
        /// the user interacts with it.
        /// </remarks>
        private void OnImpressumButton()
        {
            SceneLoader.LoadLegalNoticeScene();
        }

        /// <summary>
        /// Executes logic associated with the Nutzungsbedingungen (Terms of Use) button.
        /// </summary>
        /// <remarks>
        /// Loads the Terms of Use scene using the SceneLoader utility. This method is
        /// intended to be associated with the Nutzungsbedingungen button and is triggered when
        /// the user interacts with it.
        /// </remarks>
        private void OnNutzungsbedingungenButton()
        {
            SceneLoader.LoadTermsOfUseScene();
        }

        /// <summary>
        /// Executes logic associated with the Datenschutz (Privacy Policy) button.
        /// </summary>
        /// <remarks>
        /// Loads the Privacy Policy scene using the SceneLoader utility. This method is
        /// intended to be associated with the Datenschutz button and is triggered when
        /// the user interacts with it.
        /// </remarks>
        private void OnDatenschutzButton()
        {
            SceneLoader.LoadPrivacyPolicyScene();
        }
    }
}