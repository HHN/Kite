using System.Collections;
using UnityEngine;
using System.Runtime.InteropServices;

namespace Assets._Scripts
{
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
        // Fallbacks, falls wir nicht auf einem echten iOS-Device laufen
        private static void _InitializeTTS()
        {
        }

        private static void _Speak(string message)
        {
        }

        private static void _StopSpeaking()
        {
        }

        private static bool _IsSpeaking()
        {
            return false;
        }
#endif

        // ------------------------------------------------
        // WebGL JavaScript Plugin
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
        // Fallbacks für Editor oder andere Plattformen
        private static void TTS_Initialize()
        {
        }

        private static void TTS_Speak(string message)
        {
        }

        private static void TTS_Stop()
        {
        }

        private static int TTS_IsSpeaking()
        {
            return 0;
        }
#endif

        // ------------------------------------------------
        // Singleton-Implementierung
        // ------------------------------------------------
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
        // Initialisierung
        // ------------------------------------------------
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
        // Getter / Setter
        // ------------------------------------------------
        public bool IsTextToSpeechActivated()
        {
            return _ttsIsActive;
        }

        public void ActivateTTS()
        {
            _ttsIsActive = true;
            PlayerPrefs.SetInt("TTS", 1);
            PlayerPrefs.Save();
        }

        public void DeactivateTTS()
        {
            _ttsIsActive = false;
            PlayerPrefs.SetInt("TTS", 0);
            PlayerPrefs.Save();
        }

        public bool IsSoundEffectActivated()
        {
            return _soundEffectIsActive;
        }

        public void SetWasPaused(bool boolValue)
        {
            _wasPaused = boolValue;
        }

        public bool WasPaused()
        {
            return _wasPaused;
        }

        public void SetLastMessage(string message)
        {
            _lastMessage = message;
        }

        public string GetLastMessage()
        {
            return _lastMessage;
        }

        // ------------------------------------------------
        // Text generieren (z. B. für Multiple Choice)
        // ------------------------------------------------
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

        public IEnumerator ReadChoice()
        {
            yield return Speak(_ttsString);
            _ttsString = "Folgende Antwortmöglichkeiten stehen dir zur Auswahl: ";
            _optionCounter = 0;
        }

        // ------------------------------------------------
        // Sprechen
        // ------------------------------------------------
        public IEnumerator Speak(string message)
        {
            // Im Unity-Editor TTS nicht ausführen
#if UNITY_EDITOR
            yield break;
#else
            Debug.Log($"[TTS Manager] Speak (Build): {message}"); // Optional: Log in Builds
            yield break;
#endif

            if (!_ttsIsActive) yield break;

            _lastMessage = message;
            _isSpeaking = true;

#if UNITY_ANDROID
            // ------------------------------------------
            // Android-Implementierung
            // ------------------------------------------
            while (!_isInitialized)
            {
                yield return null;
            }

            if (_ttsObject != null)
            {
                string utteranceId = "UniqueID_" + System.Guid.NewGuid().ToString();
                int apiLevel = GetAndroidSDKVersion();

                if (apiLevel >= 21)
                {
                    // Für API Level 21 und höher
                    AndroidJavaObject bundleParams = new AndroidJavaObject("android.os.Bundle");
                    bundleParams.Call("putString", "utteranceId", utteranceId);
                    _ttsObject.Call<int>("speak", message, 0, bundleParams, utteranceId);

                    // Warte, bis die Engine fertig gesprochen hat
                    while (_ttsObject.Call<bool>("isSpeaking"))
                    {
                        yield return null;
                    }
                }
                else
                {
                    // Für API Level < 21
                    AndroidJavaObject hashMapParams = new AndroidJavaObject("java.util.HashMap");
                    hashMapParams.Call<AndroidJavaObject>("put", "utteranceId", utteranceId);
                    _ttsObject.Call<int>("speak", message, 0, hashMapParams);

                    // Grobe Wartezeit basierend auf Zeichenlänge
                    float estimatedDuration = message.Length * 0.05f;
                    yield return new WaitForSeconds(estimatedDuration);
                }
            }
            else
            {
                Debug.LogError("TextToSpeech is not initialized.");
            }

#elif UNITY_IOS && !UNITY_EDITOR
            // ------------------------------------------
            // iOS-Implementierung
            // ------------------------------------------
            _Speak(message);
            while (_IsSpeaking())
            {
                yield return null;
            }

#elif UNITY_WEBGL && !UNITY_EDITOR
            // ------------------------------------------
            // WebGL: JavaScript-Plugin
            // ------------------------------------------
            TTS_Speak(message);
            // Warte, bis TTS_IsSpeaking() 0 zurückgibt
            while (TTS_IsSpeaking() == 1)
            {
                yield return null;
            }
#endif

            _isSpeaking = false;
        }


        public void CancelSpeak()
        {
#if UNITY_ANDROID
            if (_ttsObject != null)
            {
                // Leeres Speech aufrufen, um das Aktuelle zu stoppen
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

        public bool IsSpeaking()
        {
#if UNITY_ANDROID
            return _isSpeaking;
#elif UNITY_IOS && !UNITY_EDITOR
            return _IsSpeaking();
#elif UNITY_WEBGL && !UNITY_EDITOR
            // Gibt true zurück, wenn TTS_IsSpeaking() == 1
            return TTS_IsSpeaking() == 1;
#else
            return false;
#endif
        }

        public IEnumerator RepeatLastMessage()
        {
            yield return StartCoroutine(Speak(_lastMessage));
        }

        // ------------------------------------------------
        // Android-spezifische Felder und Methoden
        // ------------------------------------------------
#if UNITY_ANDROID
        private AndroidJavaObject _ttsObject;
        private AndroidJavaObject _locale;
        private bool _isInitialized;

        private IEnumerator InitializeTTSAndroid()
        {
            // Kurze Verzögerung, damit UnityActivity korrekt initialisiert ist
            yield return new WaitForSeconds(0.1f);

            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

                // Erstelle die TextToSpeech-Instanz mit einem Listener
                _ttsObject = new AndroidJavaObject(
                    "android.speech.tts.TextToSpeech",
                    unityActivity,
                    new OnInitListener(this)
                );
            }
        }

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