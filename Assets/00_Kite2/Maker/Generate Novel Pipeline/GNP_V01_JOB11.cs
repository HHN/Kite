using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine;
using System;

public class GNP_V01_JOB11 : PipelineJob
{
    public GNP_V01_JOB11()
    {
        this.jobName = "Job11 - Nachbessern des linearen Gesprächs";
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
        stringBuilder.Append("Deine Aufgabe ist es einen Dialogskript zu überarbeiten. ");
        stringBuilder.Append("Du erhältst von mir einen Dialog, den du überarbeiten sollst, um ihn spannender und flüssiger zu gestalten und Füllworte zu entfernen. Bei der Überarbeitung des Dialogs ist es besonders wichtig, dass dieser schlüssig, sinnvoll und realistisch bleibt. Achte darauf, dass der Dialog direkt zum Punkt kommt, ohne unnötig um den heißen Brei zu reden. Jeder Satz sollte eine klare Bedeutung haben und so formuliert sein, dass er auch in einem realen Gespräch stattfinden könnte. Vermeide inhaltslose oder redundante Sätze, die nicht zum Fortgang oder zur Vertiefung der Handlung oder Charakterentwicklung beitragen. Falls Namen im Dialog vorkommen, so achte darauf dass es die richtigen Namen der Charaktere sind! Berücksichtige folgende Punkte, die du beim Überarbeiten des Dialogs respektieren sollst: ");
        stringBuilder.AppendLine();
        stringBuilder.Append("Der Dialog findet zwischen Zwei Charakteren statt. ");
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
        stringBuilder.Append("Bitte beachte, dass der Dialog ein gewisses Format hat. Dieses sollte beibehalten werden. Hier Infos zum Format: dein generiertes Ergebnis von einer speziellen Software weiterverarbeitet wird. Daher ist es entscheidend, dass du das Ergebnis in eckigen Klammern zurückgibst. Deine Antwort sollte direkt mit einer öffnenden eckigen Klammer '[' beginnen und mit einer schließenden eckigen Klammer ']' enden. Beispiel: [Dein generiertes Ergebnis]. Diese Formatierung ermöglicht eine reibungslose Integration und Verarbeitung deiner Ausgabe durch das nachgelagerte System. ");
        stringBuilder.Append("Ich benötige den Dialog im JSON-Stil. Der Dialog soll exakt 30 Elemente haben. Jedes Element soll dabei folgendermaßen aufgbaut sein: [\r\n{\"id\": \"NUMMER\", \"key\": \"KEY\", \"value\": \"TEXT\"},\r\n...,\r\n{\"id\": \"NUMMER\", \"key\": \"KEY\", \"value\": \"TEXT\"}\r\n]. Erlaubte Keys sind dabei entweder der exakte Name der ersten Hauptfigur, oder der exakte Name der zweiten Hauptfigur oder das Wort 'info'. Info-Nachrichten sollten sich dabei an den ersten Hauptcharakter wenden (die Gründerin) und in der Du-Form geschrieben sein. Die ID Nummer soll eine fortlaufende Nummerierung der ELemente sein, angefangen bei 1 und aufhörend bei 30. Achte außerdem darauf dass die Elemente nummer 3, 5, 9, 20 und 24 Fragen sein sollen, die die zweite Hauptfigur an die erste Hauptfigur stellt. Dementsprechend sollen die Elemente 4, 6, 10, 21 und 25 Antworten sein, die der Hauptcharakter (Die Gründerin) auf die Fragen gibt. ");
        stringBuilder.Append("Der Rede-Anteil der beiden Charaktere im Dialog sollte dabei ungefähr ausgeglichen sein.");
        stringBuilder.AppendLine();

        // Analyse Objekt
        stringBuilder.Append("Hier ist der Dialog den du überarbeiten sollst:");
        stringBuilder.Append(this.pipeline.GetMemory()[GenerateNovelPipeline.FIRST_JSON_SCRIPT]);

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

            if (items.Count != 30)
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

                bool succeededToPut = PutResultsIntoMemory(id, key, value);

                if (!succeededToPut)
                {
                    Debug.Log("Wrong id: " + id);
                    return false;
                }

                this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_WHILE_JOINING] = "0";
                this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_WHILE_LEAVING] = "0";
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

    public bool PutResultsIntoMemory(int id, string key, string value)
    {
        string defaultExpressionType = "0";
        switch (id)
        {
            case 1:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_01] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_01] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_01] = defaultExpressionType;
                    return true;
                }
            case 2:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_02] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_02] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_02] = defaultExpressionType;
                    return true;
                }
            case 3:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_03] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_03] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_03] = defaultExpressionType;
                    return true;
                }
            case 4:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_04_OPTION_01] = value;
                    return true;
                }
            case 5:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_05] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_05] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_05] = defaultExpressionType;
                    return true;
                }
            case 6:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_06_OPTION_01] = value;
                    return true;
                }
            case 7:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_07] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_07] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_07] = defaultExpressionType;
                    return true;
                }
            case 8:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_08] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_08] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_08] = defaultExpressionType;
                    return true;
                }
            case 9:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_09] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_09] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_09] = defaultExpressionType;
                    return true;
                }
            case 10:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_10_OPTION_01] = value;
                    return true;
                }
            case 11:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_11] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_11] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_11] = defaultExpressionType;
                    return true;
                }
            case 12:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_12] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_12] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_12] = defaultExpressionType;
                    return true;
                }
            case 13:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_13] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_13] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_13] = defaultExpressionType;
                    return true;
                }
            case 14:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_14] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_14] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_14] = defaultExpressionType;
                    return true;
                }
            case 15:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_15] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_15] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_15] = defaultExpressionType;
                    return true;
                }
            case 16:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_16] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_16] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_16] = defaultExpressionType;
                    return true;
                }
            case 17:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_17] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_17] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_17] = defaultExpressionType;
                    return true;
                }
            case 18:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_18] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_18] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_18] = defaultExpressionType;
                    return true;
                }
            case 19:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_19] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_19] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_19] = defaultExpressionType;
                    return true;
                }
            case 20:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_20] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_20] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_20] = defaultExpressionType;
                    return true;
                }
            case 21:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_21_OPTION_01] = value;
                    return true;
                }
            case 22:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_22] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_22] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_22] = defaultExpressionType;
                    return true;
                }
            case 23:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_23] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_23] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_23] = defaultExpressionType;
                    return true;
                }
            case 24:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_24] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_24] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_24] = defaultExpressionType;
                    return true;
                }
            case 25:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_25_OPTION_01] = value;
                    return true;
                }
            case 26:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_26_ALTERNATIV_01] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_26_ALTERNATIV_01] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_26_ALTERNATIV_01] = defaultExpressionType;
                    return true;
                }
            case 27:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_27_ALTERNATIV_01] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_27_ALTERNATIV_01] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_27_ALTERNATIV_01] = defaultExpressionType;
                    return true;
                }
            case 28:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_28_ALTERNATIV_01] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_28_ALTERNATIV_01] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_28_ALTERNATIV_01] = defaultExpressionType;
                    return true;
                }
            case 29:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_29_ALTERNATIV_01] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_29_ALTERNATIV_01] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_29_ALTERNATIV_01] = defaultExpressionType;
                    return true;
                }
            case 30:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_30_ALTERNATIV_01] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_30_ALTERNATIV_01] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_30_ALTERNATIV_01] = defaultExpressionType;
                    return true;
                }
        }
        return false;
    }
}
