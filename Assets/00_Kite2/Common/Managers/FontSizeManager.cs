using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FontSizeManager : MonoBehaviour
{
    // Statische Instanz
    private static FontSizeManager _instance;

    // Schriftgrößen-Variablen
    public int FontSize { get; private set; }

    // Werte f�r die minimal und maximal zul�ssige Schriftgr��e
    private int _minFontSize = 35;
    private int _maxFontSize = 50;

    // Privater Konstruktor, um Instanziierungen von au�en zu verhindern
    private FontSizeManager()
    {
        // Standard-Schriftgr��e kann hier initialisiert werden, z.B. mittlerer Wert zwischen min und max
        FontSize = _minFontSize;
    }

    // Öffentliche Methode zum Abrufen der Instanz
    public static FontSizeManager Instance()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<FontSizeManager>();
            if (_instance == null)
            {
                GameObject obj = new GameObject("FontSizeManager");
                _instance = obj.AddComponent<FontSizeManager>();
                DontDestroyOnLoad(obj);
            }
        }
        return _instance;
    }

    private void Awake()
    {
        // Schriftgr��e aus PlayerPrefs laden (Standard: minFontSize)
        FontSize = PlayerPrefs.GetInt("SavedFontSize", _minFontSize);
    }

    // Methode, um die Schriftgr��e zu setzen, dabei wird sichergestellt, dass die Gr��e im zul�ssigen Bereich bleibt
    public void SetFontSize(int newFontSize)
    {
        FontSize = Mathf.Clamp(newFontSize, _minFontSize, _maxFontSize);
        PlayerPrefs.SetInt("SavedFontSize", FontSize);
        UpdateAllTextComponents();
    }

    // Methode, um die Schriftgr��e direkt auf einen Slider-Wert anzupassen (0 = min, 1 = max)
    public void SetFontSizeFromSlider(float sliderValue)
    {
        FontSize = Mathf.RoundToInt(Mathf.Lerp(_minFontSize, _maxFontSize, sliderValue));
        PlayerPrefs.SetInt("SavedFontSize", FontSize);
        UpdateAllTextComponents();
    }

    public void UpdateAllTextComponents()
    {
        // TMP_Text-Komponenten finden und aktualisieren
        TMP_Text[] tmpTextComponents = GameObject.FindObjectsOfType<TMP_Text>();
        foreach (TMP_Text tmpTextComponent in tmpTextComponents)
        {
            // �berpr�fen, ob das Textobjekt den Tag "NoResize" hat, und �berspringen
            if (tmpTextComponent.CompareTag("NoResize"))
            {
                continue;
            }
            tmpTextComponent.fontSize = FontSize;
            tmpTextComponent.ForceMeshUpdate(); // Erzwingt das Update des Textes (optional f�r TMP)

            // **Layout f�r dieses Textobjekt neu erstellen**
            RectTransform rectTransform = tmpTextComponent.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            }
        }

        // UI-Text-Komponenten finden und aktualisieren
        Text[] uiTextComponents = GameObject.FindObjectsOfType<Text>();
        foreach (Text uiTextComponent in uiTextComponents)
        {
            // �berpr�fen, ob das Textobjekt den Tag "NoResize" hat, und �berspringen
            if (uiTextComponent.CompareTag("NoResize"))
            {
                continue;
            }
            uiTextComponent.fontSize = FontSize;

            // **Layout f�r dieses Textobjekt neu erstellen**
            RectTransform rectTransform = uiTextComponent.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            }
        }
    }
}
