using System.Collections.Generic;
using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    public class LegalInformationSceneController : SceneController
    {
        [SerializeField] private Button impressumButton;
        [SerializeField] private Button nutzungsbedingungenButton;
        [SerializeField] private Button datenschutzButton; 
        [SerializeField] private TMP_Text versionInfo;
        
        private Dictionary<Button, System.Action> _buttonActions;

        public void Start()
        {
            BackStackManager.Instance().Push(SceneNames.LegalInformationScene);
            
            InitializeButtonActions();
            AddButtonListeners();
            versionInfo.text = "Version: " + Application.version;
        }

        private void InitializeButtonActions()
        {
            _buttonActions = new Dictionary<Button, System.Action>
            {
                { impressumButton, OnImpressumButton },
                { nutzungsbedingungenButton, OnNutzungsbedingungenButton },
                { datenschutzButton, OnDatenschutzButton },
            };
        }

        private void AddButtonListeners()
        {
            foreach (var buttonAction in _buttonActions)
            {
                buttonAction.Key.onClick.AddListener(() => buttonAction.Value.Invoke());
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