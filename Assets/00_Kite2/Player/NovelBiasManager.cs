using UnityEngine;

public class NovelBiasManager
{
    private static NovelBiasManager instance;

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
    }

    public static NovelBiasManager Instance()
    {
        if (instance == null)
        {
            instance = new NovelBiasManager();
        }
        return instance;
    }

    public void MarkBiasAsRelevant(string biasName)
    {
        switch (biasName)
        {
            case BiasName.ACCESS_TO_FUNDING:
                IsBiasAccessToFundingRelevant = true;
                break;
            case BiasName.GENDER_PAY_GAP:
                IsBiasGenderPayGapRelevant = true;
                break;
            case BiasName.UNDERVALUATION_OF_WOMEN_LED_BUSINESSES:
                IsBiasUndervaluationOfWomenLedBusinessesRelevant = true;
                break;
            case BiasName.RISK_AVERSION_BIAS:
                IsBiasRiskAversionBiasRelevant = true;
                break;
            case BiasName.CONFIRMATION_BIAS:
                IsBiasConfirmationBiasRelevant = true;
                break;
            case BiasName.TOKENISM:
                IsBiasTokenismRelevant = true;
                break;
            case BiasName.IN_PERCEPTION_OF_LEADERSHIP_ABILITIES:
                IsBiasInPerceptionOfLeadershipAbilitiesRelevant = true;
                break;
            case BiasName.RACIST_AND_ETHNIC_BIASES:
                IsBiasRacistAndEthnicBiasesRelevant = true;
                break;
            case BiasName.SOCIOECONOMIC_BIASES:
                IsBiasSocioeconomicBiasesRelevant = true;
                break;
            case BiasName.AGE_AND_GENERATIONAL_BIASES:
                IsBiasAgeAndGenerationalBiasesRelevant = true;
                break;
            case BiasName.SEXUALITY_RELATED_BIASES:
                IsBiasSexualityRelatedBiasesRelevant = true;
                break;
            case BiasName.AGAINST_WOMEN_WITH_DISABILITIES:
                IsBiasAgainstWomenWithDisabilitiesRelevant = true;
                break;
            case BiasName.STEREOTYPES_AGAINST_WOMEN_IN_NON_TRADITIONAL_INDUSTRIES:
                IsBiasStereotypesAgainstWomenInNonTraditionalIndustriesRelevant = true;
                break;
            case BiasName.CULTURAL_AND_RELIGIOUS_BIASES:
                IsBiasCulturalAndReligiousBiasesRelevant = true;
                break;
            case BiasName.MATERNAL_BIAS:
                IsBiasMaternalBiasRelevant = true;
                break;
            case BiasName.AGAINST_WOMEN_WITH_CHILDREN:
                IsBiasAgainstWomenWithChildrenRelevant = true;
                break;
            case BiasName.EXPECTATIONS_REGARDING_FAMILY_PLANNING:
                IsBiasExpectationsRegardingFamilyPlanningRelevant = true;
                break;
            case BiasName.WORK_LIFE_BALANCE_EXPECTATIONS:
                IsBiasWorkLifeBalanceExpectationsRelevant = true;
                break;
            case BiasName.GENDER_SPECIFIC_STEREOTYPES:
                IsBiasGenderSpecificStereotypesRelevant = true;
                break;
            case BiasName.TIGHTROPE_BIAS:
                IsBiasTightropeBiasRelevant = true;
                break;
            case BiasName.MICROAGGRESSIONS:
                IsBiasMicroaggressionsRelevant = true;
                break;
            case BiasName.PERFORMANCE_ATTRIBUTION_BIAS:
                IsBiasPerformanceAttributionBiasRelevant = true;
                break;
            case BiasName.IN_MEDIA_AND_ADVERTISING:
                IsBiasInMediaAndAdvertisingRelevant = true;
                break;
            case BiasName.UNCONSCIOUS_BIAS_IN_COMMUNICATION:
                IsBiasUnconsciousBiasInCommunicationRelevant = true;
                break;
            case BiasName.PROVE_IT_AGAIN_BIAS:
                IsBiasProveItAgainBiasRelevant = true;
                break;
            default:
                Debug.LogWarning("Bias not Recognized: " + biasName);
                break;
        }
    }

    public static void Clear()
    {
        instance = new NovelBiasManager();
    }
}
