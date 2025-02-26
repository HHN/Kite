using System.Collections;
using Assets._Scripts.Common.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Common.Novel.Character.CharacterController
{
    public class NotarinNovelImageController : NovelImageController
    {
        [SerializeField] private GameObject backgroundPrefab;
        [SerializeField] private GameObject backgroundContainer;
        [SerializeField] private GameObject deskPrefab;
        [SerializeField] private GameObject deskContainer;
        [SerializeField] private GameObject decoGlasPrefab;
        [SerializeField] private GameObject decoGlasContainer;
        [SerializeField] private GameObject decoPlantPrefab;
        [SerializeField] private GameObject decoPlantContainer;
        [SerializeField] private AudioClip decoGlasAudio;
        [SerializeField] private AudioClip decoPlantAudio;
        [SerializeField] private Sprite[] animationFramesPlant;
        [SerializeField] private Sprite[] animationFramesGlas;

        [SerializeField] private Transform characterContainer;
        [SerializeField] private GameObject characterPrefab;

        private GameObject _instantiatedCharacter;

        private void Start()
        {
            novelKite2CharacterController = characterContainer.GetComponentInChildren<Kite2CharacterController>();

            novelKite2CharacterController.SetSkinSprite();
            novelKite2CharacterController.SetClotheSprite();
            novelKite2CharacterController.SetHairSprite();

            GameManager.Instance.AddCharacterData(
                4, // Schlüssel für den Eintrag
                new CharacterData
                {
                    skinIndex = novelKite2CharacterController.skinIndex,
                    clotheIndex = novelKite2CharacterController.clotheIndex,
                    hairIndex = novelKite2CharacterController.hairIndex
                }
            );
        }

        private void SetInitialSpritesForImages()
        {
            Image image = decoGlasPrefab.GetComponent<Image>();
            image.sprite = animationFramesGlas[0];
            if (decoGlasContainer.transform.childCount > 0)
            {
                Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
            }

            Instantiate(decoGlasPrefab, decoGlasContainer.transform);

            image = decoPlantPrefab.GetComponent<Image>();
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
            // novelKite2CharacterController = _instantiatedCharacter.GetComponent<Kite2CharacterController>();
        }

        public override void DestroyCharacter()
        {
            if (_instantiatedCharacter == null)
            {
                return;
            }

            Destroy(_instantiatedCharacter);
        }

        public override bool HandleTouchEvent(float x, float y)
        {
            // Check if animations are allowed to proceed, return false if disabled
            if (AnimationFlagSingleton.Instance().GetFlag() == false)
            {
                return false;
            }

            // Get the RectTransforms of the objects to detect touch within their bounds
            RectTransform decoGlasRectTransform = decoGlasContainer.GetComponent<RectTransform>();
            RectTransform decoPlantRectTransform = decoPlantContainer.GetComponent<RectTransform>();

            // Get the world corners of the glass decoration container
            Vector3[] cornersDecoDesk = new Vector3[4];
            decoGlasRectTransform.GetWorldCorners(cornersDecoDesk);
            Vector3 bottomLeftDecoDesk = cornersDecoDesk[0];
            Vector3 topRightDecoDesk = cornersDecoDesk[2];

            // Get the world corners of the plant decoration container
            Vector3[] cornersDecoBackground = new Vector3[4];
            decoPlantRectTransform.GetWorldCorners(cornersDecoBackground);
            Vector3 bottomLeftDecoBackground = cornersDecoBackground[0];
            Vector3 topRightDecoBackground = cornersDecoBackground[2];

            // Check if the touch coordinates are within the glass decoration bounds
            if (x >= bottomLeftDecoDesk.x && x <= topRightDecoDesk.x &&
                y >= bottomLeftDecoDesk.y && y <= topRightDecoDesk.y)
            {
                StartCoroutine(OnDecoGlas());
                return true;
            }
            // Check if the touch coordinates are within the plant decoration bounds
            else if (x >= bottomLeftDecoBackground.x && x <= topRightDecoBackground.x &&
                     y >= bottomLeftDecoBackground.y && y <= topRightDecoBackground.y)
            {
                StartCoroutine(OnDecoPlant());
                return true;
            }

            // Return false if the touch is outside both bounds
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

        private IEnumerator OnDecoGlas()
        {
            GlobalVolumeManager.Instance.PlaySound(decoGlasAudio);


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

            yield return new WaitForSeconds(0f);
        }
    }
}