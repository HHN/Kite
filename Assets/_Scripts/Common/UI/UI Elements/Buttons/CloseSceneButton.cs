using Assets._Scripts.Common.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Common.UI.UI_Elements.Buttons
{
    public class CloseSceneButton : MonoBehaviour
    {
        public Button button;

        private void Start()
        {
            button.onClick.AddListener(OnCloseButton);
        }

        public void OnCloseButton()
        {
            string lastScene = SceneRouter.GetTargetSceneForCloseButton();

            if (string.IsNullOrEmpty(lastScene))
            {
                SceneLoader.LoadMainMenuScene();
                return;
            }

            SceneLoader.LoadScene(lastScene);
        }
    }
}