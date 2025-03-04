using System.Collections.Generic;
using Assets._Scripts.Biases;
using Assets._Scripts.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Assets._Scripts.SceneControllers
{
    public class KnowledgeSceneController : MonoBehaviour
    {
        [SerializeField] private GameObject infoText;
        
        [SerializeField] private Button barrierenButton;
        [SerializeField] private Button erwartungenNormenButton;
        [SerializeField] private Button wahrnehmungFuehrungsrollenButton;
        [SerializeField] private Button barrierenHindernisseButton;

        [SerializeField] private GameObject biasGroups;
        [SerializeField] private GameObject biasInformation;

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

        [SerializeField] private GameObject biasDetailsObject;

        private TMP_Text _biasDetailsText;

        private Dictionary<Button, System.Action> _buttonActions;

        public void Start()
        {
            InitializeButtonActions();
            AddButtonListeners();
        }

        private void InitializeButtonActions()
        {
            _buttonActions = new Dictionary<Button, System.Action>
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
                { finanzierungszugangButton, () => ShowBiasDetails(BiasDescriptionTexts.FINANZIERUNGSZUGANG_HEADLINE, BiasDescriptionTexts.FINANZIERUNGSZUGANG_DESCRIPTION_PREVIEW, BiasDescriptionTexts.FINANZIERUNGSZUGANG_DESCRIPTION) },
                { genderPayGapButton, () => ShowBiasDetails(BiasDescriptionTexts.GENDER_PAY_GAP_HEADLINE, BiasDescriptionTexts.GENDER_PAY_GAP_DESCRIPTION_PREVIEW, BiasDescriptionTexts.GENDER_PAY_GAP_DESCRIPTION) },
                // { unterbewertungWeiblichGefuehrterUnternehmenButton, () => ShowBiasDetails(BiasDescriptionTexts.UNTERBEWERTUNG_WEIBLICHER_UNTERNEHMEN) },
                // { riskAversionBiasButton, () => ShowBiasDetails(BiasDescriptionTexts.RISK_AVERSION) },
                // { bestaetigungsVerzerrungButton, () => ShowBiasDetails(BiasDescriptionTexts.BESTAETIGUNGS_VERZERRUNG) },
                //
                // // Gesellschaftliche Erwartungen & soziale Normen
                // { tokenismButton, OnTokenism },
                // { biasInDerWahrnehmungVonFuehrungsFaehigkeitenButton, OnBiasInDerWahrnehmungVonFuehrungsFaehigkeiten },
                // { benevolenterSexismusButton, OnBenevolenterSexismus },
                // { altersUndGenerationenBiasesButton, OnAltersUndGenerationenBiases },
                // { stereotypeGegenueberFrauenInNichtTraditionellenBranchenButton, OnStereotypeGegenueberFrauen },
                //
                // // Wahrnehmung & Führungsrollen
                // { heteronormativitaetButton, OnHeteronormativitaet },
                // { maternalBiasButton, OnMaternalBias },
                // { erwartungshaltungBezueglichFamilienplanungButton, OnErwartungshaltungBezueglichFamilienplanung },
                // { workLifeBalanceErwartungenButton, OnWorkLifeBalanceErwartungen },
                // { geschlechtsspezifischeStereotypeButton, OnGeschlechtsspezifischeStereotype },
                //
                // // Psychologische Barrieren & kommunikative Hindernisse
                // { tightropeBiasButton, OnTightropeBias },
                // { mikroaggressionenButton, OnMikroaggressionen },
                // { leistungsattributionsBiasButton, OnLeistungsattributionsBias },
                // { unbewussteBiasInDerKommunikationButton, OnUnbewussteBiasInDerKommunikation }
            };
        }

        private void AddButtonListeners()
        {
            foreach (var buttonAction in _buttonActions)
            {
                buttonAction.Key.onClick.AddListener(() => buttonAction.Value.Invoke());
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
        }
        
        /// <summary>
        /// Zeigt detaillierte Informationen zu einem Bias-Thema.
        /// </summary>
        private void ShowBiasDetails(string biasHeadline, string biasDescriptionPreview, string biasDescription)
        {
            infoText.SetActive(false);
            biasInformation.SetActive(false);
            barrierenInfoGroup.SetActive(false);

            biasDetailsObject.SetActive(true);
            biasDetailsObject.GetComponentInChildren<TextMeshProUGUI>().text = "<size=40><align=center><b>" + biasHeadline + "</b></align></size>\n\n" +
                                                                     "<size=30><b>" + biasDescriptionPreview + "</b></size>" +
                                                                     "<size=30>" + biasDescription + "</size>";
        }

        private void OnBarrierenButton()
        {
            infoText.SetActive(true);
            biasGroups.SetActive(false);
            biasInformation.SetActive(true);

            barrierenInfoGroup.SetActive(true);
            erwartungenNormenInfoGroup.SetActive(false);
            wahrnehmungFuehrungsrollenInfoGroup.SetActive(false);
            barrierenHindernisseInfoGroup.SetActive(false);
        }

        private void OnErwartungenNormen()
        {
            infoText.SetActive(true);
            biasGroups.SetActive(false);
            biasInformation.SetActive(true);

            barrierenInfoGroup.SetActive(false);
            erwartungenNormenInfoGroup.SetActive(true);
            wahrnehmungFuehrungsrollenInfoGroup.SetActive(false);
            barrierenHindernisseInfoGroup.SetActive(false);
        }

        private void OnWahrnehmungFuehrungsrollenButton()
        {
            infoText.SetActive(true);
            biasGroups.SetActive(false);
            biasInformation.SetActive(true);

            barrierenInfoGroup.SetActive(false);
            erwartungenNormenInfoGroup.SetActive(false);
            wahrnehmungFuehrungsrollenInfoGroup.SetActive(true);
            barrierenHindernisseInfoGroup.SetActive(false);
        }

        private void OnBarrierenHindernisseButton()
        {
            infoText.SetActive(true);
            biasGroups.SetActive(false);
            biasInformation.SetActive(true);

            barrierenInfoGroup.SetActive(false);
            erwartungenNormenInfoGroup.SetActive(false);
            wahrnehmungFuehrungsrollenInfoGroup.SetActive(false);
            barrierenHindernisseInfoGroup.SetActive(true);
        }

        private void OnBarrierenInfoButton()
        {
            infoText.SetActive(true);
            biasGroups.SetActive(true);
            biasInformation.SetActive(false);

            barrierenInfoGroup.SetActive(false);
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
            erwartungenNormenInfoGroup.SetActive(false);
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
            wahrnehmungFuehrungsrollenInfoGroup.SetActive(false);
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
            barrierenHindernisseInfoGroup.SetActive(false);
        }

        // private void OnFinanzierungszugang()
        // {
        //     infoText.SetActive(false);
        //     biasInformation.SetActive(false);
        //     barrierenInfoGroup.SetActive(false);
        //     
        //     biasDetailsObject.SetActive(true);
        //     biasDetailsObject.GetComponent<TextMeshProUGUI>().text = BiasDescriptionTexts.FINANZIERUNGSZUGANG;
        // }
        //
        // private void OnGenderPayGap()
        // {
        //     infoText.SetActive(false);
        //     biasInformation.SetActive(false);
        //     barrierenInfoGroup.SetActive(false);
        //     
        //     biasDetailsObject.SetActive(true);
        //     biasDetailsObject.GetComponent<TextMeshProUGUI>().text = BiasDescriptionTexts.GENDER_PAY_GAP;
        // }
    }
}