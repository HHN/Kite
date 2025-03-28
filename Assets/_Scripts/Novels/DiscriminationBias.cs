namespace Assets._Scripts.Novels
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
        HeteronormativityBias,
        BenevolentSexismBias
    }

    public class DiscriminationBiasHelper
    {
        public static int ToInt(DiscriminationBias discriminationBias)
        {
            switch (discriminationBias)
            {
                case DiscriminationBias.None: return 0;
                case DiscriminationBias.AccessToFunding: return 1;
                case DiscriminationBias.GenderPayGap: return 2;
                case DiscriminationBias.UndervaluationOfWomenLedBusinesses: return 3;
                case DiscriminationBias.RiskAversionBias: return 4;
                case DiscriminationBias.ConfirmationBias: return 5;
                case DiscriminationBias.Tokenism: return 6;
                case DiscriminationBias.InPerceptionOfLeadershipAbilities: return 7;
                case DiscriminationBias.RacistAndEthnicBiases: return 8;
                case DiscriminationBias.SocioeconomicBiases: return 9;
                case DiscriminationBias.AgeAndGenerationalBiases: return 10;
                case DiscriminationBias.SexualityRelatedBiases: return 11;
                case DiscriminationBias.AgainstWomenWithDisabilities: return 12;
                case DiscriminationBias.StereotypesAgainstWomenInNonTraditionalIndustries: return 13;
                case DiscriminationBias.CulturalAndReligiousBiases: return 14;
                case DiscriminationBias.MaternalBias: return 15;
                case DiscriminationBias.AgainstWomenWithChildren: return 16;
                case DiscriminationBias.ExpectationsRegardingFamilyPlanning: return 17;
                case DiscriminationBias.WorkLifeBalanceExpectations: return 18;
                case DiscriminationBias.GenderSpecificStereotypes: return 19;
                case DiscriminationBias.TightropeBias: return 20;
                case DiscriminationBias.Microaggressions: return 21;
                case DiscriminationBias.PerformanceAttributionBias: return 22;
                case DiscriminationBias.InMediaAndAdvertising: return 23;
                case DiscriminationBias.UnconsciousBiasInCommunication: return 24;
                case DiscriminationBias.ProveItAgainBias: return 25;
                case DiscriminationBias.HeteronormativityBias: return 26;
                case DiscriminationBias.BenevolentSexismBias: return 27;
                default: return 0;
            }
        }

        public static DiscriminationBias ValueOf(int discriminationBias)
        {
            switch (discriminationBias)
            {
                case 0: return DiscriminationBias.None;
                case 1: return DiscriminationBias.AccessToFunding;
                case 2: return DiscriminationBias.GenderPayGap;
                case 3: return DiscriminationBias.UndervaluationOfWomenLedBusinesses;
                case 4: return DiscriminationBias.RiskAversionBias;
                case 5: return DiscriminationBias.ConfirmationBias;
                case 6: return DiscriminationBias.Tokenism;
                case 7: return DiscriminationBias.InPerceptionOfLeadershipAbilities;
                case 8: return DiscriminationBias.RacistAndEthnicBiases;
                case 9: return DiscriminationBias.SocioeconomicBiases;
                case 10: return DiscriminationBias.AgeAndGenerationalBiases;
                case 11: return DiscriminationBias.SexualityRelatedBiases;
                case 12: return DiscriminationBias.AgainstWomenWithDisabilities;
                case 13: return DiscriminationBias.StereotypesAgainstWomenInNonTraditionalIndustries;
                case 14: return DiscriminationBias.CulturalAndReligiousBiases;
                case 15: return DiscriminationBias.MaternalBias;
                case 16: return DiscriminationBias.AgainstWomenWithChildren;
                case 17: return DiscriminationBias.ExpectationsRegardingFamilyPlanning;
                case 18: return DiscriminationBias.WorkLifeBalanceExpectations;
                case 19: return DiscriminationBias.GenderSpecificStereotypes;
                case 20: return DiscriminationBias.TightropeBias;
                case 21: return DiscriminationBias.Microaggressions;
                case 22: return DiscriminationBias.PerformanceAttributionBias;
                case 23: return DiscriminationBias.InMediaAndAdvertising;
                case 24: return DiscriminationBias.UnconsciousBiasInCommunication;
                case 25: return DiscriminationBias.ProveItAgainBias;
                case 26: return DiscriminationBias.HeteronormativityBias;
                case 27: return DiscriminationBias.BenevolentSexismBias;
                default: return DiscriminationBias.None;
            }
        }

        public static string GetInformationString(DiscriminationBias discriminationBias)
        {
            switch (discriminationBias)
            {
                case DiscriminationBias.None: return "";
                case DiscriminationBias.AccessToFunding:
                    return "Hier wird folgender Bias relevant: AccessToFinancing";
                case DiscriminationBias.GenderPayGap: return "Hier wird folgender Bias relevant: Gender Pay Gap";
                case DiscriminationBias.UndervaluationOfWomenLedBusinesses:
                    return "Hier wird folgender Bias relevant: Unterbewertung weiblich geführter Unternehmen";
                case DiscriminationBias.RiskAversionBias:
                    return "Hier wird folgender Bias relevant: Risk Aversion Bias";
                case DiscriminationBias.ConfirmationBias:
                    return "Hier wird folgender Bias relevant: Bestätigungsverzerrung";
                case DiscriminationBias.Tokenism: return "Hier wird folgender Bias relevant: Tokenism";
                case DiscriminationBias.InPerceptionOfLeadershipAbilities:
                    return "Hier wird folgender Bias relevant: Bias in der Wahrnehmung von Führungsfähigkeiten";
                case DiscriminationBias.RacistAndEthnicBiases:
                    return "Hier werden folgende Biases relevant: Rassistische und ethnische Biases";
                case DiscriminationBias.SocioeconomicBiases:
                    return "Hier werden folgende Biases relevant: Sozioökonomische Biases";
                case DiscriminationBias.AgeAndGenerationalBiases:
                    return "Hier werden folgende Biases relevant: Alter- und Generationen-Biases";
                case DiscriminationBias.SexualityRelatedBiases:
                    return "Hier werden folgende Biases relevant: Sexualitätsbezogene Biases";
                case DiscriminationBias.AgainstWomenWithDisabilities:
                    return "Hier wird folgender Bias relevant: Biases gegenüber Frauen mit Behinderungen";
                case DiscriminationBias.StereotypesAgainstWomenInNonTraditionalIndustries:
                    return
                        "Hier wird folgender Bias relevant: Stereotype gegenüber Frauen in nicht-traditionellen Branchen";
                case DiscriminationBias.CulturalAndReligiousBiases:
                    return "Hier werden folgende Biases relevant: Kulturelle und religiöse Biases";
                case DiscriminationBias.MaternalBias: return "Hier wird folgender Bias relevant: Maternal Bias";
                case DiscriminationBias.AgainstWomenWithChildren:
                    return "Hier wird folgender Bias relevant: Bias gegenüber Frauen mit Kindern";
                case DiscriminationBias.ExpectationsRegardingFamilyPlanning:
                    return "Hier wird folgender Bias relevant: Erwartungshaltung bezüglich Familienplanung";
                case DiscriminationBias.WorkLifeBalanceExpectations:
                    return "Hier wird folgender Bias relevant: Work-Life-Balance-Erwartungen";
                case DiscriminationBias.GenderSpecificStereotypes:
                    return "Hier wird folgender Bias relevant: Geschlechtsspezifische Stereotypen";
                case DiscriminationBias.TightropeBias:
                    return "Hier wird folgender Bias relevant: Doppelte Bindung (Tightrope Bias)";
                case DiscriminationBias.Microaggressions: return "Hier wird folgender Bias relevant: Mikroaggressionen";
                case DiscriminationBias.PerformanceAttributionBias:
                    return "Hier wird folgender Bias relevant: Leistungsattributions-Bias";
                case DiscriminationBias.InMediaAndAdvertising:
                    return "Hier wird folgender Bias relevant: Bias in Medien und Werbung";
                case DiscriminationBias.UnconsciousBiasInCommunication:
                    return "Hier wird folgender Bias relevant: Unbewusster Bias in der Kommunikation";
                case DiscriminationBias.ProveItAgainBias:
                    return "Hier wird folgender Bias relevant: Prove-it-Again-Bias";
                case DiscriminationBias.HeteronormativityBias:
                    return "Hier wird folgender Bias relevant: Heteronormativität";
                case DiscriminationBias.BenevolentSexismBias:
                    return "Hier wird folgender Bias relevant: Benevolenter Sexismus";
                default: return "";
            }
        }
    }
}