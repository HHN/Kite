using System.Collections;
using Assets._Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Novel.CharacterController
{
    public class InvestorNovelImageController : NovelImageController
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
            novelKite2CharacterController.SetClotheSprite();
            novelKite2CharacterController.SetHairSprite();

            HandSpriteIndex handSpriteIndex = new HandSpriteIndex
            {
                colorIndex = novelKite2CharacterController.handIndex[0],
                spriteIndex = novelKite2CharacterController.handIndex[1],
            };
            
            foreach (var novelStatus in GameManager.Instance.NovelSaveStatusList)
            {
                // Wenn das Novel die gesuchte ID hat, setze den isSaved Wert
                int.TryParse(novelStatus.novelId, out int number);
                if (number == 9)
                {
                    if (!novelStatus.isSaved)
                    {
                        GameManager.Instance.AddCharacterData(
                            9, // Schlüssel für den Eintrag
                            new CharacterData
                            {
                                skinIndex = novelKite2CharacterController.skinIndex,
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
            if (!TextToSpeechManager.Instance.IsTextToSpeechActivated())
            {
                GlobalVolumeManager.Instance.PlaySound(decoPlantAudio);
            }
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