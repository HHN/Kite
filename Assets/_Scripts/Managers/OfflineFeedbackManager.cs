using System.Text;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// Implements a simple, thread-safe Singleton pattern for managing feedback collected during offline usage.
    /// </summary>
    /// <remarks>
    /// This manager uses a <c>StringBuilder</c> internally to aggregate feedback lines and provides methods 
    /// for accessing the aggregated string, adding new lines, and clearing the collected data.
    /// It is designed to be the single point of access for managing temporary offline feedback data.
    /// </remarks>
    public class OfflineFeedbackManager
    {
        private static OfflineFeedbackManager _instance;
        private StringBuilder _offlineFeedback;

        private OfflineFeedbackManager()
        {
        }

        /// <summary>
        /// Provides the single instance of the <c>OfflineFeedbackManager</c>, creating it if it does not already exist.
        /// </summary>
        /// <returns>The thread-safe singleton instance of the manager.</returns>
        public static OfflineFeedbackManager Instance()
        {
            _instance ??= new OfflineFeedbackManager();

            return _instance;
        }

        /// <summary>
        /// Retrieves the accumulated offline feedback as a trimmed string.
        /// </summary>
        /// <returns>The aggregated string content of all feedback lines, or an empty string (<c>""</c>) if no feedback has been added.</returns>
        public string GetOfflineFeedback()
        {
            return _offlineFeedback == null ? "" : _offlineFeedback.ToString().Trim();
        }
        
        /// <summary>
        /// Appends a new line of text to the internal feedback buffer.
        /// </summary>
        /// <param name="line">The string line to be appended to the offline feedback.</param>
        public void AddLineToPrompt(string line)
        {
            _offlineFeedback ??= new StringBuilder();

            _offlineFeedback.AppendLine(line);
        }

        /// <summary>
        /// Clears the internal feedback buffer by setting the underlying <c>StringBuilder</c> to <c>null</c>.
        /// </summary>
        public void Clear()
        {
            _offlineFeedback = null;
        }
    }
}