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
    /// Parser that converts a keyword string (e.g. ">>Character1|Looks|Angry<<") into a NovelKeywordModel.
    /// Expected formats:
    ///   >>End<<                   → sets End = true
    ///   >>Info<<                  → sets CharacterIndex = 0
    ///   >>Player<<                → sets CharacterIndex = 1
    ///   >>Character1|Looks|Angry<< → sets CharacterIndex = 1+1 = 2, Action = "Looks", FaceExpression = "Angry"
    ///   >>Sound|TestSound<<        → sets Sound = "TestSound"
    ///   >>Bias|ConfirmationBias<<  → sets Bias = "ConfirmationBias"
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
        /// Parst einen einzelnen Keyword-String und gibt ein NovelKeywordModel zurück.
        /// Falls der String nicht den erwarteten Mustern entspricht, wird null zurückgegeben.
        /// </summary>
        /// <param name="keyword">Der zu parsenden Keyword-String (z.B. ">>Character1|Speaks|Angry<<").</param>
        /// <param name="kiteNovelMetaData">Metadaten der Novel, um Charakternamen zu mappen.</param>
        /// <returns>Ein NovelKeywordModel oder null, wenn kein gültiges Keyword erkannt wurde.</returns>
        private static NovelKeywordModel ParseKeyword(string keyword, KiteNovelMetaData kiteNovelMetaData)
        {
            Debug.Log("Parsing keyword: " + keyword + "...");
            // Frühzeitiger Exit bei ungültiger Eingabe
            if (string.IsNullOrWhiteSpace(keyword) ||
                !keyword.StartsWith(KeywordStartMarker, StringComparison.Ordinal) ||
                !keyword.EndsWith(KeywordEndMarker, StringComparison.Ordinal))
            {
                return null;
            }

            // Entferne die Marker ">>" und "<<".
            keyword = keyword.Substring(KeywordStartMarker.Length, keyword.Length - KeywordStartMarker.Length - KeywordEndMarker.Length).Trim(); // Trimmen auch hier, falls Leerzeichen zwischen Marker und Inhalt sind.
            string innerKeyword = keyword;

            NovelKeywordModel model = new NovelKeywordModel();

            // 1. End-Kommando
            if (string.Equals(innerKeyword, "End", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(innerKeyword, "Ende", StringComparison.OrdinalIgnoreCase))
            {
                model.End = true;
                return model;
            }

            // 2. Exakte Schlüsselwörter "Info" und "Player"
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

            // 3. Keywords mit Trennzeichen '|'
            string[] parts = innerKeyword.Split(new[] { PartSeparator }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0) // Sollte nach dem Trimmen und Split nicht passieren, aber zur Sicherheit
            {
                return null;
            }

            string firstPart = parts[0];
            // A. Character-Keyword
            if (firstPart.StartsWith(CharacterPrefix, StringComparison.OrdinalIgnoreCase))
            {
                string numberPart = firstPart.Substring(CharacterPrefix.Length);
                if (int.TryParse(numberPart, out int charNum))
                {
                    string characterName = null;
                    // Verbesserung: Array oder Dictionary für TalkingPartner Zugriff
                    // Statt if-else if Kette
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
                        return null; // Kein valider Charaktername gefunden
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid Character number format in keyword: {keyword}");
                    return null;
                }

                // Gesichtsausdruck
                if (parts.Length == 2)
                {
                    // Beispiel: "Character1|Angry" -> default action "Speaks" (wenn benötigt)
                    model.FaceExpression = MappingManager.MapFaceExpressions(parts[1]);
                }
                else if (parts.Length >= 3)
                {
                    // Beispiel: "Character1|Speaks|Angry"
                    model.FaceExpression = MappingManager.MapFaceExpressions(parts[2]); // Action (parts[1]) wird aktuell nicht verwendet
                }
                else
                {
                    Debug.LogWarning($"Character keyword '{keyword}' does not contain enough parts for expression.");
                    return null; // Mindestens ein Ausdruck sollte vorhanden sein
                }

                return model;
            }
            // B. Sound-Keyword
            else if (firstPart.StartsWith(SoundPrefix, StringComparison.OrdinalIgnoreCase))
            {
                if (parts.Length > 1)
                {
                    model.CharacterIndex = 0; // Sound-Events haben normalerweise keinen sprechenden Charakter
                    model.Sound = parts[1];
                }
                else
                {
                    Debug.LogWarning($"Sound keyword '{keyword}' does not specify a sound name.");
                    return null;
                }

                return model;
            }
            // C. Bias-Keyword
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

            // Wenn keiner der erwarteten Fälle eintritt, wird null zurückgegeben.
            Debug.LogWarning($"Unknown keyword format: {keyword}");
            return null;
        }

        /// <summary>
        /// Parst den kompletten Eingabetext, der mehrere Keyword-Zeilen enthalten kann, 
        /// unter Verwendung des Separators ">>--<<" und gibt eine Liste der gültigen NovelKeywordModel zurück.
        /// </summary>
        /// <param name="fileContent">Der gesamte Text aus der Keyword-Datei.</param>
        /// <param name="kiteNovelMetaData"></param>
        /// <returns>Liste der NovelKeywordModel.</returns>
        public static List<NovelKeywordModel> ParseKeywordsFromFile(List<string> fileContent, KiteNovelMetaData kiteNovelMetaData = null)
        {
            Debug.Log("Parsing keywords from file...");
            // string[] lines = fileContent.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            List<NovelKeywordModel> models = new List<NovelKeywordModel>();

            // Wir gehen davon aus, dass einzelne Keyword-Blöcke durch den Separator ">>--<<" getrennt sind.
            foreach (string line in fileContent)
            {
                // Falls der Separator in der Zeile vorkommt, wird diese Zeile in mehrere Tokens geteilt.
                string[] tokens = line.Split(new string[] { ">>--<<" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string token in tokens)
                {
                    string trimmedToken = token.Trim();
                    if (string.IsNullOrWhiteSpace(trimmedToken))
                        continue;

                    // Stelle sicher, dass die Keywords die Marker ">>" und "<<" haben.
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