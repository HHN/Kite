using System.Collections.Generic;

namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    public abstract class NovelKeyWordValue
    {
        public const string SZENE_BUERO = ">>Szene:Buero<<";

        public const string EINTRITT_CHARAKTER_01 = ">>Eintritt:Charakter01<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_ERSCHROCKEN =
            ">>Eintritt:Charakter01,Gesichtsausdruck:erschrocken<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_GENERVT =
            ">>Eintritt:Charakter01,Gesichtsausdruck:genervt<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_UNZUFRIEDEN =
            ">>Eintritt:Charakter01,Gesichtsausdruck:unzufrieden<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_ABLEHNEND =
            ">>Eintritt:Charakter01,Gesichtsausdruck:ablehnend<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_ERSTAUNT =
            ">>Eintritt:Charakter01,Gesichtsausdruck:erstaunt<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_FRAGEND =
            ">>Eintritt:Charakter01,Gesichtsausdruck:fragend<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_KRITISCH =
            ">>Eintritt:Charakter01,Gesichtsausdruck:kritisch<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_LAECHELN_GROSS =
            ">>Eintritt:Charakter01,Gesichtsausdruck:laecheln_gross<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_LACHEND =
            ">>Eintritt:Charakter01,Gesichtsausdruck:lachend<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_LAECHELN =
            ">>Eintritt:Charakter01,Gesichtsausdruck:laecheln<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT =
            ">>Eintritt:Charakter01,Gesichtsausdruck:netral_entspannt<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_NEUTRAL =
            ">>Eintritt:Charakter01,Gesichtsausdruck:neutral<<";

        public const string EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_STOLZ =
            ">>Eintritt:Charakter01,Gesichtsausdruck:stolz<<";
        
        public const string CHARAKTER_01_SCHAUT = ">>Charakter01Schaut:<<";
        
        public const string CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_ERSCHROCKEN = 
            ">>Charakter01SchautErschrocken:<<";        
        
        public const string CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_GENERVT = 
            ">>Charakter01SchautGenervt:<<";
        
        public const string CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_UNZUFRIEDEN = 
            ">>Charakter01SchautUnzufrieden:<<";
        
        public const string CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_ABLEHNEND = 
            ">>Charakter01SchautAblehnend:<<";
        
        public const string CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_ERSTAUNT = 
            ">>Charakter01SchautErstaunt:<<";
        
        public const string CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_FRAGEND = 
            ">>Charakter01SchautFragend:<<";
        
        public const string CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_KRITISCH = 
            ">>Charakter01SchautKritisch:<<";
        
        public const string CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_LAECHELN_GROSS = 
            ">>Charakter01SchautLaechelnGross:<<";
        
        public const string CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_LACHEND = 
            ">>Charakter01SchautLachend:<<";
        
        public const string CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_LAECHELN = 
            ">>Charakter01SchautLaecheln:<<";
        
        public const string CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT = 
            ">>Charakter01SchautNeutralEntspannt:<<";
        
        public const string CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL = 
            ">>Charakter01SchautNeutral:<<";
        
        public const string CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_STOLZ = 
            ">>Charakter01SchautStolz:<<";
        
        // ------------------------------------------------------------------------------------//

        public const string CHARAKTER_01_SPRICHT = ">>Charakter01Spricht:<<";
        
        public const string CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_ERSCHROCKEN = 
            ">>Charakter01SprichtErschrocken:<<";        
        
        public const string CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_GENERVT = 
            ">>Charakter01SprichtGenervt:<<";
        
        public const string CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_UNZUFRIEDEN = 
            ">>Charakter01SprichtUnzufrieden:<<";
        
        public const string CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_ABLEHNEND = 
            ">>Charakter01SprichtAblehnend:<<";
        
        public const string CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_ERSTAUNT = 
            ">>Charakter01SprichtErstaunt:<<";
        
        public const string CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_FRAGEND = 
            ">>Charakter01SprichtFragend:<<";
        
        public const string CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_KRITISCH = 
            ">>Charakter01SprichtKritisch:<<";
        
        public const string CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_LAECHELN_GROSS = 
            ">>Charakter01SprichtLaechelnGross:<<";
        
        public const string CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_LACHEND = 
            ">>Charakter01SprichtLachend:<<";
        
        public const string CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_LAECHELN = 
            ">>Charakter01SprichtLaecheln:<<";
        
        public const string CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT = 
            ">>Charakter01SprichtNeutralEntspannt:<<";
        
        public const string CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL = 
            ">>Charakter01SprichtNeutral:<<";
        
        public const string CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_STOLZ = 
            ">>Charakter01SprichtStolz:<<";

        public const string EINTRITT_CHARAKTER_02 = ">>Eintritt:Charakter02<<";
        
        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_ERSCHROCKEN =
            ">>Eintritt:Charakter02,Gesichtsausdruck:erschrocken<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_GENERVT =
            ">>Eintritt:Charakter02,Gesichtsausdruck:genervt<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_UNZUFRIEDEN =
            ">>Eintritt:Charakter02,Gesichtsausdruck:unzufrieden<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_ABLEHNEND =
            ">>Eintritt:Charakter02,Gesichtsausdruck:ablehnend<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_ERSTAUNT =
            ">>Eintritt:Charakter02,Gesichtsausdruck:erstaunt<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_FRAGEND =
            ">>Eintritt:Charakter02,Gesichtsausdruck:fragend<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_KRITISCH =
            ">>Eintritt:Charakter02,Gesichtsausdruck:kritisch<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_LAECHELN_GROSS =
            ">>Eintritt:Charakter02,Gesichtsausdruck:laecheln_gross<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_LACHEND =
            ">>Eintritt:Charakter02,Gesichtsausdruck:lachend<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_LAECHELN =
            ">>Eintritt:Charakter02,Gesichtsausdruck:laecheln<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT =
            ">>Eintritt:Charakter02,Gesichtsausdruck:netral_entspannt<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_NEUTRAL =
            ">>Eintritt:Charakter02,Gesichtsausdruck:neutral<<";

        public const string EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_STOLZ =
            ">>Eintritt:Charakter02,Gesichtsausdruck:stolz<<";
        
        public const string CHARAKTER_02_SCHAUT = ">>Charakter02Schaut:<<";
        
        public const string CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_ERSCHROCKEN = 
            ">>Charakter02SchautErschrocken:<<";        
        
        public const string CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_GENERVT = 
            ">>Charakter02SchautGenervt:<<";
        
        public const string CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_UNZUFRIEDEN = 
            ">>Charakter02SchautUnzufrieden:<<";
        
        public const string CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_ABLEHNEND = 
            ">>Charakter02SchautAblehnend:<<";
        
        public const string CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_ERSTAUNT = 
            ">>Charakter02SchautErstaunt:<<";
        
        public const string CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_FRAGEND = 
            ">>Charakter02SchautFragend:<<";
        
        public const string CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_KRITISCH = 
            ">>Charakter02SchautKritisch:<<";
        
        public const string CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_LAECHELN_GROSS = 
            ">>Charakter02SchautLaechelnGross:<<";
        
        public const string CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_LACHEND = 
            ">>Charakter02SchautLachend:<<";
        
        public const string CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_LAECHELN = 
            ">>Charakter02SchautLaecheln:<<";
        
        public const string CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT = 
            ">>Charakter02SchautNeutralEntspannt:<<";
        
        public const string CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL = 
            ">>Charakter02SchautNeutral:<<";
        
        public const string CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_STOLZ = 
            ">>Charakter02SchautStolz:<<";
        
        // ------------------------------------------------------------------------------------//

        public const string CHARAKTER_02_SPRICHT = ">>Charakter02Spricht:<<";
        
        public const string CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_ERSCHROCKEN = 
            ">>Charakter02SprichtErschrocken:<<";        
        
        public const string CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_GENERVT = 
            ">>Charakter02SprichtGenervt:<<";
        
        public const string CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_UNZUFRIEDEN = 
            ">>Charakter02SprichtUnzufrieden:<<";
        
        public const string CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_ABLEHNEND = 
            ">>Charakter02SprichtAblehnend:<<";
        
        public const string CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_ERSTAUNT = 
            ">>Charakter02SprichtErstaunt:<<";
        
        public const string CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_FRAGEND = 
            ">>Charakter02SprichtFragend:<<";
        
        public const string CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_KRITISCH = 
            ">>Charakter02SprichtKritisch:<<";
        
        public const string CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_LAECHELN_GROSS = 
            ">>Charakter02SprichtLaechelnGross:<<";
        
        public const string CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_LACHEND = 
            ">>Charakter02SprichtLachend:<<";
        
        public const string CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_LAECHELN = 
            ">>Charakter02SprichtLaecheln:<<";
        
        public const string CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT = 
            ">>Charakter02SprichtNeutralEntspannt:<<";
        
        public const string CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL = 
            ">>Charakter02SprichtNeutral:<<";
        
        public const string CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_STOLZ = 
            ">>Charakter02SprichtStolz:<<";

        public const string EINTRITT_CHARAKTER_03 = ">>Eintritt:Charakter03<<";
        
        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_ERSCHROCKEN =
            ">>Eintritt:Charakter03,Gesichtsausdruck:erschrocken<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_GENERVT =
            ">>Eintritt:Charakter03,Gesichtsausdruck:genervt<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_UNZUFRIEDEN =
            ">>Eintritt:Charakter03,Gesichtsausdruck:unzufrieden<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_ABLEHNEND =
            ">>Eintritt:Charakter03,Gesichtsausdruck:ablehnend<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_ERSTAUNT =
            ">>Eintritt:Charakter03,Gesichtsausdruck:erstaunt<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_FRAGEND =
            ">>Eintritt:Charakter03,Gesichtsausdruck:fragend<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_KRITISCH =
            ">>Eintritt:Charakter03,Gesichtsausdruck:kritisch<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_LAECHELN_GROSS =
            ">>Eintritt:Charakter03,Gesichtsausdruck:laecheln_gross<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_LACHEND =
            ">>Eintritt:Charakter03,Gesichtsausdruck:lachend<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_LAECHELN =
            ">>Eintritt:Charakter03,Gesichtsausdruck:laecheln<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT =
            ">>Eintritt:Charakter03,Gesichtsausdruck:netral_entspannt<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_NEUTRAL =
            ">>Eintritt:Charakter03,Gesichtsausdruck:neutral<<";

        public const string EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_STOLZ =
            ">>Eintritt:Charakter03,Gesichtsausdruck:stolz<<";
        
        public const string CHARAKTER_03_SCHAUT = ">>Charakter03Schaut:<<";
        
        public const string CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_ERSCHROCKEN = 
            ">>Charakter03SchautErschrocken:<<";        
        
        public const string CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_GENERVT = 
            ">>Charakter03SchautGenervt:<<";
        
        public const string CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_UNZUFRIEDEN = 
            ">>Charakter03SchautUnzufrieden:<<";
        
        public const string CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_ABLEHNEND = 
            ">>Charakter03SchautAblehnend:<<";
        
        public const string CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_ERSTAUNT = 
            ">>Charakter03SchautErstaunt:<<";
        
        public const string CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_FRAGEND = 
            ">>Charakter03SchautFragend:<<";
        
        public const string CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_KRITISCH = 
            ">>Charakter03SchautKritisch:<<";
        
        public const string CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_LAECHELN_GROSS = 
            ">>Charakter03SchautLaechelnGross:<<";
        
        public const string CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_LACHEND = 
            ">>Charakter03SchautLachend:<<";
        
        public const string CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_LAECHELN = 
            ">>Charakter03SchautLaecheln:<<";
        
        public const string CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT = 
            ">>Charakter03SchautNeutralEntspannt:<<";
        
        public const string CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL = 
            ">>Charakter03SchautNeutral:<<";
        
        public const string CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_STOLZ = 
            ">>Charakter03SchautStolz:<<";
        
        // ------------------------------------------------------------------------------------//

        public const string CHARAKTER_03_SPRICHT = ">>Charakter03Spricht:<<";
        
        public const string CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_ERSCHROCKEN = 
            ">>Charakter03SprichtErschrocken:<<";        
        
        public const string CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_GENERVT = 
            ">>Charakter03SprichtGenervt:<<";
        
        public const string CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_UNZUFRIEDEN = 
            ">>Charakter03SprichtUnzufrieden:<<";
        
        public const string CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_ABLEHNEND = 
            ">>Charakter03SprichtAblehnend:<<";
        
        public const string CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_ERSTAUNT = 
            ">>Charakter03SprichtErstaunt:<<";
        
        public const string CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_FRAGEND = 
            ">>Charakter03SprichtFragend:<<";
        
        public const string CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_KRITISCH = 
            ">>Charakter03SprichtKritisch:<<";
        
        public const string CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_LAECHELN_GROSS = 
            ">>Charakter03SprichtLaechelnGross:<<";
        
        public const string CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_LACHEND = 
            ">>Charakter03SprichtLachend:<<";
        
        public const string CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_LAECHELN = 
            ">>Charakter03SprichtLaecheln:<<";
        
        public const string CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT = 
            ">>Charakter03SprichtNeutralEntspannt:<<";
        
        public const string CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL = 
            ">>Charakter03SprichtNeutral:<<";
        
        public const string CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_STOLZ = 
            ">>Charakter03SprichtStolz:<<";

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
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_ERSCHROCKEN,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_GENERVT,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_UNZUFRIEDEN,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_ABLEHNEND,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_ERSTAUNT,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_FRAGEND,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_KRITISCH,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_LAECHELN_GROSS,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_LACHEND,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_LAECHELN,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_NEUTRAL,
            EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_STOLZ,
            
            CHARAKTER_01_SCHAUT,
            CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_ERSCHROCKEN,
            CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_GENERVT,
            CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_UNZUFRIEDEN,
            CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_ABLEHNEND,
            CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_ERSTAUNT,
            CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_FRAGEND,
            CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_KRITISCH,
            CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_LAECHELN_GROSS,
            CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_LACHEND,
            CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_LAECHELN,
            CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT,
            CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL,
            CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_STOLZ,
            
            CHARAKTER_01_SPRICHT,
            CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_ERSCHROCKEN,
            CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_GENERVT,
            CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_UNZUFRIEDEN,
            CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_ABLEHNEND,
            CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_ERSTAUNT,
            CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_FRAGEND,
            CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_KRITISCH,
            CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_LAECHELN_GROSS,
            CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_LACHEND,
            CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_LAECHELN,
            CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT,
            CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL,
            CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_STOLZ,
            
            EINTRITT_CHARAKTER_02,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_ERSCHROCKEN,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_GENERVT,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_UNZUFRIEDEN,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_ABLEHNEND,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_ERSTAUNT,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_FRAGEND,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_KRITISCH,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_LAECHELN_GROSS,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_LACHEND,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_LAECHELN,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_NEUTRAL,
            EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_STOLZ,
            
            CHARAKTER_02_SCHAUT,
            CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_ERSCHROCKEN,
            CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_GENERVT,
            CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_UNZUFRIEDEN,
            CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_ABLEHNEND,
            CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_ERSTAUNT,
            CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_FRAGEND,
            CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_KRITISCH,
            CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_LAECHELN_GROSS,
            CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_LACHEND,
            CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_LAECHELN,
            CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT,
            CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL,
            CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_STOLZ,
            
            CHARAKTER_02_SPRICHT,
            CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_ERSCHROCKEN,
            CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_GENERVT,
            CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_UNZUFRIEDEN,
            CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_ABLEHNEND,
            CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_ERSTAUNT,
            CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_FRAGEND,
            CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_KRITISCH,
            CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_LAECHELN_GROSS,
            CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_LACHEND,
            CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_LAECHELN,
            CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT,
            CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL,
            CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_STOLZ,
            
            EINTRITT_CHARAKTER_03,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_ERSCHROCKEN,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_GENERVT,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_UNZUFRIEDEN,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_ABLEHNEND,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_ERSTAUNT,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_FRAGEND,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_KRITISCH,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_LAECHELN_GROSS,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_LACHEND,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_LAECHELN,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_NEUTRAL,
            EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_STOLZ,
            
            CHARAKTER_03_SCHAUT,
            CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_ERSCHROCKEN,
            CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_GENERVT,
            CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_UNZUFRIEDEN,
            CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_ABLEHNEND,
            CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_ERSTAUNT,
            CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_FRAGEND,
            CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_KRITISCH,
            CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_LAECHELN_GROSS,
            CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_LACHEND,
            CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_LAECHELN,
            CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT,
            CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL,
            CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_STOLZ,
            
            CHARAKTER_03_SPRICHT,
            CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_ERSCHROCKEN,
            CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_GENERVT,
            CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_UNZUFRIEDEN,
            CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_ABLEHNEND,
            CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_ERSTAUNT,
            CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_FRAGEND,
            CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_KRITISCH,
            CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_LAECHELN_GROSS,
            CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_LACHEND,
            CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_LAECHELN,
            CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT,
            CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL,
            CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_STOLZ,
            
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