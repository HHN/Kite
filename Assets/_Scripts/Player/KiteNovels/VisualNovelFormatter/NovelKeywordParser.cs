using UnityEngine;
using System;

namespace Assets
{

        /// <summary>
        /// Parser, der einen Keyword-String in ein NovelKeywordModel überführt.
        /// Erwartetes Format: 
        ///   >>Ende<<
        ///   >>Info<<
        ///   >>Player<<
        ///   >>Character1|Looks|Angry<<
        ///   >>Sound|TestSound<<
        ///   >>Bias|BestätigungsBias<<
        /// </summary>
        /// 
    public static class NovelKeywordParser
    {
            public static NovelKeywordModel ParseKeyword(string keyword)
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    return null;

                // Entferne führende und abschließende Leerzeichen.
                keyword = keyword.Trim();

                // Entferne >> und <<, falls vorhanden.
                if (keyword.StartsWith(">>") && keyword.EndsWith("<<"))
                {
                    keyword = keyword.Substring(2, keyword.Length - 4);
                }

                NovelKeywordModel model = new NovelKeywordModel();

                // 1. Prüfe auf exakte Schlüsselwörter
                if (string.Equals(keyword, "Ende", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(keyword, "End", StringComparison.OrdinalIgnoreCase))
                {
                    model.End = true;
                    return model;
                }
                if (string.Equals(keyword, "Info", StringComparison.OrdinalIgnoreCase))
                {
                    model.CharacterIndex = 0;
                    return model;
                }
                if (string.Equals(keyword, "Player", StringComparison.OrdinalIgnoreCase))
                {
                    model.CharacterIndex = 1;
                    return model;
                }

                // 2. Teile den String anhand des Trennzeichens '|'
                string[] parts = keyword.Split('|');
                if (parts.Length > 0)
                {
                    // Falls das Keyword mit "Character" beginnt:
                    if (parts[0].StartsWith("Character", StringComparison.OrdinalIgnoreCase))
                    {
                        // parts[0] z. B. "Character1"
                        string numberPart = parts[0].Substring("Character".Length);
                        if (int.TryParse(numberPart, out int num))
                        {
                            // Wir addieren 1, da Info = 0 und Player = 1 vergeben wurden.
                            model.CharacterIndex = num + 1;
                        }
                        if (parts.Length > 1)
                        {
                            model.Action = parts[1];
                        }
                        if (parts.Length > 2)
                        {
                            model.FaceExpression = parts[2];
                        }
                        return model;
                    }
                    // Falls das Keyword mit "Sound" beginnt:
                    else if (parts[0].StartsWith("Sound", StringComparison.OrdinalIgnoreCase))
                    {
                        if (parts.Length > 1)
                        {
                            model.Sound = parts[1];
                        }
                        return model;
                    }
                    // Falls das Keyword mit "Bias" beginnt:
                    else if (parts[0].StartsWith("Bias", StringComparison.OrdinalIgnoreCase))
                    {
                        if (parts.Length > 1)
                        {
                            model.Bias = parts[1];
                        }
                        return model;
                    }
                }

                return model;
            }
        }

}
