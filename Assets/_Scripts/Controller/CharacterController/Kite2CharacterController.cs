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
        public int colorIndex; // z.B. 0 für "A", 1 für "B"
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

        private static readonly int ScaredLooking = Animator.StringToHash("scared");
        private static readonly int DefeatedLooking = Animator.StringToHash("defeated");
        private static readonly int DissatisfiedLooking = Animator.StringToHash("dissatisfied");
        private static readonly int RejectingLooking = Animator.StringToHash("rejecting");
        private static readonly int AmazedLooking = Animator.StringToHash("amazed");
        private static readonly int QuestioningLooking = Animator.StringToHash("questioning");
        private static readonly int CriticalLooking = Animator.StringToHash("critical");
        private static readonly int SmilingBigLooking = Animator.StringToHash("smiling_big");
        private static readonly int LaughingLooking = Animator.StringToHash("laughing");
        private static readonly int SmilingLooking = Animator.StringToHash("smiling");
        private static readonly int NeutralRelaxedLooking = Animator.StringToHash("neutral_relaxed");
        private static readonly int NeutralLooking = Animator.StringToHash("neutral");
        private static readonly int ProudLooking = Animator.StringToHash("proud");
        private static readonly int ScaredSpeaking = Animator.StringToHash("scared_speaking");
        private static readonly int DefeatedSpeaking = Animator.StringToHash("defeated_speaking");
        private static readonly int DissatisfiedSpeaking = Animator.StringToHash("dissatisfied_speaking");
        private static readonly int RejectingSpeaking = Animator.StringToHash("rejecting_speaking");
        private static readonly int AmazedSpeaking = Animator.StringToHash("amazed_speaking");
        private static readonly int QuestioningSpeaking = Animator.StringToHash("questioning_speaking");
        private static readonly int CriticalSpeaking = Animator.StringToHash("critical_speaking");
        private static readonly int SmilingBigSpeaking = Animator.StringToHash("smiling_big_speaking");
        private static readonly int LaughingSpeaking = Animator.StringToHash("laughing_speaking");
        private static readonly int SmilingSpeaking = Animator.StringToHash("smiling_speaking");
        private static readonly int NeutralRelaxedSpeaking = Animator.StringToHash("neutral_relaxed_speaking");
        private static readonly int NeutralSpeaking = Animator.StringToHash("neutral_speaking");
        private static readonly int ProudSpeaking = Animator.StringToHash("proud_speaking");
        private static readonly int IsTalking = Animator.StringToHash("isTalking");

        public int skinIndex;
        public int glassIndex;
        public int[] handIndex;
        public int clotheIndex;
        public int hairIndex;

        public void SetSkinSprite()
        {
            if (skinSprites == null || skinSprites.Length == 0 || skinImage == null) return;

            handIndex = new int[2];

            skinIndex = Random.Range(0, skinSprites.Length);
            Sprite randomSkinImage = skinSprites[skinIndex];

            skinImage.sprite = randomSkinImage;

            if (randomSkinImage.name.Contains("a"))
            {
                handIndex[0] = 0;
            }
            else if (randomSkinImage.name.Contains("b"))
            {
                handIndex[0] = 1;
            }
            else if (randomSkinImage.name.Contains("c"))
            {
                handIndex[0] = 2;
            }
            else if (randomSkinImage.name.Contains("d"))
            {
                handIndex[0] = 3;
            }
        }

        public void SetGlassesSprite()
        {
            if (glassesSprites == null || glassesSprites.Length == 0) return;

            glassIndex = Random.Range(0, glassesSprites.Length);
            Sprite randomGlassImage = glassesSprites[glassIndex];

            glassImage.sprite = randomGlassImage;
        }

        public void SetHandSprite()
        {
            if (handSprites == null || handSprites.handColorA.Length == 0 || handSprites.handColorB.Length == 0 || handSprites.handColorC.Length == 0 || handSprites.handColorD.Length == 0) return;

            switch (handIndex[0])
            {
                case 0:
                {
                    int randomIndex = Random.Range(0, handSprites.handColorA.Length);
                    handImage.sprite = handSprites.handColorA[randomIndex];

                    handIndex[1] = randomIndex;
                    break;
                }
                case 1:
                {
                    int randomIndex = Random.Range(0, handSprites.handColorB.Length);
                    handImage.sprite = handSprites.handColorB[randomIndex];

                    handIndex[1] = randomIndex;
                    break;
                }
                case 2:
                {
                    int randomIndex = Random.Range(0, handSprites.handColorC.Length);
                    handImage.sprite = handSprites.handColorC[randomIndex];

                    handIndex[1] = randomIndex;
                    break;
                }
                case 3:
                {
                    int randomIndex = Random.Range(0, handSprites.handColorD.Length);
                    handImage.sprite = handSprites.handColorD[randomIndex];

                    handIndex[1] = randomIndex;
                    break;
                }
            }
        }

        public void SetClotheSprite()
        {
            if (clothesSprites == null || clothesSprites.Length == 0) return;

            clotheIndex = Random.Range(0, clothesSprites.Length);
            Sprite randomClotheImage = clothesSprites[clotheIndex];

            clothesImage.sprite = randomClotheImage;
        }

        public void SetHairSprite()
        {
            if (hairSprites == null || hairSprites.Length == 0) return;

            hairIndex = Random.Range(0, hairSprites.Length);
            Sprite randomHairImage = hairSprites[hairIndex];

            hairImage.sprite = randomHairImage;
        }

        public void SetSkinSprite(int skinSpriteIndex)
        {
            if (skinSprites == null || skinImage == null) return;

            skinImage.sprite = skinSprites[skinSpriteIndex];
        }

        public void SetGlassesSprite(int glassesSpriteIndex)
        {
            if (glassesSprites == null || glassImage == null) return;

            glassImage.sprite = glassesSprites[glassesSpriteIndex];
        }

        public void SetHandSprite(HandSpriteIndex handSpriteIndex)
        {
            if (handSprites == null || handImage == null) return;
            
            Debug.Log($"handSpriteIndex: {handSpriteIndex.colorIndex}, {handSpriteIndex.spriteIndex}");

            Sprite[] selectedHandColorArray = handSpriteIndex.colorIndex switch
            {
                0 => handSprites.handColorA,
                1 => handSprites.handColorB,
                2 => handSprites.handColorC,
                3 => handSprites.handColorD,
                _ => null
            };

            if (selectedHandColorArray == null)
            {
                Debug.LogWarning($"Invalid colorIndex {handSpriteIndex.colorIndex}");
                return;
            }

            if (handSpriteIndex.spriteIndex < 0 || handSpriteIndex.spriteIndex >= selectedHandColorArray.Length)
            {
                Debug.LogWarning($"Invalid spriteIndex {handSpriteIndex.spriteIndex} for color {handSpriteIndex.colorIndex}");
                return;
            }

            handImage.sprite = selectedHandColorArray[handSpriteIndex.spriteIndex];
        }

        public void SetClotheSprite(int clotheSpriteIndex)
        {
            if (clothesSprites == null || clothesImage == null) return;

            clothesImage.sprite = clothesSprites[clotheSpriteIndex];
        }

        public void SetHairSprite(int hairSpriteIndex)
        {
            if (hairSprites == null) return;

            hairImage.sprite = hairSprites[hairSpriteIndex];
        }

        public void SetFaceExpression(int expression)
        {
            switch (expression)
            {
                case 0:
                {
                    animator.Play(ScaredSpeaking);
                    return;
                }
                case 1:
                {
                    animator.Play(DefeatedSpeaking);
                    return;
                }
                case 2:
                {
                    animator.Play(DissatisfiedSpeaking);
                    return;
                }
                case 3:
                {
                    animator.Play(RejectingSpeaking);
                    return;
                }
                case 4:
                {
                    animator.Play(AmazedSpeaking);
                    return;
                }
                case 5:
                {
                    animator.Play(QuestioningSpeaking);
                    return;
                }
                case 6:
                {
                    animator.Play(CriticalSpeaking);
                    return;
                }
                case 7:
                {
                    animator.Play(SmilingBigSpeaking);
                    return;
                }
                case 8:
                {
                    animator.Play(LaughingSpeaking);
                    return;
                }
                case 9:
                {
                    animator.Play(SmilingSpeaking);
                    return;
                }
                case 10:
                {
                    animator.Play(NeutralRelaxedSpeaking);
                    return;
                }
                case 11:
                {
                    animator.Play(NeutralSpeaking);
                    return;
                }
                case 12:
                {
                    animator.Play(ProudSpeaking);
                    return;
                }
                case 13:
                {
                    animator.Play(ScaredLooking);
                    return;
                }
                case 14:
                {
                    animator.Play(DefeatedLooking);
                    return;
                }
                case 15:
                {
                    animator.Play(DissatisfiedLooking);
                    return;
                }
                case 16:
                {
                    animator.Play(RejectingLooking);
                    return;
                }
                case 17:
                {
                    animator.Play(AmazedLooking);
                    return;
                }
                case 18:
                {
                    animator.Play(QuestioningLooking);
                    return;
                }
                case 19:
                {
                    animator.Play(CriticalLooking);
                    return;
                }
                case 20:
                {
                    animator.Play(SmilingBigLooking);
                    return;
                }
                case 21:
                {
                    animator.Play(LaughingLooking);
                    return;
                }
                case 22:
                {
                    animator.Play(SmilingLooking);
                    return;
                }
                case 23:
                {
                    animator.Play(NeutralRelaxedLooking);
                    return;
                }
                case 24:
                {
                    animator.Play(NeutralLooking);
                    return;
                }
                case 25:
                {
                    animator.Play(ProudLooking);
                    return;
                }
                default:
                {
                    return;
                }
            }
        }

        public void StartTalking()
        {
            animator.SetBool(IsTalking, true);
        }

        public void StopTalking()
        {
            animator.SetBool(IsTalking, false);
        }
        
        private bool IsValidArray(Sprite[] array)
        {
            return array != null && array.Length > 0;
        }

        private bool IsValidImage(Image image)
        {
            return image != null;
        }

    }
}