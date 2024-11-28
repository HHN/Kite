using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Common.Novel.Character.CharacterController
{
    public class BueroNovelImageController : NovelImageController
    {
        [SerializeField] private GameObject decoPlantPrefab;
        [SerializeField] private GameObject decoPlantContainer;
        // [SerializeField] private GameObject characterPrefab;
        [SerializeField] private AudioClip decoPlantAudio;
        [SerializeField] private Sprite[] animationFrames;

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
            image.sprite = animationFrames[0];
            if (decoPlantContainer.transform.childCount > 0)
            {
                Destroy(decoPlantContainer.transform.GetChild(0).gameObject);
            }
            Instantiate(decoPlantPrefab, decoPlantContainer.transform);
        }
        
        private void SetInitialCharacters()
        {
            int randomIndex = Random.Range(0, characterPrefabs.Count);
            GameObject randomGameObject = characterPrefabs[randomIndex];
            
            // 1. Prefab instanziieren
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

            // Get the RectTransforms of the objects to detect touch within their bounds
            RectTransform decoPlantRectTransform = decoPlantContainer.GetComponent<RectTransform>();

            // Get the world corners of the plant decoration container
            Vector3[] cornersDecoPlant = new Vector3[4];
            decoPlantRectTransform.GetWorldCorners(cornersDecoPlant);
            Vector3 bottomLeftDecoPlant = cornersDecoPlant[0];
            Vector3 topRightDecoPlant = cornersDecoPlant[2];

            // Check if the touch coordinates are within the glass decoration bounds
            if (x >= bottomLeftDecoPlant.x && x <= topRightDecoPlant.x &&
                y >= bottomLeftDecoPlant.y && y <= topRightDecoPlant.y)
            {
                StartCoroutine(OnDecoPlant(audioSource));// Trigger the plant interaction
                return true;
            }

            // Return false if the touch is outside both bounds
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
                    image.sprite = animationFrames[1];
                    Destroy(decoPlantContainer.transform.GetChild(0).gameObject);
                    Instantiate(decoPlantPrefab, decoPlantContainer.transform);
                    yield return new WaitForSeconds(0.5f);
                    image.sprite = animationFrames[2];
                    Destroy(decoPlantContainer.transform.GetChild(0).gameObject);
                    Instantiate(decoPlantPrefab, decoPlantContainer.transform);
                    yield return new WaitForSeconds(0.5f);
                    image.sprite = animationFrames[0];
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