using System.Collections;
using Assets._Scripts.Controller.CharacterController;
using Assets._Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.Props
{
    /// <summary>
    /// Represents a "Lamp" prop in the UI that can be toggled on and off with an animation and sound effect.
    /// It implements the <see cref="IDecorationInteraction"/> interface, allowing it to be
    /// triggered as part of character interactions or other UI events.
    /// </summary>
    public class LampInteraction : MonoBehaviour, IDecorationInteraction
    {
        [SerializeField] private AudioClip soundOn;
        [SerializeField] private AudioClip soundOff;
        [SerializeField] private Sprite lampOn;
        [SerializeField] private Sprite lampOff;
        
        private bool _decoLampeStatus;

        /// <summary>
        /// Toggles the lamp's state (on/off), plays the corresponding sound, and updates its sprite.
        /// This method is designed to be called as a coroutine.
        /// </summary>
        /// <param name="container">The RectTransform of the UI container. (Note: This parameter is not directly used in the current implementation, as the lamp animates itself).</param>
        /// <returns>An IEnumerator to support coroutine execution.</returns>
        public IEnumerator PlayInteraction(RectTransform container)
        {
            AudioClip clip = _decoLampeStatus ? soundOn : soundOff;
            if (!TextToSpeechManager.Instance.IsTextToSpeechActivated() && clip)
            {
                GlobalVolumeManager.Instance.PlaySound(clip);
            }

            Image image = gameObject.GetComponent<Image>();
            image.sprite = _decoLampeStatus ? lampOff : lampOn;
            _decoLampeStatus = !_decoLampeStatus;

            yield return new WaitForSeconds(0.5f);
        }
    }
}