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
    private CommentSectionSceneMemory commentSectionSceneMemory;
    private NovelMakerSceneMemory novelMakerSceneMemory;
    private CharacterExplorerSceneMemory characterExplorerSceneMemory;
    private EnvironmentExplorerSceneMemory environmentExplorerSceneMemory;
    private HelpForNovelMakerSceneMemory helpForNovelMakerSceneMemory;
    private FinishNovelSceneMemory finishNovelSceneMemory;
    private NovelPreviewSceneMemory novelPreviewSceneMemory;

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
        this.commentSectionSceneMemory = null;
        this.novelMakerSceneMemory = null;
        this.characterExplorerSceneMemory = null;
        this.environmentExplorerSceneMemory = null;
        this.helpForNovelMakerSceneMemory = null;
        this.finishNovelSceneMemory = null;
        this.novelPreviewSceneMemory = null;
    }
}
