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
    public int minFontSize = 10;
    public int maxFontSize = 50;

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
            instance = new FontSizeManager();
        }
        return instance;
    }

    // Methode, um die Schriftgröße zu setzen, dabei wird sichergestellt, dass die Größe im zulässigen Bereich bleibt
    public void SetFontSize(int newFontSize)
    {
        fontSize = Mathf.Clamp(newFontSize, minFontSize, maxFontSize);
        UpdateAllTextComponents();
    }

    // Methode, um die Schriftgröße direkt auf einen Slider-Wert anzupassen (0 = min, 1 = max)
    public void SetFontSizeFromSlider(float sliderValue)
    {
        fontSize = Mathf.RoundToInt(Mathf.Lerp(minFontSize, maxFontSize, sliderValue));
        UpdateAllTextComponents();
    }

    public void UpdateAllTextComponents()
    {
        // TMP_Text-Komponenten finden und aktualisieren
        TMP_Text[] tmpTextComponents = GameObject.FindObjectsOfType<TMP_Text>();
        foreach (TMP_Text tmpTextComponent in tmpTextComponents)
        {
            tmpTextComponent.fontSize = fontSize;
            tmpTextComponent.ForceMeshUpdate(); // Erzwingt das Update des Textes (optional für TMP)
        }

        // UI-Text-Komponenten finden und aktualisieren
        Text[] uiTextComponents = GameObject.FindObjectsOfType<Text>();
        foreach (Text uiTextComponent in uiTextComponents)
        {
            uiTextComponent.fontSize = fontSize;
        }
    }
}
