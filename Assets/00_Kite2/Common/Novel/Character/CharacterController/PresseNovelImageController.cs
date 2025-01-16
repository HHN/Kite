using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Common.Novel.Character.CharacterController
{
    public class PresseNovelImageController : NovelImageController
    {
        [SerializeField] private GameObject decoVasePrefab;
        [SerializeField] private GameObject decoVaseContainer;
        [SerializeField] private GameObject decoGlasPrefab;
        [SerializeField] private GameObject decoGlasContainer;
        [SerializeField] private AudioClip decoGlasAudio;
        [SerializeField] private AudioClip decoVaseAudio;
        [SerializeField] private Sprite[] animationFramesVase;
        [SerializeField] private Sprite[] animationFramesGlas;

        [SerializeField] private Transform characterContainer;
        [SerializeField] private List<GameObject> characterPrefabs;
        // [SerializeField] private GameObject characterPrefab;

        private GameObject _instantiatedCharacter;

        public CharacterController characterController;

        private void Start()
        {
            SetInitialSpritesForImages();
            SetInitialCharacters();
        }

        // private void Start()
        // {
        //     // SetInitialSpritesForImages();
        //     // SetInitialCharacters();
        //     
        //     characterController = characterContainer.GetComponentInChildren<CharacterController>();
        //
        //     characterController.SetSkinSprite();
        //     characterController.SetHandSprite();
        //     characterController.SetClotheSprite();
        //     characterController.SetHairSprite();
        //
        //     GameManager.CharacterDataList = new Dictionary<long, CharacterData>
        //     {
        //         {
        //             3, // Schlüssel für den Eintrag
        //             new CharacterData
        //             {
        //                 skinIndex = characterController.skinIndex,
        //                 handIndex = characterController.handIndex,
        //                 clotheIndex = characterController.clotheIndex,
        //                 hairIndex = characterController.hairIndex
        //             }
        //         }
        //     };
        // }

        private void SetInitialSpritesForImages()
        {
            Image image = decoVasePrefab.GetComponent<Image>();
            image.sprite = animationFramesVase[0];
            if (decoVaseContainer.transform.childCount > 0)
            {
                Destroy(decoVaseContainer.transform.GetChild(0).gameObject);
            }

            Instantiate(decoVasePrefab, decoVaseContainer.transform);

            image = decoGlasPrefab.GetComponent<Image>();
            image.sprite = animationFramesGlas[0];
            if (decoGlasContainer.transform.childCount > 0)
            {
                Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
            }

            Instantiate(decoGlasPrefab, decoGlasContainer.transform);
        }

        private void SetInitialCharacters()
        {
            // if (characterPrefab == null) return;
            //
            // _instantiatedCharacter = Instantiate(characterPrefab, characterContainer, false);
            // RectTransform rectTransform = _instantiatedCharacter.GetComponent<RectTransform>();
            //
            // if (rectTransform == null) return;
            //
            // rectTransform.anchorMin = new Vector2(0.5f, 0);
            // rectTransform.anchorMax = new Vector2(0.5f, 1);
            //
            // rectTransform.pivot = new Vector2(0.5f, 0.5f);
            //
            // rectTransform.anchoredPosition = new Vector2(0, 0);
            //
            // rectTransform.sizeDelta = new Vector2(1200.339f, 0);
            //
            // rectTransform.localPosition = new Vector3(0, 0, 0);
            //
            // rectTransform.localScale = new Vector3(1, 1, 1);
            //
            // rectTransform.localRotation = Quaternion.Euler(0, 0, 0);

            if (characterPrefabs.Count <= 0) return;

            int randomIndex = Random.Range(0, characterPrefabs.Count);
            GameObject randomGameObject = characterPrefabs[randomIndex];

            _instantiatedCharacter = Instantiate(randomGameObject, characterContainer, false);
            RectTransform rectTransform = _instantiatedCharacter.GetComponent<RectTransform>();

            if (rectTransform == null) return;

            rectTransform.anchorMin = new Vector2(0.5f, 0);
            rectTransform.anchorMax = new Vector2(0.5f, 1);

            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            rectTransform.anchoredPosition = new Vector2(0, 0);

            rectTransform.sizeDelta = new Vector2(1200.339f, 0);

            rectTransform.localPosition = new Vector3(0, 0, 0);

            rectTransform.localScale = new Vector3(1, 1, 1);

            rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        public override void SetCharacter()
        {
            CharacterController = _instantiatedCharacter.GetComponent<CharacterController>();

            // CharacterController.SetSkinSprite();
            // CharacterController.SetClotheSprite();
            // CharacterController.SetHairSprite();
        }

        public override bool HandleTouchEvent(float x, float y, AudioSource audioSource)
        {
            // Check if animations are allowed to proceed, return false if disabled
            if (AnimationFlagSingleton.Instance().GetFlag() == false)
            {
                return false;
            }

            // Get the RectTransforms of the objects to detect touch within their bounds
            RectTransform decoVaseRectTransform = decoVaseContainer.GetComponent<RectTransform>();
            RectTransform decoGlasRectTransform = decoGlasContainer.GetComponent<RectTransform>();

            // Get the world corners of the vase decoration container
            Vector3[] cornersDecoVase = new Vector3[4];
            decoVaseRectTransform.GetWorldCorners(cornersDecoVase);
            Vector3 bottomLeftDecoVase = cornersDecoVase[0];
            Vector3 topRightDecoVase = cornersDecoVase[2];

            // Get the world corners of the glass decoration container
            Vector3[] cornersDecoGlas = new Vector3[4];
            decoGlasRectTransform.GetWorldCorners(cornersDecoGlas);
            Vector3 bottomLeftDecoGlas = cornersDecoGlas[0];
            Vector3 topRightDecoGlas = cornersDecoGlas[2];

            // Check if the touch coordinates are within the vase decoration bounds
            if (x >= bottomLeftDecoVase.x && x <= topRightDecoVase.x &&
                y >= bottomLeftDecoVase.y && y <= topRightDecoVase.y)
            {
                StartCoroutine(OnDecoVase(audioSource));
                return true;
            }
            // Check if the touch coordinates are within the glass decoration bounds
            else if (x >= bottomLeftDecoGlas.x && x <= topRightDecoGlas.x &&
                     y >= bottomLeftDecoGlas.y && y <= topRightDecoGlas.y)
            {
                StartCoroutine(OnDecoGlas(audioSource));
                return true;
            }

            // Return false if the touch is outside both bounds
            return false;
        }

        private IEnumerator OnDecoVase(AudioSource audioSource)
        {
            if (audioSource != null)
            {
                audioSource.clip = decoVaseAudio;
                if (audioSource.clip != null)
                {
                    audioSource.Play();
                    Image image = decoVasePrefab.GetComponent<Image>();
                    image.sprite = animationFramesVase[1];
                    Destroy(decoVaseContainer.transform.GetChild(0).gameObject);
                    Instantiate(decoVasePrefab, decoVaseContainer.transform);
                    yield return new WaitForSeconds(0.5f);
                    image.sprite = animationFramesVase[2];
                    Destroy(decoVaseContainer.transform.GetChild(0).gameObject);
                    Instantiate(decoVasePrefab, decoVaseContainer.transform);
                    yield return new WaitForSeconds(0.5f);
                    image.sprite = animationFramesVase[0];
                    Destroy(decoVaseContainer.transform.GetChild(0).gameObject);
                    Instantiate(decoVasePrefab, decoVaseContainer.transform);
                }
                else
                {
                    Debug.LogError("AudioClip couldn't be found.");
                }
            }

            yield return new WaitForSeconds(0f);
        }

        private IEnumerator OnDecoGlas(AudioSource audioSource)
        {
            if (audioSource != null)
            {
                audioSource.clip = decoGlasAudio;
                if (audioSource.clip != null)
                {
                    audioSource.Play();
                    Image image = decoGlasPrefab.GetComponent<Image>();
                    image.sprite = animationFramesGlas[1];
                    Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
                    Instantiate(decoGlasPrefab, decoGlasContainer.transform);
                    yield return new WaitForSeconds(0.5f);
                    image.sprite = animationFramesGlas[2];
                    Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
                    Instantiate(decoGlasPrefab, decoGlasContainer.transform);
                    yield return new WaitForSeconds(0.5f);
                    image.sprite = animationFramesGlas[3];
                    Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
                    Instantiate(decoGlasPrefab, decoGlasContainer.transform);
                    yield return new WaitForSeconds(0.5f);
                    image.sprite = animationFramesGlas[4];
                    Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
                    Instantiate(decoGlasPrefab, decoGlasContainer.transform);
                    yield return new WaitForSeconds(0.5f);
                    image.sprite = animationFramesGlas[5];
                    Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
                    Instantiate(decoGlasPrefab, decoGlasContainer.transform);
                    yield return new WaitForSeconds(0.5f);
                    image.sprite = animationFramesGlas[6];
                    Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
                    Instantiate(decoGlasPrefab, decoGlasContainer.transform);
                    yield return new WaitForSeconds(0.5f);
                    image.sprite = animationFramesGlas[0];
                    Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
                    Instantiate(decoGlasPrefab, decoGlasContainer.transform);
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