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

        private GameObject _instantiatedCharacter;

        private void Start()
        {
            SetInitialSpritesForImages();
            SetInitialCharacters();
            SetCharacterController();
        }

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
            if (characterPrefabs.Count > 0)
            {
                int randomIndex = Random.Range(0, characterPrefabs.Count);
                GameObject randomGameObject = characterPrefabs[randomIndex];

                _instantiatedCharacter = Instantiate(randomGameObject, characterContainer, false);
                RectTransform rectTransform = _instantiatedCharacter.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchorMin = new Vector2(0.5f, 0);
                    rectTransform.anchorMax = new Vector2(0.5f, 1);

                    rectTransform.pivot = new Vector2(0.5f, 0.5f);

                    rectTransform.anchoredPosition = new Vector2(-61, -721);

                    rectTransform.sizeDelta = new Vector2(1766.319f, 0);

                    rectTransform.localPosition = new Vector3(-61, -720, 0);

                    rectTransform.localScale = new Vector3(1, 1, 1);

                    rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }

        private void SetCharacterController()
        {
            CharacterController = _instantiatedCharacter.GetComponent<CharacterController>();
        }

        public override void SetBackground()
        {
            NovelColorManager.Instance().SetCanvasHeight(CanvasRect.rect.height);
            NovelColorManager.Instance().SetCanvasWidth(CanvasRect.rect.width);
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

            // Get the world corners of the vase decoration container
            Vector3[] cornersDecoVase = new Vector3[4];
            decoVaseRectTransform.GetWorldCorners(cornersDecoVase);
            Vector3 bottomLeftDecoVase = cornersDecoVase[0];
            Vector3 topRightDecoVase = cornersDecoVase[2];

            // Check if the touch coordinates are within the vase decoration bounds
            if (x >= bottomLeftDecoVase.x && x <= topRightDecoVase.x &&
                y >= bottomLeftDecoVase.y && y <= topRightDecoVase.y)
            {
                StartCoroutine(OnDecoVase(audioSource));
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