namespace Assets._Scripts.Utilities
{
    using UnityEngine;

    public static class LogManager
    {
        public enum LogLevel { Info, Warning, Error }

        public static LogLevel CurrentLevel = LogLevel.Info;

        public static void Info(string message, Object context = null)
        {
            if (CurrentLevel <= LogLevel.Info)
                Debug.Log(Format("[INFO]", message), context);
        }

        public static void Warning(string message, Object context = null)
        {
            if (CurrentLevel <= LogLevel.Warning)
                Debug.LogWarning(Format("[WARN]", message), context);
        }

        public static void Error(string message, Object context = null)
        {
            if (CurrentLevel <= LogLevel.Error)
                Debug.LogError(Format("[ERROR]", message), context);
        }

        private static string Format(string level, string message)
        {
            return $"{System.DateTime.Now:HH:mm:ss} {level} {message}";
        }
    }

}