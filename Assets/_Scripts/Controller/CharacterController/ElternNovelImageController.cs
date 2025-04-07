using System.Collections;
using Assets._Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.CharacterController
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
        [SerializeField] private GameObject characterMutterPrefab;
        [SerializeField] private GameObject characterVaterPrefab;

        private bool _decoLampeStatus;

        private GameObject _instantiatedMotherCharacter;
        private GameObject _instantiatedFatherCharacter;

        private void Start()
        {
            novelKite2CharacterController = motherCharacterContainer.GetComponentInChildren<Kite2CharacterController>();
            novelKite2CharacterController2 =
                fatherCharacterContainer.GetComponentInChildren<Kite2CharacterController>();

            novelKite2CharacterController.SetSkinSprite();
            novelKite2CharacterController.SetHandSprite();
            novelKite2CharacterController.SetClotheSprite();
            novelKite2CharacterController.SetHairSprite();

            novelKite2CharacterController2.SetSkinSprite();
            novelKite2CharacterController2.SetClotheSprite();
            novelKite2CharacterController2.SetHairSprite();
            novelKite2CharacterController2.SetGlassesSprite();

            HandSpriteIndex handSpriteIndex = new HandSpriteIndex
            {
                colorIndex = novelKite2CharacterController.handIndex[0],
                spriteIndex = novelKite2CharacterController.handIndex[1],
            };
            
            foreach (var novelStatus in GameManager.Instance.NovelSaveStatusList)
            {
                // Wenn das Novel die gesuchte ID hat, setze den isSaved Wert
                int.TryParse(novelStatus.novelId, out int number);
                if (number == 2)
                {
                    if (!novelStatus.isSaved)
                    {
                        GameManager.Instance.AddCharacterData(
                            2, // Schlüssel für den Eintrag
                            new CharacterData
                            {
                                skinIndex = novelKite2CharacterController.skinIndex,
                                handIndex = handSpriteIndex,
                                clotheIndex = novelKite2CharacterController.clotheIndex,
                                hairIndex = novelKite2CharacterController.hairIndex,

                                skinIndex2 = novelKite2CharacterController2.skinIndex,
                                clotheIndex2 = novelKite2CharacterController2.clotheIndex,
                                hairIndex2 = novelKite2CharacterController2.hairIndex,
                                glassIndex2 = novelKite2CharacterController2.glassIndex
                            }
                        );
                    }

                    break; // Keine Notwendigkeit mehr weiterzusuchen
                }
            }
        }

        public override void SetCharacter()
        {
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
            if (!TextToSpeechManager.Instance.IsTextToSpeechActivated())
            {
                GlobalVolumeManager.Instance.PlaySound(decoTasseAudio);
            }
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
            if (!TextToSpeechManager.Instance.IsTextToSpeechActivated())
            {
                GlobalVolumeManager.Instance.PlaySound(decoTasseAudio);
            }

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
            if (!TextToSpeechManager.Instance.IsTextToSpeechActivated())
            {
                GlobalVolumeManager.Instance.PlaySound(decoKanneAudio);
            }

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
            if (!TextToSpeechManager.Instance.IsTextToSpeechActivated())
            {
                GlobalVolumeManager.Instance.PlaySound(clip);
            }

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