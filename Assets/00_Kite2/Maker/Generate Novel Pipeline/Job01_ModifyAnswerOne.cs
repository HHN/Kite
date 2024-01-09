using UnityEngine;
using System.Text.RegularExpressions;

public class Job01_ModifyAnswerOne : PipelineJob
{
    public Job01_ModifyAnswerOne() 
    {
        this.jobName = "Modfiy Answer One";
    }

    public override PipelineJobState HandleCompletion(string completion)
    {
        Match match = Regex.Match(completion, @"\[(.*?)\]");
        if (match.Success)
        {
            this.pipeline.GetMemory().Add(GenerateNovelPipeline.MODIFIED_ANSWER_01, match.Groups[1].Value);
            return PipelineJobState.COMPLETED;
        }
        else
        {
            return PipelineJobState.FAILED;
        }
    }

    public override void InitializePrompt()
    {
        string question = this.pipeline.GetMemory()[GenerateNovelPipeline.QUESTION_01];
        string answer = this.pipeline.GetMemory()[GenerateNovelPipeline.ANSWER_01];
        string templateAnswer = this.pipeline.GetMemory()[GenerateNovelPipeline.TEMPLATE_ANSWER_01];
        prompt = "ChatGPT, im Rahmen meiner Visual Novel-App habe ich einer nutzenden Person die folgende Frage gestellt: \r\n" + question + "\r\nDie nutzende Person hat darauf mit dieser Antwort reagiert:\r\n" + answer + "\r\nBitte formuliere diese Antwort so um, dass sie dem Stil und Inhalt der folgenden Muster-Antwort entspricht:\r\n" + templateAnswer + "\r\nDie umformulierte Antwort soll den Kern der ursprünglichen Spielerantwort beibehalten, aber in der Struktur und dem Ton der Muster-Antwort angepasst sein, um ein konsistentes Nutzungserlebnis zu gewährleisten. Achte außerdem auf die eckigen Klammern, damit die modifizierte Antwort von der Software erkannt wird!\r\n";
    }
}
