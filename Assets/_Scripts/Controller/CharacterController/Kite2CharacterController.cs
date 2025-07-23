using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets._Scripts.Controller.CharacterController
{
    /// <summary>
    /// Represents a collection of sprites categorized by hand colors.
    /// </summary>
    public class HandSprites
    {
        public Sprite[] HandColorA; 
        public Sprite[] HandColorB; 
        public Sprite[] HandColorC; 
        public Sprite[] HandColorD; 
    }

    /// <summary>
    /// Represents an index for selecting a sprite from a collection of hand sprites.
    /// </summary>
    /// <remarks>
    /// This class is used to specify both the color category and the individual sprite within that category.
    /// </remarks>
    [Serializable]
    public class HandSpriteIndex
    {
        public int colorIndex; 
        public int spriteIndex; 
    }

    /// <summary>
    /// Manages the character's customizable appearance and behaviors such as skin, glasses, clothing, and hair.
    /// Provides functionality to change and randomize character sprites and manage expressions and animations.
    /// </summary>
    public class Kite2CharacterController : MonoBehaviour
    {
        [SerializeField] private Image skinImage;
        [SerializeField] private Image glassImage;
        [SerializeField] private Image handImage;
        [SerializeField] private Image clothesImage;
        [SerializeField] private Image hairImage;
        [SerializeField] private Image faceImage;

        [SerializeField] private Sprite[] skinSprites;
        [SerializeField] private Sprite[] glassesSprites;
        [SerializeField] private HandSprites handSprites;
        [SerializeField] private Sprite[] clothesSprites;
        [SerializeField] private Sprite[] hairSprites;
        [SerializeField] private Animator animator;

        private static readonly int IsTalking = Animator.StringToHash("isTalking");

        private static readonly int[] FaceExpressions = 
        {
            Animator.StringToHash("scared_speaking"),
            Animator.StringToHash("defeated_speaking"),
            Animator.StringToHash("dissatisfied_speaking"),
            Animator.StringToHash("rejecting_speaking"),
            Animator.StringToHash("amazed_speaking"),
            Animator.StringToHash("questioning_speaking"),
            Animator.StringToHash("critical_speaking"),
            Animator.StringToHash("smiling_big_speaking"),
            Animator.StringToHash("laughing_speaking"),
            Animator.StringToHash("smiling_speaking"),
            Animator.StringToHash("neutral_relaxed_speaking"),
            Animator.StringToHash("neutral_speaking"),
            Animator.StringToHash("proud_speaking"),
            Animator.StringToHash("scared"),
            Animator.StringToHash("defeated"),
            Animator.StringToHash("dissatisfied"),
            Animator.StringToHash("rejecting"),
            Animator.StringToHash("amazed"),
            Animator.StringToHash("questioning"),
            Animator.StringToHash("critical"),
            Animator.StringToHash("smiling_big"),
            Animator.StringToHash("laughing"),
            Animator.StringToHash("smiling"),
            Animator.StringToHash("neutral_relaxed"),
            Animator.StringToHash("neutral"),
            Animator.StringToHash("proud")
        };

        public int skinIndex;
        public int glassIndex;
        public int[] handIndex;
        public int clotheIndex;
        public int hairIndex;

        public int characterId;

        // Utility methods
        private bool IsValidArray(Sprite[] array) => array != null && array.Length > 0;
        private bool IsValidImage(Image image) => image != null;

        /// <summary>
        /// Assigns a random skin sprite to the character from the available skin sprite array.
        /// Updates the `skinIndex` with the randomly selected sprite's index
        /// and determines the appropriate hand index based on the naming convention of the sprite.
        /// </summary>
        /// <remarks>
        /// This method ensures the `skinImage` and `skinSprites` are valid before proceeding.
        /// If `skinSprites` is null or empty, or if `skinImage` is null, the method exits early.
        /// The hand index is derived from specific substrings found in the sprite's name.
        /// </remarks>
        public void SetSkinSprite()
        {
            if (!IsValidArray(skinSprites) || !IsValidImage(skinImage)) return;

            handIndex = new int[2];
            skinIndex = Random.Range(0, skinSprites.Length);
            Sprite randomSkinImage = skinSprites[skinIndex];
            skinImage.sprite = randomSkinImage;

            handIndex[0] = 
                randomSkinImage.name.Contains("_a") ? 0 :
                randomSkinImage.name.Contains("_b") ? 1 :
                randomSkinImage.name.Contains("_c") ? 2 :
                randomSkinImage.name.Contains("_d") ? 3 : 0;
        }

        /// <summary>
        /// Sets the skin sprite for the character to the specified index from the available skin sprite array.
        /// </summary>
        /// <param name="skinSpriteIndex">The index of the desired skin sprite in the skin sprite array.
        /// Must be within the array's bounds to successfully update the skin.</param>
        public void SetSkinSprite(int skinSpriteIndex)
        {
            if (!IsValidArray(skinSprites) || !IsValidImage(skinImage) || skinSpriteIndex >= skinSprites.Length) return;
            
            skinImage.sprite = skinSprites[skinSpriteIndex];
        }

        /// <summary>
        /// Assigns a random glasses sprite to the character from the available glasses sprite array.
        /// Updates the `glassIndex` with the randomly selected sprite's index.
        /// </summary>
        /// <remarks>
        /// This method validates the `glassesSprites` array and `glassImage` before proceeding.
        /// If `glassesSprites` is null, empty, or if `glassImage` is null, the method exits early.
        /// A random index is then selected from the array, and the corresponding sprite is assigned to the `glassImage`.
        /// </remarks>
        public void SetGlassesSprite()
        {
            if (!IsValidArray(glassesSprites) || !IsValidImage(glassImage)) return;

            glassIndex = Random.Range(0, glassesSprites.Length);
            glassImage.sprite = glassesSprites[glassIndex];
        }

        /// <summary>
        /// Sets the glasses sprite for the character based on the provided sprite index.
        /// Updates the `glassImage` with the sprite at the specified index in the `glassesSprites` array.
        /// </summary>
        /// <remarks>
        /// This method ensures that the `glassesSprites` array, the `glassImage`, and the provided sprite index are valid.
        /// If the array is null or empty, the `glassImage` is null, or the index is out of bounds, the method exits without making changes.
        /// </remarks>
        /// <param name="glassesSpriteIndex">The index of the glasses sprite to set. Must be within the bounds of the `glassesSprites` array.</param>
        public void SetGlassesSprite(int glassesSpriteIndex)
        {
            if (!IsValidArray(glassesSprites) || !IsValidImage(glassImage) || glassesSpriteIndex >= glassesSprites.Length) return;
            
            glassImage.sprite = glassesSprites[glassesSpriteIndex];
        }

        /// <summary>
        /// Configures the character's hand sprite based on the current hand color index
        /// and selects a random sprite within the specified hand color group.
        /// Updates the `handIndex` to reflect the selected color and sprite.
        /// </summary>
        /// <remarks>
        /// This method verifies that requisite references, including `handSprites` and `handImage`, are properly initialized.
        /// If the `handSprites` object is null or the `handImage` is invalid, the method exits early.
        /// The specific hand sprite array is chosen based on the first index of `handIndex`.
        /// A random sprite is then assigned from the selected array, and the second index of `handIndex` is updated
        /// with the chosen sprite's position in the array.
        /// </remarks>
        public void SetHandSprite()
        {
            if (handSprites == null || !IsValidImage(handImage)) return;

            Sprite[] selectedHandArray = handIndex[0] switch
            {
                0 => handSprites.HandColorA,
                1 => handSprites.HandColorB,
                2 => handSprites.HandColorC,
                3 => handSprites.HandColorD,
                _ => null
            };

            if (!IsValidArray(selectedHandArray)) return;

            if (selectedHandArray == null) return;
            
            int randomIndex = Random.Range(0, selectedHandArray.Length);
            handImage.sprite = selectedHandArray[randomIndex];
            handIndex[1] = randomIndex;
        }

        /// <summary>
        /// Sets the hand sprite for the character based on the specified color index and sprite index.
        /// The sprite is obtained from the corresponding hand color array in the `HandSprites` collection
        /// and applied to the `handImage`.
        /// </summary>
        /// <remarks>
        /// Validates the availability of `handSprites` and `handImage` before proceeding.
        /// Ensures the selected hand color array and sprite index are valid to prevent errors.
        /// If the indices are invalid or corresponding arrays are null, the method logs a warning and exits early.
        /// </remarks>
        /// <param name="handSpriteIndex">An object containing the color index and sprite index
        /// to determine the selected hand sprite.
        /// The `colorIndex` selects the array of hand sprites (e.g., `HandColorA`, `HandColorB`),
        /// and the `spriteIndex` selects the specific sprite from the chosen array.</param>
        public void SetHandSprite(HandSpriteIndex handSpriteIndex)
        {
            if (handSprites == null || !IsValidImage(handImage)) return;

            Sprite[] selectedHandColorArray = handSpriteIndex.colorIndex switch
            {
                0 => handSprites.HandColorA,
                1 => handSprites.HandColorB,
                2 => handSprites.HandColorC,
                3 => handSprites.HandColorD,
                _ => null
            };

            if (!IsValidArray(selectedHandColorArray)) return;
            
            if (selectedHandColorArray != null && (handSpriteIndex.spriteIndex < 0 || handSpriteIndex.spriteIndex >= selectedHandColorArray.Length))
            {
                Debug.LogWarning($"Invalid spriteIndex {handSpriteIndex.spriteIndex} for color {handSpriteIndex.colorIndex}");
                return;
            }

            if (selectedHandColorArray != null) handImage.sprite = selectedHandColorArray[handSpriteIndex.spriteIndex];
        }

        /// <summary>
        /// Assigns a random clothing sprite to the character from the available clothing sprite array.
        /// Updates the `clotheIndex` with the randomly selected sprite's index and sets the corresponding sprite for the `clothesImage`.
        /// </summary>
        /// <remarks>
        /// Ensures the `clothesImage` and `clothesSprites` are valid before proceeding.
        /// If `clothesSprites` is null or empty, or if `clothesImage` is null, the method exits early without making any changes.
        /// </remarks>
        public void SetClotheSprite()
        {
            if (!IsValidArray(clothesSprites) || !IsValidImage(clothesImage)) return;
            clotheIndex = Random.Range(0, clothesSprites.Length);
            clothesImage.sprite = clothesSprites[clotheIndex];
        }

        /// <summary>
        /// Sets the clothing sprite of the character to the specified index
        /// within the available clothing sprite array.
        /// </summary>
        /// <remarks>
        /// Ensures the `clothesImage` and `clothesSprites` are valid before updating the sprite.
        /// If `clothesSprites` is null, empty, or if `clothesImage` is null, or if the specified index
        /// is out of range, the method exits early without making changes.
        /// </remarks>
        /// <param name="clotheSpriteIndex">The index of the clothing sprite in the `clothesSprites` array to be applied.</param>
        public void SetClotheSprite(int clotheSpriteIndex)
        {
            if (!IsValidArray(clothesSprites) || !IsValidImage(clothesImage) || clotheSpriteIndex >= clothesSprites.Length) return;
            
            clothesImage.sprite = clothesSprites[clotheSpriteIndex];
        }

        /// <summary>
        /// Assigns a random hair sprite to the character from the available hair sprite array.
        /// Updates the `hairIndex` with the index of the selected sprite and applies the sprite to the `hairImage`.
        /// </summary>
        /// <remarks>
        /// This method ensures the `hairImage` and `hairSprites` are valid before proceeding.
        /// If the `hairSprites` array is null or empty, or if `hairImage` is null, the method exits early.
        /// The random hair sprite is selected using UnityEngine's `Random.Range` method based on the array length.
        /// </remarks>
        public void SetHairSprite()
        {
            if (!IsValidArray(hairSprites) || !IsValidImage(hairImage)) return;
            
            hairIndex = Random.Range(0, hairSprites.Length);
            hairImage.sprite = hairSprites[hairIndex];
        }

        /// <summary>
        /// Assigns a random hair sprite to the character from the available hair sprite array.
        /// Updates the `hairIndex` with the randomly selected sprite's index.
        /// </summary>
        /// <remarks>
        /// This method ensures that the `hairImage` and `hairSprites` are valid before proceeding.
        /// If `hairSprites` is null or empty, or if `hairImage` is null, the method exits early.
        /// </remarks>
        public void SetHairSprite(int hairSpriteIndex)
        {
            if (!IsValidArray(hairSprites) || !IsValidImage(hairImage) || hairSpriteIndex >= hairSprites.Length) return;
            
            hairImage.sprite = hairSprites[hairSpriteIndex];
        }

        /// <summary>
        /// Sets the character's face animation to the specified expression using the animator.
        /// </summary>
        /// <remarks>
        /// This method validates both the `animator` and the expression index before playing the respective animation.
        /// </remarks>
        /// <param name="expression">The index of the face expression to be set. Must be a valid index in the `FaceExpressions` array.</param>
        public void SetFaceExpression(int expression)
        {
            if (animator == null) return;
            if (expression < 0 || expression >= FaceExpressions.Length) return;

            animator.Play(FaceExpressions[expression]);
        }

        /// <summary>
        /// Activates the "talking" animation for the character by enabling the `isTalking` parameter in the animator.
        /// </summary>
        /// <remarks>
        /// This method interacts with the character's animator to trigger the talking animation state.
        /// If the animator is null, the method does nothing.
        /// </remarks>
        public void StartTalking()
        {
            animator?.SetBool(IsTalking, true);
        }

        /// <summary>
        /// Stops the talking animation of the character by setting the "isTalking" animation flag to false.
        /// </summary>
        /// <remarks>
        /// This method uses the Animator component to control the "isTalking" animation state.
        /// If the Animator instance is not null, it updates the "isTalking" boolean parameter to false, effectively halting any corresponding talking animations.
        /// </remarks>
        public void StopTalking()
        {
            animator?.SetBool(IsTalking, false);
        }
    }
}