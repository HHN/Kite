using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine;

public class GNP_V01_JOB50 : PipelineJob
{
    public GNP_V01_JOB50()
    {
        this.jobName = "Job50 - Aussehen für den Gesprächspartner bestimmen";
    }

    public override PipelineJobState HandleCompletion(string completion)
    {
        Match match = Regex.Match(completion, @"\[(.*?)\]");
        if (match.Success)
        {
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
        stringBuilder.Append("Du bist eine fortschrittliche KI, spezialisiert auf die Generierung von Geschichten.");
        stringBuilder.AppendLine();

        // Aufgabe
        stringBuilder.Append("Deine Aufgabe:");
        stringBuilder.AppendLine();
        stringBuilder.Append("Deine Aufgabe besteht darin, ein passendes Aussehen für den Gesprächspartner der Hauptperson der Geschichte auszuwählen, die du gerade erstellst. Zur Auswahl hast du eine Liste von Charakter-Beschreibungen, die weiter unten im Prompt zu finden ist. Außerdem findest du in diesem Prompt Informationen zu der Geschichte, die du gerade erstellst. Wähle die Aussehens-Beschreibung, die am besten zum Gesprächspartners des Hauptcharakters passt. Wenn keine Aussehens-Beschreibung aus der Liste passt, dann wähle die Aussehens-Beschreibung aus, die am ehesten passt. Erfinde keine neuen Aussehens-Beschreibungen! Wähle von den vorgegebenen Aussehens-Beschreibungen! Triff aufjedenfall eine Entscheidung!");
        stringBuilder.AppendLine();

        stringBuilder.Append("Der Hauptcharakter heißt " + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_MAIN_CHARACTER] + ". ");
        stringBuilder.Append("Der Hauptcharakter ist eine Gründerin. ");
        stringBuilder.Append("Die zweite Person ist der Gesprächspartner oder die Gesprächspartnerin des Hauptcharakters und heißt: " + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_TALKING_PARTNER] + ". ");
        stringBuilder.Append("Der zweite Hauptcharakter hat folgende Rolle: " + pipeline.GetMemory()[GenerateNovelPipeline.ROLE_OF_TALKING_PARTNER] + ". ");
        stringBuilder.Append(pipeline.GetMemory()[GenerateNovelPipeline.LOCATION]);
        stringBuilder.Append("Die Geschichte hat folgenden Titel: " + pipeline.GetMemory()[GenerateNovelPipeline.TITLE] + ". ");
        stringBuilder.Append("Das ist die Kurzbeschreibung der Geschichte: " + pipeline.GetMemory()[GenerateNovelPipeline.DESCRIPTION] + ". ");
        stringBuilder.Append("In der Geschichte geht es um folgendes: " + pipeline.GetMemory()[GenerateNovelPipeline.CONTEXT] + ". ");


        // Output Format
        stringBuilder.Append("Das gewünschte Ergebnis:");
        stringBuilder.AppendLine();
        stringBuilder.Append("Bitte gib als Ergebnis genau die Zahl der Aussehens-Beschreibung an, die du ausgesucht hast. Bitte beachte, dass dein generiertes Ergebnis von einer speziellen Software weiterverarbeitet wird. Daher ist es entscheidend, dass du das Ergebnis in eckigen Klammern zurückgibst. Deine Antwort sollte direkt mit einer öffnenden eckigen Klammer '[' beginnen und mit einer schließenden eckigen Klammer ']' enden. Beispiel: [Von dir ausgesuchte Zahl]. Diese Formatierung ermöglicht eine reibungslose Integration und Verarbeitung deiner Ausgabe durch das nachgelagerte System.");
        stringBuilder.AppendLine();

        //
        stringBuilder.Append("--Hier die Liste mit Aussehens-Beschreibung--\r\n1. ein älterer Herr, Freizeitkleidung\r\n2. eine ältere Dame, Freizeitkleidung\r\n3. ein junger Herr, Freizeitkleidung\r\n4. eine junge Dame, Freizeitkleidung\r\n5. ein Herr mittleren Alters, Freizeitkleidung\r\n6. eine Dame mittleren Alters, Freizeitkleidung\r\n7. ein älterer Herr, im Anzug\r\n8. eine ältere Dame, im Anzug\r\n9. ein junger Herr, im Anzug\r\n10. eine junge Dame, im Anzug\r\n11. ein Herr mittleren Alters, im Anzug\r\n12. eine Dame mittleren Alters, im Anzug\r\n13. ein Polizist\r\n14. eine Polizistin\r\n15. ein Bauarbeiter\r\n16. eine Bauarbeitern\r\n17. ein Lieferant\r\n18. eine Lieferantin\r\n--Liste Ende--");
        stringBuilder.AppendLine();

        prompt = stringBuilder.ToString();
    }
}
