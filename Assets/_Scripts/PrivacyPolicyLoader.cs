using System.Text.RegularExpressions;
using Assets._Scripts.Biases;
using TMPro;
using UnityEngine;

namespace Assets._Scripts
{
    public class PrivacyPolicyLoader : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] private Transform contentParent;
        [SerializeField] private string textFileName = "PrivacyPolicy";
        [SerializeField] private TMP_FontAsset font;

        [Header("Schriftgr��en")]
        [SerializeField] private int heading1Size = 40;
        [SerializeField] private int heading2Size = 38;
        [SerializeField] private int heading3Size = 36;
        [SerializeField] private int heading4Size = 36;
        [SerializeField] private int normalTextSize = 35;

        [Header("Schriftfarben")]
        [SerializeField] private Color heading1Color = Color.black;
        [SerializeField] private Color heading2Color = Color.black;
        [SerializeField] private Color heading3Color = Color.black;
        [SerializeField] private Color heading4Color = Color.black;
        [SerializeField] private Color normalTextColor = Color.black;
        [SerializeField] private Color linkColor = Color.blue;

        void Start()
        {
            LoadPrivacyPolicy();
        }

        void LoadPrivacyPolicy()
        {
            // L�d die Textdatei aus dem Resources-Ordner
            TextAsset textAsset = Resources.Load<TextAsset>(textFileName);
            if (textAsset == null)
            {
                Debug.LogError("Textdatei nicht gefunden: " + textFileName);
                return;
            }

            // Aufteilen der Datei in Bl�cke anhand des Delimiters "$%"
            string[] rawBlocks = textAsset.text.Split(new string[] { "$%" }, System.StringSplitOptions.RemoveEmptyEntries);

            // Regex zur Erkennung von �berschriften (Nummerierung am Zeilenanfang)
            Regex headingRegex = new Regex(@"^(?<num>(\d+(\.\d+){0,3}\.)(\s+)?)(?<content>.*)$");

            // Regex zur Erkennung von URLs in normalen Textbl�cken
            Regex linkRegex = new Regex(@"(http[s]?://\S+|www\.\S+)", RegexOptions.Compiled);

            foreach (string rawBlock in rawBlocks)
            {
                // Entferne f�hrende und abschlie�ende Leerzeichen/Tabs
                string block = rawBlock.Trim();
                if (string.IsNullOrEmpty(block))
                    continue;

                // Neues GameObject f�r diesen Block erstellen und als Child im contentParent einordnen
                GameObject blockGO = new GameObject("PrivacyBlock");
                blockGO.transform.SetParent(contentParent, false);

                // TextMeshPro-Komponente hinzuf�gen
                TextMeshProUGUI textComp = blockGO.AddComponent<TextMeshProUGUI>();
                textComp.font = font;
                textComp.enableWordWrapping = true;

                // Pr�fung, ob der Block als �berschrift interpretiert werden soll
                Match match = headingRegex.Match(block);
                if (match.Success)
                {
                    // �berschrift: Nummerierung und Text kombinieren
                    string numPart = match.Groups["num"].Value.Trim();
                    string content = match.Groups["content"].Value;
                    textComp.text = numPart + " " + content;

                    // Bestimme die �berschriftenebene anhand der Anzahl der Zahlen (getrennt durch '.')
                    int level = numPart.Split(new char[] { '.' }, System.StringSplitOptions.RemoveEmptyEntries).Length;
                    level = Mathf.Clamp(level, 1, 4);

                    // Setze Schriftgr��e, -farbe und, falls Haupt�berschrift, den Bold-Stil
                    switch (level)
                    {
                        case 1:
                            textComp.fontSize = heading1Size;
                            textComp.color = heading1Color;
                            textComp.fontStyle = TMPro.FontStyles.Bold;
                            break;
                        case 2:
                            textComp.fontSize = heading2Size;
                            textComp.color = heading2Color;
                            textComp.fontStyle = TMPro.FontStyles.Normal;
                            break;
                        case 3:
                            textComp.fontSize = heading3Size;
                            textComp.color = heading3Color;
                            textComp.fontStyle = TMPro.FontStyles.Normal;
                            break;
                        case 4:
                            textComp.fontSize = heading4Size;
                            textComp.color = heading4Color;
                            textComp.fontStyle = TMPro.FontStyles.Normal;
                            break;
                    }
                }
                else
                {
                    // Normale Textbl�cke: Links erkennen und formatieren
                    string processedText = linkRegex.Replace(block, (Match m) =>
                    {
                        string url = m.Value;
                        if (!url.StartsWith("http"))
                            url = "http://" + url;
                        // Unterstreiche den Linktext zus�tzlich mittels <u> Tags und verwende Rich-Text f�r Farbe
                        return $"<link=\"{url}\"><color=#{ColorUtility.ToHtmlStringRGBA(linkColor)}><u>{m.Value}</u></color></link>";
                    });
                    textComp.text = processedText;
                    textComp.fontSize = normalTextSize;
                    textComp.color = normalTextColor;

                    // F�ge einen Link-Handler hinzu, der Klicks auf Links verarbeitet
                    blockGO.AddComponent<TextLinkHandler>();
                }
            }
        }
    }
}
