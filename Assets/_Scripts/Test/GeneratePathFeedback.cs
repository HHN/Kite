using UnityEngine;
using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Assets._Scripts.ServerCommunication;
using Assets._Scripts.ServerCommunication.ServerCalls;

namespace OpenAI
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


        //Parse Pfaddateiern, die von PathCalculator erstellt werden
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


        //Ließt Pfaddateiern, die von TweePathCalculator erstellt werden
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


        //Elemente des Prompts. Auch zu finden im Wiki unter https://git.it.hs-heilbronn.de/nicola/kite/-/wikis/Standalone-Prompt-Composition
        //promptElements[1] und promptElements[8] sind unterschiedlich je nach Novel
        public void initializePromptElements(int i)
        {
            promptElements[0] = "Du bist eine Geschlechterforscherin.\nDeine Aufgabe ist es, den folgenden Dialogausschnitt auf Diskriminierung hin zu untersuchen.\n\n";
            promptElements[1] = "PROMPT KONTEXT"; //Novel-abhängig
            promptElements[2] = "Die Spielerin hat den gesamten Dialog durchgespielt. Ich gebe Dir allerdings nur den Dialogausschnitt, in dem Biases vorkommen, weil dies die relevanten Passagen sind. Du findest dort jeweils die Aussage des Gegenübers, den Namen des Bias, der hier zum Tragen kommt und die Reaktion der Spielerin. Nutze die Information über den Bias als Basis für Deine Interpretation.\nAußerdem gebe ich dir die twee-Datei mit allen möglichen Gesprächspassagen. Dies ist aber nur für Dich zur Kontextualisierung, nicht zur Analyse.\nSchreibe einen Analysetext über den Dialogausschnitt, in dem Biases vorkommen.Stelle die Biases und Verzerrungen dar, auf die du dich beziehst(zur Erläuterung findest du unten eine Liste mit Geschlechterbiases im Gründungsprozess). Drücke Dich allgemeinverständlich aus. Verwende bei der Benennung der Biases ausschließlich den deutschen Namen. Setze keinen einleitenden Text davor, sondern starte direkt mit der Ansprache an die Spielerin.\nAnalysiere auch das Verhalten der Spielerin und ihre Reaktionen auf diese Biases zu den jeweiligen Biases mit konkreten Beispielen aus dem Dialog.\nStelle die Vorteile des Verhaltens der Gründerin dar und deute vorsichtig an, welche Nachteile ihre Reaktion haben könnte.\nFühre das Nicht - Ansprechen geschlechterstereotyper Annahmen nicht bei den Nachteilen auf.\nSei vorsichtig mit dem Hinweis, Biases und Stereotype direkt anzusprechen, weil dies zwar generell sinnvoll sein kann, die Spielerin aber in erster Linie darauf achten muss, dass sie das Gespräch so führt, dass sie im Gespräch erfolgreich ist.\nNutze geschlechtergerechte Sprache(z.B.Gründer * innen, weibliche Gründerinnen).\nRichte den Text in der Du-Form an die Spielerin.Sei wohlwollend und ermunternd. Sprich die Gründerin nicht mit ihrem Namen an. Formuliere den Text aus einer unbestimmten Ich - Perspektive.\n\n###\n\n";
            promptElements[3] = "Hier die Liste mit Geschlechterbiases im Gründungsprozess:\n";
            promptElements[4] = "Finanzielle und Geschäftliche Herausforderungen\nFinanzierungszugang\nBias | AccessToFinancing <<\nBias: Schwierigkeiten von Frauen, Kapital für ihre Unternehmen zu beschaffen.\nGender Pay Gap\nBias | GenderPayGap <<\nBias: Lohnungleichheit zwischen Männern und Frauen.\nUnterbewertung weiblich geführter Unternehmen\nBias | UndervaluationFemaleManagedCompany <<\nBias: Geringere Bewertung von Unternehmen, die von Frauen geführt werden.\nRisk Aversion Bias\nBias | RiskAversionBias <<\nBias: Wahrnehmung von Frauen als risikoaverser.\nBestätigungsverzerrung\nBias | ConfirmationBias <<\nBias: Tendenz, Informationen zu interpretieren, die bestehende Vorurteile bestätigen.\nGesellschaftliche Erwartungen &soziale Normen\nTokenism\nBias | Tokenism <<\nBias: Wahrnehmung von Frauen in unternehmerischen Kreisen als Alibifiguren.\nBias in der Wahrnehmung von Führungsfähigkeiten\nBias | BiasInThePerceptionOfLeadershipSkills <<\nBias: Infragestellung der Führungsfähigkeiten von Frauen.\nBenevolenter Sexismus\nBias | BenevolentSexism <<\nBias: Schützend oder wohlwollend gemeinte, aber dennoch herabwürdigende Haltungen gegenüber Frauen, die sie als weniger kompetent und in Bedarf von männlicher Hilfe darstellen.\nAlter - und Generationen - Biases\nBias | AgeAndGenerationsBiases <<\nBias: Diskriminierung aufgrund von Altersstereotypen.\nStereotype gegenüber Frauen in nicht - traditionellen Branchen\nBias | StereotypesAboutWomenInNon <<\nBias: Widerstände gegen Frauen in männlich dominierten Feldern.\nProve - it - Again - Bias\nBias | ProveItAgainBias <<\nBias: Anforderung an Frauen, insbesondere in technischen Bereichen, ihre Kompetenzen wiederholt zu beweisen.\nWahrnehmung & Führungsrollen\nHeteronormativität\nBias | Heteronormativity <<\nBias: Annahmen und Erwartungen, die auf der Vorstellung beruhen, dass Heterosexualität die einzige oder bevorzugte sexuelle Orientierung ist.\nMaternal Bias\nBias | BiasesAgainstWomenWithChildren <<\nBias: Annahmen über geringere Engagementbereitschaft von Müttern oder potenziellen Müttern.\nErwartungshaltung bezüglich Familienplanung\nBias | ExpectationsRegardingFamilyPlanning <<\nBias: Annahmen über zukünftige Familienplanung bei Frauen im gebärfähigen Alter.\nWork - Life - Balance - Erwartungen\nBias | WorkLifeBalanceExpectations <<\nBias: Druck auf Frauen, ein Gleichgewicht zwischen Beruf und Familie zu finden.\nGeschlechterspezifische Stereotype\nBias | GenderSpecificStereotypes <<\nBias: Annahmen über geringere Kompetenz von Frauen in bestimmten Bereichen.\nPsychologische Barrieren &kommunikative Hindernisse\nDoppelte Bindung(Tightrope Bias)\nBias | TightropeBias <<\nBias: Konflikt zwischen der Wahrnehmung als zu weich oder zu aggressiv.\nMikroaggressionen\nBias | Microaggression <<\nBias: Subtile Formen der Diskriminierung gegenüber Frauen.\nLeistungsattributions - Bias\nBias | PerformanceAttributionBias <<\nBias: Externe Zuschreibung von Erfolgen von Frauen.\nUnbewusste Biases in der Kommunikation\nBias | UnconsciousBiasInCommunication <<\nBias: Herabsetzende Art und Weise, wie über Frauenunternehmen gesprochen wird.";
            promptElements[5] = "Hier der Dialogausschnitt, in denen Biases vorgekommen sind. Analysiere nur diesen Ausschnitt.";
            promptElements[6] = pathList[i];
            promptElements[7] = "\n\n###\n\nHier zur Kontextualisierung die twee-Datei mit dem gesamten Dialog. Aber du sollst dich wie oben dargestellt nur auf den oben dargestellten Dialogabschnitt beziehen.";
            promptElements[8] = "TWEE-DIALOG"; //Novel-abhängig
        }
        
    }
}
