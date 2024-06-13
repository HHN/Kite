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
        StartStopwatch();
        stopwatchSession = new Stopwatch();
        stopwatchSession.Start();
        hasBeenInitialized = true;
    }

    public bool IsAnalyticsInitialized()
    {
        return hasBeenInitialized;
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
        hasBeenInitialized = false;
        AnalyticsService.Instance.RequestDataDeletion();
    }

    private void OnApplicationPause(bool pauseStatus) {
        if(pauseStatus)
        {
            stopwatchSession.Stop();
        } else 
        {
            stopwatchSession.Start();
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
        if (ApplicationModeManager.Instance().IsOfflineModeActive())
        {
            return;
        }
        if (hasBeenInitialized)
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
            }    
        }
        
    }

    public void SendNovelExplorerStatistics(long visualNovelID)
    {
        if (ApplicationModeManager.Instance().IsOfflineModeActive())
        {
            return;
        }
        if (hasBeenInitialized)
        {
            StopStopwatch();
            if(stopwatch != null)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    {"millisecondsInNovelExplorer", stopwatch.ElapsedMilliseconds}
                };
                AnalyticsService.Instance.CustomData("novelExplorerStatistics", parameters);
            }    
        }
        
    }

    public void SendDetailViewStatistics(long visualNovelID)
    {
        if (ApplicationModeManager.Instance().IsOfflineModeActive())
        {
            return;
        }
        if (hasBeenInitialized)
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
            }    
        }
    }

    public void SendPlayNovelFirstConfirmation()
    {
        if (ApplicationModeManager.Instance().IsOfflineModeActive())
        {
            return;
        }
        if (hasBeenInitialized)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"millisecondsBeforePlayNovelFirstConfirmation", stopwatch.ElapsedMilliseconds}
            };
            AnalyticsService.Instance.CustomData("playNovelFirstConfirmation", parameters);
            //TODO: Add position of confirmation. Maybe user thinks he/she has to click on the symbol...    
        } 
    }

    public void SendPlayerChoice(int id)
    {
        if (ApplicationModeManager.Instance().IsOfflineModeActive())
        {
            return;
        }
        if (hasBeenInitialized)
        {
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
            choiceId = -10;  // means no choice selected
            addedAllEntriesToChoiceList = false;
            choiceList.Clear();    
        } 
    }

    public void SendPlayerFeeling(int id)
    {
        if (ApplicationModeManager.Instance().IsOfflineModeActive())
        {
            return;
        }
        if (hasBeenInitialized)
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
            feelingList.Clear();    
        }
    }

    public void SendNovelPlayTime()
    {
        if (ApplicationModeManager.Instance().IsOfflineModeActive())
        {
            return;
        }
        if (hasBeenInitialized)
        {
            StopStopwatch();
            if(stopwatch != null)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    {"playTime", stopwatch.ElapsedMilliseconds}
                };
                AnalyticsService.Instance.CustomData("novelPlayTime", parameters);
            }    
        }
    }

    public void SendWaitedForAIFeedback()
    {
        if (ApplicationModeManager.Instance().IsOfflineModeActive())
        {
            return;
        }
        if (hasBeenInitialized)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"waitForAIFeedback", waitForAIFeedback}
            };
            AnalyticsService.Instance.CustomData("waitForAIFeedback", parameters);
            waitForAIFeedback = false;    
        }
    }

    private void SendSessionStatistics ()
    {
        if (ApplicationModeManager.Instance().IsOfflineModeActive())
        {
            return;
        }
        if(hasBeenInitialized)
        {
            stopwatchSession.Stop();
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"sessionTime", stopwatchSession.ElapsedMilliseconds}
            };
            AnalyticsService.Instance.CustomData("sessionStatistics", parameters);
        }
    }
}