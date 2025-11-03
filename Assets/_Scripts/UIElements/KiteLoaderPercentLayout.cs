using UnityEngine;
using UnityEngine.UI;

public class KiteLoaderPercentLayout : MonoBehaviour
{
    [Header("Referenzen")]
    [SerializeField] private CanvasScaler scaler;
    [SerializeField] private RectTransform layoutRoot; // Canvas oder Content (Bezugsfläche)
    [SerializeField] private RectTransform circle;     // WEISSER Kreis (Image)
    [SerializeField] private RectTransform innerLogo;  // ORANGES Logo (Image)
    [SerializeField] private RectTransform wordmark;   // "kite" (optional)

    [Header("Prozentvorgaben vom Layout")]
    [SerializeField] private float circleSizePercentOfHeight = 26.7f;
    [SerializeField] private float circleTopPercentOfHeight  = 20.6f;
    [SerializeField, Range(0f,1f)] private float innerLogoRelative = 0.95f;

    [Header("Wortmarke (optional)")]
    [SerializeField] private bool  layoutWordmark = true;
    [SerializeField] private float gapUnderCirclePx = 18f;
    [SerializeField] private float wordmarkMinPx = 200f, wordmarkMaxPx = 420f;
    [SerializeField, Range(0f,1f)] private float wordmarkPercentOfContainer = 0.60f;

    [Header("Automatische Korrekturen")]
    [SerializeField] private bool forceCenterAnchors = true;
    [SerializeField] private bool ensureCircleMask   = true;
    [SerializeField] private bool parentInnerLogoToCircle  = true;

    [Header("Z-Order / Reihenfolge")]
    [Tooltip("Panel, vor dem der Circle liegen soll (z. B. TermsAndConditionPanel).")]
    [SerializeField] private RectTransform panel;                // <- HIER dein TermsAndConditionPanel zuweisen
    [Tooltip("Optional: Panel zusätzlich per Canvas-Sorting ganz nach vorn holen.")]
    [SerializeField] private bool raisePanelSortingOrder = false;
    [SerializeField] private int  panelSortingOrder      = 100;

    void Reset()
    {
        if (!scaler) scaler = GetComponentInParent<CanvasScaler>();
        if (!layoutRoot && scaler) layoutRoot = scaler.GetComponent<RectTransform>();
    }

    void Start() { Apply(); }
    void OnRectTransformDimensionsChange() { Apply(); }

    void Apply()
    {
        if (!scaler || !circle) return;
        if (!layoutRoot) layoutRoot = scaler ? scaler.GetComponent<RectTransform>() : null;
        if (!layoutRoot) return;

        // --- Struktur / Parenting ---
        if (panel && circle.parent != panel.parent)
            circle.SetParent(panel.parent, false);          // Circle unter denselben Parent wie das Panel
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

        // Raycasts nicht blockieren
        var cImg = circle.GetComponent<Image>(); if (cImg) cImg.raycastTarget = false;
        var iImg = innerLogo ? innerLogo.GetComponent<Image>() : null; if (iImg) iImg.raycastTarget = false;

        // --- Maße wie im HTML (auf Basis des Layout-Roots) ---
        float sf  = ComputeScaleFactor(scaler);
        float Hpx = layoutRoot.rect.height * sf;
        float Wpx = layoutRoot.rect.width  * sf;

        // Kreis: Größe & Position
        float circleHpx   = Hpx * (circleSizePercentOfHeight / 100f);
        float circleUnits = circleHpx / sf;
        circle.sizeDelta  = new Vector2(circleUnits, circleUnits);

        float topPx           = Hpx * (circleTopPercentOfHeight / 100f);
        float centerFromMidPx = (Hpx / 2f) - topPx - (circleHpx / 2f);
        circle.anchoredPosition = new Vector2(0f, centerFromMidPx / sf);

        // Innenlogo: 95% mittig
        if (innerLogo)
        {
            float inner = circleUnits * innerLogoRelative;
            innerLogo.sizeDelta = new Vector2(inner, inner);
            innerLogo.anchoredPosition = Vector2.zero;
            innerLogo.localScale = Vector3.one;
            if (iImg) iImg.preserveAspect = true;
        }

        // Wortmarke
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

        // --- Reihenfolge: Circle hinter Panel, Panel ganz nach vorn ---
        if (panel)
        {
            // Circle direkt VOR das Panel einfügen -> damit HINTER dem Panel gezeichnet
            int idx = panel.GetSiblingIndex();
            circle.SetSiblingIndex(Mathf.Clamp(idx, 0, panel.parent.childCount - 1));

            // Panel als Allerletztes einsortieren
            panel.SetAsLastSibling();

            // Optional: per Canvas-Sorting ganz nach vorn (unabhängig von Hierarchie)
            if (raisePanelSortingOrder)
            {
                var canv = panel.GetComponent<Canvas>();
                if (!canv) canv = panel.gameObject.AddComponent<Canvas>();
                canv.overrideSorting = true;
                canv.sortingOrder    = panelSortingOrder;
                if (!panel.GetComponent<GraphicRaycaster>())
                    panel.gameObject.AddComponent<GraphicRaycaster>();
            }
        }
    }

    static void ForceCenter(RectTransform rt)
    {
        if (!rt) return;
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot     = new Vector2(0.5f, 0.5f);
        rt.localScale = Vector3.one;
    }

    void EnsureMaskOnCircle()
    {
        var img = circle.GetComponent<Image>(); if (img) img.type = Image.Type.Simple;
        var mask = circle.GetComponent<Mask>(); if (!mask) mask = circle.gameObject.AddComponent<Mask>();
        mask.showMaskGraphic = true;
    }

    static float ComputeScaleFactor(CanvasScaler cs)
    {
        Vector2 screen = new Vector2(Screen.width, Screen.height);
        Vector2 refRes = cs.referenceResolution;
        float logWidth  = Mathf.Log(screen.x / refRes.x, 2);
        float logHeight = Mathf.Log(screen.y / refRes.y, 2);
        float logWeighted = Mathf.Lerp(logWidth, logHeight, cs.matchWidthOrHeight);
        return Mathf.Pow(2, logWeighted);
    }
}
