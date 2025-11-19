using System;
using System.Globalization;
using Assets._Scripts.Controller.SceneControllers;
using Assets._Scripts.Managers;
using Assets._Scripts.Player;
using Assets._Scripts.ServerCommunication;
using Assets._Scripts.Utilities;

namespace Assets._Scripts.Handlers
{
    /// <summary>
    /// FeedbackHandler processes the success and error responses for feedback server calls.
    /// </summary>
    /// <remarks>
    /// This class implements the <see cref="IOnSuccessHandler"/> interface to handle successful server responses.
    /// It also includes error handling logic for unsuccessful call scenarios.
    /// FeedbackHandler interacts closely with the <see cref="feedbackSceneController"/> to manage server responses
    /// tied to the feedback feature.
    /// </remarks>
    public class FeedbackHandler : IOnSuccessHandler
    {
        public FeedbackSceneController feedbackSceneController;
        public long id;
        public string dialog;
        
        private readonly CultureInfo _culture = new("de-DE");

        /// <summary>
        /// Processes the successful response received from the server by performing the following actions:
        /// - Saves the dialog content to the history for record-keeping.
        /// - Notifies the <see cref="feedbackSceneController"/> if it is available, forwarding the response for additional handling.
        /// </summary>
        /// <param name="response">The server response containing the completion data to be processed.</param>
        public void OnSuccess(Response response)
        {
            SaveDialogToHistory(response.GetCompletion());

            if (!feedbackSceneController.IsNullOrDestroyed())
            {
                feedbackSceneController.OnSuccess(response);
            }
        }

        /// <summary>
        /// Handles error scenarios during server communication, processes the error response,
        /// and delegates error handling to the associated FeedbackSceneController if available.
        /// </summary>
        /// <param name="message">The error response received from the server.</param>
        public void OnError(Response message)
        {
            SaveDialogToHistory(message.GetCompletion());

            if (!feedbackSceneController.IsNullOrDestroyed())
            {
                feedbackSceneController.OnError(message);
            }
        }

        /// <summary>
        /// Saves a dialog entry to the history by creating a new history object,
        /// populating it with relevant dialog information, and adding it to the dialog history manager.
        /// </summary>
        /// <param name="response">The response completion text to be trimmed and stored in the dialog history entry.</param>
        private void SaveDialogToHistory(string response)
        {
            DialogHistoryEntry dialogHistoryEntry = new DialogHistoryEntry();
            dialogHistoryEntry.SetNovelId(id);
            dialogHistoryEntry.SetDialog(dialog);
            dialogHistoryEntry.SetCompletion(response.Trim());
            DateTime now = DateTime.Now;
            string formattedDateTime = now.ToString("ddd | dd.MM.yyyy | HH:mm", _culture);
            dialogHistoryEntry.SetDateAndTime(formattedDateTime);
            DialogHistoryManager.Instance().AddEntry(dialogHistoryEntry);
        }
    }
}