using UnityEngine;
using System.Collections;

public class TextToSpeechManager : MonoBehaviour
{
    private static TextToSpeechManager instance;

    private AndroidJavaObject ttsObject;
    private AndroidJavaObject locale;

    private bool ttsIsActive = true;

    private string ttsString = "Folgende Antwortmöglichkeiten stehen dir zur Auswahl: ";

    private int optionCounter = 0;

    private bool isSpeaking = false;

    private bool isInitialized = false;

    private string lastMessage = "";

    private bool wasPaused = false;

    public static TextToSpeechManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TextToSpeechManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("TextToSpeechManager");
                    instance = obj.AddComponent<TextToSpeechManager>();
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ttsIsActive = PlayerPrefs.GetInt("TTS", 0) != 0;

        if (Application.platform == RuntimePlatform.Android)
        {
            // Initialize the TTS engine
            StartCoroutine(InitializeTTS());
        }
    }

    public void SetIsSpeaking(bool boolValue)
    {
        isSpeaking = boolValue;
    }

    public bool IsSpeaking()
    {
        return isSpeaking;
    }

    public void SetWasPaused(bool boolValue)
    {
        wasPaused = boolValue;
    }

    public bool WasPaused()
    {
        return wasPaused;
    }

    public void ActivateTTS()
    {
        ttsIsActive = true;
        PlayerPrefs.SetInt("TTS", 1);
        PlayerPrefs.Save();
        Debug.Log("ttsIsActive: " + ttsIsActive);
    }

    public void DeactivateTTS()
    {
        ttsIsActive = false;
        PlayerPrefs.SetInt("TTS", 0);
        PlayerPrefs.Save();
        Debug.Log("ttsIsActive: " + ttsIsActive);
    }

    public bool IsTextToSpeechActivated()
    {
        return ttsIsActive;
    }

    IEnumerator InitializeTTS()
    {
        yield return new WaitForSeconds(0.1f);

        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            // Create the TTS instance with a listener
            ttsObject = new AndroidJavaObject("android.speech.tts.TextToSpeech", unityActivity, new OnInitListener(this));
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
                Debug.Log("TextToSpeech successfully initialized.");
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
        Debug.Log("setLanguage return value: " + result);

        if (result == -2) // LANG_MISSING_DATA
        {
            Debug.LogError("The selected language is not available (LANG_MISSING_DATA).");
        }
        else if (result == -1) // LANG_NOT_SUPPORTED
        {
            Debug.LogError("The selected language is not supported (LANG_NOT_SUPPORTED).");
        }
        else
        {
            Debug.Log("Language set successfully.");
        }
    }

    public void AddChoiceToChoiceCollectionForTextToSpeech(string choice)
    {
        optionCounter++;
        switch (optionCounter)
        {
            case 1:
                ttsString += "Erste Option: " + choice + ". ";
                break;
            case 2:
                ttsString += "Zweite Option: " + choice + ". ";
                break;
            case 3:
                ttsString += "Dritte Option: " + choice + ". ";
                break;
            case 4:
                ttsString += "Vierte Option: " + choice + ". ";
                break;
            case 5:
                ttsString += "Fünfte Option: " + choice + ". ";
                break;
        }
    }

    public IEnumerator ReadChoice()
    {
        yield return Speak(ttsString);
        ttsString = "Folgende Antwortmöglichkeiten stehen dir zur Auswahl: ";
        optionCounter = 0;
    }

    public void CancelSpeak()
    {
        if (ttsObject != null)
        {
            StartEmptySpeech();
            isSpeaking = false;
        }
        else
        {
            Debug.LogError("ttsObject is null. Cannot cancel speech.");
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

    public IEnumerator Speak(string message)
    {
        if (ttsIsActive)
        {
            Debug.Log("TTS with: " + message);

            // Wait until the TTS engine is initialized
            while (!isInitialized)
            {
                yield return null;
            }

            if (ttsObject != null)
            {
                lastMessage = message;
                isSpeaking = true;

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
                Debug.Log("!!!Setze IsSpeaking auf false!!!");
                isSpeaking = false;
            }
            else
            {
                Debug.LogError("TextToSpeech is not initialized.");
            }
        }
        else
        {
            // If TTS is not active, proceed immediately
            yield break;
        }
    }

    public void StopSpeaking()
    {
        if (ttsObject != null)
        {
            StartEmptySpeech();
            isSpeaking = false;
        }
        else
        {
            Debug.LogError("ttsObject is null. Cannot stop speaking.");
        }
    }

    public void RepeatLastMessage()
    {
        StartCoroutine(Speak(lastMessage));
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
}
