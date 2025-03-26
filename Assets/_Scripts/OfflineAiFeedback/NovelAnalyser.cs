using System.Collections;
using System.Collections.Generic;
using Assets._Scripts.Novels;
using Assets._Scripts.Player;
using UnityEngine;

namespace Assets._Scripts.OfflineAiFeedback
{
    public class NovelAnalyser
    {
        private VisualNovel _objectToAnalyse;
        private bool _isAnalysed;
        private bool _loopDetected;
        private List<FeedbackNodeContainer> _allPossiblePaths;
        private int _numberOfAllPossiblePaths;

        public NovelAnalyser()
        {
            _allPossiblePaths = new List<FeedbackNodeContainer>();
            _numberOfAllPossiblePaths = 0;
        }

        public IEnumerator AnalyseNovel(VisualNovel visualNovel)
        {
            _objectToAnalyse = visualNovel;
            _isAnalysed = true;

            List<FeedbackNodeContainer> allPossiblePaths = new List<FeedbackNodeContainer>();

            NovelAnalyserHelper novelAnalyserHelper = new NovelAnalyserHelper(visualNovel);
            novelAnalyserHelper.AnalyseNovel();

            while (!novelAnalyserHelper.IsAnalysisOver())
            {
                yield return new WaitForSeconds(0.5f);
            }

            VisualNovelNames name = VisualNovelNamesHelper.ValueOf((int)visualNovel.id);

            List<NovelAnalyserHelper> analysers = NovelAnalyserHelper.GetAllPossibleNovelAnalyserHelpers(name);

            foreach (NovelAnalyserHelper helper in analysers)
            {
                FeedbackNodeContainer feedbackNodeContainer = new FeedbackNodeContainer();
                feedbackNodeContainer.novel = visualNovel.id;
                feedbackNodeContainer.path = helper.GetPath();
                feedbackNodeContainer.prompt = helper.GetPrompt();
                feedbackNodeContainer.completion = "";
                allPossiblePaths.Add(feedbackNodeContainer);
            }

            this._loopDetected = novelAnalyserHelper.LoopDetected;
            this._allPossiblePaths = allPossiblePaths;
            this._numberOfAllPossiblePaths = allPossiblePaths.Count;
        }

        public bool IsAnalysed()
        {
            return _isAnalysed;
        }

        public bool LoopDetected()
        {
            return _loopDetected;
        }

        public List<FeedbackNodeContainer> GetAllPossiblePaths()
        {
            return _allPossiblePaths;
        }

        public int GetNumberOfPossiblePaths()
        {
            return _numberOfAllPossiblePaths;
        }
    }
}