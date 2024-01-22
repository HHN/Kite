using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoSceneController : SceneController
{
    [SerializeField] private Button hideButton;
    private int clickCounter = 0;

    [SerializeField] private Button searchButton;
    [SerializeField] private TMP_InputField searchInputField;

    // About the App Buttons
    [SerializeField] private Button developmentHistoryButton;
    [SerializeField] private Button functionInformationButton;
    [SerializeField] private Button aboutTheTeamButton;
    [SerializeField] private Button faq_Button;
    [SerializeField] private Button technicalDetailsButton;
    [SerializeField] private Button updateInformationButton;
    [SerializeField] private Button privacyInformationButton;
    [SerializeField] private Button usingConditionsButton;

    // Discrimination Buttons
    [SerializeField] private Button accessToFinancesButton;
    [SerializeField] private Button genderPayGapButton;
    [SerializeField] private Button badRatingForFemaleFoundersButton;
    [SerializeField] private Button riskAversionBiasButton;
    [SerializeField] private Button confirmationBiasButton;
    [SerializeField] private Button tokenismButton;
    [SerializeField] private Button perceptionOfLeadershipBiasButton;
    [SerializeField] private Button racismButton;
    [SerializeField] private Button socioEconomicBiasButton;
    [SerializeField] private Button ageAndGenerationBiasButton;
    [SerializeField] private Button sexBiasButton;
    [SerializeField] private Button biasAgainsDiasbledWomenButton;
    [SerializeField] private Button nonTraditionalAreaBiasButton;
    [SerializeField] private Button culturalBiasButton;
    [SerializeField] private Button maternalBiasButton;
    [SerializeField] private Button momBiasButton;
    [SerializeField] private Button familyBiasButton;
    [SerializeField] private Button workLifeBalancaBiasButton;
    [SerializeField] private Button genderSpecificBiasButton;
    [SerializeField] private Button tightropeBiasButton;
    [SerializeField] private Button micoAggressionBiasButton;
    [SerializeField] private Button performanceAttributionBiasButton;
    [SerializeField] private Button mediaAndMarketingBiasButton;
    [SerializeField] private Button communicationBiasButton;
    [SerializeField] private Button proveItAgainBiasButton;

    // Success Storys Buttons
    [SerializeField] private Button founder01;
    [SerializeField] private Button founder02;
    [SerializeField] private Button founder03;
    [SerializeField] private Button founder04;
    [SerializeField] private Button founder05;
    [SerializeField] private Button founder06;

    // Ressources Buttons
    [SerializeField] private Button supportProgramsButton;
    [SerializeField] private Button communityButton;
    [SerializeField] private Button educationRessorucesButton;
    [SerializeField] private Button mentoringButton;
    [SerializeField] private Button toolsAndSoftwareButton;
    [SerializeField] private Button legalAdviceButton;

    // Legal Infromation Buttons
    [SerializeField] private Button antiDiscriminationLawButton;
    [SerializeField] private Button parentsLawButton;
    [SerializeField] private Button sameRightsLawButton;

    // Community Buttons
    [SerializeField] private Button giveFeedbackButton;
    [SerializeField] private Button discussionButton;
    [SerializeField] private Button shareSuccessStoryButton;
    [SerializeField] private Button communityEventsButton;
    [SerializeField] private Button userSurveyButton;
    [SerializeField] private Button supportNetworkButton;

    // Research Buttons
    [SerializeField] private Button currentResearchResultsButton;
    [SerializeField] private Button statisticsButton;
    [SerializeField] private Button caseResearchButton;
    [SerializeField] private Button expertOpinionButton;
    [SerializeField] private Button globalPerspectivesButton;
    [SerializeField] private Button topicSpecificButton;

    private void Start()
    {
        hideButton.onClick.AddListener(delegate { OnHiddenButtonPressed(); });
        searchButton.onClick.AddListener(delegate { OnSearchButtonPressed(); });

        developmentHistoryButton.onClick.AddListener(delegate { OnDevelopmentHistoryButtonPressed(); });
        functionInformationButton.onClick.AddListener(delegate { OnFunctionInformationButtonPressed(); });
        aboutTheTeamButton.onClick.AddListener(delegate { OnAboutTheTeamButtonPressed(); });
        faq_Button.onClick.AddListener(delegate { OnFaqButtonPressed(); });
        technicalDetailsButton.onClick.AddListener(delegate { OnTechnicalDetailsButtonPressed(); });
        updateInformationButton.onClick.AddListener(delegate { OnUpdateInformationButtonPressed(); });
        privacyInformationButton.onClick.AddListener(delegate { OnPrivacyInformationButtonPressed(); });
        usingConditionsButton.onClick.AddListener(delegate { OnUsingConditionsButtonPressed(); });

        accessToFinancesButton.onClick.AddListener(delegate { OnAccessToFinancesButtonPressed(); });
        genderPayGapButton.onClick.AddListener(delegate { OnGenderPayGapButtonPressed(); });
        badRatingForFemaleFoundersButton.onClick.AddListener(delegate { OnBadRatingForFemaleFoundersButtonPressed(); });
        riskAversionBiasButton.onClick.AddListener(delegate { OnRiskAversionBiasButtonPressed(); });
        confirmationBiasButton.onClick.AddListener(delegate { OnConfirmationBiasButtonPressed(); });
        tokenismButton.onClick.AddListener(delegate { OnTokenismButtonPressed(); });
        perceptionOfLeadershipBiasButton.onClick.AddListener(delegate { OnPerceptionOfLeadershipBiasButtonPressed(); });
        
        racismButton.onClick.AddListener(delegate { OnRacismButtonPressed(); });
        socioEconomicBiasButton.onClick.AddListener(delegate { OnSocioEconomicBiasButtonPressed(); });
        ageAndGenerationBiasButton.onClick.AddListener(delegate { OnAgeAndGenerationButtonPressed(); });
        sexBiasButton.onClick.AddListener(delegate { OnSexBiasButtonPressed(); });
        biasAgainsDiasbledWomenButton.onClick.AddListener(delegate { OnBiasAgainstDisabledWomenButtonPressed(); });
        nonTraditionalAreaBiasButton.onClick.AddListener(delegate { OnNonTraditionalAreaBiasButtonPressed(); });
        culturalBiasButton.onClick.AddListener(delegate { OnCulturalBiasButtonPressed(); });
        maternalBiasButton.onClick.AddListener(delegate { OnMaternalBiasButtonPressed(); });
        momBiasButton.onClick.AddListener(delegate { OnMomBiasButtonPressed(); });
        familyBiasButton.onClick.AddListener(delegate { OnFamilyBiasButtonPressed(); });
        workLifeBalancaBiasButton.onClick.AddListener(delegate { OnWorkLifeBalanceBiasButtonPressed(); });
        genderSpecificBiasButton.onClick.AddListener(delegate { OnGenderSpecificBiasButtonPressed(); });
        tightropeBiasButton.onClick.AddListener(delegate { OnTightropeBiasButtonPressed(); });
        micoAggressionBiasButton.onClick.AddListener(delegate { OnMicoAggressionBiasButtonPressed(); });
        performanceAttributionBiasButton.onClick.AddListener(delegate { OnPerformanceAttributionBiasButtonPressed(); });
        mediaAndMarketingBiasButton.onClick.AddListener(delegate { OnMediaAndMarketingBiasButtonPressed(); });
        communicationBiasButton.onClick.AddListener(delegate { OnCommunicationBiasButtonPressed(); });
        proveItAgainBiasButton.onClick.AddListener(delegate { OnProveItAgainBiasButtonPressed(); });

        founder01.onClick.AddListener(delegate { OnFounder01ButtonPressed(); });
        founder02.onClick.AddListener(delegate { OnFounder02ButtonPressed(); });
        founder03.onClick.AddListener(delegate { OnFounder03ButtonPressed(); });
        founder04.onClick.AddListener(delegate { OnFounder04ButtonPressed(); });
        founder05.onClick.AddListener(delegate { OnFounder05ButtonPressed(); });
        founder06.onClick.AddListener(delegate { OnFounder06ButtonPressed(); });

        supportProgramsButton.onClick.AddListener(delegate { OnSupportProgramButtonPressed(); });
        communityButton.onClick.AddListener(delegate { OncommunityButtonPressed(); });
        mentoringButton.onClick.AddListener(delegate { OnMentoringButtonPressed(); });
        toolsAndSoftwareButton.onClick.AddListener(delegate { OnToolsAndSoftwareButtonPressed(); });
        legalAdviceButton.onClick.AddListener(delegate { OnLegalAdviceButtonPressed(); });
        educationRessorucesButton.onClick.AddListener(delegate { OnEducationRessourcesButtonPressed(); });
       
        antiDiscriminationLawButton.onClick.AddListener(delegate { OnAntiDiscriminationLawButtonPressed(); });
        parentsLawButton.onClick.AddListener(delegate { OnParentsLawButtonPressed(); });
        sameRightsLawButton.onClick.AddListener(delegate { OnSameRightsLawButtonPressed(); });

        giveFeedbackButton.onClick.AddListener(delegate { OnGiveFeedbackButtonPressed(); });
        discussionButton.onClick.AddListener(delegate { OnDiscussionButtonPressed(); });
        shareSuccessStoryButton.onClick.AddListener(delegate { OnSharSuccessStoryButtonPressed(); });
        communityEventsButton.onClick.AddListener(delegate { OnCommunityEventsButtonPressed(); });
        userSurveyButton.onClick.AddListener(delegate { OnUserSurveyButtonPressed(); });
        supportNetworkButton.onClick.AddListener(delegate { OnSupportNetworkButtonPressed(); });

        currentResearchResultsButton.onClick.AddListener(delegate { OnCurrentResearchButtonPressed(); });
        statisticsButton.onClick.AddListener(delegate { OnStatisticsButtonPressed(); });
        caseResearchButton.onClick.AddListener(delegate { OnCaseResearchButtonPressed(); });
        expertOpinionButton.onClick.AddListener(delegate { OnExpertOpinionButtonPressed(); });
        globalPerspectivesButton.onClick.AddListener(delegate { OnGlobalPerspectiveButtonPressed(); });
        topicSpecificButton.onClick.AddListener(delegate { OnTopicSpecificButtonPressed(); });
    }

    public void OnHiddenButtonPressed()
    {
        clickCounter++;
        if (clickCounter == 10)
        {
            clickCounter = 0;
            LoadFeedbackRoleManagementScene();
        }
    }

    public void OnSearchButtonPressed()
    {
    }

    private void LoadFeedbackRoleManagementScene()
    {
        SceneLoader.LoadFeedbackRoleManagementScene();
    }

    public void OnDevelopmentHistoryButtonPressed()
    {
    }

    public void OnFunctionInformationButtonPressed()
    {
    }

    public void OnAboutTheTeamButtonPressed()
    {
    }

    public void OnFaqButtonPressed()
    {
    }

    public void OnTechnicalDetailsButtonPressed()
    {
    }

    public void OnUpdateInformationButtonPressed()
    {
    }

    public void OnPrivacyInformationButtonPressed()
    {
    }

    public void OnUsingConditionsButtonPressed()
    {
    }

    public void OnAccessToFinancesButtonPressed()
    {
    }

    public void OnGenderPayGapButtonPressed()
    {
    }

    public void OnBadRatingForFemaleFoundersButtonPressed()
    {
    }

    public void OnRiskAversionBiasButtonPressed()
    {
    }

    public void OnConfirmationBiasButtonPressed()
    {
    }

    public void OnTokenismButtonPressed()
    {
    }

    public void OnPerceptionOfLeadershipBiasButtonPressed()
    {
    }

    public void OnRacismButtonPressed()
    {
    }

    public void OnSocioEconomicBiasButtonPressed()
    {
    }

    public void OnAgeAndGenerationButtonPressed()
    {
    }

    public void OnSexBiasButtonPressed()
    {
    }

    public void OnBiasAgainstDisabledWomenButtonPressed()
    {
    }

    public void OnNonTraditionalAreaBiasButtonPressed()
    {
    }

    public void OnCulturalBiasButtonPressed()
    {
    }

    public void OnMaternalBiasButtonPressed()
    {
    }

    public void OnMomBiasButtonPressed()
    {
    }

    public void OnFamilyBiasButtonPressed()
    {
    }

    public void OnWorkLifeBalanceBiasButtonPressed()
    {
    }

    public void OnGenderSpecificBiasButtonPressed()
    {
    }

    public void OnTightropeBiasButtonPressed()
    {
    }

    public void OnMicoAggressionBiasButtonPressed()
    {
    }

    public void OnPerformanceAttributionBiasButtonPressed()
    {
    }

    public void OnMediaAndMarketingBiasButtonPressed()
    {
    }

    public void OnCommunicationBiasButtonPressed()
    {
    }

    public void OnProveItAgainBiasButtonPressed()
    {
    }

    public void OnFounder01ButtonPressed()
    {
    }

    public void OnFounder02ButtonPressed()
    {
    }

    public void OnFounder03ButtonPressed()
    {
    }

    public void OnFounder04ButtonPressed()
    {
    }

    public void OnFounder05ButtonPressed()
    {
    }

    public void OnFounder06ButtonPressed()
    {
    }

    public void OnSupportProgramButtonPressed()
    {
    }

    public void OncommunityButtonPressed()
    {
    }

    public void OnEducationRessourcesButtonPressed()
    {
    }

    public void OnMentoringButtonPressed()
    {
    }

    public void OnToolsAndSoftwareButtonPressed()
    {
    }

    public void OnLegalAdviceButtonPressed()
    {
    }

    public void OnAntiDiscriminationLawButtonPressed()
    {
    }

    public void OnParentsLawButtonPressed()
    {
    }

    public void OnSameRightsLawButtonPressed()
    {
    }

    public void OnGiveFeedbackButtonPressed()
    {
    }

    public void OnDiscussionButtonPressed()
    {
    }

    public void OnSharSuccessStoryButtonPressed()
    {
    }

    public void OnCommunityEventsButtonPressed()
    {
    }

    public void OnUserSurveyButtonPressed()
    {
    }

    public void OnSupportNetworkButtonPressed()
    {
    }

    public void OnCurrentResearchButtonPressed()
    {
    }

    public void OnStatisticsButtonPressed()
    {
    }

    public void OnCaseResearchButtonPressed()
    {
    }

    public void OnExpertOpinionButtonPressed()
    {
    }

    public void OnGlobalPerspectiveButtonPressed()
    {
    }

    public void OnTopicSpecificButtonPressed()
    {
    }
}
