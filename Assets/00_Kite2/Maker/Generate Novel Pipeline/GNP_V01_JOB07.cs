using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine;

public class GNP_V01_JOB07 : PipelineJob
{
    public GNP_V01_JOB07()
    {
        this.jobName = "Job07 - Generierung des Titels";
    }

    public override PipelineJobState HandleCompletion(string completion)
    {
        Match match = Regex.Match(completion, @"\[(.*?)\]");
        if (match.Success)
        {
            string result = match.Groups[1].Value;

            pipeline.GetMemory()[GenerateNovelPipeline.TITLE] = result;

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
        stringBuilder.Append("Du bist eine fortschrittliche KI, spezialisiert auf die Generierung von Visual Novels, die den Gründungsprozess von Unternehmerinnen thematisieren. Dein Hauptaugenmerk liegt dabei auf der authentischen Darstellung von Herausforderungen und Diskriminierungen, denen Frauen im Gründungsprozess begegnen. Ziel ist es, Bewusstsein und Verständnis für die spezifischen Hürden zu schaffen, die Gründerinnen aufgrund ihres Geschlechts überwinden müssen. Deine Visual Novels sollen nicht nur informieren, sondern auch inspirieren und zum Nachdenken anregen.");
        stringBuilder.AppendLine();

        // Aufgabe
        stringBuilder.Append("Deine Aufgabe:");
        stringBuilder.AppendLine();
        stringBuilder.Append("Deine Aufgabe besteht darin, einen passenden Titel für die Visual Novel zu generieren, die du gerade erstellst. Du findest in diesem Prompt eine Beschreibung der Geschichte, sowie eine Beschreibung der beiden Personen, die in dieser Geschichte miteinander interagiren, und Weiteres. Die eine Hauptperson ist eine Gründerin. Sie Interagiert mit einer zweiten Hauptperson. Generiere einen Titel. Die Leser der Visual Novel werden bereits im Vorfeld wissen, dass die eine der beiden Hauptfiguren eine Gründerin ist. Dies sollte also nicht aus dem Titel hervor gehen. Der Titel sollte außerdem keine Charakter Beschreibungen haben und auch kein Kunst-Titel sein. Stattdessen sollte aus dem Titel hervorgehen, wer die zweite Haupt-Person ist und/oder wo das Treffen der beiden stattfindet. Am besten sollte sich der Titel wie eine Ortsangabe lesen. Etwas wie 'Bei ...'. Der Titel sollte kurz und knackig sein! Es sollte kein Kunst-Titel sein, sondern einfach 2 bis 3 Worte. Der Titel darf keine Namen beinhalten! Der Titel darf keine Charakter-Eigenschaften beinhalten! Generiere den Titel in Deutscher Sprache! Weiter unten im Prompt findest du eine Liste mit Beispielen für Titel von anderen Noveln! Übernehme keine Inhalte aus den Beispielen, aber versuche den Style nachzumachen.");
        stringBuilder.AppendLine();

        //
        stringBuilder.Append("In der Geschichte, die du gerade erzeugst, geht es um folgendes: " + pipeline.GetMemory()[GenerateNovelPipeline.CONTEXT] + ". ");
        stringBuilder.Append("Der Name des Hauptcharakters in der Geschichte: " + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_MAIN_CHARACTER] + ". ");
        stringBuilder.Append("Die Rolle des Hauptcharakters in der Geschichte: Der Hauptcharakter ist eine Gründerin. ");
        stringBuilder.Append("Der Name des Gesprächspartners oder der Gesprächspartnerin des Hauptcharakters in der Geschichte: " + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_TALKING_PARTNER] + ". ");
        stringBuilder.Append("Die Rolle des Gesprächspartners oder der Gesprächspartnerin des Hauptcharakters in der Geschichte: " + pipeline.GetMemory()[GenerateNovelPipeline.ROLE_OF_TALKING_PARTNER] + ". ");
        stringBuilder.Append(pipeline.GetMemory()[GenerateNovelPipeline.LOCATION]);
        stringBuilder.AppendLine();

        // Output Format
        stringBuilder.Append("Das gewünschte Ergebnis:");
        stringBuilder.AppendLine();
        stringBuilder.Append("Bitte gib als Ergebnis genau den Titel an, den du ausgesucht hast. Bitte beachte, dass dein generiertes Ergebnis von einer speziellen Software weiterverarbeitet wird. Daher ist es entscheidend, dass du das Ergebnis in eckigen Klammern zurückgibst. Deine Antwort sollte direkt mit einer öffnenden eckigen Klammer '[' beginnen und mit einer schließenden eckigen Klammer ']' enden. Beispiel: [der von dir generierte Titel]. Diese Formatierung ermöglicht eine reibungslose Integration und Verarbeitung deiner Ausgabe durch das nachgelagerte System.");
        stringBuilder.AppendLine();

        //
        stringBuilder.Append("--Hier die Liste mit Beispielen für Titel--\r\n1. 'Telefonat mit den Eletern'\r\n2. 'Pressegespräch'\r\n3. 'Telefonat mit dem Notar'\r\n4. 'Erstgespräch Förderantrag'\r\n5. 'Anmietung eines Büros'\r\n6. 'Gründerzuschuss'\r\n7. 'Gespräch mit Bekannten'\r\n8. 'Banktermin zur Kreditvergabe'\r\n9. 'Honorarverhandlung mit Kundin'\r\n--Liste Ende--");
        stringBuilder.AppendLine();

        prompt = stringBuilder.ToString();
    }
}
