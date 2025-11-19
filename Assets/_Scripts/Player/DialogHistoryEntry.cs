using System;
using UnityEngine;

namespace Assets._Scripts.Player
{
    /// <summary>
    /// Represents a single entry in the dialog history, storing details about a specific
    /// interaction, including the novel it belongs to, the dialog content, an AI completion,
    /// and the timestamp of the interaction.
    /// This class is <see cref="Serializable"/> to allow it to be saved and loaded (e.g., for game progress).
    /// </summary>
    [Serializable]
    public class DialogHistoryEntry
    {
        [SerializeField] private long id;
        [SerializeField] private string dialog;
        [SerializeField] private string completion;
        [SerializeField] private string dateAndTime;

        /// <summary>
        /// Gets the unique identifier of the novel associated with this dialog entry.
        /// </summary>
        /// <returns>The novel ID.</returns>
        public long GetNovelId()
        {
            return id;
        }

        /// <summary>
        /// Sets the unique identifier of the novel associated with this dialog entry.
        /// </summary>
        /// <param name="novelId">The novel ID to set.</param>
        public void SetNovelId(long novelId)
        {
            id = novelId;
        }

        /// <summary>
        /// Gets the dialog text of this entry.
        /// </summary>
        /// <returns>The dialog string.</returns>
        public string GetDialog()
        {
            return dialog;
        }

        /// <summary>
        /// Sets the dialog text for this entry.
        /// </summary>
        /// <param name="newDialog">The dialog string to set.</param>
        public void SetDialog(string newDialog)
        {
            dialog = newDialog;
        }

        /// <summary>
        /// Gets the date and time when this dialog entry was created.
        /// </summary>
        /// <returns>The date and time string.</returns>
        public string GetDateAndTime()
        {
            return dateAndTime;
        }

        /// <summary>
        /// Sets the date and time for this dialog entry.
        /// </summary>
        /// <param name="newDateAndTime">The date and time string to set.</param>
        public void SetDateAndTime(string newDateAndTime)
        {
            dateAndTime = newDateAndTime;
        }

        /// <summary>
        /// Gets the AI-generated completion text associated with this dialog.
        /// </summary>
        /// <returns>The completion string.</returns>
        public string GetCompletion()
        {
            return completion;
        }

        /// <summary>
        /// Sets the AI-generated completion text for this dialog entry.
        /// </summary>
        /// <param name="newCompletion">The completion string to set.</param>
        public void SetCompletion(string newCompletion)
        {
            this.completion = newCompletion;
        }

        /// <summary>
        /// Gets the dialog text with character designations replaced for display purposes.
        /// Specifically, "Player:" is replaced with "Du:".
        /// </summary>
        /// <returns>The dialog string with replaced character designation.</returns>
        public string GetDialogWithReplacedCharacterDesignation()
        {
            string result = dialog.Replace("Player:", "Du:");
            return result;
        }
    }
}