namespace Assets._Scripts.Messages
{
    /// <summary>
    /// Provides predefined informational messages used throughout the application.
    /// </summary>
    /// <remarks>
    /// This class contains static readonly string messages to ensure consistency in
    /// text displayed to users across various features and functionalities.
    /// The messages include prompts, notifications, and explanations related to
    /// app interaction, resetting, online/offline modes, sound feedback adjustments,
    /// and more.
    /// </remarks>
    public abstract class InfoMessages
    { 
        public static readonly string RESET_APP_CONFIRMATION = "Wenn du fortfährst wird die App zurückgesetzt.\r\n\r\nWenn du dies möchtest, drücke auf \"DATEN LÖSCHEN\".\r\nFalls nicht, drücke auf \"ABBRECHEN\".";
        public static readonly string RESET_APP = "Die App wurde erfolgreich zurückgesetzt.";
        public static readonly string EXPLANATION_RESET_APP_BUTTON = "Mit diesem Button kannst du deine App zurücksetzen. Sämtliche Daten, welche durch dein Spielen entstanden sind, werden gelöscht.";
        public static readonly string EXPLANATION_APPLICATION_MODE_BUTTON = "Mit diesem Button kannst du zwischen Offline- und Online-Modus hin und her switchen.";
        public static readonly string SWITCHED_TO_ONLINE_MODE = "Die App befindet sich jetzt im Online Modus.";
        public static readonly string SWITCHED_TO_OFFLINE_MODE = "Die App befindet sich jetzt im Offline Modus.";
        public static readonly string UPDATE_AVAILABLE = "Deine App-Version ist veraltet. Ein neues Update ist verfügbar. Bitte aktualisiere deine App, um Kompatibilitätsprobleme mit dem Server zu vermeiden und ein optimales Nutzungserlebnis zu gewährleisten.";
        public static readonly string STARTED_TOGGLETEXTTOSPEECH_BUTTON = "Text wird dir nun vorgelesen.";
        public static readonly string STOPPED_TOGGLETEXTTOSPEECH_BUTTON = "Text wird dir nun nicht länger vorgelesen.";
        public static readonly string CONFIRM_FONT_SIZE_ADJUSTMENT = "Die Schriftgröße wurde angepasst.";
        public static readonly string ACTIVATED_SOUNDEFFECTS_BUTTON = "Sämtliche Soundeffekte der App wurden aktiviert. Dies ist unabhängig von der Vorlesefunktion.";
        public static readonly string DEACTIVATED_SOUNDEFFECTS_BUTTON = "Sämtliche Soundeffekte der App wurden deaktiviert. Dies ist unabhängig von der Vorlesefunktion.";
    }
}
