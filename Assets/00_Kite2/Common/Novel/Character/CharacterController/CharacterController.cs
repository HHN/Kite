using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Common.Novel.Character.CharacterController
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private Image skinImage;
        [SerializeField] private Image handImage;
        [SerializeField] private Image clotheImage;
        [SerializeField] private Image hairImage;
        [SerializeField] private Image faceImage;

        [SerializeField] private Sprite[] skinSprites;
        [SerializeField] private Sprite[] handSprites;
        [SerializeField] private Sprite[] clotheSprites;
        [SerializeField] private Sprite[] hairSprites;
        [SerializeField] private Animator animator;

        private int _skinColour;

        private static readonly int IsTalking = Animator.StringToHash("isTalking");

        public void SetSkinSprite( /*int skinSpriteIndex*/)
        {
            // if ((skinSprites.Length > skinSpriteIndex) && (skinSpriteIndex >= 0))
            // {
            //     skinImage.sprite = skinSprites[skinSpriteIndex];
            // }

            if (skinSprites == null) return;
        
            int randomIndex = Random.Range(0, skinSprites.Length);
            Sprite randomSkinImage = skinSprites[randomIndex];
        
            skinImage.sprite = randomSkinImage;

            if (randomSkinImage.name.Contains("a"))
            {
                _skinColour = 2;
            }
            else if (randomSkinImage.name.Contains("b"))
            {
                _skinColour = 1;
            }
            else if (randomSkinImage.name.Contains("c"))
            {
                _skinColour = 0;
            }
            else if (randomSkinImage.name.Contains("d"))
            {
                _skinColour = 3;
            }
        }
        
        public void SetHandSprite( /*int handSpriteIndex*/)
        {
            // if ((skinSprites.Length > handSpriteIndex) && (handSpriteIndex >= 0))
            // {
            //     skinImage.sprite = skinSprites[handSpriteIndex];
            // }

            if (handSprites == null) return;

            Sprite randomHandImage = handSprites[_skinColour];

            handImage.sprite = randomHandImage;
        }

        public void SetClotheSprite( /*int clotheSpriteIndex*/)
        {
            // if ((clotheSprites.Length > clotheSpriteIndex) && (clotheSpriteIndex >= 0))
            // {
            //     clotheImage.sprite = clotheSprites[clotheSpriteIndex];
            // }

            if (clotheSprites == null) return;

            int randomIndex = Random.Range(0, clotheSprites.Length);
            Sprite randomSkinImage = clotheSprites[randomIndex];

            clotheImage.sprite = randomSkinImage;
        }

        public void SetHairSprite( /*int hairSpriteIndex*/)
        {
            // if ((hairSprites.Length > hairSpriteIndex) && (hairSpriteIndex >= 0))
            // {
            //     hairImage.sprite = hairSprites[hairSpriteIndex];
            // }

            if (hairSprites == null) return;

            int randomIndex = Random.Range(0, hairSprites.Length);
            Sprite randomSkinImage = hairSprites[randomIndex];

            hairImage.sprite = randomSkinImage;
        }

        public void SetFaceExpression(int expression)
        {
            Debug.Log(expression);
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
                    ShowErstauntAnimation();
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

        private void PlayDefeatedAnimation()
        {
            animator.Play("defeated");
        }

        private void PlayQuestioningAnimation()
        {
            animator.Play("questioning");
        }

        private void PlayScaredAnimation()
        {
            animator.Play("scared");
        }

        private void PlayProudAnimation()
        {
            animator.Play("proud");
        }

        private void PlayHappyAnimation()
        {
            animator.Play("happy");
        }

        private void PlayRelaxedAnimation()
        {
            animator.Play("relaxed");
        }

        private void PlayCriticalAnimation()
        {
            animator.Play("critical");
        }

        private void PlayFriendlyAnimation()
        {
            animator.Play("neutral");
        }

        private void PlaySmileAnimation()
        {
            animator.Play("Smile");
        }

        private void PlayNoDealAnimation()
        {
            animator.Play("no_deal");
        }

        private void PlayRefusingAnimation()
        {
            animator.Play("refusing");
        }

        private void PlayLaughingAnimation()
        {
            animator.Play("laughing");
        }

        private void PlayAstonishedAnimation()
        {
            animator.Play("surprised");
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