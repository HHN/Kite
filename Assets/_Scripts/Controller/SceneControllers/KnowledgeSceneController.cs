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
    /// <summary>
    /// Controls the behavior and management of the Knowledge Scene in the application.
    /// Manages UI elements, user input, and navigation within the scene, as well as performing
    /// initialization tasks and layout rebuilding.
    /// </summary>
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

        [Header("Gesellschaftliche Erwartungen & soziale Normen")] 
        [SerializeField] private GameObject erwartungenNormenInfoGroup;
        [SerializeField] private Image erwartungenNormenInfoButtonImage;
        [SerializeField] private Button erwartungenNormenInfoButton;
        [SerializeField] private Button tokenismButton;
        [SerializeField] private Button biasInDerWahrnehmungVonFuehrungsFaehigkeitenButton;
        [SerializeField] private Button benevolenterSexismusButton;
        [SerializeField] private Button altersUndGenerationenBiasesButton;
        [SerializeField] private Button stereotypeGegenueberFrauenInNichtTraditionellenBranchenButton;

        [Header("Wahrnehmung & Führungsrollen")] 
        [SerializeField]
        private GameObject wahrnehmungFuehrungsrollenInfoGroup;
        [SerializeField] private Image wahrnehmungFuehrungsrollenInfoButtonImage;
        [SerializeField] private Button wahrnehmungFuehrungsrollenInfoButton;
        [SerializeField] private Button heteronormativitaetButton;
        [SerializeField] private Button maternalBiasButton;
        [SerializeField] private Button erwartungshaltungBezueglichFamilienplanungButton;
        [SerializeField] private Button workLifeBalanceErwartungenButton;
        [SerializeField] private Button geschlechtsspezifischeStereotypeButton;

        [Header("Psychologische Barrieren & kommunikative Hindernisse")] 
        [SerializeField] private GameObject barrierenHindernisseInfoGroup;
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

        /// <summary>
        /// Initializes the Knowledge Scene, setting up navigation, buttons,
        /// input field listeners, and ensuring the UI layout is updated correctly.
        /// </summary>
        public void Start()
        {
            BackStackManager.Instance().Push(SceneNames.KnowledgeScene);

            InitializeButtonActions();
            AddButtonListeners();

            _biasDetailsText = null;
            if (biasDetailsObject)
            {
                _biasDetailsText = biasDetailsObject.GetComponentInChildren<TextMeshProUGUI>();
                if (_biasDetailsText == null)
                {
                    Debug.LogWarning("Kein TextMeshProUGUI unter biasDetailsObject gefunden.");
                }
            }
            else
            {
                Debug.LogWarning("biasDetailsObject ist nicht zugewiesen.");
            }

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

        /// <summary>
        /// Configures the button actions and assigns the appropriate event handlers
        /// to ensure proper interaction behavior within the Knowledge Scene.
        /// </summary>
        private void InitializeButtonActions()
        {
            _buttonActions = new Dictionary<Button, Action>
            {
                { barrierenInfoButton, OnBarrierenInfoButton },
                { erwartungenNormenInfoButton, OnErwartungenNormenInfoButton },
                { wahrnehmungFuehrungsrollenInfoButton, OnWahrnehmungFuehrungsrollenInfoButton },
                { barrierenHindernisseInfoButton, OnBarrierenHindernisseInfoButton },

                // Structural economic barriers
                { finanzierungszugangButton, () => ShowBiasDetails(BiasType.AccessToFinancing) },
                { genderPayGapButton, () => ShowBiasDetails(BiasType.GenderPayGap) },
                { unterbewertungWeiblichGefuehrterUnternehmenButton, () => ShowBiasDetails(BiasType.UndervaluationFemaleManagedCompany) },
                { riskAversionBiasButton, () => ShowBiasDetails(BiasType.RiskAversionBias) },
                { bestaetigungsVerzerrungButton, () => ShowBiasDetails(BiasType.ConfirmationBias) },

                // Societal expectations & social norms
                { tokenismButton, () => ShowBiasDetails(BiasType.Tokenism) },
                { biasInDerWahrnehmungVonFuehrungsFaehigkeitenButton, () => ShowBiasDetails(BiasType.BiasInThePerceptionOfLeadershipSkills) },
                { benevolenterSexismusButton, () => ShowBiasDetails(BiasType.BenevolentSexism) },
                { altersUndGenerationenBiasesButton, () => ShowBiasDetails(BiasType.AgeAndGenerationsBiases) },
                { stereotypeGegenueberFrauenInNichtTraditionellenBranchenButton, () => ShowBiasDetails(BiasType.StereotypesAboutWomenInNonTraditionalIndustries) },

                // Perception & leadership roles
                { heteronormativitaetButton, () => ShowBiasDetails(BiasType.Heteronormativity) },
                { maternalBiasButton, () => ShowBiasDetails(BiasType.BiasesAgainstWomenWithChildren) },
                { erwartungshaltungBezueglichFamilienplanungButton, () => ShowBiasDetails(BiasType.ExpectationsRegardingFamilyPlanning) },
                { workLifeBalanceErwartungenButton, () => ShowBiasDetails(BiasType.WorkLifeBalanceExpectations) },
                { geschlechtsspezifischeStereotypeButton, () => ShowBiasDetails(BiasType.GenderSpecificStereotypes) },

                // Psychological barriers & communication obstacles
                { tightropeBiasButton, () => ShowBiasDetails(BiasType.TightropeBias) },
                { mikroaggressionenButton, () => ShowBiasDetails(BiasType.Microaggression) },
                { leistungsattributionsBiasButton, () => ShowBiasDetails(BiasType.PerformanceAttributionBias) },
                { unbewussteBiasInDerKommunikationButton, () => ShowBiasDetails(BiasType.UnconsciousBiasInCommunication) },

                { searchListFinanzierungszugangButton, () => ShowBiasDetails(BiasType.AccessToFinancing) },
                { searchListGenderPayGapButton, () => ShowBiasDetails(BiasType.GenderPayGap) },
                { searchListUnterbewertungWeiblichGefuehrterUnternehmenButton, () => ShowBiasDetails(BiasType.UndervaluationFemaleManagedCompany) },
                { searchListRiskAversionBiasButton, () => ShowBiasDetails(BiasType.RiskAversionBias) },
                { searchListBestaetigungsVerzerrungButton, () => ShowBiasDetails(BiasType.ConfirmationBias) },
                { searchListTokenismButton, () => ShowBiasDetails(BiasType.Tokenism) },
                { searchListBiasInDerWahrnehmungVonFuehrungsFaehigkeitenButton, () => ShowBiasDetails(BiasType.BiasInThePerceptionOfLeadershipSkills) },
                { searchListBenevolenterSexismusButton, () => ShowBiasDetails(BiasType.BenevolentSexism) },
                { searchListAltersUndGenerationenBiasesButton, () => ShowBiasDetails(BiasType.AgeAndGenerationsBiases) },
                { searchListStereotypeGegenueberFrauenInNichtTraditionellenBranchenButton, () => ShowBiasDetails(BiasType.StereotypesAboutWomenInNonTraditionalIndustries) },
                { searchListHeteronormativitaetButton, () => ShowBiasDetails(BiasType.Heteronormativity) },
                { searchListMaternalBiasButton, () => ShowBiasDetails(BiasType.BiasesAgainstWomenWithChildren) },
                { searchListErwartungshaltungBezueglichFamilienplanungButton, () => ShowBiasDetails(BiasType.ExpectationsRegardingFamilyPlanning) },
                { searchListWorkLifeBalanceErwartungenButton, () => ShowBiasDetails(BiasType.WorkLifeBalanceExpectations) },
                { searchListGeschlechtsspezifischeStereotypeButton, () => ShowBiasDetails(BiasType.GenderSpecificStereotypes) },
                { searchListTightropeBiasButton, () => ShowBiasDetails(BiasType.TightropeBias) },
                { searchListMikroaggressionenButton, () => ShowBiasDetails(BiasType.Microaggression) },
                { searchListLeistungsattributionsBiasButton, () => ShowBiasDetails(BiasType.PerformanceAttributionBias) },
                { searchListUnbewussteBiasInDerKommunikationButton, () => ShowBiasDetails(BiasType.UnconsciousBiasInCommunication) },

                { searchBarButton, CloseSearchBar }
            };
        }

        /// <summary>
        /// Adds click event listeners to all buttons in the Knowledge Scene, binding them
        /// to their associated actions defined in the button-to-action dictionary.
        /// </summary>
        private void AddButtonListeners()
        {
            foreach (var (button, action) in _buttonActions)
            {
                button.onClick.AddListener(() => action.Invoke());
            }
        }

        /// <summary>
        /// Displays the details of a specified bias type by activating the bias details UI
        /// and updating the corresponding content text.
        /// </summary>
        /// <param name="type">The type of bias to display, represented as a BiasType enum.</param>
        private void ShowBiasDetails(BiasType type)
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

        /// <summary>
        /// Toggles the visibility of the "Strukturelle wirtschaftliche Barrieren" information group,
        /// updating button states, UI layout, and sprite appearance accordingly.
        /// </summary>
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

        /// <summary>
        /// Toggles the visibility of the "Erwartungen und Normen" information group,
        /// updating button states, UI layout, and sprite appearance accordingly.
        /// </summary>
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

        /// <summary>
        /// Toggles the visibility of the "Wahrnehmung & Führungsrollen" information group,
        /// updating button states, UI layout, and sprite appearance accordingly.
        /// </summary>
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

        /// <summary>
        /// Toggles the visibility of the "Psychologische Barrieren & kommunikative Hindernisse" information group,
        /// updating button states, UI layout, and sprite appearance accordingly.
        /// </summary>
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

        /// <summary>
        /// Navigates the Knowledge Scene by toggling the visibility of UI elements depending on their current state.
        /// Adjusts the display of information and initiates scene transitions when necessary.
        /// Ensures the correct layout and content are presented in the scene.
        /// </summary>
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

                biasDetailsObject.SetActive(false);
            }
        }

        /// <summary>
        /// Filters and displays results in the search list based on the provided input.
        /// Updates the visibility of the search button, search bar image, and relevant UI elements.
        /// </summary>
        /// <param name="input">The string input provided by the user for filtering search results.</param>
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

        /// <summary>
        /// Closes the search bar by clearing the input field, hiding the search bar button,
        /// showing the search bar image, and adjusting the visibility of the search list
        /// and bias information accordingly.
        /// </summary>
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

        /// <summary>
        /// Forces an immediate rebuild of the layout for the specified content rect transform.
        /// This ensures that the UI elements are properly arranged after changes to their properties or structure.
        /// </summary>
        /// <returns>
        /// An enumerator that waits for a single frame before applying the layout rebuild operation.
        /// </returns>
        private IEnumerator RebuildLayout()
        {
            yield return null;
            LayoutRebuilder.ForceRebuildLayoutImmediate(contentRectTransform);
        }
    }
}