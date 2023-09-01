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

    public void SetFaceSprite(int faceSpriteIndex)
    {
        switch (faceSpriteIndex)
        {
            case 0:
                {
                    PlayRelaxedAnimation();
                    return;
                }
            case 1:
                {
                    PlaySurprisedAnimation();
                    return;
                }
            case 2:
                {
                    PlayRefusingAnimation();
                    return;
                }
            case 3:
                {
                    PlaySmileAnimation();
                    return;
                }
            case 4:
                {
                    PlayNeutralAnimation();
                    return;
                }
            case 5:
                {
                    PlayLaughingAnimation();
                    return;
                }
            case 6:
                {
                    PlayCriticalAnimation();
                    return;
                }
            case 7:
                {
                    PlayNoDealAnimation();
                    return;
                }
            default:
                {
                    return;
                }
        }
    }

    public void PlayRelaxedAnimation()
    {
        animator.Play("relaxed");
    }

    public void PlayCriticalAnimation()
    {
        animator.Play("critical");
    }

    public void PlayNeutralAnimation()
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

    public void PlaySurprisedAnimation()
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
