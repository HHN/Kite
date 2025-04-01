namespace Assets._Scripts.Novel
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
        RacistAndEthnicBiases,
        SocioeconomicBiases,
        AgeAndGenerationalBiases,
        SexualityRelatedBiases,
        AgainstWomenWithDisabilities,
        StereotypesAgainstWomenInNonTraditionalIndustries,
        CulturalAndReligiousBiases,
        MaternalBias,
        AgainstWomenWithChildren,
        ExpectationsRegardingFamilyPlanning,
        WorkLifeBalanceExpectations,
        GenderSpecificStereotypes,
        TightropeBias,
        Microaggressions,
        PerformanceAttributionBias,
        InMediaAndAdvertising,
        UnconsciousBiasInCommunication,
        ProveItAgainBias,
        HeteronormativitaetBias,
        BenevolenterSexismusBias
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
                DiscriminationBias.RacistAndEthnicBiases => 8,
                DiscriminationBias.SocioeconomicBiases => 9,
                DiscriminationBias.AgeAndGenerationalBiases => 10,
                DiscriminationBias.SexualityRelatedBiases => 11,
                DiscriminationBias.AgainstWomenWithDisabilities => 12,
                DiscriminationBias.StereotypesAgainstWomenInNonTraditionalIndustries => 13,
                DiscriminationBias.CulturalAndReligiousBiases => 14,
                DiscriminationBias.MaternalBias => 15,
                DiscriminationBias.AgainstWomenWithChildren => 16,
                DiscriminationBias.ExpectationsRegardingFamilyPlanning => 17,
                DiscriminationBias.WorkLifeBalanceExpectations => 18,
                DiscriminationBias.GenderSpecificStereotypes => 19,
                DiscriminationBias.TightropeBias => 20,
                DiscriminationBias.Microaggressions => 21,
                DiscriminationBias.PerformanceAttributionBias => 22,
                DiscriminationBias.InMediaAndAdvertising => 23,
                DiscriminationBias.UnconsciousBiasInCommunication => 24,
                DiscriminationBias.ProveItAgainBias => 25,
                DiscriminationBias.HeteronormativitaetBias => 26,
                DiscriminationBias.BenevolenterSexismusBias => 27,
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
                8 => DiscriminationBias.RacistAndEthnicBiases,
                9 => DiscriminationBias.SocioeconomicBiases,
                10 => DiscriminationBias.AgeAndGenerationalBiases,
                11 => DiscriminationBias.SexualityRelatedBiases,
                12 => DiscriminationBias.AgainstWomenWithDisabilities,
                13 => DiscriminationBias.StereotypesAgainstWomenInNonTraditionalIndustries,
                14 => DiscriminationBias.CulturalAndReligiousBiases,
                15 => DiscriminationBias.MaternalBias,
                16 => DiscriminationBias.AgainstWomenWithChildren,
                17 => DiscriminationBias.ExpectationsRegardingFamilyPlanning,
                18 => DiscriminationBias.WorkLifeBalanceExpectations,
                19 => DiscriminationBias.GenderSpecificStereotypes,
                20 => DiscriminationBias.TightropeBias,
                21 => DiscriminationBias.Microaggressions,
                22 => DiscriminationBias.PerformanceAttributionBias,
                23 => DiscriminationBias.InMediaAndAdvertising,
                24 => DiscriminationBias.UnconsciousBiasInCommunication,
                25 => DiscriminationBias.ProveItAgainBias,
                26 => DiscriminationBias.HeteronormativitaetBias,
                27 => DiscriminationBias.BenevolenterSexismusBias,
                _ => DiscriminationBias.None
            };
        }

        public static string GetInformationString(DiscriminationBias discriminationBias)
        {
            return discriminationBias switch
            {
                DiscriminationBias.None => "",
                DiscriminationBias.AccessToFunding => "Hier wird folgender Bias relevant: Finanzierungszugang",
                DiscriminationBias.GenderPayGap => "Hier wird folgender Bias relevant: Gender Pay Gap",
                DiscriminationBias.UndervaluationOfWomenLedBusinesses => "Hier wird folgender Bias relevant: Unterbewertung weiblich geführter Unternehmen",
                DiscriminationBias.RiskAversionBias => "Hier wird folgender Bias relevant: Risk Aversion Bias",
                DiscriminationBias.ConfirmationBias => "Hier wird folgender Bias relevant: Bestätigungsverzerrung",
                DiscriminationBias.Tokenism => "Hier wird folgender Bias relevant: Tokenism",
                DiscriminationBias.InPerceptionOfLeadershipAbilities => "Hier wird folgender Bias relevant: Bias in der Wahrnehmung von Führungsfähigkeiten",
                DiscriminationBias.RacistAndEthnicBiases => "Hier werden folgende Biases relevant: Rassistische und ethnische Biases",
                DiscriminationBias.SocioeconomicBiases => "Hier werden folgende Biases relevant: Sozioökonomische Biases",
                DiscriminationBias.AgeAndGenerationalBiases => "Hier werden folgende Biases relevant: Alter- und Generationen-Biases",
                DiscriminationBias.SexualityRelatedBiases => "Hier werden folgende Biases relevant: Sexualitätsbezogene Biases",
                DiscriminationBias.AgainstWomenWithDisabilities => "Hier wird folgender Bias relevant: Biases gegenüber Frauen mit Behinderungen",
                DiscriminationBias.StereotypesAgainstWomenInNonTraditionalIndustries => "Hier wird folgender Bias relevant: Stereotype gegenüber Frauen in nicht-traditionellen Branchen",
                DiscriminationBias.CulturalAndReligiousBiases => "Hier werden folgende Biases relevant: Kulturelle und religiöse Biases",
                DiscriminationBias.MaternalBias => "Hier wird folgender Bias relevant: Maternal Bias",
                DiscriminationBias.AgainstWomenWithChildren => "Hier wird folgender Bias relevant: Bias gegenüber Frauen mit Kindern",
                DiscriminationBias.ExpectationsRegardingFamilyPlanning => "Hier wird folgender Bias relevant: Erwartungshaltung bezüglich Familienplanung",
                DiscriminationBias.WorkLifeBalanceExpectations => "Hier wird folgender Bias relevant: Work-Life-Balance-Erwartungen",
                DiscriminationBias.GenderSpecificStereotypes => "Hier wird folgender Bias relevant: Geschlechtsspezifische Stereotypen",
                DiscriminationBias.TightropeBias => "Hier wird folgender Bias relevant: Doppelte Bindung (Tightrope Bias)",
                DiscriminationBias.Microaggressions => "Hier wird folgender Bias relevant: Mikroaggressionen",
                DiscriminationBias.PerformanceAttributionBias => "Hier wird folgender Bias relevant: Leistungsattributions-Bias",
                DiscriminationBias.InMediaAndAdvertising => "Hier wird folgender Bias relevant: Bias in Medien und Werbung",
                DiscriminationBias.UnconsciousBiasInCommunication => "Hier wird folgender Bias relevant: Unbewusster Bias in der Kommunikation",
                DiscriminationBias.ProveItAgainBias => "Hier wird folgender Bias relevant: Prove-it-Again-Bias",
                DiscriminationBias.HeteronormativitaetBias => "Hier wird folgender Bias relevant: Heteronormativität",
                DiscriminationBias.BenevolenterSexismusBias => "Hier wird folgender Bias relevant: Benevolenter Sexismus",
                _ => ""
            };
        }
    }
}