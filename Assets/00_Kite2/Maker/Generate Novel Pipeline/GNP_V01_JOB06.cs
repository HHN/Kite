using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine;

public class GNP_V01_JOB06 : PipelineJob
{
    public GNP_V01_JOB06()
    {
        this.jobName = "Job06 - Aussuchen des Repräsentationsbildes";
    }

    public override PipelineJobState HandleCompletion(string completion)
    {
        Match match = Regex.Match(completion, @"\[(.*?)\]");
        if (match.Success)
        {
            string result = match.Groups[1].Value;

            if (result.Contains("10"))
            {
                pipeline.GetMemory()[GenerateNovelPipeline.IMAGE] = "9";
                return PipelineJobState.COMPLETED;
            }
            if (result.Contains("9"))
            {
                pipeline.GetMemory()[GenerateNovelPipeline.IMAGE] = "8";
                return PipelineJobState.COMPLETED;
            }
            if (result.Contains("8"))
            {
                pipeline.GetMemory()[GenerateNovelPipeline.IMAGE] = "7";
                return PipelineJobState.COMPLETED;
            }
            if (result.Contains("7"))
            {
                pipeline.GetMemory()[GenerateNovelPipeline.IMAGE] = "6";
                return PipelineJobState.COMPLETED;
            }
            if (result.Contains("6"))
            {
                pipeline.GetMemory()[GenerateNovelPipeline.IMAGE] = "5";
                return PipelineJobState.COMPLETED;
            }
            if (result.Contains("5"))
            {
                pipeline.GetMemory()[GenerateNovelPipeline.IMAGE] = "4";
                return PipelineJobState.COMPLETED;
            }
            if (result.Contains("4"))
            {
                pipeline.GetMemory()[GenerateNovelPipeline.IMAGE] = "3";
                return PipelineJobState.COMPLETED;
            }
            if (result.Contains("3"))
            {
                pipeline.GetMemory()[GenerateNovelPipeline.IMAGE] = "2";
                return PipelineJobState.COMPLETED;
            }
            if (result.Contains("2"))
            {
                pipeline.GetMemory()[GenerateNovelPipeline.IMAGE] = "1";
                return PipelineJobState.COMPLETED;
            }
            if (result.Contains("1"))
            {
                pipeline.GetMemory()[GenerateNovelPipeline.IMAGE] = "0";
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
        stringBuilder.Append("Du bist eine fortschrittliche KI, spezialisiert auf die Generierung von Visual Novels, die den Gründungsprozess von Unternehmerinnen thematisieren. Dein Hauptaugenmerk liegt dabei auf der authentischen Darstellung von Herausforderungen und Diskriminierungen, denen Frauen im Gründungsprozess begegnen. Ziel ist es, Bewusstsein und Verständnis für die spezifischen Hürden zu schaffen, die Gründerinnen aufgrund ihres Geschlechts überwinden müssen. Deine Visual Novels sollen nicht nur informieren, sondern auch inspirieren und zum Nachdenken anregen.");
        stringBuilder.AppendLine();

        // Aufgabe
        stringBuilder.Append("Deine Aufgabe:");
        stringBuilder.AppendLine();
        stringBuilder.Append("Deine Aufgabe besteht darin, eine passendes Repräsentations-Bild für die Visual Novel auszuwählen, die du gerade erstellst. Zur Auswahl hast du eine Liste von Bildern, die weiter unten im Prompt zu finden ist. Außerdem findest du in diesem Prompt eine Beschreibung der Geschichte, die du gerade erstellst, sowie eine Beschreibung der beiden Personen, die in dieser Geschichte miteinander interagiren. Wähle das Bild, das am besten dazu geeignet ist, die Visual Novel zu repräsentieren. Lege bei deiner Entscheidung weniger Wert darauf dass die Hauptfigur eine Gründerin ist, und mehr Wert darauf wie die Hauptfigur mit der zweiten Hauptfigur in Verbindung steht und worum es in ihrer Interaktion geht. Wenn keines der Bilder aus der Liste passt, dann wähle das Bild aus, der am ehesten passt. Erfinde keine neuen Bilder! Wähle von den vorgegebenen Bildern! Triff aufjedenfall eine Entscheidung!");
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
        stringBuilder.Append("Bitte gib als Ergebnis genau die Zahl des Bildes an, das du ausgesucht hast. Bitte beachte, dass dein generiertes Ergebnis von einer speziellen Software weiterverarbeitet wird. Daher ist es entscheidend, dass du das Ergebnis in eckigen Klammern zurückgibst. Deine Antwort sollte direkt mit einer öffnenden eckigen Klammer '[' beginnen und mit einer schließenden eckigen Klammer ']' enden. Beispiel: [Von dir ausgesuchte Zahl]. Diese Formatierung ermöglicht eine reibungslose Integration und Verarbeitung deiner Ausgabe durch das nachgelagerte System.");
        stringBuilder.AppendLine();

        //
        stringBuilder.Append("--Hier die Liste mit Bildern--\r\n1. ein Bankgebäude (Finanzi-Institut)\r\n2. eine Hand, die ein Smartphone hält, auf das gerade ein Anruf von 'Mama' eingeht\r\n3. eine Hand, die ein Mikrofon hält\r\n4. ein Siegel und ein Stift (sieht amtlich aus)\r\n5. ein Bankgebäude (Finanzi-Institut) in Verbindung mit Papierunterlagen und Geld\r\n6. ein Büro-Schlüssel\r\n7. Ordner (für Papierkram)\r\n8. die Agentur für Arbeit\r\n9. Zwei Kaffe-Tassen\r\n10. Sprechblasen\r\n--Liste Ende--");
        stringBuilder.AppendLine();

        prompt = stringBuilder.ToString();
    }
}
