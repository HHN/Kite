using System;
using System.Collections.Generic;

namespace Assets._Scripts.Biases
{
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
        UnconsciousBiasInCommunication,
        ProveItAgainBias
    }

    public class DiscriminationBiasHelper
    {
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
                DiscriminationBias.ProveItAgainBias => 18,
                DiscriminationBias.Heteronormativity => 19,
                DiscriminationBias.BenevolentSexismBias => 20,
                _ => 0
            };
        }

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
                18 => DiscriminationBias.ProveItAgainBias,
                19 => DiscriminationBias.Heteronormativity,
                20 => DiscriminationBias.BenevolentSexismBias,
                _ => DiscriminationBias.None
            };
        }

        public static string GetInformationString(DiscriminationBias discriminationBias)
        {
            return discriminationBias switch
            {
                DiscriminationBias.None => "",
                DiscriminationBias.AccessToFunding => "Hier wird folgender Bias relevant: AccessToFunding",
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
                DiscriminationBias.ProveItAgainBias => "Hier wird folgender Bias relevant: Prove-it-Again-Bias",
                DiscriminationBias.Heteronormativity => "Hier wird folgender Bias relevant: Heteronormativität",
                DiscriminationBias.BenevolentSexismBias => "Hier wird folgender Bias relevant: Benevolenter Sexismus",
                _ => ""
            };
        }
        
        private static readonly Dictionary<string, string> BiasMap = new(StringComparer.OrdinalIgnoreCase)
        {
            { "AccessToFunding", "AccessToFunding" },
            { "GenderPayGap", "GenderPayGap" },
            { "UndervaluationOfWomenLedBusinesses", "Unterbewertung weiblich geführter Unternehmen" },
            { "RiskAversionBias", "Risk Aversion Bias" },
            { "ConfirmationBias", "Bestätigungsverzerrung" },
            { "Tokenism", "Tokenism" },
            { "InPerceptionOfLeadershipAbilities", "Bias in der Wahrnehmung von Führungsfähigkeiten" },
            { "AgeAndGenerationalBiases", "Alters- und Generationen-Biases" },
            { "StereotypesAgainstWomenInNonTraditionalIndustries", "Stereotype gegenüber Frauen in nicht-traditionellen Branchen" },
            { "MaternalBias", "Maternal Bias" },
            { "ExpectationsRegardingFamilyPlanning", "Hier wird folgender Bias relevant: Erwartungshaltung bezüglich Familienplanung" },
            { "WorkLifeBalanceExpectations", "Work-Life-Balance-Erwartungen" },
            { "GenderSpecificStereotypes", "Geschlechtsspezifische Stereotypen" },
            { "TightropeBias", "Doppelte Bindung (Tightrope Bias)" },
            { "Microaggressions", "Mikroaggressionen" },
            { "PerformanceAttributionBias", "Leistungsattributions-Bias" },
            { "UnconsciousBiasInCommunication", "Unbewusster Bias in der Kommunikation" },
            { "ProveItAgainBias", "Prove-it-Again-Bias" },
            { "Heteronormativity", "Heteronormativität" },
            { "BenevolentSexismBias", "Benevolenter Sexismus" }
        };

        public static string GetBiasName(string biasKeyword)
        {
            return BiasMap.TryGetValue(biasKeyword, out var value) ? value : biasKeyword;
        }
    }
}