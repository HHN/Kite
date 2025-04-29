using System.Collections;
using Assets._Scripts.Controller.CharacterController;
using Assets._Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.Props
{
    public class Jug : MonoBehaviour, IDecorationInteraction
    {
        [SerializeField] private AudioClip sound;
        [SerializeField] private Sprite[] animationFrames;

        public IEnumerator PlayInteraction(RectTransform container)
        {
            if (!TextToSpeechManager.Instance.IsTextToSpeechActivated())
            {
                GlobalVolumeManager.Instance.PlaySound(sound);
            }

            Image image = gameObject.GetComponent<Image>();
            
            image.sprite = animationFrames[1];
            if (container.transform.childCount > 0)
            {
                Destroy(container.transform.GetChild(0).gameObject);
            }
            Instantiate(gameObject, container.transform);
            yield return new WaitForSeconds(0.5f);
            
            image.sprite = animationFrames[2];
            if (container.transform.childCount > 0)
            {
                Destroy(container.transform.GetChild(0).gameObject);
            }
            Instantiate(gameObject, container.transform);
            yield return new WaitForSeconds(0.5f);
            
            image.sprite = animationFrames[3];
            if (container.transform.childCount > 0)
            {
                Destroy(container.transform.GetChild(0).gameObject);
            }
            Instantiate(gameObject, container.transform);
            yield return new WaitForSeconds(0.5f);
            
            image.sprite = animationFrames[0];
            if (container.transform.childCount > 0)
            {
                Destroy(container.transform.GetChild(0).gameObject);
            }
            Instantiate(gameObject, container.transform);

            yield return new WaitForSeconds(0f);
        }
    }
}