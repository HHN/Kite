using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;
using System.Diagnostics;
using System.Collections.Generic;

public class AnalyticsServiceHandler 
{

    private static AnalyticsServiceHandler instance;

    private Stopwatch stopwatch;

    private string fromWhereIsNovelSelected = "KITE NOVELS";

    private List<string> choiceList = new List<string>();

    private string lastQuestionForChoice;

    private long idOfCurrentNovel;

    // Private Konstruktor, um direkte Instanziierung zu verhindern
    private AnalyticsServiceHandler() {}


    public static AnalyticsServiceHandler Instance()
    {
        if (instance == null)
        {
            instance = new AnalyticsServiceHandler();
        }
        return instance;
    }
 
    public async void StartAnalytics()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();    //TODO: show the player a UI element that asks for consent.
        UnityEngine.Debug.Log("UserID: " + AnalyticsService.Instance.GetAnalyticsUserID());
        StartStopwatch();
    }

    public void StartStopwatch()
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
    }

    public long StopStopwatch()
    {
        stopwatch.Stop();
        UnityEngine.Debug.Log("Zeit seit dem Start: " + stopwatch.ElapsedMilliseconds);
        return stopwatch.ElapsedMilliseconds;
    }

    public void SetFromWhereIsNovelSelected(string fromWhereIsNovelSelected)
    {
        this.fromWhereIsNovelSelected = fromWhereIsNovelSelected;
    }

    public void AddChoiceToList(string choice)
    {
        choiceList.Add(choice);
    }

    private int GetChoiceIdByText(string text)
    {
        return choiceList.IndexOf(text);
    }

    private string GetTextByChoiceId(int id)
    {
        return choiceList[id];
    }

    public void SetLastQuestionForChoice(string question)
    {
        lastQuestionForChoice = question;
    }

    public void SetIdOfCurrentNovel(long id)
    {
        idOfCurrentNovel = id;
    }

    public void SendMainMenuStatistics()
    {
        StopStopwatch();
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            {"currentUserID", AnalyticsService.Instance.GetAnalyticsUserID()},
            {"millisecondsInMainMenu", stopwatch.ElapsedMilliseconds}
        };
        AnalyticsService.Instance.CustomData("mainMenuStatistics", parameters);
        // UnityEngine.Debug.Log("");
    }

    public void SendNovelExplorerStatistics(long visualNovelID)
    {
        StopStopwatch();
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            {"fromWhereIsNovelSelected", fromWhereIsNovelSelected},
            {"novelID", visualNovelID},
            {"millisecondsInNovelExplorer", stopwatch.ElapsedMilliseconds}
        };
        AnalyticsService.Instance.CustomData("novelExplorerStatistics", parameters);
        UnityEngine.Debug.Log("fromWhereIsNovelSelected: " + fromWhereIsNovelSelected + "\n" + "novelID: " + visualNovelID);
    }

    public void SendDetailViewStatistics(long visualNovelID)
    {
        StopStopwatch();
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            {"fromWhereIsNovelSelected", fromWhereIsNovelSelected},
            {"novelID", visualNovelID},
            {"millisecondsInDetailView", stopwatch.ElapsedMilliseconds}
        };
        AnalyticsService.Instance.CustomData("detailViewStatistics", parameters);
        UnityEngine.Debug.Log("fromWhereIsNovelSelected: " + fromWhereIsNovelSelected + "\n" + "novelID: " + visualNovelID);
    }

    public void SendPlayNovelFirstConfirmation()
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            {"millisecondsBeforePlayNovelFirstConfirmation", stopwatch.ElapsedMilliseconds}
        };
        AnalyticsService.Instance.CustomData("playNovelFirstConfirmation", parameters);
        UnityEngine.Debug.Log("millisecondsBeforePlayNovelFirstConfirmation: " + stopwatch.ElapsedMilliseconds);
        //TODO: Add position of confirmation. Maybe user thinks he/she has to click on the symbol...
    }

    public void SendPlayerChoice(int id)
    {
        var text = GetTextByChoiceId(id);
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            {"novelId", idOfCurrentNovel},
            {"question", lastQuestionForChoice},
            {"text", text},
            {"indexOfChoice", id},
            {"lengthOfChoice", text.Length}
        };
        choiceList.Clear();
        AnalyticsService.Instance.CustomData("playerChoice", parameters);
        UnityEngine.Debug.Log("idOfCurrentNovel: " + idOfCurrentNovel + "\n" +
        "question: " + lastQuestionForChoice + "\n" + 
        "text: " + text + "\n" + 
        "indexOfChoice: " + id + "\n" +
        "lengthOfChoice: " + text.Length);
    }
}