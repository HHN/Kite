using UnityEngine;

public class Job11_ChooseLocation : PipelineJob
{
    public Job11_ChooseLocation()
    {
        this.jobName = "Choose Location";
    }

    public override PipelineJobState HandleCompletion(string completion)
    {
        if (completion.Contains("1")) 
        {
            this.pipeline.GetMemory().Add(GenerateNovelPipeline.CHOSEN_LOCATION, "1");
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
        string list = this.pipeline.GetMemory()[GenerateNovelPipeline.LIST_OF_LOCATIONS];
        string descripton = this.pipeline.GetMemory()[GenerateNovelPipeline.SHORT_SUMMARY];

        prompt = "Ich gebe dir eine nummerierte Liste von Orten und eine Beschreibung einer Geschichte und du suchst den Ort aus, der am Besten zu der Geschichte passt. Wenn kein Ort passen sollte, so suche den aus, der am Besten passt. Nenne als Ergebnis die Zahl des Ortes.\r\nListe: " + list + "\r\nBeschreibung: " + descripton + "\r\n";
    }
}
