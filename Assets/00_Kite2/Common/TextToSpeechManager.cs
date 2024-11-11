using UnityEngine;
using System.Collections;

public class TextToSpeechManager : MonoBehaviour
{
    private static TextToSpeechManager instance;

    private AndroidJavaObject ttsObject;
    private AndroidJavaObject locale;

    private bool ttsIsActive = false;

    public static TextToSpeechManager Instance
    {
        get
        {
            if (instance == null)
            {
                // Versuchen, eine existierende Instanz zu finden
                instance = FindObjectOfType<TextToSpeechManager>();
                if (instance == null)
                {
                    // Neues GameObject erstellen und Komponente hinzufügen
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
        if (Application.platform == RuntimePlatform.Android)
        {
            // Initialisierung der TTS-Engine
            StartCoroutine(InitializeTTS());
        }
    }

    public void ActivateTTS()
    {
        ttsIsActive = true;
        // TODO:
        // Add save in PlayerPrefs
    }

    public void DeactivateTTS()
    {
        ttsIsActive = false;
        // TODO:
        // Add save in PlayerPrefs
    }

    public bool IsTextToSpeechActivated()
    {
        return ttsIsActive;
    }

    IEnumerator InitializeTTS()
    {
        yield return new WaitForSeconds(0.1f);

        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        TTSInitListener initListener = new TTSInitListener();

        ttsObject = new AndroidJavaObject("android.speech.tts.TextToSpeech", unityActivity, initListener);

        yield return new WaitUntil(() => initListener.isInitialized);

        if (initListener.status == 0) // SUCCESS
        {
            AndroidJavaClass localeClass = new AndroidJavaClass("java.util.Locale");
            locale = localeClass.GetStatic<AndroidJavaObject>("GERMANY");
            int result = ttsObject.Call<int>("setLanguage", locale);

            if (result == -2) // LANG_MISSING_DATA
            {
                Debug.LogError("Die ausgewählte Sprache ist nicht verfügbar.");
            }
            else if (result == -1) // LANG_NOT_SUPPORTED
            {
                Debug.LogError("Die ausgewählte Sprache wird nicht unterstützt.");
            }
        }
        else
        {
            Debug.LogError("TextToSpeech-Initialisierung fehlgeschlagen.");
        }
    }

    public void Speak(string message)
    {
        //if (ttsObject != null)
        //{
        //    int apiLevel = GetAndroidSDKVersion();
        //    if (apiLevel >= 21)
        //    {
        //        // Für API Level 21 und höher
        //        AndroidJavaObject bundleParams = new AndroidJavaObject("android.os.Bundle");
        //        ttsObject.Call<int>("speak", message, 0, bundleParams, null);
        //    }
        //    else
        //    {
        //        // Für API Level unter 21
        //        AndroidJavaObject hashMapParams = new AndroidJavaObject("java.util.HashMap");
        //        ttsObject.Call<int>("speak", message, 0, hashMapParams);
        //    }
        //}
        //else
        //{
        //    Debug.LogError("TextToSpeech ist nicht initialisiert.");
        //}
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

public class TTSInitListener : AndroidJavaProxy
{
    public bool isInitialized = false;
    public int status = -1;

    public TTSInitListener() : base("android.speech.tts.TextToSpeech$OnInitListener") { }

    void onInit(int status)
    {
        this.status = status;
        isInitialized = true;
    }
}
