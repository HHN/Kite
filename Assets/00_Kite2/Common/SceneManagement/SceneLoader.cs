using _00_Kite2;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public static void LoadMainMenuScene() {
        LoadScene(SceneNames.MAIN_MENU_SCENE);
    }

    public static void LoadPlayNovelScene()
    {
        // �berpr�fen, ob ein Speicherstand f�r die n�chste Szene existiert
        GameManager.Instance.CheckAndSetAllNovelsStatus();

        LoadScene(SceneNames.PLAY_NOVEL_SCENE);
    }

    public static void LoadFeedbackScene() {
        LoadScene(SceneNames.FEEDBACK_SCENE);
    }

    public static void LoadLogInScene() {
        LoadScene(SceneNames.LOG_IN_SCENE);
    }

    public static void LoadRegistrationScene() {
        LoadScene(SceneNames.REGISTRATION_SCENE);
    }

    public static void LoadResetPasswordScene() {
        LoadScene(SceneNames.RESET_PASSWORD_SCENE);
    }

    public static void LoadSettingsScene()
    {
        LoadScene(SceneNames.SETTINGS_SCENE);
    }

    public static void LoadChangePasswordSceneScene()
    {
        LoadScene(SceneNames.CHANGE_PASSWORD_SCENE);
    }

    public static void LoadCommentSectionScene()
    {
        LoadScene(SceneNames.COMMENT_SECTION_SCENE);
    }

    public static void LoadNovelMakerScene()
    {
        LoadScene(SceneNames.NOVEL_MAKER_SCENE);
    }

    public static void LoadCharacterExplorerScene()
    {
        LoadScene(SceneNames.CHARACTER_EXPLORER_SCENE);
    }

    public static void LoadEnvironmentExplorerScene()
    {
        LoadScene(SceneNames.ENVIRONMENT_EXPLORER_SCENE);
    }

    public static void LoadFinishNovelScene()
    {
        LoadScene(SceneNames.FINISH_NOVEL_SCENE);
    }

    public static void LoadHelpForNovelMakerScene()
    {
        LoadScene(SceneNames.HELP_FOR_NOVEL_MAKER_SCENE);
    }

    public static void LoadNovelPreviewScene()
    {
        LoadScene(SceneNames.NOVEL_PREVIEW_SCENE);
    }

    public static void LoadInitialTalkScene()
    {
        LoadScene(SceneNames.INITIAL_TALK_SCENE);
    }

    public static void LoadAddObserverScene()
    {
        LoadScene(SceneNames.ADD_OBSERVER_SCENE);
    }

    public static void LoadAiReviewExplorerScene()
    {
        LoadScene(SceneNames.AI_REVIEW_EXPLORER_SCENE);
    }

    public static void LoadFeedbackRoleManagementScene()
    {
        LoadScene(SceneNames.FEEDBACK_ROKE_MANAGEMENT_SCENE);
    }

    public static void LoadNovelReviewExplorerScene()
    {
        LoadScene(SceneNames.NOVEL_REVIEW_EXPLORER_SCENE);
    }

    public static void LoadReviewAiScene()
    {
        LoadScene(SceneNames.REVIEW_AI_SCENE);
    }

    public static void LoadReviewNovelScene()
    {
        LoadScene(SceneNames.REVIEW_NOVEL_SCENE);
    }

    public static void LoadPromptsAndCompletionsExplorerScene()
    {
        LoadScene(SceneNames.PROMPTS_AND_COMPLETIONS_EXPLORER_SCENE);
    }

    public static void LoadTermsOfUseScene()
    {
        LoadScene(SceneNames.TERMS_OF_USE_SCENE);
    }

    public static void LoadImprintScene()
    {
        LoadScene(SceneNames.IMPRINT_SCENE);
    }

    public static void LoadPrivacyPolicyScene()
    {
        LoadScene(SceneNames.PRIVACY_POLICY_SCENE);
    }

    public static void LoadPlayerPrefsScene()
    {
        LoadScene(SceneNames.PLAYER_PREFS_SCENE);
    }

    public static void LoadExpertFeedbackScene()
    {
        LoadScene(SceneNames.EXPERT_FEEDBACK_SCENE);
    }    
    
    public static void LoadFoundersBubbleScene()
    {
        LoadScene(SceneNames.FOUNDERS_BUBBLE_SCENE);
    }    
    
    public static void LoadFoundersWellScene()
    {
        LoadScene(SceneNames.FOUNDERS_WELL_SCENE);
    }    
    
    public static void LoadFoundersWell2Scene()
    {
        LoadScene(SceneNames.FOUNDERS_WELL_2_SCENE);
    }

    public static void LoadPlayInstructionScene()
    {
        LoadScene(SceneNames.PLAY_INSTRUCTION_SCENE);
    }    
    
    public static void LoadNovelHistoryScene()
    {
        LoadScene(SceneNames.NOVEL_HISTORY_SCENE);
    }

    public static void LoadRessourcenScene()
    {
        LoadScene(SceneNames.RESSOURCEN_SCENE);
    }

    public static void LoadBarrierefreiheitScene()
    {
        LoadScene(SceneNames.BARRIEREFREIHEIT_SCENE);
    }

    public static void LoadDatenschutzScene()
    {
        LoadScene(SceneNames.DATENSCHUTZ_SCENE);
    }

    public static void LoadImpressumScene()
    {
        LoadScene(SceneNames.IMPRESSUM_SCENE);
    }

    public static void LoadNutzungsbedingungenScene()
    {
        LoadScene(SceneNames.NUTZUNGSBEDINGUNGEN_SCENE);
    }    
    
    public static void LoadEinstellungenScene()
    {
        LoadScene(SceneNames.EINSTELLUNGEN_SCENE);
    }    
    
    public static void LoadGemerkteNovelsScene()
    {
        LoadScene(SceneNames.GEMERKTE_NOVELS_SCENE);
    }

    public static void LoadScene(string sceneName)
    {
        GameObject oldSceneControllerGameObject = GameObject.Find("Controller");
        if (oldSceneControllerGameObject != null)
        {
            SceneController oldSceneController = oldSceneControllerGameObject.GetComponent<SceneController>();
            if (oldSceneController != null)
            {
                oldSceneController.OnStop();
            }
        }
        SceneManager.LoadScene(sceneName);
    } 
}
