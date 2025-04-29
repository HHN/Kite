using System.Collections;
using Assets._Scripts.Controller.CharacterController;
using Assets._Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.Props
{
    public class Lamp : MonoBehaviour, IDecorationInteraction
    {
        [SerializeField] private AudioClip soundOn;
        [SerializeField] private AudioClip soundOff;
        [SerializeField] private Sprite lampOn;
        [SerializeField] private Sprite lampOff;
        
        private bool _decoLampeStatus;

        public IEnumerator PlayInteraction(RectTransform container)
        {
            AudioClip clip = _decoLampeStatus ? soundOn : soundOff;
            if (!TextToSpeechManager.Instance.IsTextToSpeechActivated())
            {
                GlobalVolumeManager.Instance.PlaySound(clip);
            }

            if (_decoLampeStatus)
            {
                Image image = gameObject.GetComponent<Image>();
                image.sprite = lampOff;
                if (container.transform.childCount > 0)
                {
                    Destroy(container.transform.GetChild(0).gameObject);
                }

                Instantiate(gameObject, container.transform);
                _decoLampeStatus = false;
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                Image image = gameObject.GetComponent<Image>();
                image.sprite = lampOn;
                if (container.transform.childCount > 0)
                {
                    Destroy(container.transform.GetChild(0).gameObject);
                }

                Instantiate(gameObject, container.transform);
                _decoLampeStatus = true;
                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(0f);
        }
    }
}