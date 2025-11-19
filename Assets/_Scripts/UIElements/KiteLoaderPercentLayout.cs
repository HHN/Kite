using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements
{
    /// <summary>
    /// Manages the layout and size of the Kite loading animation elements (circle, logo, wordmark)
    /// based on screen dimensions and reference resolution from the <c>CanvasScaler</c>.
    /// It applies specific size percentages and handles Z-order relative to a potentially overlaying UI Panel.
    /// </summary>
    public class KiteLoaderPercentLayout : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CanvasScaler scaler;
        [SerializeField] private RectTransform layoutRoot; // Canvas or Content (Reference Area)
        [SerializeField] private RectTransform circle; // WHITE Circle (Image)
        [SerializeField] private RectTransform innerLogo; // ORANGE Logo (Image)
        [SerializeField] private RectTransform wordmark; // "kite" (optional)

        [Header("Percentage Layout Specifications")]
        [SerializeField] private float circleSizePercentOfHeight = 26.7f;
        [SerializeField] private float circleTopPercentOfHeight  = 20.6f;
        [SerializeField, Range(0f,1f)] private float innerLogoRelative = 0.95f;

        [Header("Wordmark (Optional)")]
        [SerializeField] private bool  layoutWordmark = true;
        [SerializeField] private float gapUnderCirclePx = 18f;
        [SerializeField] private float wordmarkMinPx = 200f, wordmarkMaxPx = 420f;
        [SerializeField, Range(0f,1f)] private float wordmarkPercentOfContainer = 0.60f;

        [Header("Automatic Corrections")]
        [SerializeField] private bool forceCenterAnchors = true;
        [SerializeField] private bool ensureCircleMask   = true;
        [SerializeField] private bool parentInnerLogoToCircle  = true;

        [Header("Z-Order / Drawing Order")] 
        [Tooltip("The Panel that should visually cover or be related to the circle's drawing order (e.g., TermsAndConditionPanel).")]
        [SerializeField] private RectTransform panel;                // <- Assign your TermsAndConditionPanel here
        [Tooltip("Optional: Raise the Panel's sorting order using Canvas-Sorting to bring it completely to the front (independent of hierarchy).")]
        [SerializeField] private bool raisePanelSortingOrder;
        [SerializeField] private int  panelSortingOrder      = 100;

        /// <summary>
        /// Called when the script is first loaded in the editor or when a component is reset.
        /// Automatically attempts to find the necessary <c>CanvasScaler</c> and <c>layoutRoot</c> references.
        /// </summary>
        private void Reset()
        {
            if (!scaler) scaler = GetComponentInParent<CanvasScaler>();
            if (!layoutRoot && scaler) layoutRoot = scaler.GetComponent<RectTransform>();
        }

        /// <summary>
        /// Called when the script instance is being loaded. Applies the layout logic immediately.
        /// </summary>
        private void Start()
        {
            Apply();
        }

        /// <summary>
        /// Called when the RectTransform's dimensions change. Re-applies the layout to handle resolution changes.
        /// </summary>
        private void OnRectTransformDimensionsChange()
        {
            Apply();
        }

        /// <summary>
        /// Applies the percentage-based layout logic to all referenced UI elements.
        /// Calculates sizes and positions based on the CanvasScaler's reference resolution and the current screen size.
        /// </summary>
        private void Apply()
        {
            if (!scaler || !circle) return;
            if (!layoutRoot) layoutRoot = scaler ? scaler.GetComponent<RectTransform>() : null;
            if (!layoutRoot) return;

            // --- Structure / Parenting ---
            if (panel && circle.parent != panel.parent)
                circle.SetParent(panel.parent, false);          // Set Circle under the same parent as the Panel
            if (parentInnerLogoToCircle && innerLogo && innerLogo.parent != circle)
                innerLogo.SetParent(circle, false);

            if (forceCenterAnchors)
            {
                ForceCenter(layoutRoot);
                ForceCenter(circle);
                if (innerLogo) ForceCenter(innerLogo);
                if (wordmark)  ForceCenter(wordmark);
            }
            if (ensureCircleMask) EnsureMaskOnCircle();

            // Do not block Raycasts
            var cImg = circle.GetComponent<Image>(); if (cImg) cImg.raycastTarget = false;
            var iImg = innerLogo ? innerLogo.GetComponent<Image>() : null; if (iImg) iImg.raycastTarget = false;

            // --- Dimensions calculation (based on Layout Root) ---
            float sf  = ComputeScaleFactor(scaler);
            float Hpx = layoutRoot.rect.height * sf;
            float Wpx = layoutRoot.rect.width  * sf;

            // Circle: Size & Position
            float circleHpx   = Hpx * (circleSizePercentOfHeight / 100f);
            float circleUnits = circleHpx / sf;
            circle.sizeDelta  = new Vector2(circleUnits, circleUnits);

            float topPx           = Hpx * (circleTopPercentOfHeight / 100f);
            float centerFromMidPx = (Hpx / 2f) - topPx - (circleHpx / 2f);
            circle.anchoredPosition = new Vector2(0f, centerFromMidPx / sf);

            // Inner Logo: 95% centered
            if (innerLogo)
            {
                float inner = circleUnits * innerLogoRelative;
                innerLogo.sizeDelta = new Vector2(inner, inner);
                innerLogo.anchoredPosition = Vector2.zero;
                innerLogo.localScale = Vector3.one;
                if (iImg) iImg.preserveAspect = true;
            }

            // Wordmark
            if (layoutWordmark && wordmark)
            {
                float wmW = Mathf.Clamp(wordmarkPercentOfContainer * (Wpx / sf),
                    wordmarkMinPx / sf, wordmarkMaxPx / sf);
                float wmH = wmW * 0.25f;
                var wImg = wordmark.GetComponent<Image>();
                if (wImg && wImg.sprite)
                {
                    var r = wImg.sprite.rect;
                    wmH = wmW * (r.height / r.width);
                    wImg.preserveAspect = true;
                    wImg.raycastTarget  = false;
                }
                wordmark.sizeDelta = new Vector2(wmW, wmH);
                float y = (centerFromMidPx - (circleHpx / 2f) - gapUnderCirclePx - (wmH * sf) / 2f) / sf;
                wordmark.anchoredPosition = new Vector2(0f, y);
            }

            // --- Drawing Order: Circle and Panel Z-Order ---
            if (!panel) return;
            
            // Insert Circle directly BEFORE the Panel -> so it's drawn BEHIND the Panel
            int idx = panel.GetSiblingIndex();
            circle.SetSiblingIndex(Mathf.Clamp(idx, 0, panel.parent.childCount - 1));

            // Sort Panel as the very last sibling (draws it over the circle)
            panel.SetAsLastSibling();

            // Optional: Bring it to the very front using Canvas-Sorting (independent of hierarchy)
            if (!raisePanelSortingOrder) return;
            
            var canvas = panel.GetComponent<Canvas>();
            
            if (!canvas) canvas = panel.gameObject.AddComponent<Canvas>();
            
            canvas.overrideSorting = true;
            canvas.sortingOrder = panelSortingOrder;
            
            if (!panel.GetComponent<GraphicRaycaster>()) panel.gameObject.AddComponent<GraphicRaycaster>();
        }

        /// <summary>
        /// Forces the anchors and pivot of a <c>RectTransform</c> to be centered (0.5, 0.5)
        /// and resets the local scale to ensure consistent positioning relative to the parent.
        /// </summary>
        /// <param name="rt">The RectTransform to center.</param>
        private static void ForceCenter(RectTransform rt)
        {
            if (!rt) return;
            
            rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot     = new Vector2(0.5f, 0.5f);
            rt.localScale = Vector3.one;
        }

        /// <summary>
        /// Ensures that the circle element has a <c>Mask</c> component attached
        /// to properly clip its children (the inner logo) and sets its image type to <c>Simple</c>.
        /// </summary>
        private void EnsureMaskOnCircle()
        {
            var img = circle.GetComponent<Image>();
            if (img) img.type = Image.Type.Simple;
            var mask = circle.GetComponent<Mask>();
            
            if (!mask) mask = circle.gameObject.AddComponent<Mask>();
            
            mask.showMaskGraphic = true;
        }

        /// <summary>
        /// Computes the effective scale factor of the Canvas based on its <c>CanvasScaler</c> settings.
        /// This formula replicates how Unity calculates the scaling to convert reference resolution units to screen pixels.
        /// </summary>
        /// <param name="cs">The CanvasScaler instance.</param>
        /// <returns>The calculated scale factor.</returns>
        private static float ComputeScaleFactor(CanvasScaler cs)
        {
            Vector2 screen = new Vector2(Screen.width, Screen.height);
            Vector2 refRes = cs.referenceResolution;
            
            float logWidth = Mathf.Log(screen.x / refRes.x, 2);
            float logHeight = Mathf.Log(screen.y / refRes.y, 2);
            float logWeighted = Mathf.Lerp(logWidth, logHeight, cs.matchWidthOrHeight);
            
            return Mathf.Pow(2, logWeighted);
        }
    }
}
