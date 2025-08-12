namespace Assets._Scripts.Player
{
    /// <summary>
    /// Provides a default implementation for processing a GPT (Generative Pre-trained Transformer) completion string.
    /// This handler primarily focuses on basic string manipulation like trimming whitespace.
    /// It implements the <see cref="IGptCompletionHandler"/> interface.
    /// </summary>
    public class DefaultGptCompletionHandler : IGptCompletionHandler
    {
        /// <summary>
        /// Processes the raw completion string received from a GPT model.
        /// This default implementation simply trims leading and trailing whitespace from the string.
        /// </summary>
        /// <param name="completion">The raw string completion received from the GPT model.</param>
        /// <returns>The processed string completion, with leading and trailing whitespace removed.</returns>
        public string ProcessCompletion(string completion)
        {
            return completion.Trim();
        }
    }
}