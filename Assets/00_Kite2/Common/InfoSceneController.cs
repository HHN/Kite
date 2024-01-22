using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoSceneController : SceneController
{
    [SerializeField] private GameObject aboutTheAppMenu;
    [SerializeField] private GameObject discriminationMenu;
    [SerializeField] private GameObject successStoryMenu;
    [SerializeField] private GameObject ressourcesMenu;
    [SerializeField] private GameObject legalInformationMenu;
    [SerializeField] private GameObject feedbackMenu;
    [SerializeField] private GameObject researchMenu;
    [SerializeField] private GameObject financesMenu;
    [SerializeField] private GameObject intersectionalBiasesMenu;
    [SerializeField] private GameObject roleMenu;
    [SerializeField] private GameObject carrerBiasesMenu;
    [SerializeField] private GameObject scrollView;

    [SerializeField] private Button hideButton;
    private int clickCounter = 0;

    [SerializeField] private Button searchButton;
    [SerializeField] private Sprite searchIcon;
    [SerializeField] private Sprite xIcon;
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
        BackStackManager.Instance().Push(SceneNames.INFO_SCENE);

        hideButton.onClick.AddListener(delegate { OnHiddenButtonPressed(); });
        searchButton.onClick.AddListener(delegate { OnStopSearchButton(); });

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

        InitMemory();
    }

    public void OnSearchValueChanged()
    {
        if (string.IsNullOrEmpty(searchInputField.text))
        {
            StopSearch();
            return;
        }
        Search(searchInputField.text);
    }

    public void StopSearch()
    {
        searchButton.image.sprite = searchIcon;
        searchInputField.text = string.Empty;
    }

    public void Search(string value)
    {
        searchButton.image.sprite = xIcon;
    }

    public void OnStopSearchButton()
    {
        if (string.IsNullOrEmpty(searchInputField.text))
        {
            return;
        }
        StopSearch();
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

    private void LoadFeedbackRoleManagementScene()
    {
        SceneLoader.LoadFeedbackRoleManagementScene();
    }

    public void OnDevelopmentHistoryButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Entwicklungsgeschichte");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnFunctionInformationButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Funktionsweise");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnAboutTheTeamButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Team und Mitwirkende");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnFaqButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Häufig gestellte Fragen (FAQ)");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnTechnicalDetailsButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Technische Details");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnUpdateInformationButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Zukünftige Updates und Pläne");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnPrivacyInformationButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Datenschutzerklärung");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnUsingConditionsButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Nutzungsbedingungen");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnAccessToFinancesButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Finanzierungszugang");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnGenderPayGapButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Gender Pay Gap");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnBadRatingForFemaleFoundersButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Unterbewertung weiblich geführter Unternehmen");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnRiskAversionBiasButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Risk Aversion Bias");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnConfirmationBiasButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Bestätigungsverzerrung");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnTokenismButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Tokenism");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnPerceptionOfLeadershipBiasButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Bias in der Wahrnehmung von Führungsfähigkeiten");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnRacismButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Rassistische und ethnische Biases");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnSocioEconomicBiasButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Sozioökonomische Biases");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnAgeAndGenerationButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Alter- und Generationen-Biases");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnSexBiasButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Sexualitätsbezogene Biases");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnBiasAgainstDisabledWomenButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Biases gegenüber Frauen mit Behinderungen");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnNonTraditionalAreaBiasButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Stereotype gegenüber Frauen in nicht-traditionellen Branchen");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnCulturalBiasButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Kulturelle und religiöse Biases");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnMaternalBiasButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Maternal Bias");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnMomBiasButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Biases gegenüber Frauen mit Kindern");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnFamilyBiasButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Erwartungshaltung bezüglich Familienplanung");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnWorkLifeBalanceBiasButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Work-Life-Balance-Erwartungen");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnGenderSpecificBiasButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Geschlechtsspezifische Stereotypen");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnTightropeBiasButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Doppelte Bindung (Tightrope Bias)");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnMicoAggressionBiasButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Mikroaggressionen");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnPerformanceAttributionBiasButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Leistungsattributions-Bias");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnMediaAndMarketingBiasButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Bias in Medien und Werbung");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnCommunicationBiasButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Unbewusste Bias in der Kommunikation");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnProveItAgainBiasButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Prove-it-Again-Bias");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnFounder01ButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Julia Schneider");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnFounder02ButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Lena Hoffmann");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnFounder03ButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Sarah Meyer");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnFounder04ButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Laura Weber");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnFounder05ButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Lisa Wagner");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnFounder06ButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Katharina Becker");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnSupportProgramButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Förderprogramme");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OncommunityButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Netzwerke und Communitys");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnEducationRessourcesButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Bildungsressourcen");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnMentoringButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Beratung und Mentoring");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnToolsAndSoftwareButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Tools und Software");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnLegalAdviceButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Rechtliche und finanzielle Beratung");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnAntiDiscriminationLawButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Gleichstellungs- und Antidiskriminierungsgesetze");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnParentsLawButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Mutterschutz und Elternzeit");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnSameRightsLawButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Gleichstellungsrichtlinien in der Wirtschaft");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnGiveFeedbackButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Feedback einreichen");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnDiscussionButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Diskussionsforum");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnSharSuccessStoryButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Erfolgsgeschichten teilen");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnCommunityEventsButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Community-Events");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnUserSurveyButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Nutzerumfragen");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnSupportNetworkButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Unterstützungsnetzwerk");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnCurrentResearchButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Aktuelle Forschungsergebnisse");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnStatisticsButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Statistiken und Daten");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnCaseResearchButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Fallstudien");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnExpertOpinionButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Expertinnenmeinungen");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnGlobalPerspectiveButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Globale Perspektiven");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void OnTopicSpecificButtonPressed()
    {
        InfoTextManager.Instance.SetTextHead("Themenspezifische Vertiefungen");
        InfoTextManager.Instance.SetTextBody("");
        SceneLoader.LoadInfoTextScene();
    }

    public void InitMemory()
    {
        InfoSceneMemory memory = SceneMemoryManager.Instance().GetMemoryOfInfoScene();

        if (memory == null)
        {
            return;
        }
        aboutTheAppMenu.GetComponent<DropDownMenu>().SetMenuOpen(memory.isAboutTheAppMenuOpen);
        discriminationMenu.GetComponent<DropDownMenu>().SetMenuOpen(memory.isDiscriminationMenuOpen);
        successStoryMenu.GetComponent<DropDownMenu>().SetMenuOpen(memory.isSuccessStoryMenuOpen);
        ressourcesMenu.GetComponent<DropDownMenu>().SetMenuOpen(memory.isRessourcesMenuOpen);
        legalInformationMenu.GetComponent<DropDownMenu>().SetMenuOpen(memory.isLegalInformationMenuOpen);
        feedbackMenu.GetComponent<DropDownMenu>().SetMenuOpen(memory.isFeedbackMenuOpen);
        researchMenu.GetComponent<DropDownMenu>().SetMenuOpen(memory.isResearchMenuOpen);
        financesMenu.GetComponent<DropDownMenu>().SetMenuOpen(memory.isFinancesMenuOpen);
        intersectionalBiasesMenu.GetComponent<DropDownMenu>().SetMenuOpen(memory.isIntersectionalBiasesMenuOpen);
        roleMenu.GetComponent<DropDownMenu>().SetMenuOpen(memory.isRoleMenuOpen);
        carrerBiasesMenu.GetComponent<DropDownMenu>().SetMenuOpen(memory.isCarrerBiasesMenuOpen);
        searchInputField.text = memory.searchString;
        scrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = memory.scrollPosition;
        StartCoroutine(EnsureCorrectScrollPosition(memory.scrollPosition));
    }

    public IEnumerator EnsureCorrectScrollPosition(float value)
    {
        yield return null;
        Canvas.ForceUpdateCanvases();
        scrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = value;
    }

    public override void OnStop()
    {
        base.OnStop();

        InfoSceneMemory memory = new InfoSceneMemory();
        memory.isAboutTheAppMenuOpen = aboutTheAppMenu.GetComponent<DropDownMenu>().IsOpen();
        memory.isDiscriminationMenuOpen = discriminationMenu.GetComponent<DropDownMenu>().IsOpen();
        memory.isSuccessStoryMenuOpen = successStoryMenu.GetComponent<DropDownMenu>().IsOpen();
        memory.isRessourcesMenuOpen = ressourcesMenu.GetComponent<DropDownMenu>().IsOpen();
        memory.isLegalInformationMenuOpen = legalInformationMenu.GetComponent<DropDownMenu>().IsOpen();
        memory.isFeedbackMenuOpen = feedbackMenu.GetComponent<DropDownMenu>().IsOpen();
        memory.isResearchMenuOpen = researchMenu.GetComponent<DropDownMenu>().IsOpen();
        memory.isFinancesMenuOpen = financesMenu.GetComponent<DropDownMenu>().IsOpen();
        memory.isIntersectionalBiasesMenuOpen = intersectionalBiasesMenu.GetComponent<DropDownMenu>().IsOpen();
        memory.isRoleMenuOpen = roleMenu.GetComponent<DropDownMenu>().IsOpen();
        memory.isCarrerBiasesMenuOpen = carrerBiasesMenu.GetComponent<DropDownMenu>().IsOpen();
        memory.scrollPosition = scrollView.GetComponent<ScrollRect>().verticalNormalizedPosition;
        memory.searchString = searchInputField.text;
        SceneMemoryManager.Instance().SetMemoryOfInfoScene(memory);
    }
}
