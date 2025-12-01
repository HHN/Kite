using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

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
        private static readonly Regex StoryDataRegex =
            new(@":: StoryData\s*[\r\n]+([\s\S]*?)[\r\n]+::", RegexOptions.Multiline);

        // Regex to match any text inside double square brackets, which are used for links.
        private static readonly Regex LinkRegex = new(@"\[\[(.*?)\]\]");

        /// <summary>
        /// Processes the entire Twee source text and converts it into a list of TweePassage objects.
        /// It first splits the text into individual passages, extracts labels and links,
        /// and then reorders them so that the start passage is first.
        /// </summary>
        /// <param name="tweeSource">The raw Twee source text.</param>
        /// <returns>A list of TweePassage objects.</returns>
        public static List<TweePassage> ProcessTweeFile(string tweeSource)
        {
            var listOfPassages = new List<TweePassage>();
            var dictionaryOfPassages = new Dictionary<string, TweePassage>();

            if (string.IsNullOrWhiteSpace(tweeSource))
                return listOfPassages;

            // Split full Twee text into individual passages
            List<string> passages = SplitTweeIntoPassages(tweeSource);

            foreach (string passage in passages)
            {
                string label = GetLabelOfPassage(passage);

                // Skip passages without label or reserved labels
                if (string.IsNullOrEmpty(label) ||
                    string.Equals(label, "StoryTitle", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(label, "StoryData", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                List<TweeLink> links = GetTweeLinksOfPassage(passage);
                var tweePassage = new TweePassage(label, passage, links);

                // Overwrite if the same label appears again – last one wins
                dictionaryOfPassages[label] = tweePassage;
            }

            if (dictionaryOfPassages.Count == 0)
                return listOfPassages;

            string startLabel = GetStartLabelFromTweeFile(tweeSource);

            if (!string.IsNullOrEmpty(startLabel) &&
                dictionaryOfPassages.TryGetValue(startLabel, out TweePassage startObject))
            {
                // Put the start passage first
                listOfPassages.Add(startObject);

                // Add all other passages in any order except the start passage
                foreach (var kvp in dictionaryOfPassages)
                {
                    if (!ReferenceEquals(kvp.Value, startObject))
                    {
                        listOfPassages.Add(kvp.Value);
                    }
                }
            }
            else
            {
                // No valid start label found – just return everything
                listOfPassages.AddRange(dictionaryOfPassages.Values);
            }

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
            // Split the text at "\n::". We use None for StringSplitOptions to keep empty entries, if any.
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
            if (string.IsNullOrWhiteSpace(passage))
                return string.Empty;

            // A valid passage should start with "::"
            if (!passage.StartsWith("::"))
                return string.Empty;

            // Remove the initial "::"
            string passageContent = passage.Substring(2);

            // Take only the first line (label + optional tags/metadata)
            int contentStartIndex = passageContent.IndexOf('\n');
            string labelLine = contentStartIndex == -1
                ? passageContent
                : passageContent.Substring(0, contentStartIndex);

            labelLine = labelLine.Trim();

            if (labelLine.Length == 0)
                return string.Empty;

            // Cut off everything after the first '{' (metadata block),
            // whether there is a space before it or not.
            int braceIndex = labelLine.IndexOf('{');
            if (braceIndex >= 0)
            {
                labelLine = labelLine.Substring(0, braceIndex).TrimEnd();
            }

            // Remove any [Tag] blocks (e.g. [Bias])
            labelLine = RemoveBracketedTextAndTrim(labelLine);

            // Remove optional leading "\ "
            labelLine = RemoveLeadingBackslashSpace(labelLine);

            return labelLine;
        }


        /// <summary>
        /// Removes any text enclosed in square brackets (e.g. "[...]") from the input and trims the result.
        /// </summary>
        /// <param name="input">The input text to process</param>
        /// <returns>The processed string with bracketed text removed and trimmed</returns>
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
        /// <param name="input">The input string to process</param>
        /// <returns>The string with leading backslash-space removed if present, otherwise the original string</returns>
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
        /// Links are text enclosed in double square brackets and may contain additional formatting (e.g. "text->target").
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
        public static List<string> ExtractMessageOutOfTweePassage(string text)
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
            
            return text.Trim().Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        /// <summary>
        /// Extracts the keyword from a Twee passage.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>The cleaned keyword.</returns>
        public static List<string> ExtractKeywordOutOfTweePassage(string text)
        {
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
        /// <param name="input">The input string to process</param>
        /// <returns>The string with keywords removed</returns>
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
        /// <param name="input">The passage text to process</param>
        /// <returns>The passage text with title removed</returns>
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
        /// <param name="input">The input text to process.</param>
        /// <returns>The text with double square brackets removed.</returns>
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
        /// <param name="input">The input string to process</param>
        /// <returns>The string with curly braces content removed</returns>
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
        /// Removes text within double angle brackets (e.g., <<...>>).
        /// </summary>
        /// <param name="input">The input text to process</param>
        /// <returns>The text with double angle brackets content removed</returns>
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
        /// <param name="input">The input text to process</param>
        /// <returns>The text with double angle brackets content removed</returns>
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
        /// <param name="input">The input string to process</param> 
        /// <returns>The string with square brackets removed</returns>
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
        /// <param name="input">The input string to normalize</param>
        /// <returns>The normalized string</returns>
        private static string NormalizeSpaces(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // 1) Unify all types of line breaks (CRLF, CR, LF) to '\n'
            //    and reduce any sequence of one or more line breaks to exactly one '\n'
            string collapsedLines = Regex.Replace(input, @"(\r\n|\r|\n)+", "\n");

            // 2) Reduce any sequence of spaces or tabs to exactly one single space
            string collapsedSpaces = Regex.Replace(collapsedLines, @"[ \t]+", " ");

            return collapsedSpaces;
        }
    }
}