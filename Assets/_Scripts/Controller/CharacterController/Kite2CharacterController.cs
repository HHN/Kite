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
        public int colorIndex;  // z.B. 0 für "A", 1 für "B"
        public int spriteIndex; // Der spezifische Index im Hand-Sprite-Array (z.B. handColorA[0])
    }
    
    public class Kite2CharacterController : MonoBehaviour
    {
        [SerializeField] private Image skinImage;
        [SerializeField] private Image glassImage;
        [SerializeField] private Image handImage;
        [SerializeField] private Image clotheImage;
        [SerializeField] private Image hairImage;
        [SerializeField] private Image faceImage;

        [SerializeField] private Sprite[] skinSprites;
        [SerializeField] private Sprite[] glassesSprites;
        [SerializeField] private HandSprites handSprites;
        [SerializeField] private Sprite[] clotheSprites;
        [SerializeField] private Sprite[] hairSprites;
        [SerializeField] private Animator animator;

        private static readonly int IsTalking = Animator.StringToHash("isTalking");

        public int skinIndex;
        public int glassIndex;
        public int[] handIndex;
        public int clotheIndex;
        public int hairIndex;
        
        public void SetSkinSprite()
        {
            if (skinSprites == null) return;
            
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
            if (glassesSprites == null) return;

            glassIndex = Random.Range(0, glassesSprites.Length);
            Sprite randomGlassImage = glassesSprites[glassIndex];

            glassImage.sprite = randomGlassImage;
        }
        
        public void SetHandSprite()
        {
            if (handSprites == null) return;

            if (handIndex[0] == 0)
            {
                int randomIndex = Random.Range(0, handSprites.handColorA.Length);
                handImage.sprite = handSprites.handColorA[randomIndex];
                
                handIndex[1] = randomIndex;
            }
            else if (handIndex[0] == 1)
            {
                int randomIndex = Random.Range(0, handSprites.handColorB.Length);
                handImage.sprite = handSprites.handColorB[randomIndex];
                
                handIndex[1] = randomIndex;
            }
            else if (handIndex[0] == 2)
            {
                int randomIndex = Random.Range(0, handSprites.handColorC.Length);
                handImage.sprite = handSprites.handColorC[randomIndex];
                
                handIndex[1] = randomIndex;
            }
            else if (handIndex[0] == 3)
            {
                int randomIndex = Random.Range(0, handSprites.handColorD.Length);
                handImage.sprite = handSprites.handColorD[randomIndex];
                
                handIndex[1] = randomIndex;
            }
        }

        public void SetClotheSprite()
        {
            if (clotheSprites == null) return;

            clotheIndex = Random.Range(0, clotheSprites.Length);
            Sprite randomClotheImage = clotheSprites[clotheIndex];

            clotheImage.sprite = randomClotheImage;
        }

        public void SetHairSprite()
        {
            if (hairSprites == null) return;

            hairIndex = Random.Range(0, hairSprites.Length);
            Sprite randomHairImage = hairSprites[hairIndex];

            hairImage.sprite = randomHairImage;
        }
        
        public void SetSkinSprite(int skinSpriteIndex)
        {
            if (skinSprites == null) return;
        
            skinImage.sprite = skinSprites[skinSpriteIndex];
        }
        
        public void SetGlassesSprite(int glassesSpriteIndex)
        {
            if (glassesSprites == null) return;
        
            glassImage.sprite = glassesSprites[glassesSpriteIndex];
        }
        
        public void SetHandSprite(HandSpriteIndex handSpriteIndex)
        {
            if (handSprites == null) return;
            
            if (handSpriteIndex.colorIndex == 0)
            {
                handImage.sprite = handSprites.handColorA[handSpriteIndex.spriteIndex];
            }
            else if (handSpriteIndex.colorIndex == 1)
            {
                handImage.sprite = handSprites.handColorB[handSpriteIndex.spriteIndex];
            }
            else if (handSpriteIndex.colorIndex == 2)
            {
                handImage.sprite = handSprites.handColorC[handSpriteIndex.spriteIndex];
            }
            else if (handSpriteIndex.colorIndex == 3)
            {
                handImage.sprite = handSprites.handColorD[handSpriteIndex.spriteIndex];
            }
        }

        public void SetClotheSprite(int clotheSpriteIndex)
        {
            if (clotheSprites == null) return;

            clotheImage.sprite = clotheSprites[clotheSpriteIndex];
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
                case 1:
                {
                    ShowScaredAnimation();
                    return;
                }
                case 2:
                {
                    ShowDefeatedAnimation();
                    return;
                }
                case 3:
                {
                    ShowDissatisfiedAnimation();
                    return;
                }
                case 4:
                {
                    ShowRejectingAnimation();
                    return;
                }
                case 5:
                {
                    ShowAmazedAnimation();
                    return;
                }
                case 6:
                {
                    ShowQuestioningAnimation();
                    return;
                }
                case 7:
                {
                    ShowCriticalAnimation();
                    return;
                }
                case 8:
                {
                    ShowSmilingBigAnimation();
                    return;
                }
                case 9:
                {
                    ShowLaughingAnimation();
                    return;
                }
                case 10:
                {
                    ShowSmilingAnimation();
                    return;
                }
                case 11:
                {
                    ShowNeutralRelaxedAnimation();
                    return;
                }
                case 12:
                {
                    ShowNeutralAnimation();
                    return;
                }
                case 13:
                {
                    ShowProudAnimation();
                    return;
                }
                case 14:
                {
                    PlayScaredAnimation();
                    return;
                }
                case 15:
                {
                    PlayDefeatedAnimation();
                    return;
                }
                case 16:
                {
                    PlayDissatisfiedAnimation();
                    return;
                }
                case 17:
                {
                    PlayRejectingAnimation();
                    return;
                }
                case 18:
                {
                    PlayAmazedAnimation();
                    return;
                }
                case 19:
                {
                    PlayQuestioningAnimation();
                    return;
                }
                case 20:
                {
                    PlayCriticalAnimation();
                    return;
                }
                case 21:
                {
                    PlaySmilingBigAnimation();
                    return;
                }
                case 22:
                {
                    PlayLaughingAnimation();
                    return;
                }
                case 23:
                {
                    PlaySmilingAnimation();
                    return;
                }
                case 24:
                {
                    PlayNeutralRelaxedAnimation();
                    return;
                }
                case 25:
                {
                    PlayNeutralAnimation();
                    return;
                }
                case 26:
                {
                    PlayProudAnimation();
                    return;
                }
                default:
                {
                    return;
                }
            }
        }
        
        private void ShowScaredAnimation()
        {
            animator.Play("scared");
        }
        
        private void ShowDefeatedAnimation()
        {
            animator.Play("defeated");
        }
        
        private void ShowDissatisfiedAnimation()
        {
            animator.Play("dissatisfied");
        }
        
        private void ShowRejectingAnimation()
        {
            animator.Play("rejecting");
        }
        
        private void ShowAmazedAnimation()
        {
            animator.Play("amazed");
        }

        private void ShowQuestioningAnimation()
        {
            animator.Play("questioning");
        }
        
        private void ShowCriticalAnimation()
        {
            animator.Play("critical");
        }
        
        private void ShowSmilingBigAnimation()
        {
            animator.Play("smiling_big");
        }
        
        private void ShowLaughingAnimation()
        {
            animator.Play("laughing");
        }
        
        private void ShowSmilingAnimation()
        {
            animator.Play("smiling");
        }
        
        private void ShowNeutralRelaxedAnimation()
        {
            animator.Play("neutral_relaxed");
        }
        
        private void ShowNeutralAnimation()
        {
            animator.Play("neutral");
        }
        
        private void ShowProudAnimation()
        {
            animator.Play("proud");
        }

        private void PlayScaredAnimation()
        {
            animator.Play("scared_speaking");
        }
        
        private void PlayDefeatedAnimation()
        {
            animator.Play("defeated_speaking");
        }
        
        private void PlayDissatisfiedAnimation()
        {
            animator.Play("dissatisfied_speaking");
        }
        
        private void PlayRejectingAnimation()
        {
            animator.Play("rejecting_speaking");
        }
        
        private void PlayAmazedAnimation()
        {
            animator.Play("amazed_speaking");
        }

        private void PlayQuestioningAnimation()
        {
            animator.Play("questioning_speaking");
        }
        
        private void PlayCriticalAnimation()
        {
            animator.Play("critical_speaking");
        }
        
        private void PlaySmilingBigAnimation()
        {
            animator.Play("smiling_big_speaking");
        }
        
        private void PlayLaughingAnimation()
        {
            animator.Play("laughing_speaking");
        }
        
        private void PlaySmilingAnimation()
        {
            animator.Play("smiling_speaking");
        }
        
        private void PlayNeutralRelaxedAnimation()
        {
            animator.Play("neutral_relaxed_speaking");
        }
        
        private void PlayNeutralAnimation()
        {
            animator.Play("neutral_speaking");
        }
        
        private void PlayProudAnimation()
        {
            animator.Play("proud_speaking");
        }

        public void StartTalking()
        {
            animator.SetBool(IsTalking, true);
        }

        public void StopTalking()
        {
            animator.SetBool(IsTalking, false);
        }
    }
}