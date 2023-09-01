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
    [SerializeField] private Sprite[] faceSprites;

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
        if ((faceSprites.Length > faceSpriteIndex) && (faceSpriteIndex >= 0))
        {
            faceImage.sprite = faceSprites[faceSpriteIndex];
        }
    }
}
