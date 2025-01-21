using System.Collections;
using System.Collections.Generic;
using _00_Kite2.Common.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Common.Novel.Character.CharacterController
{
    public class VerhandlungNovelImageController : NovelImageController
    {
        [SerializeField] private GameObject backgroundContainer;
        [SerializeField] private GameObject decoDeskContainer;
        [SerializeField] private GameObject decoKommodeContainer;
        [SerializeField] private GameObject decoVasePrefab;
        [SerializeField] private GameObject decoVaseContainer;
        [SerializeField] private AudioClip decoVaseAudio;
        [SerializeField] private Sprite[] animationFramesVase;

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
        //             11, // Schlüssel für den Eintrag
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

            rectTransform.anchoredPosition = new Vector2(-61, -721);

            rectTransform.sizeDelta = new Vector2(1766.319f, 0);

            rectTransform.localPosition = new Vector3(-61, -720, 0);

            rectTransform.localScale = new Vector3(1, 1, 1);

            rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        public override void SetCharacter()
        {
            CharacterController = _instantiatedCharacter.GetComponent<CharacterController>();
        }

        public override void SetBackground()
        {
            NovelColorManager.Instance().SetCanvasHeight(CanvasRect.rect.height);
            NovelColorManager.Instance().SetCanvasWidth(CanvasRect.rect.width);
        }

        public override bool HandleTouchEvent(float x, float y)
        {
            // Check if animations are allowed to proceed, return false if disabled
            if (AnimationFlagSingleton.Instance().GetFlag() == false)
            {
                return false;
            }

            // Get the RectTransforms of the objects to detect touch within their bounds
            RectTransform decoVaseRectTransform = decoVaseContainer.GetComponent<RectTransform>();

            // Get the world corners of the vase decoration container
            Vector3[] cornersDecoVase = new Vector3[4];
            decoVaseRectTransform.GetWorldCorners(cornersDecoVase);
            Vector3 bottomLeftDecoVase = cornersDecoVase[0];
            Vector3 topRightDecoVase = cornersDecoVase[2];

            // Check if the touch coordinates are within the vase decoration bounds
            if (x >= bottomLeftDecoVase.x && x <= topRightDecoVase.x &&
                y >= bottomLeftDecoVase.y && y <= topRightDecoVase.y)
            {
                StartCoroutine(OnDecoVase());
                return true;
            }

            // Return false if the touch is outside both bounds
            return false;
        }

        private IEnumerator OnDecoVase()
        {
            GlobalVolumeManager.Instance.PlaySound(decoVaseAudio);

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

            yield return new WaitForSeconds(0f);
        }
    }
}