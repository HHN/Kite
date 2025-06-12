using UnityEngine;
using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Assets._Scripts.ServerCommunication;
using Assets._Scripts.ServerCommunication.ServerCalls;

namespace Assets
{
    public class GeneratePathFeedback : MonoBehaviour
    {
        //-----------------------Server Call Stuff--------------------------
        [SerializeField] GameObject gptServercallPrefab;

        //-----------------------Needed for Paths---------------------------
        [SerializeField] private string pathFile = "Assets/pathFile.txt";
        List<string> pathList = new List<string>();
        string[] promptElements = new string[9];
        string prompt = "";

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            string pathContent = ReadPathFile(pathFile);
            ParsePathFile(pathContent);
            Debug.Log(pathList.Count);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        void ParsePathFile(string pathString)
        {
            var matches = Regex.Matches(pathString, @"(.*?)(?=PATH \d+ FINISHED)", RegexOptions.Singleline);
            foreach (Match match in matches)
            {
                string matchContent = match.Groups[1].Value;
                if (!string.IsNullOrWhiteSpace(matchContent))
                {
                    pathList.Add(matchContent);
                }
            }
        }
        public string ReadPathFile(string filePath)
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
        public void initializePromptElements(int i)
        {
            promptElements[0] = "Du bist eine Geschlechterforscherin.\nDeine Aufgabe ist es, den folgenden Dialogausschnitt auf Diskriminierung hin zu untersuchen.\n\n";
            promptElements[1] = "PROMPT KONTEXT"; //Novel-abhängig
            promptElements[2] = "Die Spielerin hat den gesamten Dialog durchgespielt. Ich gebe Dir allerdings nur den Dialogausschnitt, in dem Biases vorkommen, weil dies die relevanten Passagen sind. Du findest dort jeweils die Aussage des Gegenübers, den Namen des Bias, der hier zum Tragen kommt und die Reaktion der Spielerin. Nutze die Information über den Bias als Basis für Deine Interpretation.\nAußerdem gebe ich dir die twee-Datei mit allen möglichen Gesprächspassagen. Dies ist aber nur für Dich zur Kontextualisierung, nicht zur Analyse.\nSchreibe einen Analysetext über den Dialogausschnitt, in dem Biases vorkommen.Stelle die Biases und Verzerrungen dar, auf die du dich beziehst(zur Erläuterung findest du unten eine Liste mit Geschlechterbiases im Gründungsprozess). Drücke Dich allgemeinverständlich aus. Verwende bei der Benennung der Biases ausschließlich den deutschen Namen. Setze keinen einleitenden Text davor, sondern starte direkt mit der Ansprache an die Spielerin.\nAnalysiere auch das Verhalten der Spielerin und ihre Reaktionen auf diese Biases zu den jeweiligen Biases mit konkreten Beispielen aus dem Dialog.\nStelle die Vorteile des Verhaltens der Gründerin dar und deute vorsichtig an, welche Nachteile ihre Reaktion haben könnte.\nFühre das Nicht - Ansprechen geschlechterstereotyper Annahmen nicht bei den Nachteilen auf.\nSei vorsichtig mit dem Hinweis, Biases und Stereotype direkt anzusprechen, weil dies zwar generell sinnvoll sein kann, die Spielerin aber in erster Linie darauf achten muss, dass sie das Gespräch so führt, dass sie im Gespräch erfolgreich ist.\nNutze geschlechtergerechte Sprache(z.B.Gründer * innen, weibliche Gründerinnen).\nRichte den Text in der Du-Form an die Spielerin.Sei wohlwollend und ermunternd. Sprich die Gründerin nicht mit ihrem Namen an. Formuliere den Text aus einer unbestimmten Ich - Perspektive.\n\n###\n\n";
            promptElements[3] = "Hier die Liste mit Geschlechterbiases im Gründungsprozess:\n";
            promptElements[4] = "BIAS-ERKLÄRUNGEN"; //Novel-abhängig
            promptElements[5] = "Hier der Dialogausschnitt, in denen Biases vorgekommen sind. Analysiere nur diesen Ausschnitt.";
            promptElements[6] = pathList[i];
            promptElements[7] = "\n\n###\n\nHier zur Kontextualisierung die twee-Datei mit dem gesamten Dialog. Aber du sollst dich wie oben dargestellt nur auf den oben dargestellten Dialogabschnitt beziehen.";
            promptElements[8] = "TWEE-DIALOG"; //Novel-abhängig
        }
        
    }
}
