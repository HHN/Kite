using System.Text.RegularExpressions;
using UnityEngine;

public class Job14_GenerateDescription : PipelineJob
{
    public Job14_GenerateDescription()
    {
        this.jobName = "Generate Description";
    }

    public override PipelineJobState HandleCompletion(string completion)
    {
        Match match = Regex.Match(completion, @"\[(.*?)\]");
        if (match.Success)
        {
            this.pipeline.GetMemory().Add(GenerateNovelPipeline.GENERATED_DESCRIPTION, match.Groups[1].Value);
            return PipelineJobState.COMPLETED;
        }
        else
        {
            Debug.Log("Failure with Completion: " + completion);
            return PipelineJobState.FAILED;
        }
    }

    public override void InitializePrompt()
    {
        string title = this.pipeline.GetMemory()[GenerateNovelPipeline.GENERATED_TITLE];
        string shortSummary = this.pipeline.GetMemory()[GenerateNovelPipeline.SHORT_SUMMARY];
        string descriptionTemplate = this.pipeline.GetMemory()[GenerateNovelPipeline.TEMPLATE_DESCRIPTION];
        prompt = "Ich habe eine Geschichte mit folgendem Titel: " + title + ".\r\nHier ist worum es in der Geschichte geht: " + shortSummary + "\r\nStell dir vor, dass die Geschichte ein Spiel ist, dass man spielen kann. Das Spiel umfasst dabei genau die Geschichte! Nicht mehr und nicht weniger! Versprich also nichts, was nicht in der Geschichte selbst vorhanden ist. Formuliere eine Kurzbeschreibung hierfür in 2 bis 3 Sätzen. Bitte gib mir das Ergebnis in eckigen Klammern, damit die Software damit umgehen kann. Hier ist ein Muster an dem du dich orientieren kannst: " + descriptionTemplate + "\r\n";
    }
}
