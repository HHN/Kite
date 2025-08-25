using System;
using System.Collections.Generic;

namespace Assets._Scripts.Biases
{
    /// <summary>
    /// Defines the various types of discrimination biases that can occur in different contexts.
    /// This enum categorizes a range of biases, providing a structured way to identify and handle
    /// specific types of discrimination. It includes common biases such as gender pay gap,
    /// risk aversion, stereotypes, and unconscious communication bias, among others.
    /// </summary>
    public enum DiscriminationBias
    {
        None,
        AccessToFunding,
        GenderPayGap,
        UndervaluationOfWomenLedBusinesses,
        RiskAversionBias,
        ConfirmationBias,
        Tokenism,
        InPerceptionOfLeadershipAbilities,
        BenevolentSexismBias,
        AgeAndGenerationalBiases,
        StereotypesAgainstWomenInNonTraditionalIndustries,
        Heteronormativity,
        MaternalBias,
        ExpectationsRegardingFamilyPlanning,
        WorkLifeBalanceExpectations,
        GenderSpecificStereotypes,
        TightropeBias,
        Microaggressions,
        PerformanceAttributionBias,
        UnconsciousBiasInCommunication
    }

    /// <summary>
    /// Provides utility methods for working with the <see cref="DiscriminationBias"/> enumeration.
    /// </summary>
    public class DiscriminationBiasHelper
    {
        /// <summary>
        /// Converts a <see cref="DiscriminationBias"/> enumeration value to its corresponding integer representation.
        /// </summary>
        /// <param name="discriminationBias">The <see cref="DiscriminationBias"/> value to be converted.</param>
        /// <returns>An integer representing the specified <see cref="DiscriminationBias"/> value.</returns>
        public static int ToInt(DiscriminationBias discriminationBias)
        {
            return discriminationBias switch
            {
                DiscriminationBias.None => 0,
                DiscriminationBias.AccessToFunding => 1,
                DiscriminationBias.GenderPayGap => 2,
                DiscriminationBias.UndervaluationOfWomenLedBusinesses => 3,
                DiscriminationBias.RiskAversionBias => 4,
                DiscriminationBias.ConfirmationBias => 5,
                DiscriminationBias.Tokenism => 6,
                DiscriminationBias.InPerceptionOfLeadershipAbilities => 7,
                DiscriminationBias.AgeAndGenerationalBiases => 8,
                DiscriminationBias.StereotypesAgainstWomenInNonTraditionalIndustries => 9,
                DiscriminationBias.MaternalBias => 10,
                DiscriminationBias.ExpectationsRegardingFamilyPlanning => 11,
                DiscriminationBias.WorkLifeBalanceExpectations => 12,
                DiscriminationBias.GenderSpecificStereotypes => 13,
                DiscriminationBias.TightropeBias => 14,
                DiscriminationBias.Microaggressions => 15,
                DiscriminationBias.PerformanceAttributionBias => 16,
                DiscriminationBias.UnconsciousBiasInCommunication => 17,
                DiscriminationBias.Heteronormativity => 18,
                DiscriminationBias.BenevolentSexismBias => 19,
                _ => 0
            };
        }

        /// <summary>
        /// Converts an integer representation to its corresponding <see cref="DiscriminationBias"/> enumeration value.
        /// </summary>
        /// <param name="discriminationBias">The integer value representing a specific <see cref="DiscriminationBias"/>.</param>
        /// <returns>The <see cref="DiscriminationBias"/> enumeration value corresponding to the provided integer.</returns>
        public static DiscriminationBias ValueOf(int discriminationBias)
        {
            return discriminationBias switch
            {
                0 => DiscriminationBias.None,
                1 => DiscriminationBias.AccessToFunding,
                2 => DiscriminationBias.GenderPayGap,
                3 => DiscriminationBias.UndervaluationOfWomenLedBusinesses,
                4 => DiscriminationBias.RiskAversionBias,
                5 => DiscriminationBias.ConfirmationBias,
                6 => DiscriminationBias.Tokenism,
                7 => DiscriminationBias.InPerceptionOfLeadershipAbilities,
                8 => DiscriminationBias.AgeAndGenerationalBiases,
                9 => DiscriminationBias.StereotypesAgainstWomenInNonTraditionalIndustries,
                10 => DiscriminationBias.MaternalBias,
                11 => DiscriminationBias.ExpectationsRegardingFamilyPlanning,
                12 => DiscriminationBias.WorkLifeBalanceExpectations,
                13 => DiscriminationBias.GenderSpecificStereotypes,
                14 => DiscriminationBias.TightropeBias,
                15 => DiscriminationBias.Microaggressions,
                16 => DiscriminationBias.PerformanceAttributionBias,
                17 => DiscriminationBias.UnconsciousBiasInCommunication,
                18 => DiscriminationBias.Heteronormativity,
                19 => DiscriminationBias.BenevolentSexismBias,
                _ => DiscriminationBias.None
            };
        }

        /// <summary>
        /// Retrieves the descriptive string representation of a specified <see cref="DiscriminationBias"/> value.
        /// </summary>
        /// <param name="discriminationBias">The <see cref="DiscriminationBias"/> value for which the informational string is retrieved.</param>
        /// <returns>A string describing the specified <see cref="DiscriminationBias"/> value, or an empty string if the bias is not recognized.</returns>
        public static string GetInformationString(DiscriminationBias discriminationBias)
        {
            return discriminationBias switch
            {
                DiscriminationBias.None => "",
                DiscriminationBias.AccessToFunding => "Hier wird folgender Bias relevant: AccessToFinancing",
                DiscriminationBias.GenderPayGap => "Hier wird folgender Bias relevant: Gender Pay Gap",
                DiscriminationBias.UndervaluationOfWomenLedBusinesses => "Hier wird folgender Bias relevant: Unterbewertung weiblich geführter Unternehmen",
                DiscriminationBias.RiskAversionBias => "Hier wird folgender Bias relevant: Risk Aversion Bias",
                DiscriminationBias.ConfirmationBias => "Hier wird folgender Bias relevant: Bestätigungsverzerrung",
                DiscriminationBias.Tokenism => "Hier wird folgender Bias relevant: Tokenism",
                DiscriminationBias.InPerceptionOfLeadershipAbilities => "Hier wird folgender Bias relevant: Bias in der Wahrnehmung von Führungsfähigkeiten",
                DiscriminationBias.AgeAndGenerationalBiases => "Hier werden folgende Biases relevant: Alters- und Generationen-Biases",
                DiscriminationBias.StereotypesAgainstWomenInNonTraditionalIndustries => "Hier wird folgender Bias relevant: Stereotype gegenüber Frauen in nicht-traditionellen Branchen",
                DiscriminationBias.MaternalBias => "Hier wird folgender Bias relevant: Maternal Bias",
                DiscriminationBias.ExpectationsRegardingFamilyPlanning => "Hier wird folgender Bias relevant: Erwartungshaltung bezüglich Familienplanung",
                DiscriminationBias.WorkLifeBalanceExpectations => "Hier wird folgender Bias relevant: Work-Life-Balance-Erwartungen",
                DiscriminationBias.GenderSpecificStereotypes => "Hier wird folgender Bias relevant: Geschlechtsspezifische Stereotypen",
                DiscriminationBias.TightropeBias => "Hier wird folgender Bias relevant: Doppelte Bindung (Tightrope Bias)",
                DiscriminationBias.Microaggressions => "Hier wird folgender Bias relevant: Mikroaggressionen",
                DiscriminationBias.PerformanceAttributionBias => "Hier wird folgender Bias relevant: Leistungsattributions-Bias",
                DiscriminationBias.UnconsciousBiasInCommunication => "Hier wird folgender Bias relevant: Unbewusster Bias in der Kommunikation",
                DiscriminationBias.Heteronormativity => "Hier wird folgender Bias relevant: Heteronormativität",
                DiscriminationBias.BenevolentSexismBias => "Hier wird folgender Bias relevant: Benevolenter Sexismus",
                _ => ""
            };
        }

        /// <summary>
        /// Contains mapping of bias keywords to their corresponding descriptive labels or names.
        /// This dictionary maps string keys representing various bias types to their human-readable descriptions
        /// in multiple languages or formats.
        /// </summary>
        private static readonly Dictionary<string, string> BiasMap = new(StringComparer.OrdinalIgnoreCase)
        {
            { "AccessToFinancing", "Access To Funding" },
            { "GenderPayGap", "Gender Pay Gap" },
            { "UndervaluationFemaleManagedCompany", "Unterbewertung weiblich geführter Unternehmen" },
            { "RiskAversionBias", "Risk Aversion Bias" },
            { "ConfirmationBias", "Bestätigungsverzerrung" },
            { "Tokenism", "Tokenism" },
            { "BiasInThePerceptionOfLeadershipSkills", "Bias in der Wahrnehmung von Führungsfähigkeiten" },
            { "AgeAndGenerationsBiases", "Alters- und Generationen-Biases" },
            { "StereotypesAboutWomenInNonTraditionalIndustries", "Stereotype gegenüber Frauen in nicht-traditionellen Branchen" },
            { "BiasesAgainstWomenWithChildren", "Maternal Bias" },
            { "ExpectationsRegardingFamilyPlanning", "Hier wird folgender Bias relevant: Erwartungshaltung bezüglich Familienplanung" },
            { "WorkLifeBalanceExpectations", "Work-Life-Balance-Erwartungen" },
            { "GenderSpecificStereotypes", "Geschlechtsspezifische Stereotypen" },
            { "TightropeBias", "Doppelte Bindung (Tightrope Bias)" },
            { "Microaggression", "Mikroaggressionen" },
            { "PerformanceAttributionBias", "Leistungsattributions-Bias" },
            { "UnconsciousBiasInCommunication", "Unbewusster Bias in der Kommunikation" },
            { "Heteronormativity", "Heteronormativität" },
            { "BenevolentSexism", "Benevolenter Sexismus" }
        };

        /// <summary>
        /// Retrieves the descriptive name associated with a specific bias keyword.
        /// </summary>
        /// <param name="biasKeyword">The keyword representing a type of bias.</param>
        /// <returns>A string containing the descriptive name of the specified bias if found; otherwise, returns the original keyword.</returns>
        public static string GetBiasName(string biasKeyword)
        {
            return BiasMap.TryGetValue(biasKeyword, out var value) ? value : biasKeyword;
        }
    }
}