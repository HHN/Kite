using UnityEngine;

namespace Assets._Scripts.Utilities
{
    /// <summary>
    /// Provides a centralized, static utility for logging messages with different severity levels (Info, Warning, Error).
    /// Allows control over which messages are displayed based on a configurable minimum log level.
    /// </summary>
    public static class LogManager
    {
        /// <summary>
        /// Defines the available severity levels for logging. Messages will only be logged
        /// if their level is equal to or higher than the currently configured level.
        /// </summary>
        private enum LogLevel
        {
            Info, 
            Warning, 
            Error
        }

        private static LogLevel _currentLevel = LogLevel.Info;

        /// <summary>
        /// Logs an informational message if the current log level is set to <c>Info</c> or lower.
        /// </summary>
        /// <param name="message">The message content to log.</param>
        /// <param name="context">The object to which the message applies (optional).</param>
        public static void Info(string message, Object context = null)
        {
            if (_currentLevel <= LogLevel.Info) Debug.Log(Format("[INFO]", message), context);
        }

        /// <summary>
        /// Logs a warning message if the current log level is set to <c>Warning</c> or lower.
        /// </summary>
        /// <param name="message">The warning content to log.</param>
        /// <param name="context">The object to which the warning applies (optional).</param>
        public static void Warning(string message, Object context = null)
        {
            if (_currentLevel <= LogLevel.Warning) Debug.LogWarning(Format("[WARN]", message), context);
        }

        /// <summary>
        /// Logs an error message if the current log level is set to <c>Error</c> or lower.
        /// </summary>
        /// <param name="message">The error content to log.</param>
        /// <param name="context">The object to which the error applies (optional).</param>
        public static void Error(string message, Object context = null)
        {
            if (_currentLevel <= LogLevel.Error) Debug.LogError(Format("[ERROR]", message), context);
        }

        /// <summary>
        /// Formats the log message by prepending the current timestamp and the log level tag.
        /// </summary>
        /// <param name="level">The predefined string tag for the log level (e.g., "[INFO]").</param>
        /// <param name="message">The raw message content.</param>
        /// <returns>The fully formatted log string.</returns>
        private static string Format(string level, string message)
        {
            return $"{System.DateTime.Now:HH:mm:ss} {level} {message}";
        }
    }
}