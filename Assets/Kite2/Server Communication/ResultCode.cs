public enum ResultCode
{
    NONE,
    SUCCESS,
    FAILURE,
    SUCCESSFULLY_REGISTERED_NEW_USER,
    USERNAME_ALREADY_TAKEN,
    EMAIL_ALREADY_REGISTERED,
    INVALID_EMAIL,
    TOKEN_NOT_FOUND,
    EMAIL_ALREADY_CONFIRMED,
    TOKEN_EXPIRED,
    SUCCESSFULLY_CONFIRMED,
    SUCCESSFULLY_LOGGED_IN,
    SUCCESSFULLY_LOGGED_OUT,
    INVALID_CREDENTIALS,
    SUCCESSFULLY_POSTED_NOVEL,
    LOG_OUT_FAILED,
    NOT_AUTHORIZED,
    EMAIL_NOT_CONFIRMED,
    NO_NOVEL_AVAILABLE,
    SUCCESSFULLY_GOT_NOVELS,
    SUCCESSFULLY_GOT_COMPLETION,
    NOVEL_NOT_FOUND,
    SUCCESSFULLY_DELETED_NOVEL,
    USER_NOT_FOUND,
    SUCCESSFULLY_DELETED_USER,
    SUCCESSFULLY_CHANGED_PASSWORD,
    CHANGE_OF_PASSWORD_FAILED,
    SUCCESSFULLY_RESETED_PASSWORD,
    PASSWORD_ALREADY_RESETED,
    SUCCESSFULLY_INITIATED_RESET,
    SUCCESSFULLY_FOUND_NOVEL,
    SUCCESSFULLY_POSTED_COMMENT,
    SUCCESSFULLY_UPDATED_COMMENT,
    SUCCESSFULLY_DELETED_COMMENT,
    SUCCESSFULLY_GOT_COMMENTS_FOR_NOVEL,
    NO_COMMENTS_FOUND_FOR_NOVEL,
    COMMENT_NOT_FOUND,
    FAILED_TO_POST_COMMENT,
    FAILED_TO_UPDATE_COMMENT,
    FAILED_TO_DELETE_COMMENT,
    FAILED_TO_GET_COMMENTS_FOR_NOVEL,
    SUCCESSFULLY_LIKED_COMMENT,
    SUCCESSFULLY_UNLIKED_COMMENT,
    FAILED_TO_LIKE_COMMENT,
    FAILED_TO_UNLIKE_COMMENT,
    COMMENT_LIKE_NOT_FOUND,
    SUCCESSFULLY_LIKED_NOVEL,
    SUCCESSFULLY_UNLIKED_NOVEL,
    FAILED_TO_LIKE_NOVEL,
    FAILED_TO_UNLIKE_NOVEL,
    NOVEL_LIKE_NOT_FOUND,
    SUCCESSFULLY_GOT_NOVEL_LIKE_INFORMATION,
    FAILED_TO_GET_NOVEL_LIKE_INFORMATION,
    SUCCESSFULLY_GOT_SCORE,
    FAILED_TO_GET_SCORE,
    SUCCESSFULLY_UPDATED_SCORE,
    FAILED_TO_UPDATE_SCORE
}

public class ResultCodeHelper
{
    public static int ToInt(ResultCode resultCode)
    {
        switch (resultCode)
        {
            case ResultCode.SUCCESS: { return 1; }
            case ResultCode.FAILURE: { return 2; }
            case ResultCode.SUCCESSFULLY_REGISTERED_NEW_USER: { return 3; }
            case ResultCode.USERNAME_ALREADY_TAKEN: { return 4; }
            case ResultCode.EMAIL_ALREADY_REGISTERED: { return 5; }
            case ResultCode.INVALID_EMAIL: { return 6; }
            case ResultCode.TOKEN_NOT_FOUND: { return 7; }
            case ResultCode.EMAIL_ALREADY_CONFIRMED: { return 8; }
            case ResultCode.TOKEN_EXPIRED: { return 9; }
            case ResultCode.SUCCESSFULLY_CONFIRMED: { return 10; }
            case ResultCode.SUCCESSFULLY_LOGGED_IN: { return 11; }
            case ResultCode.SUCCESSFULLY_LOGGED_OUT: { return 12; }
            case ResultCode.INVALID_CREDENTIALS: { return 13; }
            case ResultCode.SUCCESSFULLY_POSTED_NOVEL: { return 14; }
            case ResultCode.LOG_OUT_FAILED: { return 15; }
            case ResultCode.NOT_AUTHORIZED: { return 16; }
            case ResultCode.EMAIL_NOT_CONFIRMED: { return 17; }
            case ResultCode.NO_NOVEL_AVAILABLE: { return 18; }
            case ResultCode.SUCCESSFULLY_GOT_NOVELS: { return 19; }
            case ResultCode.SUCCESSFULLY_GOT_COMPLETION: { return 20; }
            case ResultCode.NOVEL_NOT_FOUND: { return 21; }
            case ResultCode.SUCCESSFULLY_DELETED_NOVEL: { return 22; }
            case ResultCode.USER_NOT_FOUND: { return 23; }
            case ResultCode.SUCCESSFULLY_DELETED_USER: { return 24; }
            case ResultCode.SUCCESSFULLY_CHANGED_PASSWORD: { return 25; }
            case ResultCode.CHANGE_OF_PASSWORD_FAILED: { return 26; }
            case ResultCode.SUCCESSFULLY_RESETED_PASSWORD: { return 27; }
            case ResultCode.PASSWORD_ALREADY_RESETED: { return 28; }
            case ResultCode.SUCCESSFULLY_INITIATED_RESET: { return 29; }
            case ResultCode.SUCCESSFULLY_FOUND_NOVEL: { return 30; }
            case ResultCode.SUCCESSFULLY_POSTED_COMMENT: { return 31; }
            case ResultCode.SUCCESSFULLY_UPDATED_COMMENT: { return 32; }
            case ResultCode.SUCCESSFULLY_DELETED_COMMENT: { return 33; }
            case ResultCode.SUCCESSFULLY_GOT_COMMENTS_FOR_NOVEL: { return 34; }
            case ResultCode.NO_COMMENTS_FOUND_FOR_NOVEL: { return 35; }
            case ResultCode.COMMENT_NOT_FOUND: { return 36; }
            case ResultCode.FAILED_TO_POST_COMMENT: { return 37; }
            case ResultCode.FAILED_TO_UPDATE_COMMENT: { return 38; }
            case ResultCode.FAILED_TO_DELETE_COMMENT: { return 39; }
            case ResultCode.FAILED_TO_GET_COMMENTS_FOR_NOVEL: { return 40; }
            case ResultCode.SUCCESSFULLY_LIKED_COMMENT: { return 41; }
            case ResultCode.SUCCESSFULLY_UNLIKED_COMMENT: { return 42; }
            case ResultCode.FAILED_TO_LIKE_COMMENT: { return 43; }
            case ResultCode.FAILED_TO_UNLIKE_COMMENT: { return 44; }
            case ResultCode.COMMENT_LIKE_NOT_FOUND: { return 45; }
            case ResultCode.SUCCESSFULLY_LIKED_NOVEL: { return 46; }
            case ResultCode.SUCCESSFULLY_UNLIKED_NOVEL: { return 47; }
            case ResultCode.FAILED_TO_LIKE_NOVEL: { return 48; }
            case ResultCode.FAILED_TO_UNLIKE_NOVEL: { return 49; }
            case ResultCode.NOVEL_LIKE_NOT_FOUND: { return 50; }
            case ResultCode.SUCCESSFULLY_GOT_NOVEL_LIKE_INFORMATION: { return 51; }
            case ResultCode.FAILED_TO_GET_NOVEL_LIKE_INFORMATION: { return 52; }
            case ResultCode.SUCCESSFULLY_GOT_SCORE: { return 53; }
            case ResultCode.FAILED_TO_GET_SCORE: { return 54; }
            case ResultCode.SUCCESSFULLY_UPDATED_SCORE: { return 55; }
            case ResultCode.FAILED_TO_UPDATE_SCORE: { return 56; }
            default: { return -1; }
        }
    }

    public static ResultCode ValueOf(int i)
    {
        switch (i)
        {
            case 1: { return ResultCode.SUCCESS; }
            case 2: { return ResultCode.FAILURE; }
            case 3: { return ResultCode.SUCCESSFULLY_REGISTERED_NEW_USER; }
            case 4: { return ResultCode.USERNAME_ALREADY_TAKEN; }
            case 5: { return ResultCode.EMAIL_ALREADY_REGISTERED; }
            case 6: { return ResultCode.INVALID_EMAIL; }
            case 7: { return ResultCode.TOKEN_NOT_FOUND; }
            case 8: { return ResultCode.EMAIL_ALREADY_CONFIRMED; }
            case 9: { return ResultCode.TOKEN_EXPIRED; }
            case 10: { return ResultCode.SUCCESSFULLY_CONFIRMED; }
            case 11: { return ResultCode.SUCCESSFULLY_LOGGED_IN; }
            case 12: { return ResultCode.SUCCESSFULLY_LOGGED_OUT; }
            case 13: { return ResultCode.INVALID_CREDENTIALS; }
            case 14: { return ResultCode.SUCCESSFULLY_POSTED_NOVEL; }
            case 15: { return ResultCode.LOG_OUT_FAILED; }
            case 16: { return ResultCode.NOT_AUTHORIZED; }
            case 17: { return ResultCode.EMAIL_NOT_CONFIRMED; }
            case 18: { return ResultCode.NO_NOVEL_AVAILABLE; }
            case 19: { return ResultCode.SUCCESSFULLY_GOT_NOVELS; }
            case 20: { return ResultCode.SUCCESSFULLY_GOT_COMPLETION; }
            case 21: { return ResultCode.NOVEL_NOT_FOUND; }
            case 22: { return ResultCode.SUCCESSFULLY_DELETED_NOVEL; }
            case 23: { return ResultCode.USER_NOT_FOUND; }
            case 24: { return ResultCode.SUCCESSFULLY_DELETED_USER; }
            case 25: { return ResultCode.SUCCESSFULLY_CHANGED_PASSWORD; }
            case 26: { return ResultCode.CHANGE_OF_PASSWORD_FAILED; }
            case 27: { return ResultCode.SUCCESSFULLY_RESETED_PASSWORD; }
            case 28: { return ResultCode.PASSWORD_ALREADY_RESETED; }
            case 29: { return ResultCode.SUCCESSFULLY_INITIATED_RESET; }
            case 30: { return ResultCode.SUCCESSFULLY_FOUND_NOVEL; }
            case 31: { return ResultCode.SUCCESSFULLY_POSTED_COMMENT; }
            case 32: { return ResultCode.SUCCESSFULLY_UPDATED_COMMENT; }
            case 33: { return ResultCode.SUCCESSFULLY_DELETED_COMMENT; }
            case 34: { return ResultCode.SUCCESSFULLY_GOT_COMMENTS_FOR_NOVEL; }
            case 35: { return ResultCode.NO_COMMENTS_FOUND_FOR_NOVEL; }
            case 36: { return ResultCode.COMMENT_NOT_FOUND; }
            case 37: { return ResultCode.FAILED_TO_POST_COMMENT; }
            case 38: { return ResultCode.FAILED_TO_UPDATE_COMMENT; }
            case 39: { return ResultCode.FAILED_TO_DELETE_COMMENT; }
            case 40: { return ResultCode.FAILED_TO_GET_COMMENTS_FOR_NOVEL; }
            case 41: { return ResultCode.SUCCESSFULLY_LIKED_COMMENT; }
            case 42: { return ResultCode.SUCCESSFULLY_UNLIKED_COMMENT; }
            case 43: { return ResultCode.FAILED_TO_LIKE_COMMENT; }
            case 44: { return ResultCode.FAILED_TO_UNLIKE_COMMENT; }
            case 45: { return ResultCode.COMMENT_LIKE_NOT_FOUND; }
            case 46: { return ResultCode.SUCCESSFULLY_LIKED_NOVEL; }
            case 47: { return ResultCode.SUCCESSFULLY_UNLIKED_NOVEL; }
            case 48: { return ResultCode.FAILED_TO_LIKE_NOVEL; }
            case 49: { return ResultCode.FAILED_TO_UNLIKE_NOVEL; }
            case 50: { return ResultCode.NOVEL_LIKE_NOT_FOUND; }
            case 51: { return ResultCode.SUCCESSFULLY_GOT_NOVEL_LIKE_INFORMATION; }
            case 52: { return ResultCode.FAILED_TO_GET_NOVEL_LIKE_INFORMATION; }
            case 53: { return ResultCode.SUCCESSFULLY_GOT_SCORE; }
            case 54: { return ResultCode.FAILED_TO_GET_SCORE; }
            case 55: { return ResultCode.SUCCESSFULLY_UPDATED_SCORE; }
            case 56: { return ResultCode.FAILED_TO_UPDATE_SCORE; }
            default: { return ResultCode.NONE; }
        }
    }
}
