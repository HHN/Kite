using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine;

public class GNP_V01_JOB05 : PipelineJob
{
    public GNP_V01_JOB05()
    {
        this.jobName = "Job05 - Ort für die Geschichte aussuchen";
    }

    public override PipelineJobState HandleCompletion(string completion)
    {
        Match match = Regex.Match(completion, @"\[(.*?)\]");
        if (match.Success)
        {
            string result = match.Groups[1].Value;

            if (result.Contains("7"))
            {
                pipeline.GetMemory()[GenerateNovelPipeline.BACKGROUND_SPRITE] = "7";
                pipeline.GetMemory()[GenerateNovelPipeline.LOCATION] = "Die Begegnung der beiden Haputcharakteren spielt sich auf einer öffentlichen Veranstaltung ab. ";
                return PipelineJobState.COMPLETED;
            }
            if (result.Contains("6"))
            {
                pipeline.GetMemory()[GenerateNovelPipeline.BACKGROUND_SPRITE] = "6";
                pipeline.GetMemory()[GenerateNovelPipeline.LOCATION] = "Die Begegnung der beiden Haputcharaktere spielt sich in einem Einzelhandel ab. ";
                return PipelineJobState.COMPLETED;
            }
            if (result.Contains("5"))
            {
                pipeline.GetMemory()[GenerateNovelPipeline.BACKGROUND_SPRITE] = "5";
                pipeline.GetMemory()[GenerateNovelPipeline.LOCATION] = "Die Begegnung der beiden Haputcharaktere spielt sich in einer staatlichen Behörde ab. ";
                return PipelineJobState.COMPLETED;
            }
            if (result.Contains("4"))
            {
                pipeline.GetMemory()[GenerateNovelPipeline.BACKGROUND_SPRITE] = "4";
                pipeline.GetMemory()[GenerateNovelPipeline.LOCATION] = "Die Begegnung der beiden Haputcharaktere spielt sich in einem Park ab. ";
                return PipelineJobState.COMPLETED;
            }
            if (result.Contains("3"))
            {
                pipeline.GetMemory()[GenerateNovelPipeline.BACKGROUND_SPRITE] = "3";
                pipeline.GetMemory()[GenerateNovelPipeline.LOCATION] = "Die Begegnung der beiden Haputcharaktere spielt sich im Zuhause einer Privatperon ab. ";
                return PipelineJobState.COMPLETED;
            }
            if (result.Contains("2"))
            {
                pipeline.GetMemory()[GenerateNovelPipeline.BACKGROUND_SPRITE] = "2";
                pipeline.GetMemory()[GenerateNovelPipeline.LOCATION] = "Die Begegnung der beiden Haputcharaktere spielt sich in einem Caffee ab. ";
                return PipelineJobState.COMPLETED;
            }
            if (result.Contains("1"))
            {
                pipeline.GetMemory()[GenerateNovelPipeline.BACKGROUND_SPRITE] = "1";
                pipeline.GetMemory()[GenerateNovelPipeline.LOCATION] = "Die Begegnung der beiden Haputcharaktere spielt sich in einem Büro ab. ";
                return PipelineJobState.COMPLETED;
            }
            return PipelineJobState.FAILED;
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
        stringBuilder.Append("Deine Aufgabe besteht darin, einen passenden Ort für die Handlung der Geschichte auszuwählen, die du gerade erstellst. Zur Auswahl hast du eine Liste von Orten, die weiter unten im Prompt zu finden ist. Außerdem findest du in diesem Prompt eine Beschreibung der Geschichte, die du gerade erstellst, sowie eine Beschreibung der beiden Personen, die in dieser Geschichte miteinander interagiren. Wähle den Ort, der am besten für die Begegnung der beiden Haupt-Charaktere geeignet ist. Lege bei deiner Entscheidung weniger Wert darauf dass die Hauptfigur eine Gründerin ist, und mehr Wert darauf wie die Hauptfigur zu den zweiten Hauptfigur in Verbindung steht. Wenn keiner der Orte aus der Liste passt, dann wähle den Ort aus, der am ehesten passt. Erfinde keine neuen Orte! Wähle von den vorgegebenen Orten! Triff aufjedenfall eine Entscheidung!");
        stringBuilder.AppendLine();

        //
        stringBuilder.Append("In der Geschichte, die du gerade erzeugst, geht es um folgendes: " + pipeline.GetMemory()[GenerateNovelPipeline.CONTEXT] + ". ");
        stringBuilder.Append("Der Name des Hauptcharakters in der Geschichte: " + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_MAIN_CHARACTER] + ". ");
        stringBuilder.Append("Die Rolle des Hauptcharakters in der Geschichte: Der Hauptcharakter ist eine Gründerin. ");
        stringBuilder.Append("Der Name des Gesprächspartners oder der Gesprächspartnerin des Hauptcharakters in der Geschichte: " + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_TALKING_PARTNER] + ". ");
        stringBuilder.Append("Die Rolle des Gesprächspartners oder der Gesprächspartnerin des Hauptcharakters in der Geschichte: " + pipeline.GetMemory()[GenerateNovelPipeline.ROLE_OF_TALKING_PARTNER] + ". ");
        stringBuilder.AppendLine();

        // Output Format
        stringBuilder.Append("Das gewünschte Ergebnis:");
        stringBuilder.AppendLine();
        stringBuilder.Append("Bitte gib als Ergebnis genau die Zahl des Ortes an, die du ausgesucht hast. Bitte beachte, dass dein generiertes Ergebnis von einer speziellen Software weiterverarbeitet wird. Daher ist es entscheidend, dass du das Ergebnis in eckigen Klammern zurückgibst. Deine Antwort sollte direkt mit einer öffnenden eckigen Klammer '[' beginnen und mit einer schließenden eckigen Klammer ']' enden. Beispiel: [Von dir ausgesuchte Zahl]. Diese Formatierung ermöglicht eine reibungslose Integration und Verarbeitung deiner Ausgabe durch das nachgelagerte System.");
        stringBuilder.AppendLine();

        //
        stringBuilder.Append("--Hier die Liste mit Orten--\r\n1. ein Büro\r\n2. ein Caffee\r\n3. das Zuhause einer Person\r\n4. ein Park\r\n5. eine staatliche Behörde\r\n6. ein Einzelhandel\r\n7. eine öffentliche Veranstaltung\r\n--Liste Ende--");
        stringBuilder.AppendLine();

        prompt = stringBuilder.ToString();
    }
}
