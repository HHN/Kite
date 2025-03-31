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
                case 1: // Schaut Erschrocken
                {
                    ShowErschrockenAnimation();
                    // PlayRelaxedAnimation();
                    return;
                }
                case 2: // Schaut Genervt
                {
                    ShowGenervtAnimation();
                    // PlayAstonishedAnimation();
                    return;
                }
                case 3: // Schaut Unzufrieden
                {
                    ShowUnzufriedenAnimation();
                    // PlayRefusingAnimation();
                    return;
                }
                case 4: // Schaut Ablehnend
                {
                    ShowAblehnendAnimation();
                    // PlaySmileAnimation();
                    return;
                }
                case 5: // Schaut Erstaunt
                {
                    ShowErstauntAnimation();
                    // PlayFriendlyAnimation();
                    return;
                }
                case 6: // Schaut Fragend
                {
                    ShowFragendAnimation();
                    // PlayLaughingAnimation();
                    return;
                }
                case 7: // Schaut Kritisch
                {
                    ShowKritischAnimation();
                    // PlayCriticalAnimation();
                    return;
                }
                case 8: // Schaut Lächeln_Groß
                {
                    ShowLaechelnGrossAnimation();
                    // PlayNoDealAnimation();
                    return;
                }
                case 9: // Schaut Lachend
                {
                    ShowLachendAnimation();
                    // PlayHappyAnimation();
                    return;
                }
                case 10:    // Schaut Lächeln
                {
                    ShowLaechelnAnimation();
                    // PlayProudAnimation();
                    return;
                }
                case 11:    // Schaut Neutral_Entspannt
                {
                    ShowNeutralEntspanntAnimation();
                    // PlayScaredAnimation();
                    return;
                }
                case 12:    // Schaut Neutral
                {
                    ShowNeutralAnimation();
                    // PlayQuestioningAnimation();
                    return;
                }
                case 13:    // Schaut Stolz
                {
                    ShowStolzAnimation();
                    // PlayDefeatedAnimation();
                    return;
                }
                case 14: // Spricht Erschrocken
                {
                    PlayErschrockenAnimation();
                    // PlayRelaxedAnimation();
                    return;
                }
                case 15: // Spricht Genervt
                {
                    PlayGenervtAnimation();
                    // PlayAstonishedAnimation();
                    return;
                }
                case 16: // Spricht Unzufrieden
                {
                    PlayUnzufriedenAnimation();
                    // PlayRefusingAnimation();
                    return;
                }
                case 17: // Spricht Ablehnend
                {
                    PlayAblehnendAnimation();
                    // PlaySmileAnimation();
                    return;
                }
                case 18: // Spricht Erstaunt
                {
                    PlayErstauntAnimation();
                    // PlayFriendlyAnimation();
                    return;
                }
                case 19: // Spricht Fragend
                {
                    PlayFragendAnimation();
                    // PlayLaughingAnimation();
                    return;
                }
                case 20: // Spricht Kritisch
                {
                    PlayKritischAnimation();
                    // PlayCriticalAnimation();
                    return;
                }
                case 21: // Spricht Lächeln_Groß
                {
                    PlayLaechelnGrossAnimation();
                    // PlayNoDealAnimation();
                    return;
                }
                case 22: // Spricht Lachend
                {
                    PlayLachendAnimation();
                    // PlayHappyAnimation();
                    return;
                }
                case 23:    // Spricht Lächeln
                {
                    PlayLaechelnAnimation();
                    // PlayProudAnimation();
                    return;
                }
                case 24:    // Spricht Neutral_Entspannt
                {
                    PlayNeutralEntspanntAnimation();
                    // PlayScaredAnimation();
                    return;
                }
                case 25:    // Spricht Neutral
                {
                    PlayNeutralAnimation();
                    // PlayQuestioningAnimation();
                    return;
                }
                case 26:    // Spricht Stolz
                {
                    PlayStolzAnimation();
                    // PlayDefeatedAnimation();
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