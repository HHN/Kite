using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class TweeProcessor
{
    private static Regex storyDataRegex = new Regex(@":: StoryData\s*\n([\s\S]*?)\n::", RegexOptions.Multiline);
    private static Regex linkRegex = new Regex(@"\[\[(.*?)\]\]");

    public static List<TweePassage> ProcessTweeFile(string tweeSource)
    {
        List<TweePassage> listOfPassages = new List<TweePassage>();
        Dictionary<string, TweePassage> dictionaryOfPassages = new Dictionary<string, TweePassage>();

        List<string> passages = SplitTweeIntoPassages(tweeSource);

        foreach (string passage in passages)
        {
            string label = GetLabelOfPassage(passage);
            if (string.IsNullOrEmpty(label) || label == "StoryTitle" || label == "StoryData")
            {
                continue;
            }

            List<TweeLink> links = GetTweeLinksOfPassage(passage);
            TweePassage tweePassage = new TweePassage(label, passage, links);
            dictionaryOfPassages[tweePassage.Label] = tweePassage;
        }

        string startLabel = GetStartLabelFromTweeFile(tweeSource);
        if (!dictionaryOfPassages.ContainsKey(startLabel))
        {
            return listOfPassages;
        }

        TweePassage startObject = dictionaryOfPassages[startLabel];
        dictionaryOfPassages.Remove(startLabel);

        listOfPassages.AddRange(dictionaryOfPassages.Values);
        listOfPassages.Insert(0, startObject);

        return listOfPassages;
    }

    private static List<string> SplitTweeIntoPassages(string tweeText)
    {
        List<string> passages = new List<string>(tweeText.Split(new[] { "\n::" }, StringSplitOptions.None));

        for (int i = 1; i < passages.Count; i++)
        {
            passages[i] = "::" + passages[i];
        }

        if (!tweeText.StartsWith("::") && passages.Count > 0)
        {
            passages[0] = "::" + passages[0];
        }

        return passages;
    }


    private static string GetLabelOfPassage(string passage)
    {
        if (!passage.StartsWith("::"))
        {
            return "";
        }
        string passageContent = passage.Substring(2);
        int contentStartIndex = passageContent.IndexOf('\n');

        if (contentStartIndex == -1)
        {
            return passageContent.Trim();
        }
        string labelAndMetadata = passageContent.Substring(0, contentStartIndex).Trim();
        int labelEndIndex = labelAndMetadata.IndexOf(" {");

        if (labelEndIndex == -1)
        {
            return labelAndMetadata;
        }
        string label = labelAndMetadata.Substring(0, labelEndIndex).Trim();
        label = RemoveLeadingBackslashSpace(label);
        label = label.Trim();
        return label;
    }

    private static string RemoveLeadingBackslashSpace(string input)
    {
        if (input.StartsWith("\\ "))
        {
            return input.Substring(2);
        }
        return input;
    }

    private static List<TweeLink> GetTweeLinksOfPassage(string passage)
    {
        List<TweeLink> links = new List<TweeLink>();

        foreach (Match linkMatch in linkRegex.Matches(passage))
        {
            string fullLinkText = linkMatch.Groups[1].Value;
            string linkText = fullLinkText;
            string linkTarget = fullLinkText;
            bool showAfterSelection = false;

            if (fullLinkText.Contains("->"))
            {
                var linkParts = fullLinkText.Split(new[] { "->" }, StringSplitOptions.None);
                linkText = linkParts[0].Trim();
                linkTarget = linkParts[1].Trim();
                showAfterSelection = true;
            }
            else if (fullLinkText.Contains("|"))
            {
                var linkParts = fullLinkText.Split(new[] { "|" }, StringSplitOptions.None);
                linkText = linkParts[0].Trim();
                linkTarget = linkParts[1].Trim();
            }
            links.Add(new TweeLink(linkText, linkTarget, showAfterSelection));
        }
        return links;
    }

    public static string GetStartLabelFromTweeFile(string tweeFileContent)
    {
        Match match = storyDataRegex.Match(tweeFileContent);

        if (!match.Success)
        {
            return null;
        }

        string jsonContent = match.Groups[1].Value;

        try
        {
            StoryDataPassage storyData = JsonConvert.DeserializeObject<StoryDataPassage>(jsonContent);
            return storyData?.Start;
        }
        catch
        {
            return null;
        }
    }

    public static string ExtractMessageOutOfTweePassage(string text)
    {
        text = RemoveTitleFromPassage(text);
        text = RemoveTextInDoubleBrackets(text);
        text = RemoveTextInCurlyBraces(text);
        text = RemoveTextInParentheses(text);
        text = RemoveTextInDoubleAngleBrackets(text);
        text = RemoveTextInDoubleAngleBracketsOtherDirection(text);
        text = RemoveSquareBrackets(text);
        text = RemoveKeyWords(text);
        text = NormalizeSpaces(text);
        return text.Trim();
    }

    private static string RemoveKeyWords(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }
        foreach (string keyWord in NovelKeyWordValue.ALL_KEY_WORDS)
        {
            input = input.Replace(keyWord, "");
        }
        return input;
    }

    private static string RemoveTitleFromPassage(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }
        var titlePattern = @"^\:\: [^\n]+";

        var result = Regex.Replace(input, titlePattern, "").Trim();

        return result;
    }

    private static string RemoveTextInDoubleBrackets(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }
        var pattern = @"\[\[(.*?)\]\]";

        var result = Regex.Replace(input, pattern, "");

        return result;
    }

    private static string RemoveTextInCurlyBraces(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }
        var pattern = @"\{(.*?)\}";

        var result = Regex.Replace(input, pattern, "");

        return result;
    }
    private static string RemoveTextInParentheses(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }

        var pattern = @"\([^\)]*\)";

        var result = Regex.Replace(input, pattern, "");

        return result;
    }

    private static string RemoveTextInDoubleAngleBrackets(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }

        var pattern = @"\<\<.*?\>\>";

        var result = Regex.Replace(input, pattern, "");

        return result;
    }

    private static string RemoveTextInDoubleAngleBracketsOtherDirection(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }

        var pattern = @"\>\>.*?\<\<";

        var result = Regex.Replace(input, pattern, "");

        return result;
    }

    private static string RemoveSquareBrackets(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }
        var result = input.Replace("[", "").Replace("]", "");
        return result;
    }

    private static string NormalizeSpaces(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }
        var pattern = @"\s+";

        var result = Regex.Replace(input, pattern, " ");

        return result;
    }
}