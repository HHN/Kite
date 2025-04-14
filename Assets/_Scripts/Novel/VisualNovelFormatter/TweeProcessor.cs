using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Assets._Scripts.Player.KiteNovels.VisualNovelFormatter;
using Assets._Scripts.Player.KiteNovels.VisualNovelFormatter;
using UnityEngine;

namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    /// <summary>
    /// The TweeProcessor is responsible for processing a Twee formatted source.
    /// It splits the source into passages, extracts labels, links, and cleans up text for further processing.
    /// </summary>
    public abstract class TweeProcessor
    {
        // Regex to extract the StoryData block (everything between ":: StoryData" and the next "::") 
        // using multiline mode so that it matches across multiple lines.
        private static readonly Regex StoryDataRegex = new(@":: StoryData\s*\n([\s\S]*?)\n::", RegexOptions.Multiline);

        // Regex to match any text inside double square brackets, which are used for links.
        private static readonly Regex LinkRegex = new Regex(@"\[\[(.*?)\]\]");

        /// <summary>
        /// Processes the entire Twee source text and converts it into a list of TweePassage objects.
        /// It first splits the text into individual passages, extracts labels and links,
        /// and then reorders them so that the start passage is first.
        /// </summary>
        /// <param name="tweeSource">The raw Twee source text.</param>
        /// <returns>A list of TweePassage objects.</returns>
        public static List<TweePassage> ProcessTweeFile(string tweeSource)
        {
            // List that will store all passages after processing.
            List<TweePassage> listOfPassages = new List<TweePassage>();
            // Dictionary to store passages by their label (for later lookup).
            Dictionary<string, TweePassage> dictionaryOfPassages = new Dictionary<string, TweePassage>();

            // Split the full Twee text into individual passage strings.
            List<string> passages = SplitTweeIntoPassages(tweeSource);

            // Process each passage string.
            foreach (string passage in passages)
            {
                // Extract the label (name) of the passage.
                string label = GetLabelOfPassage(passage);
                // Skip passages without a valid label or with reserved labels "StoryTitle" or "StoryData".
                if (string.IsNullOrEmpty(label) || label == "StoryTitle" || label == "StoryData")
                {
                    continue;
                }

                // Extract any links that are present within the passage.
                List<TweeLink> links = GetTweeLinksOfPassage(passage);
                // Create a TweePassage object with the extracted label, full passage text, and its links.
                TweePassage tweePassage = new TweePassage(label, passage, links);
                // Store the passage in the dictionary for later reordering.
                dictionaryOfPassages[tweePassage.Label] = tweePassage;
            }

            // Get the label of the starting passage from the Twee file (from the StoryData block).
            string startLabel = GetStartLabelFromTweeFile(tweeSource);
            // If the starting label is not found in the dictionary, simply return the collected passages.
            if (!dictionaryOfPassages.ContainsKey(startLabel))
            {
                return listOfPassages;
            }

            // Retrieve the starting passage and remove it from the dictionary.
            TweePassage startObject = dictionaryOfPassages[startLabel];
            dictionaryOfPassages.Remove(startLabel);

            // Add the remaining passages to the list.
            listOfPassages.AddRange(dictionaryOfPassages.Values);
            // Insert the starting passage at the beginning of the list.
            listOfPassages.Insert(0, startObject);

            return listOfPassages;
        }

        /// <summary>
        /// Splits the Twee text into individual passage strings.
        /// The delimiter used is "\n::" because passages start with "::".
        /// After splitting, the missing "::" is added back to each passage (except the first, if needed).
        /// </summary>
        /// <param name="tweeText">The full Twee source text.</param>
        /// <returns>A list of passage strings.</returns>
        private static List<string> SplitTweeIntoPassages(string tweeText)
        {
            // Split the text at "\n::". We use None for StringSplitOptions to keep empty entries if any.
            List<string> passages = new List<string>(tweeText.Split(new[] { "\n::" }, StringSplitOptions.None));

            // For all passages starting from index 1, prepend "::" because the split removed it.
            for (int i = 1; i < passages.Count; i++)
            {
                passages[i] = "::" + passages[i];
            }

            // If the original text did not start with "::", ensure the first passage also starts with it.
            if (!tweeText.StartsWith("::") && passages.Count > 0)
            {
                passages[0] = "::" + passages[0];
            }

            return passages;
        }

        /// <summary>
        /// Extracts the label from a passage.
        /// The label is expected to be on the first line after "::", possibly with metadata following.
        /// It removes any extra information like metadata and formatting artifacts.
        /// </summary>
        /// <param name="passage">The passage text.</param>
        /// <returns>The cleaned label of the passage.</returns>
        private static string GetLabelOfPassage(string passage)
        {
            // A valid passage should start with "::"
            if (!passage.StartsWith("::"))
            {
                return "";
            }

            // Remove the initial "::"
            string passageContent = passage.Substring(2);
            // Find the index of the first newline, which marks the end of the label/metadata line.
            int contentStartIndex = passageContent.IndexOf('\n');

            // If there is no newline, return the entire content trimmed.
            if (contentStartIndex == -1)
            {
                return passageContent.Trim();
            }

            // Extract the first line that contains the label and potential metadata.
            string labelAndMetadata = passageContent.Substring(0, contentStartIndex).Trim();
            // Look for the beginning of metadata (which starts with " {")
            int labelEndIndex = labelAndMetadata.IndexOf(" {");

            // If no metadata is found, return the full line as the label.
            if (labelEndIndex == -1)
            {
                return labelAndMetadata;
            }

            // Otherwise, extract only the label part before the metadata.
            string label = labelAndMetadata.Substring(0, labelEndIndex).Trim();
            // Remove any leading backslash-space sequences.
            label = RemoveLeadingBackslashSpace(label);
            // Remove any text enclosed in square brackets.
            label = RemoveBracketedTextAndTrim(label);
            label = label.Trim();
            return label;
        }

        /// <summary>
        /// Removes any text enclosed in square brackets (e.g. "[...]" ) from the input and trims the result.
        /// </summary>
        private static string RemoveBracketedTextAndTrim(string input)
        {
            // Use a regex to remove any substring within square brackets.
            string withoutBrackets = Regex.Replace(input, @"\[.*?\]", "");
            string trimmedResult = withoutBrackets.Trim();
            return trimmedResult;
        }

        /// <summary>
        /// Removes a leading "\ " (backslash followed by a space) from the input string.
        /// </summary>
        private static string RemoveLeadingBackslashSpace(string input)
        {
            if (input.StartsWith("\\ "))
            {
                return input.Substring(2);
            }

            return input;
        }

        /// <summary>
        /// Extracts all links from a passage.
        /// Links are text enclosed in double square brackets, and may contain additional formatting (e.g. "text->target").
        /// </summary>
        /// <param name="passage">The passage text.</param>
        /// <returns>A list of TweeLink objects extracted from the passage.</returns>
        private static List<TweeLink> GetTweeLinksOfPassage(string passage)
        {
            List<TweeLink> links = new List<TweeLink>();

            // Use the LinkRegex to find all matches for text in double square brackets.
            foreach (Match linkMatch in LinkRegex.Matches(passage))
            {
                // Full text inside the brackets.
                string fullLinkText = linkMatch.Groups[1].Value;
                string linkText = fullLinkText;
                string linkTarget = fullLinkText;
                bool showAfterSelection = false;

                // If the link contains "->", split into text and target and mark it to be shown after selection.
                if (fullLinkText.Contains("->"))
                {
                    var linkParts = fullLinkText.Split(new[] { "->" }, StringSplitOptions.None);
                    linkText = linkParts[0].Trim();
                    linkTarget = linkParts[1].Trim();
                    showAfterSelection = true;
                }
                // Otherwise, if the link contains a pipe ("|"), use that to separate text and target.
                else if (fullLinkText.Contains("|"))
                {
                    var linkParts = fullLinkText.Split(new[] { "|" }, StringSplitOptions.None);
                    linkText = linkParts[0].Trim();
                    linkTarget = linkParts[1].Trim();
                }

                // Create a new TweeLink object and add it to the list.
                links.Add(new TweeLink(linkText, linkTarget, showAfterSelection));
            }

            return links;
        }

        /// <summary>
        /// Extracts the starting label from the Twee file by looking into the StoryData block.
        /// The StoryData block is expected to contain a JSON snippet from which the start label is read.
        /// </summary>
        /// <param name="tweeFileContent">The full Twee file content.</param>
        /// <returns>The start label defined in the StoryData, or null if not found.</returns>
        public static string GetStartLabelFromTweeFile(string tweeFileContent)
        {
            // Use the StoryDataRegex to extract the JSON content within the StoryData block.
            Match match = StoryDataRegex.Match(tweeFileContent);

            if (!match.Success) return null;

            string jsonContent = match.Groups[1].Value;

            try
            {
                // Deserialize the JSON into a StoryDataPassage object.
                StoryDataPassage storyData = JsonConvert.DeserializeObject<StoryDataPassage>(jsonContent);
                // Return the "Start" property from the StoryData.
                return storyData?.Start;
            }
            catch
            {
                // If deserialization fails, return null.
                return null;
            }
        }

        /// <summary>
        /// Extracts the message text from a Twee passage by removing titles, links, metadata, and various formatting symbols.
        /// This cleaned-up text is used later for further processing.
        /// </summary>
        /// <param name="text">The full passage text.</param>
        /// <returns>The cleaned message text.</returns>
        public static string ExtractMessageOutOfTweePassage(string text)
        {
            // Remove the title from the passage.
            text = RemoveTitleFromPassage(text);
            // Remove any text enclosed in double square brackets.
            text = RemoveTextInDoubleBrackets(text);
            // Remove any text enclosed in curly braces.
            text = RemoveTextInCurlyBraces(text);
            // Note: Removing text in parentheses is disabled because it is needed (e.g., "(bga)").
            // Remove text enclosed in double angle brackets (<< >>).
            text = RemoveTextInDoubleAngleBrackets(text);
            // Remove text in the opposite angle bracket order (>> <<).
            text = RemoveTextInDoubleAngleBracketsOtherDirection(text);
            // Remove any remaining square brackets.
            text = RemoveSquareBrackets(text);
            // Remove known keywords from the text.
            text = RemoveKeyWords(text);
            // Normalize extra spaces to a single space.
            text = NormalizeSpaces(text);
            return text.Trim();
        }

        /// <summary>
        /// Extracts the keyword from a Twee passage.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>The cleaned keyword.</returns>
        public static List<string> ExtractKeywordOutOfTweePassage(string text)
        {
            Debug.Log($"string in ExtractKeywordOutOfTweePassage: {text}");
            
            var result = new List<string>();
            
            if (string.IsNullOrEmpty(text)) return result;

            var matches = Regex.Matches(text, @"\>\>.*?\<\<");
            
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    result.Add(match.Value.Trim());
                }
            }

            return result;
        }

        /// <summary>
        /// Removes all substrings enclosed between ">>" and "<<"
        /// from the input string. This replaces the previous approach that used
        /// NovelKeyWordValue.ALL_KEY_WORDS.
        /// </summary>
        private static string RemoveKeyWords(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }

            // Pattern explanation:
            // >>      : matches the literal ">>"
            // .*?     : matches any characters (non-greedy)
            // <<      : matches the literal "<<"
            string pattern = @">>.*?<<";

            // Replace all occurrences with an empty string.
            string result = Regex.Replace(input, pattern, "");
            return result;
        }


        /// <summary>
        /// Removes the title line (starting with ":: ") from the passage.
        /// </summary>
        private static string RemoveTitleFromPassage(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }

            // Regex pattern to match the title at the beginning of the passage.
            var titlePattern = @"^\:\: [^\n]+";
            // Replace the matched title with an empty string and trim the result.
            var result = Regex.Replace(input, titlePattern, "").Trim();
            return result;
        }

        /// <summary>
        /// Removes text within double square brackets (e.g., [[...]]), which are used for links.
        /// </summary>
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

        /// <summary>
        /// Removes text within curly braces (e.g., {...}).
        /// </summary>
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

        /// <summary>
        /// Removes text within parentheses (e.g., (�)).
        /// Currently not used because text in parentheses might be needed.
        /// </summary>
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

        /// <summary>
        /// Removes text within double angle brackets (e.g., <<...>>).
        /// </summary>
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

        /// <summary>
        /// Removes text in the reverse double angle bracket order (>>...<<).
        /// </summary>
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

        /// <summary>
        /// Removes all square brackets from the input.
        /// </summary>
        private static string RemoveSquareBrackets(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }

            var result = input.Replace("[", "").Replace("]", "");
            return result;
        }

        /// <summary>
        /// Normalizes whitespace by replacing multiple spaces with a single space.
        /// </summary>
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
}