using System;
using System.IO;
using System.Text;
using Assets._Scripts.Novel;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets._Scripts.Managers
{
    public class PromptManager
    {
        private static PromptManager _instance;
        private StringBuilder _prompt;
        private StringBuilder _dialog;

        private readonly string _promptPath;
        private readonly string _knowledgeBasePath;

        private PromptManager()
        {
            _promptPath = Path.Combine(Application.streamingAssetsPath, "Prompt.txt");
            _knowledgeBasePath = Path.Combine(Application.streamingAssetsPath, "KnowledgeBase.txt");
        }

        public static PromptManager Instance()
        {
            if (_instance == null)
            {
                _instance = new PromptManager();
            }

            return _instance;
        }

        public string GetPrompt(string context)
        {
            if (_prompt == null)
            {
                return "";
            }

            string promptString = _prompt.ToString();

            return promptString.Replace("{{Context}}", context);
        }

        public string GetDialog()
        {
            return _dialog == null ? "" : _dialog.ToString();
        }

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

        private void LoadPromptFromFile()
        {
            LoadTextFile(_promptPath, 
                content =>
                {
                    _prompt.Append(content).AppendLine();
                }, 
                "Prompt");
        }

        private void LoadKnowledgeBaseFromFile()
        {
            LoadTextFile(_knowledgeBasePath, 
                content =>
                {
                    _prompt.Append(content).AppendLine();
                },
                "Knowledge base");
        }
        
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
        Debug.LogWarning($"{fileLabel} file not found at: {path}");
    }
#endif
        }


        public void AddLineToPrompt(string line)
        {
            if (_prompt == null)
            {
                _prompt = new StringBuilder();
            }

            if (_dialog == null)
            {
                _dialog = new StringBuilder();
            }

            _prompt.AppendLine(line);
            _dialog.AppendLine(line);
        }

        public void AddFormattedLineToPrompt(string characterName, string text)
        {
            string formattedLine = characterName.Contains("Hinweis")
                ? $"<i><b>{characterName}:</b> {text}</i>"
                : $"<b>{characterName}:</b> {text}";
            AddLineToPrompt(formattedLine);
        }
    }
}