using System.Collections;
using Assets._Scripts.Controller.CharacterController;
using Assets._Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.Props
{
    /// <summary>
    /// Represents a "Mug" prop in the UI that can perform an interactive animation.
    /// It implements the <see cref="IDecorationInteraction"/> interface, allowing it to be
    /// triggered as part of character interactions or other UI events.
    /// </summary>
    public class MugInteraction : MonoBehaviour, IDecorationInteraction
    {
        [SerializeField] private AudioClip sound;
        [SerializeField] private Sprite[] animationFrames;

        /// <summary>
        /// Plays an interactive animation for the mug, including sound and sprite changes.
        /// This method is designed to be called as a coroutine.
        /// </summary>
        /// <param name="container">The RectTransform of the UI container where the animated mug should be displayed.</param>
        /// <returns>An IEnumerator to support coroutine execution.</returns>
        public IEnumerator PlayInteraction(RectTransform container)
        {
            if (!TextToSpeechManager.Instance.IsTextToSpeechActivated()&& sound)
            {
                GlobalVolumeManager.Instance.PlaySound(sound);
            }

            GameObject instance;
            if (container.transform.childCount > 0)
            {
                instance = container.transform.GetChild(0).gameObject;
            }
            else
            {
                instance = Instantiate(gameObject, container.transform);
            }

            Image image = instance.GetComponent<Image>();
            if (image == null) yield break;

            for (int i = 1; i < animationFrames.Length; i++)
            {
                image.sprite = animationFrames[i];
                yield return new WaitForSeconds(0.5f);
            }

            image.sprite = animationFrames[0];
        }
    }
}