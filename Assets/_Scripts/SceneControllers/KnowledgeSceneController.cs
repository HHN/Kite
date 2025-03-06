using System;
using System.Collections.Generic;
using Assets._Scripts.Biases;
using Assets._Scripts.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.SceneControllers
{
    public class KnowledgeSceneController : MonoBehaviour
    {
        [SerializeField] private GameObject infoText;
        [SerializeField] private GameObject biasGroups;
        [SerializeField] private GameObject biasInformation;
        [SerializeField] private GameObject biasDetailsObject;
        
        [SerializeField] private Button barrierenButton;
        [SerializeField] private Button erwartungenNormenButton;
        [SerializeField] private Button wahrnehmungFuehrungsrollenButton;
        [SerializeField] private Button barrierenHindernisseButton;

        [Header("Strukturelle wirtschaftliche Barrieren")] [SerializeField]
        private GameObject barrierenInfoGroup;
        [SerializeField] private Button barrierenInfoButton;
        [SerializeField] private Button finanzierungszugangButton;
        [SerializeField] private Button genderPayGapButton;
        [SerializeField] private Button unterbewertungWeiblichGefuehrterUnternehmenButton;
        [SerializeField] private Button riskAversionBiasButton;
        [SerializeField] private Button bestaetigungsVerzerrungButton;

        [Header("Gesellschaftliche Erwartungen & soziale Normen")] [SerializeField]
        private GameObject erwartungenNormenInfoGroup;
        [SerializeField] private Button erwartungenNormenInfoButton;
        [SerializeField] private Button tokenismButton;
        [SerializeField] private Button biasInDerWahrnehmungVonFuehrungsFaehigkeitenButton;
        [SerializeField] private Button benevolenterSexismusButton;
        [SerializeField] private Button altersUndGenerationenBiasesButton;
        [SerializeField] private Button stereotypeGegenueberFrauenInNichtTraditionellenBranchenButton;

        [Header("Wahrnehmung & Führungsrollen")] [SerializeField]
        private GameObject wahrnehmungFuehrungsrollenInfoGroup;
        [SerializeField] private Button wahrnehmungFuehrungsrollenInfoButton;
        [SerializeField] private Button heteronormativitaetButton;
        [SerializeField] private Button maternalBiasButton;
        [SerializeField] private Button erwartungshaltungBezueglichFamilienplanungButton;
        [SerializeField] private Button workLifeBalanceErwartungenButton;
        [SerializeField] private Button geschlechtsspezifischeStereotypeButton;

        [Header("Psychologische Barrieren & kommunikative Hindernisse")] [SerializeField]
        private GameObject barrierenHindernisseInfoGroup;
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
        
        private enum SceneState
        {
            MainCategories,
            BiasCategories,
            BiasDetails
        }

        private SceneState _currentSceneState;

        public void Start()
        {
            InitializeButtonActions();
            AddButtonListeners();
            
            _biasDetailsText = biasDetailsObject.GetComponentInChildren<TextMeshProUGUI>();
            _currentSceneState = SceneState.MainCategories;

            if (inputField != null)
            {
                inputField.onValueChanged.AddListener(Search);
            }
            else
            {
                Debug.LogError("InputField ist nicht zugewiesen.");
            }
        }

        private void InitializeButtonActions()
        {
            _buttonActions = new Dictionary<Button, Action>
            {
                { barrierenButton, () => ShowCategory(barrierenInfoGroup) },
                { erwartungenNormenButton, () => ShowCategory(erwartungenNormenInfoGroup) },
                { wahrnehmungFuehrungsrollenButton, () => ShowCategory(wahrnehmungFuehrungsrollenInfoGroup) },
                { barrierenHindernisseButton, () => ShowCategory(barrierenHindernisseInfoGroup) },

                { barrierenInfoButton, OnBarrierenInfoButton },
                { erwartungenNormenInfoButton, OnErwartungenNormenInfoButton },
                { wahrnehmungFuehrungsrollenInfoButton, OnWahrnehmungFuehrungsrollenInfoButton },
                { barrierenHindernisseInfoButton, OnBarrierenHindernisseInfoButton },

                // Strukturelle wirtschaftliche Barrieren
                { finanzierungszugangButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.Finanzierungszugang) },
                { genderPayGapButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.GenderPayGap) },
                { unterbewertungWeiblichGefuehrterUnternehmenButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.UnterbewertungWeiblichGefuehrterUnternehmen) },
                { riskAversionBiasButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.RiskAversionBias) },
                { bestaetigungsVerzerrungButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.ConfirmationBias) },

                // Gesellschaftliche Erwartungen & soziale Normen
                { tokenismButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.Tokenism) },
                { biasInDerWahrnehmungVonFuehrungsFaehigkeitenButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.BiasInDerWahrnehmungVonFuehrungsfaehigkeiten) },
                { benevolenterSexismusButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.BenevolenterSexismus) },
                { altersUndGenerationenBiasesButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.AltersUndGenerationenBiases) },
                { stereotypeGegenueberFrauenInNichtTraditionellenBranchenButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.Stereotype) },
                
                // Wahrnehmung & Führungsrollen
                { heteronormativitaetButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.Heteronormativitaet) },
                { maternalBiasButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.MaternalBias) },
                { erwartungshaltungBezueglichFamilienplanungButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.FamilienplanungBias) },
                { workLifeBalanceErwartungenButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.WorkLifeBalance) },
                { geschlechtsspezifischeStereotypeButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.GeschlechtsspezifischeStereotype) },

                // Psychologische Barrieren & kommunikative Hindernisse
                { tightropeBiasButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.TightropeBias) },
                { mikroaggressionenButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.Mikroagressionen) },
                { leistungsattributionsBiasButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.LeistungsattributionsBias) },
                { unbewussteBiasInDerKommunikationButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.UnbewussteBiasInDerKommunikation) },
                
                { searchListFinanzierungszugangButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.Finanzierungszugang) },
                { searchListGenderPayGapButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.GenderPayGap) },
                { searchListUnterbewertungWeiblichGefuehrterUnternehmenButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.UnterbewertungWeiblichGefuehrterUnternehmen) },
                { searchListRiskAversionBiasButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.RiskAversionBias) },
                { searchListBestaetigungsVerzerrungButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.ConfirmationBias) },
                { searchListTokenismButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.Tokenism) },
                { searchListBiasInDerWahrnehmungVonFuehrungsFaehigkeitenButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.BiasInDerWahrnehmungVonFuehrungsfaehigkeiten) },
                { searchListBenevolenterSexismusButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.BenevolenterSexismus) },
                { searchListAltersUndGenerationenBiasesButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.AltersUndGenerationenBiases) },
                { searchListStereotypeGegenueberFrauenInNichtTraditionellenBranchenButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.Stereotype) },
                { searchListHeteronormativitaetButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.Heteronormativitaet) },
                { searchListMaternalBiasButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.MaternalBias) },
                { searchListErwartungshaltungBezueglichFamilienplanungButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.FamilienplanungBias) },
                { searchListWorkLifeBalanceErwartungenButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.WorkLifeBalance) },
                { searchListGeschlechtsspezifischeStereotypeButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.GeschlechtsspezifischeStereotype) },
                { searchListTightropeBiasButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.TightropeBias) },
                { searchListMikroaggressionenButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.Mikroagressionen) },
                { searchListLeistungsattributionsBiasButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.LeistungsattributionsBias) },
                { searchListUnbewussteBiasInDerKommunikationButton, () => ShowBiasDetails(BiasDescriptionTexts.BiasType.UnbewussteBiasInDerKommunikation) },
                
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
        /// Aktiviert eine bestimmte Bias-Kategorie und deaktiviert die anderen.
        /// </summary>
        private void ShowCategory(GameObject activeGroup)
        {
            infoText.SetActive(true);
            biasGroups.SetActive(false);
            biasInformation.SetActive(true);

            barrierenInfoGroup.SetActive(false);
            erwartungenNormenInfoGroup.SetActive(false);
            wahrnehmungFuehrungsrollenInfoGroup.SetActive(false);
            barrierenHindernisseInfoGroup.SetActive(false);

            activeGroup.SetActive(true);
            _currentBiasInformationChapter = activeGroup;
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
            _biasDetailsText.text = BiasDescriptionTexts.GetStringByType(type);
        }

        private void OnBarrierenInfoButton()
        {
            infoText.SetActive(true);
            biasGroups.SetActive(true);
            biasInformation.SetActive(false);

            barrierenInfoGroup.SetActive(true);
            erwartungenNormenInfoGroup.SetActive(false);
            wahrnehmungFuehrungsrollenInfoGroup.SetActive(false);
            barrierenHindernisseInfoGroup.SetActive(false);
        }

        private void OnErwartungenNormenInfoButton()
        {
            infoText.SetActive(true);
            biasGroups.SetActive(true);
            biasInformation.SetActive(false);

            barrierenInfoGroup.SetActive(false);
            erwartungenNormenInfoGroup.SetActive(true);
            wahrnehmungFuehrungsrollenInfoGroup.SetActive(false);
            barrierenHindernisseInfoGroup.SetActive(false);
        }

        private void OnWahrnehmungFuehrungsrollenInfoButton()
        {
            infoText.SetActive(true);
            biasGroups.SetActive(true);
            biasInformation.SetActive(false);

            barrierenInfoGroup.SetActive(false);
            erwartungenNormenInfoGroup.SetActive(false);
            wahrnehmungFuehrungsrollenInfoGroup.SetActive(true);
            barrierenHindernisseInfoGroup.SetActive(false);
        }

        private void OnBarrierenHindernisseInfoButton()
        {
            infoText.SetActive(true);
            biasGroups.SetActive(true);
            biasInformation.SetActive(false);

            barrierenInfoGroup.SetActive(false);
            erwartungenNormenInfoGroup.SetActive(false);
            wahrnehmungFuehrungsrollenInfoGroup.SetActive(false);
            barrierenHindernisseInfoGroup.SetActive(true);
        }

        public void NavigateScene()
        {
            infoText.SetActive(true);

            if (biasGroups.activeInHierarchy)
            {
                SceneLoader.LoadFoundersBubbleScene();
            }
            else if (biasInformation.activeInHierarchy)
            {
                biasGroups.SetActive(true);
                biasInformation.SetActive(false);
            }
            else if (biasDetailsObject.activeInHierarchy)
            {
                biasInformation.SetActive(true);
                _currentBiasInformationChapter.SetActive(true);
                biasDetailsObject.SetActive(false);
            }
        }

        private void Search(string input)
        {
            searchBarButton.gameObject.SetActive(true);
            searchBarImage.gameObject.SetActive(false);
            
            biasGroups.SetActive(false);
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

            if (!biasGroups.activeInHierarchy)
            {
                biasGroups.SetActive(true);
            }
        }
    }
}