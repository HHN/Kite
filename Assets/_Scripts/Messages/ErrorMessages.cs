namespace Assets._Scripts.Messages
{
    public abstract class ErrorMessages
    {
        public static readonly string USERNAME_ALREADY_TAKEN = "Der Benutzername ist bereits vergeben.";
        public static readonly string EMAIL_ALREADY_REGISTERED = "Die Email-Adresse ist bereits registriert.";
        public static readonly string EMAIL_INVALID = "Die Email-Adresse ist nicht gültig.";
        public static readonly string NO_INTERNET = "Es gibt keine Verbindung zum Internet.";
        public static readonly string UNEXPECTED_SERVER_ERROR = "Ein unerwarteter Server-Fehler ist aufgetreten.";
        public static readonly string MICROFON_NOT_CONNECTED_ERROR = "Ein unerwarteter Fehler ist aufgetreten.";
        public static readonly string INVALID_CREDENTIALS = "Die Zugangsdaten sind ungültig.";
        public static readonly string NO_NOVELS = "Es sind keine spielbaren Visual Novels verfügbar.";
        public static readonly string NOT_LOGGED_IN = "Du musst dich einloggen um diese Aktion durchzuführen";
        public static readonly string NOT_EVERYTHING_ENTERED = "Bitte fülle alle notwendigen Felder aus.";
        public static readonly string EMAIL_NOT_CONFIRMED = "Bitte bestätige deine Email Adresse um dich einloggen zu können.";
        public static readonly string WRONG_PASSWORD = "Das angegebene alte Passwort ist nicht korrekt.";
        public static readonly string USER_NOT_FOUND = "Der angegebene Benutzername oder die angegebene Email-Adresse ist nicht registriert.";
        public static readonly string NO_COMMENT_ENTERED = "Bitte gebe den Kommentar ein, den du posten möchtest!";
        public static readonly string REVIEW_OBSERVER_ALREADY_EXISTS = "Die Email-Adresse wurde bereits als Beobachter hinzugefügt.";
        public static readonly string NOVEL_NOT_FOUND = "Die gewünschte Novels konnte leider nicht gefunden werden.";
    }
}
