using System;
using System.IO;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Assets._Scripts._Mappings;
using Assets._Scripts.Controller.CharacterController;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.Player;
using Assets._Scripts.SaveNovelData;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.UIElements.Messages;
using Assets._Scripts.Utilities;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Typewriter._Core;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    /// <summary>
    /// Represents a mapping of a character's visual appearance in a novel scene.
    /// </summary>
    [Serializable]
    public class CharacterVisualEntry
    {
        public string characterName;
        public GameObject prefab;
    }

    /// <summary>
    /// Manages the flow and interaction of a visual novel scene, including dialog progression,
    /// character expressions, event handling, and user interactions during the gameplay.
    /// </summary>
    public class PlayNovelSceneController : SceneController
    {
        private const float WaitingTime = 0.5f;

        private static PlayNovelSceneController _instance;

        [Header("UI-Komponenten")] 
        [SerializeField] private GameObject viewPort;
        [SerializeField] private GameObject conversationViewport;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button legalInformationButton;
        [SerializeField] private ConversationContentGuiController conversationContent;
        [SerializeField] public Button confirmArea;
        [SerializeField] public Button confirmArea2;
        [SerializeField] private ChatScrollView chatScroll;
        [SerializeField] private GameObject headerImage;

        [Header("Novel-Visuals und Prefabs")] 
        [SerializeField] private GameObject[] novelVisuals;
        [SerializeField] private List<CharacterVisualEntry> novelVisualMappings = new();
        [SerializeField] private GameObject novelImageContainer;
        [SerializeField] private GameObject backgroundContainer;
        [SerializeField] private GameObject[] backgroundPrefab;
        [SerializeField] private GameObject[] deskPrefab;
        [SerializeField] private GameObject[] decoDeskPrefab;
        [SerializeField] private GameObject[] decoBackgroundPrefab;
        [SerializeField] private GameObject currentBackground;
        [SerializeField] private GameObject currentDesk;
        [SerializeField] private GameObject currentDecoDesk;
        [SerializeField] private GameObject currentDecoBackground;
        [SerializeField] private GameObject[] novelAnimations;
        [SerializeField] private GameObject viewPortOfImages;
        [SerializeField] private GameObject currentAnimation;

        [Header("GPT und MessageBox")] 
        [SerializeField] private GameObject gptServerCallPrefab;
        [SerializeField] private LeaveNovelAndGoBackMessageBox leaveGameAndGoBackMessageBoxObject;
        [SerializeField] private GameObject leaveGameAndGoBackMessageBox;
        [SerializeField] private GameObject hintForSavegameMessageBox;

        [Header("Skript- und Controller-Referenzen")] 
        [SerializeField] private VisualNovel novelToPlay;
        [SerializeField] public TypewriterCore currentTypeWriter;
        [SerializeField] public SelectOptionContinueConversation selectOptionContinueConversation;
        [SerializeField] private Kite2CharacterController currentTalkingKite2CharacterController;

        [Header("Spielstatus und Logik")]
        [SerializeField] private bool isWaitingForConfirmation;
        [SerializeField] private VisualNovelEvent nextEventToPlay;
        [SerializeField] private bool isTyping;
        [SerializeField] private List<string> playThroughHistory = new();
        [SerializeField] private List<VisualNovelEvent> eventHistory = new();

        private readonly Dictionary<string, VisualNovelEvent> _novelEvents = new();
        private readonly string[] _optionsId = new string[2];
        private readonly Dictionary<string, int> _soundIndexByName = new(StringComparer.OrdinalIgnoreCase);
        private bool _audioReady;
        private Dictionary<string, GameObject> _characterToPrefabMap;

        private AudioClip[] _clips;

        private bool _clipsDumpedOnce;
        private ConversationContentGuiController _conversationContentGuiController;
        private GameObject _hintForSavegameMessageBoxObject;
        private int _novelCharacter;
        private NovelImageController _novelImagesController;
        private int _optionsCount;
        private VisualNovelEvent _savedEventToResume;
        private IEnumerator _speakingCoroutine;
        private Coroutine _timerCoroutine;
        private bool _typingWasSkipped;
        
        public bool isPaused;

        public Dictionary<int, int> CharacterExpressions { get; } = new();
        public VisualNovel NovelToPlay => novelToPlay;
        public List<string> PlayThroughHistory => playThroughHistory;
        public string[] OptionsId => _optionsId;
        public List<VisualNovelEvent> EventHistory => eventHistory;
        public NovelImageController NovelImageController => _novelImagesController;

        /// <summary>
        /// Provides a globally accessible instance of the <see cref="PlayNovelSceneController"/> class.
        /// This singleton pattern ensures that only one instance of the controller exists in the application.
        /// </summary>
        /// <remarks>
        /// If no instance is already present, it automatically creates one, attaches it to a new GameObject, and persists it across scenes by using DontDestroyOnLoad.
        /// This property facilitates managing the visual novel scene's lifecycle, interaction, and progression by offering a centralized access point.
        /// </remarks>
        public static PlayNovelSceneController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<PlayNovelSceneController>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("PlayNovelSceneController");
                        _instance = obj.AddComponent<PlayNovelSceneController>();
                        DontDestroyOnLoad(obj);
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Initializes the PlayNovelSceneController, including setting up references,
        /// clearing data managers, and initializing components.
        /// </summary>
        private void Start()
        {
            FooterActivationManager.Instance().SetFooterActivated(false);

            _conversationContentGuiController = FindAnyObjectByType<ConversationContentGuiController>();
            novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();

            // --- Automatisches Feedback (aus v2 übernommen) ---
            GeneratedFeedbackManager.Instance.Reset();
            GeneratedFeedbackManager.Instance.SetIdForNovel((int)novelToPlay.id);
            // (Hinweis: LoadFeedbacks() wird global an anderer Stelle erwartet. Falls nötig, dort aufrufen.)

            StartCoroutine(BootstrapAudioAndInit());
        }

        /// <summary>
        /// Executes the initial setup routine required before the game can fully initialize and proceed.
        /// This coroutine first loads all necessary audio clips by calling <c>LoadAudioClipsFromMapping()</c>.
        /// Once the audio is loaded, it sets the <c>_audioReady</c> flag to true and proceeds to call the main <c>Initialize()</c> method.
        /// </summary>
        private IEnumerator BootstrapAudioAndInit()
        {
            yield return StartCoroutine(LoadAudioClipsFromMapping());

            _audioReady = true;

            Initialize();
        }
        
        /// <summary>
        /// A debugging utility that logs detailed information about all currently loaded audio clips 
        /// to the console, prefixed with a specified context string.
        /// </summary>
        /// <param name="audioDumpContext">A descriptive string used to identify the source or context of the audio dump 
        /// (e.g., "Post Load", "Scene Exit").</param>
        private void DumpClips(string audioDumpContext)
        {
            if (_clips == null) return;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine($"[AudioDump] ({audioDumpContext}) clips.Length={_clips.Length}");

            for (int i = 0; i < _clips.Length; i++)
            {
                var c = _clips[i];
                if (c == null)
                {
                    sb.AppendLine($"  [{i}] null");
                    continue;
                }
                sb.AppendLine($"  [{i}] name='{c.name}', len={c.length:F2}s, samples={c.samples}, freq={c.frequency}, channels={c.channels}, loadState={c.loadState}");
            }
        }

        /// <summary>
        /// Liest SoundMapping.txt (StreamingAssets) und lädt ausschließlich die referenzierten .wav
        /// aus Assets/_AudioResources. Die Clips landen an ihren Indizes, Lücken bleiben null.
        /// Es wird auf das vollständige Laden gewartet; bei fehlenden/fehlerhaften Files gibt es Warnungen.
        /// </summary>
        private IEnumerator LoadAudioClipsFromMapping()
        {
            string mappingPath = Path.Combine(Application.streamingAssetsPath, "SoundMapping.txt");
            string mappingText;

            if (mappingPath.Contains("://") || mappingPath.Contains(":///"))
            {
                using UnityWebRequest req = UnityWebRequest.Get(mappingPath);
                yield return req.SendWebRequest();
                if (req.result != UnityWebRequest.Result.Success)
                {
                    LogManager.Warning($"[AudioLoad] SoundMapping konnte nicht geladen werden: {req.error} ({mappingPath})");
                    _clips = Array.Empty<AudioClip>();
                    yield break;
                }
                mappingText = req.downloadHandler.text;
            }
            else
            {
                if (!File.Exists(mappingPath))
                {
                    LogManager.Warning($"[AudioLoad] SoundMapping.txt nicht gefunden unter: {mappingPath}");
                    _clips = Array.Empty<AudioClip>();
                    yield break;
                }
                mappingText = File.ReadAllText(mappingPath);
            }

            var pairs = new List<(string name, int index)>();
            foreach (var raw in mappingText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var line = raw.Trim();
                if (line.Length == 0 || line.StartsWith("#")) continue;

                var parts = line.Split(':');
                if (parts.Length != 2)
                {
                    LogManager.Warning($"[AudioLoad] Zeile ignoriert (kein Name:Index): '{line}'");
                    continue;
                }

                var soundPair = parts[0].Trim();
                if (!int.TryParse(parts[1], out int idx))
                {
                    LogManager.Warning($"[AudioLoad] Ungültiger Index in Zeile: '{line}'");
                    continue;
                }

                pairs.Add((soundPair, idx));
            }

            if (pairs.Count == 0)
            {
                LogManager.Warning("[AudioLoad] Mapping ist leer – es wird kein Clip geladen.");
                _clips = Array.Empty<AudioClip>();
                yield break;
            }

            int size = pairs.Max(p => p.index) + 1;
            _clips = new AudioClip[size];

            _soundIndexByName.Clear();
            for (int i = 0; i < pairs.Count; i++)
            {
                _soundIndexByName[pairs[i].name] = pairs[i].index;
            }

            var missing = new List<string>();
            int loaded = 0;

            var loadOperations = new List<ResourceRequest>();
            var loadPairs = new List<(int index, string clipName)>();
    
            foreach (var (clipName, index) in pairs)
            {
                string resourcePath = "_AudioResources/" + clipName;
                ResourceRequest request = Resources.LoadAsync<AudioClip>(resourcePath);
                loadOperations.Add(request);
                loadPairs.Add((index, clipName));
            }

            while (loadOperations.Any(op => !op.isDone))
            {
                yield return null;
            }

            for (int i = 0; i < loadOperations.Count; i++)
            {
                var clip = loadOperations[i].asset as AudioClip;
                var (index, clipName) = loadPairs[i];
        
                if (clip == null)
                {
                    LogManager.Warning($"[AudioLoad] Resources.LoadAsync konnte '{clipName}' nicht finden.");
                    missing.Add($"{clipName}.wav");
                    continue;
                }

                clip.name = clipName;

                if (index >= 0 && index < _clips.Length)
                {
                    _clips[index] = clip;
                    loaded++;
                }
                else
                {
                    LogManager.Warning($"[AudioLoad] Index außerhalb des Bereichs ({index}) für '{clipName}' (clips.Length={_clips.Length})");
                }
            }

            if (missing.Count > 0)
            {
                LogManager.Warning($"[AudioLoad] Zusammenfassung: {loaded}/{pairs.Count} Clips geladen. Fehlend/fehlerhaft: {string.Join(", ", missing)}");
            }
        }

        /// <summary>
        /// Initializes the current Visual Novel within the scene, setting up essential parts,
        /// clearing global variables, and assigning visual and event-related properties.
        /// Ensures readiness by validating the existence of the novel and initializing relevant systems.
        /// </summary>
        private void Initialize()
        {
            if (novelToPlay == null) return;

            PromptManager.Instance().InitializePrompt(novelToPlay);

            novelToPlay.ClearGlobalVariables();
            novelToPlay.feedback = string.Empty;
            novelToPlay.playedPath = string.Empty;

            if (novelToPlay.novelEvents.Count <= 0) return;

            SetVisualElements();
            InitializeCharacterExpressions();
            InitializeNovelEvents();
            CheckForSavegame();
        }

        /// <summary>
        /// Initializes the mapping between character names and their corresponding prefabs
        /// using the data provided in the novel visual mappings. This ensures that
        /// each character is associated with the correct prefab during scene execution.
        /// </summary>
        private void InitializeCharacterToPrefabMap()
        {
            _characterToPrefabMap = new Dictionary<string, GameObject>(novelVisualMappings.Count, StringComparer.OrdinalIgnoreCase);

            foreach (var entry in novelVisualMappings)
            {
                if (!string.IsNullOrEmpty(entry.characterName) && entry.prefab != null)
                {
                    _characterToPrefabMap.TryAdd(entry.characterName, entry.prefab);
                }
            }
        }

        /// <summary>
        /// Configures visual elements for the novel scene, including resizing UI components, mapping characters to prefabs,
        /// and instantiating the required visuals for the first character.
        /// </summary>
        private void SetVisualElements()
        {
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            RectTransform conversationViewportTransform = conversationViewport.GetComponent<RectTransform>();
            conversationViewportTransform.sizeDelta = new Vector2(0, -canvasRect.rect.height * 0.5f);

            RectTransform viewPortTransform = viewPort.GetComponent<RectTransform>();

            string character = novelToPlay.characters[0];
                
            if (_characterToPrefabMap == null)
            {
                InitializeCharacterToPrefabMap();
            }
            
            if (_characterToPrefabMap == null)
            {
                LogManager.Error("Character-to-Prefab mapping dictionary failed to initialize. Cannot proceed with UI setup.");
                return;
            }
            
            if (!_characterToPrefabMap.TryGetValue(character, out var prefabToInstantiate))
            {
                LogManager.Warning($"No prefab found for character '{character}', using fallback.");
                prefabToInstantiate = novelVisualMappings.Count > 0 ? novelVisualMappings[0].prefab : null;
            }

            if (!prefabToInstantiate)
            {
                LogManager.Error("No valid prefab to instantiate.");
                return;
            }
                    
            GameObject novelImagesInstance = Instantiate(prefabToInstantiate, viewPortTransform);
            Transform controllerTransform = novelImagesInstance.transform.Find("Controller");
            _novelImagesController = controllerTransform.GetComponent<NovelImageController>();
        }

        /// <summary>
        /// Initializes the dictionary of character expressions for the current visual novel.
        /// This method extracts a list of unique character IDs from the novel's events, excluding specific IDs.
        /// Each extracted character ID is added as a type in the dictionary with an initial expression value of zero.
        /// </summary>
        private void InitializeCharacterExpressions()
        {
            CharacterExpressions.Clear();

            List<int> characters = novelToPlay.novelEvents
                .Select(e => e.character) 
                .Where(c => c != 0 && c != 1 && c != 4) 
                .Distinct()
                .ToList();

            foreach (var characterId in characters)
            {
                CharacterExpressions[characterId] = 0;
            }
        }

        /// <summary>
        /// Initializes the collection of novel events for the current visual novel.
        /// Populates an internal dictionary with event IDs and their corresponding event objects
        /// to enable efficient access during gameplay.
        /// </summary>
        private void InitializeNovelEvents()
        {
            foreach (VisualNovelEvent novelEvent in novelToPlay.novelEvents)
            {
                _novelEvents.Add(novelEvent.id, novelEvent);
            }
        }

        /// <summary>
        /// Checks if there is saved progress for the current visual novel and takes appropriate actions.
        /// If saved progress exists, it displays a hint to the user about continuing from the saved state.
        /// Otherwise, sets the next event to the first event in the novel and begins playing it.
        /// </summary>
        private void CheckForSavegame()
        {
            string novelId = novelToPlay.id.ToString();
            if (GameManager.Instance.HasSavedProgress(novelId))
            {
                ShowHintForSavegameMessageBox();
            }
            else
            {
                nextEventToPlay = novelToPlay.novelEvents[0];

                StartCoroutine(PlayNextEvent());
            }
        }

        /// <summary>
        /// Handles the confirmation input during gameplay. Executes various checks
        /// and behaviors based on the current state of the visual novel, such as
        /// skipping typing effects, processing touch interactions, or advancing
        /// to the next event in the story sequence. Cancels any ongoing text-to-speech
        /// playback and ensures proper state transitions for confirmation handling.
        /// </summary>
        public void OnConfirm()
        {
            TextToSpeechManager.Instance.CancelSpeak();

            Vector2 mousePosition = Input.mousePosition;

            if (_novelImagesController.HandleTouchEvent(mousePosition.x, mousePosition.y))
            {
                return;
            }

            if (isTyping)
            {
                if (currentTypeWriter != null)
                {
                    currentTypeWriter.SkipTypewriter();
                    currentTypeWriter = null;
                }

                _typingWasSkipped = true;
                SetTyping(false);
                return;
            }

            if (!isWaitingForConfirmation)
            {
                return;
            }

            SetWaitingForConfirmation(false);
            StartCoroutine(PlayNextEvent());
        }

        /// <summary>
        /// Reads the last saved or triggered event message, uses the text-to-speech manager
        /// to vocalize it, and then proceeds to play the next event in the scene.
        /// </summary>
        /// <returns>
        /// An IEnumerator for coroutine execution to manage the asynchronous operations
        /// of text-to-speech and event progression.
        /// </returns>
        public IEnumerator ReadLast()
        {
            StartCoroutine(TextToSpeechManager.Instance.Speak(TextToSpeechManager.Instance.GetLastMessage()));
            yield return StartCoroutine(PlayNextEvent());
        }

        /// <summary>
        /// Advances the visual novel scene by registering the current scene in the back stack
        /// and starting the process to play the next event in the sequence.
        /// </summary>
        public void Continue()
        {
            if (!_audioReady)
            {
                LogManager.Warning("[PlayNovelSceneController] Continue() blocked — audio not ready yet");
                return;
            }

            BackStackManager.Instance.Push(SceneNames.PlayNovelScene);
            StartCoroutine(PlayNextEvent());
        }

        /// <summary>
        /// Executes the next event in the visual novel sequence by managing the event flow,
        /// handling event-specific preparations, saving the event history, and processing
        /// various types of events such as background changes, character actions, messages,
        /// and more. If the event type signals the end of the sequence or requires transitioning
        /// to the next event, the method recursively triggers itself to continue execution.
        /// </summary>
        /// <returns>An IEnumerator used for coroutine execution, allowing asynchronous
        /// event handling within the visual novel sequence.</returns>
        private IEnumerator PlayNextEvent()
        {
            if (isPaused) yield break;
            
            if (TextToSpeechManager.Instance.IsTextToSpeechActivated()) yield return WaitForSpeechToFinish();

            HandleEventPreparation();
            eventHistory.Add(nextEventToPlay);

            VisualNovelEventType type = VisualNovelEventTypeHelper.ValueOf(nextEventToPlay.eventType);

            switch (type)
            {
                case VisualNovelEventType.CharacterJoinEvent:
                {
                    HandleCharacterJoinEvent(nextEventToPlay);
                    break;
                }
                case VisualNovelEventType.CharacterExitEvent:
                {
                    HandleCharacterExitEvent(nextEventToPlay);
                    break;
                }
                case VisualNovelEventType.ShowMessageEvent:
                {
                    HandleShowMessageEvent(nextEventToPlay);
                    ScrollToBottom();
                    break;
                }
                case VisualNovelEventType.AddChoiceEvent:
                {
                    HandleAddChoiceEvent(nextEventToPlay);
                    break;
                }
                case VisualNovelEventType.ShowChoicesEvent:
                {
                    confirmArea.gameObject.SetActive(false);
                    confirmArea2.gameObject.SetActive(false);
                    HandleShowChoicesEvent(nextEventToPlay);
                    ScrollToBottom();
                    break;
                }
                case VisualNovelEventType.EndNovelEvent:
                {
                    HandleEndNovelEvent();
                    break;
                }
                case VisualNovelEventType.PlaySoundEvent:
                {
                    HandlePlaySoundEvent(nextEventToPlay);
                    break;
                }
                case VisualNovelEventType.MarkBiasEvent:
                {
                    HandleMarkBiasEvent(nextEventToPlay);
                    break;
                }
                case VisualNovelEventType.None:
                default:
                {
                    string nextEventID = nextEventToPlay.nextId;
                    nextEventToPlay = _novelEvents[nextEventID];
                    yield return StartCoroutine(PlayNextEvent());
                    break;
                }
            }
        }

        /// <summary>
        /// Waits for the Text-to-Speech manager to finish speaking before proceeding.
        /// This ensures that dialogue or text playback has completed before continuing execution.
        /// </summary>
        /// <returns>An IEnumerator to be used in coroutine execution, allowing for asynchronous waiting.</returns>
        private IEnumerator WaitForSpeechToFinish()
        {
            if (_speakingCoroutine == null) yield break;
            
            while (TextToSpeechManager.Instance.IsSpeaking())
            {
                yield return null;
            }
        }

        /// <summary>
        /// Prepares for the execution of the next visual novel event by performing necessary cleanup
        /// and state adjustments such as resetting interaction flags, skipping typewriter animations,
        /// and evaluating event-specific conditions.
        /// </summary>
        private void HandleEventPreparation()
        {
            if (selectOptionContinueConversation != null)
            {
                selectOptionContinueConversation = null;
            }

            if (currentTypeWriter != null)
            {
                currentTypeWriter.SkipTypewriter();
                currentTypeWriter = null;
            }

            if (nextEventToPlay.id.StartsWith("OptionsLabel") && !GameManager.Instance.calledFromReload)
            {
                string numericPart = nextEventToPlay.id.Substring("OptionsLabel".Length);

                if (int.TryParse(numericPart, out _))
                {
                    _optionsId[0] = _optionsId[1];
                    _optionsId[1] = nextEventToPlay.id;
                }
            }
        }

        /// <summary>
        /// Handles the playback of sound events during the visual novel's progression based on the specified event parameters.
        /// Adjusts the flow of the novel by managing sound playback and timing for later events.
        /// </summary>
        /// <param name="novelEvent">The visual novel event containing information about the sound to play and related behaviors such as waiting for user confirmation.</param>
        private void HandlePlaySoundEvent(VisualNovelEvent novelEvent)
        {
            if (!_clipsDumpedOnce)
            {
                DumpClips("HandlePlaySoundEvent-enter");
                _clipsDumpedOnce = true;
            }

            SetNextEvent(novelEvent);

            string key = novelEvent.audioClipToPlay;

            bool isNone = string.Equals(key, "NONE", StringComparison.OrdinalIgnoreCase);
            bool isLeaveScene = string.Equals(key, "LEAVESCENE", StringComparison.OrdinalIgnoreCase);

            if (!isNone && !isLeaveScene)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    LogManager.Warning("[PlaySound] audioClipToPlay ist leer.");
                }
                else if (_soundIndexByName.TryGetValue(key.Trim(), out int idx))
                {

                    if (idx >= 0 && idx < (_clips?.Length ?? 0))
                    {
                        if (_clips != null)
                        {
                            var clip = _clips[idx];
                            if (clip != null)
                            {
                                if (clip.loadState == AudioDataLoadState.Unloaded)
                                {
                                    clip.LoadAudioData();
                                }
                            
                                GlobalVolumeManager.Instance.PlaySound(clip);
                            }
                            else
                            {
                                LogManager.Warning($"[PlaySound] Clip an idx {idx} ist NULL (Datei fehlte/Fehler beim Laden?)");
                            }
                        }
                    }
                    else
                    {
                        LogManager.Warning($"[PlaySound] idx {idx} außerhalb von clips (len={(_clips?.Length ?? 0)})");
                    }
                }
                else
                {
                    LogManager.Warning($"[PlaySound] Kein Mapping für '{key}' (nicht in SoundMapping.txt?)");
                }
            }

            if (novelEvent.waitForUserConfirmation)
            {
                SetWaitingForConfirmation(true);
                return;
            }
            if (isLeaveScene)
            {
                StartCoroutine(StartNextEventInOneSeconds(2.5f));
                return;
            }

            StartCoroutine(StartNextEventInOneSeconds(1));
        }

        /// <summary>
        /// Handles the marking of bias as relevant for a given visual novel event, updates the prompt with bias information,
        /// and proceeds to execute the next event.
        /// </summary>
        /// <param name="novelEvent">The current visual novel event containing bias-related data.</param>
        private void HandleMarkBiasEvent(VisualNovelEvent novelEvent)
        {
            SetNextEvent(novelEvent);
            string biasInformation = novelEvent.relevantBias;
            PromptManager.Instance().AddFormattedLineToPrompt("Hinweis", biasInformation);
            StartCoroutine(PlayNextEvent());
        }

        /// <summary>
        /// Handles the event where a character joins the scene during the visual novel flow.
        /// Sets up the event and updates the character display using the image controller.
        /// Also manages timing for the next event transition.
        /// </summary>
        /// <param name="novelEvent">The visual novel event containing information about the character joining.</param>
        private void HandleCharacterJoinEvent(VisualNovelEvent novelEvent)
        {
            SetNextEvent(novelEvent);
            _novelImagesController.SetCharacter();

            StartCoroutine(StartNextEventInOneSeconds(1));
        }

        /// <summary>
        /// Handles the exit of a character during a visual novel event. This involves setting the next event,
        /// destroying the character's current image, and transitioning to the next event with a delay.
        /// </summary>
        /// <param name="novelEvent">The visual novel event that triggers the character's exit.</param>
        private void HandleCharacterExitEvent(VisualNovelEvent novelEvent)
        {
            SetNextEvent(novelEvent);

            _novelImagesController.DestroyCharacter();

            StartCoroutine(StartNextEventInOneSeconds(1));
        }

        /// <summary>
        /// Handles the process of displaying a visual novel message event.
        /// This includes setting up character expressions, updating the conversation content,
        /// and preparing for any required user confirmation or proceeding to the next event.
        /// </summary>
        /// <param name="novelEvent">The event containing details to display, such as message text, character ID, and expression type.</param>
        private void HandleShowMessageEvent(VisualNovelEvent novelEvent)
        {
            CreateSpeakingCoroutine(novelEvent.text);

            SetNextEvent(novelEvent);

            _novelCharacter = novelEvent.character;

            if (!CharacterExpressions.ContainsKey(_novelCharacter) && _novelCharacter != 0 && _novelCharacter != 1 && _novelCharacter != 4)
            {
                LogManager.Warning($"Character ID {_novelCharacter} is not registered.");
                return;
            }

            if (CharacterExpressions.ContainsKey(_novelCharacter) && _novelCharacter != 0 && _novelCharacter != 1 && _novelCharacter != 4)
            {
                CharacterExpressions[_novelCharacter] = novelEvent.expressionType;
                _novelImagesController.SetFaceExpression(_novelCharacter, CharacterExpressions[_novelCharacter]);
            }

            if (novelEvent.show)
            {
                conversationContent.AddContent(novelEvent, this);

                string character = MappingManager.characterMapping.FirstOrDefault(pair => pair.Value == novelEvent.character).Key;
                AddEntryToPlayThroughHistory(character, novelEvent.text);
            }

            if (novelEvent.waitForUserConfirmation)
            {
                SetWaitingForConfirmation(true);
                return;
            }

            StartCoroutine(StartNextEventInOneSeconds(1));
        }

        /// <summary>
        /// Handles the addition of a choice event within the visual novel.
        /// It processes the event, updates the conversation content, checks for user confirmation,
        /// and integrates it into the text-to-speech system.
        /// </summary>
        /// <param name="novelEvent">The visual novel event containing the choice
        /// details to be handled.</param>
        private void HandleAddChoiceEvent(VisualNovelEvent novelEvent)
        {
            SetNextEvent(novelEvent);

            conversationContent.AddContent(novelEvent, this);

            if (novelEvent.waitForUserConfirmation)
            {
                SetWaitingForConfirmation(true);
                return;
            }

            TextToSpeechManager.Instance.AddChoiceToChoiceCollectionForTextToSpeech(novelEvent.text);
            StartCoroutine(PlayNextEvent());
        }

        /// <summary>
        /// Handles the event of displaying choices during a visual novel sequence.
        /// This involves reading choices text via text-to-speech, enabling animations,
        /// logging the event to playthrough history, and updating the conversation content.
        /// </summary>
        /// <param name="novelEvent">The visual novel event containing the character and dialogue options to display.</param>
        private void HandleShowChoicesEvent(VisualNovelEvent novelEvent)
        {
            StartCoroutine(TextToSpeechManager.Instance.ReadChoice());

            AnimationFlagSingleton.Instance().SetFlag(true);

            string character = MappingManager.characterMapping.FirstOrDefault(pair => pair.Value == novelEvent.character).Key;
            AddEntryToPlayThroughHistory(character, novelEvent.text);

            conversationContent.AddContent(novelEvent, this);
        }

        /// <summary>
        /// Handles the completion of a visual novel event by updating the player's novel history, triggering animations, and deciding
        /// the next scene to load based on the completed novel's title.
        /// </summary>
        public void HandleEndNovelEvent()
        {
            string currentNovel = MappingManager.allNovels.Find(novel => novel.id == novelToPlay.id).title;

            if (GeneratedFeedbackManager.Instance.HasEventsForId((int)novelToPlay.id))
            {
                novelToPlay.feedback = GeneratedFeedbackManager.Instance.GetFeedback();
                if (!string.IsNullOrEmpty(novelToPlay.feedback))
                {
                    novelToPlay.feedback = novelToPlay.feedback.Replace("**", "");
                    SaveDialogToHistory(novelToPlay.feedback);
                }
            }

            PlayerDataManager.Instance().SetNovelHistory(playThroughHistory);
            PlayThroughCounterAnimationManager.Instance().SetAnimation(true, currentNovel);

            if (novelToPlay.id == 13)
            {
                SceneLoader.LoadFoundersBubbleScene();
                return;
            }
            
            PlayRecordManager.Instance().IncreasePlayCounterForNovel(currentNovel);
            SceneLoader.LoadFeedbackScene();
        }

        /// <summary>
        /// Waits for a specified amount of time before initiating the next event in the visual novel sequence.
        /// Updates character expressions if applicable during the wait.
        /// </summary>
        /// <param name="second">The duration, in seconds, to wait before starting the next event.</param>
        /// <returns>An IEnumerator used to control the timing of the next event.</returns>
        private IEnumerator StartNextEventInOneSeconds(float second)
        {
            if (_novelCharacter != 0 && CharacterExpressions.ContainsKey(_novelCharacter))
            {
                if (CharacterExpressions[_novelCharacter] <= 12)
                {
                    CharacterExpressions[_novelCharacter] += 13;
                    _novelImagesController.SetFaceExpression(_novelCharacter, CharacterExpressions[_novelCharacter]);
                }
            }

            yield return new WaitForSeconds(second);

            StartCoroutine(PlayNextEvent());
        }

        /// <summary>
        /// Displays the player's response to the conversation and handles updating the playthrough history and UI elements.
        /// </summary>
        /// <param name="message">The response message from the player that will be displayed.</param>
        /// <param name="show">Determines whether to display the player's response in the UI.</param>
        public void ShowAnswer(string message, bool show)
        {
            if (!show) return;

            AddEntryToPlayThroughHistory("Player", message);
            conversationContent.ShowPlayerAnswer(message);
            ScrollToBottom();
        }

        /// <summary>
        /// Sets the next visual novel event to play based on the provided event ID.
        /// </summary>
        /// <param name="id">The unique identifier of the visual novel event to be set as the next event.</param>
        public void SetNextEvent(string id)
        {
            nextEventToPlay = _novelEvents[id];
        }

        /// <summary>
        /// Sets the next visual novel event to play based on the provided event's next ID.
        /// </summary>
        /// <param name="novelEvent">The current visual novel event that specifies the ID of the next event to play.</param>
        private void SetNextEvent(VisualNovelEvent novelEvent)
        {
            string nextEventID = novelEvent.nextId;
            nextEventToPlay = _novelEvents[nextEventID];
        }

        /// <summary>
        /// Scrolls the chat view to display the latest content at the bottom of the scrollable area
        /// and updates all text components to reflect any font size changes.
        /// </summary>
        public void ScrollToBottom()
        {
            StartCoroutine(chatScroll.ScrollToBottom());
            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        /// <summary>
        /// Sets the state of whether the system is waiting for user confirmation.
        /// </summary>
        /// <param name="value">A boolean indicating whether the system should be waiting for confirmation (true) or not (false).</param>
        public void SetWaitingForConfirmation(bool value)
        {
            isWaitingForConfirmation = value;
        }

        /// <summary>
        /// Sets the typing state for the scene controller. Updates internal state and
        /// handles confirmation logic depending on the typing state and current conditions.
        /// </summary>
        /// <param name="typing">A boolean indicating whether typing is currently in progress.</param>
        public void SetTyping(bool typing)
        {
            if (typing || !isWaitingForConfirmation) return;
            
            SetWaitingForConfirmation(false);

            float delay = WaitingTime;
            if (_typingWasSkipped)
            {
                delay = 0f; 
                _typingWasSkipped = false; 
            }

            StartCoroutine(StartNextEventInOneSeconds(delay));
        }

        /// <summary>
        /// Handles the completion of the current animation, setting the controller to a confirmation waiting state
        /// and safely destroying the current animation object if it exists.
        /// </summary>
        public void AnimationFinished()
        {
            SetWaitingForConfirmation(true);

            if (currentAnimation.IsNullOrDestroyed()) return;

            Destroy(currentAnimation);
        }

        /// <summary>
        /// Fügt zur History hinzu (bevorzugt v1) und registriert das Ereignis beim GeneratedFeedbackManager (aus v2).
        /// </summary>
        private void AddEntryToPlayThroughHistory(string characterRole, string text)
        {
            if (characterRole == "None") characterRole = "";
            playThroughHistory.Add(characterRole + ": " + text);

            GeneratedFeedbackManager.Instance.SetEvent(text);
        }

        /// <summary>
        /// Replaces placeholders in the specified text with corresponding values from the replacement dictionary.
        /// Placeholders are defined within "&gt;&lt;" in the text and replaced with mapped values from the dictionary.
        /// </summary>
        /// <param name="text">The input text containing placeholders to be replaced.</param>
        /// <param name="replacements">A dictionary containing the keys and their associated replacement values.</param>
        /// <returns>The text with placeholders replaced by the corresponding values from the replacement dictionary. If no match is found for a placeholder, it remains unchanged.</returns>
        public static string ReplacePlaceholders(string text, Dictionary<string, string> replacements)
        {
            return Regex.Replace(text, @"\>(.*?)\<", match =>
            {
                string key = match.Groups[1].Value;
                return replacements.TryGetValue(key, out string replacement) ? replacement : match.Value;
            });
        }

        /// <summary>
        /// Adds a specified path value to the current visual novel's progression path.
        /// This is used to track the player's choices and the storyline path they are following.
        /// </summary>
        /// <param name="pathValue">The integer value representing the path to be added to the visual novel's progression.</param>
        public void AddPathToNovel(int pathValue)
        {
            novelToPlay.AddToPath(pathValue);
        }

        /// <summary>
        /// Restores the game state to the last choice's point, including clearing the event history
        /// and playthrough history beyond this point, and resetting the next event to play.
        /// It ensures proper adjustments to the conversation content and removes any excess history
        /// created after the restored choice.
        /// </summary>
        public void RestoreChoice()
        {
            if (string.IsNullOrEmpty(_optionsId[0])) return;

            string eventIdToRestore = _optionsId[0];

            int indexToRestore = eventHistory.FindIndex(e => e.id == eventIdToRestore);
            if (indexToRestore == -1) return;

            if (_optionsCount != 0)
            {
                indexToRestore -= _optionsCount;
            }

            if (indexToRestore >= 0)
            {
                eventHistory.RemoveRange(indexToRestore, eventHistory.Count - indexToRestore);
            }

            int colonCount = 0;
            int indexToRemoveFrom = -1;

            for (int i = playThroughHistory.Count - 1; i >= 0; i--)
            {
                if (playThroughHistory[i].Trim() != ":") continue;

                colonCount++;

                if (colonCount != 2) continue;

                indexToRemoveFrom = i;
                break;
            }

            if (indexToRemoveFrom != -1)
            {
                playThroughHistory.RemoveRange(indexToRemoveFrom, playThroughHistory.Count - indexToRemoveFrom);

                _conversationContentGuiController.ClearUIAfter(indexToRemoveFrom, _optionsCount);
            }

            _optionsCount = 0;

            if (indexToRestore <= 0) return;

            SetNextEvent(eventIdToRestore);
            StartCoroutine(PlayNextEvent());
        }

        /// <summary>
        /// Starts a coroutine to handle text-to-speech functionality for the given text.
        /// Uses the TextToSpeechManager to initiate the speaking process.
        /// </summary>
        /// <param name="text">The text to be spoken by the coroutine.</param>
        private void CreateSpeakingCoroutine(string text)
        {
            _speakingCoroutine = TextToSpeechManager.Instance.Speak(text);
            StartCoroutine(_speakingCoroutine);
        }

        /// <summary>
        /// Displays or updates the hint message box associated with savegame functionality.
        /// Ensures that only one instance of the message box is active at a time.
        /// Initializes and activates the message box using the designated canvas.
        /// </summary>
        private void ShowHintForSavegameMessageBox()
        {
            if (hintForSavegameMessageBox == null || canvas.IsNullOrDestroyed()) return;

            if (_hintForSavegameMessageBoxObject != null && !_hintForSavegameMessageBoxObject.IsNullOrDestroyed())
            {
                _hintForSavegameMessageBoxObject.GetComponent<HintForSavegameMessageBox>().CloseMessageBox();
            }

            _hintForSavegameMessageBoxObject = Instantiate(hintForSavegameMessageBox, canvas.transform);
            _hintForSavegameMessageBoxObject.GetComponent<HintForSavegameMessageBox>().Activate();
        }

        /// <summary>
        /// Resumes the visual novel gameplay from a previously saved state. This method loads
        /// the saved data, restores the current event, updates the playthrough history,
        /// reconfigures the GUI, and initializes any required components to continue the
        /// narrative seamlessly. The gameplay resumes from where the player last left off.
        /// </summary>
        public void ResumeFromSavedState()
        {
            string novelId = NovelToPlay.id.ToString();
            NovelSaveData savedData = SaveLoadManager.Load(novelId);

            if (savedData == null)
            {
                LogManager.Warning("No saved data found for the novel.");
                return;
            }

            nextEventToPlay = novelToPlay.novelEvents.FirstOrDefault(e => e.id == savedData.currentEventId)
                              ?? novelToPlay.novelEvents[0];

            playThroughHistory = new List<string>(savedData.playThroughHistory);

            _optionsId[0] = savedData.optionsId[1];
            _optionsCount = savedData.optionCount;
            eventHistory = savedData.eventHistory;

            conversationContent.ReconstructGuiContent(savedData);

            long searchId = novelToPlay.id;

            if (_novelImagesController != null && savedData.characterPrefabData != null)
            {
                if (savedData.characterPrefabData.TryGetValue(searchId, out CharacterData characterData))
                {
                    ApplyCharacterData(_novelImagesController, characterData);
                }
            }

            RestoreCharacterExpressions(savedData);

            ActivateMessageBoxes();

            StartCoroutine(PlayNextEvent());
        }

        /// <summary>
        /// Applies character data, such as appearance attributes, to the corresponding character controllers
        /// managed by the specified NovelImageController.
        /// </summary>
        /// <param name="controller">The NovelImageController that manages the list of character controllers.</param>
        /// <param name="characterData">The CharacterData object containing attribute indices for characters such as skin, hand, clothing, hair, and glasses.</param>
        private void ApplyCharacterData(NovelImageController controller, CharacterData characterData)
        {
            if (controller == null)
            {
                LogManager.Error("ApplyCharacterData failed: controller is null");
                return;
            }

            if (controller.characterControllers == null || controller.characterControllers.Count == 0)
            {
                LogManager.Error("ApplyCharacterData failed: characterControllers list is null or empty");
                return;
            }

            if (controller.characterControllers.Count >= 1 && controller.characterControllers[0] != null)
            {
                controller.characterControllers[0].SetSkinSprite(characterData.skinIndex);
                controller.characterControllers[0].SetHandSprite(characterData.handIndex);
                controller.characterControllers[0].SetClotheSprite(characterData.clotheIndex);
                controller.characterControllers[0].SetHairSprite(characterData.hairIndex);
                controller.characterControllers[0].SetGlassesSprite(characterData.glassIndex);
            }
            else
            {
                LogManager.Warning("First character controller is missing or null.");
            }

            if (controller.characterControllers.Count >= 2 && controller.characterControllers[1] != null)
            {
                controller.characterControllers[1].SetSkinSprite(characterData.skinIndex2);
                controller.characterControllers[1].SetGlassesSprite(characterData.glassIndex2);
                controller.characterControllers[1].SetClotheSprite(characterData.clotheIndex2);
                controller.characterControllers[1].SetHairSprite(characterData.hairIndex2);
                controller.characterControllers[1].SetGlassesSprite(characterData.glassIndex2);
            }
            else
            {
                LogManager.Warning("Second character controller is missing or null.");
            }
        }

        /// <summary>
        /// Restores the character expressions from saved data by updating the visual novel image controller
        /// with the previously saved expressions.
        /// </summary>
        /// <param name="savedData">An instance of <see cref="NovelSaveData"/> containing the saved character expressions data.</param>
        private void RestoreCharacterExpressions(NovelSaveData savedData)
        {
            foreach (var kvp in savedData.characterExpressions)
            {
                _novelImagesController.SetFaceExpression(kvp.Key, kvp.Value);
            }
        }

        /// <summary>
        /// Restarts the visual novel by deleting its associated save data, resetting the next event to the first
        /// event in the novel, and starting the next event after a delay.
        /// </summary>
        public void RestartNovel()
        {
            SaveLoadManager.DeleteNovelSaveData(novelToPlay.id.ToString());

            nextEventToPlay = novelToPlay.novelEvents[0];

            StartCoroutine(StartNextEventInOneSeconds(2));
        }

        /// <summary>
        /// Retrieves the current visual novel event queued to play.
        /// </summary>
        /// <returns>The next event to play in the visual novel.</returns>
        public VisualNovelEvent GetCurrentEvent()
        {
            return nextEventToPlay;
        }

        /// <summary>
        /// Activates all message boxes within the conversation content by ensuring
        /// that inactive message box GameObjects are set to active. This is used
        /// to prepare message boxes for displaying conversation or narrative content.
        /// </summary>
        private void ActivateMessageBoxes()
        {
            foreach (var messageBox in conversationContent.GuiContent)
            {
                if (messageBox != null && !messageBox.activeSelf)
                {
                    messageBox.SetActive(true);
                }
            }
        }

        /// <summary>
        /// Schreibt einen finalen Feedback-Dialogeintrag in die History (einfache Variante).
        /// </summary>
        private void SaveDialogToHistory(string text)
        {
            playThroughHistory.Add(text);
        }
    }
}
