using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.SceneControllers
{
    public class ResourcesSceneController : SceneController
    {
        [SerializeField] private RectTransform layout;

        [SerializeField] private Button bgaButton;
        [SerializeField] private Button hhnButton;
        [SerializeField] private Button kite2Button;

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.ResourcesScene);
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);

            bgaButton.onClick.AddListener(OnBgaButton);
            hhnButton.onClick.AddListener(OnHhnButton);
            kite2Button.onClick.AddListener(OnKite2Button);
        }

        private void OnBgaButton()
        {
            Application.OpenURL("https://www.gruenderinnenagentur.de/home");
        }

        private void OnHhnButton()
        {
            Application.OpenURL("https://www.hs-heilbronn.de/de/lab-sozioinformatik");
        }

        private void OnKite2Button()
        {
            Application.OpenURL("https://kite2.de/");
        }
    }
}