using Assets._Scripts.Managers;
using Assets._Scripts.Server_Communication;

namespace Assets._Scripts.Player
{
    public class GptRequestEventOnSuccessHandler : IOnSuccessHandler
    {
        public string VariablesNameForGptPrompt;
        public IGptCompletionHandler CompletionHandler;

        public void OnSuccess(Response response)
        {
            string completion = response.GetCompletion();
            string processedCompletion = completion;

            if (CompletionHandler != null)
            {
                processedCompletion = CompletionHandler.ProcessCompletion(completion);
            }

            if (PlayManager.Instance().GetVisualNovelToPlay().IsVariableExistent(VariablesNameForGptPrompt))
            {
                PlayManager.Instance().GetVisualNovelToPlay().RemoveGlobalVariable(VariablesNameForGptPrompt);
            }

            PlayManager.Instance().GetVisualNovelToPlay()
                .AddGlobalVariable(VariablesNameForGptPrompt, processedCompletion);
        }
    }
}