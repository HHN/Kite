using UnityEngine;
using UnityEngine.UI;

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



    public void SetSkinSprite(int skinSpriteIndex)
    {
        if ((skinSprites.Length > skinSpriteIndex) && (skinSpriteIndex >= 0))
        {
            skinImage.sprite = skinSprites[skinSpriteIndex];
        }
    }

    public void SetClotheSprite(int clotheSpriteIndex)
    {
        if ((clotheSprites.Length > clotheSpriteIndex) && (clotheSpriteIndex >= 0))
        {
            clotheImage.sprite = clotheSprites[clotheSpriteIndex];
        }
    }

    public void SetHairSprite(int hairSpriteIndex)
    {
        if ((hairSprites.Length > hairSpriteIndex) && (hairSpriteIndex >= 0))
        {
            hairImage.sprite = hairSprites[hairSpriteIndex];
        }
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

    public void PlayDefeatedAnimation()
    {
        animator.Play("defeated");
    }

    public void PlayQuestioningAnimation()
    {
        animator.Play("questioning");
    }

    public void PlayScaredAnimation()
    {
        animator.Play("scared");
    }

    public void PlayProudAnimation()
    {
        animator.Play("proud");
    }

    public void PlayHappyAnimation()
    {
        animator.Play("happy");
    }

    public void PlayRelaxedAnimation()
    {
        animator.Play("relaxed");
    }

    public void PlayCriticalAnimation()
    {
        animator.Play("critical");
    }

    public void PlayFriendlyAnimation()
    {
        animator.Play("neutral");
    }

    public void PlaySmileAnimation()
    {
        animator.Play("Smile");
    }

    public void PlayNoDealAnimation()
    {
        animator.Play("no_deal");
    }

    public void PlayRefusingAnimation()
    {
        animator.Play("refusing");
    }

    public void PlayLaughingAnimation()
    {
        animator.Play("laughing");
    }

    public void PlayAstonishedAnimation()
    {
        animator.Play("surprised");
    }

    public void StartTalking()
    {
        animator.SetBool("isTalking", true);
    }

    public void StopTalking()
    {
        animator.SetBool("isTalking", false);
    }
}
