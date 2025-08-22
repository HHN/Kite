using System;
using System.Collections.Generic;

namespace Assets._Scripts.Biases
{
    /// <summary>
    /// Represents a specific bias with associated metadata.
    /// </summary>
    /// <remarks>
    /// This class provides the structure for describing various types of biases.
    /// Each instance of the class contains details about the bias type, its category,
    /// headline, preview, and a full description for informational purposes.
    /// </remarks>
    [Serializable]
    public class Bias
    {
        public BiasType biasType;
        public string category;
        public string headline;
        public string preview;
        public string description;
    }

    /// <summary>
    /// Represents the types of biases that can be encountered in social, organizational, or personal contexts.
    /// </summary>
    public enum BiasType
    {
        AccessToFinancing,
        GenderPayGap,
        UndervaluationFemaleManagedCompany,
        RiskAversionBias,
        ConfirmationBias,
        Tokenism,
        BiasInThePerceptionOfLeadershipSkills,
        BenevolentSexism,
        AgeAndGenerationsBiases,
        StereotypesAboutWomenInNonTraditionalIndustries,
        Heteronormativity,
        BiasesAgainstWomenWithChildren,
        ExpectationsRegardingFamilyPlanning,
        WorkLifeBalanceExpectations,
        GenderSpecificStereotypes,
        TightropeBias,
        Microaggression,
        PerformanceAttributionBias,
        UnconsciousBiasInCommunication
    }

    /// <summary>
    /// Serves as a data container for deserializing a collection of biases from a JSON file.
    /// </summary>
    /// <remarks>
    /// This class encapsulates a collection of bias data items, where each item represents an individual `BiasJsonItem`.
    /// It is primarily used for JSON deserialization to load bias data into the application.
    /// </remarks>
    [Serializable]
    public class BiasJsonWrapper
    {
        public List<BiasJsonItem> items;
    }

    /// <summary>
    /// Represents an individual bias item retrieved from a JSON structure.
    /// </summary>
    /// <remarks>
    /// This class defines the structure of a bias as stored or represented in JSON format.
    /// An instance includes attributes such as type, category, headline, preview, and description
    /// to detail the characteristics and context of the bias.
    /// </remarks>
    [Serializable]
    public class BiasJsonItem
    {
        public string type;
        public string category;
        public string headline;
        public string preview;
        public string description;
    }
}