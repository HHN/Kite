using System.Text;

public class PromptManager
{
    private static PromptManager instance;
    public StringBuilder prompt;
    public StringBuilder dialog;

    private PromptManager() { }

    public static PromptManager Instance()
    {
        if (instance == null)
        {
            instance = new PromptManager();
        }
        return instance;
    }

    public string GetPrompt(string context)
    {
        if (prompt == null)
        {
            return "";
        }
        string prompString = prompt.ToString();
       
        return prompString.Replace("{{Context}}", context);
    }

    public string GetDialog()
    {
        if (dialog == null)
        {
            return "";
        }
        return dialog.ToString();
    }

    public void InitializePrompt()
    {
        prompt = new StringBuilder();
        dialog = new StringBuilder();

        //ROLLE
        prompt.Append("Du bist eine Geschlechterforscherin. ");
        prompt.AppendLine();
        //AUFGABE
        prompt.Append("Deine Aufgabe ist es, den folgenden Dialog auf Diskriminierung hin zu untersuchen. ");
        prompt.AppendLine();
        //Kontext
        prompt.Append("{{Context}} ");
        prompt.AppendLine();
        //Output Format
        prompt.Append("Schreibe einen Analysetext. Stelle die Biases und Verzerrungen dar, auf die du dich beziehst (unten eine Liste mit Geschlechterbiases im Gründungsprozess). Richte den Text in der Du-Form an Lea, verwende dabei aber niemals ihren Namen. Sei wohlwollend und ermunternd. Sprich Lea nicht mit ihrem Namen an. Formuliere den Text aus einer unbestimmten Ich-Perspektive. ");
        prompt.AppendLine();
        //Wissens Basis
        prompt.Append("Hier die Liste mit Geschlechterbiases im Gründungsprozess:\r\nFinanzielle und Geschäftliche Herausforderungen\r\n\r\nFinanzierungszugang\r\nBias Beschreibung: Schwierigkeiten von Frauen, Kapital für ihre Unternehmen zu beschaffen.\r\nGender Pay Gap\r\nBias Beschreibung: Lohnungleichheit zwischen Männern und Frauen.\r\nUnterbewertung weiblich geführter Unternehmen\r\nBias Beschreibung: Geringere Bewertung von Unternehmen, die von Frauen geführt werden.\r\nRisk Aversion Bias\r\nBias Beschreibung: Wahrnehmung von Frauen als risikoaverser.\r\nBestätigungsverzerrung\r\nBias Beschreibung: Tendenz, Informationen zu interpretieren, die bestehende Vorurteile bestätigen.\r\nTokenism\r\nBias Beschreibung: Wahrnehmung von Frauen in unternehmerischen Kreisen als Alibifiguren.\r\nBias in der Wahrnehmung von Führungsfähigkeiten\r\nBias Beschreibung: Infragestellung der Führungsfähigkeiten von Frauen.\r\nIntersektionale und spezifische Biases\r\n\r\nRassistische und ethnische Biases\r\nBias Beschreibung: Zusätzliche Vorurteile gegenüber Frauen aus Minderheitengruppen.\r\nSozioökonomische Biases\r\nBias Beschreibung: Größere Hindernisse für Frauen aus niedrigeren sozioökonomischen Schichten.\r\nAlter- und Generationen-Biases\r\nBias: Diskriminierung aufgrund von Altersstereotypen.\r\nSexualitätsbezogene Biases\r\nBias: Vorurteile gegenüber lesbischen, bisexuellen oder queeren Frauen.\r\nBiases gegenüber Frauen mit Behinderungen\r\nBias: Zusätzliche Herausforderungen für Frauen mit körperlichen oder geistigen Behinderungen.\r\nStereotype gegenüber Frauen in nicht-traditionellen Branchen\r\nBias: Widerstände gegen Frauen in männlich dominierten Feldern.\r\nKulturelle und religiöse Biases\r\nBias: Diskriminierung aufgrund kultureller oder religiöser Zugehörigkeit.\r\nBiases im Bereich der Rollen- und Familienwahrnehmung\r\n\r\nMaternal Bias\r\nBias: Annahmen über geringere Engagementbereitschaft von Müttern oder potenziellen Müttern.\r\nBiases gegenüber Frauen mit Kindern\r\nBias: Benachteiligung von Müttern, insbesondere Alleinerziehenden.\r\nErwartungshaltung bezüglich Familienplanung\r\nBias: Annahmen über zukünftige Familienplanung bei Frauen im gebärfähigen Alter.\r\nWork-Life-Balance-Erwartungen\r\nBias: Druck auf Frauen, ein Gleichgewicht zwischen Beruf und Familie zu finden.\r\nKarriereentwicklungs- und Wahrnehmungsbiases\r\n\r\nGeschlechtsspezifische Stereotypen\r\nBias: Annahmen über geringere Kompetenz von Frauen in bestimmten Bereichen.\r\nDoppelte Bindung (Tightrope Bias)\r\nBias: Konflikt zwischen der Wahrnehmung als zu weich oder zu aggressiv.\r\nMikroaggressionen\r\nBias: Subtile Formen der Diskriminierung gegenüber Frauen.\r\nLeistungsattributions-Bias\r\nBias: Externe Zuschreibung von Erfolgen von Frauen.\r\nBias in Medien und Werbung\r\nBias: Verzerrte Darstellung von Unternehmerinnen in den Medien.\r\nUnbewusste Bias in der Kommunikation\r\nBias: Herabsetzende Art und Weise, wie über Frauenunternehmen gesprochen wird.\r\nProve-it-Again-Bias\r\nBias: Anforderung an Frauen, insbesondere in technischen Bereichen, ihre Kompetenzen wiederholt zu beweisen. ");
        prompt.AppendLine();
        //Analyse Objekt
        prompt.Append("Hier ist der Dialog:");
    }

    public void AddLineToPrompt(string line)
    {
        if (prompt == null)
        {
            prompt = new StringBuilder();
        }
        if (dialog == null)
        {
            dialog = new StringBuilder();
        }
        prompt.AppendLine(line);
        dialog.AppendLine(line);
    }

    public void AddFormattedLineToPrompt(string characterName, string text)
    {
        string formattedLine = $"<b>{characterName}:</b> {text}";
        AddLineToPrompt(formattedLine);
    }
}
