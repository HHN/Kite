using System;
using System.Collections;
using System.Collections.Generic;
using Assets._Scripts.Biases;
using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    public class KnowledgeSceneController : MonoBehaviour
    {
        [SerializeField] private RectTransform contentRectTransform;

        [SerializeField] private GameObject infoText;
        [SerializeField] private GameObject biasGroups;
        [SerializeField] private GameObject biasInformation;
        [SerializeField] private GameObject biasDetailsObject;

        [SerializeField] private Sprite arrowLeftSprite;
        [SerializeField] private Sprite arrowDownSprite;

        [Header("Strukturelle wirtschaftliche Barrieren")] 
        [SerializeField] private GameObject barrierenInfoGroup;
        [SerializeField] private Image barrierenInfoButtonImage;
        [SerializeField] private Button barrierenInfoButton;
        [SerializeField] private Button finanzierungszugangButton;
        [SerializeField] private Button genderPayGapButton;
        [SerializeField] private Button unterbewertungWeiblichGefuehrterUnternehmenButton;
        [SerializeField] private Button riskAversionBiasButton;
        [SerializeField] private Button bestaetigungsVerzerrungButton;

        [Header("Gesellschaftliche Erwartungen & soziale Normen")] [SerializeField]
        private GameObject erwartungenNormenInfoGroup;
        [SerializeField] private Image erwartungenNormenInfoButtonImage;
        [SerializeField] private Button erwartungenNormenInfoButton;
        [SerializeField] private Button tokenismButton;
        [SerializeField] private Button biasInDerWahrnehmungVonFuehrungsFaehigkeitenButton;
        [SerializeField] private Button benevolenterSexismusButton;
        [SerializeField] private Button altersUndGenerationenBiasesButton;
        [SerializeField] private Button stereotypeGegenueberFrauenInNichtTraditionellenBranchenButton;

        [Header("Wahrnehmung & Führungsrollen")] [SerializeField]
        private GameObject wahrnehmungFuehrungsrollenInfoGroup;
        [SerializeField] private Image wahrnehmungFuehrungsrollenInfoButtonImage;
        [SerializeField] private Button wahrnehmungFuehrungsrollenInfoButton;
        [SerializeField] private Button heteronormativitaetButton;
        [SerializeField] private Button maternalBiasButton;
        [SerializeField] private Button erwartungshaltungBezueglichFamilienplanungButton;
        [SerializeField] private Button workLifeBalanceErwartungenButton;
        [SerializeField] private Button geschlechtsspezifischeStereotypeButton;

        [Header("Psychologische Barrieren & kommunikative Hindernisse")] [SerializeField]
        private GameObject barrierenHindernisseInfoGroup;
        [SerializeField] private Image barrierenHindernisseInfoButtonImage;
        [SerializeField] private Button barrierenHindernisseInfoButton;
        [SerializeField] private Button tightropeBiasButton;
        [SerializeField] private Button mikroaggressionenButton;
        [SerializeField] private Button leistungsattributionsBiasButton;
        [SerializeField] private Button unbewussteBiasInDerKommunikationButton;

        [Header("SearchBar")]
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button searchBarButton;
        [SerializeField] private Image searchBarImage;
        [SerializeField] private GameObject searchList;
        [SerializeField] private RectTransform searchBarRect;
        
        [SerializeField] private Button searchListFinanzierungszugangButton;
        [SerializeField] private Button searchListGenderPayGapButton;
        [SerializeField] private Button searchListUnterbewertungWeiblichGefuehrterUnternehmenButton;
        [SerializeField] private Button searchListRiskAversionBiasButton;
        [SerializeField] private Button searchListBestaetigungsVerzerrungButton;
        [SerializeField] private Button searchListTokenismButton;
        [SerializeField] private Button searchListBiasInDerWahrnehmungVonFuehrungsFaehigkeitenButton;
        [SerializeField] private Button searchListBenevolenterSexismusButton;
        [SerializeField] private Button searchListAltersUndGenerationenBiasesButton;
        [SerializeField] private Button searchListStereotypeGegenueberFrauenInNichtTraditionellenBranchenButton;
        [SerializeField] private Button searchListHeteronormativitaetButton;
        [SerializeField] private Button searchListMaternalBiasButton;
        [SerializeField] private Button searchListErwartungshaltungBezueglichFamilienplanungButton;
        [SerializeField] private Button searchListWorkLifeBalanceErwartungenButton;
        [SerializeField] private Button searchListGeschlechtsspezifischeStereotypeButton;
        [SerializeField] private Button searchListTightropeBiasButton;
        [SerializeField] private Button searchListMikroaggressionenButton;
        [SerializeField] private Button searchListLeistungsattributionsBiasButton;
        [SerializeField] private Button searchListUnbewussteBiasInDerKommunikationButton;

        private GameObject _currentBiasInformationChapter;
        
        private TMP_Text _biasDetailsText;

        private Dictionary<Button, Action> _buttonActions;
        
        private bool _barrienenInfoGroupActive;
        private bool _erwartungsInfoGroupActive;
        private bool _wahrnehmungFuehrungsrollenInfoGroupActive;
        private bool _barrierenHindernisseInfoGroupActive;

        public void Start()
        {
            BackStackManager.Instance().Push(SceneNames.KnowledgeScene);
            
            InitializeButtonActions();
            AddButtonListeners();
            
            _biasDetailsText = biasDetailsObject.GetComponentInChildren<TextMeshProUGUI>();

            if (inputField)
            {
                inputField.onValueChanged.AddListener(Search);
            }
            else
            {
                Debug.LogError("InputField ist nicht zugewiesen.");
            }
            
            FontSizeManager.Instance().UpdateAllTextComponents();
            StartCoroutine(RebuildLayout());
        }

        private void InitializeButtonActions()
        {
            _buttonActions = new Dictionary<Button, Action>
            {
                { barrierenInfoButton, OnBarrierenInfoButton },
                { erwartungenNormenInfoButton, OnErwartungenNormenInfoButton },
                { wahrnehmungFuehrungsrollenInfoButton, OnWahrnehmungFuehrungsrollenInfoButton },
                { barrierenHindernisseInfoButton, OnBarrierenHindernisseInfoButton },

                // Strukturelle wirtschaftliche Barrieren
                { finanzierungszugangButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.AccessToFunding) },
                { genderPayGapButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.GenderPayGap) },
                { unterbewertungWeiblichGefuehrterUnternehmenButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.UndervaluationOfWomenLedBusinesses) },
                { riskAversionBiasButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.RiskAversionBias) },
                { bestaetigungsVerzerrungButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.ConfirmationBias) },

                // Gesellschaftliche Erwartungen & soziale Normen
                { tokenismButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.Tokenism) },
                { biasInDerWahrnehmungVonFuehrungsFaehigkeitenButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.InPerceptionOfLeadershipAbilities) },
                { benevolenterSexismusButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.BenevolentSexismBias) },
                { altersUndGenerationenBiasesButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.AgeAndGenerationalBiases) },
                { stereotypeGegenueberFrauenInNichtTraditionellenBranchenButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.StereotypesAgainstWomenInNonTraditionalIndustries) },
                
                // Wahrnehmung & Führungsrollen
                { heteronormativitaetButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.Heteronormativity) },
                { maternalBiasButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.MaternalBias) },
                { erwartungshaltungBezueglichFamilienplanungButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.ExpectationsRegardingFamilyPlanning) },
                { workLifeBalanceErwartungenButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.WorkLifeBalanceExpectations) },
                { geschlechtsspezifischeStereotypeButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.GenderSpecificStereotypes) },

                // Psychologische Barrieren & kommunikative Hindernisse
                { tightropeBiasButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.TightropeBias) },
                { mikroaggressionenButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.Microaggressions) },
                { leistungsattributionsBiasButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.PerformanceAttributionBias) },
                { unbewussteBiasInDerKommunikationButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.UnconsciousBiasInCommunication) },
                
                { searchListFinanzierungszugangButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.AccessToFunding) },
                { searchListGenderPayGapButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.GenderPayGap) },
                { searchListUnterbewertungWeiblichGefuehrterUnternehmenButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.UndervaluationOfWomenLedBusinesses) },
                { searchListRiskAversionBiasButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.RiskAversionBias) },
                { searchListBestaetigungsVerzerrungButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.ConfirmationBias) },
                { searchListTokenismButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.Tokenism) },
                { searchListBiasInDerWahrnehmungVonFuehrungsFaehigkeitenButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.InPerceptionOfLeadershipAbilities) },
                { searchListBenevolenterSexismusButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.BenevolentSexismBias) },
                { searchListAltersUndGenerationenBiasesButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.AgeAndGenerationalBiases) },
                { searchListStereotypeGegenueberFrauenInNichtTraditionellenBranchenButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.StereotypesAgainstWomenInNonTraditionalIndustries) },
                { searchListHeteronormativitaetButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.Heteronormativity) },
                { searchListMaternalBiasButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.MaternalBias) },
                { searchListErwartungshaltungBezueglichFamilienplanungButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.ExpectationsRegardingFamilyPlanning) },
                { searchListWorkLifeBalanceErwartungenButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.WorkLifeBalanceExpectations) },
                { searchListGeschlechtsspezifischeStereotypeButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.GenderSpecificStereotypes) },
                { searchListTightropeBiasButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.TightropeBias) },
                { searchListMikroaggressionenButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.Microaggressions) },
                { searchListLeistungsattributionsBiasButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.PerformanceAttributionBias) },
                { searchListUnbewussteBiasInDerKommunikationButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.UnconsciousBiasInCommunication) },
                
                { searchBarButton, CloseSearchBar }
            };
        }

        private void AddButtonListeners()
        {
            foreach (var (button, action) in _buttonActions)
            {
                button.onClick.AddListener(() => action.Invoke());
            }
        }

        /// <summary>
        /// Zeigt detaillierte Informationen zu einem Bias-Thema.
        /// </summary>
        private void ShowBiasDetails(BiasDescriptionTexts.BiasType type)
        {
            if (searchList.activeInHierarchy) searchList.SetActive(false);
            
            infoText.SetActive(false);
            biasInformation.SetActive(false);
            barrierenInfoGroup.SetActive(false);

            biasDetailsObject.SetActive(true);
            _biasDetailsText.text = BiasDescriptionTexts.GetBiasText(type);
            
            FontSizeManager.Instance().UpdateAllTextComponents();
            StartCoroutine(RebuildLayout());
        }

        private void OnBarrierenInfoButton()
        {
            if (_barrienenInfoGroupActive)
            {
                finanzierungszugangButton.gameObject.SetActive(false);
                genderPayGapButton.gameObject.SetActive(false);
                unterbewertungWeiblichGefuehrterUnternehmenButton.gameObject.SetActive(false);
                riskAversionBiasButton.gameObject.SetActive(false);
                bestaetigungsVerzerrungButton.gameObject.SetActive(false);
                
                barrierenInfoButtonImage.sprite = arrowLeftSprite;
                
                _barrienenInfoGroupActive = false;
            }
            else
            {
                finanzierungszugangButton.gameObject.SetActive(true);
                genderPayGapButton.gameObject.SetActive(true);
                unterbewertungWeiblichGefuehrterUnternehmenButton.gameObject.SetActive(true);
                riskAversionBiasButton.gameObject.SetActive(true);
                bestaetigungsVerzerrungButton.gameObject.SetActive(true);
                
                barrierenInfoButtonImage.sprite = arrowDownSprite;
                
                _barrienenInfoGroupActive = true;

                _currentBiasInformationChapter = barrierenInfoGroup;
            }
            
            FontSizeManager.Instance().UpdateAllTextComponents();
            StartCoroutine(RebuildLayout());
        }

        private void OnErwartungenNormenInfoButton()
        {
            if (_erwartungsInfoGroupActive)
            {
                tokenismButton.gameObject.SetActive(false);
                biasInDerWahrnehmungVonFuehrungsFaehigkeitenButton.gameObject.SetActive(false);
                benevolenterSexismusButton.gameObject.SetActive(false);
                altersUndGenerationenBiasesButton.gameObject.SetActive(false);
                stereotypeGegenueberFrauenInNichtTraditionellenBranchenButton.gameObject.SetActive(false);
                
                erwartungenNormenInfoButtonImage.sprite = arrowLeftSprite;
                
                _erwartungsInfoGroupActive = false;
            }
            else
            {
                tokenismButton.gameObject.SetActive(true);
                biasInDerWahrnehmungVonFuehrungsFaehigkeitenButton.gameObject.SetActive(true);
                benevolenterSexismusButton.gameObject.SetActive(true);
                altersUndGenerationenBiasesButton.gameObject.SetActive(true);
                stereotypeGegenueberFrauenInNichtTraditionellenBranchenButton.gameObject.SetActive(true);
                
                erwartungenNormenInfoButtonImage.sprite = arrowDownSprite;
                
                _erwartungsInfoGroupActive = true;

                _currentBiasInformationChapter = erwartungenNormenInfoGroup;
            }

            FontSizeManager.Instance().UpdateAllTextComponents();
            StartCoroutine(RebuildLayout());
        }

        private void OnWahrnehmungFuehrungsrollenInfoButton()
        {
            if (_wahrnehmungFuehrungsrollenInfoGroupActive)
            {
                heteronormativitaetButton.gameObject.SetActive(false);
                maternalBiasButton.gameObject.SetActive(false);
                erwartungshaltungBezueglichFamilienplanungButton.gameObject.SetActive(false);
                workLifeBalanceErwartungenButton.gameObject.SetActive(false);
                geschlechtsspezifischeStereotypeButton.gameObject.SetActive(false);
                
                wahrnehmungFuehrungsrollenInfoButtonImage.sprite = arrowLeftSprite;
                
                _wahrnehmungFuehrungsrollenInfoGroupActive = false;
            }
            else
            {
                heteronormativitaetButton.gameObject.SetActive(true);
                maternalBiasButton.gameObject.SetActive(true);
                erwartungshaltungBezueglichFamilienplanungButton.gameObject.SetActive(true);
                workLifeBalanceErwartungenButton.gameObject.SetActive(true);
                geschlechtsspezifischeStereotypeButton.gameObject.SetActive(true);
                
                wahrnehmungFuehrungsrollenInfoButtonImage.sprite = arrowDownSprite;
                
                _wahrnehmungFuehrungsrollenInfoGroupActive = true;
                
                _currentBiasInformationChapter = wahrnehmungFuehrungsrollenInfoGroup;
            }
            
            FontSizeManager.Instance().UpdateAllTextComponents();
            StartCoroutine(RebuildLayout());
        }

        private void OnBarrierenHindernisseInfoButton()
        {
            if (_barrierenHindernisseInfoGroupActive)
            {
                tightropeBiasButton.gameObject.SetActive(false);
                mikroaggressionenButton.gameObject.SetActive(false);
                leistungsattributionsBiasButton.gameObject.SetActive(false);
                unbewussteBiasInDerKommunikationButton.gameObject.SetActive(false);
                
                barrierenHindernisseInfoButtonImage.sprite = arrowLeftSprite;
                
                _barrierenHindernisseInfoGroupActive = false;
            }
            else
            {
                tightropeBiasButton.gameObject.SetActive(true);
                mikroaggressionenButton.gameObject.SetActive(true);
                leistungsattributionsBiasButton.gameObject.SetActive(true);
                unbewussteBiasInDerKommunikationButton.gameObject.SetActive(true);
                
                barrierenHindernisseInfoButtonImage.sprite = arrowDownSprite;
                
                _barrierenHindernisseInfoGroupActive = true;
                
                _currentBiasInformationChapter = barrierenHindernisseInfoGroup;
            }

            FontSizeManager.Instance().UpdateAllTextComponents();
            StartCoroutine(RebuildLayout());
        }

        public void NavigateScene()
        {
            infoText.SetActive(true);

            if (biasInformation.activeInHierarchy)
            {
                SceneLoader.LoadFoundersBubbleScene();
            }
            else if (biasDetailsObject.activeInHierarchy)
            {
                biasInformation.SetActive(true);
                barrierenInfoButton.gameObject.SetActive(true);
                erwartungenNormenInfoButton.gameObject.SetActive(true);
                wahrnehmungFuehrungsrollenInfoButton.gameObject.SetActive(true);
                barrierenHindernisseInfoButton.gameObject.SetActive(true);
                
                CloseSearchBar();
                
                // _currentBiasInformationChapter.SetActive(true);
                biasDetailsObject.SetActive(false);
            }
        }

        private void Search(string input)
        {
            searchBarButton.gameObject.SetActive(true);
            searchBarImage.gameObject.SetActive(false);
            
            biasInformation.SetActive(false);
            searchList.SetActive(true);

            string inputLower = input.ToLower();
            bool hasResults = false;

            foreach (Transform entry in searchList.transform)
            {
                bool isActive = entry.name.ToLower().Contains(inputLower);
                entry.gameObject.SetActive(isActive);
                if (isActive) hasResults = true;
            }

            if (hasResults)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(searchList.GetComponent<VerticalLayoutGroup>().GetComponent<RectTransform>());
            }
        }

        private void CloseSearchBar()
        {
            inputField.text = "";
            
            searchBarButton.gameObject.SetActive(false);
            searchBarImage.gameObject.SetActive(true);

            if (searchList.activeInHierarchy)
            {
                searchList.SetActive(false);
            }

            if (!biasInformation.activeInHierarchy)
            {
                biasInformation.SetActive(true);
            }
        }

    private IEnumerator RebuildLayout()
    {
        yield return null; // Einen Frame warten
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentRectTransform);
    }
    }
}