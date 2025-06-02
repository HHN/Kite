using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Assets._Scripts.Biases;

namespace Assets._Scripts
{
    public class PrivacyPolicyLoader : MonoBehaviour
    {
        [Header("Setup")] [SerializeField] private Transform contentParent;
        [SerializeField] private string textFileName; // Wichtig: Dateiendung .txt
        [SerializeField] private TMP_FontAsset font;

        [Header("Schriftgrößen")] [SerializeField]
        private int heading1Size = 40;

        [SerializeField] private int heading2Size = 38;
        [SerializeField] private int heading3Size = 36;
        [SerializeField] private int heading4Size = 36;
        [SerializeField] private int normalTextSize = 35;
        [SerializeField] private int indentedTextSize = 35; // Beispiel für eingerückten Text

        [Header("Schriftfarben")] [SerializeField]
        private Color heading1Color = Color.black;

        [SerializeField] private Color heading2Color = Color.black;
        [SerializeField] private Color heading3Color = Color.black;
        [SerializeField] private Color heading4Color = Color.black;
        [SerializeField] private Color normalTextColor = Color.black;
        [SerializeField] private Color indentedTextColor = Color.black; // Beispiel für eingerückten Text
        [SerializeField] private Color linkColor = Color.blue;

        void Start()
        {
            LoadPrivacyPolicyFromFile();
        }

        void LoadPrivacyPolicyFromFile()
        {
            // string filePath = Path.Combine(Application.streamingAssetsPath, textFileName);
            TextAsset textAsset = Resources.Load<TextAsset>(textFileName);

            if (textAsset == null)
            {
                Debug.LogError("Textdatei nicht gefunden: " + textFileName);
                return;
            }

            string text = textAsset.text;
            // string text = File.ReadAllText(textAsset.text);
            // Trenne den Text in Abschnitte, entweder nach Zeilen, die mit '#' beginnen, oder nach Leerzeilen
            string[] sections = Regex.Split(text, @"(?m)^(#[^#])|(?m)^(\s*)$");

            // Regex zur Erkennung von Markdown-ähnlichen Links: [Anzeigetext](URL)
            Regex linkRegex = new Regex(@"\[(?<text>[^\]]+)\]\((?<url>[^\)]+)\)", RegexOptions.Compiled);
            string currentBlockText = "";
            GameObject currentBlockGO = null;

            for (int i = 0; i < sections.Length; i++)
            {
                string line = sections[i].Trim();
                bool isLastSection = (i == sections.Length - 1);

                if (string.IsNullOrWhiteSpace(line))
                {
                    // Leerzeile: Schließe den vorherigen Block ab und beginne einen neuen
                    if (currentBlockGO != null)
                    {
                        FinishBlock(currentBlockGO, currentBlockText, linkRegex);
                    }
                    currentBlockText = "";
                    currentBlockGO = null; // Setze zurück, damit ein neuer Block erzeugt wird
                    continue;
                }

                if (line.StartsWith("#"))
                {
                    // Überschrift: Schließe den vorherigen Block ab und beginne einen neuen
                    if (currentBlockGO != null)
                    {
                        FinishBlock(currentBlockGO, currentBlockText, linkRegex);
                    }
                    //Beginne neuen Block
                    currentBlockText = line;
                    currentBlockGO = new GameObject("PrivacyBlock");
                    currentBlockGO.transform.SetParent(contentParent, false);
                }
                else
                {
                    // Normaler Text: Füge ihn zum aktuellen Block hinzu
                    if (currentBlockGO == null) //Erstelle einen neuen Block, falls noch keiner existiert
                    {
                         currentBlockGO = new GameObject("PrivacyBlock");
                         currentBlockGO.transform.SetParent(contentParent, false);
                    }
                    currentBlockText += (string.IsNullOrEmpty(currentBlockText) ? "" : " ") + line;
                }

                //Verarbeite den letzten Block
                if (isLastSection)
                {
                    FinishBlock(currentBlockGO, currentBlockText, linkRegex);
                }
            }
        }

        private void FinishBlock(GameObject blockGO, string blockText, Regex linkRegex)
        {
            TextMeshProUGUI textComp = blockGO.AddComponent<TextMeshProUGUI>();
            textComp.font = font;
            textComp.enableWordWrapping = true;
            string displaytext = blockText;

            if (!string.IsNullOrEmpty(blockText))
            {
                if (blockText.StartsWith("#"))
                {
                    // Debug.Log($"blockText: {blockText}");
                    if (blockText.StartsWith("#### "))
                    {
                        textComp.fontSize = heading4Size;
                        textComp.color = heading4Color;
                        displaytext = linkRegex.Replace(blockText.Substring(5), MatchEvaluator);
                        
                    }
                    else if (blockText.StartsWith("### "))
                    {
                        textComp.fontSize = heading3Size;
                        textComp.color = heading3Color;
                        displaytext = linkRegex.Replace(blockText.Substring(4), MatchEvaluator);
                       
                    }
                    else if (blockText.StartsWith("## "))
                    {
                        // Debug.Log($"heading2Size: {heading2Size}");
                        textComp.fontSize = heading2Size;
                        // Debug.Log($"textComp.fontSize: {textComp.fontSize}");
                        textComp.color = heading2Color;
                        textComp.fontStyle = TMPro.FontStyles.Bold;
                        displaytext = linkRegex.Replace(blockText.Substring(3), MatchEvaluator);
                        
                    }
                    else if (blockText.StartsWith("# "))
                    {
                        textComp.fontSize = heading1Size;
                        textComp.color = heading1Color;
                        textComp.fontStyle = TMPro.FontStyles.Bold;
                        displaytext = linkRegex.Replace(blockText.Substring(2), MatchEvaluator);
                        
                    }
                   
                }
                else
                {
                    textComp.fontSize = normalTextSize;
                    textComp.color = normalTextColor;
                    displaytext = linkRegex.Replace(blockText, MatchEvaluator);
                }
            }
            else
            {
                textComp.fontSize = normalTextSize;
                textComp.color = normalTextColor;
                displaytext = linkRegex.Replace(blockText, MatchEvaluator);
            }

            textComp.text = displaytext;
            textComp.ForceMeshUpdate(); // Füge diese Zeile hinzu, um die Textanzeige zu aktualisieren
            if (textComp.text.Contains("<link="))
            {
                blockGO.AddComponent<TextLinkHandler>();
            }
        }

        private string MatchEvaluator(Match match)
        {
            string text = match.Groups["text"].Value;
            string url = match.Groups["url"].Value;
            if (!url.StartsWith("http"))
                url = "http://" + url;
            return $"<link=\"{url}\"><color=#{ColorUtility.ToHtmlStringRGBA(linkColor)}><u>{text}</u></color></link>";
        }
    }
}