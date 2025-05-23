using System.Collections.Generic;
using Assets._Scripts.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    public class SettingsSceneController : SceneController
    {
        [SerializeField] private Button toggleDialogButton;
        [SerializeField] private Image activeDialogImage;
        [SerializeField] private Image inactiveDialogImage;
        [SerializeField] private Button barrierefreiheitButton;
        [SerializeField] private Button soundeinstellungButton;
        [SerializeField] private TMP_Text versionInfo;

        private Dictionary<Button, System.Action> _buttonActions;

        private bool _isActiveState = false;

        public void Start()
        {
            InitializeButtonActions();
            AddButtonListeners();
            versionInfo.text = "Version: " + Application.version;
        }

        private void InitializeButtonActions()
        {
            _buttonActions = new Dictionary<Button, System.Action>
            {
                { toggleDialogButton, OnToggleDialogButton },
                { barrierefreiheitButton, OnBarrierefreiheitButton },
                { soundeinstellungButton, OnSoundeinstellungButton }
            };
        }

        private void AddButtonListeners()
        {
            foreach (var buttonAction in _buttonActions)
            {
                buttonAction.Key.onClick.AddListener(() => buttonAction.Value.Invoke());
            }
        }

        /// <summary>
        /// Diese Methode wird aufgerufen, wenn der toggleDialogButton geklickt wird.
        /// Sie schaltet die Sichtbarkeit von activeDialogImage und inactiveDialogImage um.
        /// </summary>
        private void OnToggleDialogButton()
        {
            // Umschalten des Zustands
            _isActiveState = !_isActiveState;

            // Setze die Sichtbarkeit der Images basierend auf dem neuen Zustand
            if (activeDialogImage != null)
            {
                activeDialogImage.gameObject.SetActive(_isActiveState);
            }

            if (inactiveDialogImage != null)
            {
                inactiveDialogImage.gameObject.SetActive(!_isActiveState);
            }
        }

        private void OnBarrierefreiheitButton()
        {
            SceneLoader.LoadAccessibilityScene();
        }

        private void OnImpressumButton()
        {
            SceneLoader.LoadLegalNoticeScene();
        }

        private void OnNutzungsbedingungenButton()
        {
            SceneLoader.LoadTermsOfUseScene();
        }

        private void OnDatenschutzButton()
        {
            SceneLoader.LoadPrivacyPolicyScene();
        }

        private void OnSoundeinstellungButton()
        {
            SceneLoader.LoadSoundSettingsScene();
        }
    }
}