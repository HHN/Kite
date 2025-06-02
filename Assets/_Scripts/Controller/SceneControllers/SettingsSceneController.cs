using System.Collections.Generic;
using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    public class SettingsSceneController : SceneController
    {
        [SerializeField] private Button barrierefreiheitButton;
        [SerializeField] private Button soundeinstellungButton;
        [SerializeField] private TMP_Text versionInfo;
        
        private Dictionary<Button, System.Action> _buttonActions;

        public void Start()
        {
            BackStackManager.Instance().Push(SceneNames.SettingsScene);
            InitializeButtonActions();
            AddButtonListeners();
            versionInfo.text = "Version: " + Application.version;
        }

        private void InitializeButtonActions()
        {
            _buttonActions = new Dictionary<Button, System.Action>
            {
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