using UnityEngine;

public class SceneMemoryManager
{
    private static SceneMemoryManager instance;

    private SettingsSceneMemory settingsSceneMemory;
    private CharakterMakerSceneMemory charakterMakerSceneMemory;
    private DialogueMakerSceneMemory dialogueMakerSceneMemory;
    private EnvironmentMakerSceneMemory environmentMakerSceneMemory;
    private GenerateNovelSceneMemory generateNovelSceneMemory;
    private SaveNovelSceneMemory saveNovelSceneMemory;
    private DetailsViewSceneMemory detailsViewSceneMemory;
    private FeedbackSceneMemory feedbackSceneMemory;
    private NovelExplorerSceneMemory novelExplorerSceneMemory;
    private PlayNovelSceneMemory playNovelSceneMemory;
    private ChangePasswordSceneMemory changePasswordSceneMemory;
    private LogInSceneMemory logInSceneMemory;
    private RegistrationSceneMemory registrationSceneMemory;
    private ResetPasswordSceneMemory resetPasswordSceneMemory;

    private SceneMemoryManager()
    {
        this.settingsSceneMemory = null;
        this.charakterMakerSceneMemory = null;
        this.dialogueMakerSceneMemory = null;
        this.environmentMakerSceneMemory = null;
        this.generateNovelSceneMemory = null;
        this.saveNovelSceneMemory = null;
        this.detailsViewSceneMemory = null;
        this.feedbackSceneMemory = null;
        this.novelExplorerSceneMemory = null;
        this.playNovelSceneMemory = null;
        this.changePasswordSceneMemory = null;
        this.logInSceneMemory = null;
        this.registrationSceneMemory = null;
        this.resetPasswordSceneMemory = null;
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

    public CharakterMakerSceneMemory GetMemoryOfCharakterMakerScene()
    {
        return this.charakterMakerSceneMemory;
    }

    public DialogueMakerSceneMemory GetMemoryOfDialogueMakerScene()
    {
        return this.dialogueMakerSceneMemory;
    }

    public EnvironmentMakerSceneMemory GetMemoryOfEnvironmentMakerScene()
    {
        return this.environmentMakerSceneMemory;
    }

    public GenerateNovelSceneMemory GetMemoryOfGenerateNovelScene()
    {
        return this.generateNovelSceneMemory;
    }

    public SaveNovelSceneMemory GetMemoryOfSaveNovelScene()
    {
        return this.saveNovelSceneMemory;
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

    public void SetMemoryOfSettingsScene(SettingsSceneMemory settingsSceneMemory)
    {
        this.settingsSceneMemory = settingsSceneMemory;
    }

    public void SetMemoryOfCharakterMakerScene(CharakterMakerSceneMemory charakterMakerSceneMemory)
    {
        this.charakterMakerSceneMemory = charakterMakerSceneMemory;
    }

    public void SetMemoryOfDialogueMakerScene(DialogueMakerSceneMemory dialogueMakerSceneMemory)
    {
        this.dialogueMakerSceneMemory = dialogueMakerSceneMemory;
    }

    public void SetMemoryOfEnvironmentMakerScene(EnvironmentMakerSceneMemory environmentMakerSceneMemory)
    {
        this.environmentMakerSceneMemory = environmentMakerSceneMemory;
    }

    public void SetMemoryOfGenerateNovelScene(GenerateNovelSceneMemory generateNovelSceneMemory)
    {
        this.generateNovelSceneMemory = generateNovelSceneMemory;
    }

    public void SetMemoryOfSaveNovelScene(SaveNovelSceneMemory saveNovelSceneMemory)
    {
        this.saveNovelSceneMemory = saveNovelSceneMemory;
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

    public void GetMemoryOfResetPasswordScene(ResetPasswordSceneMemory resetPasswordSceneMemory)
    {
        this.resetPasswordSceneMemory = resetPasswordSceneMemory;
    }

    public void ClearMemory()
    {
        this.settingsSceneMemory = null;
        this.charakterMakerSceneMemory = null;
        this.dialogueMakerSceneMemory = null;
        this.environmentMakerSceneMemory = null;
        this.generateNovelSceneMemory = null;
        this.saveNovelSceneMemory = null;
        this.detailsViewSceneMemory = null;
        this.feedbackSceneMemory = null;
        this.novelExplorerSceneMemory = null;
        this.playNovelSceneMemory = null;
        this.changePasswordSceneMemory = null;
        this.logInSceneMemory = null;
        this.registrationSceneMemory = null;
        this.resetPasswordSceneMemory = null;
    }
}
