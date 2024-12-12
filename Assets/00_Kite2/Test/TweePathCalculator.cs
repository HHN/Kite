using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace _00_Kite2.Test
{
    public class TweePathCalculator
    {
        private Dictionary<string, (List<string> Links, string Body, string Speaker)> Graph =
            new Dictionary<string, (List<string> Links, string Body, string Speaker)>();

        private Dictionary<string, string> MetaData = new Dictionary<string, string>();
        private HashSet<string> Speakers = new HashSet<string>();
        private Dictionary<string, string> CharacterToSpeakerMap = new Dictionary<string, string>();

        /// <summary>
        /// Liest eine Twee-Datei von der angegebenen Datei und gibt den Inhalt als String zurück.
        /// </summary>
        /// <param name="filePath">Pfad zur Twee-Datei</param>
        /// <returns>Inhalt der Twee-Datei als String</returns>
        public string ReadTweeFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Die Datei wurde nicht gefunden: {filePath}");
            }

            try
            {
                // Dateiinhalt einlesen und als String zurückgeben
                return File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Fehler beim Lesen der Datei: {ex.Message}");
            }
        }

        public void ParseMetaTweeFile(string tweeContent)
        {
            string metaPattern = @"""(talkingPartner\d+)""\s*:\s*""([^""]*)""";

            MatchCollection matches = Regex.Matches(tweeContent, metaPattern);

            foreach (Match match in matches)
            {
                string key = match.Groups[1].Value.Trim(); // e.g., talkingPartner01
                string value = match.Groups[2].Value.Trim(); // e.g., Notarin

                string numberMatch = Regex.Match(key, @"\d+").Value; // Extrahiere die Nummer
                if (!string.IsNullOrEmpty(value))
                {
                    // Dynamisch alle möglichen `Charakter01Spricht...` zuordnen
                    string dynamicPattern = $@"Charakter{numberMatch}";

                    if (!CharacterToSpeakerMap.ContainsKey(dynamicPattern))
                    {
                        CharacterToSpeakerMap[dynamicPattern] = value;
                    }
                }
            }

            // Statische Zuordnungen
            CharacterToSpeakerMap[@"InfoNachrichtWirdAngezeigt"] = "Info";
            CharacterToSpeakerMap[@"SpielerinCharakterSpricht"] = "Spielerin";

            Debug.Log("CharacterToSpeakerMap Inhalt:");
            foreach (var entry in CharacterToSpeakerMap)
            {
                Debug.Log($"Key: {entry.Key}, Value: {entry.Value}");
            }
        }

        public void ParseTweeFile(string tweeContent)
        {
            string nodePattern = @"::\s*([^\n\{\[\|]+).*?\n((?:.|\n)*?)(?=(::|$))";
            // string linkPattern = @"\[\[(?:.*?(?:\||->))?([^\]]+)\]\]";
            // string linkPattern = @"\[\[(.*?)\s*(?:\||->)\s*(.*?)\]\]";
            // string linkPattern = @"\[\[(.*?)(?:\s*(\||->)\s*(.*?))?\]\]";
            // string linkPattern = @"\[\[(?:(.*?)(?:\s*(?:\||->)\s*(.*?))|([^|\]]+))\]\]";
            string linkPattern = @"\[\[(?:(.*?)(?:\s*(?:\||->)\s*(.*?))|([^|\]]+))\]\]";

            string speakerPattern = @">>([^\s<>]+):<<"; // Fängt jeden Sprecher ein, der mit >> beginnt und :<< endet

            MatchCollection matches = Regex.Matches(tweeContent, nodePattern);

            foreach (Match match in matches)
            {
                string nodeName = match.Groups[1].Value.Trim();
                string nodeBody = match.Groups[2].Value;

                List<string> links = new List<string>();
                List<(string Speaker, string Text)> conversations = new List<(string Speaker, string Text)>();

                // Sprecher extrahieren
                MatchCollection speakerMatches = Regex.Matches(nodeBody, speakerPattern);
                int lastIndex = 0;
                foreach (Match speakerMatch in speakerMatches)
                {
                    string speaker = speakerMatch.Groups[1].Value.Trim();
                    Debug.Log("speaker: " + speaker);
                    
                    // Prüfe, ob der Sprecher in der CharacterToSpeakerMap enthalten ist
                    foreach (var characterToSpeaker in CharacterToSpeakerMap)
                    {
                        Debug.Log("characterToSpeaker: " + characterToSpeaker.Key + " " + characterToSpeaker.Value);
                        if (speaker.Contains(characterToSpeaker.Key))
                        {
                            speaker = characterToSpeaker.Value; // Ersetze durch den gemappten Sprecher
                            Debug.Log("speaker new: " + speaker);
                        }
                        // else
                        // {
                        //     Debug.LogWarning($"Sprecher '{speaker}' nicht in CharacterToSpeakerMap gefunden!");
                        // }
                    }

                    int startIndex = speakerMatch.Index + speakerMatch.Length;
                    int endIndex = nodeBody.IndexOf(">>", startIndex);
                    endIndex = endIndex == -1 ? nodeBody.Length : endIndex;

                    string text = nodeBody.Substring(startIndex, endIndex - startIndex).Trim();
                    conversations.Add((speaker, text));
                    lastIndex = endIndex;
                }

                // Füge verbleibenden Text als Teil der letzten Sprecher-Phase hinzu
                if (lastIndex < nodeBody.Length)
                {
                    string remainingText = nodeBody.Substring(lastIndex).Trim();
                    if (!string.IsNullOrEmpty(remainingText))
                    {
                        conversations.Add(("Unbekannt", remainingText));
                    }
                }

                // Links und Gesprächsteile extrahieren
                MatchCollection linkMatches = Regex.Matches(nodeBody, linkPattern);
                foreach (Match linkMatch in linkMatches)
                {
                    string targetText = null;
                    string targetLink = null;

                    if (!string.IsNullOrEmpty(linkMatch.Groups[2].Value)) // Zieltext nach | oder ->
                    {
                        targetText = linkMatch.Groups[1].Value.Trim(); // Text vor | oder ->
                        targetLink = linkMatch.Groups[2].Value.Trim(); // Text nach | oder ->
                    }
                    else if (!string.IsNullOrEmpty(linkMatch.Groups[3].Value)) // Alleinstehender Text
                    {
                        targetLink = linkMatch.Groups[3].Value.Trim(); // Alleinstehender Text als Link
                        targetText = ""; // Kein separater Text, also dasselbe wie targetLink
                    }

                    Debug.Log($"Gefunden: targetText='{targetText}', targetLink='{targetLink}'");

                    // Füge den Gesprächsteil zur Konversation hinzu
                    if (targetText != null)
                    {
                        conversations.Add(("Unbekannt", targetText));
                        Debug.Log($"Gesprächsteil hinzugefügt: Speaker='Unbekannt', Text='{targetText}'");
                    }
                    
                    // Füge den Zielknoten als Link hinzu
                    if (!string.IsNullOrEmpty(targetLink))
                    {
                        links.Add(targetLink);
                    }
                }

                // Prüfe auf den >>Ende<<-Marker
                if (nodeBody.Contains(">>Ende<<") && !links.Contains("Ende"))
                {
                    links.Add("Ende");
                }

                if (!Graph.ContainsKey(nodeName))
                {
                    Graph[nodeName] = (links, string.Join("\n", conversations.Select(c => $"{c.Speaker}: {c.Text}")), "Unbekannt");
                    Debug.Log($"Node: {nodeName}, Conversations: {string.Join(", ", conversations.Select(c => $"{c.Speaker}: {c.Text}"))}");
                }
                else
                {
                    Graph[nodeName].Links.AddRange(links);
                }
            }

            // Stelle sicher, dass "Ende" im Graph existiert
            if (!Graph.ContainsKey("Ende"))
            {
                Graph["Ende"] = (new List<string>(), string.Empty, "Unbekannt");
            }
        }

        public List<List<string>> GetAllPaths(string startNode)
        {
            List<List<string>> allPaths = new List<List<string>>();
            List<string> currentPath = new List<string>();

            void DFS(string node)
            {
                if (!Graph.ContainsKey(node))
                {
                    Debug.LogError($"Knoten {node} existiert nicht im Graphen!");
                    return;
                }

                // Füge den aktuellen Knoten zum Pfad hinzu
                currentPath.Add(node);

                // var (links, body, speaker) = Graph[node];

                var (links, _, _) = Graph[node];

                // Wenn Endknoten erreicht
                if (links.Count == 0 || node == "Ende")
                {
                    allPaths.Add(new List<string>(currentPath));
                }
                else
                {
                    foreach (string neighbor in links)
                    {
                        DFS(neighbor);
                    }
                }

                currentPath.RemoveAt(currentPath.Count - 1);
            }

            DFS(startNode);
            return allPaths;
        }

        public void PrintPathsAsConversationAndPath(List<List<string>> paths)
        {
            Debug.Log($"Gefundene Pfade: {paths.Count}");

            foreach (var path in paths)
            {
                string conversation = "Gespräch:\n";

                foreach (string nodeName in path)
                {
                    if (Graph.ContainsKey(nodeName))
                    {
                        var (_, body, _) = Graph[nodeName];

                        // Verarbeitung des Node-Bodies für die Ausgabe
                        string[] lines = body.Split('\n');
                        foreach (var line in lines)
                        {
                            // Entferne Meta-Informationen wie >>--<<, Links und leere Zeilen
                            if (line.Trim().StartsWith(">>") || string.IsNullOrWhiteSpace(line))
                            {
                                continue;
                            }

                            // Extrahiere Sprecher und Text
                            int colonIndex = line.IndexOf(":");
                            if (colonIndex > -1)
                            {
                                string speaker = line.Substring(0, colonIndex).Trim();
                                string text = line.Substring(colonIndex + 1).Trim();

                                // Vereinheitliche Sprecher basierend auf Mapping in `CharacterToSpeakerMap`
                                if (CharacterToSpeakerMap.TryGetValue(speaker, out string mappedSpeaker))
                                {
                                    speaker = mappedSpeaker; // Ersetze mit dem Namen aus der Meta-Datei
                                }
                                else
                                {
                                    Debug.LogWarning($"Sprecher '{speaker}' nicht in CharacterToSpeakerMap gefunden!");
                                }

                                // Füge die bereinigte Zeile zur Konversation hinzu
                                conversation += $"{speaker}: {text}\n\n";
                            }
                            else
                            {
                                // Falls kein Sprecher erkennbar ist, füge die Zeile ohne Änderungen hinzu
                                conversation += line.Trim() + "\n\n";
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError($"Knoten {nodeName} wurde im Graph nicht gefunden!");
                    }
                }

                // Ausgabe der bereinigten Konversation
                Debug.Log(conversation.Trim());
            }
        }

        private string ExtractLinkText(string body, string targetNode)
        {
            string linkPattern = $@"\[\[(.*?)\s*(?:\||->)\s*{Regex.Escape(targetNode)}\]\]";
            Match match = Regex.Match(body, linkPattern);

            // Gib den gefundenen Linktext zurück oder den Zielknoten, wenn kein Text vorhanden ist
            return match.Success ? match.Groups[1].Value.Trim() : string.Empty;
        }
    }
}