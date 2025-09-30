using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Assets._Scripts.Novel;
using Assets._Scripts.Utilities;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets._Scripts.Managers
{
    
    [System.Serializable]
    public class KnowledgeItem
    {
        public string type;
        public string category;
        public string headline;
        public string bias;
    }
    
    [System.Serializable]
    public class KnowledgeBase
    {
        public string title;
        public List<KnowledgeItem> items;
    }
    
    /// <summary>
    /// Manages the process of building and handling dialog prompts for a visual novel.
    /// This class is implemented as a singleton to ensure a single instance throughout the application.
    /// </summary>
    public class PromptManager
    {
        private static PromptManager _instance;
        private StringBuilder _prompt;
        private StringBuilder _dialog;

        private readonly string _promptPath;

        /// <summary>
        /// Provides functionality to manage prompts and dialogs for a visual novel.
        /// Handles creation, formatting, storage, and retrieval of prompt and dialog text.
        /// Implements a singleton pattern to maintain a single instance across the application.
        /// </summary>
        private PromptManager()
        {
            _promptPath = Path.Combine(Application.streamingAssetsPath, "Prompt.txt");
        }

        /// <summary>
        /// Provides access to the single instance of the PromptManager class.
        /// Ensures that only one instance of PromptManager exists throughout the application's lifecycle,
        /// adhering to the singleton design pattern.
        /// </summary>
        /// <returns>The single instance of the PromptManager.</returns>
        public static PromptManager Instance()
        {
            if (_instance == null)
            {
                _instance = new PromptManager();
            }

            return _instance;
        }

        /// <summary>
        /// Retrieves the current prompt with the context replaced in the template if available.
        /// Returns an empty string if the prompt has not been initialized.
        /// </summary>
        /// <param name="context">The context string to replace within the prompt template.</param>
        /// <returns>A string representing the prompt with the context replaced, or an empty string if no prompt is defined.</returns>
        public string GetPrompt(string context)
        {
            if (_prompt == null)
            {
                return "";
            }

            string promptString = _prompt.ToString();

            return promptString.Replace("{{Context}}", context);
        }

        /// <summary>
        /// Retrieves the currently stored dialog as a string.
        /// Returns an empty string if there is no dialog stored.
        /// </summary>
        /// <returns>The dialog content as a string.</returns>
        public string GetDialog()
        {
            return _dialog == null ? "" : _dialog.ToString();
        }

        /// <summary>
        /// Initializes the prompt and dialog text for a given visual novel.
        /// Depending on the novel's ID, it configures the prompt with predefined text
        /// or loads the prompt and knowledge base from external files.
        /// </summary>
        /// <param name="novel">The visual novel object to use for initializing the prompt.</param>
        public void InitializePrompt(VisualNovel novel)
        {
            _prompt = new StringBuilder();
            _dialog = new StringBuilder();

            if ((int)novel.id == 14)
            {
                _prompt.Append(novel.context);
                _prompt.Append("Du bist eine Organisationsentwicklerin und Kommunikationstrainerin. " +
                               "Deine Aufgabe ist es, den folgenden Dialog dahingehend zu untersuchen, wie kommunikativ geschickt sich die KI-Einführungsperson verhalten hat. " +
                               "Es ist das Gespräch einer KI-Einführungsperson mit einem Mitarbeiter aus Vertrieb und Marketing. " +
                               "Schreibe einen Analysetext. Berücksichtige dabei Wissen aus den Bereichen der Einwandbehandlung, Gesprächstechniken, Kommunikationstechniken, Organisationsentwicklung etc. " +
                               "Analysiere das Verhalten der KI-Einführungsperson und ihre Reaktionen auf den Mitarbeiter mit Bezug zu konkreten Beispielen aus dem Dialog. " +
                               "Stelle dar, wo die KI-Einführungsperson geschickt agiert hat und wo eher nicht. Deute an, welche Vor- und Nachteile ihr Verhalten haben könnte. " +
                               "Nutze geschlechtergerechte Sprache (z.B. Gründer*innen, weibliche Gründerinnen). " +
                               "Richte den Text in der Du-Form an die KI-Einführungsperson. Sei wohlwollend und ermunternd. Formuliere den Text aus einer unbestimmten Ich-Perspektive.");
            }
            else
            {
                LoadPromptFromFile();
                LoadKnowledgeBaseFromFile();
            }

            _prompt.Append("Hier ist der Dialog:");
        }

        /// <summary>
        /// Reads the content of a prompt file from the predefined path and appends it
        /// to the current prompt using a specified callback for handling file content.
        /// This method ensures the proper integration of external prompt data into the existing system.
        /// </summary>
        private void LoadPromptFromFile()
        {
            LoadTextFile(_promptPath,
                content => { _prompt.Append(content).AppendLine(); },
                "Prompt");
        }

        /// <summary>
        /// Loads the knowledge base from a JSON file located in the Resources folder.
        /// Parses the JSON content into a KnowledgeBase object and appends its items to the prompt.
        /// Ensures the inclusion of details such as type, category, headline, and bias for each knowledge item.
        /// Logs an error message if the knowledge base file is not found.
        /// </summary>
        private void LoadKnowledgeBaseFromFile()
        {
            TextAsset json = Resources.Load<TextAsset>("KnowledgeBase");
            if (json != null)
            {
                KnowledgeBase kb = JsonUtility.FromJson<KnowledgeBase>(json.text);
                foreach (var item in kb.items)
                {
                    _prompt.AppendLine($"{item.type}");
                    _prompt.AppendLine($"{item.category}");
                    _prompt.AppendLine($"{item.headline}");
                    _prompt.AppendLine($"{item.bias}");
                    _prompt.AppendLine();
                }
            }
            else
            {
                LogManager.Error("KnowledgeBase not found in Resources!");
            }
        }

        /// <summary>
        /// Loads a text file from the specified path, processes its content, and triggers a callback function upon success.
        /// Provides error handling for different platforms and logs warnings when the file is not found.
        /// </summary>
        /// <param name="path">The file path to the text file to be loaded.</param>
        /// <param name="onSuccess">The action to perform with the file's content once successfully loaded.</param>
        /// <param name="fileLabel">A label used for logging purposes to identify the type of file being loaded.</param>
        private void LoadTextFile(string path, Action<string> onSuccess, string fileLabel)
        {
        #if UNITY_WEBGL
            UnityWebRequest request = UnityWebRequest.Get(path);
            request.SendWebRequest().completed += (op) =>
            {
                if (request.result == UnityWebRequest.Result.Success)
                {
                    onSuccess(request.downloadHandler.text);
                }
                else
                {
                    Application.ExternalCall("logMessage", $"Error loading {fileLabel}: {request.error}");
                }
            };
        #else
            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                onSuccess(text);
            }
            else
            {
                LogManager.Warning($"{fileLabel} file not found at: {path}");
            }
        #endif
        }

        /// <summary>
        /// Appends a new line of text to the current prompt and dialog builders.
        /// If the prompt or dialog builders are uninitialized, they are created before appending the line.
        /// </summary>
        /// <param name="line">The line of text to add to the prompt and dialog.</param>
        public void AddLineToPrompt(string line)
        {
            _prompt ??= new StringBuilder();
            _dialog ??= new StringBuilder();

            _prompt.AppendLine(line);
            _dialog.AppendLine(line);
        }

        /// <summary>
        /// Adds a formatted line to the current prompt, including the character's name and their dialogue text.
        /// The formatting distinguishes between regular character dialogue and hint messages by applying different styles.
        /// </summary>
        /// <param name="characterName">The name of the character or source of the dialogue. If it contains "Hinweis", a hint style is applied.</param>
        /// <param name="text">The dialogue or hint text to be added to the prompt.</param>
        public void AddFormattedLineToPrompt(string characterName, string text)
        {
            string formattedLine = characterName.Contains("Hinweis")
                ? $"<i><b>{characterName}:</b> {text}</i>"
                : $"<b>{characterName}:</b> {text}";
            AddLineToPrompt(formattedLine);
        }
    }
}