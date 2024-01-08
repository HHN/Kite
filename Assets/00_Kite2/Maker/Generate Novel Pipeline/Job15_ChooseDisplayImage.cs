using UnityEngine;

public class Job15_ChooseDisplayImage : PipelineJob
{
    public Job15_ChooseDisplayImage()
    {
        this.jobName = "Choose Display Image";
    }

    public override PipelineJobState HandleCompletion(string completion)
    {
        if (completion.Contains("8"))
        {
            this.pipeline.GetMemory().Add(GenerateNovelPipeline.CHOSEN_DISPLAY_IMAGE, "8");
            return PipelineJobState.COMPLETED;
        }
        if (completion.Contains("7"))
        {
            this.pipeline.GetMemory().Add(GenerateNovelPipeline.CHOSEN_DISPLAY_IMAGE, "7");
            return PipelineJobState.COMPLETED;
        }
        if (completion.Contains("6"))
        {
            this.pipeline.GetMemory().Add(GenerateNovelPipeline.CHOSEN_DISPLAY_IMAGE, "0");
            return PipelineJobState.COMPLETED;
        }
        if (completion.Contains("5"))
        {
            this.pipeline.GetMemory().Add(GenerateNovelPipeline.CHOSEN_DISPLAY_IMAGE, "5");
            return PipelineJobState.COMPLETED;
        }
        if (completion.Contains("4"))
        {
            this.pipeline.GetMemory().Add(GenerateNovelPipeline.CHOSEN_DISPLAY_IMAGE, "6");
            return PipelineJobState.COMPLETED;
        }
        if (completion.Contains("3"))
        {
            this.pipeline.GetMemory().Add(GenerateNovelPipeline.CHOSEN_DISPLAY_IMAGE, "3");
            return PipelineJobState.COMPLETED;
        }
        if (completion.Contains("2"))
        {
            this.pipeline.GetMemory().Add(GenerateNovelPipeline.CHOSEN_DISPLAY_IMAGE, "2");
            return PipelineJobState.COMPLETED;
        }
        if (completion.Contains("1"))
        {
            this.pipeline.GetMemory().Add(GenerateNovelPipeline.CHOSEN_DISPLAY_IMAGE, "1");
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
        string listOfDisplayImages = this.pipeline.GetMemory()[GenerateNovelPipeline.LIST_OF_DISPLAY_IMAGES];
        string title = this.pipeline.GetMemory()[GenerateNovelPipeline.GENERATED_TITLE];
        string description = this.pipeline.GetMemory()[GenerateNovelPipeline.GENERATED_DESCRIPTION];
        prompt = "Ich beschreibe dir jetzt eine nummerierte Liste von Bildern und gebe dir dann den Titel und die Beschreibung einer Geschichte. Bitte suche dir das passende Bild aus um die Geschichte zu repräsentieren. Gib mir die Zahl des Bildes, dass du ausgesucht hast, damit die Software damit umgehen kann.\vListe an Bildern: " + listOfDisplayImages + "\r\nTitel: " + title + "\r\nBeschreibung: " + description + "\r\n";
    }
}
