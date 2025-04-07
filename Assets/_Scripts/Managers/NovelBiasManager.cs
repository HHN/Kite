using Assets._Scripts.Novel;
using UnityEngine;

namespace Assets._Scripts.Managers
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

        public void MarkBiasAsRelevant(string biasName)
        {
            //TODO: Umbauen, damit die entsprechenden Biases in einer Liste gespeichert werden, welche weiterverarbeitet wird. Booleankaskade kann raus.

            //switch (biasName)
            //{
            //    case DiscriminationBias.AccessToFunding:
            //        IsBiasAccessToFundingRelevant = true;
            //        break;
            //    case DiscriminationBias.GenderPayGap:
            //        IsBiasGenderPayGapRelevant = true;
            //        break;
            //    case DiscriminationBias.UndervaluationOfWomenLedBusinesses:
            //        IsBiasUndervaluationOfWomenLedBusinessesRelevant = true;
            //        break;
            //    case DiscriminationBias.RiskAversionBias:
            //        IsBiasRiskAversionBiasRelevant = true;
            //        break;
            //    case DiscriminationBias.ConfirmationBias:
            //        IsBiasConfirmationBiasRelevant = true;
            //        break;
            //    case DiscriminationBias.Tokenism:
            //        IsBiasTokenismRelevant = true;
            //        break;
            //    case DiscriminationBias.InPerceptionOfLeadershipAbilities:
            //        IsBiasInPerceptionOfLeadershipAbilitiesRelevant = true;
            //        break;
            //    case DiscriminationBias.RacistAndEthnicBiases:
            //        IsBiasRacistAndEthnicBiasesRelevant = true;
            //        break;
            //    case DiscriminationBias.SocioeconomicBiases:
            //        IsBiasSocioeconomicBiasesRelevant = true;
            //        break;
            //    case DiscriminationBias.AgeAndGenerationalBiases:
            //        IsBiasAgeAndGenerationalBiasesRelevant = true;
            //        break;
            //    case DiscriminationBias.SexualityRelatedBiases:
            //        IsBiasSexualityRelatedBiasesRelevant = true;
            //        break;
            //    case DiscriminationBias.AgainstWomenWithDisabilities:
            //        IsBiasAgainstWomenWithDisabilitiesRelevant = true;
            //        break;
            //    case DiscriminationBias.StereotypesAgainstWomenInNonTraditionalIndustries:
            //        IsBiasStereotypesAgainstWomenInNonTraditionalIndustriesRelevant = true;
            //        break;
            //    case DiscriminationBias.CulturalAndReligiousBiases:
            //        IsBiasCulturalAndReligiousBiasesRelevant = true;
            //        break;
            //    case DiscriminationBias.MaternalBias:
            //        IsBiasMaternalBiasRelevant = true;
            //        break;
            //    case DiscriminationBias.AgainstWomenWithChildren:
            //        IsBiasAgainstWomenWithChildrenRelevant = true;
            //        break;
            //    case DiscriminationBias.ExpectationsRegardingFamilyPlanning:
            //        IsBiasExpectationsRegardingFamilyPlanningRelevant = true;
            //        break;
            //    case DiscriminationBias.WorkLifeBalanceExpectations:
            //        IsBiasWorkLifeBalanceExpectationsRelevant = true;
            //        break;
            //    case DiscriminationBias.GenderSpecificStereotypes:
            //        IsBiasGenderSpecificStereotypesRelevant = true;
            //        break;
            //    case DiscriminationBias.TightropeBias:
            //        IsBiasTightropeBiasRelevant = true;
            //        break;
            //    case DiscriminationBias.Microaggressions:
            //        IsBiasMicroaggressionsRelevant = true;
            //        break;
            //    case DiscriminationBias.PerformanceAttributionBias:
            //        IsBiasPerformanceAttributionBiasRelevant = true;
            //        break;
            //    case DiscriminationBias.InMediaAndAdvertising:
            //        IsBiasInMediaAndAdvertisingRelevant = true;
            //        break;
            //    case DiscriminationBias.UnconsciousBiasInCommunication:
            //        IsBiasUnconsciousBiasInCommunicationRelevant = true;
            //        break;
            //    case DiscriminationBias.ProveItAgainBias:
            //        IsBiasProveItAgainBiasRelevant = true;
            //        break;
            //    case DiscriminationBias.HeteronormativitaetBias:
            //        IsBiasHeteronormativitaetBiasRelevant = true;
            //        break;
            //    case DiscriminationBias.BenevolenterSexismusBias:
            //        IsBiasBenevolenterSexismusBiasRelevant = true;
            //        break;
            //    default:
            //        Debug.LogWarning("Bias not Recognized: " + biasName);
            //        break;
            //}
        }

        public static void Clear()
        {
            _instance = new NovelBiasManager();
        }
    }
}