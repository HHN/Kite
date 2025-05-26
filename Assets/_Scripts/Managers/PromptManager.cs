using System.Text;
using Assets._Scripts.Novel;

namespace Assets._Scripts.Managers
{
    public class PromptManager
    {
        private static PromptManager _instance;
        private StringBuilder _prompt;
        private StringBuilder _dialog;

        private PromptManager()
        {
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
            if (_dialog == null)
            {
                return "";
            }

            return _dialog.ToString();
        }

        public void InitializePrompt(VisualNovel novel)
        {
            _prompt = new StringBuilder();
            _dialog = new StringBuilder();

            if ((int)novel.id == 14)
            {
                _prompt.Append(novel.context);
                // _prompt.Append("Du bist eine Organisationsentwicklerin und Kommunikationstrainerin. " +
                //                "Deine Aufgabe ist es, den folgenden Dialog dahingehend zu untersuchen, wie kommunikativ geschickt sich die KI-Einführungsperson verhalten hat. " +
                //                "Es ist das Gespräch einer KI-Einführungsperson mit einem Mitarbeiter aus Vertrieb und Marketing. " +
                //                "Schreibe einen Analysetext. Berücksichtige dabei Wissen aus den Bereichen der Einwandbehandlung, Gesprächstechniken, Kommunikationstechniken, Organisationsentwicklung etc. " +
                //                "Analysiere das Verhalten der KI-Einführungsperson und ihre Reaktionen auf den Mitarbeiter mit Bezug zu konkreten Beispielen aus dem Dialog. " +
                //                "Stelle dar, wo die KI-Einführungsperson geschickt agiert hat und wo eher nicht. Deute an, welche Vor- und Nachteile ihr Verhalten haben könnte. " +
                //                "Nutze geschlechtergerechte Sprache (z.B. Gründer*innen, weibliche Gründerinnen). " +
                //                "Richte den Text in der Du-Form an die KI-Einführungsperson. Sei wohlwollend und ermunternd. Formuliere den Text aus einer unbestimmten Ich-Perspektive.");
            }
            else
            {
                //ROLLE
                _prompt.Append("Du bist eine Geschlechterforscherin. ");
                _prompt.AppendLine();

                //AUFGABE
                _prompt.Append("Deine Aufgabe ist es, den folgenden Dialog auf Diskriminierung hin zu untersuchen. ");
                _prompt.AppendLine();

                //Kontext
                _prompt.Append("{{Context}} ");
                _prompt.AppendLine();

                //Output Format
                _prompt.Append(
                    "Schreibe einen Analysetext. Stelle die Biases und Verzerrungen dar, auf die du dich beziehst (unten eine Liste mit Geschlechterbiases im Gründungsprozess). \n\n" +
                    "Im Dialog findest Du auch Hinweise auf Biases, die an der jeweiligen Stelle des Dialogs zum Tragen kommen. Nutze diese Hinweise zur Analyse des Dialogs. \n\n" +
                    "Analysiere auch Leas Verhalten und ihre Reaktionen auf diese Biases zu den jeweiligen Biases.mit konkreten Beispielen aus dem Dialog. \n\n" +
                    "Stelle die Vorteile von Leas Verhalten dar und deute vorsichtig an, welche Nachteile ihre Reaktion haben könnte.\n\n" +
                    "Führe das Nicht-Ansprechen geschlechterstereotyper Annahmen nicht bei den Nachteilen auf.\n\n" +
                    "Sei vorsichtig mit dem Hinweis, Biases und Stereotype direkt anzusprechen, weil dies zwar generell sinnvoll sein kann, Lea aber in erster Linie darauf achten muss, dass sie das Gespräch so führt, dass sie im Gespräch erfolgreich ist.\n\n" +
                    "Nutze geschlechtergerechte Sprache (z.B. Gründer*innen, weibliche Gründerinnen).\n\n" +
                    "Richte den Text in der Du-Form an Lea. Sei wohlwollend und ermunternd. Sprich Lea nicht mit ihrem Namen an. Formuliere den Text aus einer unbestimmten Ich-Perspektive.\n\n"
                );
                _prompt.AppendLine();

                //Wissens Basis
                _prompt.Append(
                    "Hier die Liste mit Geschlechterbiases im Gründungsprozess:\n\n" +
                    "Finanzielle und Geschäftliche Herausforderungen\n\n" +
                    "Finanzierungszugang \n>>Bias|AccessToFinancing<<\nBias: Schwierigkeiten von Frauen, Kapital für ihre Unternehmen zu beschaffen.\n\n" +
                    "Gender Pay Gap\n>>Bias|GenderPayGap<<\nBias: Lohnungleichheit zwischen Männern und Frauen.\n\n" +
                    "Unterbewertung weiblich geführter Unternehmen\n>>Bias|UndervaluationFemaleManagedCompany<<\nBias: Geringere Bewertung von Unternehmen, die von Frauen geführt werden.\n\n" +
                    "Risk Aversion Bias\n>>Bias|RiskAversionBias<<\nBias: Wahrnehmung von Frauen als risikoaverser.\n\n" +
                    "Bestätigungsverzerrung\n>>Bias|ConfirmationBias<<\nBias: Tendenz, Informationen zu interpretieren, die bestehende Vorurteile bestätigen.\n\n" +
                    "Gesellschaftliche Erwartungen & soziale Normen\n\n" +
                    "Tokenism\n>>Bias|Tokenism<<\nBias: Wahrnehmung von Frauen in unternehmerischen Kreisen als Alibifiguren.\n\n" +
                    "Bias in der Wahrnehmung von Führungsfähigkeiten\n>>Bias|BiasInThePerceptionOfLeadershipSkills<<\nBias: Infragestellung der Führungsfähigkeiten von Frauen.\n\n" +
                    "Benevolenter Sexismus\n>>Bias|BenevolentSexism<<\nBias: Schützend oder wohlwollend gemeinte, aber dennoch herabwürdigende Haltungen gegenüber Frauen, die sie als weniger kompetent und in Bedarf von männlicher Hilfe darstellen.\n\n" +
                    "Alter- und Generationen-Biases\n>>Bias|AgeAndGenerationsBiases<<\nBias: Diskriminierung aufgrund von Altersstereotypen.\n\n" +
                    "Stereotype gegenüber Frauen in nicht-traditionellen Branchen\n>>Bias|StereotypesAboutWomenInNon<<\nBias: Widerstände gegen Frauen in männlich dominierten Feldern.\n\n" +
                    "Prove-it-Again-Bias\n>>Bias|ProveItAgainBias<<\nBias: Anforderung an Frauen, insbesondere in technischen Bereichen, ihre Kompetenzen wiederholt zu beweisen.\n\n" +
                    "Wahrnehmung & Führungsrollen\n\n" +
                    "Heteronormativität\n>>Bias|Heteronormativity<<\nBias: Annahmen und Erwartungen, die auf der Vorstellung beruhen, dass Heterosexualität die einzige oder bevorzugte sexuelle Orientierung ist.\n\n" +
                    "Maternal Bias\n>>Bias|BiasesAgainstWomenWithChildren<<\nBias: Annahmen über geringere Engagementbereitschaft von Müttern oder potenziellen Müttern.\n\n" +
                    "Erwartungshaltung bezüglich Familienplanung\n>>Bias|ExpectationsRegardingFamilyPlanning<<\nBias: Annahmen über zukünftige Familienplanung bei Frauen im gebärfähigen Alter.\n\n" +
                    "Work-Life-Balance-Erwartungen\n>>Bias|WorkLifeBalanceExpectations<<\nBias: Druck auf Frauen, ein Gleichgewicht zwischen Beruf und Familie zu finden.\n\n" +
                    "Geschlechterspezifische Stereotype\n>>Bias|GenderSpecificStereotypes<<\nBias: Annahmen über geringere Kompetenz von Frauen in bestimmten Bereichen.\n\n" +
                    "Psychologische Barrieren & kommunikative Hindernisse\n\n" +
                    "Doppelte Bindung (Tightrope Bias)\n>>Bias|TightropeBias<<\nBias: Konflikt zwischen der Wahrnehmung als zu weich oder zu aggressiv.\n\n" +
                    "Mikroaggressionen\n>>Bias|Microaggression<<\nBias: Subtile Formen der Diskriminierung gegenüber Frauen.\n\n" +
                    "Leistungsattributions-Bias\n>>Bias|PerformanceAttributionBias<<\nBias: Externe Zuschreibung von Erfolgen von Frauen.\n\n" +
                    "Unbewusste Biases in der Kommunikation\n>>Bias|UnconsciousBiasInCommunication<<\nBias: Herabsetzende Art und Weise, wie über Frauenunternehmen gesprochen wird.\n"
                );
            }

            _prompt.AppendLine();

            //Analyse Objekt
            _prompt.Append("Hier ist der Dialog:");
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