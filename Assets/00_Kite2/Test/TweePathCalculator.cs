using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace _00_Kite2.Test
{
    public class TweePathCalculator
    {
        private Dictionary<string, List<string>> Graph = new Dictionary<string, List<string>>();
    
        /// <summary>
        /// Liest eine Twee-Datei von der angegebenen Datei und gibt den Inhalt als String zurück.
        /// </summary>
        /// <param name="filePath">Pfad zur Twee-Datei</param>
        /// <returns>Inhalt der Twee-Datei als String</returns>
        public string ReadTweeFile(string filePath)
        {
            Debug.Log("Lade Datei: " + filePath);
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

        public void ParseTweeFile(string tweeContent)
        {
            Debug.Log("Analysiere Datei");

            // Regex für Knoten und Verbindungen
            // string nodePattern = @"::\s*([^\[\n\{]+).*?((.|\n)*?)\n(?=(::|$))";
            string nodePattern = @"::\s*([^\n\{\[\|]+).*?\n((?:.|\n)*?)(?=(::|$))";
            string linkPattern = @"\[\[(?:.*?(?:\||->))?([^\]]+)\]\]";
            string endPattern = @">>\s*Ende\s*<<";
            
            MatchCollection matches = Regex.Matches(tweeContent, nodePattern);

            foreach (Match match in matches)
            {
                string nodeName = match.Groups[1].Value.Trim(); // Knoten-Name
                string nodeBody = match.Groups[2].Value;
                
                // Debug.Log($"Parsing Node: {nodeName}");

                List<string> links = new List<string>();

                // Suche nach Verbindungen (Links)
                MatchCollection linkMatches = Regex.Matches(nodeBody, linkPattern);
                foreach (Match linkMatch in linkMatches)
                {
                    string linkTarget = linkMatch.Groups[1].Value.Trim(); // Zielknoten
                    // Debug.Log($"Gefundener Link: {nodeName} -> {linkTarget}");
                    links.Add(linkTarget);
                }

                // Prüfe, ob `>>Ende<<` vorhanden ist
                if (Regex.IsMatch(nodeBody, endPattern))
                {
                    // Debug.Log($"Gefundener Endpunkt: {nodeName} enthält >>Ende<<");
                    links.Add("Ende");
                }

                // Verbindungen zum Graph hinzufügen
                if (!Graph.ContainsKey(nodeName))
                {
                    Graph[nodeName] = new List<string>();
                }

                Graph[nodeName].AddRange(links);
            }

            // Stelle sicher, dass "Ende" im Graph existiert
            if (!Graph.ContainsKey("Ende"))
            {
                Graph["Ende"] = new List<string>();
            }
        }
    
        public List<List<string>> GetAllPaths(string startNode)
        {
            List<List<string>> allPaths = new List<List<string>>();
            List<string> currentPath = new List<string>();
            HashSet<string> visited = new HashSet<string>();

            void DFS(string node)
            {
                // Überprüfen, ob der Knoten bereits besucht wurde
                if (visited.Contains(node))
                {
                    return; // Beende die Rekursion
                }
                
                // Füge den aktuellen Knoten zum Pfad hinzu und markiere ihn als besucht
                currentPath.Add(node);
                visited.Add(node);

                if (!Graph.ContainsKey(node) || Graph[node].Count == 0 || node == "Ende")
                {
                    allPaths.Add(new List<string>(currentPath));
                }
                else
                {
                    foreach (string neighbor in Graph[node])
                    {
                        DFS(neighbor);
                    }
                }

                // Backtracking: Entferne den Knoten aus dem aktuellen Pfad und markiere ihn als nicht besucht
                currentPath.RemoveAt(currentPath.Count - 1);
                visited.Remove(node);
            }

            DFS(startNode);
            return allPaths;
        }

        public void PrintPaths(List<List<string>> paths)
        {
            Debug.Log($"Gefundene Pfade: {paths.Count}");
            // foreach (var path in paths)
            // {
            //     Debug.Log("Pfad: " + string.Join(" -> ", path));
            // }
        }
    
        public void PrintGraph()
        {
            // Debug.Log("Graph:");
            foreach (var node in Graph)
            {
                string neighbors = string.Join(", ", node.Value);
                // Debug.Log($"{node.Key} -> [{neighbors}]");
            }
        }

        public void PrintGraphAsTree(string startNode, int depth = 0)
        {
            if (!Graph.ContainsKey(startNode)) return;

            string prefix = new string('-', depth * 2);
            // Debug.Log($"{prefix}{startNode}");

            foreach (var neighbor in Graph[startNode])
            {
                PrintGraphAsTree(neighbor, depth + 1);
            }
        }

    }
}
