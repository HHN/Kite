using System.Collections.Generic;
using UnityEngine;

namespace _00_Kite2.Common.Novel.Character.CharacterController
{
    public class IntroNovelImageController : NovelImageController
    {
        [SerializeField] private Transform characterContainer;
        [SerializeField] private List<GameObject> characterPrefabs;
        // [SerializeField] private GameObject characterPrefab;

        private GameObject _instantiatedCharacter;

        private void Start()
        {
            SetInitialCharacters();
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
            // rectTransform.anchoredPosition = new Vector2(111, 594);
            //
            // rectTransform.sizeDelta = new Vector2(1200.339f, 0);
            //
            // rectTransform.localPosition = new Vector3(111, 594, 0);
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

            rectTransform.anchoredPosition = new Vector2(111, 594);

            rectTransform.sizeDelta = new Vector2(1200.339f, 0);

            rectTransform.localPosition = new Vector3(111, 594, 0);

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

            return false;
        }
    }
}