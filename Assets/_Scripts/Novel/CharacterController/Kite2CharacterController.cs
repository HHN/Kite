using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets._Scripts.Novel.CharacterController
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

            handImage.sprite = handSpriteIndex.colorIndex switch
            {
                0 => handSprites.handColorA[handSpriteIndex.spriteIndex],
                1 => handSprites.handColorB[handSpriteIndex.spriteIndex],
                2 => handSprites.handColorC[handSpriteIndex.spriteIndex],
                3 => handSprites.handColorD[handSpriteIndex.spriteIndex],
                _ => handImage.sprite
            };
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
                    ShowErschrockenAnimation();
                    return;
                }
                case 2:
                {
                    ShowGenervtAnimation();
                    return;
                }
                case 3:
                {
                    ShowUnzufriedenAnimation();
                    return;
                }
                case 4:
                {
                    ShowAblehnendAnimation();
                    return;
                }
                case 5:
                {
                    ShowErstauntAnimation();
                    return;
                }
                case 6:
                {
                    ShowFragendAnimation();
                    return;
                }
                case 7:
                {
                    ShowKritischAnimation();
                    return;
                }
                case 8:
                {
                    ShowLaechelnGrossAnimation();
                    return;
                }
                case 9:
                {
                    ShowLachendAnimation();
                    return;
                }
                case 10:
                {
                    ShowLaechelnAnimation();
                    return;
                }
                case 11:
                {
                    ShowNeutralEntspanntAnimation();
                    return;
                }
                case 12:
                {
                    ShowNeutralAnimation();
                    return;
                }
                case 13:
                {
                    ShowStolzAnimation();
                    return;
                }
                case 14:
                {
                    PlayErschrockenAnimation();
                    return;
                }
                case 15:
                {
                    PlayGenervtAnimation();
                    return;
                }
                case 16:
                {
                    PlayUnzufriedenAnimation();
                    return;
                }
                case 17:
                {
                    PlayAblehnendAnimation();
                    return;
                }
                case 18:
                {
                    PlayErstauntAnimation();
                    return;
                }
                case 19:
                {
                    PlayFragendAnimation();
                    return;
                }
                case 20:
                {
                    PlayKritischAnimation();
                    return;
                }
                case 21:
                {
                    PlayLaechelnGrossAnimation();
                    return;
                }
                case 22:
                {
                    PlayLachendAnimation();
                    return;
                }
                case 23:
                {
                    PlayLaechelnAnimation();
                    return;
                }
                case 24:
                {
                    PlayNeutralEntspanntAnimation();
                    return;
                }
                case 25:
                {
                    PlayNeutralAnimation();
                    return;
                }
                case 26:
                {
                    PlayStolzAnimation();
                    return;
                }
                default:
                {
                    return;
                }
            }
        }
        
        private void ShowErschrockenAnimation()
        {
            animator.Play("scared");
        }
        
        private void ShowGenervtAnimation()
        {
            animator.Play("defeated");
        }
        
        private void ShowUnzufriedenAnimation()
        {
            animator.Play("dissatisfied");
        }
        
        private void ShowAblehnendAnimation()
        {
            animator.Play("rejecting");
        }
        
        private void ShowErstauntAnimation()
        {
            animator.Play("amazed");
        }

        private void ShowFragendAnimation()
        {
            animator.Play("questioning");
        }
        
        private void ShowKritischAnimation()
        {
            animator.Play("critical");
        }
        
        private void ShowLaechelnGrossAnimation()
        {
            animator.Play("smiling_big");
        }
        
        private void ShowLachendAnimation()
        {
            animator.Play("laughing");
        }
        
        private void ShowLaechelnAnimation()
        {
            animator.Play("smiling");
        }
        
        private void ShowNeutralEntspanntAnimation()
        {
            animator.Play("neutral_relaxed");
        }
        
        private void ShowNeutralAnimation()
        {
            animator.Play("neutral");
        }
        
        private void ShowStolzAnimation()
        {
            animator.Play("proud");
        }

        private void PlayErschrockenAnimation()
        {
            animator.Play("scared_speaking");
        }
        
        private void PlayGenervtAnimation()
        {
            animator.Play("defeated_speaking");
        }
        
        private void PlayUnzufriedenAnimation()
        {
            animator.Play("dissatisfied_speaking");
        }
        
        private void PlayAblehnendAnimation()
        {
            animator.Play("rejecting_speaking");
        }
        
        private void PlayErstauntAnimation()
        {
            animator.Play("amazed_speaking");
        }

        private void PlayFragendAnimation()
        {
            animator.Play("questioning_speaking");
        }
        
        private void PlayKritischAnimation()
        {
            animator.Play("critical_speaking");
        }
        
        private void PlayLaechelnGrossAnimation()
        {
            animator.Play("smiling_big_speaking");
        }
        
        private void PlayLachendAnimation()
        {
            animator.Play("laughing_speaking");
        }
        
        private void PlayLaechelnAnimation()
        {
            animator.Play("smiling_speaking");
        }
        
        private void PlayNeutralEntspanntAnimation()
        {
            animator.Play("neutral_relaxed_speaking");
        }
        
        private void PlayNeutralAnimation()
        {
            animator.Play("neutral_speaking");
        }
        
        private void PlayStolzAnimation()
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