using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Common.Novel.Character.CharacterController
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private Image skinImage;
        [SerializeField] private Image clotheImage;
        [SerializeField] private Image hairImage;
        [SerializeField] private Image faceImage;

        [SerializeField] private Sprite[] skinSprites;
        [SerializeField] private Sprite[] clotheSprites;
        [SerializeField] private Sprite[] hairSprites;
        [SerializeField] private Animator animator;

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
            switch (expression)
            {
                case 1: // Erschrocken
                {
                    PlayErschrockenAnimation();
                    // PlayRelaxedAnimation();
                    return;
                }
                case 2: // Genervt
                {
                    PlayGenervtAnimation();
                    // PlayAstonishedAnimation();
                    return;
                }
                case 3: // Unzufrieden
                {
                    PlayUnzufriedenAnimation();
                    // PlayRefusingAnimation();
                    return;
                }
                case 4: // Ablehnend
                {
                    PlayAblehnendAnimation();
                    // PlaySmileAnimation();
                    return;
                }
                case 5: // Erstaunt
                {
                    PlayErstauntAnimation();
                    // PlayFriendlyAnimation();
                    return;
                }
                case 6: // Fragend
                {
                    PlayFragendAnimation();
                    // PlayLaughingAnimation();
                    return;
                }
                case 7: // Kritisch
                {
                    PlayKritischAnimation();
                    // PlayCriticalAnimation();
                    return;
                }
                case 8: // Lächeln_Groß
                {
                    PlayLaechelnGrossAnimation();
                    // PlayNoDealAnimation();
                    return;
                }
                case 9: // Lachend
                {
                    PlayLachendAnimation();
                    // PlayHappyAnimation();
                    return;
                }
                case 10:    // Lächeln
                {
                    PlayLaechelnAnimation();
                    // PlayProudAnimation();
                    return;
                }
                case 11:    // Neutral_Entspannt
                {
                    PlayNeutralEntspanntAnimation();
                    // PlayScaredAnimation();
                    return;
                }
                case 12:    // Neutral
                {
                    PlayNeutralAnimation();
                    // PlayQuestioningAnimation();
                    return;
                }
                case 13:    // Stolz
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

        private void PlayErschrockenAnimation()
        {
            animator.Play("erschrocken");
        }
        
        private void PlayGenervtAnimation()
        {
            animator.Play("genervt");
        }
        
        private void PlayUnzufriedenAnimation()
        {
            animator.Play("unzufrieden");
        }
        
        private void PlayAblehnendAnimation()
        {
            animator.Play("ablehnend");
        }
        
        private void PlayErstauntAnimation()
        {
            animator.Play("erstaunt");
        }

        private void PlayFragendAnimation()
        {
            animator.Play("fragend");
        }
        
        private void PlayKritischAnimation()
        {
            animator.Play("kritisch");
        }
        
        private void PlayLaechelnGrossAnimation()
        {
            animator.Play("laecheln_gross");
        }
        
        private void PlayLachendAnimation()
        {
            animator.Play("lachend");
        }
        
        private void PlayLaechelnAnimation()
        {
            animator.Play("laecheln");
        }
        
        private void PlayNeutralEntspanntAnimation()
        {
            animator.Play("neutral_entspannt");
        }
        
        private void PlayNeutralAnimation()
        {
            animator.Play("neutral");
        }
        
        private void PlayStolzAnimation()
        {
            animator.Play("stolz");
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