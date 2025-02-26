using Assets._Scripts.Common.SceneMemory;

namespace Assets._Scripts.Common.Managers
{
    public class SceneMemoryManager
    {
        private static SceneMemoryManager _instance;

        private FeedbackSceneMemory _feedbackSceneMemory;
        private PlayNovelSceneMemory _playNovelSceneMemory;
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