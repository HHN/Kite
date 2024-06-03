public class SceneMemoryManager
{
    private static SceneMemoryManager instance;

    private SettingsSceneMemory settingsSceneMemory;
    private DetailsViewSceneMemory detailsViewSceneMemory;
    private FeedbackSceneMemory feedbackSceneMemory;
    private NovelExplorerSceneMemory novelExplorerSceneMemory;
    private PlayNovelSceneMemory playNovelSceneMemory;
    private InfoSceneMemory infoSceneMemory;
    private InfoTextSceneMemory infoTextSceneMemory;
    private AddObserverSceneMemory addObserverSceneMemory;
    private AiReviewExplorerSceneMemory aiReviewExplorerSceneMemory;
    private NovelReviewExplorerSceneMemory novelReviewExplorerSceneMemory;
    private FeedbackRoleManagementSceneMemory feedbackRoleManagementSceneMemory;
    private ReviewAiSceneMemory reviewAiSceneMemory;
    private ReviewNovelSceneMemory reviewNovelSceneMemory;
    private PromptsAndCompletionsExplorerSceneMemory promptsAndCompletionsExplorerSceneMemory;
    private ImprintSceneMemory imprintSceneMemory;
    private TermsOfUseSceneMemory termsOfUseSceneMemory;
    private PrivacyPolicySceneMemory privacyPolicySceneMemory;
    private ExpertFeedbackSceneMemory expertFeedbackSceneMemory;
    private FoundersBubbleSceneMemory foundersBubbleSceneMemory;
    private FoundersWellSceneMemory foundersWellSceneMemory;
    private FoundersWell2SceneMemory foundersWell2SceneMemory;
    private PlayInstructionSceneMemory playInstructionSceneMemory;
    private NovelHistorySceneMemory novelHistorySceneMemory;

    private BarrierefreiheitSceneMemory barrierefreiheitSceneMemory;
    private ImpressumSceneMemory impressumSceneMemory;
    private DatenschutzSceneMemory datenschutzSceneMemory;
    private NutzungsbedingungenSceneMemory nutzungsbedingungenSceneMemory;
    private RessourcenSceneMemory ressourcenSceneMemory;

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

    public InfoSceneMemory GetMemoryOfInfoScene()
    {
        return this.infoSceneMemory;
    }

    public InfoTextSceneMemory GetMemoryOfInfoTextScene()
    {
        return this.infoTextSceneMemory;
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

    public PromptsAndCompletionsExplorerSceneMemory GetMemoryOfPromptsAndCompletionsExplorerScene()
    {
        return this.promptsAndCompletionsExplorerSceneMemory;
    }

    public ImprintSceneMemory GetMemoryOfImprintScene()
    {
        return this.imprintSceneMemory;
    }

    public TermsOfUseSceneMemory GetMemoryOfTermsOfUseScene()
    {
        return this.termsOfUseSceneMemory;
    }

    public PrivacyPolicySceneMemory GetMemoryOfPrivacyPolicyScene()
    {
        return this.privacyPolicySceneMemory;
    }

    public ExpertFeedbackSceneMemory GetMemoryOfExpertFeedbackScene()
    {
        return this.expertFeedbackSceneMemory;
    }    
    
    public FoundersBubbleSceneMemory GetMemoryOfFoundersBubbleScene()
    {
        return this.foundersBubbleSceneMemory;
    }    
    
    public FoundersWellSceneMemory GetMemoryOfFoundersWellScene()
    {
        return this.foundersWellSceneMemory;
    }       
    
    public FoundersWell2SceneMemory GetMemoryOfFoundersWell2Scene()
    {
        return this.foundersWell2SceneMemory;
    }    
    
    public PlayInstructionSceneMemory GetMemoryOfPlayInstructionScene()
    {
        return this.playInstructionSceneMemory;
    }       
    
    public ImpressumSceneMemory GetMemoryOfImpressumScene()
    {
        return this.impressumSceneMemory;
    }        
    
    public DatenschutzSceneMemory GetMemoryOfDatenschutzScene()
    {
        return this.datenschutzSceneMemory;
    }       
    
    public NutzungsbedingungenSceneMemory GetMemoryOfNutzungsbedingungenScene()
    {
        return this.nutzungsbedingungenSceneMemory;
    }      
    
    public BarrierefreiheitSceneMemory GetMemoryOfBarrierefreiheitScene()
    {
        return this.barrierefreiheitSceneMemory;
    }       
    
    public RessourcenSceneMemory GetMemoryOfRessourcenScene()
    {
        return this.ressourcenSceneMemory;
    }    
    
    public NovelHistorySceneMemory GetMemoryOfNovelHistoryScene()
    {
        return this.novelHistorySceneMemory;
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

    public void SetMemoryOfInfoScene(InfoSceneMemory infoSceneMemory)
    {
        this.infoSceneMemory = infoSceneMemory;
    }

    public void SetMemoryOfInfoTextScene(InfoTextSceneMemory infoTextSceneMemory)
    {
        this.infoTextSceneMemory = infoTextSceneMemory;
    }

    public void SetMemoryOfImprintScene(ImprintSceneMemory imprintSceneMemory)
    {
        this.imprintSceneMemory = imprintSceneMemory;
    }

    public void SetMemoryOfTermsOfUseScene(TermsOfUseSceneMemory termsOfUseSceneMemory)
    {
        this.termsOfUseSceneMemory = termsOfUseSceneMemory;
    }

    public void SetMemoryOfPrivacyPolicyScene(PrivacyPolicySceneMemory privacyPolicySceneMemory)
    {
        this.privacyPolicySceneMemory = privacyPolicySceneMemory;
    }

    public void SetMemoryOfExpertFeedbackScene(ExpertFeedbackSceneMemory expertFeedbackSceneMemory)
    {
        this.expertFeedbackSceneMemory = expertFeedbackSceneMemory;
    }

    public void SetMemoryOfAddObserverScene(AddObserverSceneMemory addObserverSceneMemory)
    {
        this.addObserverSceneMemory = addObserverSceneMemory;
    }

    public void SetMemoryOfAiReviewExplorerScene(AiReviewExplorerSceneMemory aiReviewExplorerSceneMemory)
    {
        this.aiReviewExplorerSceneMemory = aiReviewExplorerSceneMemory;
    }

    public void SetMemoryOfNovelReviewExplorerScene(NovelReviewExplorerSceneMemory novelReviewExplorerSceneMemory)
    {
        this.novelReviewExplorerSceneMemory = novelReviewExplorerSceneMemory;
    }

    public void SetMemoryOfFeedbackRoleManagementScene(FeedbackRoleManagementSceneMemory feedbackRoleManagementSceneMemory)
    {
        this.feedbackRoleManagementSceneMemory = feedbackRoleManagementSceneMemory;
    }

    public void SetMemoryOfReviewAiScene(ReviewAiSceneMemory reviewAiSceneMemory)
    {
        this.reviewAiSceneMemory = reviewAiSceneMemory;
    }

    public void SetMemoryOfReviewNovelScene(ReviewNovelSceneMemory reviewNovelSceneMemory)
    {
        this.reviewNovelSceneMemory = reviewNovelSceneMemory;
    }

    public void SetMemoryOfPromptsAndCompletionsExplorerScene(PromptsAndCompletionsExplorerSceneMemory promptsAndCompletionsExplorerSceneMemory)
    {
        this.promptsAndCompletionsExplorerSceneMemory = promptsAndCompletionsExplorerSceneMemory;
    }    
    
    public void SetMemoryOfFoundersBubbleScene(FoundersBubbleSceneMemory foundersBubbleSceneMemory)
    {
        this.foundersBubbleSceneMemory = foundersBubbleSceneMemory;
    }    
    
    public void SetMemoryOfFoundersWellScene(FoundersWellSceneMemory foundersWellSceneMemory)
    {
        this.foundersWellSceneMemory = foundersWellSceneMemory;
    }      
    
    public void SetMemoryOfFoundersWell2Scene(FoundersWell2SceneMemory foundersWell2SceneMemory)
    {
        this.foundersWell2SceneMemory = foundersWell2SceneMemory;
    }    
    
    public void SetMemoryOfPlayInstructionScene(PlayInstructionSceneMemory playInstructionSceneMemory)
    {
        this.playInstructionSceneMemory = playInstructionSceneMemory;
    }    
    
    public void SetMemoryOfNovelHistoryScene(NovelHistorySceneMemory novelHistorySceneMemory)
    {
        this.novelHistorySceneMemory = novelHistorySceneMemory;
    }    
    
    public void SetMemoryOfDatenschutzScene(DatenschutzSceneMemory datenschutzSceneMemory)
    {
        this.datenschutzSceneMemory = datenschutzSceneMemory;
    }    
    
    public void SetMemoryOfNutzungsbedingungenScene(NutzungsbedingungenSceneMemory nutzungsbedingungenSceneMemory)
    {
        this.nutzungsbedingungenSceneMemory = nutzungsbedingungenSceneMemory;
    }    
    
    public void SetMemoryOfImpressumScene(ImpressumSceneMemory impressumSceneMemory)
    {
        this.impressumSceneMemory = impressumSceneMemory;
    }    
    
    public void SetMemoryOfRessourcenScene(RessourcenSceneMemory ressourcenSceneMemory)
    {
        this.ressourcenSceneMemory = ressourcenSceneMemory;
    }    
    
    public void SetMemoryOfBarrierefreiheit(BarrierefreiheitSceneMemory barrierefreiheitSceneMemory)
    {
        this.barrierefreiheitSceneMemory = barrierefreiheitSceneMemory;
    }

    public void ClearMemory()
    {
        this.settingsSceneMemory = null;
        this.detailsViewSceneMemory = null;
        this.feedbackSceneMemory = null;
        this.novelExplorerSceneMemory = null;
        this.playNovelSceneMemory = null;
        this.infoSceneMemory = null;
        this.infoTextSceneMemory = null;
        this.addObserverSceneMemory = null;
        this.aiReviewExplorerSceneMemory = null;
        this.novelReviewExplorerSceneMemory = null;
        this.feedbackRoleManagementSceneMemory = null;
        this.reviewAiSceneMemory = null;
        this.reviewNovelSceneMemory = null;
        this.promptsAndCompletionsExplorerSceneMemory = null;
        this.termsOfUseSceneMemory = null;
        this.imprintSceneMemory = null;
        this.privacyPolicySceneMemory = null;
        this.expertFeedbackSceneMemory = null;
        this.foundersBubbleSceneMemory = null;
        this.foundersWellSceneMemory = null;
        this.foundersWell2SceneMemory = null;
        this.playInstructionSceneMemory = null;
        this.novelHistorySceneMemory = null;

        this.barrierefreiheitSceneMemory = null;
        this.ressourcenSceneMemory = null;
        this.impressumSceneMemory = null;
        this.datenschutzSceneMemory = null;
        this.nutzungsbedingungenSceneMemory = null;
    }
}
