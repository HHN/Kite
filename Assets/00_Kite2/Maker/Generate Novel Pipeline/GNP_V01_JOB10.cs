using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine;
using System;

public class GNP_V01_JOB10 : PipelineJob
{
    public GNP_V01_JOB10()
    {
        this.jobName = "Job10 - Erzeugen des linearen Gesprächs";
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
                this.pipeline.GetMemory()[GenerateNovelPipeline.FIRST_JSON_SCRIPT] = "[" + result + "]";
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
        stringBuilder.Append("Deine Aufgabe ist es einen Dialogskript zu erstellen. Der Dialog findet zwischen Zwei Charakteren statt. ");
        stringBuilder.Append("Der Hauptcharakter heißt " + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_MAIN_CHARACTER] + ". ");
        stringBuilder.Append("Der Hauptcharakter ist eine Gründerin. ");
        stringBuilder.Append("Der zweite Hauptcharakter und Gesprächspartner der Gründerin heißt: " + pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_TALKING_PARTNER] + ". ");
        stringBuilder.Append("Der zweite Hauptcharakter hat folgende Rolle: " + pipeline.GetMemory()[GenerateNovelPipeline.ROLE_OF_TALKING_PARTNER] + ". ");
        stringBuilder.Append(pipeline.GetMemory()[GenerateNovelPipeline.LOCATION]);
        stringBuilder.Append("Die Geschichte hat folgenden Titel: " + pipeline.GetMemory()[GenerateNovelPipeline.TITLE] + ". ");
        stringBuilder.Append("Das ist die Kurzbeschreibung der Geschichte: " + pipeline.GetMemory()[GenerateNovelPipeline.DESCRIPTION] + ". ");
        stringBuilder.Append("In der Geschichte geht es um folgendes: " + pipeline.GetMemory()[GenerateNovelPipeline.CONTEXT] + ". ");
        stringBuilder.AppendLine();

        // Output Format
        stringBuilder.Append("Das gewünschte Ergebnis:");
        stringBuilder.AppendLine();
        stringBuilder.Append("Bitte beachte, dass dein generiertes Ergebnis von einer speziellen Software weiterverarbeitet wird. Daher ist es entscheidend, dass du das Ergebnis in eckigen Klammern zurückgibst. Deine Antwort sollte direkt mit einer öffnenden eckigen Klammer '[' beginnen und mit einer schließenden eckigen Klammer ']' enden. Beispiel: [Dein generiertes Ergebnis]. Diese Formatierung ermöglicht eine reibungslose Integration und Verarbeitung deiner Ausgabe durch das nachgelagerte System. ");
        stringBuilder.Append("Ich benötige den Dialog im JSON-Stil. Der Dialog soll exakt 38 Elemente haben. Jedes Element soll dabei folgendermaßen aufgbaut sein: [\r\n{\"id\": \"NUMMER\", \"key\": \"KEY\", \"value\": \"TEXT\"},\r\n...,\r\n{\"id\": \"NUMMER\", \"key\": \"KEY\", \"value\": \"TEXT\"}\r\n]. Erlaubte Keys sind dabei entweder der exakte Name der ersten Hauptfigur, oder der exakte Name der zweiten Hauptfigur oder das Wort 'info'. Info-Nachrichten sollten sich dabei an den ersten Hauptcharakter wenden (die Gründerin) und in der Du-Form geschrieben sein. Die ID Nummer soll eine fortlaufende Nummerierung der ELemente sein, angefangen bei 1 und aufhörend bei 38. Achte außerdem darauf dass die Elemente nummer 3, 5, 9, 20 und 24 Fragen sein sollen, die die zweite Hauptfigur an die erste Hauptfigur stellt. Dementsprechend sollen die Elemente 4, 6, 10, 21 und 25 Antworten sein, die der Hauptcharakter (Die Gründerin) auf die Fragen gibt. ");
        stringBuilder.Append("Der Rede-Anteil der beiden Charaktere im Dialog sollte dabei ungefähr ausgeglichen sein.");
        stringBuilder.AppendLine();

        prompt = stringBuilder.ToString();
    }

    public bool HandleReturnValue(string jsonString)
    {
        string mainCharacter = pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_MAIN_CHARACTER];
        string secondCharacter = pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_TALKING_PARTNER];

        int numberOfMainCharakterText = 0;
        int numberOfSecondCharakterText = 0;
        int numberOfInfoText = 0;

        try
        {
            var items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Element>>(jsonString);

            if (items.Count != 38)
            {
                Debug.Log("Not the right amount of elements in dialogue Generation. Count: " + items.Count);
                return false;
            }

            foreach (var item in items)
            {
                int id = item.id;
                string key = item.Key;
                string value = item.Value;

                if (key == mainCharacter) 
                {
                    numberOfMainCharakterText++;
                } 
                else if (key == secondCharacter)
                {
                    numberOfSecondCharakterText++;
                } 
                else if (key == "info")
                {
                    numberOfInfoText++;
                } 
                else
                {
                    Debug.Log("Wrong key. Key: " + key);
                    return false;
                }
            }
            if (numberOfMainCharakterText > 23 || numberOfSecondCharakterText > 23 || numberOfInfoText > 23)
            {
                Debug.Log("Bad distribution of talking parts (to much): Main / Second / Info: " + numberOfMainCharakterText + " / " + numberOfSecondCharakterText + " / " + numberOfInfoText);
                return false;
            }

            if (numberOfMainCharakterText < 10 || numberOfSecondCharakterText < 10)
            {
                Debug.Log("Bad distribution of talking parts (not enough): Main / Second / Info: " + numberOfMainCharakterText + " / " + numberOfSecondCharakterText + " / " + numberOfInfoText);
                return false;
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
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
