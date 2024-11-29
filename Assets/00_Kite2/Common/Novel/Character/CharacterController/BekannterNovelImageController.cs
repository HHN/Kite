using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Common.Novel.Character.CharacterController
{
    public class BekannterNovelImageController : NovelImageController
    {
        [SerializeField] private GameObject decoPlantPrefab;

        [SerializeField] private GameObject decoPlantContainer;

        // [SerializeField] private GameObject characterPrefab;
        [SerializeField] private AudioClip decoPlantAudio;
        [SerializeField] private Sprite[] animationFramesPlant;

        [SerializeField] private Transform characterContainer;
        [SerializeField] private List<GameObject> characterPrefabs;

        private GameObject _instantiatedCharacter;

        private void Start()
        {
            SetInitialSpritesForImages();
            SetInitialCharacters();
        }

        private void SetInitialSpritesForImages()
        {
            Image image = decoPlantPrefab.GetComponent<Image>();
            image.sprite = animationFramesPlant[0];
            if (decoPlantContainer.transform.childCount > 0)
            {
                Destroy(decoPlantContainer.transform.GetChild(0).gameObject);
            }

            Instantiate(decoPlantPrefab, decoPlantContainer.transform);
        }

        private void SetInitialCharacters()
        {
            if (characterPrefabs.Count > 0)
            {
                int randomIndex = Random.Range(0, characterPrefabs.Count);
                GameObject randomGameObject = characterPrefabs[randomIndex];
                
                _instantiatedCharacter = Instantiate(randomGameObject, characterContainer, false);
                RectTransform rectTransform = _instantiatedCharacter.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchorMin = new Vector2(0.5f, 0);
                    rectTransform.anchorMax = new Vector2(0.5f, 1);

                    rectTransform.pivot = new Vector2(0.5f, 0.5f);

                    rectTransform.anchoredPosition = new Vector2(0, 0);

                    rectTransform.sizeDelta = new Vector2(1200.339f, 0);

                    rectTransform.localPosition = new Vector3(0, 0, 0);

                    rectTransform.localScale = new Vector3(1, 1, 1);

                    rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }

        public override void SetCharacter()
        {
            CharacterController = _instantiatedCharacter.GetComponent<CharacterController>();
        }

        public override bool HandleTouchEvent(float x, float y, AudioSource audioSource)
        {
            // Check if animations are allowed to proceed, return false if disabled
            if (AnimationFlagSingleton.Instance().GetFlag() == false)
            {
                return false;
            }

            // Get the RectTransforms of the object to detect touch within their bounds
            RectTransform decoPlantRectTransform = decoPlantContainer.GetComponent<RectTransform>();

            // Get the world corners of the plant decoration container
            Vector3[] cornersDecoBackground = new Vector3[4];
            decoPlantRectTransform.GetWorldCorners(cornersDecoBackground);
            Vector3 bottomLeftDecoBackground = cornersDecoBackground[0];
            Vector3 topRightDecoBackground = cornersDecoBackground[2];

            // Check if the touch coordinates are within the bounds of the plant decoration
            if (x >= bottomLeftDecoBackground.x && x <= topRightDecoBackground.x &&
                y >= bottomLeftDecoBackground.y && y <= topRightDecoBackground.y)
            {
                StartCoroutine(OnDecoPlant(audioSource)); // Trigger the plant interaction
                return true;
            }

            // Return false if the touch is outside the bounds
            return false;
        }

        private IEnumerator OnDecoPlant(AudioSource audioSource)
        {
            if (audioSource != null)
            {
                audioSource.clip = decoPlantAudio;
                if (audioSource.clip != null)
                {
                    audioSource.Play();
                    Image image = decoPlantPrefab.GetComponent<Image>();
                    image.sprite = animationFramesPlant[1];
                    Destroy(decoPlantContainer.transform.GetChild(0).gameObject);
                    Instantiate(decoPlantPrefab, decoPlantContainer.transform);
                    yield return new WaitForSeconds(0.5f);
                    image.sprite = animationFramesPlant[2];
                    Destroy(decoPlantContainer.transform.GetChild(0).gameObject);
                    Instantiate(decoPlantPrefab, decoPlantContainer.transform);
                    yield return new WaitForSeconds(0.5f);
                    image.sprite = animationFramesPlant[0];
                    Destroy(decoPlantContainer.transform.GetChild(0).gameObject);
                    Instantiate(decoPlantPrefab, decoPlantContainer.transform);
                }
                else
                {
                    Debug.LogError("AudioClip couldn't be found.");
                }
            }

            yield return new WaitForSeconds(0f);
        }
    }
}