using System;
using System.Collections.Generic;
using Assets._Scripts._Mappings;
using Assets._Scripts.Novel.VisualNovelFormatter;
using UnityEngine;

namespace Assets._Scripts.Novel
{
    /// <summary>
    /// Model for a keyword. All fields are optional.
    /// </summary>
    public class NovelKeywordModel
    {
        public int CharacterIndex { get; set; }
        public int FaceExpression { get; set; }
        public string Bias { get; set; }
        public string Sound { get; set; }
        public bool? End { get; set; }
    }

    /// <summary>
    /// Parser that converts a keyword string (e.g., "&gt;&gt;Character1|Looks|Angry&lt;&lt;") into a NovelKeywordModel.
    /// It handles various predefined formats for characters, sounds, biases, and special commands like "End".
    /// <br/>
    /// <br/>
    /// Expected formats:<br/>
    /// &gt;&gt;End&lt;&lt;                   → sets End = true<br/>
    /// &gt;&gt;Info&lt;&lt;                  → sets CharacterIndex based on "Info" mapping<br/>
    /// &gt;&gt;Player&lt;&lt;                → sets CharacterIndex based on "Player" mapping<br/>
    /// &gt;&gt;Character1|Looks|Angry&lt;&lt; → sets CharacterIndex based on Character1's mapped name (e.g., from TalkingPartner01), and FaceExpression based on "Angry". The 'Looks' part (action) is parsed but currently not used in the model.<br/>
    /// &gt;&gt;Sound|TestSound&lt;&lt;        → sets Sound = "TestSound"<br/>
    /// &gt;&gt;Bias|ConfirmationBias&lt;&lt;  → sets Bias = "ConfirmationBias"
    /// </summary>
    public static class NovelKeywordParser
    {
        private const string KeywordStartMarker = ">>";
        private const string KeywordEndMarker = "<<";
        private const string PartSeparator = "|";
        private const string CharacterPrefix = "Character";
        private const string SoundPrefix = "Sound";
        private const string BiasPrefix = "Bias";

        /// <summary>
        /// Parses a single keyword string and returns a <see cref="NovelKeywordModel"/>.
        /// If the string does not match the expected patterns, null is returned.
        /// </summary>
        /// <param name="keyword">The keyword string to parse (e.g., "&gt;&gt;Character1|Speaks|Angry&lt;&lt;").</param>
        /// <param name="kiteNovelMetaData">Novel metadata to map character names to their respective indices.</param>
        /// <returns>A <see cref="NovelKeywordModel"/> or null if no valid keyword was recognized.</returns>
        private static NovelKeywordModel ParseKeyword(string keyword, KiteNovelMetaData kiteNovelMetaData)
        {
            // Early exit for invalid input
            if (string.IsNullOrWhiteSpace(keyword) ||
                !keyword.StartsWith(KeywordStartMarker, StringComparison.Ordinal) ||
                !keyword.EndsWith(KeywordEndMarker, StringComparison.Ordinal))
            {
                return null;
            }

            // Remove the ">>" and "<<" markers.
            keyword = keyword.Substring(KeywordStartMarker.Length, keyword.Length - KeywordStartMarker.Length - KeywordEndMarker.Length).Trim(); // Trim here as well, in case of spaces between marker and content.
            string innerKeyword = keyword;

            NovelKeywordModel model = new NovelKeywordModel();

            // 1. End command
            if (string.Equals(innerKeyword, "End", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(innerKeyword, "Ende", StringComparison.OrdinalIgnoreCase))
            {
                model.End = true;
                return model;
            }

            // 2. Exact keywords "Info" and "Player"
            if (string.Equals(innerKeyword, "Info", StringComparison.OrdinalIgnoreCase))
            {
                model.CharacterIndex = MappingManager.MapCharacter("Info");
                model.FaceExpression = MappingManager.MapFaceExpressions("NeutralRelaxed");
                return model;
            }

            if (string.Equals(innerKeyword, "Player", StringComparison.OrdinalIgnoreCase))
            {
                model.CharacterIndex = MappingManager.MapCharacter("Player");
                model.FaceExpression = MappingManager.MapFaceExpressions("NeutralRelaxed");
                return model;
            }

            // 3. Keywords with separator '|'
            string[] parts = innerKeyword.Split(new[] { PartSeparator }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0) // Should not happen after trimming and splitting, but for safety
            {
                return null;
            }

            string firstPart = parts[0];
            // A. Character keyword
            if (firstPart.StartsWith(CharacterPrefix, StringComparison.OrdinalIgnoreCase))
            {
                string numberPart = firstPart.Substring(CharacterPrefix.Length);
                if (int.TryParse(numberPart, out int charNum))
                {
                    string characterName = null;
                    // Improvement: Use an array or dictionary for TalkingPartner access
                    // Instead of if-else if chain
                    switch (charNum)
                    {
                        case 1: characterName = kiteNovelMetaData.TalkingPartner01; break;
                        case 2: characterName = kiteNovelMetaData.TalkingPartner02; break;
                        case 3: characterName = kiteNovelMetaData.TalkingPartner03; break;
                        default:
                            Debug.LogWarning($"Unknown Character number '{charNum}' in keyword: {keyword}");
                            return null;
                    }

                    if (!string.IsNullOrEmpty(characterName))
                    {
                        model.CharacterIndex = MappingManager.MapCharacter(characterName);
                    }
                    else
                    {
                        Debug.LogWarning($"TalkingPartner{charNum:D2} is null or empty in metadata for keyword: {keyword}");
                        return null; // No valid character name found
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid Character number format in keyword: {keyword}");
                    return null;
                }

                // Face expression
                if (parts.Length == 2)
                {
                    // Example: "Character1|Angry" -> default action "Speaks" (if needed)
                    model.FaceExpression = MappingManager.MapFaceExpressions(parts[1]);
                }
                else if (parts.Length >= 3)
                {
                    // Example: "Character1|Speaks|Angry"
                    model.FaceExpression = MappingManager.MapFaceExpressions(parts[2]); // Action (parts[1]) is currently not used
                }
                else
                {
                    Debug.LogWarning($"Character keyword '{keyword}' does not contain enough parts for expression.");
                    return null; // At least one expression should be present
                }

                return model;
            }
            // B. Sound keyword
            else if (firstPart.StartsWith(SoundPrefix, StringComparison.OrdinalIgnoreCase))
            {
                if (parts.Length > 1)
                {
                    model.CharacterIndex = 0; // Sound events typically don't have a speaking character
                    model.Sound = parts[1];
                }
                else
                {
                    Debug.LogWarning($"Sound keyword '{keyword}' does not specify a sound name.");
                    return null;
                }

                return model;
            }
            // C. Bias keyword
            else if (firstPart.StartsWith(BiasPrefix, StringComparison.OrdinalIgnoreCase))
            {
                if (parts.Length > 1)
                {
                    model.Bias = MappingManager.MapBias(parts[1]);
                }
                else
                {
                    Debug.LogWarning($"Bias keyword '{keyword}' does not specify a bias name.");
                    return null;
                }

                return model;
            }

            // If none of the expected cases occur, null is returned.
            Debug.LogWarning($"Unknown keyword format: {keyword}");
            return null;
        }

        /// <summary>
        /// Parses the entire input text, which may contain multiple keyword lines,
        /// using the separator "&gt;&gt;--&lt;&lt;" and returns a list of valid <see cref="NovelKeywordModel"/> objects.
        /// Each line is split by the separator, and each resulting token is parsed as a keyword.
        /// </summary>
        /// <param name="fileContent">The entire text content from the keyword file, provided as a list of strings (lines).</param>
        /// <param name="kiteNovelMetaData">Optional novel metadata used for character mapping during parsing.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="NovelKeywordModel"/> objects representing the parsed keywords.</returns>
        public static List<NovelKeywordModel> ParseKeywordsFromFile(List<string> fileContent, KiteNovelMetaData kiteNovelMetaData = null)
        {
            List<NovelKeywordModel> models = new List<NovelKeywordModel>();

            // We assume that individual keyword blocks are separated by the ">>--<<" separator.
            foreach (string line in fileContent)
            {
                // If the separator is present in the line, this line is split into multiple tokens.
                string[] tokens = line.Split(new string[] { ">>--<<" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string token in tokens)
                {
                    string trimmedToken = token.Trim();
                    if (string.IsNullOrWhiteSpace(trimmedToken))
                        continue;

                    // Ensure that the keywords have the ">>" and "<<" markers.
                    if (!(trimmedToken.StartsWith(KeywordStartMarker) && trimmedToken.EndsWith(KeywordEndMarker)))
                    {
                        continue;
                    }

                    NovelKeywordModel model = ParseKeyword(trimmedToken, kiteNovelMetaData);
                    if (model != null)
                    {
                        models.Add(model);
                    }
                }
            }

            return models;
        }
    }
}