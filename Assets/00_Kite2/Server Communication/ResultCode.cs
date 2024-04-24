public enum ResultCode
{
    NONE,
    FAILURE,
    NOT_AUTHORIZED,
    SUCCESSFULLY_GOT_COMPLETION,
    SUCCESSFULLY_GOT_ALL_NOVEL_REVIEWS,
    SUCCESSFULLY_ADDED_NOVEL_REVIEW,
    SUCCESSFULLY_DELETED_NOVEL_REVIEW,
    NO_SUCH_NOVEL_REVIEW,
    SUCCESSFULLY_GOT_ALL_AI_REVIEWS,
    SUCCESSFULLY_ADDED_AI_REVIEW,
    SUCCESSFULLY_DELETED_AI_REVIEW,
    NO_SUCH_AI_REVIEW,
    SUCCESSFULLY_GOT_ALL_REVIEW_OBSERVER,
    SUCCESSFULLY_ADDED_REVIEW_OBSERVER,
    SUCCESSFULLY_DELETED_REVIEW_OBSERVER,
    NO_SUCH_REVIEW_OBSERVER,
    REVIEW_OBSERVER_ALREADY_EXISTS,
    SUCCESSFULLY_GOT_VERSION,
    SUCCESSFULLY_GOT_USER_ROLE,
    SUCCESSFULLY_GOT_ALL_DATA_OBJECTS,
    SUCCESSFULLY_ADDED_DATA_OBJECT,
    SUCCESSFULLY_DELETED_DATA_OBJECT,
    NO_SUCH_DATA_OBJECT,
    SUCCESSFULLY_POSTET_EXPERT_FEEDBACK_QUESTION,
    SUCCESSFULLY_DELETED_EXPERT_FEEDBACK_QUESTION,
    SUCCESSFULLY_FOUND_EXPERT_FEEDBACK_QUESTION,
    NO_SUCH_EXPERT_FEEDBACK_QUESTION,
    SUCCESSFULLY_POSTET_EXPERT_FEEDBACK_ANSWER,
    SUCCESSFULLY_DELETED_EXPERT_FEEDBACK_ANSWER,
    SUCCESSFULLY_FOUND_EXPERT_FEEDBACK_ANSWER,
    NO_SUCH_EXPERT_FEEDBACK_ANSWER,
    SUCCESSFULLY_GOT_ALL_EXPERT_FEEDBACK_QUESTIONS,
    SUCCESSFULLY_GOT_ALL_EXPERT_FEEDBACK_ANSWERS
}

public class ResultCodeHelper
{
    public static int ToInt(ResultCode resultCode)
    {
        switch (resultCode)
        {
            case ResultCode.FAILURE: { return 1; }
            case ResultCode.NOT_AUTHORIZED: { return 2; }
            case ResultCode.SUCCESSFULLY_GOT_COMPLETION: { return 3; }
            case ResultCode.SUCCESSFULLY_GOT_ALL_NOVEL_REVIEWS: { return 4; }
            case ResultCode.SUCCESSFULLY_ADDED_NOVEL_REVIEW: { return 5; }
            case ResultCode.SUCCESSFULLY_DELETED_NOVEL_REVIEW: { return 6; }
            case ResultCode.NO_SUCH_NOVEL_REVIEW: { return 7; }
            case ResultCode.SUCCESSFULLY_GOT_ALL_AI_REVIEWS: { return 8; }
            case ResultCode.SUCCESSFULLY_ADDED_AI_REVIEW: { return 9; }
            case ResultCode.SUCCESSFULLY_DELETED_AI_REVIEW: { return 10; }
            case ResultCode.NO_SUCH_AI_REVIEW: { return 11; }
            case ResultCode.SUCCESSFULLY_GOT_ALL_REVIEW_OBSERVER: { return 12; }
            case ResultCode.SUCCESSFULLY_ADDED_REVIEW_OBSERVER: { return 13; }
            case ResultCode.SUCCESSFULLY_DELETED_REVIEW_OBSERVER: { return 14; }
            case ResultCode.NO_SUCH_REVIEW_OBSERVER: { return 15; }
            case ResultCode.REVIEW_OBSERVER_ALREADY_EXISTS: { return 16; }
            case ResultCode.SUCCESSFULLY_GOT_VERSION: { return 17; }
            case ResultCode.SUCCESSFULLY_GOT_USER_ROLE: { return 18; }
            case ResultCode.SUCCESSFULLY_GOT_ALL_DATA_OBJECTS: { return 19; }
            case ResultCode.SUCCESSFULLY_ADDED_DATA_OBJECT: { return 20; }
            case ResultCode.SUCCESSFULLY_DELETED_DATA_OBJECT: { return 21; }
            case ResultCode.NO_SUCH_DATA_OBJECT: { return 22; }
            case ResultCode.SUCCESSFULLY_POSTET_EXPERT_FEEDBACK_QUESTION: { return 23; }
            case ResultCode.SUCCESSFULLY_DELETED_EXPERT_FEEDBACK_QUESTION: { return 24; }
            case ResultCode.SUCCESSFULLY_FOUND_EXPERT_FEEDBACK_QUESTION: { return 25; }
            case ResultCode.NO_SUCH_EXPERT_FEEDBACK_QUESTION: { return 26; }
            case ResultCode.SUCCESSFULLY_POSTET_EXPERT_FEEDBACK_ANSWER: { return 27; }
            case ResultCode.SUCCESSFULLY_DELETED_EXPERT_FEEDBACK_ANSWER: { return 28; }
            case ResultCode.SUCCESSFULLY_FOUND_EXPERT_FEEDBACK_ANSWER: { return 29; }
            case ResultCode.NO_SUCH_EXPERT_FEEDBACK_ANSWER: { return 30; }
            case ResultCode.SUCCESSFULLY_GOT_ALL_EXPERT_FEEDBACK_QUESTIONS: { return 31; }
            case ResultCode.SUCCESSFULLY_GOT_ALL_EXPERT_FEEDBACK_ANSWERS: { return 32; }
            default: { return -1; }
        }
    }

    public static ResultCode ValueOf(int i)
    {
        switch (i)
        {
            case 1: { return ResultCode.FAILURE; }
            case 2: { return ResultCode.NOT_AUTHORIZED; }
            case 3: { return ResultCode.SUCCESSFULLY_GOT_COMPLETION; }
            case 4: { return ResultCode.SUCCESSFULLY_GOT_ALL_NOVEL_REVIEWS; }
            case 5: { return ResultCode.SUCCESSFULLY_ADDED_NOVEL_REVIEW; }
            case 6: { return ResultCode.SUCCESSFULLY_DELETED_NOVEL_REVIEW; }
            case 7: { return ResultCode.NO_SUCH_NOVEL_REVIEW; }
            case 8: { return ResultCode.SUCCESSFULLY_GOT_ALL_AI_REVIEWS; }
            case 9: { return ResultCode.SUCCESSFULLY_ADDED_AI_REVIEW; }
            case 10: { return ResultCode.SUCCESSFULLY_DELETED_AI_REVIEW; }
            case 11: { return ResultCode.NO_SUCH_AI_REVIEW; }
            case 12: { return ResultCode.SUCCESSFULLY_GOT_ALL_REVIEW_OBSERVER; }
            case 13: { return ResultCode.SUCCESSFULLY_ADDED_REVIEW_OBSERVER; }
            case 14: { return ResultCode.SUCCESSFULLY_DELETED_REVIEW_OBSERVER; }
            case 15: { return ResultCode.NO_SUCH_REVIEW_OBSERVER; }
            case 16: { return ResultCode.REVIEW_OBSERVER_ALREADY_EXISTS; }
            case 17: { return ResultCode.SUCCESSFULLY_GOT_VERSION; }
            case 18: { return ResultCode.SUCCESSFULLY_GOT_USER_ROLE; }
            case 19: { return ResultCode.SUCCESSFULLY_GOT_ALL_DATA_OBJECTS; }
            case 20: { return ResultCode.SUCCESSFULLY_ADDED_DATA_OBJECT; }
            case 21: { return ResultCode.SUCCESSFULLY_DELETED_DATA_OBJECT; }
            case 22: { return ResultCode.NO_SUCH_DATA_OBJECT;}
            case 23: { return ResultCode.SUCCESSFULLY_POSTET_EXPERT_FEEDBACK_QUESTION; }
            case 24: { return ResultCode.SUCCESSFULLY_DELETED_EXPERT_FEEDBACK_QUESTION; }
            case 25: { return ResultCode.SUCCESSFULLY_FOUND_EXPERT_FEEDBACK_QUESTION; }
            case 26: { return ResultCode.NO_SUCH_EXPERT_FEEDBACK_QUESTION; }
            case 27: { return ResultCode.SUCCESSFULLY_POSTET_EXPERT_FEEDBACK_ANSWER; }
            case 28: { return ResultCode.SUCCESSFULLY_DELETED_EXPERT_FEEDBACK_ANSWER; }
            case 29: { return ResultCode.SUCCESSFULLY_FOUND_EXPERT_FEEDBACK_ANSWER; }
            case 30: { return ResultCode.NO_SUCH_EXPERT_FEEDBACK_ANSWER; }
            case 31: { return ResultCode.SUCCESSFULLY_GOT_ALL_EXPERT_FEEDBACK_QUESTIONS; }
            case 32: { return ResultCode.SUCCESSFULLY_GOT_ALL_EXPERT_FEEDBACK_ANSWERS; }
            default: { return ResultCode.NONE; }
        }
    }
}
