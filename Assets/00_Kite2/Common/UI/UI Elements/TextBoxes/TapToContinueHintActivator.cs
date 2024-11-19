using _00_Kite2.Player;
using UnityEngine;

namespace _00_Kite2.Common.UI.UI_Elements.TextBoxes
{
    public class TapToContinueHintActivator : MonoBehaviour
    {
        private PlayNovelSceneController _controller;

        private void Start()
        {
            _controller = FindObjectOfType<PlayNovelSceneController>();
        }

        public void OnStartTyping()
        {
            _controller.SetTyping(true);
        }

        public void OnStopTyping()
        {
            _controller.SetTyping(false);
        }
    }
}
