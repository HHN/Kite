using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FontSizeManager : MonoBehaviour
{
    // Statische Instanz
    private static FontSizeManager instance;

    // Schriftgrößen-Variablen
    public int fontSize { get; private set; }

    // Werte für die minimal und maximal zulässige Schriftgröße
    private int minFontSize = 35;
    private int maxFontSize = 50;

    // Privater Konstruktor, um Instanziierungen von außen zu verhindern
    private FontSizeManager()
    {
        // Standard-Schriftgröße kann hier initialisiert werden, z.B. mittlerer Wert zwischen min und max
        fontSize = minFontSize;
    }

    // Öffentliche Methode zum Abrufen der Instanz
    public static FontSizeManager Instance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<FontSizeManager>();
            if (instance == null)
            {
                GameObject obj = new GameObject("FontSizeManager");
                instance = obj.AddComponent<FontSizeManager>();
                DontDestroyOnLoad(obj);
            }
        }
        return instance;
    }

    void Awake()
    {
        // Schriftgröße aus PlayerPrefs laden (Standard: minFontSize)
        fontSize = PlayerPrefs.GetInt("SavedFontSize", minFontSize);
    }

    // Methode, um die Schriftgröße zu setzen, dabei wird sichergestellt, dass die Größe im zulässigen Bereich bleibt
    public void SetFontSize(int newFontSize)
    {
        fontSize = Mathf.Clamp(newFontSize, minFontSize, maxFontSize);
        PlayerPrefs.SetInt("SavedFontSize", fontSize);
        UpdateAllTextComponents();
    }

    // Methode, um die Schriftgröße direkt auf einen Slider-Wert anzupassen (0 = min, 1 = max)
    public void SetFontSizeFromSlider(float sliderValue)
    {
        fontSize = Mathf.RoundToInt(Mathf.Lerp(minFontSize, maxFontSize, sliderValue));
        PlayerPrefs.SetInt("SavedFontSize", fontSize);
        UpdateAllTextComponents();
    }

    public void UpdateAllTextComponents()
    {
        Debug.Log("Aktuelle Fontsize: " + fontSize);
        // TMP_Text-Komponenten finden und aktualisieren
        TMP_Text[] tmpTextComponents = GameObject.FindObjectsOfType<TMP_Text>();
        foreach (TMP_Text tmpTextComponent in tmpTextComponents)
        {

            // Überprüfen, ob das Textobjekt den Tag "NoResize" hat, und überspringen
            if (tmpTextComponent.CompareTag("NoResize"))
            {
                continue;
            }
            tmpTextComponent.fontSize = fontSize;
            tmpTextComponent.ForceMeshUpdate(); // Erzwingt das Update des Textes (optional für TMP)
        }

        // UI-Text-Komponenten finden und aktualisieren
        Text[] uiTextComponents = GameObject.FindObjectsOfType<Text>();
        foreach (Text uiTextComponent in uiTextComponents)
        {

            // Überprüfen, ob das Textobjekt den Tag "NoResize" hat, und überspringen
            if (uiTextComponent.CompareTag("NoResize"))
            {
                continue;
            }
            uiTextComponent.fontSize = fontSize;
        }
        Debug.Log("Set Fontsize to: " + fontSize);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
