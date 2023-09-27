using UnityEngine;
using UnityEngine.UI;

public class ShaderToggle : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material brightnessMaterial;
    [SerializeField] private Material blurrMaterial;
    [SerializeField] private Image characterImageFace;
    [SerializeField] private Image characterImageHair;
    [SerializeField] private Image characterImageCloths;
    [SerializeField] private Image characterImageSkin;

    private void Awake()
    {
        characterImageFace.material = defaultMaterial;
        characterImageHair.material = defaultMaterial;
        characterImageCloths.material = defaultMaterial;
        characterImageSkin.material = defaultMaterial;
    }

    public void SetCharacterBrightness(bool value)
    {
        if (!value)
        {
            characterImageFace.material = defaultMaterial;
            characterImageHair.material = defaultMaterial;
            characterImageCloths.material = defaultMaterial;
            characterImageSkin.material = defaultMaterial;
        }
        else
        {
            characterImageFace.material = brightnessMaterial;
            characterImageHair.material = brightnessMaterial;
            characterImageCloths.material = brightnessMaterial;
            characterImageSkin.material = brightnessMaterial;
        }
    }

    public void SetCharacterBlur(bool value)
    {
        if (!value)
        {
            characterImageFace.material = defaultMaterial;
            characterImageHair.material = defaultMaterial;
            characterImageCloths.material = defaultMaterial;
            characterImageSkin.material = defaultMaterial;
        }
        else
        {
            characterImageFace.material = blurrMaterial;
            characterImageHair.material = blurrMaterial;
            characterImageCloths.material = blurrMaterial;
            characterImageSkin.material = blurrMaterial;
        }
    }
}
