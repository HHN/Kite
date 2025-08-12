using Assets._Scripts.Controller.SceneControllers;
using UnityEngine;

namespace Assets._Scripts.UIElements.TextBoxes
{
    /// <summary>
    /// Manages the activation and deactivation of a "Tap to Continue" hint based on text typing events.
    /// It communicates with the <see cref="PlayNovelSceneController"/> to update the typing status.
    /// </summary>
    public class TapToContinueHintActivator : MonoBehaviour
    {
        private PlayNovelSceneController _controller;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Finds and assigns the <see cref="PlayNovelSceneController"/> in the scene.
        /// </summary>
        private void Start()
        {
            _controller = FindObjectOfType<PlayNovelSceneController>();
        }

        /// <summary>
        /// This method should be called when text typing begins (e.g., at the start of a typewriter animation).
        /// It informs the <see cref="PlayNovelSceneController"/> that text is currently being typed.
        /// </summary>
        public void OnStartTyping()
        {
            _controller.SetTyping(true);
        }

        /// <summary>
        /// This method should be called when text typing ends (e.g., when a typewriter animation completes).
        /// It informs the <see cref="PlayNovelSceneController"/> that text typing has finished.
        /// </summary>
        public void OnStopTyping()
        {
            _controller.SetTyping(false);
        }
    }
}