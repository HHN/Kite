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
                    "Schreibe einen Analysetext. Stelle die Biases und Verzerrungen dar, auf die du dich beziehst (unten eine Liste mit Geschlechterbiases im Gründungsprozess).\r\n" +
                    "Im Dialog findest du auch Hinweise auf Biases, die an der jeweiligen Stelle des Dialogs zum Tragen kommen. Nutze diese Hinweise zur Analyse des Dialogs.\r\n" +
                    "Analysiere auch Leas Verhalten und ihre Reaktionen auf diese Biases mit konkreten Beispielen aus dem Dialog.\r\n" +
                    "Stelle die Vorteile von Leas Verhalten dar und deute vorsichtig an, welche Nachteile ihre Reaktion haben könnte.\r\n" +
                    "Führe das Nicht-Ansprechen geschlechterstereotyper Annahmen nicht bei den Nachteilen auf.\r\n" +
                    "Sei vorsichtig mit dem Hinweis, Biases und Stereotype direkt anzusprechen, weil dies zwar generell sinnvoll sein kann,\r\n" +
                    "Lea aber in erster Linie darauf achten muss, dass sie das Gespräch so führt, dass sie im Gespräch erfolgreich ist.\r\n" +
                    "Nutze geschlechtergerechte Sprache (z.B. Gründer*innen, weibliche Gründerinnen).\r\n" +
                    "Richte den Text in der Du-Form an Lea. Sei wohlwollend und ermunternd. Sprich Lea nicht mit ihrem Namen an.\r\n" +
                    "Formuliere den Text aus einer unbestimmten Ich-Perspektive."
                );
                _prompt.AppendLine();

                //Wissens Basis
                _prompt.Append(
                    "Hier die Liste mit Geschlechterbiases im Gründungsprozess:\r\n\r\n" +
                    "Finanzielle und geschäftliche Herausforderungen:\r\n" +
                    "- AccessToFunding: Schwierigkeiten von Frauen, Kapital für ihre Unternehmen zu beschaffen.\r\n" +
                    "- Gender Pay Gap: Lohnungleichheit zwischen Männern und Frauen.\r\n" +
                    "- Unterbewertung weiblich geführter Unternehmen: Geringere Bewertung von Unternehmen, die von Frauen geführt werden.\r\n" +
                    "- Risk Aversion Bias: Wahrnehmung von Frauen als risikoaverser.\r\n" +
                    "- Bestätigungsverzerrung: Tendenz, Informationen zu interpretieren, die bestehende Vorurteile bestätigen.\r\n" +
                    "- Tokenism: Wahrnehmung von Frauen in unternehmerischen Kreisen als Alibifiguren.\r\n" +
                    "- Bias in der Wahrnehmung von Führungsfähigkeiten: Infragestellung der Führungsfähigkeiten von Frauen.\r\n" +
                    "- Benevolenter Sexismus: Schützend oder wohlwollend gemeinte, aber dennoch herabwürdigende Haltungen gegenüber Frauen.\r\n\r\n" +
                    "Intersektionale und spezifische Biases:\r\n" +
                    "- Rassistische und ethnische Biases: Zusätzliche Vorurteile gegenüber Frauen aus Minderheitengruppen.\r\n" +
                    "- Sozioökonomische Biases: Größere Hindernisse für Frauen aus niedrigeren sozioökonomischen Schichten.\r\n" +
                    "- Alter- und Generationen-Biases: Diskriminierung aufgrund von Altersstereotypen.\r\n" +
                    "- Sexualitätsbezogene Biases: Vorurteile gegenüber lesbischen, bisexuellen oder queeren Frauen.\r\n" +
                    "- Biases gegenüber Frauen mit Behinderungen: Zusätzliche Herausforderungen für Frauen mit körperlichen oder geistigen Behinderungen.\r\n" +
                    "- Stereotype gegenüber Frauen in nicht-traditionellen Branchen: Widerstände gegen Frauen in männlich dominierten Feldern.\r\n" +
                    "- Kulturelle und religiöse Biases: Diskriminierung aufgrund kultureller oder religiöser Zugehörigkeit.\r\n" +
                    "- Heteronormativität: Annahmen und Erwartungen, dass Heterosexualität die bevorzugte sexuelle Orientierung ist.\r\n\r\n" +
                    "Biases im Bereich der Rollen- und Familienwahrnehmung:\r\n" +
                    "- Maternal Bias: Annahmen über geringere Engagementbereitschaft von Müttern oder potenziellen Müttern.\r\n" +
                    "- Biases gegenüber Frauen mit Kindern: Benachteiligung von Müttern, insbesondere Alleinerziehenden.\r\n" +
                    "- Erwartungshaltung bezüglich Familienplanung: Annahmen über zukünftige Familienplanung bei Frauen im gebärfähigen Alter.\r\n" +
                    "- Work-Life-Balance-Erwartungen: Druck auf Frauen, ein Gleichgewicht zwischen Beruf und Familie zu finden.\r\n\r\n" +
                    "Karriereentwicklungs- und Wahrnehmungsbiases:\r\n" +
                    "- Geschlechtsspezifische Stereotypen: Annahmen über geringere Kompetenz von Frauen in bestimmten Bereichen.\r\n" +
                    "- Doppelte Bindung (Tightrope Bias): Konflikt zwischen der Wahrnehmung als zu weich oder zu aggressiv.\r\n" +
                    "- Mikroaggressionen: Subtile Formen der Diskriminierung gegenüber Frauen.\r\n" +
                    "- Leistungsattributions-Bias: Externe Zuschreibung von Erfolgen von Frauen.\r\n" +
                    "- Bias in Medien und Werbung: Verzerrte Darstellung von Unternehmerinnen in den Medien.\r\n" +
                    "- Unbewusste Bias in der Kommunikation: Herabsetzende Art und Weise, wie über Frauenunternehmen gesprochen wird.\r\n" +
                    "- Prove-it-Again-Bias: Anforderung an Frauen, insbesondere in technischen Bereichen, ihre Kompetenzen wiederholt zu beweisen.\r\n"
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