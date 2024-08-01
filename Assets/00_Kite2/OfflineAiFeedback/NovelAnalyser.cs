using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovelAnalyser
{
    private VisualNovel objectToAnalyse;
    private bool isAnalysed;
    private bool loopDetected;
    private List<FeedbackNodeContainer> allPossiblePaths;
    private int numberOfAllPossiblePaths;

    public NovelAnalyser()
    {
        allPossiblePaths = new List<FeedbackNodeContainer>();
        numberOfAllPossiblePaths = 0;
    }

    public IEnumerator AnalyseNovel(VisualNovel visualNovel)
    {
        objectToAnalyse = visualNovel;
        isAnalysed = true;

        List<FeedbackNodeContainer> allPossiblePaths = new List<FeedbackNodeContainer>();

        NovelAnalyserHelper novelAnalyserHelper = new NovelAnalyserHelper(visualNovel);
        novelAnalyserHelper.AnalyseNovel();

        while (!novelAnalyserHelper.IsAnalysationOver())
        {
            yield return new WaitForSeconds(0.5f);
        }

        VisualNovelNames name = VisualNovelNamesHelper.ValueOf((int) visualNovel.id);

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

        this.loopDetected = novelAnalyserHelper.loopDetected;
        this.allPossiblePaths = allPossiblePaths;
        this.numberOfAllPossiblePaths = allPossiblePaths.Count;
    }

    public bool IsAnalysed() 
    { 
        return isAnalysed; 
    }

    public bool LoopDetected()
    {
        return loopDetected;
    }

    public List<FeedbackNodeContainer> GetAllPossiblePaths() 
    {
        return allPossiblePaths;
    }

    public int GetNumberOfPossiblePaths()
    {
        return numberOfAllPossiblePaths;
    }
}
