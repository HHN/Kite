using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine;
using System;

public class GNP_V01_JOB12 : PipelineJob
{
    public GNP_V01_JOB12()
    {
        this.jobName = "Job12 - Erzeugung von alternativen Antworten an der ersten Frage Stelle.";
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

        stringBuilder.Append("Du bist eine fortschrittliche KI, spezialisiert auf die Generierung von Visual Novels, die den Gründungsprozess von Unternehmerinnen thematisieren. Dein Hauptaugenmerk liegt dabei auf der authentischen Darstellung von Herausforderungen und Diskriminierungen, denen Frauen im Gründungsprozess begegnen. Ziel ist es, Bewusstsein und Verständnis für die spezifischen Hürden zu schaffen, die Gründerinnen aufgrund ihres Geschlechts überwinden müssen. Deine Visual Novels sollen nicht nur informieren, sondern auch inspirieren und zum Nachdenken anregen. Die Geschichten, die du erstellst, sollen aber auch unabhängig von der Thematik Geschichten sein, die Sinn machen und sich gut lesen lassen.");
        stringBuilder.AppendLine();
        stringBuilder.Append("Ich werde dir einen Dialog zur Verfügung stellen, der eine Lücke enthält. Deine Aufgabe ist es, für diese Lücke 3 verschiedene Sätze vorzuschlagen, die jeweils eine andere Stimmung widerspiegeln. Dabei soll ein Satz witzig sein, einer verärgert und einer verunsichert. Wichtig ist, dass die Sätze nicht nur unterschiedliche Stimmungen, sondern auch unterschiedliche Inhalte aufweisen. Sie müssen so formuliert sein, dass sie den Dialog als Ganzes schlüssig und sinnvoll erscheinen lassen, wenn man ihn liest.");
        stringBuilder.AppendLine();
        stringBuilder.Append("Der Dialog ist ein Gespräch zwischen einer Gründerin namens " + this.pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_MAIN_CHARACTER] + " und einer weiteren Person. Die weitere Person ist: " + this.pipeline.GetMemory()[GenerateNovelPipeline.ROLE_OF_TALKING_PARTNER] + " Der Name der weiteren Person ist " + this.pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_TALKING_PARTNER] + ". " + this.pipeline.GetMemory()[GenerateNovelPipeline.CONTEXT]);
        stringBuilder.AppendLine();
        stringBuilder.Append("Dein generiertes Ergebnis wird von einer speziellen Software weiterverarbeitet. Daher ist es entscheidend, dass du das Ergebnis in eckigen Klammern zurückgibst. Deine Antwort sollte direkt mit einer öffnenden eckigen Klammer '[' beginnen und mit einer schließenden eckigen Klammer ']' enden. Beispiel: [Dein generiertes Ergebnis]. Diese Formatierung ermöglicht eine reibungslose Integration und Verarbeitung deiner Ausgabe durch das nachgelagerte System. Ich benötige die Dialog-Optionen im JSON-Stil. Gib mir genau 3 weitere Antwort-Optionen. Jedes Element soll dabei folgendermaßen aufgbaut sein: [\r\n{\"id\": \"1\", \"value\": \"TEXT\"},{\"id\": \"2\", \"value\": \"TEXT\"},\r\n{\"id\": \"3\", \"value\": \"TEXT\"}\r\n].");
        stringBuilder.AppendLine();
        stringBuilder.Append("Der Dialog lautet wie folgt: ");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine(this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_01] + ": " + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_01]);
        stringBuilder.AppendLine(this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_02] + ": " + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_02]);
        stringBuilder.AppendLine(this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_03] + ": " + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_03]);
        stringBuilder.AppendLine(this.pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_MAIN_CHARACTER] + ": [Lücke die du auffüllen sollst]");
        stringBuilder.AppendLine();
        stringBuilder.Append("Bitte gib für die Lücke im Dialog die 3 vorgeschlagenen Sätze an, wobei jeder Satz eine der genannten Stimmungen repräsentiert und dabei einen eigenständigen Inhalt beiträgt.");

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
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_04_OPTION_02] = value;
                } 
                else if (id == 2)
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_04_OPTION_03] = value;
                }
                else if (id == 3)
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_04_OPTION_04] = value;
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
