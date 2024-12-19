using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Player
{
    public class ShaderToggle : MonoBehaviour
    {
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material brightnessMaterial;
        [SerializeField] private Material blurMaterial;
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
                characterImageFace.material = blurMaterial;
                characterImageHair.material = blurMaterial;
                characterImageCloths.material = blurMaterial;
                characterImageSkin.material = blurMaterial;
            }
        }
    }
}