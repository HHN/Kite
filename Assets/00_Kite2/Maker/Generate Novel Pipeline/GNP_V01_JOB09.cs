using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine;

public class GNP_V01_JOB09 : PipelineJob
{
    public GNP_V01_JOB09()
    {
        this.jobName = "Job09 - Bestimmung der Geschlechter-Biases die eine Rolle spielen.";
    }

    public override PipelineJobState HandleCompletion(string completion)
    {
        Match match = Regex.Match(completion, @"\[(.*?)\]");
        if (match.Success)
        {
            string result = match.Groups[1].Value;

            pipeline.GetMemory()[GenerateNovelPipeline.BIASES] = result;

            return PipelineJobState.COMPLETED;
        }
        else
        {
            return PipelineJobState.FAILED;
        }
    }

    public override void InitializePrompt()
    {
        StringBuilder stringBuilder = new StringBuilder();

        // Rolle
        stringBuilder.Append("Deine Rolle:");
        stringBuilder.AppendLine();
        stringBuilder.Append("Du bist eine fortschrittliche KI, spezialisiert auf die Generierung von Visual Novels, die den Gründungsprozess von Unternehmerinnen thematisieren. Dein Hauptaugenmerk liegt dabei auf der authentischen Darstellung von Herausforderungen und Diskriminierungen, denen Frauen im Gründungsprozess begegnen. Ziel ist es, Bewusstsein und Verständnis für die spezifischen Hürden zu schaffen, die Gründerinnen aufgrund ihres Geschlechts überwinden müssen. Deine Visual Novels sollen nicht nur informieren, sondern auch inspirieren und zum Nachdenken anregen. Weiter unten in diesem Prompt findest du eine Liste mit Geschlechterbiasen im Gründungsprozess, was deine Wissensbasis darstellt.");
        stringBuilder.AppendLine();

        // Aufgabe
        stringBuilder.Append("Deine Aufgabe:");
        stringBuilder.AppendLine();
        stringBuilder.Append("Deine Aufgabe ist es, die Beschreibung einer Geschichte zu analysieren, in der Diskriminierung einer Gründerin stattfindet. Anschließend sollst du bestimmen welche der Geschlechter-Biases in dieser Geschichte eine rolle spielen und dies in einem kleinen Text ausformulieren. Der Text soll dabei kurz und knackig zusammenfassen.");
        stringBuilder.AppendLine();


        // Output Format
        stringBuilder.Append("Das gewünschte Ergebnis:");
        stringBuilder.AppendLine();
        stringBuilder.Append("Bitte beachte, dass dein generiertes Ergebnis von einer speziellen Software weiterverarbeitet wird. Daher ist es entscheidend, dass du das Ergebnis in eckigen Klammern zurückgibst. Deine Antwort sollte direkt mit einer öffnenden eckigen Klammer '[' beginnen und mit einer schließenden eckigen Klammer ']' enden. Beispiel: [Dein generiertes Ergebnis]. Diese Formatierung ermöglicht eine reibungslose Integration und Verarbeitung deiner Ausgabe durch das nachgelagerte System.");
        stringBuilder.AppendLine();

        // Wissens Basis
        stringBuilder.Append("Die Wissensbasis für deine Aufgabe:");
        stringBuilder.AppendLine();
        stringBuilder.Append("Hier die Liste mit Geschlechterbiases im Gründungsprozess:\r\nFinanzielle und Geschäftliche Herausforderungen\r\n\r\nFinanzierungszugang\r\nBias Beschreibung: Schwierigkeiten von Frauen, Kapital für ihre Unternehmen zu beschaffen.\r\nGender Pay Gap\r\nBias Beschreibung: Lohnungleichheit zwischen Männern und Frauen.\r\nUnterbewertung weiblich geführter Unternehmen\r\nBias Beschreibung: Geringere Bewertung von Unternehmen, die von Frauen geführt werden.\r\nRisk Aversion Bias\r\nBias Beschreibung: Wahrnehmung von Frauen als risikoaverser.\r\nBestätigungsverzerrung\r\nBias Beschreibung: Tendenz, Informationen zu interpretieren, die bestehende Vorurteile bestätigen.\r\nTokenism\r\nBias Beschreibung: Wahrnehmung von Frauen in unternehmerischen Kreisen als Alibifiguren.\r\nBias in der Wahrnehmung von Führungsfähigkeiten\r\nBias Beschreibung: Infragestellung der Führungsfähigkeiten von Frauen.\r\nIntersektionale und spezifische Biases\r\n\r\nRassistische und ethnische Biases\r\nBias Beschreibung: Zusätzliche Vorurteile gegenüber Frauen aus Minderheitengruppen.\r\nSozioökonomische Biases\r\nBias Beschreibung: Größere Hindernisse für Frauen aus niedrigeren sozioökonomischen Schichten.\r\nAlter- und Generationen-Biases\r\nBias: Diskriminierung aufgrund von Altersstereotypen.\r\nSexualitätsbezogene Biases\r\nBias: Vorurteile gegenüber lesbischen, bisexuellen oder queeren Frauen.\r\nBiases gegenüber Frauen mit Behinderungen\r\nBias: Zusätzliche Herausforderungen für Frauen mit körperlichen oder geistigen Behinderungen.\r\nStereotype gegenüber Frauen in nicht-traditionellen Branchen\r\nBias: Widerstände gegen Frauen in männlich dominierten Feldern.\r\nKulturelle und religiöse Biases\r\nBias: Diskriminierung aufgrund kultureller oder religiöser Zugehörigkeit.\r\nBiases im Bereich der Rollen- und Familienwahrnehmung\r\n\r\nMaternal Bias\r\nBias: Annahmen über geringere Engagementbereitschaft von Müttern oder potenziellen Müttern.\r\nBiases gegenüber Frauen mit Kindern\r\nBias: Benachteiligung von Müttern, insbesondere Alleinerziehenden.\r\nErwartungshaltung bezüglich Familienplanung\r\nBias: Annahmen über zukünftige Familienplanung bei Frauen im gebärfähigen Alter.\r\nWork-Life-Balance-Erwartungen\r\nBias: Druck auf Frauen, ein Gleichgewicht zwischen Beruf und Familie zu finden.\r\nKarriereentwicklungs- und Wahrnehmungsbiases\r\n\r\nGeschlechtsspezifische Stereotypen\r\nBias: Annahmen über geringere Kompetenz von Frauen in bestimmten Bereichen.\r\nDoppelte Bindung (Tightrope Bias)\r\nBias: Konflikt zwischen der Wahrnehmung als zu weich oder zu aggressiv.\r\nMikroaggressionen\r\nBias: Subtile Formen der Diskriminierung gegenüber Frauen.\r\nLeistungsattributions-Bias\r\nBias: Externe Zuschreibung von Erfolgen von Frauen.\r\nBias in Medien und Werbung\r\nBias: Verzerrte Darstellung von Unternehmerinnen in den Medien.\r\nUnbewusste Bias in der Kommunikation\r\nBias: Herabsetzende Art und Weise, wie über Frauenunternehmen gesprochen wird.\r\nProve-it-Again-Bias\r\nBias: Anforderung an Frauen, insbesondere in technischen Bereichen, ihre Kompetenzen wiederholt zu beweisen.");
        stringBuilder.AppendLine();

        stringBuilder.Append("Die Geschichte hat folgenden Titel: " + pipeline.GetMemory()[GenerateNovelPipeline.TITLE] + ". ");
        stringBuilder.Append("Das ist die Kurzbeschreibung der Geschichte: " + pipeline.GetMemory()[GenerateNovelPipeline.DESCRIPTION] + ". ");
        stringBuilder.Append("In der Geschichte geht es um folgendes: " + pipeline.GetMemory()[GenerateNovelPipeline.CONTEXT] + ". ");
        stringBuilder.Append("Der Name des Hauptcharakters in der Geschichte: " + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_MAIN_CHARACTER] + ". ");
        stringBuilder.Append("Die Rolle des Hauptcharakters in der Geschichte: Der Hauptcharakter ist eine Gründerin. ");
        stringBuilder.Append("Der Name des Gesprächspartners oder der Gesprächspartnerin des Hauptcharakters in der Geschichte: " + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_TALKING_PARTNER] + ". ");
        stringBuilder.Append("Die Rolle des Gesprächspartners oder der Gesprächspartnerin des Hauptcharakters in der Geschichte: " + pipeline.GetMemory()[GenerateNovelPipeline.ROLE_OF_TALKING_PARTNER] + ". ");
        stringBuilder.Append(pipeline.GetMemory()[GenerateNovelPipeline.LOCATION]);
        stringBuilder.AppendLine();

        prompt = stringBuilder.ToString();
    }
}
