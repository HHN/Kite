using System.Text.RegularExpressions;
using UnityEngine;

public class Job05_SummarizeNovel : PipelineJob
{
    public Job05_SummarizeNovel()
    {
        this.jobName = "Summarize Novel";
    }


    public override PipelineJobState HandleCompletion(string completion)
    {
        Match match = Regex.Match(completion, @"\[(.*?)\]");
        if (match.Success)
        {
            this.pipeline.GetMemory().Add(GenerateNovelPipeline.SHORT_SUMMARY, match.Groups[1].Value);
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
        string linearNovel = this.pipeline.GetMemory()[GenerateNovelPipeline.GENERATED_LINEAR_NOVEL];
        prompt = "Bitte fasse folgende Geschichte in 3 bis 4 Sätzen zusammen. Liefere mir das Ergebnis in eckigen Klammern. Beispiel: [Das hier ist eine Zusammenfassung]. Geschichte: " + linearNovel;
    }
}
