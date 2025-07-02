using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets._Scripts.Test
{
    public class TweePathCalculator
    {
        //private readonly Dictionary<string, (List<string> Links, List<string> Speakers, string Body)> _graph = new Dictionary<string, (List<string> Links, List<string> Speakers, string Body)>();
        private readonly Dictionary<string,Node> _graph = new Dictionary<string,Node>();
        private readonly Dictionary<string, string> _characterToSpeakerMap = new Dictionary<string, string>();

        private string outputFile = "Assets/PathOutput.txt";

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
                    // Dynamisch alle möglichen `Charakter01...` zuordnen
                    string dynamicPattern = $@"Character{int.Parse(numberMatch)}";

                    if (!_characterToSpeakerMap.ContainsKey(dynamicPattern))
                    {
                        _characterToSpeakerMap[dynamicPattern] = value;
                    }
                }
            }

            // Statische Zuordnungen
            _characterToSpeakerMap[@"InfoNachrichtWirdAngezeigt"] = "Info";
            _characterToSpeakerMap[@"SpielerinCharakterSpricht"] = "Spielerin";

            // Ausgabe der gesamten CharacterToSpeakerMap
            Debug.Log("CharacterToSpeakerMap-Inhalt:");
            foreach (var entry in _characterToSpeakerMap)
            {
                Debug.Log($"  {entry.Key} -> {entry.Value}");
            }
        }

        public void ParseTweeFile(string tweeContent)
        {
            string nodePattern = @"::\s*([^\n\{\[\|]+).*?\n((?:.|\n)*?)(?=(::|$))";
            string linkPattern = @"\[\[(?:(.*?)(?:\s*(?:\||->)\s*(.*?))|([^|\]]+))\]\]";

            MatchCollection matches = Regex.Matches(tweeContent, nodePattern);

            foreach (Match match in matches)
            {
                string nodeName = match.Groups[1].Value.Trim();
                string nodeBody = match.Groups[2].Value;
                string dialogue = "";

                List<Link> links = new List<Link>();
                Dictionary<string,int> linkCount = new Dictionary<string,int>();
                List<(string Speaker, string Text)> conversations = new List<(string Speaker, string Text)>();

                //Speaker ersetzen
                foreach (KeyValuePair<string,string> speaker in _characterToSpeakerMap)
                {
                    if(speaker.Value != "")
                    {
                        nodeBody = nodeBody.Replace(speaker.Key, speaker.Value);
                    }
                }

               
                var speakerMatch = Regex.Match(nodeBody, @">>(.*?)\|.*?<<\s*(.*?)\s*>>--<<", RegexOptions.Singleline);
                if (speakerMatch.Success)
                {
                    string speaker = speakerMatch.Groups[1].Value.Trim();
                    string message = speakerMatch.Groups[2].Value.Trim();
                    dialogue = $"{speaker}: {message}\n";
                }

                // Extract bias
                var biasMatch = Regex.Match(nodeBody, @">>Bias\|(.*?)<<");
                if (biasMatch.Success)
                {
                    string bias = biasMatch.Groups[1].Value.Trim();
                    dialogue = dialogue + $"Bias: {bias}";
                }
                
                // Links extrahieren
                MatchCollection linkMatches = Regex.Matches(nodeBody, linkPattern);
                foreach (Match linkMatch in linkMatches)
                {
                    Link targetLink = new Link();
                    if (!string.IsNullOrEmpty(linkMatch.Groups[2].Value))
                    {
                        targetLink.targetNode = linkMatch.Groups[2].Value.Trim(); // Text nach | oder ->
                    }
                    else if (!string.IsNullOrEmpty(linkMatch.Groups[3].Value))
                    {
                        targetLink.targetNode = linkMatch.Groups[3].Value.Trim(); // Alleinstehender Text als Link
                    }

                    if (!string.IsNullOrEmpty(targetLink.targetNode))
                    {
                        targetLink.dialogueText = linkMatch.Groups[1].Value.Trim();
                        links.Add(targetLink);
                        if(linkCount.ContainsKey(targetLink.targetNode))
                        {
                            linkCount[targetLink.targetNode]++;
                        }
                        else
                        {
                            linkCount[targetLink.targetNode]=1;
                        }
                    }
                }

                nodeBody = Regex.Replace(nodeBody, @"\[\[.*?->.*?\]\]\s*", "", RegexOptions.Singleline);
                // Speichere Node im Graphen
                if (!_graph.ContainsKey(nodeName))
                {
                    Node newNode = new Node(nodeName, nodeBody, links);
                    _graph[nodeName] = newNode;
                    _graph[nodeName].linkCount = linkCount;
                    newNode.dialogue = dialogue;
                }
                else
                {
                    _graph[nodeName].links.AddRange(links);
                }
            }
                // Stelle sicher, dass "Ende" im Graph existiert
                if (!_graph.ContainsKey("Ende"))
            {
                _graph["Ende"] = new Node("Ende", "",new List<Link>());
            }
        }


        //Gibt alle Pfade zurück mit Antworten zu den Nodes
        public List<Dictionary<Node, Link>> GetAllPaths(string startNode)
        {
            Debug.Log("GetAllPaths gestartet");

            List<Dictionary<Node, Link>> allPaths = new List<Dictionary<Node, Link>>();
            Dictionary<Node, Link> currentPath = new Dictionary<Node, Link>();

            void DFS(Node node)
            {
                if (!_graph.ContainsKey(node.name))
                {
                    Debug.LogError($"Knoten {node} existiert nicht im Graphen!");
                    return;
                }

                // Füge den aktuellen Knoten zum Pfad hinzu

                List<Link> links = _graph[node.name].links;

                // Wenn Endknoten erreicht
                if (links.Count == 0 || node.name == "Ende")
                {
                    allPaths.Add(new Dictionary<Node, Link>(currentPath));
                }
                else
                {
                    foreach (Link neighbor in links)
                    {
                        currentPath[node] = neighbor;
                        DFS(_graph[neighbor.targetNode]);
                    }
                }

                //currentPath.RemoveAt(currentPath.Count - 1);
                currentPath.Remove(node);
            }

            DFS(_graph[startNode]);
            return allPaths;
        }

        //Reduziert Pfade nur auf Bias-Knoten und gibt einmalige Pfade zurück
        public List<Dictionary<Node,Link>> ReturnUniquePaths(List<Dictionary<Node, Link>> paths)
        {
            int pathCount = 1;
            List<Dictionary<Node, Link>> newPaths = new List<Dictionary<Node, Link>>();
            foreach(Dictionary<Node, Link> path in paths)
            {
                bool duplicate = false;
                Dictionary<Node, Link> newPath = new Dictionary<Node, Link>();
                foreach(KeyValuePair<Node,Link> decision in path)
                {
                    string nodeBody = decision.Key.body;
                    if(nodeBody.Contains(">>Bias|"))
                    {
                        newPath.Add(decision.Key,decision.Value);
                    }
                }
                foreach(Dictionary<Node, Link> listElement in newPaths)
                {
                    if(PathEqual(listElement, newPath)) //Checks if the path was documented before
                    {
                        duplicate = true;
                    }
                }
                if(!duplicate) 
                {
                    newPaths.Add(newPath);
                    WritePath(newPath);
                    WriteInFile("PATH " + pathCount + " FINISHED" + "\r\n", outputFile);
                    pathCount++;
                }
            }
            Debug.Log("Unique Paths created");
            Debug.Log($"Gefundene Pfade: {newPaths.Count}");
            
            foreach (Dictionary<Node,Link> path in newPaths)
            {
                Debug.Log("Path:");
                foreach(KeyValuePair<Node,Link> decision in path)
                {
                    Debug.Log("Node: " + decision.Key.name + "; Link: " + decision.Value.targetNode);
                }
            }
            return newPaths;
        }

        //Returns true if two paths are equal
        public bool PathEqual(Dictionary<Node, Link> firstPath, Dictionary<Node, Link> secondPath)
        {
            if(firstPath.Count != secondPath.Count)
            {
                return false;
            }
            foreach(KeyValuePair<Node,Link> decision in firstPath)
            {
                if(secondPath.TryGetValue(decision.Key,out Link value))
                {
                    if(!_graph[value.targetNode].body.Contains("End") && !value.Equals(decision.Value))
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        //Writes a path into a specified file
        public void WritePath(Dictionary<Node,Link> path)
        {
            foreach(KeyValuePair<Node,Link> node in path)
            {
                WriteInFile(node.Key.body.Trim() + "\r\n", outputFile);
                if (node.Value.dialogueText != string.Empty)
                {
                    WriteInFile("Spielerin: " + node.Value.dialogueText + "\r\n", outputFile);
                }
            }
        }


        //Simple method for file output
        public void WriteInFile(string output, string path)
        {
            if (!File.Exists(path))
            {
                var sr = File.CreateText(path);
                sr.WriteLine(output);
                sr.Close();
            }
            else
            {
                var sr = new StreamWriter(path, append:true);
                sr.WriteLine(output);
                sr.Close();
            }
        }

        public void PrintPathsAndSpeakers(List<Dictionary<Node,Link>> paths)
        {
            // Debug.Log($"Gefundene Pfade: {paths.Count}");
            //
            // foreach (Dictionary<Node,Link> path in paths)
            // {
            //     Debug.Log("Path:");
            //     foreach(KeyValuePair<Node,Link> decision in path)
            //     {
            //         Debug.Log("Node: " + decision.Key.name + "; Link: " + decision.Value.targetNode);
            //     }
            // }

            // foreach (var path in paths)
            // {
            //     string pathOutput = string.Join(" -> ", path);
            //     Debug.Log($"Pfad: {pathOutput}");
            //
            //     foreach (string node in path)
            //     {
            //         if (_graph.ContainsKey(node))
            //         {
            //             var (_, speakers, _) = _graph[node];
            //
            //             foreach (var speaker in speakers)
            //             {
            //                 Debug.Log($"Sprecher im Knoten '{node}': {speaker}");
            //             }
            //
            //             // Sprecher ausgeben
            //             if (speakers.Count > 0)
            //             {
            //                 string speakerList = string.Join(", ", speakers);
            //                 Debug.Log($"Knoten '{node}' hat folgende Sprecher: {speakerList}");
            //             }
            //             else
            //             {
            //                 Debug.Log($"Knoten '{node}' hat keine Sprecher.");
            //             }
            //         }
            //     }
            // }
        }

        // public void PrintPaths(List<List<string>> paths)
        // {
        //     Debug.Log($"Gefundene Pfade: {paths.Count}");
        //
        //     foreach (var path in paths)
        //     {
        //         string purePath = string.Join(" -> ", path);
        //         Debug.Log($"Pfad: {purePath}");
        //     }
        // }

        // --- Auskommentierter Code für spätere Verwendung ---

        /*
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

                        string[] lines = body.Split('\n');
                        foreach (var line in lines)
                        {
                            if (line.Trim().StartsWith(">>") || string.IsNullOrWhiteSpace(line))
                            {
                                continue;
                            }

                            int colonIndex = line.IndexOf(":");
                            if (colonIndex > -1)
                            {
                                string speaker = line.Substring(0, colonIndex).Trim();
                                string text = line.Substring(colonIndex + 1).Trim();

                                if (CharacterToSpeakerMap.TryGetValue(speaker, out string mappedSpeaker))
                                {
                                    speaker = mappedSpeaker;
                                }

                                conversation += $"{speaker}: {text}\n\n";
                            }
                            else
                            {
                                conversation += line.Trim() + "\n\n";
                            }
                        }
                    }
                }

                Debug.Log(conversation.Trim());
            }
        }
        */
    }

    public class Node
    {
        public string name;
        public string body;
        public string dialogue;
        public List<Link> links = new List<Link>();
        public Dictionary<string,int> linkCount = new Dictionary<string,int>();

        public Node(string _name, string _nodeBody, List<Link> _links)
        {
            name = _name;
            body = _nodeBody;
            links = _links;
        }
        public override bool Equals(object obj) //Needed for path comparison
        {
            if (obj is not Node other) return false;
            return name == other.name;
        }

        public override int GetHashCode() //Needed for path comparison
        {
            return HashCode.Combine(name); // Use .NET's built-in hash combiner
        }
    }
    public class Link
    {
        public string targetNode;
        public string dialogueText;
        public override bool Equals(object obj) //Needed for path comparison
        {
            if (obj is not Link other) return false;
            return targetNode == other.targetNode && dialogueText == other.dialogueText;
        }

        public override int GetHashCode() //Needed for path comparison
        {
            return HashCode.Combine(targetNode, dialogueText); // Use .NET's built-in hash combiner
        }
    }

}