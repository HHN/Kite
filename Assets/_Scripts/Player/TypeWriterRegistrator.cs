using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Typewriter._Core;
using UnityEngine;

namespace Assets._Scripts.Player
{
    public class TypeWriterRegistrator : MonoBehaviour
    {
        private PlayNovelSceneController _controller;

        private void Start()
        {
            _controller = GameObject.Find("Controller").GetComponent<PlayNovelSceneController>();
        }

        public void OnStartTyping()
        {
            TypewriterCore typewriterCore = GetComponent<TypewriterCore>();

            _controller.currentTypeWriter = typewriterCore;

            if (GameManager.Instance.calledFromReload)
            {
                typewriterCore.SkipTypewriter();
            }
            else
            {
                _controller.StartTalking();
            }
        }

        public void OnStopTyping()
        {
            _controller.StopTalking();
        }
    }
}