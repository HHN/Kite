using Assets._Scripts.Managers;
using Assets._Scripts.ServerCommunication;

namespace Assets._Scripts.Player
{
    /// <summary>
    /// Handles the successful response from a GPT (Generative Pre-trained Transformer) request.
    /// It processes the AI completion, optionally using a specific completion handler,
    /// and then stores the result in a global variable within the active visual novel.
    /// This class implements the <see cref="IOnSuccessHandler"/> interface.
    /// </summary>
    public class GptRequestEventOnSuccessHandler : IOnSuccessHandler
    {
        public string VariablesNameForGptPrompt;
        public IGptCompletionHandler CompletionHandler;

        /// <summary>
        /// Called when a GPT request successfully receives a response.
        /// This method extracts the completion, processes it (if a <see cref="CompletionHandler"/> is provided),
        /// and updates a global variable in the current visual novel with the processed completion.
        /// </summary>
        /// <param name="response">The <see cref="Response"/> object containing the GPT completion and other data.</param>
        public void OnSuccess(Response response)
        {
            string completion = response.GetCompletion();
            string processedCompletion = completion;

            // If a custom CompletionHandler is provided, use it to process the completion.
            if (CompletionHandler != null)
            {
                processedCompletion = CompletionHandler.ProcessCompletion(completion);
            }

            // Check if the target global variable already exists and remove it to ensure it's updated cleanly.
            if (PlayManager.Instance().GetVisualNovelToPlay().IsVariableExistent(VariablesNameForGptPrompt))
            {
                PlayManager.Instance().GetVisualNovelToPlay().RemoveGlobalVariable(VariablesNameForGptPrompt);
            }

            // Add or update the global variable in the current visual novel with the processed completion.
            PlayManager.Instance().GetVisualNovelToPlay()
                .AddGlobalVariable(VariablesNameForGptPrompt, processedCompletion);
        }
    }
}