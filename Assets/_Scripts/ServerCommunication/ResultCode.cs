namespace Assets._Scripts.ServerCommunication
{
    /// <summary>
    /// Defines an enumeration of possible result codes returned by the server
    /// in response to various API calls. Each code represents a specific outcome
    /// (success, failure, specific errors, etc.).
    /// </summary>
    public enum ResultCode
    {
        None,
        Failure,
        NotAuthorized,
        SuccessfullyGotCompletion,
        SuccessfullyGotAllNovelReviews,
        SuccessfullyAddedNovelReview,
        SuccessfullyDeletedNovelReview,
        NoSuchNovelReview,
        SuccessfullyGotAllAIReviews,
        SuccessfullyAddedAIReview,
        SuccessfullyDeletedAIReview,
        NoSuchAIReview,
        SuccessfullyGotAllReviewObserver,
        SuccessfullyAddedReviewObserver,
        SuccessfullyDeletedReviewObserver,
        NoSuchReviewObserver,
        ReviewObserverAlreadyExists,
        SuccessfullyGotVersion,
        SuccessfullyGotUserRole,
        SuccessfullyGotAllDataObjects,
        SuccessfullyAddedDataObject,
        SuccessfullyDeletedDataObject,
        NoSuchDataObject,
        SuccessfullyPostetExpertFeedbackQuestion,
        SuccessfullyDeletedExpertFeedbackQuestion,
        SuccessfullyFoundExpertFeedbackQuestion,
        NoSuchExpertFeedbackQuestion,
        SuccessfullyPostetExpertFeedbackAnswer,
        SuccessfullyDeletedExpertFeedbackAnswer,
        SuccessfullyFoundExpertFeedbackAnswer,
        NoSuchExpertFeedbackAnswer,
        SuccessfullyGotAllExpertFeedbackQuestions,
        SuccessfullyGotAllExpertFeedbackAnswers
    }

    /// <summary>
    /// A helper class to convert an integer value into its corresponding <see cref="ResultCode"/> enumeration.
    /// </summary>
    public class ResultCodeHelper
    {
        /// <summary>
        /// Converts an integer value into a <see cref="ResultCode"/> enum.
        /// This is useful when the server returns an integer code and needs to be mapped
        /// to a more readable and type-safe enum.
        /// </summary>
        /// <param name="i">The integer value representing a result code.</param>
        /// <returns>The corresponding <see cref="ResultCode"/> enum value. Returns <see cref="ResultCode.None"/> if the integer does not match any defined code.</returns>
        public static ResultCode ValueOf(int i)
        {
            return i switch
            {
                1 => ResultCode.Failure,
                2 => ResultCode.NotAuthorized,
                3 => ResultCode.SuccessfullyGotCompletion,
                4 => ResultCode.SuccessfullyGotAllNovelReviews,
                5 => ResultCode.SuccessfullyAddedNovelReview,
                6 => ResultCode.SuccessfullyDeletedNovelReview,
                7 => ResultCode.NoSuchNovelReview,
                8 => ResultCode.SuccessfullyGotAllAIReviews,
                9 => ResultCode.SuccessfullyAddedAIReview,
                10 => ResultCode.SuccessfullyDeletedAIReview,
                11 => ResultCode.NoSuchAIReview,
                12 => ResultCode.SuccessfullyGotAllReviewObserver,
                13 => ResultCode.SuccessfullyAddedReviewObserver,
                14 => ResultCode.SuccessfullyDeletedReviewObserver,
                15 => ResultCode.NoSuchReviewObserver,
                16 => ResultCode.ReviewObserverAlreadyExists,
                17 => ResultCode.SuccessfullyGotVersion,
                18 => ResultCode.SuccessfullyGotUserRole,
                19 => ResultCode.SuccessfullyGotAllDataObjects,
                20 => ResultCode.SuccessfullyAddedDataObject,
                21 => ResultCode.SuccessfullyDeletedDataObject,
                22 => ResultCode.NoSuchDataObject,
                23 => ResultCode.SuccessfullyPostetExpertFeedbackQuestion,
                24 => ResultCode.SuccessfullyDeletedExpertFeedbackQuestion,
                25 => ResultCode.SuccessfullyFoundExpertFeedbackQuestion,
                26 => ResultCode.NoSuchExpertFeedbackQuestion,
                27 => ResultCode.SuccessfullyPostetExpertFeedbackAnswer,
                28 => ResultCode.SuccessfullyDeletedExpertFeedbackAnswer,
                29 => ResultCode.SuccessfullyFoundExpertFeedbackAnswer,
                30 => ResultCode.NoSuchExpertFeedbackAnswer,
                31 => ResultCode.SuccessfullyGotAllExpertFeedbackQuestions,
                32 => ResultCode.SuccessfullyGotAllExpertFeedbackAnswers,
                _ => ResultCode.None
            };
        }
    }
}