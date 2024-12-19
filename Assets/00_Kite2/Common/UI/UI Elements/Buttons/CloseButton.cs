using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Common.UI.UI_Elements.Buttons
{
    public class CloseButton : MonoBehaviour
    {
        public Button button;
        public GameObject gameObjectToHide;

        private void Start()
        {
            button.onClick.AddListener(OnClick);
        }

        public void OnClick()
        {
            if (gameObjectToHide == null)
            {
                return;
            }

            gameObjectToHide.SetActive(false);
        }
    }
}