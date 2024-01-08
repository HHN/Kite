using System.Text.RegularExpressions;
using System;
using UnityEngine;

public class Job06_GenerateDialogOptionsOne : PipelineJob
{
    public Job06_GenerateDialogOptionsOne()
    {
        this.jobName = "Generate Dialog Option 01";
    }

    public override PipelineJobState HandleCompletion(string completion)
    {
        try
        {
            string pattern = @"[\[\]{}]";
            string cleanedInput = Regex.Replace(completion, pattern, "");
            string[] splitString = cleanedInput.Split(new string[] { "„", "\"", "," }, StringSplitOptions.RemoveEmptyEntries);

            string str1 = splitString[1].Trim();
            string str2 = splitString[3].Trim();
            string str3 = splitString[5].Trim();

            this.pipeline.GetMemory().Add(GenerateNovelPipeline.ALTERNATIVE_ANSWER_01_01, str1);
            this.pipeline.GetMemory().Add(GenerateNovelPipeline.ALTERNATIVE_ANSWER_01_02, str2);
            this.pipeline.GetMemory().Add(GenerateNovelPipeline.ALTERNATIVE_ANSWER_01_03, str3);

            return PipelineJobState.COMPLETED;
        }
        catch
        {
            Debug.Log("Failure with Completion: " + completion);
            return PipelineJobState.FAILED;
        }
    }

    public override void InitializePrompt()
    {
        string shortSummary = this.pipeline.GetMemory()[GenerateNovelPipeline.SHORT_SUMMARY];
        string text01 = ((GenerateNovelPipeline) this.pipeline).idToValue[5];
        string text02 = ((GenerateNovelPipeline)this.pipeline).idToValue[6];
        string text03 = ((GenerateNovelPipeline)this.pipeline).idToValue[7];

        prompt = "Ich habe folgende Geschichte: " + shortSummary + ". \r\nIn einer Szene haben Person A und Person B folgenden Dialog:\r\nPerson A: „" + text01 + "“\r\nPerson B: „" + text02 + "“\r\nPerson A: „" + text03 + "“\r\nKannst du mir bitte drei alternative Antworten von Person B im JSON-Format generieren? Es ist wichtig dass du das Format einhälts, damit die Software mit deiner Completion umgehen kann. Deine Antwort soll ungefähr so aussehen. Schreib nicht „Antwort:“ oder „Person B“ dazu! Bedenke: Es sind Alternative Antwort-Möglichekeiten Für Person B gefragt und nicht etwa möglichkeiten wie man den Dialog Fortführen könnte. Achte auch darauf, dass die verschiedenen Antwort-Möglichkeiten einen unterschiedlichen Charakter haben, und nicht etwa nur umformulierte Versionen der ursprünglichen Antwort sind. \r\n\r\n[\r\n  { „Ja\" },\r\n  { „Nein\" },\r\n  { „Vielleicht\" }\r\n]\r\n";
    }
}
