using System.Collections;
using Assets._Scripts.Common.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Common.Novel.CharacterController
{
    public class BekannterNovelImageController : NovelImageController
    {
        [SerializeField] private GameObject decoPlantPrefab;
        [SerializeField] private GameObject decoPlantContainer;
        [SerializeField] private AudioClip decoPlantAudio;
        [SerializeField] private Sprite[] animationFramesPlant;

        [SerializeField] private Transform characterContainer;
        [SerializeField] private GameObject characterPrefab;

        private GameObject _instantiatedCharacter;

        private void Start()
        {
            novelKite2CharacterController = characterContainer.GetComponentInChildren<Kite2CharacterController>();

            novelKite2CharacterController.SetSkinSprite();
            // novelKite2CharacterController.SetHandSprite();
            novelKite2CharacterController.SetClotheSprite();
            novelKite2CharacterController.SetHairSprite();

            HandSpriteIndex handSpriteIndex = new HandSpriteIndex
            {
                colorIndex = novelKite2CharacterController.handIndex[0],
                spriteIndex = novelKite2CharacterController.handIndex[1],
            };

            GameManager.Instance.AddCharacterData(
                9, // Schlüssel für den Eintrag
                new CharacterData
                {
                    skinIndex = novelKite2CharacterController.skinIndex,
                    // handIndex = handSpriteIndex,
                    clotheIndex = novelKite2CharacterController.clotheIndex,
                    hairIndex = novelKite2CharacterController.hairIndex
                }
            );
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
            if (characterPrefab == null) return;

            _instantiatedCharacter = Instantiate(characterPrefab, characterContainer, false);
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
            // base.novelKite2CharacterController = _instantiatedCharacter.GetComponent<Kite2CharacterController>();
        }

        public override bool HandleTouchEvent(float x, float y)
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
                StartCoroutine(OnDecoPlant()); // Trigger the plant interaction
                return true;
            }

            // Return false if the touch is outside the bounds
            return false;
        }

        private IEnumerator OnDecoPlant()
        {
            GlobalVolumeManager.Instance.PlaySound(decoPlantAudio);
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
            yield return new WaitForSeconds(0f);
        }
    }
}