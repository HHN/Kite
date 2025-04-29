using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets._Scripts.Controller.CharacterController
{
    [Serializable] // Ermöglicht Anzeige im Inspector
    public class HandSprites
    {
        public Sprite[] handColorA; // z.B. "a", "b", "c"
        public Sprite[] handColorB; // z.B. "a", "b", "c"
        public Sprite[] handColorC; // z.B. "a", "b", "c"
        public Sprite[] handColorD; // z.B. "a", "b", "c"
    }

    [Serializable]
    public class HandSpriteIndex
    {
        public int colorIndex; // z. B. 0 für "A", 1 für "B"
        public int spriteIndex; // Der spezifische Index im Hand-Sprite-Array (z.B. handColorA[0])
    }

    public class Kite2CharacterController : MonoBehaviour
    {
        [SerializeField] private Image skinImage;
        [SerializeField] private Image glassImage;
        [SerializeField] private Image handImage;
        [SerializeField] private Image clothesImage;
        [SerializeField] private Image hairImage;
        [SerializeField] private Image faceImage;

        [SerializeField] private Sprite[] skinSprites;
        [SerializeField] private Sprite[] glassesSprites;
        [SerializeField] private HandSprites handSprites;
        [SerializeField] private Sprite[] clothesSprites;
        [SerializeField] private Sprite[] hairSprites;
        [SerializeField] private Animator animator;

        private static readonly int IsTalking = Animator.StringToHash("isTalking");

        private static readonly int[] FaceExpressions = new int[]
        {
            Animator.StringToHash("scared_speaking"),
            Animator.StringToHash("defeated_speaking"),
            Animator.StringToHash("dissatisfied_speaking"),
            Animator.StringToHash("rejecting_speaking"),
            Animator.StringToHash("amazed_speaking"),
            Animator.StringToHash("questioning_speaking"),
            Animator.StringToHash("critical_speaking"),
            Animator.StringToHash("smiling_big_speaking"),
            Animator.StringToHash("laughing_speaking"),
            Animator.StringToHash("smiling_speaking"),
            Animator.StringToHash("neutral_relaxed_speaking"),
            Animator.StringToHash("neutral_speaking"),
            Animator.StringToHash("proud_speaking"),
            Animator.StringToHash("scared"),
            Animator.StringToHash("defeated"),
            Animator.StringToHash("dissatisfied"),
            Animator.StringToHash("rejecting"),
            Animator.StringToHash("amazed"),
            Animator.StringToHash("questioning"),
            Animator.StringToHash("critical"),
            Animator.StringToHash("smiling_big"),
            Animator.StringToHash("laughing"),
            Animator.StringToHash("smiling"),
            Animator.StringToHash("neutral_relaxed"),
            Animator.StringToHash("neutral"),
            Animator.StringToHash("proud")
        };

        public int skinIndex;
        public int glassIndex;
        public int[] handIndex;
        public int clotheIndex;
        public int hairIndex;

        public int characterId;

        // Utility methods
        private bool IsValidArray(Sprite[] array) => array != null && array.Length > 0;
        private bool IsValidImage(Image image) => image != null;

        public void SetSkinSprite()
        {
            if (!IsValidArray(skinSprites) || !IsValidImage(skinImage)) return;

            handIndex = new int[2];
            skinIndex = Random.Range(0, skinSprites.Length);
            Sprite randomSkinImage = skinSprites[skinIndex];
            skinImage.sprite = randomSkinImage;

            handIndex[0] = 
                randomSkinImage.name.Contains("_a") ? 0 :
                randomSkinImage.name.Contains("_b") ? 1 :
                randomSkinImage.name.Contains("_c") ? 2 :
                randomSkinImage.name.Contains("_d") ? 3 : 0;
        }
        
        public void SetSkinSprite(int skinSpriteIndex)
        {
            if (!IsValidArray(skinSprites) || !IsValidImage(skinImage) || skinSpriteIndex >= skinSprites.Length) return;
            skinImage.sprite = skinSprites[skinSpriteIndex];
        }

        public void SetGlassesSprite()
        {
            if (!IsValidArray(glassesSprites) || !IsValidImage(glassImage)) return;

            glassIndex = Random.Range(0, glassesSprites.Length);
            glassImage.sprite = glassesSprites[glassIndex];
        }

        public void SetGlassesSprite(int glassesSpriteIndex)
        {
            if (!IsValidArray(glassesSprites) || !IsValidImage(glassImage) || glassesSpriteIndex >= glassesSprites.Length) return;
            glassImage.sprite = glassesSprites[glassesSpriteIndex];
        }
        
        public void SetHandSprite()
        {
            if (handSprites == null || !IsValidImage(handImage)) return;

            Sprite[] selectedHandArray = handIndex[0] switch
            {
                0 => handSprites.handColorA,
                1 => handSprites.handColorB,
                2 => handSprites.handColorC,
                3 => handSprites.handColorD,
                _ => null
            };

            if (!IsValidArray(selectedHandArray)) return;

            if (selectedHandArray != null)
            {
                int randomIndex = Random.Range(0, selectedHandArray.Length);
                handImage.sprite = selectedHandArray[randomIndex];
                handIndex[1] = randomIndex;
            }
        }
        
        public void SetHandSprite(HandSpriteIndex handSpriteIndex)
        {
            if (handSprites == null || !IsValidImage(handImage)) return;

            Sprite[] selectedHandColorArray = handSpriteIndex.colorIndex switch
            {
                0 => handSprites.handColorA,
                1 => handSprites.handColorB,
                2 => handSprites.handColorC,
                3 => handSprites.handColorD,
                _ => null
            };

            if (!IsValidArray(selectedHandColorArray)) return;
            if (selectedHandColorArray != null && (handSpriteIndex.spriteIndex < 0 || handSpriteIndex.spriteIndex >= selectedHandColorArray.Length))
            {
                Debug.LogWarning($"Invalid spriteIndex {handSpriteIndex.spriteIndex} for color {handSpriteIndex.colorIndex}");
                return;
            }

            if (selectedHandColorArray != null) handImage.sprite = selectedHandColorArray[handSpriteIndex.spriteIndex];
        }


        public void SetClotheSprite()
        {
            if (!IsValidArray(clothesSprites) || !IsValidImage(clothesImage)) return;
            clotheIndex = Random.Range(0, clothesSprites.Length);
            clothesImage.sprite = clothesSprites[clotheIndex];
        }
        
        public void SetClotheSprite(int clotheSpriteIndex)
        {
            if (!IsValidArray(clothesSprites) || !IsValidImage(clothesImage) || clotheSpriteIndex >= clothesSprites.Length) return;
            clothesImage.sprite = clothesSprites[clotheSpriteIndex];
        }

        public void SetHairSprite()
        {
            if (!IsValidArray(hairSprites) || !IsValidImage(hairImage)) return;
            hairIndex = Random.Range(0, hairSprites.Length);
            hairImage.sprite = hairSprites[hairIndex];
        }

        public void SetHairSprite(int hairSpriteIndex)
        {
            if (!IsValidArray(hairSprites) || !IsValidImage(hairImage) || hairSpriteIndex >= hairSprites.Length) return;
            hairImage.sprite = hairSprites[hairSpriteIndex];
        }

        public void SetFaceExpression(int expression)
        {
            if (animator == null) return;
            if (expression < 0 || expression >= FaceExpressions.Length) return;

            animator.Play(FaceExpressions[expression]);
        }

        public void StartTalking()
        {
            animator?.SetBool(IsTalking, true);
        }

        public void StopTalking()
        {
            animator?.SetBool(IsTalking, false);
        }
    }
}