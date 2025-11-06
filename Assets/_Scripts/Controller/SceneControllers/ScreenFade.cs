using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    public static ScreenFade Instance { get; private set; }

    [SerializeField] private float defaultFadeIn  = 0.18f; // -> zu Schwarz
    [SerializeField] private float defaultFadeOut = 0.40f; // -> zurück sichtbar

    Canvas _canvas;
    CanvasGroup _group;
    Image _img;

    void Awake()
    {
        if (Instance && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        EnsureOverlay();
        HideImmediate();
    }

    void EnsureOverlay()
    {
        if (_canvas) return;

        var goCanvas = new GameObject("ScreenFadeCanvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        goCanvas.transform.SetParent(transform, false);

        _canvas = goCanvas.GetComponent<Canvas>();
        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        _canvas.sortingOrder = 32767; // ganz vorne

        var scaler = goCanvas.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1080, 2160);
        scaler.matchWidthOrHeight = 1f;

        var goImage = new GameObject("Black", typeof(Image), typeof(CanvasGroup));
        goImage.transform.SetParent(goCanvas.transform, false);

        _img = goImage.GetComponent<Image>();
        _img.color = Color.black;
        _img.raycastTarget = false;

        var rt = _img.rectTransform;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        _group = goImage.GetComponent<CanvasGroup>();
        _group.alpha = 0f;
        _group.interactable = false;
        _group.blocksRaycasts = false;
    }

    public void HideImmediate()
    {
        EnsureOverlay();
        _group.alpha = 0f;
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        EnsureOverlay();
        if (Mathf.Approximately(duration, 0f))
        {
            _group.alpha = to;
            yield break;
        }

        float t = 0f;
        _group.alpha = from;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            _group.alpha = Mathf.Lerp(from, to, t / duration);
            yield return null;
        }
        _group.alpha = to;
    }

    /// <summary>
    /// Sofort zu Schwarz blenden, dann loadAction aufrufen. Wenn eine Scene geladen wurde,
    /// automatisch wieder herausfaden.
    /// </summary>
    public void FadeToBlackAndLoad(Action loadAction, float fadeIn = -1f, float fadeOut = -1f)
    {
        if (fadeIn  < 0f) fadeIn  = defaultFadeIn;
        if (fadeOut < 0f) fadeOut = defaultFadeOut;
        StartCoroutine(CoFadeToBlackAndLoad(loadAction, fadeIn, fadeOut));
    }

    IEnumerator CoFadeToBlackAndLoad(Action loadAction, float fadeIn, float fadeOut)
    {
        EnsureOverlay();

        // 1) Zu Schwarz
        yield return Fade(_group.alpha, 1f, fadeIn);

        // 2) Auf SceneLoaded warten -> dann wieder rausfaden
        void Handler(Scene s, LoadSceneMode m)
        {
            SceneManager.sceneLoaded -= Handler;
            StartCoroutine(Fade(1f, 0f, fadeOut));
        }
        SceneManager.sceneLoaded += Handler;

        // 3) Laden starten (dein eigener Code ruft dann SceneManager.LoadScene)
        loadAction?.Invoke();
    }
}
