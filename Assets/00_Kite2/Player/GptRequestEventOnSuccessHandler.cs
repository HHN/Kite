using _00_Kite2.Common.Managers;

public class GptRequestEventOnSuccessHandler : OnSuccessHandler
{
    public string variablesNameForGptPromp;
    public GptCompletionHandler completionHandler;

    public void OnSuccess(Response response)
    {
        string completion = response.GetCompletion();
        string processedCompletion = completion;

        if (completionHandler != null)
        {
            processedCompletion = completionHandler.ProcessCompletion(completion);
        }
        if (PlayManager.Instance().GetVisualNovelToPlay().IsVariableExistend(variablesNameForGptPromp))
        {
            PlayManager.Instance().GetVisualNovelToPlay().RemoveGlobalVariable(variablesNameForGptPromp);
        }
        PlayManager.Instance().GetVisualNovelToPlay().AddGlobalVariable(variablesNameForGptPromp, processedCompletion);
    }
}
