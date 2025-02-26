using Assets._Scripts.Common.Managers;
using Assets._Scripts.Common.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Common
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

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.FOUNDERS_WELL_2_SCENE);

            ressourcenButton.onClick.AddListener(OnRessourcenButton);
            playHistoryButton.onClick.AddListener(OnPlayHistoryButton);
            gemerkteNovelsButton.onClick.AddListener(OnGemerkteNovelsButton);
            einstellungenButton.onClick.AddListener(OnEinstellungenButton);
            foundersWellButton.onClick.AddListener(OnFoundersWellButton);
            editButton.onClick.AddListener(OnEditButton);
            profilePicture.onClick.AddListener(OnProfilePicture);

            // Load Screen size for later use
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            NovelColorManager.Instance().SetCanvasHeight(canvasRect.rect.height);
            NovelColorManager.Instance().SetCanvasWidth(canvasRect.rect.width);
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