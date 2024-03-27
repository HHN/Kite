using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class TweeProcessor
{
    public static List<TweePassage> ProcessTweeFile(string fileContent)
    {
        List<TweePassage> passages = new List<TweePassage>();

        // Regex to identify passages
        Regex passageRegex = new Regex(@"::\s*(.*?)\n(.*?)\n?", RegexOptions.Singleline);
        Regex linkRegex = new Regex(@"\[\[(.*?)\]\]");

        foreach (Match passageMatch in passageRegex.Matches(fileContent))
        {
            TweePassage tweePassage = new TweePassage();
            string passageName = passageMatch.Groups[1].Value.Trim();
            string passageText = passageMatch.Groups[2].Value.Trim();
            string passageTextWithoutLinks = linkRegex.Replace(passageText, "");
            List<TweeLink> links = new List<TweeLink>();

            foreach (Match linkMatch in linkRegex.Matches(passageText))
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
                else
                {
                    linkText = fullLinkText;
                    linkTarget = fullLinkText;
                }

                link.text = linkText;
                link.target = linkTarget;

                links.Add(link);
                links.Add(link);
            }


            tweePassage.label = passageName;
            tweePassage.body = passageTextWithoutLinks;
            tweePassage.links = links;
            passages.Add(tweePassage);
        }

        return passages;
    }

    static void Check(string tweeText)
    {
        List<TweePassage> passages = ProcessTweeFile(tweeText);

        // Ergebnisse ausgeben
        foreach (TweePassage passage in passages)
        {
            Debug.Log("");
            Debug.Log("--- Passage Start ---");
            Debug.Log("Label: " + passage.label);
            Debug.Log("Body: " + passage.body);

            foreach (TweeLink link in passage.links)
            {
                Debug.Log("Link: " + link.text + " |->| " + link.target);
            }
            Debug.Log("--- Passage Ende ---");
            Debug.Log("");
        }
    }
}
