using Assets._Scripts.Player;
using UnityEngine;

namespace Assets._Scripts.Common.UI_Elements.TextBoxes
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