using System.Collections;
using Assets._Scripts.Controller.CharacterController;
using Assets._Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.Props
{
    /// <summary>
    /// Represents a "Plant" prop in the UI that can perform an interactive animation.
    /// It implements the <see cref="IDecorationInteraction"/> interface, allowing it to be
    /// triggered as part of character interactions or other UI events.
    /// </summary>
    public class Plant : MonoBehaviour, IDecorationInteraction
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private AudioClip sound;
        [SerializeField] private Sprite[] animationFrames;

        /// <summary>
        /// Plays an interactive animation for the plant, including sound and sprite changes.
        /// This method is designed to be called as a coroutine.
        /// </summary>
        /// <param name="container">The RectTransform of the UI container where the animated plant should be displayed.</param>
        /// <returns>An IEnumerator to support coroutine execution.</returns>
        public IEnumerator PlayInteraction(RectTransform container)
        {
            if (!TextToSpeechManager.Instance.IsTextToSpeechActivated())
            {
                GlobalVolumeManager.Instance.PlaySound(sound);
            }

            Image image = prefab.GetComponent<Image>();
            image.sprite = animationFrames[1];
            if (container.transform.childCount > 0)
            {
                Destroy(container.transform.GetChild(0).gameObject);
            }
            Instantiate(prefab, container.transform);
            yield return new WaitForSeconds(0.5f);
            
            image.sprite = animationFrames[2];
            if (container.transform.childCount > 0)
            {
                Destroy(container.transform.GetChild(0).gameObject);
            }
            Instantiate(prefab, container.transform);
            yield return new WaitForSeconds(0.5f);
            
            image.sprite = animationFrames[0];
            if (container.transform.childCount > 0)
            {
                Destroy(container.transform.GetChild(0).gameObject);
            }
            Instantiate(prefab, container.transform);

            yield return new WaitForSeconds(0f);
        }
    }
}