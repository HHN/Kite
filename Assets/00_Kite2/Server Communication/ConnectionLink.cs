using static System.Net.WebRequestMethods;

public class ConnectionLink
{
    public static readonly string BASE_LINK = "http://85.215.46.25:8080/"; // "http://localhost:8080/";
    public static readonly string REGISTRATION_LINK = BASE_LINK + "registration";
    public static readonly string LOG_IN_LINK = BASE_LINK + "login";
    public static readonly string LOG_OUT_LINK = BASE_LINK + "userlogout";
    public static readonly string NOVELS_LINK = BASE_LINK + "novels";
    public static readonly string COMPLETION_LINK = BASE_LINK + "ai";
    public static readonly string VOICE_TO_TEXT_LINK = BASE_LINK + "vtt";
    public static readonly string DELETE_ACCOUNT_LINK = BASE_LINK + "delete";
    public static readonly string CHANGE_PASSWORD_LINK = BASE_LINK + "changepassword";
    public static readonly string RESET_PASSWORD_LINK = BASE_LINK + "resetpassword";
    public static readonly string FIND_NOVEL_LINK = BASE_LINK + "novel";
    public static readonly string COMMENT_LINK = BASE_LINK + "comment";
    public static readonly string COMMENT_LIKE_LINK = BASE_LINK + "commentlike";
    public static readonly string NOVEL_LIKE_LINK = BASE_LINK + "novellike";
    public static readonly string SCORE_LINK = BASE_LINK + "score";
    public static readonly string MONEY_LINK = BASE_LINK + "money";
    public static readonly string NOVEL_REVIEW_LINK = BASE_LINK + "novelreview";
    public static readonly string AI_REVIEW_LINK = BASE_LINK + "aireview";
    public static readonly string OBSERVER_LINK = BASE_LINK + "reviewobserver";
}
