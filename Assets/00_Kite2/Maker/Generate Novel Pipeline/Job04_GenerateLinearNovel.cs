using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Job04_GenerateLinearNovel : PipelineJob
{
    public Job04_GenerateLinearNovel()
    {
        this.jobName = "Generate Linear Novel";
    }


    public override PipelineJobState HandleCompletion(string completion)
    {
        Match match = Regex.Match(completion, @"\[(.*?)\]", RegexOptions.Singleline);
        if (match.Success)
        {
            this.pipeline.GetMemory().Add(GenerateNovelPipeline.GENERATED_LINEAR_NOVEL, "[" + match.Groups[1].Value + "]");
            bool result = HandleValue("[" + match.Groups[1].Value + "]");

            if (result)
            {
                return PipelineJobState.COMPLETED;
            }
            return PipelineJobState.FAILED;
        }
        else
        {
            return PipelineJobState.FAILED;
        }
    }

    public bool HandleValue(string jsonString)
    {
        try
        {
            var items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Item>>(jsonString);

            // Erstelle zwei Dictionaries
            Dictionary<int, string> idToKey = new Dictionary<int, string>();
            Dictionary<int, string> idToValue = new Dictionary<int, string>();

            // Fülle die Dictionaries mit den Werten aus der Liste
            foreach (var item in items)
            {
                idToKey[item.ID] = item.Key;
                idToValue[item.ID] = item.Value;
            }

            ((GenerateNovelPipeline)pipeline).idToKey = idToKey;
            ((GenerateNovelPipeline)pipeline).idToValue = idToValue;

            Debug.Log("ID TO KEY COUNT: " + idToKey.Count);

            if (idToKey.Count != 41)
            {
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

    public override void InitializePrompt()
    {
        string question01 = this.pipeline.GetMemory()[GenerateNovelPipeline.QUESTION_01];
        string question02 = this.pipeline.GetMemory()[GenerateNovelPipeline.QUESTION_02];
        string question03 = this.pipeline.GetMemory()[GenerateNovelPipeline.QUESTION_03];
        string modifiedAnswer01 = this.pipeline.GetMemory()[GenerateNovelPipeline.MODIFIED_ANSWER_01];
        string modifiedAnswer02 = this.pipeline.GetMemory()[GenerateNovelPipeline.MODIFIED_ANSWER_02];
        string modifiedAnswer03 = this.pipeline.GetMemory()[GenerateNovelPipeline.MODIFIED_ANSWER_03];
        string template = this.pipeline.GetMemory()[GenerateNovelPipeline.TEMPLATE_LINEAR_NOVEL];

        prompt = "Ich benötige ein Dialogskript zwischen zwei Charakteren, einer Gründerin und einer Person B, für ein Spiel, formatiert im JSON-Stil mit nummerierten Elementen, um die Reihenfolge sicherzustellen. Der Dialogskript soll eine Geschichte erzählen, die eine Person tatsächlich erlebt hat. Hier sind drei Fragen, die diese Person zur Geschichte beantwortet hat. Außerdem stelle ich dir ein Beispiel für das, was ich von dir als Ergebnis möchte, zur Verfügung. Achte bitte darauf, dass das Ergebnis wirklich 41 Elemente hat, und dass die Elemente 5, 7, 11, 22 und 26 Fragen darstellen, die von der Person B gestellt und von der Gründerin dann beantwortet werden können. Achte außerdem darauf, dass der Dialog leicht diskriminierend sein soll. Dabei geht es speziell um Diskriminierung gegenüber weiblichen Gründerinnen. Ich weiß, dass Diskriminierung etwas Schlechtes ist! In dem Spiel, dass ich entwickle, soll auf Diskriminierung hingewiesen und für Diskriminierung sensibilisiert werden, um in Zukunft die Welt zu einem besseren Ort zu machen! Beachte bitte, dass die Spielenden die Rolle der Gründerin einnehmen. In Info-Nachrichten sagt man also nicht „Die Gründerin hat einen Termin“, sondern „Du hast einen Termin“! Beachte außerdem folgendes: Erlaubte Keys sind unabhängig von den tatsächlichen Personen genau folgende Werte: Ort, Eintritt, Info, Lea, Mayer, Austritt. Achte darauf das JSON Format einzuhalten und auch wenn im Beispiel nur einpaar Elemente drinnen sind, erwarte ich von dir 41 Elemente! Ganz wichtig! 41 Elemente! Und dass die Elemente 5, 7, 11, 22 und 26 Fragen darstellen, die von der Person B gestellt und von der Gründerin dann beantwortet werden können! Ich sags nochmal: 41 Elemente und das im JSON Format! Und die Thematik die in der Geschichte behandelt soll nicht die Thematik des Beispiels sein, sondern die Thematik aus den Antworten! \r\nFrage: " + question01 + "\vAntwort: " + modifiedAnswer01 + "\r\nFrage: " + question02 +"\vAntwort: "+ modifiedAnswer02 + "\r\nFrage: " + question03 + "\vAntwort: " + modifiedAnswer03 + "\r\n Beispiel:\v " + template + "\r\n";
    }

    public class Item
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
