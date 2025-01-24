using System.Collections;
using UnityEngine;
using System.Runtime.InteropServices;

namespace _00_Kite2.Common
{
    public class TextToSpeechManager : MonoBehaviour
    {
        private static TextToSpeechManager _instance;

        private bool _ttsIsActive = true;

        private bool _soundeffectsIsActive = true;

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

        void Awake()
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

        void Start()
        {
            _ttsIsActive = PlayerPrefs.GetInt("TTS", 0) != 0;
            _soundeffectsIsActive = PlayerPrefs.GetInt("Soundeffects", 0) != 0;


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



        // Soundeffects:
        public bool IsSoundeffectsActivated()
        {
            return _soundeffectsIsActive;
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
        if (ttsObject != null)
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
// Im Unity-Editor sofort die Coroutine beenden
    yield break;
#endif
            if (_ttsIsActive)
            {
                _lastMessage = message;
                _isSpeaking = true;

#if UNITY_ANDROID
            // Android implementation
            while (!isInitialized)
            {
                yield return null;
            }

            if (ttsObject != null)
            {
                string utteranceId = "UniqueID_" + System.Guid.NewGuid().ToString();

                int apiLevel = GetAndroidSDKVersion();
                if (apiLevel >= 21)
                {
                    // For API Level 21 and above
                    AndroidJavaObject bundleParams = new AndroidJavaObject("android.os.Bundle");
                    bundleParams.Call("putString", "utteranceId", utteranceId);
                    ttsObject.Call<int>("speak", message, 0, bundleParams, utteranceId);

                    // Wait until the TTS engine is finished
                    while (ttsObject.Call<bool>("isSpeaking"))
                    {
                        yield return null;
                    }
                }
                else
                {
                    // For API Level below 21
                    AndroidJavaObject hashMapParams = new AndroidJavaObject("java.util.HashMap");
                    hashMapParams.Call<AndroidJavaObject>("put", "utteranceId", utteranceId);
                    ttsObject.Call<int>("speak", message, 0, hashMapParams);

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
                // If TTS is not active, proceed immediately
                yield break;
            }
        }

        public void StopSpeaking()
        {
#if UNITY_ANDROID
        if (ttsObject != null)
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
    private AndroidJavaObject ttsObject;
    private AndroidJavaObject locale;

    private bool isInitialized = false;

    IEnumerator InitializeTTSAndroid()
    {
        yield return new WaitForSeconds(0.1f);

        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            // Create the TTS instance with a listener
            ttsObject =
 new AndroidJavaObject("android.speech.tts.TextToSpeech", unityActivity, new OnInitListener(this));
        }
    }

    // OnInitListener class to handle TTS initialization
    private class OnInitListener : AndroidJavaProxy
    {
        private TextToSpeechManager ttsManager;

        public OnInitListener(TextToSpeechManager manager) : base("android.speech.tts.TextToSpeech$OnInitListener")
        {
            ttsManager = manager;
        }

        public void onInit(int status)
        {
            if (status == 0) // SUCCESS
            {
                ttsManager.isInitialized = true;
                ttsManager.SetLanguage();
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
        locale = new AndroidJavaObject("java.util.Locale", "de", "DE");

        int result = ttsObject.Call<int>("setLanguage", locale);

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
            ttsObject.Call<int>("speak", emptyMessage, 0, bundleParams, utteranceId);
        }
        else
        {
            AndroidJavaObject hashMapParams = new AndroidJavaObject("java.util.HashMap");
            hashMapParams.Call<AndroidJavaObject>("put", "utteranceId", utteranceId);
            ttsObject.Call<int>("speak", emptyMessage, 0, hashMapParams);
        }
    }

    int GetAndroidSDKVersion()
    {
        AndroidJavaClass versionClass = new AndroidJavaClass("android.os.Build$VERSION");
        return versionClass.GetStatic<int>("SDK_INT");
    }

    void OnDestroy()
    {
        if (ttsObject != null)
        {
            ttsObject.Call("shutdown");
            ttsObject.Dispose();
            ttsObject = null;
        }
    }
#endif
    }
}