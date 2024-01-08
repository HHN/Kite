using UnityEngine;
using System.Text.RegularExpressions;

public class Job13_GenerateTitle : PipelineJob
{
    public Job13_GenerateTitle()
    {
        this.jobName = "Generate title";
    }

    public override PipelineJobState HandleCompletion(string completion)
    {
        Match match = Regex.Match(completion, @"\[(.*?)\]");
        if (match.Success)
        {
            this.pipeline.GetMemory().Add(GenerateNovelPipeline.GENERATED_TITLE, match.Groups[1].Value);
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
        string story = this.pipeline.GetMemory()[GenerateNovelPipeline.GENERATED_LINEAR_NOVEL];
        prompt = "Ich gebe dir eine Geschichte, die sich um eine signifikante Interaktion oder ein wichtiges Gespräch zwischen Hauptcharakteren dreht. Der eine Hauptcharakter ist eine Gründerin. Der Kern der Geschichte liegt in dem Austausch zwischen der Gründerin und einer anderen Person, sei es ein beratendes Gespräch, eine hitzige Diskussion oder ein entscheidender Moment des Verständnisses. Bitte erstelle einen Titel, der kurz und prägnant ist. Wenn man den Titel liest, soll man wissen wer der Gesprächspartner der Gründerin ist. Alternativ soll man wissen wo die Gründern hingeht oder was die Gründerin macht. Bitte nicht mehr als 3 oder 4 Worte. Am besten aber nur ein Wort! Bitte gib mir das Ergebnis in eckigen Klammern, damit die Software damit umgehen kann\r\nGeschichte: " + story + ".\r\n";
    }
}
