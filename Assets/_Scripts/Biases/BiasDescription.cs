using UnityEngine;

namespace Assets._Scripts.Biases
{
    /// <summary>
    /// Represents a ScriptableObject used to define the description and metadata for a specific type of bias.
    /// </summary>
    /// <remarks>
    /// This class is designed to store detailed information about biases, including
    /// - The type of bias.
    /// - A headline describing the bias.
    /// - A preview text for summarization.
    /// - A detailed descriptive text.
    /// Instances of this class can be created and managed within the Unity Editor
    /// using the "Create > Biases > Bias Description" menu.
    /// </remarks>
    /// <example>
    /// Use this class to define and group descriptive data for various bias types,
    /// improving data organization in Unity projects related to biases.
    /// </example>
    [CreateAssetMenu(fileName = "NewBiasDescription", menuName = "Biases/Bias Description")]
    public class BiasDescription : ScriptableObject
    {
        public BiasDescriptionTexts.BiasType biasType;

        public string headline;

        [TextArea(5, 10)]
        public string preview;

        [TextArea(10, 20)]
        public string description;
    }
}