using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets._Scripts.Utilities
{
    /// <summary>
    /// Manages screen transitions using a persistent, full-screen black overlay for fading in and out.
    /// Implements a persistent Singleton pattern to be accessible across all scenes.
    /// </summary>
    public class ScreenFade : MonoBehaviour
    {
        /// <summary>
        /// Provides the static, globally accessible instance of the ScreenFade manager.
        /// </summary>
        public static ScreenFade Instance { get; private set; }

        [SerializeField] private float defaultFadeIn  = 0.18f; // The default time in seconds to fade to black.
        [SerializeField] private float defaultFadeOut = 0.40f; // The default time in seconds to fade back to visible (from black).

        private Canvas _canvas;
        private CanvasGroup _group;
        private Image _img;

        /// <summary>
        /// Initializes the Singleton instance and ensures persistence across scene loads.
        /// It then sets up the visual overlay components and hides the fader immediately.
        /// </summary>
        private void Awake()
        {
            if (Instance && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            EnsureOverlay();
            HideImmediate();
        }

        /// <summary>
        /// Ensures that the necessary overlay Canvas, Image, and CanvasGroup components exist.
        /// If they don't exist, this method dynamically creates a dedicated, highest-priority Canvas 
        /// and a full-screen black image to handle the fading effect.
        /// </summary>
        private void EnsureOverlay()
        {
            if (_canvas) return;

            var goCanvas = new GameObject("ScreenFadeCanvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            goCanvas.transform.SetParent(transform, false);

            _canvas = goCanvas.GetComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _canvas.sortingOrder = 32767; // highest sorting order

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

        /// <summary>
        /// Immediately hides the fade overlay by setting its alpha to 0.
        /// </summary>
        private void HideImmediate()
        {
            EnsureOverlay();
            _group.alpha = 0f;
        }

        /// <summary>
        /// Performs a smooth, time-based fade transition of the overlay's alpha.
        /// </summary>
        /// <param name="from">The starting alpha value (0.0 to 1.0).</param>
        /// <param name="to">The target alpha value (0.0 to 1.0).</param>
        /// <param name="duration">The duration of the fade transition in seconds.</param>
        /// <returns>An IEnumerator for the coroutine execution.</returns>
        private IEnumerator Fade(float from, float to, float duration)
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
        /// Initiates a sequence: fades the screen to black, executes a load action (e.g., loading a new scene),
        /// and then automatically fades the screen back to visible upon scene load completion.
        /// </summary>
        /// <param name="loadAction">The action to be executed while the screen is black (usually scene loading logic).</param>
        /// <param name="fadeIn">Optional: The time in seconds to fade to black. Uses default if less than 0.</param>
        /// <param name="fadeOut">Optional: The time in seconds to fade back to visible. Uses default if less than 0.</param>
        public void FadeToBlackAndLoad(Action loadAction, float fadeIn = -1f, float fadeOut = -1f)
        {
            if (fadeIn  < 0f) fadeIn  = defaultFadeIn;
            if (fadeOut < 0f) fadeOut = defaultFadeOut;
            StartCoroutine(CoFadeToBlackAndLoad(loadAction, fadeIn, fadeOut));
        }

        /// <summary>
        /// The core coroutine that orchestrates the fade to black, executes the load action, 
        /// and triggers the fade out when the new scene has finished loading via the <c>sceneLoaded</c> event.
        /// </summary>
        /// <param name="loadAction">The action containing the scene loading logic.</param>
        /// <param name="fadeIn">The time in seconds to fade in (to black).</param>
        /// <param name="fadeOut">The time in seconds to fade out (to visible).</param>
        /// <returns>An IEnumerator for the coroutine execution.</returns>
        private IEnumerator CoFadeToBlackAndLoad(Action loadAction, float fadeIn, float fadeOut)
        {
            EnsureOverlay();

            // 1) Fade to black
            yield return Fade(_group.alpha, 1f, fadeIn);

            // 2) Set up handler to fade out upon scene completion
            void Handler(Scene s, LoadSceneMode m)
            {
                SceneManager.sceneLoaded -= Handler;
                StartCoroutine(Fade(1f, 0f, fadeOut));
            }
            SceneManager.sceneLoaded += Handler;

            // 3) Start loading (loadAction should internally call SceneManager.LoadScene)
            loadAction?.Invoke();
        }
    }
}
