namespace Assets._Scripts.Player
{
    /// <summary>
    /// Defines an interface for classes that handle the processing of
    /// completion strings received from a GPT (Generative Pre-trained Transformer) model.
    /// Implementations of this interface can define custom logic for cleaning,
    /// formatting, or further interpreting AI-generated text.
    /// </summary>
    public interface IGptCompletionHandler
    {
        /// <summary>
        /// Processes a raw completion string obtained from a GPT model.
        /// </summary>
        /// <param name="completion">The raw string completion to be processed.</param>
        /// <returns>The processed and potentially modified completion string.</returns>
        string ProcessCompletion(string completion);
    }
}