using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Common.Novel.Character.CharacterController
{
    public class Kite2CharacterController : MonoBehaviour
    {
        [SerializeField] private Image skinImage;
        [SerializeField] private Image glassImage;
        [SerializeField] private Image handImage;
        [SerializeField] private Image clotheImage;
        [SerializeField] private Image hairImage;
        [SerializeField] private Image faceImage;

        [SerializeField] private Sprite[] skinSprites;
        [SerializeField] private Sprite[] glassSprites;
        [SerializeField] private Sprite[] handSprites;
        [SerializeField] private Sprite[] clotheSprites;
        [SerializeField] private Sprite[] hairSprites;
        [SerializeField] private Animator animator;

        private static readonly int IsTalking = Animator.StringToHash("isTalking");

        public int skinIndex;
        public int glassIndex;
        public int handIndex;
        public int clotheIndex;
        public int hairIndex;
        
        public void SetSkinSprite( /*int skinSpriteIndex*/)
        {
            // if ((skinSprites.Length > skinSpriteIndex) && (skinSpriteIndex >= 0))
            // {
            //     skinImage.sprite = skinSprites[skinSpriteIndex];
            // }

            if (skinSprites == null) return;
        
            skinIndex = Random.Range(0, skinSprites.Length);
            Sprite randomSkinImage = skinSprites[skinIndex];
        
            skinImage.sprite = randomSkinImage;

            if (randomSkinImage.name.Contains("a"))
            {
                handIndex = 0;
            }
            else if (randomSkinImage.name.Contains("b"))
            {
                handIndex = 1;
            }
            else if (randomSkinImage.name.Contains("c"))
            {
                handIndex = 2;
            }
            else if (randomSkinImage.name.Contains("d"))
            {
                handIndex = 3;
            }
        }
        
        public void SetGlassesSprite( /*int glassesSpriteIndex*/)
        {
            // if ((skinSprites.Length > glassesSpriteIndex) && (glassesSpriteIndex >= 0))
            // {
            //     skinImage.sprite = skinSprites[glassesSpriteIndex];
            // }

            if (glassSprites == null) return;

            Sprite randomGlassImage = handSprites[glassIndex];

            handImage.sprite = randomGlassImage;
        }
        
        public void SetHandSprite( /*int handSpriteIndex*/)
        {
            // if ((skinSprites.Length > handSpriteIndex) && (handSpriteIndex >= 0))
            // {
            //     skinImage.sprite = skinSprites[handSpriteIndex];
            // }

            if (handSprites == null) return;

            Sprite randomHandImage = handSprites[handIndex];

            handImage.sprite = randomHandImage;
        }

        public void SetClotheSprite( /*int clotheSpriteIndex*/)
        {
            // if ((clotheSprites.Length > clotheSpriteIndex) && (clotheSpriteIndex >= 0))
            // {
            //     clotheImage.sprite = clotheSprites[clotheSpriteIndex];
            // }
            

            if (clotheSprites == null) return;

            clotheIndex = Random.Range(0, clotheSprites.Length);
            
            Debug.Log("clotheIndex: " + clotheIndex);
            
            Debug.Log("SetClotheSprite: " + clotheSprites[clotheIndex]);
            
            Sprite randomSkinImage = clotheSprites[clotheIndex];

            clotheImage.sprite = randomSkinImage;
        }

        public void SetHairSprite( /*int hairSpriteIndex*/)
        {
            // if ((hairSprites.Length > hairSpriteIndex) && (hairSpriteIndex >= 0))
            // {
            //     hairImage.sprite = hairSprites[hairSpriteIndex];
            // }

            if (hairSprites == null) return;

            hairIndex = Random.Range(0, hairSprites.Length);
            Sprite randomSkinImage = hairSprites[hairIndex];

            hairImage.sprite = randomSkinImage;
        }
        
        public void SetSkinSprite(int skinSpriteIndex)
        {
            if (skinSprites == null) return;
        
            skinImage.sprite = skinSprites[skinSpriteIndex];
        }
        
        public void SetHandSprite(int handSpriteIndex)
        {
            if (handSprites == null) return;

            handImage.sprite = handSprites[handSpriteIndex];
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
            animator.Play("erschrocken");
        }
        
        private void ShowGenervtAnimation()
        {
            animator.Play("genervt");
        }
        
        private void ShowUnzufriedenAnimation()
        {
            animator.Play("unzufrieden");
        }
        
        private void ShowAblehnendAnimation()
        {
            animator.Play("ablehnend");
        }
        
        private void ShowErstauntAnimation()
        {
            animator.Play("erstaunt");
        }

        private void ShowFragendAnimation()
        {
            animator.Play("fragend");
        }
        
        private void ShowKritischAnimation()
        {
            animator.Play("kritisch");
        }
        
        private void ShowLaechelnGrossAnimation()
        {
            animator.Play("laecheln_gross");
        }
        
        private void ShowLachendAnimation()
        {
            animator.Play("lachend");
        }
        
        private void ShowLaechelnAnimation()
        {
            animator.Play("laecheln");
        }
        
        private void ShowNeutralEntspanntAnimation()
        {
            animator.Play("neutral_entspannt");
        }
        
        private void ShowNeutralAnimation()
        {
            animator.Play("neutral");
        }
        
        private void ShowStolzAnimation()
        {
            animator.Play("stolz");
        }

        private void PlayErschrockenAnimation()
        {
            animator.Play("erschrocken_sprechen");
        }
        
        private void PlayGenervtAnimation()
        {
            animator.Play("genervt_sprechen");
        }
        
        private void PlayUnzufriedenAnimation()
        {
            animator.Play("unzufrieden_sprechen");
        }
        
        private void PlayAblehnendAnimation()
        {
            animator.Play("ablehnend_sprechen");
        }
        
        private void PlayErstauntAnimation()
        {
            animator.Play("erstaunt_sprechen");
        }

        private void PlayFragendAnimation()
        {
            animator.Play("fragend_sprechen");
        }
        
        private void PlayKritischAnimation()
        {
            animator.Play("kritisch_sprechen");
        }
        
        private void PlayLaechelnGrossAnimation()
        {
            animator.Play("laecheln_gross_sprechen");
        }
        
        private void PlayLachendAnimation()
        {
            animator.Play("lachend_sprechen");
        }
        
        private void PlayLaechelnAnimation()
        {
            animator.Play("laecheln_sprechen");
        }
        
        private void PlayNeutralEntspanntAnimation()
        {
            animator.Play("neutral_entspannt_sprechen");
        }
        
        private void PlayNeutralAnimation()
        {
            animator.Play("neutral_sprechen");
        }
        
        private void PlayStolzAnimation()
        {
            animator.Play("stolz_sprechen");
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