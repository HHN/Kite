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
    SUCCESSFULLY_GOT_COMPLETION
}

public class ResultCodeHelper
{
    public static int toInt(ResultCode resultCode)
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
            default: { return -1; }
        }
    }

    public static ResultCode valueOf(int i)
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
            default: { return ResultCode.NONE; }
        }
    }
}
