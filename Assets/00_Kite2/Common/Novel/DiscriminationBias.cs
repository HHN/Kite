namespace _00_Kite2.Common.Novel
{
    public enum DiscriminationBias
    {
        NONE,
        ACCESS_TO_FUNDING,
        GENDER_PAY_GAP,
        UNDERVALUATION_OF_WOMEN_LED_BUSINESSES,
        RISK_AVERSION_BIAS,
        CONFIRMATION_BIAS,
        TOKENISM,
        IN_PERCEPTION_OF_LEADERSHIP_ABILITIES,
        RACIST_AND_ETHNIC_BIASES,
        SOCIOECONOMIC_BIASES,
        AGE_AND_GENERATIONAL_BIASES,
        SEXUALITY_RELATED_BIASES,
        AGAINST_WOMEN_WITH_DISABILITIES,
        STEREOTYPES_AGAINST_WOMEN_IN_NON_TRADITIONAL_INDUSTRIES,
        CULTURAL_AND_RELIGIOUS_BIASES,
        MATERNAL_BIAS,
        AGAINST_WOMEN_WITH_CHILDREN,
        EXPECTATIONS_REGARDING_FAMILY_PLANNING,
        WORK_LIFE_BALANCE_EXPECTATIONS,
        GENDER_SPECIFIC_STEREOTYPES,
        TIGHTROPE_BIAS,
        MICROAGGRESSIONS,
        PERFORMANCE_ATTRIBUTION_BIAS,
        IN_MEDIA_AND_ADVERTISING,
        UNCONSCIOUS_BIAS_IN_COMMUNICATION,
        PROVE_IT_AGAIN_BIAS,
        HETERONORMATIVITAET_BIAS,
        BENEVOLENTER_SEXISMUS_BIAS
    }

    public class DiscriminationBiasHelper
    {
        public static int ToInt(DiscriminationBias discriminationBias)
        {
            switch (discriminationBias)
            {
                case DiscriminationBias.NONE: return 0;
                case DiscriminationBias.ACCESS_TO_FUNDING: return 1;
                case DiscriminationBias.GENDER_PAY_GAP: return 2;
                case DiscriminationBias.UNDERVALUATION_OF_WOMEN_LED_BUSINESSES: return 3;
                case DiscriminationBias.RISK_AVERSION_BIAS: return 4;
                case DiscriminationBias.CONFIRMATION_BIAS: return 5;
                case DiscriminationBias.TOKENISM: return 6;
                case DiscriminationBias.IN_PERCEPTION_OF_LEADERSHIP_ABILITIES: return 7;
                case DiscriminationBias.RACIST_AND_ETHNIC_BIASES: return 8;
                case DiscriminationBias.SOCIOECONOMIC_BIASES: return 9;
                case DiscriminationBias.AGE_AND_GENERATIONAL_BIASES: return 10;
                case DiscriminationBias.SEXUALITY_RELATED_BIASES: return 11;
                case DiscriminationBias.AGAINST_WOMEN_WITH_DISABILITIES: return 12;
                case DiscriminationBias.STEREOTYPES_AGAINST_WOMEN_IN_NON_TRADITIONAL_INDUSTRIES: return 13;
                case DiscriminationBias.CULTURAL_AND_RELIGIOUS_BIASES: return 14;
                case DiscriminationBias.MATERNAL_BIAS: return 15;
                case DiscriminationBias.AGAINST_WOMEN_WITH_CHILDREN: return 16;
                case DiscriminationBias.EXPECTATIONS_REGARDING_FAMILY_PLANNING: return 17;
                case DiscriminationBias.WORK_LIFE_BALANCE_EXPECTATIONS: return 18;
                case DiscriminationBias.GENDER_SPECIFIC_STEREOTYPES: return 19;
                case DiscriminationBias.TIGHTROPE_BIAS: return 20;
                case DiscriminationBias.MICROAGGRESSIONS: return 21;
                case DiscriminationBias.PERFORMANCE_ATTRIBUTION_BIAS: return 22;
                case DiscriminationBias.IN_MEDIA_AND_ADVERTISING: return 23;
                case DiscriminationBias.UNCONSCIOUS_BIAS_IN_COMMUNICATION: return 24;
                case DiscriminationBias.PROVE_IT_AGAIN_BIAS: return 25;
                case DiscriminationBias.HETERONORMATIVITAET_BIAS: return 26;
                case DiscriminationBias.BENEVOLENTER_SEXISMUS_BIAS: return 27;
                default: return 0;
            }
        }

        public static DiscriminationBias ValueOf(int discriminationBias)
        {
            switch (discriminationBias)
            {
                case 0: return DiscriminationBias.NONE;
                case 1: return DiscriminationBias.ACCESS_TO_FUNDING;
                case 2: return DiscriminationBias.GENDER_PAY_GAP;
                case 3: return DiscriminationBias.UNDERVALUATION_OF_WOMEN_LED_BUSINESSES;
                case 4: return DiscriminationBias.RISK_AVERSION_BIAS;
                case 5: return DiscriminationBias.CONFIRMATION_BIAS;
                case 6: return DiscriminationBias.TOKENISM;
                case 7: return DiscriminationBias.IN_PERCEPTION_OF_LEADERSHIP_ABILITIES;
                case 8: return DiscriminationBias.RACIST_AND_ETHNIC_BIASES;
                case 9: return DiscriminationBias.SOCIOECONOMIC_BIASES;
                case 10: return DiscriminationBias.AGE_AND_GENERATIONAL_BIASES;
                case 11: return DiscriminationBias.SEXUALITY_RELATED_BIASES;
                case 12: return DiscriminationBias.AGAINST_WOMEN_WITH_DISABILITIES;
                case 13: return DiscriminationBias.STEREOTYPES_AGAINST_WOMEN_IN_NON_TRADITIONAL_INDUSTRIES;
                case 14: return DiscriminationBias.CULTURAL_AND_RELIGIOUS_BIASES;
                case 15: return DiscriminationBias.MATERNAL_BIAS;
                case 16: return DiscriminationBias.AGAINST_WOMEN_WITH_CHILDREN;
                case 17: return DiscriminationBias.EXPECTATIONS_REGARDING_FAMILY_PLANNING;
                case 18: return DiscriminationBias.WORK_LIFE_BALANCE_EXPECTATIONS;
                case 19: return DiscriminationBias.GENDER_SPECIFIC_STEREOTYPES;
                case 20: return DiscriminationBias.TIGHTROPE_BIAS;
                case 21: return DiscriminationBias.MICROAGGRESSIONS;
                case 22: return DiscriminationBias.PERFORMANCE_ATTRIBUTION_BIAS;
                case 23: return DiscriminationBias.IN_MEDIA_AND_ADVERTISING;
                case 24: return DiscriminationBias.UNCONSCIOUS_BIAS_IN_COMMUNICATION;
                case 25: return DiscriminationBias.PROVE_IT_AGAIN_BIAS;
                case 26: return DiscriminationBias.HETERONORMATIVITAET_BIAS;
                case 27: return DiscriminationBias.BENEVOLENTER_SEXISMUS_BIAS;
                default: return DiscriminationBias.NONE;
            }
        }

        internal static string GetInformationString(DiscriminationBias discriminationBias)
        {
            switch (discriminationBias)
            {
                case DiscriminationBias.NONE: return "";
                case DiscriminationBias.ACCESS_TO_FUNDING:
                    return "Hier wird folgender Bias relevant: Finanzierungszugang";
                case DiscriminationBias.GENDER_PAY_GAP: return "Hier wird folgender Bias relevant: Gender Pay Gap";
                case DiscriminationBias.UNDERVALUATION_OF_WOMEN_LED_BUSINESSES:
                    return "Hier wird folgender Bias relevant: Unterbewertung weiblich geführter Unternehmen";
                case DiscriminationBias.RISK_AVERSION_BIAS:
                    return "Hier wird folgender Bias relevant: Risk Aversion Bias";
                case DiscriminationBias.CONFIRMATION_BIAS:
                    return "Hier wird folgender Bias relevant: Bestätigungsverzerrung";
                case DiscriminationBias.TOKENISM: return "Hier wird folgender Bias relevant: Tokenism";
                case DiscriminationBias.IN_PERCEPTION_OF_LEADERSHIP_ABILITIES:
                    return "Hier wird folgender Bias relevant: Bias in der Wahrnehmung von Führungsfähigkeiten";
                case DiscriminationBias.RACIST_AND_ETHNIC_BIASES:
                    return "Hier wird folgender Bias relevant: Rassistische und ethnische Biases";
                case DiscriminationBias.SOCIOECONOMIC_BIASES:
                    return "Hier wird folgender Bias relevant: Sozioökonomische Biases";
                case DiscriminationBias.AGE_AND_GENERATIONAL_BIASES:
                    return "Hier wird folgender Bias relevant: Alter- und Generationen-Biases";
                case DiscriminationBias.SEXUALITY_RELATED_BIASES:
                    return "Hier wird folgender Bias relevant: Sexualitätsbezogene Biases";
                case DiscriminationBias.AGAINST_WOMEN_WITH_DISABILITIES:
                    return "Hier wird folgender Bias relevant: Biases gegenüber Frauen mit Behinderungen";
                case DiscriminationBias.STEREOTYPES_AGAINST_WOMEN_IN_NON_TRADITIONAL_INDUSTRIES:
                    return
                        "Hier wird folgender Bias relevant: Stereotype gegenüber Frauen in nicht-traditionellen Branchen";
                case DiscriminationBias.CULTURAL_AND_RELIGIOUS_BIASES:
                    return "Hier wird folgender Bias relevant: Kulturelle und religiöse Biases";
                case DiscriminationBias.MATERNAL_BIAS: return "Hier wird folgender Bias relevant: Maternal Bias";
                case DiscriminationBias.AGAINST_WOMEN_WITH_CHILDREN:
                    return "Hier wird folgender Bias relevant: Biases gegenüber Frauen mit Kindern";
                case DiscriminationBias.EXPECTATIONS_REGARDING_FAMILY_PLANNING:
                    return "Hier wird folgender Bias relevant: Erwartungshaltung bezüglich Familienplanung";
                case DiscriminationBias.WORK_LIFE_BALANCE_EXPECTATIONS:
                    return "Hier wird folgender Bias relevant: Work-Life-Balance-Erwartungen";
                case DiscriminationBias.GENDER_SPECIFIC_STEREOTYPES:
                    return "Hier wird folgender Bias relevant: Geschlechtsspezifische Stereotypen";
                case DiscriminationBias.TIGHTROPE_BIAS:
                    return "Hier wird folgender Bias relevant: Doppelte Bindung (Tightrope Bias)";
                case DiscriminationBias.MICROAGGRESSIONS: return "Hier wird folgender Bias relevant: Mikroaggressionen";
                case DiscriminationBias.PERFORMANCE_ATTRIBUTION_BIAS:
                    return "Hier wird folgender Bias relevant: Leistungsattributions-Bias";
                case DiscriminationBias.IN_MEDIA_AND_ADVERTISING:
                    return "Hier wird folgender Bias relevant: Bias in Medien und Werbung";
                case DiscriminationBias.UNCONSCIOUS_BIAS_IN_COMMUNICATION:
                    return "Hier wird folgender Bias relevant: Unbewusste Bias in der Kommunikation";
                case DiscriminationBias.PROVE_IT_AGAIN_BIAS:
                    return "Hier wird folgender Bias relevant: Prove-it-Again-Bias";
                case DiscriminationBias.HETERONORMATIVITAET_BIAS:
                    return "Hier wird folgender Bias relevant: Heteronormativität";
                case DiscriminationBias.BENEVOLENTER_SEXISMUS_BIAS:
                    return "Hier wird folgender Bias relevant: Benevolenter Sexismus";
                default: return "";
            }
        }
    }
}