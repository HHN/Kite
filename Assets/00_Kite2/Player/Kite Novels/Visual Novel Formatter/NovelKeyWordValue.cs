using System.Collections.Generic;

namespace _00_Kite2.Player.Kite_Novels.Visual_Novel_Formatter
{
    public abstract class NovelKeyWordValue
    {
        public const string SZENE_BUERO = ">>Szene:Buero<<";

        public const string EINTRITT_CHARAKTER_01 = ">>Eintritt:Charakter01<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_RELAXED =
            ">>Eintritt:Charakter01,Gesichtsausdruck:relaxed<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_ASTONISHED =
            ">>Eintritt:Charakter01,Gesichtsausdruck:astonished<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_REFUSING =
            ">>Eintritt:Charakter01,Gesichtsausdruck:refusing<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_SMILING =
            ">>Eintritt:Charakter01,Gesichtsausdruck:smiling<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_FRIENDLY =
            ">>Eintritt:Charakter01,Gesichtsausdruck:friendly<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_LAUGHING =
            ">>Eintritt:Charakter01,Gesichtsausdruck:laughing<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_CRITICAL =
            ">>Eintritt:Charakter01,Gesichtsausdruck:critical<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_DECISION_NO =
            ">>Eintritt:Charakter01,Gesichtsausdruck:decisionNo<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_HAPPY =
            ">>Eintritt:Charakter01,Gesichtsausdruck:happy<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_PROUD =
            ">>Eintritt:Charakter01,Gesichtsausdruck:proud<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_SCARED =
            ">>Eintritt:Charakter01,Gesichtsausdruck:scared<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_QUESTIONING =
            ">>Eintritt:Charakter01,Gesichtsausdruck:questioning<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_DEFEATED =
            ">>Eintritt:Charakter01,Gesichtsausdruck:defeated<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_01 = ">>Charakter01Spricht:<<";
        public const string CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_RELAXED = ">>Charakter01SprichtRelaxed:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_ASTONISHED =
            ">>Charakter01SprichtAstonished:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_REFUSING =
            ">>Charakter01SprichtRefusing:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_SMILING = ">>Charakter01SprichtSmiling:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_FRIENDLY =
            ">>Charakter01SprichtFriendly:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_LAUGHING =
            ">>Charakter01SprichtLaughing:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_CRITICAL =
            ">>Charakter01SprichtCritical:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_DECISION_NO =
            ">>Charakter01SprichtDecisionNo:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_HAPPY = ">>Charakter01SprichtHappy:<<";
        public const string CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_PROUD = ">>Charakter01SprichtProud:<<";
        public const string CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_SCARED = ">>Charakter01SprichtScared:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_QUESTIONING =
            ">>Charakter01SprichtQuestioning:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_DEFEATED =
            ">>Charakter01SprichtDefeated:<<";

        public const string EINTRITT_CHARAKTER_02 = ">>Eintritt:Charakter02<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_RELAXED =
            ">>Eintritt:Charakter02,Gesichtsausdruck:relaxed<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_ASTONISHED =
            ">>Eintritt:Charakter02,Gesichtsausdruck:astonished<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_REFUSING =
            ">>Eintritt:Charakter02,Gesichtsausdruck:refusing<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_SMILING =
            ">>Eintritt:Charakter02,Gesichtsausdruck:smiling<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_FRIENDLY =
            ">>Eintritt:Charakter02,Gesichtsausdruck:friendly<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_LAUGHING =
            ">>Eintritt:Charakter02,Gesichtsausdruck:laughing<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_CRITICAL =
            ">>Eintritt:Charakter02,Gesichtsausdruck:critical<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_DECISION_NO =
            ">>Eintritt:Charakter02,Gesichtsausdruck:decisionNo<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_HAPPY =
            ">>Eintritt:Charakter02,Gesichtsausdruck:happy<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_PROUD =
            ">>Eintritt:Charakter02,Gesichtsausdruck:proud<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_SCARED =
            ">>Eintritt:Charakter02,Gesichtsausdruck:scared<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_QUESTIONING =
            ">>Eintritt:Charakter02,Gesichtsausdruck:questioning<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_DEFEATED =
            ">>Eintritt:Charakter02,Gesichtsausdruck:defeated<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_02 = ">>Charakter02Spricht:<<";
        public const string CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_RELAXED = ">>Charakter02SprichtRelaxed:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_ASTONISHED =
            ">>Charakter02SprichtAstonished:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_REFUSING =
            ">>Charakter02SprichtRefusing:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_SMILING = ">>Charakter02SprichtSmiling:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_FRIENDLY =
            ">>Charakter02SprichtFriendly:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_LAUGHING =
            ">>Charakter02SprichtLaughing:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_CRITICAL =
            ">>Charakter02SprichtCritical:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_DECISION_NO =
            ">>Charakter02SprichtDecisionNo:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_HAPPY = ">>Charakter02SprichtHappy:<<";
        public const string CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_PROUD = ">>Charakter02SprichtProud:<<";
        public const string CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_SCARED = ">>Charakter02SprichtScared:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_QUESTIONING =
            ">>Charakter02SprichtQuestioning:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_DEFEATED =
            ">>Charakter02SprichtDefeated:<<";

        public const string EINTRITT_CHARAKTER_03 = ">>Eintritt:Charakter03<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_RELAXED =
            ">>Eintritt:Charakter03,Gesichtsausdruck:relaxed<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_ASTONISHED =
            ">>Eintritt:Charakter03,Gesichtsausdruck:astonished<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_REFUSING =
            ">>Eintritt:Charakter03,Gesichtsausdruck:refusing<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_SMILING =
            ">>Eintritt:Charakter03,Gesichtsausdruck:smiling<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_FRIENDLY =
            ">>Eintritt:Charakter03,Gesichtsausdruck:friendly<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_LAUGHING =
            ">>Eintritt:Charakter03,Gesichtsausdruck:laughing<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_CRITICAL =
            ">>Eintritt:Charakter03,Gesichtsausdruck:critical<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_DECISION_NO =
            ">>Eintritt:Charakter03,Gesichtsausdruck:decisionNo<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_HAPPY =
            ">>Eintritt:Charakter03,Gesichtsausdruck:happy<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_PROUD =
            ">>Eintritt:Charakter03,Gesichtsausdruck:proud<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_SCARED =
            ">>Eintritt:Charakter03,Gesichtsausdruck:scared<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_QUESTIONING =
            ">>Eintritt:Charakter03,Gesichtsausdruck:questioning<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_DEFEATED =
            ">>Eintritt:Charakter03,Gesichtsausdruck:defeated<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_03 = ">>Charakter03Spricht:<<";
        public const string CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_RELAXED = ">>Charakter03SprichtRelaxed:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_ASTONISHED =
            ">>Charakter03SprichtAstonished:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_REFUSING =
            ">>Charakter03SprichtRefusing:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_SMILING = ">>Charakter03SprichtSmiling:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_FRIENDLY =
            ">>Charakter03SprichtFriendly:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_LAUGHING =
            ">>Charakter03SprichtLaughing:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_CRITICAL =
            ">>Charakter03SprichtCritical:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_DECISION_NO =
            ">>Charakter03SprichtDecisionNo:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_HAPPY = ">>Charakter03SprichtHappy:<<";
        public const string CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_PROUD = ">>Charakter03SprichtProud:<<";
        public const string CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_SCARED = ">>Charakter03SprichtScared:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_QUESTIONING =
            ">>Charakter03SprichtQuestioning:<<";

        public const string CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_DEFEATED =
            ">>Charakter03SprichtDefeated:<<";

        public const string SPIELER_CHARAKTER_SPRICHT = ">>SpielerCharakterSpricht:<<";

        public const string INFO_NACHRICHT_WIRD_ANGEZEIGT = ">>InfoNachrichtWirdAngezeigt:<<";

        public const string SOUND_ABSPIELEN_WATER_POURING = ">>SoundAbspielen:waterPouring<<";
        public const string SOUND_ABSPIELEN_LEAVE_SCENE = ">>SoundAbspielen:leaveScene<<";
        public const string SOUND_ABSPIELEN_TELEPHONE_CALL = ">>SoundAbspielen:telephoneCall<<";
        public const string SOUND_ABSPIELEN_PAPER_SOUND = ">>SoundAbspielen:paperSound<<";
        public const string SOUND_ABSPIELEN_MAN_LAUGHING = ">>SoundAbspielen:manLaughing<<";

        public const string ANIMATION_ABSPIELEN_WATER_POURING = ">>AnimationAbspielen:waterPouring<<";

        public const string DIALOG_OPTIONEN = ">>DialogOptionen<<";

        public const string ENDE = ">>Ende<<";

        public const string FREITEXT_EINGABE = ">>FreitextEingabe<<";

        public const string GPT_PROMPT_MIT_DEFAULT_COMPLETION_HANDLER = ">>GPTPromptMitDefaultCompletionHandler<<";

        public const string PERSISTENTES_SPEICHERN = ">>PersistentesSpeichern<<";

        public const string VARIABLE_SETZEN = ">>VariableSetzen<<";

        public const string VARIABLE_AUS_BOOLSCHEM_AUSDRUCK_BESTIMMEN = ">>VariableAusBoolschemAusdruckBestimmen<<";

        public const string FEEDBACK_HINZUFUEGEN = ">>FeedbackHinzufuegen<<";

        public const string FEEDBACK_UNTER_BEDINGUNG_HINZUFUEGEN = ">>FeedbackUnterBedingungHinzufuegen<<";

        public const string RELEVANTER_BIAS_FINANZIERUNGSZUGANG = ">>RelevanterBias:Finanzierungszugang<<";
        public const string RELEVANTER_BIAS_GENDER_PAY_GAP = ">>RelevanterBias:GenderPayGap<<";

        public const string RELEVANTER_BIAS_UNTERBEWERTUNG_WEIBLICH_GEFUEHRTER_UNTERNEHMEN =
            ">>RelevanterBias:UnterbewertungWeiblichGefuehrterUnternehmen<<";

        public const string RELEVANTER_BIAS_RISK_AVERSION_BIAS = ">>RelevanterBias:RiskAversionBias<<";
        public const string RELEVANTER_BIAS_BESTAETIGUNGSVERZERRUNG = ">>RelevanterBias:Bestaetigungsverzerrung<<";
        public const string RELEVANTER_BIAS_TOKENISM = ">>RelevanterBias:Tokenism<<";

        public const string RELEVANTER_BIAS_BIAS_IN_DER_WAHRNEHMUNG_VON_FUEHRUNGSFAEHIGKEITEN =
            ">>RelevanterBias:BiasInDerWahrnehmungVonFuehrungsfaehigkeiten<<";

        public const string RELEVANTER_BIAS_RASSISTISCHE_UND_ETHNISCHE_BIASES =
            ">>RelevanterBias:RassistischeUndEthnischeBiases<<";

        public const string RELEVANTER_BIAS_SOZIOOEKONOMISCHE_BIASES = ">>RelevanterBias:SoziooekonomischeBiases<<";

        public const string RELEVANTER_BIAS_ALTER_UND_GENERATIONEN_BIASES =
            ">>RelevanterBias:AlterUndGenerationenBiases<<";

        public const string RELEVANTER_BIAS_SEXUALITAETSBEZOGENE_BIASES =
            ">>RelevanterBias:SexualitaetsbezogeneBiases<<";

        public const string RELEVANTER_BIAS_BIASES_GEGENUEBER_FRAUEN_MIT_BEHINDERUNGEN =
            ">>RelevanterBias:BiasesGegenueberFrauenMitBehinderungen<<";

        public const string RELEVANTER_BIAS_STEREOTYPE_GEGENUEBER_FRAUEN_IN_NICHT_TRADITIONELLEN_BRANCHEN =
            ">>RelevanterBias:StereotypeGegenueberFrauenInNichtTraditionellenBranchen<<";

        public const string RELEVANTER_BIAS_KULTURELLE_UND_RELIGIOESE_BIASES =
            ">>RelevanterBias:KulturelleUndReligioeseBiases<<";

        public const string RELEVANTER_BIAS_MATERNAL_BIAS = ">>RelevanterBias:MaternalBias<<";

        public const string RELEVANTER_BIAS_BIASES_GEGENUEBER_FRAUEN_MIT_KINDERN =
            ">>RelevanterBias:BiasesGegenueberFrauenMitKindern<<";

        public const string RELEVANTER_BIAS_ERWARTUNGSHALTUNG_BEZUEGLICH_FAMILIENPLANUNG =
            ">>RelevanterBias:ErwartungshaltungBezueglichFamilienplanung<<";

        public const string RELEVANTER_BIAS_WORK_LIFE_BALANCE_ERWARTUNGEN =
            ">>RelevanterBias:WorkLifeBalanceErwartungen<<";

        public const string RELEVANTER_BIAS_GESCHLECHTSSPEZIFISCHE_STEREOTYPEN =
            ">>RelevanterBias:GeschlechtsspezifischeStereotypen<<";

        public const string RELEVANTER_BIAS_TIGHTROPE_BIAS = ">>RelevanterBias:TightropeBias<<";
        public const string RELEVANTER_BIAS_MIKROAGGRESSIONEN = ">>RelevanterBias:Mikroaggressionen<<";
        public const string RELEVANTER_BIAS_LEISTUNGSATTRIBUTIONS_BIAS = ">>RelevanterBias:LeistungsattributionsBias<<";
        public const string RELEVANTER_BIAS_BIAS_IN_MEDIEN_UND_WERBUNG = ">>RelevanterBias:BiasInMedienUndWerbung<<";

        public const string RELEVANTER_BIAS_UNBEWUSSTE_BIAS_IN_DER_KOMMUNIKATION =
            ">>RelevanterBias:UnbewussteBiasInDerKommunikation<<";

        public const string RELEVANTER_BIAS_PROVE_IT_AGAIN_BIAS = ">>RelevanterBias:ProveItAgainBias<<";
        public const string RELEVANTER_BIAS_HETERONORMATIVITAET_BIAS = ">>RelevanterBias:Heteronormativitaet<<";
        public const string RELEVANTER_BIAS_BENEVOLENTER_SEXISMUS_BIAS = ">>RelevanterBias:BenevolenterSexismus<<";

        public static List<string> ALL_KEY_WORDS = new List<string>
        {
            SZENE_BUERO,
            EINTRITT_CHARAKTER_01,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_RELAXED,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_ASTONISHED,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_REFUSING,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_SMILING,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_FRIENDLY,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_LAUGHING,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_CRITICAL,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_DECISION_NO,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_HAPPY,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_PROUD,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_SCARED,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_QUESTIONING,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_DEFEATED,
            CHARAKTER_SPRICHT_CHARAKTER_01,
            CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_RELAXED,
            CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_ASTONISHED,
            CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_REFUSING,
            CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_SMILING,
            CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_FRIENDLY,
            CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_LAUGHING,
            CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_CRITICAL,
            CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_DECISION_NO,
            CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_HAPPY,
            CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_PROUD,
            CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_SCARED,
            CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_QUESTIONING,
            CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_DEFEATED,
            EINTRITT_CHARAKTER_02,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_RELAXED,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_ASTONISHED,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_REFUSING,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_SMILING,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_FRIENDLY,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_LAUGHING,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_CRITICAL,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_DECISION_NO,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_HAPPY,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_PROUD,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_SCARED,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_QUESTIONING,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_DEFEATED,
            CHARAKTER_SPRICHT_CHARAKTER_02,
            CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_RELAXED,
            CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_ASTONISHED,
            CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_REFUSING,
            CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_SMILING,
            CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_FRIENDLY,
            CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_LAUGHING,
            CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_CRITICAL,
            CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_DECISION_NO,
            CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_HAPPY,
            CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_PROUD,
            CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_SCARED,
            CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_QUESTIONING,
            CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_DEFEATED,
            EINTRITT_CHARAKTER_03,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_RELAXED,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_ASTONISHED,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_REFUSING,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_SMILING,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_FRIENDLY,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_LAUGHING,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_CRITICAL,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_DECISION_NO,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_HAPPY,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_PROUD,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_SCARED,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_QUESTIONING,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_DEFEATED,
            CHARAKTER_SPRICHT_CHARAKTER_03,
            CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_RELAXED,
            CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_ASTONISHED,
            CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_REFUSING,
            CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_SMILING,
            CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_FRIENDLY,
            CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_LAUGHING,
            CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_CRITICAL,
            CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_DECISION_NO,
            CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_HAPPY,
            CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_PROUD,
            CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_SCARED,
            CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_QUESTIONING,
            CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_DEFEATED,
            SPIELER_CHARAKTER_SPRICHT,
            INFO_NACHRICHT_WIRD_ANGEZEIGT,
            SOUND_ABSPIELEN_WATER_POURING,
            SOUND_ABSPIELEN_LEAVE_SCENE,
            SOUND_ABSPIELEN_TELEPHONE_CALL,
            SOUND_ABSPIELEN_PAPER_SOUND,
            SOUND_ABSPIELEN_MAN_LAUGHING,
            ANIMATION_ABSPIELEN_WATER_POURING,
            DIALOG_OPTIONEN,
            ENDE,
            FREITEXT_EINGABE,
            GPT_PROMPT_MIT_DEFAULT_COMPLETION_HANDLER,
            PERSISTENTES_SPEICHERN,
            VARIABLE_SETZEN,
            VARIABLE_AUS_BOOLSCHEM_AUSDRUCK_BESTIMMEN,
            FEEDBACK_HINZUFUEGEN,
            FEEDBACK_UNTER_BEDINGUNG_HINZUFUEGEN,
            RELEVANTER_BIAS_FINANZIERUNGSZUGANG,
            RELEVANTER_BIAS_GENDER_PAY_GAP,
            RELEVANTER_BIAS_UNTERBEWERTUNG_WEIBLICH_GEFUEHRTER_UNTERNEHMEN,
            RELEVANTER_BIAS_RISK_AVERSION_BIAS,
            RELEVANTER_BIAS_BESTAETIGUNGSVERZERRUNG,
            RELEVANTER_BIAS_TOKENISM,
            RELEVANTER_BIAS_BIAS_IN_DER_WAHRNEHMUNG_VON_FUEHRUNGSFAEHIGKEITEN,
            RELEVANTER_BIAS_RASSISTISCHE_UND_ETHNISCHE_BIASES,
            RELEVANTER_BIAS_SOZIOOEKONOMISCHE_BIASES,
            RELEVANTER_BIAS_ALTER_UND_GENERATIONEN_BIASES,
            RELEVANTER_BIAS_SEXUALITAETSBEZOGENE_BIASES,
            RELEVANTER_BIAS_BIASES_GEGENUEBER_FRAUEN_MIT_BEHINDERUNGEN,
            RELEVANTER_BIAS_STEREOTYPE_GEGENUEBER_FRAUEN_IN_NICHT_TRADITIONELLEN_BRANCHEN,
            RELEVANTER_BIAS_KULTURELLE_UND_RELIGIOESE_BIASES,
            RELEVANTER_BIAS_MATERNAL_BIAS,
            RELEVANTER_BIAS_BIASES_GEGENUEBER_FRAUEN_MIT_KINDERN,
            RELEVANTER_BIAS_ERWARTUNGSHALTUNG_BEZUEGLICH_FAMILIENPLANUNG,
            RELEVANTER_BIAS_WORK_LIFE_BALANCE_ERWARTUNGEN,
            RELEVANTER_BIAS_GESCHLECHTSSPEZIFISCHE_STEREOTYPEN,
            RELEVANTER_BIAS_TIGHTROPE_BIAS,
            RELEVANTER_BIAS_MIKROAGGRESSIONEN,
            RELEVANTER_BIAS_LEISTUNGSATTRIBUTIONS_BIAS,
            RELEVANTER_BIAS_BIAS_IN_MEDIEN_UND_WERBUNG,
            RELEVANTER_BIAS_UNBEWUSSTE_BIAS_IN_DER_KOMMUNIKATION,
            RELEVANTER_BIAS_PROVE_IT_AGAIN_BIAS,
            RELEVANTER_BIAS_HETERONORMATIVITAET_BIAS,
            RELEVANTER_BIAS_BENEVOLENTER_SEXISMUS_BIAS
        };
    }
}