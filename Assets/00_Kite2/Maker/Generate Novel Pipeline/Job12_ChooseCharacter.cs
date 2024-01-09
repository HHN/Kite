using UnityEngine;

public class Job12_ChooseCharacter : PipelineJob
{
    public Job12_ChooseCharacter()
    {
        this.jobName = "Choose Character";
    }

    public override PipelineJobState HandleCompletion(string completion)
    {
        if (completion.Contains("1"))
        {
            this.pipeline.GetMemory().Add(GenerateNovelPipeline.CHOSEN_CHARACTER, "1");
            return PipelineJobState.COMPLETED;
        }
        else
        {
            return PipelineJobState.FAILED;
        }
    }

    public override void InitializePrompt()
    {
        string list = this.pipeline.GetMemory()[GenerateNovelPipeline.LIST_OF_CHARACTERS];
        string descripton = this.pipeline.GetMemory()[GenerateNovelPipeline.SHORT_SUMMARY];

        prompt = "Ich gebe dir eine nummerierte Liste von Characteren und eine Beschreibung einer Geschichte und du suchst den Character aus, der am Besten als Gesprächspartner zu der Geschichte passt. Wenn kein Character passen sollte, so suche den aus, der am Besten passt. Nenne als Ergebnis die Zahl des Characters.\r\nListe: " + list + "\r\nBeschreibung: " + descripton + "\r\n";
    }
}
