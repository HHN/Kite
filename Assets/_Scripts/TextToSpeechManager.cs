using System.Collections;
using UnityEngine;

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

    // iOS native methods
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void _InitializeTTS();

    [DllImport("__Internal")]
    private static extern void _Speak(string message);

    [DllImport("__Internal")]
    private static extern void _StopSpeaking();

    [DllImport("__Internal")]
    private static extern bool _IsSpeaking();
#endif

    public static TextToSpeechManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TextToSpeechManager>();
                if (_instance == null)
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
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _ttsIsActive = PlayerPrefs.GetInt("TTS", 0) != 0;
        _soundEffectIsActive = PlayerPrefs.GetInt("SoundEffect", 0) != 0;

#if UNITY_ANDROID
        if (Application.platform == RuntimePlatform.Android)
        {
            // Initialize the TTS engine for Android
            StartCoroutine(InitializeTTSAndroid());
        }
#elif UNITY_IOS
        // Initialize the TTS engine for iOS
        _InitializeTTS();
#endif
    }

    // SoundEffect:
    public bool IsSoundEffectActivated()
    {
        return _soundEffectIsActive;
    }

    public void SetLastMessage(string message)
    {
        _lastMessage = message;
    }

    public string GetLastMessage()
    {
        return _lastMessage;
    }

    public void SetIsSpeaking(bool boolValue)
    {
        _isSpeaking = boolValue;
    }

    public bool IsSpeaking()
    {
#if UNITY_ANDROID
        return _isSpeaking;
#elif UNITY_IOS
        return _IsSpeaking();
#else
        return false;
#endif
    }

    public void SetWasPaused(bool boolValue)
    {
        _wasPaused = boolValue;
    }

    public bool WasPaused()
    {
        return _wasPaused;
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

    public bool IsTextToSpeechActivated()
    {
        return _ttsIsActive;
    }

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

    public void CancelSpeak()
    {
#if UNITY_ANDROID
        if (_ttsObject != null)
        {
            StartEmptySpeech();
            _isSpeaking = false;
        }
#elif UNITY_IOS
        _StopSpeaking();
        _isSpeaking = false;
#endif
    }

    public IEnumerator Speak(string message)
    {
#if UNITY_EDITOR
        // Im Unity-Editor die Coroutine beenden, da TTS dort nicht läuft
        yield break;
#endif
        if (_ttsIsActive)
        {
            _lastMessage = message;
            _isSpeaking = true;

#if UNITY_ANDROID
            // Android implementation
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
                    // For API Level 21 and above
                    AndroidJavaObject bundleParams = new AndroidJavaObject("android.os.Bundle");
                    bundleParams.Call("putString", "utteranceId", utteranceId);
                    _ttsObject.Call<int>("speak", message, 0, bundleParams, utteranceId);

                    // Wait until the TTS engine is finished
                    while (_ttsObject.Call<bool>("isSpeaking"))
                    {
                        yield return null;
                    }
                }
                else
                {
                    // For API Level below 21
                    AndroidJavaObject hashMapParams = new AndroidJavaObject("java.util.HashMap");
                    hashMapParams.Call<AndroidJavaObject>("put", "utteranceId", utteranceId);
                    _ttsObject.Call<int>("speak", message, 0, hashMapParams);

                    // Use estimated wait time
                    float estimatedDuration = message.Length * 0.05f; // Adjustable based on speech rate
                    yield return new WaitForSeconds(estimatedDuration);
                }
            }
            else
            {
                Debug.LogError("TextToSpeech is not initialized.");
            }
#elif UNITY_IOS
            // iOS implementation
            _Speak(message);

            // Wait until the speech is finished
            while (_IsSpeaking())
            {
                yield return null;
            }
#endif
            _isSpeaking = false;
            yield return null;
        }
        else
        {
            // TTS ist nicht aktiv; direkt beenden
            yield break;
        }
    }

    public void StopSpeaking()
    {
#if UNITY_ANDROID
        if (_ttsObject != null)
        {
            StartEmptySpeech();
            _isSpeaking = false;
        }
        else
        {
            Debug.LogError("ttsObject is null. Cannot stop speaking.");
        }
#elif UNITY_IOS
        _StopSpeaking();
        _isSpeaking = false;
#endif
    }

    public IEnumerator RepeatLastMessage()
    {
        yield return StartCoroutine(Speak(_lastMessage));
    }

#if UNITY_ANDROID
    // Android-specific fields and methods
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

            // Create the TTS instance with a listener
            _ttsObject = new AndroidJavaObject(
                "android.speech.tts.TextToSpeech",
                unityActivity,
                new OnInitListener(this)
            );
        }
    }

    // OnInitListener class to handle TTS initialization
    private class OnInitListener : AndroidJavaProxy
    {
        private readonly TextToSpeechManager _ttsManager;

        // Hier exakt die Java-Schnittstelle: "android.speech.tts.TextToSpeech$OnInitListener"
        public OnInitListener(TextToSpeechManager manager)
            : base("android.speech.tts.TextToSpeech$OnInitListener")
        {
            _ttsManager = manager;
        }

        // Java-Methode heißt onInit(int status) (kleines 'o'), 
        // aber in diesem Interface wird sie groß O geschrieben: OnInitListener
        // => Die Methode in C# muss "onInit" heißen, um gematcht zu werden
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
        // Use the desired locale
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
