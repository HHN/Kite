namespace _00_Kite2.Common.Managers
{
    public class SceneMemoryManager
    {
        private static SceneMemoryManager _instance;

        private FeedbackSceneMemory _feedbackSceneMemory;
        private PlayNovelSceneMemory _playNovelSceneMemory;
        private AddObserverSceneMemory _addObserverSceneMemory;
        private AiReviewExplorerSceneMemory _aiReviewExplorerSceneMemory;
        private NovelReviewExplorerSceneMemory _novelReviewExplorerSceneMemory;
        private FeedbackRoleManagementSceneMemory _feedbackRoleManagementSceneMemory;
        private ReviewAiSceneMemory _reviewAiSceneMemory;
        private ReviewNovelSceneMemory _reviewNovelSceneMemory;
        private PromptsAndCompletionsExplorerSceneMemory _promptsAndCompletionsExplorerSceneMemory;
        private ImprintSceneMemory _imprintSceneMemory;
        private TermsOfUseSceneMemory _termsOfUseSceneMemory;
        private PrivacyPolicySceneMemory _privacyPolicySceneMemory;
        private ExpertFeedbackSceneMemory _expertFeedbackSceneMemory;
        private FoundersBubbleSceneMemory _foundersBubbleSceneMemory;
        private FoundersWell2SceneMemory _foundersWell2SceneMemory;
        private PlayInstructionSceneMemory _playInstructionSceneMemory;
        private NovelHistorySceneMemory _novelHistorySceneMemory;

        private BarrierefreiheitSceneMemory _barrierefreiheitSceneMemory;
        private ImpressumSceneMemory _impressumSceneMemory;
        private DatenschutzSceneMemory _datenschutzSceneMemory;
        private NutzungsbedingungenSceneMemory _nutzungsbedingungenSceneMemory;
        private RessourcenSceneMemory _ressourcenSceneMemory;

        private SceneMemoryManager()
        {
            ClearMemory();
        }

        public static SceneMemoryManager Instance()
        {
            if (_instance == null)
            {
                _instance = new SceneMemoryManager();
            }

            return _instance;
        }

        public FeedbackSceneMemory GetMemoryOfFeedbackScene()
        {
            return this._feedbackSceneMemory;
        }

        public PlayNovelSceneMemory GetMemoryOfPlayNovelScene()
        {
            return this._playNovelSceneMemory;
        }

        public AddObserverSceneMemory GetMemoryOfAddObserverScene()
        {
            return this._addObserverSceneMemory;
        }

        public AiReviewExplorerSceneMemory GetMemoryOfAiReviewExplorerScene()
        {
            return this._aiReviewExplorerSceneMemory;
        }

        public NovelReviewExplorerSceneMemory GetMemoryOfNovelReviewExplorerScene()
        {
            return this._novelReviewExplorerSceneMemory;
        }

        public FeedbackRoleManagementSceneMemory GetMemoryOfFeedbackRoleManagementScene()
        {
            return this._feedbackRoleManagementSceneMemory;
        }

        public ReviewAiSceneMemory GetMemoryOfReviewAiScene()
        {
            return this._reviewAiSceneMemory;
        }

        public ReviewNovelSceneMemory GetMemoryOfReviewNovelScene()
        {
            return this._reviewNovelSceneMemory;
        }

        public PromptsAndCompletionsExplorerSceneMemory GetMemoryOfPromptsAndCompletionsExplorerScene()
        {
            return this._promptsAndCompletionsExplorerSceneMemory;
        }

        public ImprintSceneMemory GetMemoryOfImprintScene()
        {
            return this._imprintSceneMemory;
        }

        public TermsOfUseSceneMemory GetMemoryOfTermsOfUseScene()
        {
            return this._termsOfUseSceneMemory;
        }

        public PrivacyPolicySceneMemory GetMemoryOfPrivacyPolicyScene()
        {
            return this._privacyPolicySceneMemory;
        }

        public ExpertFeedbackSceneMemory GetMemoryOfExpertFeedbackScene()
        {
            return this._expertFeedbackSceneMemory;
        }

        public FoundersBubbleSceneMemory GetMemoryOfFoundersBubbleScene()
        {
            return this._foundersBubbleSceneMemory;
        }

        public FoundersWell2SceneMemory GetMemoryOfFoundersWell2Scene()
        {
            return this._foundersWell2SceneMemory;
        }

        public PlayInstructionSceneMemory GetMemoryOfPlayInstructionScene()
        {
            return this._playInstructionSceneMemory;
        }

        public ImpressumSceneMemory GetMemoryOfImpressumScene()
        {
            return this._impressumSceneMemory;
        }

        public DatenschutzSceneMemory GetMemoryOfDatenschutzScene()
        {
            return this._datenschutzSceneMemory;
        }

        public NutzungsbedingungenSceneMemory GetMemoryOfNutzungsbedingungenScene()
        {
            return this._nutzungsbedingungenSceneMemory;
        }

        public BarrierefreiheitSceneMemory GetMemoryOfBarrierefreiheitScene()
        {
            return this._barrierefreiheitSceneMemory;
        }

        public RessourcenSceneMemory GetMemoryOfRessourcenScene()
        {
            return this._ressourcenSceneMemory;
        }

        public NovelHistorySceneMemory GetMemoryOfNovelHistoryScene()
        {
            return this._novelHistorySceneMemory;
        }

        public void SetMemoryOfFeedbackScene(FeedbackSceneMemory feedbackSceneMemory)
        {
            this._feedbackSceneMemory = feedbackSceneMemory;
        }

        public void SetMemoryOfPlayNovelScene(PlayNovelSceneMemory playNovelSceneMemory)
        {
            this._playNovelSceneMemory = playNovelSceneMemory;
        }

        public void SetMemoryOfImprintScene(ImprintSceneMemory imprintSceneMemory)
        {
            this._imprintSceneMemory = imprintSceneMemory;
        }

        public void SetMemoryOfTermsOfUseScene(TermsOfUseSceneMemory termsOfUseSceneMemory)
        {
            this._termsOfUseSceneMemory = termsOfUseSceneMemory;
        }

        public void SetMemoryOfPrivacyPolicyScene(PrivacyPolicySceneMemory privacyPolicySceneMemory)
        {
            this._privacyPolicySceneMemory = privacyPolicySceneMemory;
        }

        public void SetMemoryOfExpertFeedbackScene(ExpertFeedbackSceneMemory expertFeedbackSceneMemory)
        {
            this._expertFeedbackSceneMemory = expertFeedbackSceneMemory;
        }

        public void SetMemoryOfAddObserverScene(AddObserverSceneMemory addObserverSceneMemory)
        {
            this._addObserverSceneMemory = addObserverSceneMemory;
        }

        public void SetMemoryOfAiReviewExplorerScene(AiReviewExplorerSceneMemory aiReviewExplorerSceneMemory)
        {
            this._aiReviewExplorerSceneMemory = aiReviewExplorerSceneMemory;
        }

        public void SetMemoryOfNovelReviewExplorerScene(NovelReviewExplorerSceneMemory novelReviewExplorerSceneMemory)
        {
            this._novelReviewExplorerSceneMemory = novelReviewExplorerSceneMemory;
        }

        public void SetMemoryOfFeedbackRoleManagementScene(
            FeedbackRoleManagementSceneMemory feedbackRoleManagementSceneMemory)
        {
            this._feedbackRoleManagementSceneMemory = feedbackRoleManagementSceneMemory;
        }

        public void SetMemoryOfReviewAiScene(ReviewAiSceneMemory reviewAiSceneMemory)
        {
            this._reviewAiSceneMemory = reviewAiSceneMemory;
        }

        public void SetMemoryOfReviewNovelScene(ReviewNovelSceneMemory reviewNovelSceneMemory)
        {
            this._reviewNovelSceneMemory = reviewNovelSceneMemory;
        }

        public void SetMemoryOfPromptsAndCompletionsExplorerScene(
            PromptsAndCompletionsExplorerSceneMemory promptsAndCompletionsExplorerSceneMemory)
        {
            this._promptsAndCompletionsExplorerSceneMemory = promptsAndCompletionsExplorerSceneMemory;
        }

        public void SetMemoryOfFoundersBubbleScene(FoundersBubbleSceneMemory foundersBubbleSceneMemory)
        {
            this._foundersBubbleSceneMemory = foundersBubbleSceneMemory;
        }

        public void SetMemoryOfFoundersWell2Scene(FoundersWell2SceneMemory foundersWell2SceneMemory)
        {
            this._foundersWell2SceneMemory = foundersWell2SceneMemory;
        }

        public void SetMemoryOfPlayInstructionScene(PlayInstructionSceneMemory playInstructionSceneMemory)
        {
            this._playInstructionSceneMemory = playInstructionSceneMemory;
        }

        public void SetMemoryOfNovelHistoryScene(NovelHistorySceneMemory novelHistorySceneMemory)
        {
            this._novelHistorySceneMemory = novelHistorySceneMemory;
        }

        public void SetMemoryOfDatenschutzScene(DatenschutzSceneMemory datenschutzSceneMemory)
        {
            this._datenschutzSceneMemory = datenschutzSceneMemory;
        }

        public void SetMemoryOfNutzungsbedingungenScene(NutzungsbedingungenSceneMemory nutzungsbedingungenSceneMemory)
        {
            this._nutzungsbedingungenSceneMemory = nutzungsbedingungenSceneMemory;
        }

        public void SetMemoryOfImpressumScene(ImpressumSceneMemory impressumSceneMemory)
        {
            this._impressumSceneMemory = impressumSceneMemory;
        }

        public void SetMemoryOfRessourcenScene(RessourcenSceneMemory ressourcenSceneMemory)
        {
            this._ressourcenSceneMemory = ressourcenSceneMemory;
        }

        public void SetMemoryOfBarrierefreiheit(BarrierefreiheitSceneMemory barrierefreiheitSceneMemory)
        {
            this._barrierefreiheitSceneMemory = barrierefreiheitSceneMemory;
        }

        public void ClearMemory()
        {
            this._feedbackSceneMemory = null;
            this._playNovelSceneMemory = null;
            this._addObserverSceneMemory = null;
            this._aiReviewExplorerSceneMemory = null;
            this._novelReviewExplorerSceneMemory = null;
            this._feedbackRoleManagementSceneMemory = null;
            this._reviewAiSceneMemory = null;
            this._reviewNovelSceneMemory = null;
            this._promptsAndCompletionsExplorerSceneMemory = null;
            this._termsOfUseSceneMemory = null;
            this._imprintSceneMemory = null;
            this._privacyPolicySceneMemory = null;
            this._expertFeedbackSceneMemory = null;
            this._foundersBubbleSceneMemory = null;
            this._foundersWell2SceneMemory = null;
            this._playInstructionSceneMemory = null;
            this._novelHistorySceneMemory = null;

            this._barrierefreiheitSceneMemory = null;
            this._ressourcenSceneMemory = null;
            this._impressumSceneMemory = null;
            this._datenschutzSceneMemory = null;
            this._nutzungsbedingungenSceneMemory = null;
        }
    }
}