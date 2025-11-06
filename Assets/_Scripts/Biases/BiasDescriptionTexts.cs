using System.Collections.Generic;
using System.Linq;
using Assets._Scripts._Mappings;
using UnityEngine;

namespace Assets._Scripts.Biases
{
    /// <summary>
    /// A static class that provides descriptions for various biases identified by their specific types.
    /// </summary>
    public static class BiasDescriptionTexts
    {
        private static Dictionary<string, Bias> _biases;
        private const string LinkColor = "#F5944E";

        /// <summary>
        /// A static class providing functionalities to manage and retrieve descriptions of various biases.
        /// </summary>
        static BiasDescriptionTexts()
        {
            InitializeBiasData();
        }

        /// <summary>
        /// Initializes bias data by retrieving all biases from the mapping manager and storing them in a dictionary.
        /// </summary>
        /// <remarks>
        /// Ensures the biases dictionary is not reinitialized if it already contains data.
        /// Used for populating the internal biases structure with available bias information.
        /// </remarks>
        private static void InitializeBiasData()
        {
            if (_biases != null) return;
            
            _biases = MappingManager.GetAllBiases();
        }

        /// <summary>
        /// Retrieves the description text for a specific bias type.
        /// </summary>
        /// <param name="biasKey">The type of bias for which to retrieve the description.</param>
        /// <returns>A string containing the formatted bias description or a default "TEXT NOT FOUND" if unavailable.</returns>
        public static string GetBiasText(string biasKey)
        {
            _biases ??= MappingManager.GetAllBiases();

            if (_biases == null || !_biases.TryGetValue(biasKey, out Bias biasInfo)) return "TEXT NOT FOUND";
            
            string styledDescription = biasInfo.description.Replace("=LINK_COLOR", $"={LinkColor}");
                
            return $"<align=center><b>{biasInfo.headline}</b></align>\n\n" +
                   $"{biasInfo.preview}\n\n" +
                   $"{styledDescription}";

        }
    }
}