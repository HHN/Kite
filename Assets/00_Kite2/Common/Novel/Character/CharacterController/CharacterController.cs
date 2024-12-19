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
                case 1:
                {
                    PlayRelaxedAnimation();
                    return;
                }
                case 2:
                {
                    PlayAstonishedAnimation();
                    return;
                }
                case 3:
                {
                    PlayRefusingAnimation();
                    return;
                }
                case 4:
                {
                    PlaySmileAnimation();
                    return;
                }
                case 5:
                {
                    PlayFriendlyAnimation();
                    return;
                }
                case 6:
                {
                    PlayLaughingAnimation();
                    return;
                }
                case 7:
                {
                    PlayCriticalAnimation();
                    return;
                }
                case 8:
                {
                    PlayNoDealAnimation();
                    return;
                }
                case 9:
                {
                    PlayHappyAnimation();
                    return;
                }
                case 10:
                {
                    PlayProudAnimation();
                    return;
                }
                case 11:
                {
                    PlayScaredAnimation();
                    return;
                }
                case 12:
                {
                    PlayQuestioningAnimation();
                    return;
                }
                case 13:
                {
                    PlayDefeatedAnimation();
                    return;
                }
                default:
                {
                    return;
                }
            }
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