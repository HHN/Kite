using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Assets._Scripts.Biases;

namespace Assets._Scripts
{
    /// <summary>
    /// This script is responsible for loading a privacy policy or similar text content
    /// from a text file located in the Unity Resources folder. It parses the text
    /// using Markdown-like syntax for headings and links, and dynamically creates
    /// TextMeshProUGUI elements to display the formatted content.
    /// </summary>
    public class PrivacyPolicyLoader : MonoBehaviour
    {
        [Header("Setup")] [SerializeField] private Transform contentParent;
        [SerializeField] private string textFileName;
        [SerializeField] private TMP_FontAsset font;

        [Header("Schriftgrößen")] [SerializeField]
        private int heading1Size = 40;

        [SerializeField] private int heading2Size = 38;
        [SerializeField] private int heading3Size = 36;
        [SerializeField] private int heading4Size = 36;
        [SerializeField] private int normalTextSize = 35;
        [SerializeField] private int indentedTextSize = 35;

        [Header("Schriftfarben")] [SerializeField]
        private Color heading1Color = Color.black;

        [SerializeField] private Color heading2Color = Color.black;
        [SerializeField] private Color heading3Color = Color.black;
        [SerializeField] private Color heading4Color = Color.black;
        [SerializeField] private Color normalTextColor = Color.black;
        [SerializeField] private Color indentedTextColor = Color.black;
        [SerializeField] private Color linkColor = Color.blue;

        /// <summary>
        /// Called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// Initiates the process of loading and parsing the privacy policy text.
        /// </summary>
        private void Start()
        {
            LoadPrivacyPolicyFromFile();
        }

        /// <summary>
        /// Loads the text content from the specified text file in the Resources folder,
        /// then parses it line by line to create formatted TextMeshProUGUI elements.
        /// It supports basic Markdown-like headings (#, ##, ###, ####) and links ([text](url)).
        /// </summary>
        private void LoadPrivacyPolicyFromFile()
        {
            // Load the TextAsset from the Resources folder.
            TextAsset textAsset = Resources.Load<TextAsset>(textFileName);

            if (textAsset == null)
            {
                Debug.LogError("Textdatei nicht gefunden: " + textFileName + ". Make sure it's in a Resources folder.", this);
                return;
            }

            string text = textAsset.text;
            // Split the text into sections. Sections are delimited by lines starting with '#' (headings)
            // or by empty lines. (?m) enables multiline mode for Regex.Split.
            string[] sections = Regex.Split(text, @"(?m)^(#[^#])|(?m)^(\s*)$");

            // Regex to find Markdown-like links: [display text](URL)
            // It captures the display text in group "text" and the URL in group "url".
            Regex linkRegex = new Regex(@"\[(?<text>[^\]]+)\]\((?<url>[^\)]+)\)", RegexOptions.Compiled);

            string currentBlockText = "";
            GameObject currentBlockGO = null;

            for (int i = 0; i < sections.Length; i++)
            {
                string line = sections[i].Trim(); // Get the current line and remove leading/trailing whitespace.
                bool isLastSection = (i == sections.Length - 1); // Check if this is the very last section.

                if (string.IsNullOrWhiteSpace(line))
                {
                    // If it's an empty line, it signifies the end of a block.
                    if (currentBlockGO != null)
                    {
                        // Finish processing the previous block if it exists.
                        FinishBlock(currentBlockGO, currentBlockText, linkRegex);
                    }
                    currentBlockText = ""; // Reset block text.
                    currentBlockGO = null; // Reset block GameObject, so a new one is created for the next content.
                    continue; // Move to the next section.
                }

                if (line.StartsWith("#"))
                {
                    // If the line starts with '#', it's a heading, so it's the start of a new block.
                    if (currentBlockGO != null)
                    {
                        // Finish the previous block before starting a new one.
                        FinishBlock(currentBlockGO, currentBlockText, linkRegex);
                    }

                    // Start a new block for the heading.
                    currentBlockText = line; // The heading itself is the content.
                    currentBlockGO = new GameObject("PrivacyBlock"); // Create a new GameObject for this text block.
                    currentBlockGO.transform.SetParent(contentParent, false); // Parent it to the content parent.
                }
                else
                {
                    // If it's not a heading and not an empty line, it's normal text.
                    if (currentBlockGO == null) // If no current block exists (e.g., first line is not a heading).
                    {
                        currentBlockGO = new GameObject("PrivacyBlock");
                        currentBlockGO.transform.SetParent(contentParent, false);
                    }
                    // Append the line to the current block's text. Add a space if it's not the first line in the block.
                    currentBlockText += (string.IsNullOrEmpty(currentBlockText) ? "" : " ") + line;
                }

                // If this is the last section in the file, ensure the last block is processed.
                if (isLastSection)
                {
                    FinishBlock(currentBlockGO, currentBlockText, linkRegex);
                }
            }
        }

        /// <summary>
        /// Finalizes a text block by adding a TextMeshProUGUI component to its GameObject,
        /// applying appropriate font size, color, and style based on whether it's a heading or normal text,
        /// and processing any Markdown-like links within the text.
        /// </summary>
        /// <param name="blockGO">The GameObject representing the current text block.</param>
        /// <param name="blockText">The raw text content of the block.</param>
        /// <param name="linkRegex">The Regex object used to find and format links.</param>
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
                        textComp.fontSize = heading2Size;
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
            textComp.ForceMeshUpdate();
            if (textComp.text.Contains("<link="))
            {
                blockGO.AddComponent<TextLinkHandler>();
            }
        }

        /// <summary>
        /// A MatchEvaluator delegate method used by Regex.Replace to format Markdown-like links
        /// into TextMeshPro rich text link tags.
        /// </summary>
        /// <param name="match">The Regex match object containing captured groups for text and URL.</param>
        /// <returns>A formatted string with TextMeshPro link tags and styling.</returns>
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