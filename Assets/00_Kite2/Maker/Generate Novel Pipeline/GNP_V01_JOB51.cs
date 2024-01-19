using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine;
using System;

public class GNP_V01_JOB51 : PipelineJob
{
    public GNP_V01_JOB51()
    {
        this.jobName = "Job51";
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

        stringBuilder.Append("Deine Rolle: Du bist eine fortschrittliche KI, spezialisiert auf die Generierung von Dialogen für Visual Novels, die den Gründungsprozess von Unternehmerinnen thematisieren. Dein Hauptaugenmerk liegt auf der authentischen Darstellung von Herausforderungen und Diskriminierungen, denen Frauen im Gründungsprozess begegnen.");
        stringBuilder.AppendLine();
        stringBuilder.Append("Deine Aufgabe: Du sollst einen Dialogskript für eine Visual Novel erstellen, wobei ich dir die ersten 25 Nachrichten des Dialogs vorgebe. Ab der 26. Nachricht sollst du den Dialog fortführen, und somit also nur das Ende des Dialogs schreiben. Deine Aufgabe ist es, den Dialog so zu gestalten, dass er ein witziges und spielerisches Ende nimmt. Dies soll den freudigen Aspekt dieser Geschichte unterstreichen.");
        stringBuilder.AppendLine();
        stringBuilder.Append("Das gewünschte Ergebnis: Formuliere den restlichen Dialog im JSON-Stil, sodass er insgesamt 30 Elemente hat, wobei die ersten 25 von mir vorgegeben sind. Der Redeanteil beider Charaktere sollte ausgeglichen sein. Gib das Ergebnis in eckigen Klammern an, um eine reibungslose Integration in das nachgelagerte System zu gewährleisten.");
        stringBuilder.AppendLine();
        stringBuilder.Append("Hier ist der Dialog:");
        stringBuilder.AppendLine("[");
        stringBuilder.AppendLine("{\"id\": \"1\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_01] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_01] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"2\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_02] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_02] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"3\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_03] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_03] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"4\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_MAIN_CHARACTER] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_04_OPTION_01] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"5\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_05] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_05] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"6\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_MAIN_CHARACTER] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_06_OPTION_01] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"7\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_07] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_07] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"8\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_08] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_08] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"9\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_09] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_09] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"10\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_MAIN_CHARACTER] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_10_OPTION_01] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"11\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_11] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_11] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"12\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_12] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_12] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"13\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_13] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_13] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"14\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_14] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_14] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"15\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_15] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_15] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"16\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_16] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_16] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"17\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_17] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_17] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"18\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_18] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_18] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"19\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_19] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_19] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"20\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_20] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_20] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"21\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_MAIN_CHARACTER] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_21_OPTION_01] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"22\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_22] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_22] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"23\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_23] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_23] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"24\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_24] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_24] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"25\", \"key\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.NAME_OF_MAIN_CHARACTER] + "\", \"value\": \"" + this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_25_OPTION_02] + "\"},");
        stringBuilder.AppendLine("{\"id\": \"26\", \"key\": \"VON DIR AUSZUFÜLLEN\", \"value\": \"VON DIR AUSZUFÜLLEN\"},");
        stringBuilder.AppendLine("{\"id\": \"27\", \"key\": \"VON DIR AUSZUFÜLLEN\", \"value\": \"VON DIR AUSZUFÜLLEN\"},");
        stringBuilder.AppendLine("{\"id\": \"28\", \"key\": \"VON DIR AUSZUFÜLLEN\", \"value\": \"VON DIR AUSZUFÜLLEN\"},");
        stringBuilder.AppendLine("{\"id\": \"29\", \"key\": \"VON DIR AUSZUFÜLLEN\", \"value\": \"VON DIR AUSZUFÜLLEN\"},");
        stringBuilder.AppendLine("{\"id\": \"30\", \"key\": \"VON DIR AUSZUFÜLLEN\", \"value\": \"VON DIR AUSZUFÜLLEN\"}");
        stringBuilder.AppendLine("]");

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

                PutResultsIntoMemory(id, key, value);

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
        string defaultExpressionType = "4";
        switch (id)
        {
            case 26:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_26_ALTERNATIV_02] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_26_ALTERNATIV_02] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_26_ALTERNATIV_02] = defaultExpressionType;
                    return true;
                }
            case 27:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_27_ALTERNATIV_02] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_27_ALTERNATIV_02] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_27_ALTERNATIV_02] = defaultExpressionType;
                    return true;
                }
            case 28:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_28_ALTERNATIV_02] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_28_ALTERNATIV_02] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_28_ALTERNATIV_02] = defaultExpressionType;
                    return true;
                }
            case 29:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_29_ALTERNATIV_02] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_29_ALTERNATIV_02] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_29_ALTERNATIV_02] = defaultExpressionType;
                    return true;
                }
            case 30:
                {
                    this.pipeline.GetMemory()[GenerateNovelPipeline.MESSAGE_30_ALTERNATIV_02] = value;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.SPEAKER_30_ALTERNATIV_02] = key;
                    this.pipeline.GetMemory()[GenerateNovelPipeline.EXPRESSION_TYPE_30_ALTERNATIV_02] = defaultExpressionType;
                    return true;
                }
        }
        return false;
    }
}
