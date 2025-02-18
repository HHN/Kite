using System.Text;

namespace _00_Kite2.Common.Managers
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

        public void InitializePrompt()
        {
            _prompt = new StringBuilder();
            _dialog = new StringBuilder();

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
                "Schreibe einen Analysetext. Stelle die Biases und Verzerrungen dar, auf die du dich beziehst (unten eine Liste mit Geschlechterbiases im Gründungsprozess). Im Dialog findest Du auch Hinweise auf Biases, die an der jeweiligen Stelle des Dialogs zum Tragen kommen. Nutze diese Hinweise zur Analyse des Dialogs. Analysiere auch Leas Verhalten und ihre Reaktionen auf diese Biases mit konkreten Beispielen aus dem Dialog. Stelle die Vorteile von Leas Verhalten dar und deute vorsichtig an, welche Nachteile ihre Reaktion haben könnte. Führe das Nicht-Ansprechen geschlechterstereotyper Annahmen nicht bei den Nachteilen auf. Sei vorsichtig mit dem Hinweis, Biases und Stereotype direkt anzusprechen, weil dies zwar generell sinnvoll sein kann, Lea aber in erster Linie darauf achten muss, dass sie das Gespräch so führt, dass sie im Gespräch erfolgreich ist. Nutze geschlechtergerechte Sprache (z.B. Gründer*innen, weibliche Gründerinnen). Richte den Text in der Du-Form an Lea. Sei wohlwollend und ermunternd. Sprich Lea nicht mit ihrem Namen an. Formuliere den Text aus einer unbestimmten Ich-Perspektive.");
            _prompt.AppendLine();
            //Wissens Basis
            _prompt.Append(
                "Hier die Liste mit Geschlechterbiases im Gründungsprozess:\r\nFinanzielle und Geschäftliche Herausforderungen\r\n\r\nFinanzierungszugang\r\nBias Beschreibung: Schwierigkeiten von Frauen, Kapital für ihre Unternehmen zu beschaffen.\r\nGender Pay Gap\r\nBias Beschreibung: Lohnungleichheit zwischen Männern und Frauen.\r\nUnterbewertung weiblich geführter Unternehmen\r\nBias Beschreibung: Geringere Bewertung von Unternehmen, die von Frauen geführt werden.\r\nRisk Aversion Bias\r\nBias Beschreibung: Wahrnehmung von Frauen als risikoaverser.\r\nBestätigungsverzerrung\r\nBias Beschreibung: Tendenz, Informationen zu interpretieren, die bestehende Vorurteile bestätigen.\r\nTokenism\r\nBias Beschreibung: Wahrnehmung von Frauen in unternehmerischen Kreisen als Alibifiguren.\r\nBias in der Wahrnehmung von Führungsfähigkeiten\r\nBias Beschreibung: Infragestellung der Führungsfähigkeiten von Frauen.\r\nBenevolenter Sexismus\r\nBias Beschreibung: Schützend oder wohlwollend gemeinte, aber dennoch herabwürdigende Haltungen gegenüber Frauen, die sie als weniger kompetent und in Bedarf von männlicher Hilfe darstellen.\r\nIntersektionale und spezifische Biases\r\n\r\nRassistische und ethnische Biases\r\nBias Beschreibung: Zusätzliche Vorurteile gegenüber Frauen aus Minderheitengruppen.\r\nSozioökonomische Biases\r\nBias Beschreibung: Größere Hindernisse für Frauen aus niedrigeren sozioökonomischen Schichten.\r\nAlter- und Generationen-Biases\r\nBias: Diskriminierung aufgrund von Altersstereotypen.\r\nSexualitätsbezogene Biases\r\nBias: Vorurteile gegenüber lesbischen, bisexuellen oder queeren Frauen.\r\nBiases gegenüber Frauen mit Behinderungen\r\nBias: Zusätzliche Herausforderungen für Frauen mit körperlichen oder geistigen Behinderungen.\r\nStereotype gegenüber Frauen in nicht-traditionellen Branchen\r\nBias: Widerstände gegen Frauen in männlich dominierten Feldern.\r\nKulturelle und religiöse Biases\r\nBias: Diskriminierung aufgrund kultureller oder religiöser Zugehörigkeit.\r\nHeteronormativität\r\nBias Beschreibung: Annahmen und Erwartungen, die auf der Vorstellung beruhen, dass Heterosexualität die einzige oder bevorzugte sexuelle Orientierung ist.\r\nBiases im Bereich der Rollen- und Familienwahrnehmung\r\n\r\nMaternal Bias\r\nBias: Annahmen über geringere Engagementbereitschaft von Müttern oder potenziellen Müttern.\r\nBiases gegenüber Frauen mit Kindern\r\nBias: Benachteiligung von Müttern, insbesondere Alleinerziehenden.\r\nErwartungshaltung bezüglich Familienplanung\r\nBias: Annahmen über zukünftige Familienplanung bei Frauen im gebärfähigen Alter.\r\nWork-Life-Balance-Erwartungen\r\nBias: Druck auf Frauen, ein Gleichgewicht zwischen Beruf und Familie zu finden.\r\nKarriereentwicklungs- und Wahrnehmungsbiases\r\n\r\nGeschlechtsspezifische Stereotypen\r\nBias: Annahmen über geringere Kompetenz von Frauen in bestimmten Bereichen.\r\nDoppelte Bindung (Tightrope Bias)\r\nBias: Konflikt zwischen der Wahrnehmung als zu weich oder zu aggressiv.\r\nMikroaggressionen\r\nBias: Subtile Formen der Diskriminierung gegenüber Frauen.\r\nLeistungsattributions-Bias\r\nBias: Externe Zuschreibung von Erfolgen von Frauen.\r\nBias in Medien und Werbung\r\nBias: Verzerrte Darstellung von Unternehmerinnen in den Medien, z.B. durch Überbetonung des Frau-Seins oder der Darstellung von Gründerinnen als \"Vorzeigefrauen\"..\r\nUnbewusste Bias in der Kommunikation\r\nBias: Herabsetzende Art und Weise, wie über Frauenunternehmen gesprochen wird.\r\nProve-it-Again-Bias\r\nBias: Anforderung an Frauen, insbesondere in technischen Bereichen, ihre Kompetenzen wiederholt zu beweisen. ");
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
            string formattedLine = characterName.Contains("Hinweis") ? $"<i><b>{characterName}:</b> {text}</i>" : $"<b>{characterName}:</b> {text}";
            AddLineToPrompt(formattedLine);
        }
    }
}