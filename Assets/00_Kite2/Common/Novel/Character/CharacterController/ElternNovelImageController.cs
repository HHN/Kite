using System.Collections;
using System.Collections.Generic;
using _00_Kite2.Common.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Common.Novel.Character.CharacterController
{
    public class ElternNovelImageController : NovelImageController
    {
        [SerializeField] private GameObject backgroundPrefab;
        [SerializeField] private GameObject backgroundContainer;
        [SerializeField] private GameObject deskPrefab;
        [SerializeField] private GameObject deskContainer;
        [SerializeField] private GameObject decoTasse1Prefab;
        [SerializeField] private GameObject decoTasse1Container;
        [SerializeField] private GameObject decoTasse2Prefab;
        [SerializeField] private GameObject decoTasse2Container;
        [SerializeField] private GameObject decoKannePrefab;
        [SerializeField] private GameObject decoKanneContainer;
        [SerializeField] private GameObject decoLampePrefab;
        [SerializeField] private GameObject decoLampeContainer;
        [SerializeField] private AudioClip decoTasseAudio;
        [SerializeField] private AudioClip decoKanneAudio;
        [SerializeField] private AudioClip decoLampeOnAudio;
        [SerializeField] private AudioClip decoLampeOffAudio;
        [SerializeField] private Sprite[] animationFramesTasse1;
        [SerializeField] private Sprite[] animationFramesTasse2;
        [SerializeField] private Sprite[] animationFramesKanne;
        [SerializeField] private Sprite decoLampeOff;
        [SerializeField] private Sprite decoLampeOn;

        [SerializeField] private Transform motherCharacterContainer;
        [SerializeField] private Transform fatherCharacterContainer;
        [SerializeField] private List<GameObject> characterMutterPrefabs;

        [SerializeField] private List<GameObject> characterVaterPrefabs;
        // [SerializeField] private GameObject characterMutterPrefab;
        // [SerializeField] private GameObject characterVaterPrefab;

        private bool _decoLampeStatus;

        private GameObject _instantiatedMotherCharacter;
        private GameObject _instantiatedFatherCharacter;

        public CharacterController characterController;
        public CharacterController characterController2;

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
        //     characterController = motherCharacterContainer.GetComponentInChildren<CharacterController>();
        //     characterController2 = fatherCharacterContainer.GetComponentInChildren<CharacterController>();
        //
        //     characterController.SetSkinSprite();
        //     characterController.SetHandSprite();
        //     characterController.SetClotheSprite();
        //     characterController.SetHairSprite();
        //     
        //     characterController2.SetSkinSprite();
        //     characterController2.SetHandSprite();
        //     characterController2.SetClotheSprite();
        //     characterController2.SetHairSprite();
        //
        //     GameManager.CharacterDataList = new Dictionary<long, CharacterData>
        //     {
        //         {
        //             2, // Schlüssel für den Eintrag
        //             new CharacterData
        //             {
        //                 skinIndex = characterController.skinIndex,
        //                 handIndex = characterController.handIndex,
        //                 clotheIndex = characterController.clotheIndex,
        //                 hairIndex = characterController.hairIndex,
        //                 
        //                 skinIndex2 = characterController2.skinIndex,
        //                 handIndex2 = characterController2.handIndex,
        //                 clotheIndex2 = characterController2.clotheIndex,
        //                 hairIndex2 = characterController2.hairIndex
        //             }
        //         }
        //     };
        // }

        private void SetInitialSpritesForImages()
        {
            Image image = decoTasse1Prefab.GetComponent<Image>();
            image.sprite = animationFramesTasse1[0];
            if (decoTasse1Container.transform.childCount > 0)
            {
                Destroy(decoTasse1Container.transform.GetChild(0).gameObject);
            }

            Instantiate(decoTasse1Prefab, decoTasse1Container.transform);

            image = decoTasse2Prefab.GetComponent<Image>();
            image.sprite = animationFramesTasse2[0];
            if (decoTasse2Container.transform.childCount > 0)
            {
                Destroy(decoTasse2Container.transform.GetChild(0).gameObject);
            }

            Instantiate(decoTasse2Prefab, decoTasse2Container.transform);

            image = decoKannePrefab.GetComponent<Image>();
            image.sprite = animationFramesKanne[0];
            if (decoKanneContainer.transform.childCount > 0)
            {
                Destroy(decoKanneContainer.transform.GetChild(0).gameObject);
            }

            Instantiate(decoKannePrefab, decoKanneContainer.transform);

            image = decoLampePrefab.GetComponent<Image>();
            image.sprite = decoLampeOff;
            if (decoLampeContainer.transform.childCount > 0)
            {
                Destroy(decoLampeContainer.transform.GetChild(0).gameObject);
            }

            Instantiate(decoLampePrefab, decoLampeContainer.transform);
        }

        private void SetInitialCharacters()
        {
            if (characterMutterPrefabs.Count <= 0 || characterVaterPrefabs.Count <= 0) return;

            int randomIndexMutter = Random.Range(0, characterMutterPrefabs.Count);
            GameObject randomGameObjectMutter = characterMutterPrefabs[randomIndexMutter];

            _instantiatedMotherCharacter = Instantiate(randomGameObjectMutter, motherCharacterContainer, false);
            RectTransform rectTransformMutter = _instantiatedMotherCharacter.GetComponent<RectTransform>();
            if (rectTransformMutter != null)
            {
                rectTransformMutter.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransformMutter.anchorMax = new Vector2(0.5f, 0.5f);

                rectTransformMutter.pivot = new Vector2(0.5f, 0.5f);

                rectTransformMutter.anchoredPosition = new Vector2(-315, -597);

                rectTransformMutter.sizeDelta = new Vector2(1200.339f, 1044);

                rectTransformMutter.localPosition = new Vector3(-315, -597, 0);

                rectTransformMutter.localScale = new Vector3(1, 1, 1);

                rectTransformMutter.localRotation = Quaternion.Euler(0, 0, 0);
            }

            int randomIndexVater = Random.Range(0, characterVaterPrefabs.Count);
            GameObject randomGameObjectVater = characterVaterPrefabs[randomIndexVater];

            _instantiatedFatherCharacter = Instantiate(randomGameObjectVater, fatherCharacterContainer, false);
            RectTransform rectTransformVater = _instantiatedFatherCharacter.GetComponent<RectTransform>();

            if (rectTransformVater != null)
            {
                rectTransformVater.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransformVater.anchorMax = new Vector2(0.5f, 0.5f);

                rectTransformVater.pivot = new Vector2(0.5f, 0.5f);

                rectTransformVater.anchoredPosition = new Vector2(204, -610);

                rectTransformVater.sizeDelta = new Vector2(1200.339f, 1044);

                rectTransformVater.localPosition = new Vector3(204, -610, 0);

                rectTransformVater.localScale = new Vector3(1, 1, 1);

                rectTransformVater.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }

        public override void SetCharacter()
        {
            CharacterController = _instantiatedMotherCharacter.GetComponent<CharacterController>();
            CharacterController2 = _instantiatedFatherCharacter.GetComponent<CharacterController>();
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
            RectTransform decoTasse1RectTransform = decoTasse1Container.GetComponent<RectTransform>();
            RectTransform decoTasse2RectTransform = decoTasse2Container.GetComponent<RectTransform>();
            RectTransform decoKanneRectTransform = decoKanneContainer.GetComponent<RectTransform>();
            RectTransform decoLampeRectTransform = decoLampeContainer.GetComponent<RectTransform>();

            // Get the world corners of the tasse1 decoration container
            Vector3[] cornersDecoTasse1 = new Vector3[4];
            decoTasse1RectTransform.GetWorldCorners(cornersDecoTasse1);
            Vector3 bottomLeftDecoTasse1 = cornersDecoTasse1[0];
            Vector3 topRightDecoTasse1 = cornersDecoTasse1[2];

            // Get the world corners of the tasse2 decoration container
            Vector3[] cornersDecoTasse2 = new Vector3[4];
            decoTasse2RectTransform.GetWorldCorners(cornersDecoTasse2);
            Vector3 bottomLeftDecoTasse2 = cornersDecoTasse2[0];
            Vector3 topRightDecoTasse2 = cornersDecoTasse2[2];

            // Get the world corners of the kanne decoration container
            Vector3[] cornersDecoKanne = new Vector3[4];
            decoKanneRectTransform.GetWorldCorners(cornersDecoKanne);
            Vector3 bottomLeftDecoKanne = cornersDecoKanne[0];
            Vector3 topRightDecoKanne = cornersDecoKanne[2];

            // Get the world corners of the lampe decoration container
            CalculatePointsFromBottomRightCorner(decoLampeRectTransform, 180, 348, out var bottomLeftDecoLampe,
                out var topRightDecoLampe);

            // Check if the touch coordinates are within the tasse1 decoration bounds
            if (x >= bottomLeftDecoTasse1.x && x <= topRightDecoTasse1.x &&
                y >= bottomLeftDecoTasse1.y && y <= topRightDecoTasse1.y)
            {
                StartCoroutine(OnDecoTasse1());
                return true;
            }

            // Check if the touch coordinates are within the tasse2 decoration bounds
            if (x >= bottomLeftDecoTasse2.x && x <= topRightDecoTasse2.x &&
                y >= bottomLeftDecoTasse2.y && y <= topRightDecoTasse2.y)
            {
                StartCoroutine(OnDecoTasse2());
                return true;
            }

            // Check if the touch coordinates are within the kanne decoration bounds
            if (x >= bottomLeftDecoKanne.x && x <= topRightDecoKanne.x &&
                y >= bottomLeftDecoKanne.y && y <= topRightDecoKanne.y)
            {
                StartCoroutine(OnDecoKanne());
                return true;
            }

            // Check if the touch coordinates are within the lampe decoration bounds
            if (x >= bottomLeftDecoLampe.x && x <= topRightDecoLampe.x &&
                y >= bottomLeftDecoLampe.y && y <= topRightDecoLampe.y)
            {
                StartCoroutine(OnDecoLampe());
                return true;
            }

            // Return false if the touch is outside both bounds
            return false;
        }

        private IEnumerator OnDecoTasse1()
        {
            GlobalVolumeManager.Instance.PlaySound(decoTasseAudio);
            Image image = decoTasse1Prefab.GetComponent<Image>();
            image.sprite = animationFramesTasse1[1];
            if (decoTasse1Container.transform.childCount > 0)
            {
                Destroy(decoTasse1Container.transform.GetChild(0).gameObject);
            }

            Instantiate(decoTasse1Prefab, decoTasse1Container.transform);
            yield return new WaitForSeconds(0.5f);
            image.sprite = animationFramesTasse1[2];
            if (decoTasse1Container.transform.childCount > 0)
            {
                Destroy(decoTasse1Container.transform.GetChild(0).gameObject);
            }

            Instantiate(decoTasse1Prefab, decoTasse1Container.transform);
            yield return new WaitForSeconds(0.5f);
            image.sprite = animationFramesTasse1[3];
            if (decoTasse1Container.transform.childCount > 0)
            {
                Destroy(decoTasse1Container.transform.GetChild(0).gameObject);
            }

            Instantiate(decoTasse1Prefab, decoTasse1Container.transform);
            yield return new WaitForSeconds(0.5f);
            image.sprite = animationFramesTasse1[0];
            if (decoTasse1Container.transform.childCount > 0)
            {
                Destroy(decoTasse1Container.transform.GetChild(0).gameObject);
            }

            Instantiate(decoTasse1Prefab, decoTasse1Container.transform);
        

            yield return new WaitForSeconds(0f);
        }

        private IEnumerator OnDecoTasse2()
        {
                GlobalVolumeManager.Instance.PlaySound(decoTasseAudio);

                Image image = decoTasse2Prefab.GetComponent<Image>();
                image.sprite = animationFramesTasse2[1];
                if (decoTasse2Container.transform.childCount > 0)
                {
                    Destroy(decoTasse2Container.transform.GetChild(0).gameObject);
                }

                Instantiate(decoTasse2Prefab, decoTasse2Container.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesTasse2[2];
                if (decoTasse2Container.transform.childCount > 0)
                {
                    Destroy(decoTasse2Container.transform.GetChild(0).gameObject);
                }

                Instantiate(decoTasse2Prefab, decoTasse2Container.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesTasse2[3];
                if (decoTasse2Container.transform.childCount > 0)
                {
                    Destroy(decoTasse2Container.transform.GetChild(0).gameObject);
                }

                Instantiate(decoTasse2Prefab, decoTasse2Container.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesTasse2[0];
                if (decoTasse2Container.transform.childCount > 0)
                {
                    Destroy(decoTasse2Container.transform.GetChild(0).gameObject);
                }

                Instantiate(decoTasse2Prefab, decoTasse2Container.transform);
            

            yield return new WaitForSeconds(0f);
        }

        private IEnumerator OnDecoKanne()
        {
                GlobalVolumeManager.Instance.PlaySound(decoKanneAudio);

                Image image = decoKannePrefab.GetComponent<Image>();
                image.sprite = animationFramesKanne[1];
                if (decoKanneContainer.transform.childCount > 0)
                {
                    Destroy(decoKanneContainer.transform.GetChild(0).gameObject);
                }

                Instantiate(decoKannePrefab, decoKanneContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesKanne[2];
                if (decoKanneContainer.transform.childCount > 0)
                {
                    Destroy(decoKanneContainer.transform.GetChild(0).gameObject);
                }

                Instantiate(decoKannePrefab, decoKanneContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesKanne[3];
                if (decoKanneContainer.transform.childCount > 0)
                {
                    Destroy(decoKanneContainer.transform.GetChild(0).gameObject);
                }

                Instantiate(decoKannePrefab, decoKanneContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesKanne[0];
                if (decoKanneContainer.transform.childCount > 0)
                {
                    Destroy(decoKanneContainer.transform.GetChild(0).gameObject);
                }

                Instantiate(decoKannePrefab, decoKanneContainer.transform);

            yield return new WaitForSeconds(0f);
        }

        private IEnumerator OnDecoLampe()
        {
                AudioClip clip = _decoLampeStatus ? decoLampeOffAudio : decoLampeOnAudio;
                GlobalVolumeManager.Instance.PlaySound(clip);

                if (_decoLampeStatus)
                {
                    Image image = decoLampePrefab.GetComponent<Image>();
                    image.sprite = decoLampeOff;
                    if (decoLampeContainer.transform.childCount > 0)
                    {
                        Destroy(decoLampeContainer.transform.GetChild(0).gameObject);
                    }

                    Instantiate(decoLampePrefab, decoLampeContainer.transform);
                    _decoLampeStatus = false;
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    Image image = decoLampePrefab.GetComponent<Image>();
                    image.sprite = decoLampeOn;
                    if (decoLampeContainer.transform.childCount > 0)
                    {
                        Destroy(decoLampeContainer.transform.GetChild(0).gameObject);
                    }

                    Instantiate(decoLampePrefab, decoLampeContainer.transform);
                    _decoLampeStatus = true;
                    yield return new WaitForSeconds(0.5f);
                }

            yield return new WaitForSeconds(0f);
        }

        private void CalculatePointsFromBottomRightCorner(RectTransform rectTransform, float xOffset, float yOffset,
            out Vector3 resultingXPoint, out Vector3 resultingYPoint)
        {
            // Hol die Welt-Koordinaten der Ecken des RectTransforms
            Vector3[] worldCorners = new Vector3[4];
            rectTransform.GetWorldCorners(worldCorners);

            // Rechte untere Ecke (Index 3 im Array)
            Vector3 bottomRightCorner = worldCorners[3];

            // Berechne den Punkt mit dem xOffset
            resultingXPoint = new Vector3(bottomRightCorner.x - xOffset, bottomRightCorner.y, 0);

            // Berechne den Punkt mit dem yOffset
            resultingYPoint = new Vector3(bottomRightCorner.x, bottomRightCorner.y + yOffset, 0);
        }
    }
}