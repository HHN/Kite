using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine;
using System;

public class GNP_V01_JOB13 : PipelineJob
{
    public GNP_V01_JOB13()
    {
        this.jobName = "Job13 - Erzeugung von alternativen Antworten an der zweiten Frage Stelle.";
    }

    public override PipelineJobState HandleCompletion(string completion)
    {
        Match match = Regex.Match(completion, @"\[(.*?)\]", RegexOptions.Singleline);
        if (match.Success)
        {
            string result = match.Groups[1].Value;

            bool valid = HandleReturnValue("[" + result + "]");

            if (valid)
            {
                this.pipeline.GetMemory()[GenerateNovelPipeline.SECOND_JSON_SCRIPT] = "[" + result + "]";
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
        stringBuilder.Append("Deine Rolle: Du bist eine fortschrittliche KI, spezialisiert auf die Generierung von Visual Novels, die den Gründungsprozess von Unternehmerinnen thematisieren. Dein Hauptaugenmerk liegt dabei auf der authentischen Darstellung von Herausforderungen und Diskriminierungen, denen Frauen im Gründungsprozess begegnen. Ziel ist es, Bewusstsein und Verständnis für die spezifischen Hürden zu schaffen, die Gründerinnen aufgrund ihres Geschlechts überwinden müssen. Deine Visual Novels sollen nicht nur informieren, sondern auch inspirieren und zum Nachdenken anregen. ");
        stringBuilder.AppendLine();

        // Aufgabe
        stringBuilder.Append("Ich gebe dir jetzt einen kurzen Dialog zwischen zwei Personen. ");
        stringBuilder.AppendLine();

        stringBuilder.Append("Die erste Person ist der Hauptcharakter und heißt " + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_MAIN_CHARACTER] + ". ");
        stringBuilder.Append("Der Hauptcharakter ist eine Gründerin. ");
        stringBuilder.Append("Die zweite Person ist der zweite Hauptcharakter und Gesprächspartner der Gründerin und heißt: " + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_TALKING_PARTNER] + ". ");
        stringBuilder.Append("Der zweite Hauptcharakter hat folgende Rolle: " + pipeline.GetMemory()[GenerateNovelPipeline.ROLE_OF_TALKING_PARTNER] + ". ");
        stringBuilder.Append(pipeline.GetMemory()[GenerateNovelPipeline.LOCATION]);
        stringBuilder.Append("Die Geschichte hat folgenden Titel: " + pipeline.GetMemory()[GenerateNovelPipeline.TITLE] + ". ");
        stringBuilder.Append("Das ist die Kurzbeschreibung der Geschichte: " + pipeline.GetMemory()[GenerateNovelPipeline.DESCRIPTION] + ". ");
        stringBuilder.Append("In der Geschichte geht es um folgendes: " + pipeline.GetMemory()[GenerateNovelPipeline.CONTEXT] + ". ");

        stringBuilder.Append("Deine Aufgabe: Ich gebe dir jetzt einen kurzen Dialog zwischen " + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_MAIN_CHARACTER] + " und " + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_TALKING_PARTNER] + ". Dabei fragt zunächst " + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_TALKING_PARTNER] + " die Gründerin etwas und die Gründerin antwortet darauf. Im Anschluss führt " + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_TALKING_PARTNER] + " das Gespräch fort. Was ich von dir will ist, dass du dir alternative Antwort-Möglichkeiten der Gründerin überlegst. Liefere mir 3 weitere Antwort-Möglichkeiten! Formuliere die Antwort-Möglichkeiten so, dass das Gespräch trotzdem wie hier im Dialog zu sehen fortgeführt werden kann! Du sollst von den 3 Sätzen die ich dir gebe also nur für den mittleren Alternativen überlegen und der erste und der drite Satz müssen trotzdem zu deiner Alternative passen!");
        // Output Format
        stringBuilder.Append("Das gewünschte Ergebnis: Bitte beachte, dass dein generiertes Ergebnis von einer speziellen Software weiterverarbeitet wird. Daher ist es entscheidend, dass du das Ergebnis in eckigen Klammern zurückgibst. Diese Formatierung ermöglicht eine reibungslose Integration und Verarbeitung deiner Ausgabe durch das nachgelagerte System.");
        stringBuilder.AppendLine();

        // Output Format
        stringBuilder.Append("Das gewünschte Ergebnis:");
        stringBuilder.AppendLine();
        stringBuilder.Append("Dein generiertes Ergebnis von einer speziellen Software weiterverarbeitet wird. Daher ist es entscheidend, dass du das Ergebnis in eckigen Klammern zurückgibst. Deine Antwort sollte direkt mit einer öffnenden eckigen Klammer '[' beginnen und mit einer schließenden eckigen Klammer ']' enden. Beispiel: [Dein generiertes Ergebnis]. Diese Formatierung ermöglicht eine reibungslose Integration und Verarbeitung deiner Ausgabe durch das nachgelagerte System. ");
        stringBuilder.Append("Ich benötige die Dialog-Optionen im JSON-Stil. Gib mir genau 3 weitere Antwort-Optionen. Jedes Element soll dabei folgendermaßen aufgbaut sein: [\r\n{\"id\": \"1\", \"value\": \"TEXT\"},{\"id\": \"2\", \"value\": \"TEXT\"},\r\n{\"id\": \"3\", \"value\": \"TEXT\"}\r\n]. Die erste Antwort-Option soll dabei einen negativen Charakter haben. Die zweite Antwort-Option soll einen aufgeregten Charakter haben. Die dritte Antwort-Option soll einen verwunderten Charakter haben.");
        stringBuilder.AppendLine();

        // Analyse Objekt
        stringBuilder.Append("Hier der fragliche Dialog:");
        stringBuilder.AppendLine();
        stringBuilder.Append(pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_TALKING_PARTNER] + ": " + pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_05]);
        stringBuilder.Append(pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_MAIN_CHARACTER] + ": " + pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_06_OPTION_01]);
        stringBuilder.Append(pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_TALKING_PARTNER] + ": " + pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_07]);

        prompt = stringBuilder.ToString();
    }

    public bool HandleReturnValue(string jsonString)
    {
        try
        {
            var items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Element>>(jsonString);

            if (items.Count != 3)
            {
                Debug.Log("Not the right amount of elements in dialogue Generation. Count: " + items.Count);
                return false;
            }

            foreach (var item in items)
            {
                int id = item.id;
                string value = item.Value;

                if (id == 1)
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_06_OPTION_02] = value;
                }
                else if (id == 2)
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_06_OPTION_03] = value;
                }
                else if (id == 3)
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_06_OPTION_04] = value;
                }
                else
                {
                    Debug.Log("Wrong ID : " + id);
                    return false;
                }
            }
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }

    public class Element
    {
        public int id { get; set; }
        public string Value { get; set; }
    }
}
