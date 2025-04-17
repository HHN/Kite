using System.Collections;
using Assets._Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.CharacterController
{
    public class HonorarNovelImageController : NovelImageController
    {
        [SerializeField] private GameObject decoVasePrefab;
        [SerializeField] private GameObject decoVaseContainer;
        [SerializeField] private AudioClip decoVaseAudio;
        [SerializeField] private Sprite[] animationFramesVase;

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
            
            foreach (var novelStatus in GameManager.Instance.NovelSaveStatusList)
            {
                // Wenn das Novel die gesuchte ID hat, setze den isSaved Wert
                int.TryParse(novelStatus.novelId, out int number);
                if (number == 11)
                {
                    if (!novelStatus.isSaved)
                    {
                        GameManager.Instance.AddCharacterData(
                            11, // Schlüssel für den Eintrag
                            new CharacterData
                            {
                                skinIndex = novelKite2CharacterController.skinIndex,
                                // handIndex = novelKite2CharacterController.handIndex,
                                clotheIndex = novelKite2CharacterController.clotheIndex,
                                hairIndex = novelKite2CharacterController.hairIndex
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
            if (!TextToSpeechManager.Instance.IsTextToSpeechActivated())
            {
                GlobalVolumeManager.Instance.PlaySound(decoVaseAudio);
            }

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