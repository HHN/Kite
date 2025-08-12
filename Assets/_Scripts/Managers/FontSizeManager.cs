using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// Manages font size settings across the application, allowing for dynamic updates to
    /// text components and saving user preferences for font sizes.
    /// </summary>
    public class FontSizeManager : MonoBehaviour
    {
        // Static instance
        private static FontSizeManager _instance;

        // Font size variable
        private int FontSize { get; set; }

        // Values for minimum and maximum allowed font size
        private const int MinFontSize = 35;
        private const int MaxFontSize = 50;

        /// <summary>
        /// Manages font size settings globally across the application. Provides functionalities
        /// to set, retrieve, and apply font settings dynamically to text components, ensuring
        /// a consistent font size experience for users. The manager also persists user preferences
        /// for font sizes between sessions.
        /// </summary>
        private FontSizeManager()
        {
            // Default font size can be initialized here, e.g. middle value between min and max
            FontSize = MinFontSize;
        }

        /// <summary>
        /// Provides global access to the singleton instance of the FontSizeManager class. Ensures that only one instance exists
        /// and initializes the instance when accessed for the first time. If no instance exists in the scene, a new one
        /// is created and marked to persist across scenes.
        /// </summary>
        /// <returns>The singleton instance of the FontSizeManager.</returns>
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

        /// <summary>
        /// Called when the FontSizeManager script instance is being loaded. This method initializes the
        /// font size by retrieving the previously saved font size from PlayerPrefs. If no saved font size
        /// is found, it assigns a default value of the minimum font size.
        /// </summary>
        private void Awake()
        {
            // Load font size from PlayerPrefs (default: minFontSize)
            FontSize = PlayerPrefs.GetInt("SavedFontSize", MinFontSize);
        }

        /// <summary>
        /// Sets the font size to the specified value, clamping it within the allowed minimum
        /// and maximum font size range. The updated font size is saved in the user's preferences
        /// and applied dynamically to all text components in the application.
        /// </summary>
        /// <param name="newFontSize">The new font size to be applied. The value will be clamped within the allowed range.</param>
        public void SetFontSize(int newFontSize)
        {
            FontSize = Mathf.Clamp(newFontSize, MinFontSize, MaxFontSize);
            PlayerPrefs.SetInt("SavedFontSize", FontSize);
            UpdateAllTextComponents();
        }

        /// <summary>
        /// Updates the font size on all text components within the current scene. The method locates
        /// and iterates over all text components of supported types and applies the globally set font size.
        /// It also rebuilds the layout of the text components to ensure proper alignment and spacing are preserved.
        /// Text components tagged with "NoResize" are excluded from updates.
        /// </summary>
        public void UpdateAllTextComponents()
        {
            // Find and update TMP_Text components
            TMP_Text[] tmpTextComponents = FindObjectsOfType<TMP_Text>();
            foreach (TMP_Text tmpTextComponent in tmpTextComponents)
            {
                // Check if the text object has "NoResize" tag and skip
                if (tmpTextComponent.CompareTag("NoResize"))
                {
                    continue;
                }

                tmpTextComponent.fontSize = FontSize;
                tmpTextComponent.ForceMeshUpdate(); // Forces text update (optional for TMP)

                // **Rebuild layout for this text object**
                RectTransform rectTransform = tmpTextComponent.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
                }
            }

            // Find and update UI Text components
            Text[] uiTextComponents = FindObjectsOfType<Text>();
            foreach (Text uiTextComponent in uiTextComponents)
            {
                // Check if the text object has "NoResize" tag and skip
                if (uiTextComponent.CompareTag("NoResize"))
                {
                    continue;
                }

                uiTextComponent.fontSize = FontSize;

                // Rebuild layout for this text object
                RectTransform rectTransform = uiTextComponent.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
                }
            }
        }
    }
}