using UnityEngine;

public class SceneMemoryManager
{
    private static SceneMemoryManager instance;

    private SettingsSceneMemory settingsSceneMemory;
    private DetailsViewSceneMemory detailsViewSceneMemory;
    private FeedbackSceneMemory feedbackSceneMemory;
    private NovelExplorerSceneMemory novelExplorerSceneMemory;
    private PlayNovelSceneMemory playNovelSceneMemory;
    private ChangePasswordSceneMemory changePasswordSceneMemory;
    private LogInSceneMemory logInSceneMemory;
    private RegistrationSceneMemory registrationSceneMemory;
    private ResetPasswordSceneMemory resetPasswordSceneMemory;
    private InfoSceneMemory infoSceneMemory;
    private InfoTextSceneMemory infoTextSceneMemory;
    private CommentSectionSceneMemory commentSectionSceneMemory;
    private NovelMakerSceneMemory novelMakerSceneMemory;
    private CharacterExplorerSceneMemory characterExplorerSceneMemory;
    private EnvironmentExplorerSceneMemory environmentExplorerSceneMemory;
    private HelpForNovelMakerSceneMemory helpForNovelMakerSceneMemory;
    private FinishNovelSceneMemory finishNovelSceneMemory;
    private NovelPreviewSceneMemory novelPreviewSceneMemory;
    private AddObserverSceneMemory addObserverSceneMemory;
    private AiReviewExplorerSceneMemory aiReviewExplorerSceneMemory;
    private NovelReviewExplorerSceneMemory novelReviewExplorerSceneMemory;
    private FeedbackRoleManagementSceneMemory feedbackRoleManagementSceneMemory;
    private ReviewAiSceneMemory reviewAiSceneMemory;
    private ReviewNovelSceneMemory reviewNovelSceneMemory;
    private ReviewObserverExplorerSceneMemory reviewObserverExplorerSceneMemory;

    private SceneMemoryManager()
    {
        ClearMemory();
    }

    public static SceneMemoryManager Instance()
    {
        if (instance == null)
        {
            instance = new SceneMemoryManager();
        }
        return instance;
    }

    public SettingsSceneMemory GetMemoryOfSettingsScene()
    {
        return this.settingsSceneMemory;
    }

    public DetailsViewSceneMemory GetMemoryOfDetailsViewScene()
    {
        return this.detailsViewSceneMemory;
    }

    public FeedbackSceneMemory GetMemoryOfFeedbackScene()
    {
        return this.feedbackSceneMemory;
    }

    public NovelExplorerSceneMemory GetMemoryOfNovelExplorerScene()
    {
        return this.novelExplorerSceneMemory;
    }

    public PlayNovelSceneMemory GetMemoryOfPlayNovelScene()
    {
        return this.playNovelSceneMemory;
    }

    public ChangePasswordSceneMemory GetMemoryOfChangePasswordScene()
    {
        return this.changePasswordSceneMemory;
    }

    public LogInSceneMemory GetMemoryOfLogInScene()
    {
        return this.logInSceneMemory;
    }

    public RegistrationSceneMemory GetMemoryOfRegistrationScene()
    {
        return this.registrationSceneMemory;
    }

    public ResetPasswordSceneMemory GetMemoryOfResetPasswordScene()
    {
        return this.resetPasswordSceneMemory;
    }

    public InfoSceneMemory GetMemoryOfInfoScene()
    {
        return this.infoSceneMemory;
    }

    public InfoTextSceneMemory GetMemoryOfInfoTextScene()
    {
        return this.infoTextSceneMemory;
    }

    public CommentSectionSceneMemory GetMemoryOfCommentSectionScene()
    {
        return this.commentSectionSceneMemory;
    }

    public NovelMakerSceneMemory GetMemoryOfNovelMakerScene()
    {
        return this.novelMakerSceneMemory;
    }

    public CharacterExplorerSceneMemory GetMemoryOfCharacterExplorerScene()
    {
        return this.characterExplorerSceneMemory;
    }

    public EnvironmentExplorerSceneMemory GetMemoryOfEnvironmentExplorerScene()
    {
        return this.environmentExplorerSceneMemory;
    }

    public HelpForNovelMakerSceneMemory GetMemoryOfHelpForNovelMakerScene()
    {
        return this.helpForNovelMakerSceneMemory;
    }

    public FinishNovelSceneMemory GetMemoryOfFinishNovelScene()
    {
        return this.finishNovelSceneMemory;
    }

    public NovelPreviewSceneMemory GetMemoryOfNovelPreviewScene()
    {
        return this.novelPreviewSceneMemory;
    }

    public AddObserverSceneMemory GetMemoryOfAddObserverScene()
    {
        return this.addObserverSceneMemory;
    }

    public AiReviewExplorerSceneMemory GetMemoryOfAiReviewExplorerScene()
    {
        return this.aiReviewExplorerSceneMemory;
    }

    public NovelReviewExplorerSceneMemory GetMemoryOfNovelReviewExplorerScene()
    {
        return this.novelReviewExplorerSceneMemory;
    }

    public FeedbackRoleManagementSceneMemory GetMemoryOfFeedbackRoleManagementScene()
    {
        return this.feedbackRoleManagementSceneMemory;
    }

    public ReviewAiSceneMemory GetMemoryOfReviewAiScene()
    {
        return this.reviewAiSceneMemory;
    }

    public ReviewNovelSceneMemory GetMemoryOfReviewNovelScene()
    {
        return this.reviewNovelSceneMemory;
    }

    public ReviewObserverExplorerSceneMemory GetMemoryOfReviewObserverExplorerScene()
    {
        return this.reviewObserverExplorerSceneMemory;
    }

    public void SetMemoryOfSettingsScene(SettingsSceneMemory settingsSceneMemory)
    {
        this.settingsSceneMemory = settingsSceneMemory;
    }

    public void SetMemoryOfDetailsViewScene(DetailsViewSceneMemory detailsViewSceneMemory)
    {
        this.detailsViewSceneMemory = detailsViewSceneMemory;
    }

    public void SetMemoryOfFeedbackScene(FeedbackSceneMemory feedbackSceneMemory)
    {
        this.feedbackSceneMemory = feedbackSceneMemory;
    }

    public void SetMemoryOfNovelExplorerScene(NovelExplorerSceneMemory novelExplorerSceneMemory)
    {
        this.novelExplorerSceneMemory = novelExplorerSceneMemory;
    }

    public void SetMemoryOfPlayNovelScene(PlayNovelSceneMemory playNovelSceneMemory)
    {
        this.playNovelSceneMemory = playNovelSceneMemory;
    }

    public void SetMemoryOfChangePasswordScene(ChangePasswordSceneMemory changePasswordSceneMemory)
    {
        this.changePasswordSceneMemory = changePasswordSceneMemory;
    }

    public void SetMemoryOfLogInScene(LogInSceneMemory logInSceneMemory)
    {
        this.logInSceneMemory = logInSceneMemory;
    }

    public void SetMemoryOfRegistrationScene(RegistrationSceneMemory registrationSceneMemory)
    {
        this.registrationSceneMemory = registrationSceneMemory;
    }

    public void SetMemoryOfResetPasswordScene(ResetPasswordSceneMemory resetPasswordSceneMemory)
    {
        this.resetPasswordSceneMemory = resetPasswordSceneMemory;
    }

    public void SetMemoryOfInfoScene(InfoSceneMemory infoSceneMemory)
    {
        this.infoSceneMemory = infoSceneMemory;
    }

    public void SetMemoryOfInfoTextScene(InfoTextSceneMemory infoTextSceneMemory)
    {
        this.infoTextSceneMemory = infoTextSceneMemory;
    }

    public void SetMemoryOfCommentSectionScene(CommentSectionSceneMemory commentSectionSceneMemory)
    {
        this.commentSectionSceneMemory = commentSectionSceneMemory;
    }

    public void SetMemoryOfNovelMakerScene(NovelMakerSceneMemory novelMakerSceneMemory)
    {
        this.novelMakerSceneMemory = novelMakerSceneMemory;
    }

    public void SetMemoryOfCharacterExplorerScene(CharacterExplorerSceneMemory characterExplorerSceneMemory)
    {
        this.characterExplorerSceneMemory = characterExplorerSceneMemory;
    }

    public void SetMemoryOfEnvironmentExplorerScene(EnvironmentExplorerSceneMemory environmentExplorerSceneMemory)
    {
        this.environmentExplorerSceneMemory = environmentExplorerSceneMemory;
    }

    public void SetMemoryOfHelpForNovelMakerScene(HelpForNovelMakerSceneMemory helpForNovelMakerSceneMemory)
    {
        this.helpForNovelMakerSceneMemory = helpForNovelMakerSceneMemory;
    }

    public void SetMemoryOfFinishNovelScene(FinishNovelSceneMemory finishNovelSceneMemory)
    {
        this.finishNovelSceneMemory = finishNovelSceneMemory;
    }

    public void SetMemoryOfNovelPreviewScene(NovelPreviewSceneMemory novelPreviewSceneMemory)
    {
        this.novelPreviewSceneMemory = novelPreviewSceneMemory;
    }

    public void GetMemoryOfAddObserverScene(AddObserverSceneMemory addObserverSceneMemory)
    {
        this.addObserverSceneMemory = addObserverSceneMemory;
    }

    public void GetMemoryOfAiReviewExplorerScene(AiReviewExplorerSceneMemory aiReviewExplorerSceneMemory)
    {
        this.aiReviewExplorerSceneMemory = aiReviewExplorerSceneMemory;
    }

    public void GetMemoryOfNovelReviewExplorerScene(NovelReviewExplorerSceneMemory novelReviewExplorerSceneMemory)
    {
        this.novelReviewExplorerSceneMemory = novelReviewExplorerSceneMemory;
    }

    public void GetMemoryOfFeedbackRoleManagementScene(FeedbackRoleManagementSceneMemory feedbackRoleManagementSceneMemory)
    {
        this.feedbackRoleManagementSceneMemory = feedbackRoleManagementSceneMemory;
    }

    public void GetMemoryOfReviewAiScene(ReviewAiSceneMemory reviewAiSceneMemory)
    {
        this.reviewAiSceneMemory = reviewAiSceneMemory;
    }

    public void GetMemoryOfReviewNovelScene(ReviewNovelSceneMemory reviewNovelSceneMemory)
    {
        this.reviewNovelSceneMemory = reviewNovelSceneMemory;
    }

    public void GetMemoryOfReviewObserverExplorerScene(ReviewObserverExplorerSceneMemory reviewObserverExplorerSceneMemory)
    {
        this.reviewObserverExplorerSceneMemory = reviewObserverExplorerSceneMemory;
    }

    public void ClearMemory()
    {
        this.settingsSceneMemory = null;
        this.detailsViewSceneMemory = null;
        this.feedbackSceneMemory = null;
        this.novelExplorerSceneMemory = null;
        this.playNovelSceneMemory = null;
        this.changePasswordSceneMemory = null;
        this.logInSceneMemory = null;
        this.registrationSceneMemory = null;
        this.resetPasswordSceneMemory = null;
        this.infoSceneMemory = null;
        this.infoTextSceneMemory = null;
        this.commentSectionSceneMemory = null;
        this.novelMakerSceneMemory = null;
        this.characterExplorerSceneMemory = null;
        this.environmentExplorerSceneMemory = null;
        this.helpForNovelMakerSceneMemory = null;
        this.finishNovelSceneMemory = null;
        this.novelPreviewSceneMemory = null;
        this.addObserverSceneMemory = null;
        this.aiReviewExplorerSceneMemory = null;
        this.novelReviewExplorerSceneMemory = null;
        this.feedbackRoleManagementSceneMemory = null;
        this.reviewAiSceneMemory = null;
        this.reviewNovelSceneMemory = null;
        this.reviewObserverExplorerSceneMemory = null;
    }
}
