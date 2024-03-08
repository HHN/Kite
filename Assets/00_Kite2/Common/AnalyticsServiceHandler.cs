using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;
using System.Diagnostics;
using System.Collections.Generic;

public class AnalyticsServiceHandler 
{

    private static AnalyticsServiceHandler instance;

    private Stopwatch stopwatch = null;

    private Stopwatch stopwatchSession;

    private string fromWhereIsNovelSelected = "";

    private List<string> choiceList = new List<string>();

    private List<string> feelingList = new List<string>();

    private string lastQuestionForChoice;

    private long idOfCurrentNovel;

    private bool addedAllEntriesToChoiceList = false;

    private int choiceId = -10;

    private bool waitForAIFeedback = false;

    private bool hasBeenInitialized = false;

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
        // UnityEngine.Debug.Log("UserID: " + AnalyticsService.Instance.GetAnalyticsUserID());
        StartStopwatch();
        stopwatchSession = new Stopwatch();
        stopwatchSession.Start();
        hasBeenInitialized = true;
    }

    public void CollectData()
    {
        AnalyticsService.Instance.StartDataCollection();
    }

    public void DoNotCollectData()
    {
        AnalyticsService.Instance.StopDataCollection();
    }

    public void DeleteCollectedData()
    {
        AnalyticsService.Instance.RequestDataDeletion();
    }

    private void OnApplicationPause(bool pauseStatus) {
        if(pauseStatus)
        {
            stopwatchSession.Stop();
            // UnityEngine.Debug.Log("Stopped session time");
        } else 
        {
            stopwatchSession.Start();
            // UnityEngine.Debug.Log("Continued session time");
        }
    }

    private void OnApplicationQuit() 
    {
        SendSessionStatistics();
    }

    public void StartStopwatch()
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
    }

    public long StopStopwatch()
    {
        if(stopwatch != null)
        {
        stopwatch.Stop();
        // UnityEngine.Debug.Log("Zeit seit dem Start: " + stopwatch.ElapsedMilliseconds);
        return stopwatch.ElapsedMilliseconds; 
        }
        return -1;
    }

    public void SetFromWhereIsNovelSelected(string fromWhereIsNovelSelected)
    {
        if(fromWhereIsNovelSelected == null || fromWhereIsNovelSelected == "")
        {
            return;
        }
        this.fromWhereIsNovelSelected = fromWhereIsNovelSelected;
    }

    public void AddChoiceToList(string choice)
    {
        if(choice == null || choice == "")
        {
            return;
        }
        choiceList.Add(choice);
    }

    public void AddedLastChoice ()
    {
        addedAllEntriesToChoiceList = true;
        if(choiceId >= 0)
        {
            SendPlayerChoice(choiceId);
        }
    }

    public void SetChoiceId(int choiceId)
    {
        if(addedAllEntriesToChoiceList)
        {
            SendPlayerChoice(choiceId);
        } else {
            this.choiceId = choiceId;
        }
    }

    public void AddFeelingToList(string feeling)
    {
        if(feeling == null || feeling == "")
        {
            return;
        }
        feelingList.Add(feeling);
    }

    private int GetChoiceIdByText(string text)
    {
        if(text == null || text == "")
        {
            return -1;
        }
        return choiceList.IndexOf(text);
    }

    private string GetTextByChoiceId(int id)
    {
        return choiceList[id];
    }

    private string GetTextByFeelingId(int id)
    {
        return feelingList[id];
    }

    public void SetLastQuestionForChoice(string question)
    {
        if(question == null || question == "")
        {
            return;
        }
        lastQuestionForChoice = question;
    }

    public void SetIdOfCurrentNovel(long id)
    {
        idOfCurrentNovel = id;
    }

    private string GetChoosableFeelings()
    {
        return string.Join(",", feelingList);
    }

    public void SetWaitedForAiFeedbackTrue()
    {
        waitForAIFeedback = true;
    }

    public void SendMainMenuStatistics()
    {
        if(hasBeenInitialized)
        {
            StopStopwatch();
            if(stopwatch != null)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    {"currentUserID", AnalyticsService.Instance.GetAnalyticsUserID()},
                    {"millisecondsInMainMenu", stopwatch.ElapsedMilliseconds}
                };
                AnalyticsService.Instance.CustomData("mainMenuStatistics", parameters);
                // UnityEngine.Debug.Log("");
            }    
        }
        
    }

    public void SendNovelExplorerStatistics(long visualNovelID)
    {
        if(hasBeenInitialized)
        {
            StopStopwatch();
            if(stopwatch != null)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    // {"fromWhereIsNovelSelected", fromWhereIsNovelSelected},
                    // {"novelID", visualNovelID},
                    {"millisecondsInNovelExplorer", stopwatch.ElapsedMilliseconds}
                };
                AnalyticsService.Instance.CustomData("novelExplorerStatistics", parameters);
                // UnityEngine.Debug.Log("fromWhereIsNovelSelected: " + fromWhereIsNovelSelected + "\n" + "novelID: " + visualNovelID);
            }    
        }
        
    }

    public void SendDetailViewStatistics(long visualNovelID)
    {
        if(hasBeenInitialized)
        {
            StopStopwatch();
            if(stopwatch != null)
            {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    {"fromWhereIsNovelSelected", fromWhereIsNovelSelected},
                    {"novelID", visualNovelID},
                    {"millisecondsInDetailView", stopwatch.ElapsedMilliseconds}
                };
                AnalyticsService.Instance.CustomData("detailViewStatistics", parameters);
                // UnityEngine.Debug.Log("fromWhereIsNovelSelected: " + fromWhereIsNovelSelected + "\n" + "novelID: " + visualNovelID); 
            }    
        }
    }

    public void SendPlayNovelFirstConfirmation()
    {
        if(hasBeenInitialized)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"millisecondsBeforePlayNovelFirstConfirmation", stopwatch.ElapsedMilliseconds}
            };
            AnalyticsService.Instance.CustomData("playNovelFirstConfirmation", parameters);
            // UnityEngine.Debug.Log("millisecondsBeforePlayNovelFirstConfirmation: " + stopwatch.ElapsedMilliseconds);
            //TODO: Add position of confirmation. Maybe user thinks he/she has to click on the symbol...    
        } 
    }

    public void SendPlayerChoice(int id)
    {
        if(hasBeenInitialized)
        {
            // UnityEngine.Debug.Log("Length of choice: " + choiceList.Count);
            // UnityEngine.Debug.Log("ID: " + id);
            var text = GetTextByChoiceId(id);
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"novelID", idOfCurrentNovel},
                {"question", lastQuestionForChoice},
                {"text", text},
                {"indexOfChoice", id},
                {"lengthOfChoice", text.Length}
            };
            AnalyticsService.Instance.CustomData("playerChoice", parameters);
            // UnityEngine.Debug.Log("idOfCurrentNovel: " + idOfCurrentNovel + "\n" +
            // "question: " + lastQuestionForChoice + "\n" + 
            // "text: " + text + "\n" + 
            // "indexOfChoice: " + id + "\n" +
            // "lengthOfChoice: " + text.Length);
            choiceId = -10;  // means no choice selected
            addedAllEntriesToChoiceList = false;
            choiceList.Clear();    
        } 
    }

    public void SendPlayerFeeling(int id)
    {
        if(hasBeenInitialized)
        {
            var text = "";
            if(feelingList.Count <= id)
            {
                text = "Skip";
            } else {
                text = GetTextByFeelingId(id);
            }
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"novelID", idOfCurrentNovel},
                {"text", text},
                {"indexOfFeeling", id},
                {"chooseableFeelings", GetChoosableFeelings()}
            };
            AnalyticsService.Instance.CustomData("playerFeeling", parameters);
            //UnityEngine.Debug.Log("idOfCurrentNovel: " + idOfCurrentNovel + "\n" +
            // "text: " + text + "\n" + 
            // "indexOfFeeling: " + id + "\n" +
            // "chooseableFeelings: " + GetChoosableFeelings());
            feelingList.Clear();    
        }
    }

    public void SendNovelPlayTime()
    {
        if(hasBeenInitialized)
        {
            StopStopwatch();
            if(stopwatch != null)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    {"playTime", stopwatch.ElapsedMilliseconds}
                };
                AnalyticsService.Instance.CustomData("novelPlayTime", parameters);
                //UnityEngine.Debug.Log("Novel ended after: " + stopwatch.ElapsedMilliseconds);
            }    
        }
    }

    public void SendWaitedForAIFeedback()
    {
        if(hasBeenInitialized)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"waitForAIFeedback", waitForAIFeedback}
            };
            AnalyticsService.Instance.CustomData("waitForAIFeedback", parameters);
            UnityEngine.Debug.Log("Did player waited for ai feedback? Answer: " + waitForAIFeedback);
            waitForAIFeedback = false;    
        }
    }

    private void SendSessionStatistics ()
    {
        if(hasBeenInitialized)
        {
            stopwatchSession.Stop();
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"sessionTime", stopwatchSession.ElapsedMilliseconds}
            };
            AnalyticsService.Instance.CustomData("sessionStatistics", parameters);
            //UnityEngine.Debug.Log("Session ended after: " + stopwatchSession.ElapsedMilliseconds);    
        }
    }
}