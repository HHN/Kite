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

        /// <summary>
        /// Liest eine Twee-Datei von der angegebenen Datei und gibt den Inhalt als String zurück.
        /// </summary>
        /// <param name="filePath">Pfad zur Twee-Datei</param>
        /// <returns>Inhalt der Twee-Datei als String</returns>
        public string ReadNovelTweeFile(string filePath)
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
                    string dynamicPattern = $@"Charakter{numberMatch}\w*:";
                    string pattern = $@"Charakter\d+\w*:";
                    // Debug.Log($"Mapping Pattern {dynamicPattern} to {value}");

                    if (!CharacterToSpeakerMap.ContainsKey(pattern))
                    {
                        CharacterToSpeakerMap[pattern] = value;
                    }
                }
            }

            foreach (var entry in CharacterToSpeakerMap)
            {
                Debug.Log($"Character: {entry.Key}, Speaker: {entry.Value}");
            }
        }

        public void ParseTweeFile(string tweeContent)
        {
            string nodePattern = @"::\s*([^\n\{\[\|]+).*?\n((?:.|\n)*?)(?=(::|$))";
            string linkPattern = @"\[\[(?:.*?(?:\||->))?([^\]]+)\]\]";
            // string speakerPattern = @">>Charakter\d+\w*:<<"; // Dynamisches Pattern für CharakterXX...
            string speakerPattern = @">>([^\s<>]+):<<"; // Fängt jeden Sprecher ein, der mit >> beginnt und :<< endet

            MatchCollection matches = Regex.Matches(tweeContent, nodePattern);

            foreach (Match match in matches)
            {
                string nodeName = match.Groups[1].Value.Trim();
                string nodeBody = match.Groups[2].Value;
                string speaker = "Unbekannt";

                // Finde den Sprechenden
                Match speakerMatch = Regex.Match(nodeBody, speakerPattern);
                if (speakerMatch.Success)
                {
                    string rawSpeaker = speakerMatch.Groups[0].Value.Trim();
                    Debug.Log("rawSpeaker: " + rawSpeaker);

                    string mappedSpeaker = null;
                    
                    // 1. Direkte Ersetzung für spezifische Muster wie InfoNachrichtWirdAngezeigt
                    if (rawSpeaker == ">>InfoNachrichtWirdAngezeigt:<<")
                    {
                        mappedSpeaker = "Info";
                    }
                    else if (rawSpeaker == ">>SpielerinCharakterSpricht:<<")
                    {
                        mappedSpeaker = "Spielerin";
                    }
                    else
                    {
                        // 2. Allgemeines Mapping basierend auf der CharacterToSpeakerMap
                        foreach (var pair in CharacterToSpeakerMap)
                        {
                            string regexPattern = pair.Key;
                            string mappedValue = pair.Value;

                            // Prüfe, ob der rawSpeaker mit dem regulären Ausdruck übereinstimmt
                            if (Regex.IsMatch(rawSpeaker, regexPattern))
                            {
                                mappedSpeaker = mappedValue;
                                break; // Sobald ein Match gefunden wurde, beenden
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(mappedSpeaker))
                    {
                        speaker = mappedSpeaker;
                    }
                    else
                    {
                        Debug.LogWarning($"Kein Mapping gefunden für: {rawSpeaker}");
                    }
                }

                List<string> links = new List<string>();

                // Suche nach Links
                MatchCollection linkMatches = Regex.Matches(nodeBody, linkPattern);
                foreach (Match linkMatch in linkMatches)
                {
                    string linkTarget = linkMatch.Groups[1].Value.Trim();
                    links.Add(linkTarget);
                }

                if (!Graph.ContainsKey(nodeName))
                {
                    Graph[nodeName] = (new List<string>(), nodeBody.Trim(), speaker);
                }

                Graph[nodeName].Links.AddRange(links);
            }

            if (!Graph.ContainsKey("Ende"))
            {
                Graph["Ende"] = (new List<string>(), string.Empty, string.Empty);
            }
        }

        public void PrintMetaData()
        {
            Debug.Log("Meta-Daten:");
            foreach (var meta in MetaData)
            {
                Debug.Log($"{meta.Key}: {meta.Value}");
            }
        }

        public List<List<string>> GetAllPaths(string startNode)
        {
            List<List<string>> allPaths = new List<List<string>>();
            List<string> currentPath = new List<string>();

            void DFS(string node)
            {
                // // Überprüfen, ob der Knoten bereits besucht wurde
                // if (visited.Contains(node))
                // {
                //     return; // Beende die Rekursion
                // }

                if (!Graph.ContainsKey(node))
                {
                    Debug.LogError($"Knoten {node} existiert nicht im Graphen!");
                    return;
                }

                // Füge den aktuellen Knoten zum Pfad hinzu und markiere ihn als besucht
                currentPath.Add(node);
                // visited.Add(node);

                var (links, body, speaker) = Graph[node];

                // Wenn Endknoten erreicht
                if (links.Count == 0 || node == "Ende")
                {
                    // Debug.Log($"Knoten: {node}, Inhalt: {body}");
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
                string purePath = "Pfad: ";

                for (int i = 0; i < path.Count; i++)
                {
                    string nodeName = path[i];
                    purePath += nodeName; // Füge den Knoten-Namen zum Pfad hinzu

                    if (i < path.Count - 1)
                    {
                        purePath += " -> "; // Pfeil zwischen den Knoten
                    }

                    if (Graph.ContainsKey(nodeName))
                    {
                        var (links, body, speaker) = Graph[nodeName];

                        // Bereinige den Body, um Metainformationen wie >>...<< und [[...]] zu entfernen
                        string cleanedBody = Regex.Replace(body, @">>.*?<<", "").Trim();
                        cleanedBody = Regex.Replace(cleanedBody, @"\[\[.*?\]\]", "").Trim();

                        // Füge den bereinigten Body hinzu, falls er nicht leer ist
                        if (!string.IsNullOrEmpty(cleanedBody))
                        {
                            conversation += $"{speaker}: {cleanedBody}\n\n";
                        }

                        // Extrahiere nur die tatsächlich gewählte Antwort für den nächsten Knoten
                        if (i < path.Count - 1)
                        {
                            string nextNode = path[i + 1];
                            string linkText = ExtractLinkText(body, nextNode);

                            if (!string.IsNullOrEmpty(linkText))
                            {
                                string nextSpeaker = "Unbekannt";
                                if (Graph.ContainsKey(nextNode) && !string.IsNullOrEmpty(Graph[nextNode].Speaker))
                                {
                                    nextSpeaker = Graph[nextNode].Speaker;
                                }

                                conversation += $"{nextSpeaker}: {linkText}\n\n";
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError($"Knoten {nodeName} wurde im Graph nicht gefunden!");
                    }
                }

                // Ausgabe des Gesprächs
                Debug.Log(conversation);

                // Ausgabe des reinen Pfads
                Debug.Log(purePath);
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