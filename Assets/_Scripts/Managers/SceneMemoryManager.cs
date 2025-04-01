using Assets._Scripts.SceneMemory;

namespace Assets._Scripts.Managers
{
    public class SceneMemoryManager
    {
        private static SceneMemoryManager _instance;

        private FeedbackSceneMemory _feedbackSceneMemory;
        private PlayNovelSceneMemory _playNovelSceneMemory;
        private FoundersBubbleSceneMemory _foundersBubbleSceneMemory;
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
            return _feedbackSceneMemory;
        }

        public PlayNovelSceneMemory GetMemoryOfPlayNovelScene()
        {
            return _playNovelSceneMemory;
        }

        public FoundersBubbleSceneMemory GetMemoryOfFoundersBubbleScene()
        {
            return _foundersBubbleSceneMemory;
        }

        public PlayInstructionSceneMemory GetMemoryOfPlayInstructionScene()
        {
            return _playInstructionSceneMemory;
        }

        public ImpressumSceneMemory GetMemoryOfImpressumScene()
        {
            return _impressumSceneMemory;
        }

        public DatenschutzSceneMemory GetMemoryOfDatenschutzScene()
        {
            return _datenschutzSceneMemory;
        }

        public NutzungsbedingungenSceneMemory GetMemoryOfNutzungsbedingungenScene()
        {
            return _nutzungsbedingungenSceneMemory;
        }

        public BarrierefreiheitSceneMemory GetMemoryOfBarrierefreiheitScene()
        {
            return _barrierefreiheitSceneMemory;
        }

        public RessourcenSceneMemory GetMemoryOfRessourcenScene()
        {
            return _ressourcenSceneMemory;
        }

        public NovelHistorySceneMemory GetMemoryOfNovelHistoryScene()
        {
            return _novelHistorySceneMemory;
        }

        public void SetMemoryOfFeedbackScene(FeedbackSceneMemory feedbackSceneMemory)
        {
            _feedbackSceneMemory = feedbackSceneMemory;
        }

        public void SetMemoryOfPlayNovelScene(PlayNovelSceneMemory playNovelSceneMemory)
        {
            _playNovelSceneMemory = playNovelSceneMemory;
        }

        public void SetMemoryOfFoundersBubbleScene(FoundersBubbleSceneMemory foundersBubbleSceneMemory)
        {
            _foundersBubbleSceneMemory = foundersBubbleSceneMemory;
        }

        public void SetMemoryOfPlayInstructionScene(PlayInstructionSceneMemory playInstructionSceneMemory)
        {
            _playInstructionSceneMemory = playInstructionSceneMemory;
        }

        public void SetMemoryOfNovelHistoryScene(NovelHistorySceneMemory novelHistorySceneMemory)
        {
            _novelHistorySceneMemory = novelHistorySceneMemory;
        }

        public void SetMemoryOfDatenschutzScene(DatenschutzSceneMemory datenschutzSceneMemory)
        {
            _datenschutzSceneMemory = datenschutzSceneMemory;
        }

        public void SetMemoryOfNutzungsbedingungenScene(NutzungsbedingungenSceneMemory nutzungsbedingungenSceneMemory)
        {
            _nutzungsbedingungenSceneMemory = nutzungsbedingungenSceneMemory;
        }

        public void SetMemoryOfImpressumScene(ImpressumSceneMemory impressumSceneMemory)
        {
            _impressumSceneMemory = impressumSceneMemory;
        }

        public void SetMemoryOfRessourcenScene(RessourcenSceneMemory ressourcenSceneMemory)
        {
            _ressourcenSceneMemory = ressourcenSceneMemory;
        }

        public void SetMemoryOfBarrierefreiheit(BarrierefreiheitSceneMemory barrierefreiheitSceneMemory)
        {
            _barrierefreiheitSceneMemory = barrierefreiheitSceneMemory;
        }

        public void ClearMemory()
        {
            _feedbackSceneMemory = null;
            _playNovelSceneMemory = null;
            _foundersBubbleSceneMemory = null;
            _playInstructionSceneMemory = null;
            _novelHistorySceneMemory = null;

            _barrierefreiheitSceneMemory = null;
            _ressourcenSceneMemory = null;
            _impressumSceneMemory = null;
            _datenschutzSceneMemory = null;
            _nutzungsbedingungenSceneMemory = null;
        }
    }
}