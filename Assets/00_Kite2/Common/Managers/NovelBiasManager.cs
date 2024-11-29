using UnityEngine;

namespace _00_Kite2.Common.Managers
{
    public class NovelBiasManager
    {
        private static NovelBiasManager _instance;

        public bool IsBiasAccessToFundingRelevant { get; set; }
        public bool IsBiasGenderPayGapRelevant { get; set; }
        public bool IsBiasUndervaluationOfWomenLedBusinessesRelevant { get; set; }
        public bool IsBiasRiskAversionBiasRelevant { get; set; }
        public bool IsBiasConfirmationBiasRelevant { get; set; }
        public bool IsBiasTokenismRelevant { get; set; }
        public bool IsBiasInPerceptionOfLeadershipAbilitiesRelevant { get; set; }
        public bool IsBiasRacistAndEthnicBiasesRelevant { get; set; }
        public bool IsBiasSocioeconomicBiasesRelevant { get; set; }
        public bool IsBiasAgeAndGenerationalBiasesRelevant { get; set; }
        public bool IsBiasSexualityRelatedBiasesRelevant { get; set; }
        public bool IsBiasAgainstWomenWithDisabilitiesRelevant { get; set; }
        public bool IsBiasStereotypesAgainstWomenInNonTraditionalIndustriesRelevant { get; set; }
        public bool IsBiasCulturalAndReligiousBiasesRelevant { get; set; }
        public bool IsBiasMaternalBiasRelevant { get; set; }
        public bool IsBiasAgainstWomenWithChildrenRelevant { get; set; }
        public bool IsBiasExpectationsRegardingFamilyPlanningRelevant { get; set; }
        public bool IsBiasWorkLifeBalanceExpectationsRelevant { get; set; }
        public bool IsBiasGenderSpecificStereotypesRelevant { get; set; }
        public bool IsBiasTightropeBiasRelevant { get; set; }
        public bool IsBiasMicroaggressionsRelevant { get; set; }
        public bool IsBiasPerformanceAttributionBiasRelevant { get; set; }
        public bool IsBiasInMediaAndAdvertisingRelevant { get; set; }
        public bool IsBiasUnconsciousBiasInCommunicationRelevant { get; set; }
        public bool IsBiasProveItAgainBiasRelevant { get; set; }
        public bool IsBiasHeteronormativitaetBiasRelevant { get; set; }
        public bool IsBiasBenevolenterSexismusBiasRelevant { get; set; }

        private NovelBiasManager()
        {
            IsBiasAccessToFundingRelevant = false;
            IsBiasGenderPayGapRelevant = false;
            IsBiasUndervaluationOfWomenLedBusinessesRelevant = false;
            IsBiasRiskAversionBiasRelevant = false;
            IsBiasConfirmationBiasRelevant = false;
            IsBiasTokenismRelevant = false;
            IsBiasInPerceptionOfLeadershipAbilitiesRelevant = false;
            IsBiasRacistAndEthnicBiasesRelevant = false;
            IsBiasSocioeconomicBiasesRelevant = false;
            IsBiasAgeAndGenerationalBiasesRelevant = false;
            IsBiasSexualityRelatedBiasesRelevant = false;
            IsBiasAgainstWomenWithDisabilitiesRelevant = false;
            IsBiasStereotypesAgainstWomenInNonTraditionalIndustriesRelevant = false;
            IsBiasCulturalAndReligiousBiasesRelevant = false;
            IsBiasMaternalBiasRelevant = false;
            IsBiasAgainstWomenWithChildrenRelevant = false;
            IsBiasExpectationsRegardingFamilyPlanningRelevant = false;
            IsBiasWorkLifeBalanceExpectationsRelevant = false;
            IsBiasGenderSpecificStereotypesRelevant = false;
            IsBiasTightropeBiasRelevant = false;
            IsBiasMicroaggressionsRelevant = false;
            IsBiasPerformanceAttributionBiasRelevant = false;
            IsBiasInMediaAndAdvertisingRelevant = false;
            IsBiasUnconsciousBiasInCommunicationRelevant = false;
            IsBiasProveItAgainBiasRelevant = false;
            IsBiasHeteronormativitaetBiasRelevant = false;
            IsBiasBenevolenterSexismusBiasRelevant = false;
        }

        public static NovelBiasManager Instance()
        {
            if (_instance == null)
            {
                _instance = new NovelBiasManager();
            }

            return _instance;
        }

        public void MarkBiasAsRelevant(DiscriminationBias biasName)
        {
            switch (biasName)
            {
                case DiscriminationBias.ACCESS_TO_FUNDING:
                    IsBiasAccessToFundingRelevant = true;
                    break;
                case DiscriminationBias.GENDER_PAY_GAP:
                    IsBiasGenderPayGapRelevant = true;
                    break;
                case DiscriminationBias.UNDERVALUATION_OF_WOMEN_LED_BUSINESSES:
                    IsBiasUndervaluationOfWomenLedBusinessesRelevant = true;
                    break;
                case DiscriminationBias.RISK_AVERSION_BIAS:
                    IsBiasRiskAversionBiasRelevant = true;
                    break;
                case DiscriminationBias.CONFIRMATION_BIAS:
                    IsBiasConfirmationBiasRelevant = true;
                    break;
                case DiscriminationBias.TOKENISM:
                    IsBiasTokenismRelevant = true;
                    break;
                case DiscriminationBias.IN_PERCEPTION_OF_LEADERSHIP_ABILITIES:
                    IsBiasInPerceptionOfLeadershipAbilitiesRelevant = true;
                    break;
                case DiscriminationBias.RACIST_AND_ETHNIC_BIASES:
                    IsBiasRacistAndEthnicBiasesRelevant = true;
                    break;
                case DiscriminationBias.SOCIOECONOMIC_BIASES:
                    IsBiasSocioeconomicBiasesRelevant = true;
                    break;
                case DiscriminationBias.AGE_AND_GENERATIONAL_BIASES:
                    IsBiasAgeAndGenerationalBiasesRelevant = true;
                    break;
                case DiscriminationBias.SEXUALITY_RELATED_BIASES:
                    IsBiasSexualityRelatedBiasesRelevant = true;
                    break;
                case DiscriminationBias.AGAINST_WOMEN_WITH_DISABILITIES:
                    IsBiasAgainstWomenWithDisabilitiesRelevant = true;
                    break;
                case DiscriminationBias.STEREOTYPES_AGAINST_WOMEN_IN_NON_TRADITIONAL_INDUSTRIES:
                    IsBiasStereotypesAgainstWomenInNonTraditionalIndustriesRelevant = true;
                    break;
                case DiscriminationBias.CULTURAL_AND_RELIGIOUS_BIASES:
                    IsBiasCulturalAndReligiousBiasesRelevant = true;
                    break;
                case DiscriminationBias.MATERNAL_BIAS:
                    IsBiasMaternalBiasRelevant = true;
                    break;
                case DiscriminationBias.AGAINST_WOMEN_WITH_CHILDREN:
                    IsBiasAgainstWomenWithChildrenRelevant = true;
                    break;
                case DiscriminationBias.EXPECTATIONS_REGARDING_FAMILY_PLANNING:
                    IsBiasExpectationsRegardingFamilyPlanningRelevant = true;
                    break;
                case DiscriminationBias.WORK_LIFE_BALANCE_EXPECTATIONS:
                    IsBiasWorkLifeBalanceExpectationsRelevant = true;
                    break;
                case DiscriminationBias.GENDER_SPECIFIC_STEREOTYPES:
                    IsBiasGenderSpecificStereotypesRelevant = true;
                    break;
                case DiscriminationBias.TIGHTROPE_BIAS:
                    IsBiasTightropeBiasRelevant = true;
                    break;
                case DiscriminationBias.MICROAGGRESSIONS:
                    IsBiasMicroaggressionsRelevant = true;
                    break;
                case DiscriminationBias.PERFORMANCE_ATTRIBUTION_BIAS:
                    IsBiasPerformanceAttributionBiasRelevant = true;
                    break;
                case DiscriminationBias.IN_MEDIA_AND_ADVERTISING:
                    IsBiasInMediaAndAdvertisingRelevant = true;
                    break;
                case DiscriminationBias.UNCONSCIOUS_BIAS_IN_COMMUNICATION:
                    IsBiasUnconsciousBiasInCommunicationRelevant = true;
                    break;
                case DiscriminationBias.PROVE_IT_AGAIN_BIAS:
                    IsBiasProveItAgainBiasRelevant = true;
                    break;
                case DiscriminationBias.HETERONORMATIVITAET_BIAS:
                    IsBiasHeteronormativitaetBiasRelevant = true;
                    break;
                case DiscriminationBias.BENEVOLENTER_SEXISMUS_BIAS:
                    IsBiasBenevolenterSexismusBiasRelevant = true;
                    break;
                default:
                    Debug.LogWarning("Bias not Recognized: " + biasName);
                    break;
            }
        }

        public static void Clear()
        {
            _instance = new NovelBiasManager();
        }
    }
}