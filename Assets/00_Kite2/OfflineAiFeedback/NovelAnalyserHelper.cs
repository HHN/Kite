using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class NovelAnalyserHelper
{
    private Dictionary<string, VisualNovelEvent> novelEvents;
    private VisualNovelEvent nextEventToAnalyse;
    private VisualNovel objectUnderAnalyse;
    private VisualNovelNames visualNovelName;
    private HashSet<Character> currentCharacters;
    private List<VisualNovelEvent> choices;
    private HashSet<string> alreadyPlayedEvents;
    private bool isOriginalNode = true;
    private int children = 0;
    private NovelAnalyserHelper parent = null;
    private bool isAnalysationFinished = false;
    private bool isAnalysationSuccessfull = false;
    private static Dictionary<VisualNovelNames, int> numberOfPossiblePaths;
    private static Dictionary<VisualNovelNames, List<NovelAnalyserHelper>> allPossibleNovelAnalyserHelpers;
    private static Dictionary<VisualNovelNames, List<NovelAnalyserHelper>> allPossibleBiasCombinations;
    private string path;
    private StringBuilder prompt;
    private bool endetAnalysationByReachingEndNovelEvent;
    public bool loopDetected;

    public NovelAnalyserHelper(VisualNovel novelToAnalyse)
    {
        objectUnderAnalyse = novelToAnalyse;
        visualNovelName = VisualNovelNamesHelper.ValueOf((int) novelToAnalyse.id);
        InitializePrompt();
        path = "";
        currentCharacters = new HashSet<Character>();
        choices = new List<VisualNovelEvent>();
        alreadyPlayedEvents = new HashSet<string>();

        if (numberOfPossiblePaths == null)
        {
            numberOfPossiblePaths = new Dictionary<VisualNovelNames, int>();
            numberOfPossiblePaths[VisualNovelNames.BANK_KREDIT_NOVEL] = 0;
            numberOfPossiblePaths[VisualNovelNames.BEKANNTE_TREFFEN_NOVEL] = 0;
            numberOfPossiblePaths[VisualNovelNames.BANK_KONTO_NOVEL] = 0;
            numberOfPossiblePaths[VisualNovelNames.FOERDERANTRAG_NOVEL] = 0;
            numberOfPossiblePaths[VisualNovelNames.ELTERN_NOVEL] = 0;
            numberOfPossiblePaths[VisualNovelNames.NOTARIAT_NOVEL] = 0;
            numberOfPossiblePaths[VisualNovelNames.PRESSE_NOVEL] = 0;
            numberOfPossiblePaths[VisualNovelNames.BUERO_NOVEL] = 0;
            numberOfPossiblePaths[VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL] = 0;
            numberOfPossiblePaths[VisualNovelNames.HONORAR_NOVEL] = 0;
            numberOfPossiblePaths[VisualNovelNames.LEBENSPARTNER_NOVEL] = 0;
            numberOfPossiblePaths[VisualNovelNames.INTRO_NOVEL] = 0;
        }
        if (allPossibleNovelAnalyserHelpers == null) 
        { 
            allPossibleNovelAnalyserHelpers = new Dictionary<VisualNovelNames, List<NovelAnalyserHelper>>();
            allPossibleNovelAnalyserHelpers[VisualNovelNames.BANK_KREDIT_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleNovelAnalyserHelpers[VisualNovelNames.BEKANNTE_TREFFEN_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleNovelAnalyserHelpers[VisualNovelNames.BANK_KONTO_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleNovelAnalyserHelpers[VisualNovelNames.FOERDERANTRAG_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleNovelAnalyserHelpers[VisualNovelNames.ELTERN_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleNovelAnalyserHelpers[VisualNovelNames.NOTARIAT_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleNovelAnalyserHelpers[VisualNovelNames.PRESSE_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleNovelAnalyserHelpers[VisualNovelNames.BUERO_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleNovelAnalyserHelpers[VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleNovelAnalyserHelpers[VisualNovelNames.HONORAR_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleNovelAnalyserHelpers[VisualNovelNames.LEBENSPARTNER_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleNovelAnalyserHelpers[VisualNovelNames.INTRO_NOVEL] = new List<NovelAnalyserHelper>();
        }
        if (allPossibleBiasCombinations == null) 
        { 
            allPossibleBiasCombinations = new Dictionary<VisualNovelNames, List<NovelAnalyserHelper>>();
            allPossibleBiasCombinations[VisualNovelNames.BANK_KREDIT_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleBiasCombinations[VisualNovelNames.BEKANNTE_TREFFEN_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleBiasCombinations[VisualNovelNames.BANK_KONTO_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleBiasCombinations[VisualNovelNames.FOERDERANTRAG_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleBiasCombinations[VisualNovelNames.ELTERN_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleBiasCombinations[VisualNovelNames.NOTARIAT_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleBiasCombinations[VisualNovelNames.PRESSE_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleBiasCombinations[VisualNovelNames.BUERO_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleBiasCombinations[VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleBiasCombinations[VisualNovelNames.HONORAR_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleBiasCombinations[VisualNovelNames.LEBENSPARTNER_NOVEL] = new List<NovelAnalyserHelper>();
            allPossibleBiasCombinations[VisualNovelNames.INTRO_NOVEL] = new List<NovelAnalyserHelper>();
        }

        allPossibleNovelAnalyserHelpers[visualNovelName].Add(this);
    }

    public string GetPrompt()
    {
        return prompt.ToString().Replace("{{Context}}", objectUnderAnalyse.context);
    }

    public string GetPath()
    {
        return path;
    }    

    public static List<NovelAnalyserHelper> GetAllPossibleNovelAnalyserHelpers(VisualNovelNames visualNovelName)
    {
        List<NovelAnalyserHelper> allUniqueNovelAnalyserHelpersFromStartToEnd = new List<NovelAnalyserHelper>();

        foreach (NovelAnalyserHelper novelTester in allPossibleNovelAnalyserHelpers[visualNovelName])
        {
            if (novelTester.endetAnalysationByReachingEndNovelEvent)
            {
                allUniqueNovelAnalyserHelpersFromStartToEnd.Add(novelTester);
            }
        }
        return allUniqueNovelAnalyserHelpersFromStartToEnd;
    }

    public static int GetNumberOfPossiblePaths(VisualNovelNames visualNovelName)
    {
        return numberOfPossiblePaths[visualNovelName];
    }

    public VisualNovel GetObjectUnderTest()
    {
        return objectUnderAnalyse;
    }

    public void AnalyseNovel()
    {
        if (objectUnderAnalyse == null)
        {
            OnAnalysationFailed("Novel under test is null.", "-", "-");
            return;
        }
        objectUnderAnalyse.ClearGlobalVariables();
        objectUnderAnalyse.feedback = string.Empty;
        novelEvents = new Dictionary<string, VisualNovelEvent>();
        currentCharacters = new HashSet<Character>();
        choices = new List<VisualNovelEvent>();
        alreadyPlayedEvents = new HashSet<string>();

        if (string.IsNullOrEmpty(objectUnderAnalyse.title))
        {
            OnAnalysationFailed("Novel title is null or empty.", "-", "-");
            return;
        }
        if (objectUnderAnalyse.novelEvents?.Count <= 0)
        {
            OnAnalysationFailed("No novel events found.", objectUnderAnalyse.title, "-");
            return;
        }
        foreach (VisualNovelEvent novelEvent in objectUnderAnalyse.novelEvents)
        {
            novelEvents.Add(novelEvent?.id, novelEvent);
        }
        nextEventToAnalyse = objectUnderAnalyse.novelEvents[0];
        PlayNextEvent();
    }

    private void PlayNextEvent()
    {
        if (nextEventToAnalyse == null)
        {
            OnAnalysationFailed("Event to play is null!", objectUnderAnalyse.title, "-");
            return;
        }
        VisualNovelEvent eventUnderTest = nextEventToAnalyse;

        if (string.IsNullOrEmpty(eventUnderTest.id))
        {
            OnAnalysationFailed("Event id is null or empty!", objectUnderAnalyse.title, "-");
            return;
        }
        if (alreadyPlayedEvents.Contains(eventUnderTest.id))
        {
            this.loopDetected = true;
            OnAnalysationFailed("Loop Detected!", objectUnderAnalyse.title, eventUnderTest.id);
            //AnalysationEndedSuccessfully();
            return;
        }
        alreadyPlayedEvents.Add(eventUnderTest.id);

        if (string.IsNullOrEmpty(eventUnderTest.nextId) &&
           (VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) != VisualNovelEventType.SHOW_CHOICES_EVENT) &&
           (VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) != VisualNovelEventType.END_NOVEL_EVENT))
        {
            OnAnalysationFailed("Id of next event is null or empty!", objectUnderAnalyse.title, eventUnderTest.id);
            return;
        }
        if (!novelEvents.ContainsKey(eventUnderTest.nextId) &&
           (VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) != VisualNovelEventType.SHOW_CHOICES_EVENT) &&
           (VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) != VisualNovelEventType.END_NOVEL_EVENT))
        {
            OnAnalysationFailed("Next event to play not found!", objectUnderAnalyse.title, eventUnderTest.id);
            return;
        }
        if ((VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) == VisualNovelEventType.ADD_CHOICE_EVENT)
            && (string.IsNullOrEmpty(eventUnderTest.onChoice)))
        {
            OnAnalysationFailed("Add Choice event without onChoice value!", objectUnderAnalyse.title, eventUnderTest.id);
            return;
        }
        if ((VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) == VisualNovelEventType.ADD_CHOICE_EVENT) &&
            !novelEvents.ContainsKey(eventUnderTest.onChoice))
        {
            OnAnalysationFailed("Add Choice event with on choice target that could not be found!", objectUnderAnalyse.title, eventUnderTest.id);
            return;
        }
        VisualNovelEventType type = VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType);

        if ((VisualNovelEventTypeHelper.ValueOf(nextEventToAnalyse.eventType) != VisualNovelEventType.SHOW_CHOICES_EVENT) &&
    (VisualNovelEventTypeHelper.ValueOf(nextEventToAnalyse.eventType) != VisualNovelEventType.END_NOVEL_EVENT))
        {
            string nextEventID = eventUnderTest.nextId;
            nextEventToAnalyse = novelEvents[nextEventID];
        }

        switch (type)
        {
            case VisualNovelEventType.SET_BACKGROUND_EVENT:
                {
                    HandleBackgrundEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.CHARAKTER_JOIN_EVENT:
                {
                    HandleCharacterJoinEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.CHARAKTER_EXIT_EVENT:
                {
                    HandleCharacterExitEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.SHOW_MESSAGE_EVENT:
                {
                    HandleShowMessageEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.ADD_CHOICE_EVENT:
                {
                    HandleAddChoiceEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.SHOW_CHOICES_EVENT:
                {
                    HandleShowChoicesEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.END_NOVEL_EVENT:
                {
                    numberOfPossiblePaths[visualNovelName]++;
                    endetAnalysationByReachingEndNovelEvent = true;
                    AnalysationEndedSuccessfully();
                    break;
                }
            case VisualNovelEventType.PLAY_SOUND_EVENT:
                {
                    HandlePlaySoundEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.PLAY_ANIMATION_EVENT:
                {
                    HandlePlayAnimationEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.FREE_TEXT_INPUT_EVENT:
                {
                    HandleFreeTextInputEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.GPT_PROMPT_EVENT:
                {
                    HandleGptPromptEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.SAVE_PERSISTENT_EVENT:
                {
                    HandleSavePersistentEvent(eventUnderTest);
                    break;
                }
            case VisualNovelEventType.MARK_BIAS_EVENT:
                {
                    HandleMarkBiasEvent(eventUnderTest);
                    break;
                }
            default:
                {
                    OnAnalysationFailed("Event without event type!", objectUnderAnalyse.title, eventUnderTest.id);
                    return;
                }
        }
    }

    private void HandlePlaySoundEvent(VisualNovelEvent novelEvent)
    {
        if (KiteSoundHelper.ValueOf(novelEvent.audioClipToPlay) == KiteSound.NONE)
        {
            OnAnalysationFailed("Sound Event without audio clip!", objectUnderAnalyse.title, novelEvent.id);
            return;
        }
        PlayNextEvent();
    }

    private void HandlePlayAnimationEvent(VisualNovelEvent novelEvent)
    {
        if (KiteAnimationHelper.ValueOf(novelEvent.animationToPlay) == KiteAnimation.NONE)
        {
            OnAnalysationFailed("Animation Event without animation!", objectUnderAnalyse.title, novelEvent.id);
            return;
        }
        PlayNextEvent();
    }

    private void HandleFreeTextInputEvent(VisualNovelEvent novelEvent)
    {
        if (string.IsNullOrEmpty(novelEvent.questionForFreeTextInput))
        {
            OnAnalysationFailed("Freetext input Event without question!", objectUnderAnalyse.title, novelEvent.id);
            return;
        }
        if (string.IsNullOrEmpty(novelEvent.variablesName))
        {
            OnAnalysationFailed("Freetext input Event without variable!", objectUnderAnalyse.title, novelEvent.id);
            return;
        }
        PlayNextEvent();
    }

    private void HandleGptPromptEvent(VisualNovelEvent novelEvent)
    {
        if (string.IsNullOrEmpty(novelEvent.gptPrompt))
        {
            OnAnalysationFailed("GPT prompt event without prompt!", objectUnderAnalyse.title, novelEvent.id);
            return;
        }
        if (string.IsNullOrEmpty(novelEvent.variablesNameForGptPromp))
        {
            OnAnalysationFailed("GPT prompt event without variable!", objectUnderAnalyse.title, novelEvent.id);
            return;
        }
        PlayNextEvent();
    }

    private void HandleSavePersistentEvent(VisualNovelEvent novelEvent)
    {
        if (string.IsNullOrEmpty(novelEvent.key))
        {
            OnAnalysationFailed("Save persistent event without key!", objectUnderAnalyse.title, novelEvent.id);
            return;
        }
        if (string.IsNullOrEmpty(novelEvent.value))
        {
            OnAnalysationFailed("Save persistent event without value!", objectUnderAnalyse.title, novelEvent.id);
            return;
        }
        PlayNextEvent();
    }

    private void HandleMarkBiasEvent(VisualNovelEvent novelEvent)
    {
        if (DiscriminationBiasHelper.ValueOf(novelEvent.relevantBias) == DiscriminationBias.NONE)
        {
            OnAnalysationFailed("Discrimation bias event without discrimation bias!", objectUnderAnalyse.title, novelEvent.id);
            return;
        }
        PlayNextEvent();
    }

    private void HandleBackgrundEvent(VisualNovelEvent novelEvent)
    {
        if (LocationHelper.ValueOf(novelEvent.backgroundSpriteId) == Location.NONE)
        {
            OnAnalysationFailed("Location event without Location!", objectUnderAnalyse.title, novelEvent.id);
            return;
        }
        PlayNextEvent();
    }

    private void HandleCharacterJoinEvent(VisualNovelEvent novelEvent)
    {
        if (CharacterTypeHelper.ValueOf(novelEvent.character) == Character.NONE)
        {
            OnAnalysationFailed("Character joins event without character!", objectUnderAnalyse.title, novelEvent.id);
            return;
        }
        currentCharacters.Add(CharacterTypeHelper.ValueOf(novelEvent.character));
        PlayNextEvent();
    }

    private void HandleCharacterExitEvent(VisualNovelEvent novelEvent)
    {
        if ((CharacterTypeHelper.ValueOf(novelEvent.character) != Character.NONE) &&
            (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.OUTRO) &&
            (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.INTRO) &&
            (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.INFO) &&
            (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.PLAYER))
        {
            if (!currentCharacters.Contains(CharacterTypeHelper.ValueOf(novelEvent.character)))
            {
                OnAnalysationFailed("Character exit event with character that is not in the scene!", objectUnderAnalyse.title, novelEvent.id);
                return;
            }
            else
            {
                currentCharacters.Remove(CharacterTypeHelper.ValueOf(novelEvent.character));
            }
        }
        else
        {
            currentCharacters = new HashSet<Character>();
        }
        PlayNextEvent();
    }

    private void HandleShowMessageEvent(VisualNovelEvent novelEvent)
    {
        if (string.IsNullOrEmpty(novelEvent.text))
        {
            OnAnalysationFailed("Show message event without message!", objectUnderAnalyse.title, novelEvent.id);
            return;
        }
        if (!currentCharacters.Contains(CharacterTypeHelper.ValueOf(novelEvent.character)) &&
            (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.INTRO) &&
            (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.OUTRO) &&
            (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.INFO) &&
            (CharacterTypeHelper.ValueOf(novelEvent.character) != Character.PLAYER))
        {
            OnAnalysationFailed("Show message event with speaking character that is not in the scene!", objectUnderAnalyse.title, novelEvent.id);
            return;
        }
        if (CharacterExpressionHelper.ValueOf(novelEvent.expressionType) == CharacterExpression.NONE)
        {
            OnAnalysationFailed("Show message event without character expression!", objectUnderAnalyse.title, novelEvent.id);
            return;
        }

        AddFormattedLineToPrompt(CharacterTypeHelper.GetNameOfCharacter(novelEvent.character), novelEvent.text);

        PlayNextEvent();
    }

    public void InitializePrompt()
    {
        this.prompt = new StringBuilder();

        //ROLLE
        prompt.Append("Du bist eine Geschlechterforscherin. ");
        prompt.AppendLine();
        //AUFGABE
        prompt.Append("Deine Aufgabe ist es, den folgenden Dialog auf Diskriminierung hin zu untersuchen. ");
        prompt.AppendLine();
        //Kontext
        prompt.Append("{{Context}} ");
        prompt.AppendLine();
        //Output Format
        prompt.Append("Schreibe einen Analysetext. Stelle die Biases und Verzerrungen dar, auf die du dich beziehst (unten eine Liste mit Geschlechterbiases im Gr ndungsprozess). Richte den Text in der Du-Form an Lea, verwende dabei aber niemals ihren Namen. Sei wohlwollend und ermunternd. Sprich Lea nicht mit ihrem Namen an. Formuliere den Text aus einer unbestimmten Ich-Perspektive. ");
        prompt.AppendLine();
        //Wissens Basis
        prompt.Append("Hier die Liste mit Geschlechterbiases im Gr ndungsprozess:\r\nFinanzielle und Gesch ftliche Herausforderungen\r\n\r\nFinanzierungszugang\r\nBias Beschreibung: Schwierigkeiten von Frauen, Kapital f r ihre Unternehmen zu beschaffen.\r\nGender Pay Gap\r\nBias Beschreibung: Lohnungleichheit zwischen M nnern und Frauen.\r\nUnterbewertung weiblich gef hrter Unternehmen\r\nBias Beschreibung: Geringere Bewertung von Unternehmen, die von Frauen gef hrt werden.\r\nRisk Aversion Bias\r\nBias Beschreibung: Wahrnehmung von Frauen als risikoaverser.\r\nBest tigungsverzerrung\r\nBias Beschreibung: Tendenz, Informationen zu interpretieren, die bestehende Vorurteile best tigen.\r\nTokenism\r\nBias Beschreibung: Wahrnehmung von Frauen in unternehmerischen Kreisen als Alibifiguren.\r\nBias in der Wahrnehmung von F hrungsf higkeiten\r\nBias Beschreibung: Infragestellung der F hrungsf higkeiten von Frauen.\r\nIntersektionale und spezifische Biases\r\n\r\nRassistische und ethnische Biases\r\nBias Beschreibung: Zus tzliche Vorurteile gegen ber Frauen aus Minderheitengruppen.\r\nSozio konomische Biases\r\nBias Beschreibung: Gr  ere Hindernisse f r Frauen aus niedrigeren sozio konomischen Schichten.\r\nAlter- und Generationen-Biases\r\nBias: Diskriminierung aufgrund von Altersstereotypen.\r\nSexualit tsbezogene Biases\r\nBias: Vorurteile gegen ber lesbischen, bisexuellen oder queeren Frauen.\r\nBiases gegen ber Frauen mit Behinderungen\r\nBias: Zus tzliche Herausforderungen f r Frauen mit k rperlichen oder geistigen Behinderungen.\r\nStereotype gegen ber Frauen in nicht-traditionellen Branchen\r\nBias: Widerst nde gegen Frauen in m nnlich dominierten Feldern.\r\nKulturelle und religi se Biases\r\nBias: Diskriminierung aufgrund kultureller oder religi ser Zugeh rigkeit.\r\nBiases im Bereich der Rollen- und Familienwahrnehmung\r\n\r\nMaternal Bias\r\nBias: Annahmen  ber geringere Engagementbereitschaft von M ttern oder potenziellen M ttern.\r\nBiases gegen ber Frauen mit Kindern\r\nBias: Benachteiligung von M ttern, insbesondere Alleinerziehenden.\r\nErwartungshaltung bez glich Familienplanung\r\nBias: Annahmen  ber zuk nftige Familienplanung bei Frauen im geb rf higen Alter.\r\nWork-Life-Balance-Erwartungen\r\nBias: Druck auf Frauen, ein Gleichgewicht zwischen Beruf und Familie zu finden.\r\nKarriereentwicklungs- und Wahrnehmungsbiases\r\n\r\nGeschlechtsspezifische Stereotypen\r\nBias: Annahmen  ber geringere Kompetenz von Frauen in bestimmten Bereichen.\r\nDoppelte Bindung (Tightrope Bias)\r\nBias: Konflikt zwischen der Wahrnehmung als zu weich oder zu aggressiv.\r\nMikroaggressionen\r\nBias: Subtile Formen der Diskriminierung gegen ber Frauen.\r\nLeistungsattributions-Bias\r\nBias: Externe Zuschreibung von Erfolgen von Frauen.\r\nBias in Medien und Werbung\r\nBias: Verzerrte Darstellung von Unternehmerinnen in den Medien.\r\nUnbewusste Bias in der Kommunikation\r\nBias: Herabsetzende Art und Weise, wie  ber Frauenunternehmen gesprochen wird.\r\nProve-it-Again-Bias\r\nBias: Anforderung an Frauen, insbesondere in technischen Bereichen, ihre Kompetenzen wiederholt zu beweisen. ");
        prompt.AppendLine();
        //Analyse Objekt
        prompt.Append("Hier ist der Dialog:");
    }

    public void AddLineToPrompt(string line)
    {
        if (prompt == null)
        {
            prompt = new StringBuilder();
        }
        prompt.AppendLine(line);
    }

    public void AddFormattedLineToPrompt(string characterName, string text)
    {
        string formattedLine = $"<b>{characterName}:</b> {text}";
        AddLineToPrompt(formattedLine);
    }

    private void HandleAddChoiceEvent(VisualNovelEvent novelEvent)
    {
        choices.Add(novelEvent);

        if (string.IsNullOrEmpty(novelEvent.text))
        {
            OnAnalysationFailed("Add choice event without text!", objectUnderAnalyse.title, novelEvent.id);
            return;
        }
        PlayNextEvent();
    }

    private void HandleShowChoicesEvent(VisualNovelEvent novelEvent)
    {
        if (choices == null || choices.Count == 0)
        {
            OnAnalysationFailed("Show choices event without choices!", objectUnderAnalyse.title, novelEvent.id);
            return;
        }
        int index = 0;
        foreach (VisualNovelEvent visualNovelEvent in choices)
        {
            NovelAnalyserHelper novelTester = this.DeepCopy();
            novelTester.PerformChoice(novelEvents[visualNovelEvent.onChoice], visualNovelEvent, index);
            index++;
        }
    }

    private void PerformChoice(VisualNovelEvent visualNovelEvent, VisualNovelEvent addChoiceEvent, int index)
    {
        if (string.IsNullOrEmpty(path))
        {
            path = index.ToString();

        }
        else
        {
            path = path + ":" + index;
        }
        if (addChoiceEvent.show)
        {
            AddFormattedLineToPrompt(CharacterTypeHelper.GetNameOfCharacter(Character.PLAYER), addChoiceEvent.text);
        }

        if (visualNovelEvent == null)
        {
            OnAnalysationFailed("On choice event with target that could not be found!", objectUnderAnalyse.title, visualNovelEvent.id);
            return;
        }
        nextEventToAnalyse = visualNovelEvent;
        choices = new List<VisualNovelEvent>();
        PlayNextEvent();
    }

    private NovelAnalyserHelper DeepCopy()
    {
        NovelAnalyserHelper newCopy = new NovelAnalyserHelper(objectUnderAnalyse.DeepCopy());
        newCopy.loopDetected = this.loopDetected;
        newCopy.visualNovelName = this.visualNovelName;

        newCopy.novelEvents = new Dictionary<string, VisualNovelEvent>();
        foreach (var entry in this.novelEvents)
        {
            newCopy.novelEvents.Add(entry.Key, entry.Value.DeepCopy());
        }

        if (this.nextEventToAnalyse != null)
            newCopy.nextEventToAnalyse = this.nextEventToAnalyse.DeepCopy();

        newCopy.currentCharacters = new HashSet<Character>();
        foreach (Character character in this.currentCharacters)
        {
            newCopy.currentCharacters.Add(character);
        }

        newCopy.choices = new List<VisualNovelEvent>();

        foreach (VisualNovelEvent choice in this.choices)
        {
            newCopy.choices.Add(choice.DeepCopy());
        }

        newCopy.alreadyPlayedEvents = new HashSet<string>();

        foreach (string alreadyPlayedEvent in this.alreadyPlayedEvents)
        {
            newCopy.alreadyPlayedEvents.Add(alreadyPlayedEvent);
        }

        newCopy.path = this.path;
        newCopy.prompt = new StringBuilder(this.prompt.ToString());
        newCopy.parent = this;
        newCopy.isOriginalNode = false;
        children++;
        return newCopy;
    }

    private void SuccessfullyEndOfAnalysationTriggerdByChildren()
    {
        if (isAnalysationFinished)
        {
            return;
        }
        children--;

        if (children == 0)
        {
            AnalysationEndedSuccessfully();
        }
    }

    private void FailedEndOfAnalysationTriggerdByChildren()
    {
        if (isAnalysationFinished)
        {
            return;
        }
        isAnalysationFinished = true;
        AnalysationEndedEarly();
    }

    private void AnalysationEndedSuccessfully()
    {
        isAnalysationFinished = true;
        isAnalysationSuccessfull = true;
        parent?.SuccessfullyEndOfAnalysationTriggerdByChildren();
    }

    private void AnalysationEndedEarly()
    {
        if (isOriginalNode)
        {
            Debug.LogError("Finished Test of Novel with Errors. Novel under Test: " + objectUnderAnalyse.title + ";");
        }
        else
        {
            parent?.FailedEndOfAnalysationTriggerdByChildren();
        }
    }

    private void OnAnalysationFailed(string error, string visualNovelUnderTest, string eventUnderTest)
    {
        isAnalysationFinished = true;
        AnalysationEndedEarly();
        Debug.LogError("Error while testing novel. Novel under test: " + visualNovelUnderTest + "; Event under test: " + eventUnderTest + "; Error: " + error + ";");
    }

    public bool IsAnalysationOver()
    {
        return isAnalysationFinished;
    }

    public bool IsAnalysationSuccessfull()
    {
        return isAnalysationSuccessfull;
    }

}
