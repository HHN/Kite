using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.ServerCommunication
{
    /// <summary>
    /// Represents a generic response object received from the server.
    /// This class can encapsulate various types of data depending on the server call,
    /// including result codes, text, AI completions, lists of reviews, and other data objects.
    /// It is <see cref="Serializable"/> to allow for easy deserialization from JSON.
    /// </summary>
    [Serializable]
    public class Response
    {
        [SerializeField] private int resultCode;
        [SerializeField] private string resultText;
        [SerializeField] private string completion;
        [SerializeField] private int version;
        [SerializeField] private int userRole;

        /// <summary>
        /// Sets the numerical result code of the server response.
        /// </summary>
        /// <param name="resultCode">The integer result code.</param>
        public void SetResultCode(int resultCode)
        {
            this.resultCode = resultCode;
        }

        /// <summary>
        /// Gets the numerical result code of the server response.
        /// </summary>
        /// <returns>The integer result code.</returns>
        public int GetResultCode()
        {
            return resultCode;
        }

        /// <summary>
        /// Sets the descriptive result text of the server response.
        /// </summary>
        /// <param name="resultText">The string result text.</param>
        public void SetResultText(string resultText)
        {
            this.resultText = resultText;
        }

        /// <summary>
        /// Gets the descriptive result text of the server response.
        /// </summary>
        /// <returns>The string result text.</returns>
        public string GetResultText()
        {
            return this.resultText;
        }

        /// <summary>
        /// Sets the AI completion string received in the response.
        /// </summary>
        /// <param name="completion">The AI-generated completion string.</param>
        public void SetCompletion(string completion)
        {
            this.completion = completion;
        }

        /// <summary>
        /// Gets the AI completion string received in the response.
        /// </summary>
        /// <returns>The AI-generated completion string.</returns>
        public string GetCompletion()
        {
            return completion;
        }

        /// <summary>
        /// Sets the version integer in the response.
        /// </summary>
        /// <param name="version">The integer version number.</param>
        public void SetVersion(int version)
        {
            this.version = version;
        }

        /// <summary>
        /// Gets the version integer from the response.
        /// </summary>
        /// <returns>The integer version number.</returns>
        public int GetVersion()
        {
            return version;
        }

        /// <summary>
        /// Sets the user role integer in the response.
        /// </summary>
        /// <param name="userRole">The integer user role.</param>
        public void SetUserRole(int userRole)
        {
            this.userRole = userRole;
        }

        /// <summary>
        /// Gets the user role integer from the response.
        /// </summary>
        /// <returns>The integer user role.</returns>
        public int GetUserRole()
        {
            return userRole;
        }
    }
}