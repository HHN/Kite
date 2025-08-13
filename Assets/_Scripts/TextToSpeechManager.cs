using System.Collections;
using UnityEngine;
using System.Runtime.InteropServices;

namespace Assets._Scripts
{
    /// <summary>
    /// Manages Text-to-Speech (TTS) functionality across different platforms (iOS, WebGL, Android)
    /// and handles sound effect activation settings. It implements a Singleton pattern to ensure
    /// a single, persistent instance throughout the application.
    /// </summary>
    public class TextToSpeechManager : MonoBehaviour
    {
        private static TextToSpeechManager _instance;

        private bool _ttsIsActive = true;
        private bool _soundEffectIsActive = true;

        private string _ttsString = "Folgende Antwortmöglichkeiten stehen dir zur Auswahl: ";
        private int _optionCounter;
        private bool _isSpeaking;
        private string _lastMessage = "";
        private bool _wasPaused;

        // ------------------------------------------------
        // iOS native methods
        // ------------------------------------------------
#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void _InitializeTTS();

        [DllImport("__Internal")]
        private static extern void _Speak(string message);

        [DllImport("__Internal")]
        private static extern void _StopSpeaking();

        [DllImport("__Internal")]
        private static extern bool _IsSpeaking();
#else
        // Fallback implementations for the Unity Editor or other platforms.
        // These do nothing to prevent errors when not running on a real iOS device.
        private static void _InitializeTTS() { }
        private static void _Speak(string message) { }
        private static void _StopSpeaking() { }
        private static bool _IsSpeaking() { return false; }
#endif

        // ------------------------------------------------
        // WebGL JavaScript Plugin Methods
        // ------------------------------------------------
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void TTS_Initialize();

        [DllImport("__Internal")]
        private static extern void TTS_Speak(string message);

        [DllImport("__Internal")]
        private static extern void TTS_Stop();

        [DllImport("__Internal")]
        private static extern int TTS_IsSpeaking(); // Rückgabe int statt bool
#else
        // Fallback implementations for the Unity Editor or other platforms.
        private static void TTS_Initialize() { }
        private static void TTS_Speak(string message) { }
        private static void TTS_Stop() { }
        private static int TTS_IsSpeaking() { return 0; } // Returns 0 (false) as a fallback.
#endif

        // ------------------------------------------------
        // Singleton Implementation
        // ------------------------------------------------
        /// <summary>
        /// Provides the static instance of the TextToSpeechManager.
        /// If no instance exists in the scene, it finds or creates one
        /// and ensures it persists across scene loads.
        /// </summary>
        public static TextToSpeechManager Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType<TextToSpeechManager>();
                    if (!_instance)
                    {
                        GameObject obj = new GameObject("TextToSpeechManager");
                        _instance = obj.AddComponent<TextToSpeechManager>();
                        DontDestroyOnLoad(obj);
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Implements the core logic for the Singleton pattern, ensuring only one instance exists.
        /// If an instance already exists, it destroys duplicate GameObjects.
        /// </summary>
        private void Awake()
        {
            if (!_instance)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        // ------------------------------------------------
        // Initialization
        // ------------------------------------------------
        /// <summary>
        /// Called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// Loads TTS and sound effect preferences from PlayerPrefs and performs platform-specific TTS initialization.
        /// </summary>
        private void Start()
        {
            _ttsIsActive = PlayerPrefs.GetInt("TTS", 0) != 0;
            _soundEffectIsActive = PlayerPrefs.GetInt("SoundEffect", 0) != 0;

#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                // Android-spezifische Initialisierung
                StartCoroutine(InitializeTTSAndroid());
            }
#elif UNITY_IOS && !UNITY_EDITOR
            // iOS-spezifische Initialisierung
            _InitializeTTS();
#elif UNITY_WEBGL && !UNITY_EDITOR
            // WebGL: JavaScript-Plugin initialisieren
            TTS_Initialize();
#endif
        }

        // ------------------------------------------------
        // Getter / Setter for TTS and Sound Effect States
        // ------------------------------------------------
        /// <summary>
        /// Checks if Text-to-Speech is currently activated.
        /// </summary>
        /// <returns>True if TTS is active, false otherwise.</returns>
        public bool IsTextToSpeechActivated()
        {
            return _ttsIsActive;
        }

        /// <summary>
        /// Activates Text-to-Speech and saves the preference to PlayerPrefs.
        /// </summary>
        public void ActivateTTS()
        {
            _ttsIsActive = true;
            PlayerPrefs.SetInt("TTS", 1);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Deactivates Text-to-Speech and saves the preference to PlayerPrefs.
        /// </summary>
        public void DeactivateTTS()
        {
            _ttsIsActive = false;
            PlayerPrefs.SetInt("TTS", 0);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Checks if sound effects are currently activated.
        /// </summary>
        /// <returns>True if sound effects are active, false otherwise.</returns>
        public bool IsSoundEffectActivated()
        {
            return _soundEffectIsActive;
        }

        /// <summary>
        /// Sets the flag indicating whether the speech was previously paused.
        /// </summary>
        /// <param name="boolValue">True if speech was paused, false otherwise.</param>
        public void SetWasPaused(bool boolValue)
        {
            _wasPaused = boolValue;
        }

        /// <summary>
        /// Checks if the speech was previously paused.
        /// </summary>
        /// <returns>True if speech was paused, false otherwise.</returns>
        public bool WasPaused()
        {
            return _wasPaused;
        }

        /// <summary>
        /// Sets the last message that was spoken or attempted to be spoken.
        /// </summary>
        /// <param name="message">The message string.</param>
        public void SetLastMessage(string message)
        {
            _lastMessage = message;
        }

        /// <summary>
        /// Retrieves the last message that was spoken.
        /// </summary>
        /// <returns>The last message string.</returns>
        public string GetLastMessage()
        {
            return _lastMessage;
        }

        // ------------------------------------------------
        // Text Generation for Multiple Choice
        // ------------------------------------------------
        /// <summary>
        /// Adds a choice option to a collection that will be spoken as part of a multiple-choice prompt.
        /// It prepends an ordinal (e.g., "Erste Option:", "Zweite Option:") to each choice.
        /// </summary>
        /// <param name="choice">The text of the choice option.</param>
        public void AddChoiceToChoiceCollectionForTextToSpeech(string choice)
        {
            _optionCounter++;
            switch (_optionCounter)
            {
                case 1:
                    _ttsString += "Erste Option: " + choice + ". ";
                    break;
                case 2:
                    _ttsString += "Zweite Option: " + choice + ". ";
                    break;
                case 3:
                    _ttsString += "Dritte Option: " + choice + ". ";
                    break;
                case 4:
                    _ttsString += "Vierte Option: " + choice + ". ";
                    break;
                case 5:
                    _ttsString += "Fünfte Option: " + choice + ". ";
                    break;
            }
        }

        /// <summary>
        /// Initiates the Text-to-Speech playback for the collected multiple-choice prompt.
        /// Resets the internal string and counter after speaking.
        /// </summary>
        /// <returns>An IEnumerator for use in a Coroutine.</returns>
        public IEnumerator ReadChoice()
        {
            yield return Speak(_ttsString);
            _ttsString = "Folgende Antwortmöglichkeiten stehen dir zur Auswahl: ";
            _optionCounter = 0;
        }

        // ------------------------------------------------
        // Speaking Functionality
        // ------------------------------------------------
        /// <summary>
        /// Initiates Text-to-Speech playback for a given message.
        /// This method is platform-dependent, using native calls for iOS/WebGL and Android Java interoperability.
        /// It waits for the speech to complete before yielding.
        /// </summary>
        /// <param name="message">The text message to be spoken.</param>
        /// <returns>An IEnumerator for use in a Coroutine.</returns>
        public IEnumerator Speak(string message)
        {
            // In the Unity Editor, TTS is often not supported directly via native plugins,
            // so we typically skip actual speech and just log.
#if UNITY_EDITOR
            yield break;
#endif

            if (!_ttsIsActive) yield break;

            _lastMessage = message;
            _isSpeaking = true;

#if UNITY_ANDROID
            // --- Android Implementation ---
            // Wait until the Android TTS engine is initialized.
            while (!_isInitialized)
            {
                yield return null;
            }

            if (_ttsObject != null)
            {
                string utteranceId = "UniqueID_" + System.Guid.NewGuid().ToString();
                int apiLevel = GetAndroidSDKVersion();

                if (apiLevel >= 21) // For Android API Level 21 (Lollipop) and higher.
                {
                    AndroidJavaObject bundleParams = new AndroidJavaObject("android.os.Bundle");
                    bundleParams.Call("putString", "utteranceId", utteranceId);
                    _ttsObject.Call<int>("speak", message, 0, bundleParams, utteranceId);

                     // Wait until the TTS engine reports that it has finished speaking.
                    while (_ttsObject.Call<bool>("isSpeaking"))
                    {
                        yield return null;
                    }
                }
                else // For Android API Level < 21 (older versions).
                {
                    AndroidJavaObject hashMapParams = new AndroidJavaObject("java.util.HashMap");
                    hashMapParams.Call<AndroidJavaObject>("put", "utteranceId", utteranceId);
                    _ttsObject.Call<int>("speak", message, 0, hashMapParams);

                    // For older Android versions, isSpeaking() might not be reliable or available.
                    // Use a rough estimate of duration to wait.
                    float estimatedDuration = message.Length * 0.05f;
                    yield return new WaitForSeconds(estimatedDuration);
                }
            }
            else
            {
                Debug.LogError("TextToSpeech is not initialized.");
            }

#elif UNITY_IOS && !UNITY_EDITOR // iOS Implementation (native plugin).
            _Speak(message);
            while (_IsSpeaking())
            {
                yield return null;
            }

#elif UNITY_WEBGL && !UNITY_EDITOR // WebGL Implementation (JavaScript plugin).
            TTS_Speak(message);
            // Warte, bis TTS_IsSpeaking() 0 zurückgibt
            while (TTS_IsSpeaking() == 1)
            {
                yield return null;
            }
#endif

            _isSpeaking = false;
        }
        
        /// <summary>
        /// Cancels any currently ongoing Text-to-Speech playback.
        /// The implementation is platform-dependent.
        /// </summary>
        public void CancelSpeak()
        {
#if UNITY_ANDROID
            if (_ttsObject != null)
            {
                // For Android, a common way to stop immediately is to speak an empty string.
                // This might vary depending on the Android TTS engine's behavior.

                StartEmptySpeech();
                _isSpeaking = false;
            }
#elif UNITY_IOS && !UNITY_EDITOR
            _StopSpeaking();
            _isSpeaking = false;
#elif UNITY_WEBGL && !UNITY_EDITOR
            TTS_Stop();
            _isSpeaking = false;
#endif
        }

        /// <summary>
        /// Checks if Text-to-Speech is currently speaking.
        /// The implementation is platform-dependent.
        /// </summary>
        /// <returns>True if TTS is speaking, false otherwise.</returns>
        public bool IsSpeaking()
        {
#if UNITY_ANDROID
            return _isSpeaking;
#elif UNITY_IOS && !UNITY_EDITOR
            return _IsSpeaking();
#elif UNITY_WEBGL && !UNITY_EDITOR
            return TTS_IsSpeaking() == 1; // Call JavaScript function and interpret an integer result.
#else
            return false;
#endif
        }

        /// <summary>
        /// Repeats the last spoken message using Text-to-Speech.
        /// </summary>
        /// <returns>An IEnumerator for use in a Coroutine.</returns>
        public IEnumerator RepeatLastMessage()
        {
            yield return StartCoroutine(Speak(_lastMessage));
        }

        // ------------------------------------------------
        // Android-specific Fields and Methods
        // ------------------------------------------------
#if UNITY_ANDROID
        private AndroidJavaObject _ttsObject;
        private AndroidJavaObject _locale;
        private bool _isInitialized;

        private IEnumerator InitializeTTSAndroid()
        {
           // Give a short delay to ensure the Unity Android Activity is fully ready.
            yield return new WaitForSeconds(0.1f);

            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

                // Create the TextToSpeech instance, passing the Unity Activity and a custom OnInitListener.
                _ttsObject = new AndroidJavaObject(
                    "android.speech.tts.TextToSpeech",
                    unityActivity,
                    new OnInitListener(this)
                );
            }
        }

         /// <summary>
        /// An inner class that acts as an AndroidJavaProxy for the TextToSpeech.OnInitListener interface.
        /// It receives the initialization status from the Android TTS engine.
        /// </summary>
        private class OnInitListener : AndroidJavaProxy
        {
            private readonly TextToSpeechManager _ttsManager;

            public OnInitListener(TextToSpeechManager manager)
                : base("android.speech.tts.TextToSpeech$OnInitListener")
            {
                _ttsManager = manager;
            }

            public void onInit(int status)
            {
                if (status == 0) // SUCCESS
                {
                    _ttsManager._isInitialized = true;
                    _ttsManager.SetLanguage();
                }
                else
                {
                    Debug.LogError("Error initializing TextToSpeech.");
                }
            }
        }

        private void SetLanguage()
        {
            // Gewünschte Sprache: Deutsch (Deutschland)
            AndroidJavaClass localeClass = new AndroidJavaClass("java.util.Locale");
            _locale = new AndroidJavaObject("java.util.Locale", "de", "DE");

            int result = _ttsObject.Call<int>("setLanguage", _locale);

            if (result == -2) // LANG_MISSING_DATA
            {
                Debug.LogError("The selected language is not available (LANG_MISSING_DATA).");
            }
            else if (result == -1) // LANG_NOT_SUPPORTED
            {
                Debug.LogError("The selected language is not supported (LANG_NOT_SUPPORTED).");
            }
        }

        private void StartEmptySpeech()
        {
            string emptyMessage = "";
            string utteranceId = "UniqueID_" + System.Guid.NewGuid().ToString();
            int apiLevel = GetAndroidSDKVersion();

            if (apiLevel >= 21)
            {
                AndroidJavaObject bundleParams = new AndroidJavaObject("android.os.Bundle");
                bundleParams.Call("putString", "utteranceId", utteranceId);
                _ttsObject.Call<int>("speak", emptyMessage, 0, bundleParams, utteranceId);
            }
            else
            {
                AndroidJavaObject hashMapParams = new AndroidJavaObject("java.util.HashMap");
                hashMapParams.Call<AndroidJavaObject>("put", "utteranceId", utteranceId);
                _ttsObject.Call<int>("speak", emptyMessage, 0, hashMapParams);
            }
        }

        private int GetAndroidSDKVersion()
        {
            AndroidJavaClass versionClass = new AndroidJavaClass("android.os.Build$VERSION");
            return versionClass.GetStatic<int>("SDK_INT");
        }

        private void OnDestroy()
        {
            if (_ttsObject != null)
            {
                _ttsObject.Call("shutdown");
                _ttsObject.Dispose();
                _ttsObject = null;
            }
        }
#endif
    }
}