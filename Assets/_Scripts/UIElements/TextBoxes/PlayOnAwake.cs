using Assets._Scripts.Managers;
using UnityEngine;

namespace Assets._Scripts.UIElements.TextBoxes
{
    /// <summary>
    /// Plays an audio clip immediately when the GameObject it's attached to becomes active in the scene.
    /// This is useful for background music, ambient sounds, or one-shot sound effects upon scene load or UI activation.
    /// </summary>
    public class PlayOnAwake : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;

        /// <summary>
        /// Called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// It plays the assigned audio clip through the GlobalVolumeManager.
        /// </summary>
        private void Start()
        {
            GlobalVolumeManager.Instance.PlaySound(audioClip);
        }
    }
}
