using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Managers
{
    public class FontSizeManager : MonoBehaviour
    {
        // Statische Instanz
        private static FontSizeManager _instance;

        // Schriftgrößen-Variablen
        public int FontSize { get; private set; }

        // Werte f�r die minimal und maximal zulässige Schriftgröße
        private const int MIN_FONT_SIZE = 35;
        private const int MAX_FONT_SIZE = 50;

        // Privater Konstruktor, um Instanziierungen von au�en zu verhindern
        private FontSizeManager()
        {
            // Standard-Schriftgröße kann hier initialisiert werden, z.B. mittlerer Wert zwischen min und max
            FontSize = MIN_FONT_SIZE;
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
            // Schriftgröße aus PlayerPrefs laden (Standard: minFontSize)
            FontSize = UnityEngine.PlayerPrefs.GetInt("SavedFontSize", MIN_FONT_SIZE);
        }

        // Methode, um die Schriftgröße zu setzen, dabei wird sichergestellt, dass die Gr��e im zulässigen Bereich bleibt
        public void SetFontSize(int newFontSize)
        {
            FontSize = Mathf.Clamp(newFontSize, MIN_FONT_SIZE, MAX_FONT_SIZE);
            UnityEngine.PlayerPrefs.SetInt("SavedFontSize", FontSize);
            UpdateAllTextComponents();
        }

        // Methode, um die Schriftgröße direkt auf einen Slider-Wert anzupassen (0 = min, 1 = max)
        public void SetFontSizeFromSlider(float sliderValue)
        {
            FontSize = Mathf.RoundToInt(Mathf.Lerp(MIN_FONT_SIZE, MAX_FONT_SIZE, sliderValue));
            UnityEngine.PlayerPrefs.SetInt("SavedFontSize", FontSize);
            UpdateAllTextComponents();
        }

        public void UpdateAllTextComponents()
        {
            // TMP_Text-Komponenten finden und aktualisieren
            TMP_Text[] tmpTextComponents = FindObjectsOfType<TMP_Text>();
            foreach (TMP_Text tmpTextComponent in tmpTextComponents)
            {
                // Überprüfen, ob das Textobjekt den Tag "NoResize" hat, und überspringen
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
            Text[] uiTextComponents = FindObjectsOfType<Text>();
            foreach (Text uiTextComponent in uiTextComponents)
            {
                // Überprüfen, ob das Textobjekt den Tag "NoResize" hat, und überspringen
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
}