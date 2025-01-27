using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _00_Kite2.Common.Novel.Character.CharacterController
{
    public class BueroNovelImageController : NovelImageController
    {
        [SerializeField] private GameObject decoPlantPrefab;
        [SerializeField] private GameObject decoPlantContainer;
        [SerializeField] private AudioClip decoPlantAudio;
        [SerializeField] private Sprite[] animationFrames;

        [SerializeField] private Transform characterContainer;
        [SerializeField] private List<GameObject> characterPrefabs;
        // [SerializeField] private GameObject characterPrefab;

        private GameObject _instantiatedCharacter;

        [FormerlySerializedAs("characterController")] public Kite2CharacterController kite2CharacterController;

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
        //     novelKite2CharacterController = characterContainer.GetComponentInChildren<Kite2CharacterController>();
        //
        //     novelKite2CharacterController.SetSkinSprite();
        //     novelKite2CharacterController.SetHandSprite();
        //     novelKite2CharacterController.SetClotheSprite();
        //     novelKite2CharacterController.SetHairSprite();
        //
        //     GameManager.CharacterDataList = new Dictionary<long, CharacterData>
        //     {
        //         {
        //             6, // Schlüssel für den Eintrag
        //             new CharacterData
        //             {
        //                 skinIndex = novelKite2CharacterController.skinIndex,
        //                 handIndex = novelKite2CharacterController.handIndex,
        //                 clotheIndex = novelKite2CharacterController.clotheIndex,
        //                 hairIndex = novelKite2CharacterController.hairIndex
        //             }
        //         }
        //     };
        // }

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
            base.novelKite2CharacterController = _instantiatedCharacter.GetComponent<Kite2CharacterController>();
        }

        public override bool HandleTouchEvent(float x, float y)
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
                StartCoroutine(OnDecoPlant()); // Trigger the plant interaction
                return true;
            }

            // Return false if the touch is outside both bounds
            return false;
        }

        private IEnumerator OnDecoPlant()
        {
            GlobalVolumeManager.Instance.PlaySound(decoPlantAudio);
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
            yield return new WaitForSeconds(0f);
        }
    }
}