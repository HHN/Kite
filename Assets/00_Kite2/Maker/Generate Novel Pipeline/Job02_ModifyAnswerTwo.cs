using System.Text.RegularExpressions;
using UnityEngine;

public class Job02_ModifyAnswerTwo : PipelineJob
{
    public Job02_ModifyAnswerTwo()
    {
        this.jobName = "Modfiy Answer Two";
    }

    public override PipelineJobState HandleCompletion(string completion)
    {
        Match match = Regex.Match(completion, @"\[(.*?)\]");
        if (match.Success)
        {
            this.pipeline.GetMemory().Add(GenerateNovelPipeline.MODIFIED_ANSWER_02, match.Groups[1].Value);
            return PipelineJobState.COMPLETED;
        }
        else
        {
            return PipelineJobState.FAILED;
        }
    }

    public override void InitializePrompt()
    {
        string question = this.pipeline.GetMemory()[GenerateNovelPipeline.QUESTION_02];
        string answer = this.pipeline.GetMemory()[GenerateNovelPipeline.ANSWER_02];
        string templateAnswer = this.pipeline.GetMemory()[GenerateNovelPipeline.TEMPLATE_ANSWER_02];

        string template = "Gegeben ist eine Frage: '{question}' und die darauf gegebene Antwort einer Person: '{original_answer}'. Hier ist auch eine Muster-Antwort zu der gleichen Frage, die einen spezifischen Stil und Aussagekraft hat: '{sample_answer}'. Bitte passe die originale Antwort so an, dass sie dem Stil und der Aussagekraft der Muster-Antwort entspricht, ohne jedoch den Inhalt der Muster-Antwort zu übernehmen. Das Ergebnis soll in eckigen Klammern dargestellt werden.";
        prompt = template.Replace("{question}", question).Replace("{original_answer}", answer).Replace("{sample_answer}", templateAnswer);

        //prompt = "ChatGPT, im Rahmen meiner Visual Novel-App habe ich einer nutzenden Person die folgende Frage gestellt: \r\n" + question + "\r\nDie nutzende Person hat darauf mit dieser Antwort reagiert:\r\n" + answer + "\r\nBitte formuliere diese Antwort so um, dass sie dem Stil der folgenden Muster-Antwort entspricht:\r\n" + templateAnswer + "\r\nDie umformulierte Antwort soll den Kern der ursprünglichen Spielerantwort beibehalten, aber in der Struktur und dem Ton der Muster-Antwort angepasst sein, um ein konsistentes Nutzungserlebnis zu gewährleisten. Die umformulierte Antwort soll nicht Inhalte aus der Muster-Antwort übernehmen! Achte außerdem auf die eckigen Klammern, damit die modifizierte Antwort von der Software erkannt wird!\r\n";
    }
}
