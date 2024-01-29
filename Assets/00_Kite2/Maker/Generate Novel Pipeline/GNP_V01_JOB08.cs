using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine;

public class GNP_V01_JOB08 : PipelineJob
{
    public GNP_V01_JOB08()
    {
        this.jobName = "Job08 - Generierung einer Beschreibung";
    }

    public override PipelineJobState HandleCompletion(string completion)
    {
        Match match = Regex.Match(completion, @"\[(.*?)\]");
        if (match.Success)
        {
            string result = match.Groups[1].Value;

            pipeline.GetMemory()[GenerateNovelPipeline.DESCRIPTION] = result;

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

        stringBuilder.Append("Deine Aufgabe ist es, eine kurze Beschreibung für die Visual Novel '");
        stringBuilder.Append(pipeline.GetMemory()[GenerateNovelPipeline.TITLE]);
        stringBuilder.Append("' zu generieren, in der die Hauptfigur ");
        stringBuilder.Append(pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_MAIN_CHARACTER]);
        stringBuilder.Append(", eine Gründerin, und ");
        stringBuilder.Append(pipeline.GetMemory()[GenerateNovelPipeline.ROLE_OF_TALKING_PARTNER]);
        stringBuilder.Append("eine Schlüsselinteraktion haben. Die Beschreibung sollte 3 bis 5 Sätze lang sein und in der 'Du'-Form formuliert werden, als ob du direkt mit der Gründerin sprichst. Die Beschreibung soll die Gedanken und Pläne der Gründerin vor dem Treffen darstellen, ohne Charakternamen und ohne Vorgreifen auf Ereignisse. Verwende keine Charaktereigenschaften und orientiere dich an dem Stil der Beispiel-beschreibungen, aber übernehme keine Inhalte daraus. Die Sprache der Beschreibung ist Deutsch. ");

        // Output Format
        stringBuilder.Append("Das gewünschte Ergebnis:");
        stringBuilder.AppendLine();
        stringBuilder.Append("Bitte gib als Ergebnis genau die Beschreibung an, die du generiert hast. Bitte beachte, dass dein generiertes Ergebnis von einer speziellen Software weiterverarbeitet wird. Daher ist es entscheidend, dass du das Ergebnis in eckigen Klammern zurückgibst. Deine Antwort sollte direkt mit einer öffnenden eckigen Klammer '[' beginnen und mit einer schließenden eckigen Klammer ']' enden. Beispiel: [die von dir generierte Beschreibung]. Diese Formatierung ermöglicht eine reibungslose Integration und Verarbeitung deiner Ausgabe durch das nachgelagerte System.");
        stringBuilder.AppendLine();

        //
        stringBuilder.Append("--Hier die Liste mit Beispielen für Beschreibungen--\r\n1. [Du hast eine Einladung zu einem Bankgespräch erhalten, um mehr Informationen über die Eröffnung eines Bankkontos zu erhalten und dieses darauf zu beantragen.]\r\n2. [Du hast eine Einladung zu einem Bankgespräch erhalten, um mehr Informationen über einen Kredit zu erhalten und diesen darauf zu beantragen.]\r\n3. [Du triffst dich mit einer Notarin, um einen Termin für deine Gründung auszumachen.]\r\n4. [Nachdem du dich gründlich informiert hast, hast du dich dazu entschlossen ein Unternehmen zu gründen. Du hast dir auch schon die nächsten Schritte überlegt und rufst nun deine Eltern an, um ihnen von deinem Gründungsvorhaben zu berichten.]\r\n5. [Du triffst dich mit einem*einer Bekannten, den*die du seit ein paar Jahren nicht mehr gesehen hast, in einem kleinen Café. Er*Sie hat dir soeben erzählt, wo er*sie zurzeit beruflich steht. Nun bist du an der Reihe.]\r\n6. [Du bist in Kontakt mit einem Kunden gekommen. Nachdem geklärt wurde, dass deine angebotenen Leistungen zu den Anforderungen deines Gegenübers passen, geht es nun um die Honorarverhandlung.]\r\n7. [Du wurdest zu einem Termin bei der Agentur für Arbeit eingeladen, wo du dich mit einem Berater über deine Geschäftsidee unterhalten kannst und hoffentlich Informationen zu passenden Förderungen erhalten wirst.]\r\n8. [Du befindest dich auf einer Veranstaltung, bei der Jungunternehmer*innen ihre Geschäftsidee vor einem Publikum präsentieren können, um Rückmeldung zu der Idee zu erhalten und zu networken. Nachdem du deine Geschäftsidee vor dem Publikum gepitcht hast, stellst du dich an einen Tisch mit anderen Gästen, um mit ihnen zu reden.]\r\n9. [Du hast heute einen Termin für die Besichtigung von Büroräumen.]\r\n10. [Du bist heute bei der örtlichen Agentur für Arbeit, um einen Gründerzuschuss zu beantragen.]\r\n--Liste Ende--");
        stringBuilder.AppendLine();

        prompt = stringBuilder.ToString();
    }
}
