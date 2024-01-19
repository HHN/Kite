using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine;
using System;

public class GNP_V01_JOB36 : PipelineJob
{
    public GNP_V01_JOB36()
    {
        this.jobName = "Job36 - Gesichtsausdruck Nachricht";
    }

    public override PipelineJobState HandleCompletion(string completion)
    {
        Match match = Regex.Match(completion, @"\[(.*?)\]");
        if (match.Success)
        {
            string result = match.Groups[1].Value;

            int intValue = 0;

            bool parseable = int.TryParse(result, out intValue);

            if (parseable)
            {
                pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_24] = result;
                return PipelineJobState.COMPLETED;
            }
            else
            {
                Console.WriteLine("Wrong returnvalue;");
                return PipelineJobState.FAILED;
            }
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
        stringBuilder.Append("Deine Rolle: Du bist eine fortschrittliche KI, spezialisiert auf die Generierung von Visual Novels, die den Gründungsprozess von Unternehmerinnen thematisieren. Dein Hauptaugenmerk liegt dabei auf der authentischen Darstellung von Herausforderungen und Diskriminierungen, denen Frauen im Gründungsprozess begegnen. Ziel ist es, Bewusstsein und Verständnis für die spezifischen Hürden zu schaffen, die Gründerinnen aufgrund ihres Geschlechts überwinden müssen. Deine Visual Novels sollen nicht nur informieren, sondern auch inspirieren und zum Nachdenken anregen. ");
        stringBuilder.AppendLine();

        // Aufgabe
        stringBuilder.Append("Ich gebe dir jetzt eine Dialog-Nachricht aus einem Dialog zwischen zwei Personen. ");
        stringBuilder.AppendLine();

        stringBuilder.Append("Die erste Person ist der Hauptcharakter und heißt " + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_MAIN_CHARACTER] + ". ");
        stringBuilder.Append("Der Hauptcharakter ist eine Gründerin. ");
        stringBuilder.Append("Die zweite Person ist der zweite Hauptcharakter und Gesprächspartner der Gründerin und heißt: " + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_TALKING_PARTNER] + ". ");
        stringBuilder.Append("Der zweite Hauptcharakter hat folgende Rolle: " + pipeline.GetMemory()[GenerateNovelPipeline.ROLE_OF_TALKING_PARTNER] + ". ");
        stringBuilder.Append(pipeline.GetMemory()[GenerateNovelPipeline.LOCATION]);
        stringBuilder.Append("Die Geschichte hat folgenden Titel: " + pipeline.GetMemory()[GenerateNovelPipeline.TITLE] + ". ");
        stringBuilder.Append("Das ist die Kurzbeschreibung der Geschichte: " + pipeline.GetMemory()[GenerateNovelPipeline.DESCRIPTION] + ". ");
        stringBuilder.Append("In der Geschichte geht es um folgendes: " + pipeline.GetMemory()[GenerateNovelPipeline.CONTEXT] + ". ");

        stringBuilder.AppendLine();
        stringBuilder.Append("Ich gebe dir jetzt genau die eine Nachricht und dazu eine Liste von Gesichtsausdrücken die die sprechende Person gehabt haben könnte, während sie diese Nachricht aussprach. Bitte such den passenden Gesichtsausdruck aus.");
        stringBuilder.AppendLine();

        // Output Format
        stringBuilder.Append("Das gewünschte Ergebnis:");
        stringBuilder.AppendLine();
        stringBuilder.Append("Bitte gib als Ergebnis genau die Zahl des Gesichtsausdruckes an, die du ausgesucht hast. Gib keinen zusätzlichen Text oder ähnliches dazu. Bitte beachte, dass dein generiertes Ergebnis von einer speziellen Software weiterverarbeitet wird. Daher ist es entscheidend, dass du das Ergebnis in eckigen Klammern zurückgibst. Deine Antwort sollte direkt mit einer öffnenden eckigen Klammer '[' beginnen und mit einer schließenden eckigen Klammer ']' enden. Beispiel: [Von dir ausgesuchte Zahl]. Diese Formatierung ermöglicht eine reibungslose Integration und Verarbeitung deiner Ausgabe durch das nachgelagerte System.");
        stringBuilder.AppendLine();

        stringBuilder.Append("Hier die fragliche Nachricht: " + pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_24]);
        stringBuilder.Append("Die Nachricht wurde gesprochen von: " + pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_24]);
        stringBuilder.AppendLine();
        stringBuilder.Append("--Hier die Liste mit Gesichtsausdrücken --1. entspannt; 2. staunend; 3.sich weigernd; 4. lächelnd; 5. freundlich; 6. lachend; 7. kritisch; 8.verneinend; 9.glücklich; 10. stolz; 11.erschrocken; 12.fragend; 13.besiegt;--Liste Ende--");
        stringBuilder.AppendLine();

        prompt = stringBuilder.ToString();
    }
}
