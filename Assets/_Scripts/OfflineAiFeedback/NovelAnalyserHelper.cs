using System.Collections.Generic;
using System.Text;
using Assets._Scripts.Novel;
using Assets._Scripts.Player;
using UnityEngine;

namespace Assets._Scripts.OfflineAiFeedback
{
    public class NovelAnalyserHelper
    {
        private Dictionary<string, VisualNovelEvent> _novelEvents;
        private VisualNovelEvent _nextEventToAnalyse;
        private readonly VisualNovel _objectUnderAnalyse;
        private VisualNovelNames _visualNovelName;
        private HashSet<CharacterRole> _currentCharacters;
        private List<VisualNovelEvent> _choices;
        private HashSet<string> _alreadyPlayedEvents;
        private bool _isOriginalNode = true;
        private int _children;
        private NovelAnalyserHelper _parent;
        private bool _isAnalysisFinished;
        private bool _isAnalysisSuccessful;
        private static Dictionary<VisualNovelNames, int> _numberOfPossiblePaths;
        private static Dictionary<VisualNovelNames, List<NovelAnalyserHelper>> _allPossibleNovelAnalyserHelpers;
        private static Dictionary<VisualNovelNames, List<NovelAnalyserHelper>> _allPossibleBiasCombinations;
        private string _path;
        private StringBuilder _prompt;
        private bool _endetAnalysisByReachingEndNovelEvent;
        public bool LoopDetected;

        public NovelAnalyserHelper(VisualNovel novelToAnalyse)
        {
            _objectUnderAnalyse = novelToAnalyse;
            _visualNovelName = VisualNovelNamesHelper.ValueOf((int)novelToAnalyse.id);
            InitializePrompt();
            _path = "";
            _currentCharacters = new HashSet<CharacterRole>();
            _choices = new List<VisualNovelEvent>();
            _alreadyPlayedEvents = new HashSet<string>();

            if (_numberOfPossiblePaths == null)
            {
                _numberOfPossiblePaths = new Dictionary<VisualNovelNames, int>();
                _numberOfPossiblePaths[VisualNovelNames.BankKreditNovel] = 0;
                _numberOfPossiblePaths[VisualNovelNames.InvestorNovel] = 0;
                _numberOfPossiblePaths[VisualNovelNames.ElternNovel] = 0;
                _numberOfPossiblePaths[VisualNovelNames.NotariatNovel] = 0;
                _numberOfPossiblePaths[VisualNovelNames.PresseNovel] = 0;
                _numberOfPossiblePaths[VisualNovelNames.VermieterNovel] = 0;
                _numberOfPossiblePaths[VisualNovelNames.HonorarNovel] = 0;
                _numberOfPossiblePaths[VisualNovelNames.EinstiegsNovel] = 0;
            }

            if (_allPossibleNovelAnalyserHelpers == null)
            {
                _allPossibleNovelAnalyserHelpers = new Dictionary<VisualNovelNames, List<NovelAnalyserHelper>>();
                _allPossibleNovelAnalyserHelpers[VisualNovelNames.BankKreditNovel] = new List<NovelAnalyserHelper>();
                _allPossibleNovelAnalyserHelpers[VisualNovelNames.InvestorNovel] = new List<NovelAnalyserHelper>();
                _allPossibleNovelAnalyserHelpers[VisualNovelNames.ElternNovel] = new List<NovelAnalyserHelper>();
                _allPossibleNovelAnalyserHelpers[VisualNovelNames.NotariatNovel] = new List<NovelAnalyserHelper>();
                _allPossibleNovelAnalyserHelpers[VisualNovelNames.PresseNovel] = new List<NovelAnalyserHelper>();
                _allPossibleNovelAnalyserHelpers[VisualNovelNames.VermieterNovel] = new List<NovelAnalyserHelper>(); 
                _allPossibleNovelAnalyserHelpers[VisualNovelNames.HonorarNovel] = new List<NovelAnalyserHelper>();
                _allPossibleNovelAnalyserHelpers[VisualNovelNames.EinstiegsNovel] = new List<NovelAnalyserHelper>();
            }

            if (_allPossibleBiasCombinations == null)
            {
                _allPossibleBiasCombinations = new Dictionary<VisualNovelNames, List<NovelAnalyserHelper>>();
                _allPossibleBiasCombinations[VisualNovelNames.BankKreditNovel] = new List<NovelAnalyserHelper>();
                _allPossibleBiasCombinations[VisualNovelNames.InvestorNovel] = new List<NovelAnalyserHelper>();
                _allPossibleBiasCombinations[VisualNovelNames.ElternNovel] = new List<NovelAnalyserHelper>();
                _allPossibleBiasCombinations[VisualNovelNames.NotariatNovel] = new List<NovelAnalyserHelper>();
                _allPossibleBiasCombinations[VisualNovelNames.PresseNovel] = new List<NovelAnalyserHelper>();
                _allPossibleBiasCombinations[VisualNovelNames.VermieterNovel] = new List<NovelAnalyserHelper>();
                _allPossibleBiasCombinations[VisualNovelNames.HonorarNovel] = new List<NovelAnalyserHelper>();
                _allPossibleBiasCombinations[VisualNovelNames.EinstiegsNovel] = new List<NovelAnalyserHelper>();
            }

            _allPossibleNovelAnalyserHelpers[_visualNovelName].Add(this);
        }

        public string GetPrompt()
        {
            return _prompt.ToString().Replace("{{Context}}", _objectUnderAnalyse.context);
        }

        public string GetPath()
        {
            return _path;
        }

        public static List<NovelAnalyserHelper> GetAllPossibleNovelAnalyserHelpers(VisualNovelNames visualNovelName)
        {
            List<NovelAnalyserHelper> allUniqueNovelAnalyserHelpersFromStartToEnd = new List<NovelAnalyserHelper>();

            foreach (NovelAnalyserHelper novelTester in _allPossibleNovelAnalyserHelpers[visualNovelName])
            {
                if (novelTester._endetAnalysisByReachingEndNovelEvent)
                {
                    allUniqueNovelAnalyserHelpersFromStartToEnd.Add(novelTester);
                }
            }

            return allUniqueNovelAnalyserHelpersFromStartToEnd;
        }

        public void AnalyseNovel()
        {
            if (_objectUnderAnalyse == null)
            {
                OnAnalysisFailed("Novel under test is null.", "-", "-");
                return;
            }

            _objectUnderAnalyse.ClearGlobalVariables();
            _objectUnderAnalyse.feedback = string.Empty;
            _novelEvents = new Dictionary<string, VisualNovelEvent>();
            _currentCharacters = new HashSet<CharacterRole>();
            _choices = new List<VisualNovelEvent>();
            _alreadyPlayedEvents = new HashSet<string>();

            if (string.IsNullOrEmpty(_objectUnderAnalyse.title))
            {
                OnAnalysisFailed("Novel title is null or empty.", "-", "-");
                return;
            }

            if (_objectUnderAnalyse.novelEvents?.Count <= 0)
            {
                OnAnalysisFailed("No novel events found.", _objectUnderAnalyse.title, "-");
                return;
            }

            foreach (VisualNovelEvent novelEvent in _objectUnderAnalyse.novelEvents)
            {
                _novelEvents.Add(novelEvent?.id, novelEvent);
            }

            _nextEventToAnalyse = _objectUnderAnalyse.novelEvents[0];
            PlayNextEvent();
        }

        private void PlayNextEvent()
        {
            if (_nextEventToAnalyse == null)
            {
                OnAnalysisFailed("Event to play is null!", _objectUnderAnalyse.title, "-");
                return;
            }

            VisualNovelEvent eventUnderTest = _nextEventToAnalyse;

            if (string.IsNullOrEmpty(eventUnderTest.id))
            {
                OnAnalysisFailed("Event id is null or empty!", _objectUnderAnalyse.title, "-");
                return;
            }

            if (_alreadyPlayedEvents.Contains(eventUnderTest.id))
            {
                this.LoopDetected = true;
                OnAnalysisFailed("Loop Detected!", _objectUnderAnalyse.title, eventUnderTest.id);
                //AnalysisEndedSuccessfully();
                return;
            }

            _alreadyPlayedEvents.Add(eventUnderTest.id);

            if (string.IsNullOrEmpty(eventUnderTest.nextId) &&
                (VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) !=
                 VisualNovelEventType.SHOW_CHOICES_EVENT) &&
                (VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) != VisualNovelEventType.END_NOVEL_EVENT))
            {
                OnAnalysisFailed("Id of next event is null or empty!", _objectUnderAnalyse.title, eventUnderTest.id);
                return;
            }

            if (!_novelEvents.ContainsKey(eventUnderTest.nextId) &&
                (VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) !=
                 VisualNovelEventType.SHOW_CHOICES_EVENT) &&
                (VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) != VisualNovelEventType.END_NOVEL_EVENT))
            {
                OnAnalysisFailed("Next event to play not found!", _objectUnderAnalyse.title, eventUnderTest.id);
                return;
            }

            if ((VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) == VisualNovelEventType.ADD_CHOICE_EVENT)
                && (string.IsNullOrEmpty(eventUnderTest.onChoice)))
            {
                OnAnalysisFailed("Add Choice event without onChoice value!", _objectUnderAnalyse.title,
                    eventUnderTest.id);
                return;
            }

            if ((VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType) ==
                 VisualNovelEventType.ADD_CHOICE_EVENT) &&
                !_novelEvents.ContainsKey(eventUnderTest.onChoice))
            {
                OnAnalysisFailed("Add Choice event with on choice target that could not be found!",
                    _objectUnderAnalyse.title, eventUnderTest.id);
                return;
            }

            VisualNovelEventType type = VisualNovelEventTypeHelper.ValueOf(eventUnderTest.eventType);

            if ((VisualNovelEventTypeHelper.ValueOf(_nextEventToAnalyse.eventType) !=
                 VisualNovelEventType.SHOW_CHOICES_EVENT) &&
                (VisualNovelEventTypeHelper.ValueOf(_nextEventToAnalyse.eventType) !=
                 VisualNovelEventType.END_NOVEL_EVENT))
            {
                string nextEventID = eventUnderTest.nextId;
                _nextEventToAnalyse = _novelEvents[nextEventID];
            }

            switch (type)
            {
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
                    _numberOfPossiblePaths[_visualNovelName]++;
                    _endetAnalysisByReachingEndNovelEvent = true;
                    AnalysisEndedSuccessfully();
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
                    OnAnalysisFailed("Event without event type!", _objectUnderAnalyse.title, eventUnderTest.id);
                    return;
                }
            }
        }

        private void HandlePlaySoundEvent(VisualNovelEvent novelEvent)
        {
            if (KiteSoundHelper.ValueOf(novelEvent.audioClipToPlay) == KiteSound.None)
            {
                OnAnalysisFailed("Sound Event without audio clip!", _objectUnderAnalyse.title, novelEvent.id);
                return;
            }

            PlayNextEvent();
        }

        private void HandlePlayAnimationEvent(VisualNovelEvent novelEvent)
        {
            if (KiteAnimationHelper.ValueOf(novelEvent.animationToPlay) == KiteAnimation.None)
            {
                OnAnalysisFailed("Animation Event without animation!", _objectUnderAnalyse.title, novelEvent.id);
                return;
            }

            PlayNextEvent();
        }

        private void HandleFreeTextInputEvent(VisualNovelEvent novelEvent)
        {
            if (string.IsNullOrEmpty(novelEvent.questionForFreeTextInput))
            {
                OnAnalysisFailed("Freetext input Event without question!", _objectUnderAnalyse.title, novelEvent.id);
                return;
            }

            if (string.IsNullOrEmpty(novelEvent.variablesName))
            {
                OnAnalysisFailed("Freetext input Event without variable!", _objectUnderAnalyse.title, novelEvent.id);
                return;
            }

            PlayNextEvent();
        }

        private void HandleGptPromptEvent(VisualNovelEvent novelEvent)
        {
            if (string.IsNullOrEmpty(novelEvent.gptPrompt))
            {
                OnAnalysisFailed("GPT prompt event without prompt!", _objectUnderAnalyse.title, novelEvent.id);
                return;
            }

            if (string.IsNullOrEmpty(novelEvent.variablesNameForGptPrompt))
            {
                OnAnalysisFailed("GPT prompt event without variable!", _objectUnderAnalyse.title, novelEvent.id);
                return;
            }

            PlayNextEvent();
        }

        private void HandleSavePersistentEvent(VisualNovelEvent novelEvent)
        {
            if (string.IsNullOrEmpty(novelEvent.key))
            {
                OnAnalysisFailed("Save persistent event without key!", _objectUnderAnalyse.title, novelEvent.id);
                return;
            }

            if (string.IsNullOrEmpty(novelEvent.value))
            {
                OnAnalysisFailed("Save persistent event without value!", _objectUnderAnalyse.title, novelEvent.id);
                return;
            }

            PlayNextEvent();
        }

        private void HandleMarkBiasEvent(VisualNovelEvent novelEvent)
        {
            if (DiscriminationBiasHelper.ValueOf(novelEvent.relevantBias) == DiscriminationBias.None)
            {
                OnAnalysisFailed("Discrimination bias event without discrimination bias!", _objectUnderAnalyse.title,
                    novelEvent.id);
                return;
            }

            PlayNextEvent();
        }

        private void HandleCharacterJoinEvent(VisualNovelEvent novelEvent)
        {
            if (CharacterTypeHelper.ValueOf(novelEvent.character) == CharacterRole.None)
            {
                OnAnalysisFailed("CharacterRole joins event without character!", _objectUnderAnalyse.title,
                    novelEvent.id);
                return;
            }

            _currentCharacters.Add(CharacterTypeHelper.ValueOf(novelEvent.character));
            PlayNextEvent();
        }

        private void HandleCharacterExitEvent(VisualNovelEvent novelEvent)
        {
            if ((CharacterTypeHelper.ValueOf(novelEvent.character) != CharacterRole.None) &&
                (CharacterTypeHelper.ValueOf(novelEvent.character) != CharacterRole.Outro) &&
                (CharacterTypeHelper.ValueOf(novelEvent.character) != CharacterRole.Intro) &&
                (CharacterTypeHelper.ValueOf(novelEvent.character) != CharacterRole.Info) &&
                (CharacterTypeHelper.ValueOf(novelEvent.character) != CharacterRole.Player))
            {
                if (!_currentCharacters.Contains(CharacterTypeHelper.ValueOf(novelEvent.character)))
                {
                    OnAnalysisFailed("CharacterRole exit event with character that is not in the scene!",
                        _objectUnderAnalyse.title, novelEvent.id);
                    return;
                }
                else
                {
                    _currentCharacters.Remove(CharacterTypeHelper.ValueOf(novelEvent.character));
                }
            }
            else
            {
                _currentCharacters = new HashSet<CharacterRole>();
            }

            PlayNextEvent();
        }

        private void HandleShowMessageEvent(VisualNovelEvent novelEvent)
        {
            if (string.IsNullOrEmpty(novelEvent.text))
            {
                OnAnalysisFailed("Show message event without message!", _objectUnderAnalyse.title, novelEvent.id);
                return;
            }

            if (!_currentCharacters.Contains(CharacterTypeHelper.ValueOf(novelEvent.character)) &&
                (CharacterTypeHelper.ValueOf(novelEvent.character) != CharacterRole.Intro) &&
                (CharacterTypeHelper.ValueOf(novelEvent.character) != CharacterRole.Outro) &&
                (CharacterTypeHelper.ValueOf(novelEvent.character) != CharacterRole.Info) &&
                (CharacterTypeHelper.ValueOf(novelEvent.character) != CharacterRole.Player))
            {
                OnAnalysisFailed("Show message event with speaking character that is not in the scene!",
                    _objectUnderAnalyse.title, novelEvent.id);
                return;
            }

            if (CharacterExpressionHelper.ValueOf(novelEvent.expressionType) == CharacterExpression.None)
            {
                OnAnalysisFailed("Show message event without character expression!", _objectUnderAnalyse.title,
                    novelEvent.id);
                return;
            }

            AddFormattedLineToPrompt(CharacterTypeHelper.GetNameOfCharacter(novelEvent.character), novelEvent.text);

            PlayNextEvent();
        }

        private void InitializePrompt()
        {
            this._prompt = new StringBuilder();

            //ROLLE
            _prompt.Append("Du bist eine Geschlechterforscherin. ");
            _prompt.AppendLine();
            //AUFGABE
            _prompt.Append("Deine Aufgabe ist es, den folgenden Dialog auf Diskriminierung hin zu untersuchen. ");
            _prompt.AppendLine();
            //Kontext
            _prompt.Append("{{Context}} ");
            _prompt.AppendLine();
            //Output Format
            _prompt.Append(
                "Schreibe einen Analysetext. Stelle die Biases und Verzerrungen dar, auf die du dich beziehst (unten eine Liste mit Geschlechterbiases im Gründungsprozess). Richte den Text in der Du-Form an Lea, verwende dabei aber niemals ihren Namen. Sei wohlwollend und ermunternd. Sprich Lea nicht mit ihrem Namen an. Formuliere den Text aus einer unbestimmten Ich-Perspektive. ");
            _prompt.AppendLine();
            //Wissens Basis
            _prompt.Append(
                "Hier die Liste mit Geschlechterbiases im Gründungsprozess:\r\nFinanzielle und Geschäftliche Herausforderungen\r\n\r\nFinanzierungszugang\r\nBias Beschreibung: Schwierigkeiten von Frauen, Kapital f r ihre Unternehmen zu beschaffen.\r\nGender Pay Gap\r\nBias Beschreibung: Lohnungleichheit zwischen Männern und Frauen.\r\nUnterbewertung weiblich geführter Unternehmen\r\nBias Beschreibung: Geringere Bewertung von Unternehmen, die von Frauen geführt werden.\r\nRisk Aversion Bias\r\nBias Beschreibung: Wahrnehmung von Frauen als risikoaverser.\r\nBestätigungsverzerrung\r\nBias Beschreibung: Tendenz, Informationen zu interpretieren, die bestehende Vorurteile bestätigen.\r\nTokenism\r\nBias Beschreibung: Wahrnehmung von Frauen in unternehmerischen Kreisen als Alibifiguren.\r\nBias in der Wahrnehmung von Führungsfähigkeiten\r\nBias Beschreibung: Infragestellung der Führungsfähigkeiten von Frauen.\r\nIntersektionale und spezifische Biases\r\n\r\nRassistische und ethnische Biases\r\nBias Beschreibung: Zusätzliche Vorurteile gegen ber Frauen aus Minderheitengruppen.\r\nSozioökonomische Biases\r\nBias Beschreibung: Gr  ere Hindernisse f r Frauen aus niedrigeren sozioökonomischen Schichten.\r\nAlter- und Generationen-Biases\r\nBias: Diskriminierung aufgrund von Altersstereotypen.\r\nSexualitätsbezogene Biases\r\nBias: Vorurteile gegen ber lesbischen, bisexuellen oder queeren Frauen.\r\nBiases gegen ber Frauen mit Behinderungen\r\nBias: Zusätzliche Herausforderungen für Frauen mit körperlichen oder geistigen Behinderungen.\r\nStereotype gegen ber Frauen in nicht-traditionellen Branchen\r\nBias: Widerst nde gegen Frauen in männlich dominierten Feldern.\r\nKulturelle und religiöse Biases\r\nBias: Diskriminierung aufgrund kultureller oder religiöser Zugehörigkeit.\r\nBiases im Bereich der Rollen- und Familienwahrnehmung\r\n\r\nMaternal Bias\r\nBias: Annahmen  ber geringere Engagementbereitschaft von Müttern oder potenziellen Müttern.\r\nBiases gegen ber Frauen mit Kindern\r\nBias: Benachteiligung von Müttern, insbesondere Alleinerziehenden.\r\nErwartungshaltung bez glich Familienplanung\r\nBias: Annahmen  ber zukünftige Familienplanung bei Frauen im gebärfähigen Alter.\r\nWork-Life-Balance-Erwartungen\r\nBias: Druck auf Frauen, ein Gleichgewicht zwischen Beruf und Familie zu finden.\r\nKarriereentwicklungs- und Wahrnehmungsbiases\r\n\r\nGeschlechtsspezifische Stereotypen\r\nBias: Annahmen  ber geringere Kompetenz von Frauen in bestimmten Bereichen.\r\nDoppelte Bindung (Tightrope Bias)\r\nBias: Konflikt zwischen der Wahrnehmung als zu weich oder zu aggressiv.\r\nMikroaggressionen\r\nBias: Subtile Formen der Diskriminierung gegen ber Frauen.\r\nLeistungsattributions-Bias\r\nBias: Externe Zuschreibung von Erfolgen von Frauen.\r\nBias in Medien und Werbung\r\nBias: Verzerrte Darstellung von Unternehmerinnen in den Medien.\r\nUnbewusste Bias in der Kommunikation\r\nBias: Herabsetzende Art und Weise, wie  ber Frauenunternehmen gesprochen wird.\r\nProve-it-Again-Bias\r\nBias: Anforderung an Frauen, insbesondere in technischen Bereichen, ihre Kompetenzen wiederholt zu beweisen. ");
            _prompt.AppendLine();
            //Analyse Objekt
            _prompt.Append("Hier ist der Dialog:");
        }

        private void AddLineToPrompt(string line)
        {
            if (_prompt == null)
            {
                _prompt = new StringBuilder();
            }

            _prompt.AppendLine(line);
        }

        private void AddFormattedLineToPrompt(string characterName, string text)
        {
            string formattedLine = $"<b>{characterName}:</b> {text}";
            AddLineToPrompt(formattedLine);
        }

        private void HandleAddChoiceEvent(VisualNovelEvent novelEvent)
        {
            _choices.Add(novelEvent);

            if (string.IsNullOrEmpty(novelEvent.text))
            {
                OnAnalysisFailed("Add choice event without text!", _objectUnderAnalyse.title, novelEvent.id);
                return;
            }

            PlayNextEvent();
        }

        private void HandleShowChoicesEvent(VisualNovelEvent novelEvent)
        {
            if (_choices == null || _choices.Count == 0)
            {
                OnAnalysisFailed("Show choices event without choices!", _objectUnderAnalyse.title, novelEvent.id);
                return;
            }

            int index = 0;
            foreach (VisualNovelEvent visualNovelEvent in _choices)
            {
                NovelAnalyserHelper novelTester = this.DeepCopy();
                novelTester.PerformChoice(_novelEvents[visualNovelEvent.onChoice], visualNovelEvent, index);
                index++;
            }
        }

        private void PerformChoice(VisualNovelEvent visualNovelEvent, VisualNovelEvent addChoiceEvent, int index)
        {
            if (string.IsNullOrEmpty(_path))
            {
                _path = index.ToString();
            }
            else
            {
                _path = _path + ":" + index;
            }

            if (addChoiceEvent.show)
            {
                AddFormattedLineToPrompt(CharacterTypeHelper.GetNameOfCharacter(CharacterRole.Player),
                    addChoiceEvent.text);
            }

            if (visualNovelEvent == null)
            {
                OnAnalysisFailed("On choice event with target that could not be found!", _objectUnderAnalyse.title,
                    visualNovelEvent.id);
                return;
            }

            _nextEventToAnalyse = visualNovelEvent;
            _choices = new List<VisualNovelEvent>();
            PlayNextEvent();
        }

        private NovelAnalyserHelper DeepCopy()
        {
            NovelAnalyserHelper newCopy = new NovelAnalyserHelper(_objectUnderAnalyse.DeepCopy());
            newCopy.LoopDetected = this.LoopDetected;
            newCopy._visualNovelName = this._visualNovelName;

            newCopy._novelEvents = new Dictionary<string, VisualNovelEvent>();
            foreach (var entry in this._novelEvents)
            {
                newCopy._novelEvents.Add(entry.Key, entry.Value.DeepCopy());
            }

            if (this._nextEventToAnalyse != null)
                newCopy._nextEventToAnalyse = this._nextEventToAnalyse.DeepCopy();

            newCopy._currentCharacters = new HashSet<CharacterRole>();
            foreach (CharacterRole character in this._currentCharacters)
            {
                newCopy._currentCharacters.Add(character);
            }

            newCopy._choices = new List<VisualNovelEvent>();

            foreach (VisualNovelEvent choice in this._choices)
            {
                newCopy._choices.Add(choice.DeepCopy());
            }

            newCopy._alreadyPlayedEvents = new HashSet<string>();

            foreach (string alreadyPlayedEvent in this._alreadyPlayedEvents)
            {
                newCopy._alreadyPlayedEvents.Add(alreadyPlayedEvent);
            }

            newCopy._path = this._path;
            newCopy._prompt = new StringBuilder(this._prompt.ToString());
            newCopy._parent = this;
            newCopy._isOriginalNode = false;
            _children++;
            return newCopy;
        }

        private void SuccessfullyEndOfAnalysisTriggeredByChildren()
        {
            if (_isAnalysisFinished)
            {
                return;
            }

            _children--;

            if (_children == 0)
            {
                AnalysisEndedSuccessfully();
            }
        }

        private void FailedEndOfAnalysisTriggeredByChildren()
        {
            if (_isAnalysisFinished)
            {
                return;
            }

            _isAnalysisFinished = true;
            AnalysisEndedEarly();
        }

        private void AnalysisEndedSuccessfully()
        {
            _isAnalysisFinished = true;
            _isAnalysisSuccessful = true;
            _parent?.SuccessfullyEndOfAnalysisTriggeredByChildren();
        }

        private void AnalysisEndedEarly()
        {
            if (_isOriginalNode)
            {
                Debug.LogError("Finished Test of Novel with Errors. Novel under Test: " + _objectUnderAnalyse.title +
                               ";");
            }
            else
            {
                _parent?.FailedEndOfAnalysisTriggeredByChildren();
            }
        }

        private void OnAnalysisFailed(string error, string visualNovelUnderTest, string eventUnderTest)
        {
            _isAnalysisFinished = true;
            AnalysisEndedEarly();
            Debug.LogError("Error while testing novel. Novel under test: " + visualNovelUnderTest +
                           "; Event under test: " + eventUnderTest + "; Error: " + error + ";");
        }

        public bool IsAnalysisOver()
        {
            return _isAnalysisFinished;
        }

        public bool IsAnalysisSuccessful()
        {
            return _isAnalysisSuccessful;
        }
    }
}