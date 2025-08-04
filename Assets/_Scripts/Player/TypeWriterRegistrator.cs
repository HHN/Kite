using Assets._Scripts.Controller.SceneControllers;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Typewriter._Core;
using UnityEngine;

namespace Assets._Scripts.Player
{
    /// <summary>
    /// Registers a typewriter component with the main <see cref="PlayNovelSceneController"/>.
    /// It handles actions at the start and end of typing, such as controlling character talking animations
    /// and skipping typewriter effects if the game is loading from a saved state.
    /// </summary>
    public class TypeWriterRegistrator : MonoBehaviour
    {
        private PlayNovelSceneController _controller;

        /// <summary>
        /// Initializes the registrator by finding and storing a reference to the
        /// <see cref="PlayNovelSceneController"/> in the scene.
        /// </summary>
        private void Start()
        {
            _controller = GameObject.Find("PlayNovelSceneController").GetComponent<PlayNovelSceneController>();
        }

        /// <summary>
        /// Called when the typewriter effect starts typing text.
        /// This method registers the current <see cref="TypewriterCore"/> with the scene controller.
        /// It also handles skipping the typewriter effect if the game is being reloaded
        /// and initiates character talking animations.
        /// </summary>
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
                // _controller.StartTalking();
            }
        }

        /// <summary>
        /// Called when the typewriter effect finishes typing all text.
        /// This method signals the <see cref="PlayNovelSceneController"/> to stop any
        /// ongoing character talking animations.
        /// </summary>
        public void OnStopTyping()
        {
            // _controller.StopTalking();
        }
    }
}