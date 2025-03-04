using System.Collections.Generic;
using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.SceneControllers
{
    public class FoundersWell2SceneController : SceneController
    {
        [SerializeField] private Button ressourcenButton;
        [SerializeField] private Button playHistoryButton;
        [SerializeField] private Button gemerkteNovelsButton;
        [SerializeField] private Button einstellungenButton;
        [SerializeField] private Button foundersWellButton;
        [SerializeField] private Button editButton;
        [SerializeField] private Button profilePicture;

        private Dictionary<Button, System.Action> _buttonActions;

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.FOUNDERS_WELL_2_SCENE);

            InitializeButtonActions();
            AddButtonListeners();

            // Load Screen size for later use
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            NovelColorManager.Instance().SetCanvasHeight(canvasRect.rect.height);
            NovelColorManager.Instance().SetCanvasWidth(canvasRect.rect.width);
        }

        private void InitializeButtonActions()
        {
            _buttonActions = new Dictionary<Button, System.Action>
            {
                { ressourcenButton, OnRessourcenButton },
                { playHistoryButton, OnPlayHistoryButton },
                { gemerkteNovelsButton, OnGemerkteNovelsButton },
                { einstellungenButton, OnEinstellungenButton },
                { foundersWellButton, OnFoundersWellButton },
                { editButton, OnEditButton },
                { profilePicture, OnProfilePicture }
            };
        }

        private void AddButtonListeners()
        {
            foreach (var buttonAction in _buttonActions)
            {
                buttonAction.Key.onClick.AddListener(() => buttonAction.Value.Invoke());
            }
        }

        private void OnRessourcenButton()
        {
            SceneLoader.LoadRessourcenScene();
        }

        private void OnPlayHistoryButton()
        {
            SceneLoader.LoadNovelHistoryScene();
        }

        private void OnGemerkteNovelsButton()
        {
            SceneLoader.LoadGemerkteNovelsScene();
        }

        private void OnEinstellungenButton()
        {
            SceneLoader.LoadEinstellungenScene();
        }

        private void OnFoundersWellButton()
        {
            SceneLoader.LoadFoundersBubbleScene();
        }

        private void OnEditButton()
        {
            DisplayInfoMessage(
                "Diese Funktionalitäten sind derzeit noch nicht verfügbar, da sich die App noch in der Entwicklung befindet. Wir arbeiten daran, die volle Funktionalität bald bereitzustellen.");
        }

        private void OnProfilePicture()
        {
            DisplayInfoMessage(
                "Diese Funktionalitäten sind derzeit noch nicht verfügbar, da sich die App noch in der Entwicklung befindet. Wir arbeiten daran, die volle Funktionalität bald bereitzustellen.");
        }
    }
}