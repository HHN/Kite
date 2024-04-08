using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class TweeProcessor
{
    public static List<TweePassage> ProcessTweeFile(string tweeSource)
    {
        List<TweePassage> listOfPassages = new List<TweePassage>();
        Dictionary<string, TweePassage> dictionaryOfPassages = new Dictionary<string, TweePassage>();

        List<string> passages = SplitTweeIntoPassages(tweeSource);

        foreach (string passage in passages)
        {
            string label = GetLabelOfPassage(passage);
            List<TweeLink> links = GetTweeLinksOfPassage(passage);
            
            if ((string.IsNullOrEmpty(label)) || (label == "StoryTitle") || (label == "StoryData")) {
                continue;
            }

            TweePassage tweePassage = new TweePassage();
            tweePassage.label = label;
            tweePassage.passage = passage;
            tweePassage.links = links;
            dictionaryOfPassages[tweePassage.label] = tweePassage;
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
        string[] splitText = tweeText.StartsWith("::") ? tweeText.Substring(2).Split(new[] { "\n::" }, StringSplitOptions.None) : tweeText.Split(new[] { "\n::" }, StringSplitOptions.None);

        List<string> passages = new List<string>();

        foreach (var passage in splitText)
        {
            string formattedPassage = tweeText.StartsWith("::") || passages.Count > 0 ? "::" + passage : passage;
            passages.Add(formattedPassage);
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
        Regex linkRegex = new Regex(@"\[\[(.*?)\]\]");

        foreach (Match linkMatch in linkRegex.Matches(passage))
        {
            TweeLink link = new TweeLink();
            string fullLinkText = linkMatch.Groups[1].Value;
            string linkText;
            string linkTarget;

            if (fullLinkText.Contains("->"))
            {
                var linkParts = fullLinkText.Split(new[] { "->" }, StringSplitOptions.None);
                linkText = linkParts[0].Trim();
                linkTarget = linkParts[1].Trim();
            }
            else if (fullLinkText.Contains("|"))
            {
                var linkParts = fullLinkText.Split(new[] { "|" }, StringSplitOptions.None);
                linkText = linkParts[0].Trim();
                linkTarget = linkParts[1].Trim();
            }
            else
            {
                linkText = fullLinkText;
                linkTarget = fullLinkText;
            }

            link.text = linkText;
            link.target = linkTarget;

            links.Add(link);
        }

        return links;
    }

    public static string GetStartLabelFromTweeFile(string tweeFileContent)
    {
        Regex storyDataRegex = new Regex(@":: StoryData\s*\n([\s\S]*?)\n::", RegexOptions.Multiline);

        Match match = storyDataRegex.Match(tweeFileContent);
        if (match.Success)
        {
            string jsonContent = match.Groups[1].Value;

            try
            {
                var storyData = JsonConvert.DeserializeObject<StoryData>(jsonContent);
                return storyData.start;
            }
            catch
            {
                return "";
            }
        }
        return "";
    }
}
class StoryData
{
    public string start { get; set; }
}