using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine;

public class GNP_V01_JOB04 : PipelineJob
{
    public GNP_V01_JOB04()
    {
        this.jobName = "Job04 - Bestimmung des Namen des Gesprächspartners.";
    }

    public override PipelineJobState HandleCompletion(string completion)
    {
        Match match = Regex.Match(completion, @"\[(.*?)\]");
        if (match.Success)
        {
            string result = match.Groups[1].Value;

            pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_TALKING_PARTNER] = result;

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
        stringBuilder.Append("Deine Aufgabe ist es, einen Vor- und Nachnamen für den Gesprächspartner der Hauptfigur in der von dir erstellten Visual Novel zu bestimmen. Wichtig ist, dass du das Ergebnis direkt und ausschließlich als Namen angibst, ohne zusätzlichen Satz oder Erklärung. Beispiel: [Vorname Nachname]. Berücksichtige dabei das Geschlecht des/der Gesprächspartner*in. Achte darauf, dass der Name nicht mit dem des Hauptcharakters übereinstimmt. Wenn allerdings der*die Gesprächspartner*in mit dem Hauptcharakter verwandt ist, dann ist derselbe Nachname erwünscht.");
        stringBuilder.AppendLine();

        // Kontext
        stringBuilder.Append("Der Kontext deiner Aufgabe:");
        stringBuilder.AppendLine();
        stringBuilder.Append("In der Novel, die du gerade erzeugst, geht es um folgendes: " + pipeline.GetMemory()[GenerateNovelPipeline.CONTEXT]);
        stringBuilder.Append("Der Name des Hauptcharakters in der Novel: [" + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_MAIN_CHARACTER] + "]");
        stringBuilder.Append("Die Rolle des Hauptcharakters in der Novel: Der Hauptcharakter ist eine Gründerin.");
        stringBuilder.Append("Die Rolle des Gesprächspartners oder der Gesprächspartnerin des Hauptcharakters in der Novel: " + pipeline.GetMemory()[GenerateNovelPipeline.ROLE_OF_TALKING_PARTNER]);
        stringBuilder.AppendLine();

        // Output Format
        stringBuilder.Append("Das gewünschte Ergebnis:");
        stringBuilder.AppendLine();
        stringBuilder.Append("Bitte beachte, dass dein generiertes Ergebnis von einer speziellen Software weiterverarbeitet wird. Daher ist es entscheidend, dass du das Ergebnis in eckigen Klammern zurückgibst. Deine Antwort sollte direkt mit einer öffnenden eckigen Klammer '[' beginnen und mit einer schließenden eckigen Klammer ']' enden. Beispiel: [Vorname Nachname]. Diese Formatierung ermöglicht eine reibungslose Integration und Verarbeitung deiner Ausgabe durch das nachgelagerte System. Bitte lasse zusätzliche beschreibende Informationen weg und gebe nur die gewünschte Antwort!");
        stringBuilder.AppendLine();

        prompt = stringBuilder.ToString();
    }
}
