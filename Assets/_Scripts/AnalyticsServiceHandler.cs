using System.Collections.Generic;
using System.Diagnostics;
using Assets._Scripts.Managers;
using Unity.Services.Analytics;
using Unity.Services.Core;

namespace Assets._Scripts
{
    public class AnalyticsServiceHandler
    {
        private static AnalyticsServiceHandler _instance;

        private Stopwatch _stopwatch;

        private Stopwatch _stopwatchSession;

        private string _fromWhereIsNovelSelected = "";

        private readonly List<string> _choiceList = new List<string>();

        private readonly List<string> _feelingList = new List<string>();

        private string _lastQuestionForChoice;

        private long _idOfCurrentNovel;

        private bool _addedAllEntriesToChoiceList;

        private int _choiceId = -10;

        private bool _waitForAIFeedback;

        private bool _hasBeenInitialized;

        // Private Konstruktor, um direkte Instanziierung zu verhindern
        private AnalyticsServiceHandler()
        {
            _hasBeenInitialized = false;
        }


        public static AnalyticsServiceHandler Instance()
        {
            if (_instance == null)
            {
                _instance = new AnalyticsServiceHandler();
            }

            return _instance;
        }

        public async void StartAnalytics()
        {
            await UnityServices.InitializeAsync();
            StartStopwatch();
            _stopwatchSession = new Stopwatch();
            _stopwatchSession.Start();
            _hasBeenInitialized = true;
        }

        public bool IsAnalyticsInitialized()
        {
            return _hasBeenInitialized;
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
            _hasBeenInitialized = false;
            AnalyticsService.Instance.RequestDataDeletion();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                _stopwatchSession.Stop();
            }
            else
            {
                _stopwatchSession.Start();
            }
        }

        private void OnApplicationQuit()
        {
            SendSessionStatistics();
        }

        public void StartStopwatch()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        private void StopStopwatch()
        {
            if (_stopwatch != null)
            {
                _stopwatch.Stop();
            }
        }

        public void SetFromWhereIsNovelSelected(string fromWhereIsNovelSelected)
        {
            if (string.IsNullOrEmpty(fromWhereIsNovelSelected))
            {
                return;
            }

            _fromWhereIsNovelSelected = fromWhereIsNovelSelected;
        }

        public void AddChoiceToList(string choice)
        {
            if (string.IsNullOrEmpty(choice))
            {
                return;
            }

            _choiceList.Add(choice);
        }

        public void AddedLastChoice()
        {
            _addedAllEntriesToChoiceList = true;
            if (_choiceId >= 0)
            {
                SendPlayerChoice(_choiceId);
            }
        }

        public void SetChoiceId(int choiceId)
        {
            if (_addedAllEntriesToChoiceList)
            {
                SendPlayerChoice(choiceId);
            }
            else
            {
                _choiceId = choiceId;
            }
        }

        public void AddFeelingToList(string feeling)
        {
            if (string.IsNullOrEmpty(feeling))
            {
                return;
            }

            _feelingList.Add(feeling);
        }

        private int GetChoiceIdByText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return -1;
            }

            return _choiceList.IndexOf(text);
        }

        private string GetTextByChoiceId(int id)
        {
            return _choiceList[id];
        }

        private string GetTextByFeelingId(int id)
        {
            return _feelingList[id];
        }

        public void SetLastQuestionForChoice(string question)
        {
            if (string.IsNullOrEmpty(question))
            {
                return;
            }

            _lastQuestionForChoice = question;
        }

        public void SetIdOfCurrentNovel(long id)
        {
            _idOfCurrentNovel = id;
        }

        private string GetSelectableFeelings()
        {
            return string.Join(",", _feelingList);
        }

        public void SetWaitedForAiFeedbackTrue()
        {
            _waitForAIFeedback = true;
        }

        public void SendMainMenuStatistics()
        {
            if (ApplicationModeManager.Instance().IsOfflineModeActive())
            {
                return;
            }

            if (_hasBeenInitialized)
            {
                StopStopwatch();
                if (_stopwatch != null)
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        { "currentUserID", AnalyticsService.Instance.GetAnalyticsUserID() },
                        { "millisecondsInMainMenu", _stopwatch.ElapsedMilliseconds }
                    };
                    // AnalyticsService.Instance.CustomData("mainMenuStatistics", parameters);
                }
            }
        }

        public void SendNovelExplorerStatistics(long visualNovelID)
        {
            if (ApplicationModeManager.Instance().IsOfflineModeActive())
            {
                return;
            }

            if (_hasBeenInitialized)
            {
                StopStopwatch();
                if (_stopwatch != null)
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        { "millisecondsInNovelExplorer", _stopwatch.ElapsedMilliseconds }
                    };
                    // AnalyticsService.Instance.CustomData("novelExplorerStatistics", parameters);
                }
            }
        }

        public void SendDetailViewStatistics(long visualNovelID)
        {
            if (ApplicationModeManager.Instance().IsOfflineModeActive())
            {
                return;
            }

            if (_hasBeenInitialized)
            {
                StopStopwatch();
                if (_stopwatch != null)
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        { "fromWhereIsNovelSelected", _fromWhereIsNovelSelected },
                        { "novelID", visualNovelID },
                        { "millisecondsInDetailView", _stopwatch.ElapsedMilliseconds }
                    };
                    // AnalyticsService.Instance.CustomData("detailViewStatistics", parameters);
                }
            }
        }

        public void SendPlayNovelFirstConfirmation()
        {
            if (ApplicationModeManager.Instance().IsOfflineModeActive())
            {
                return;
            }

            if (_hasBeenInitialized)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    { "millisecondsBeforePlayNovelFirstConfirmation", _stopwatch.ElapsedMilliseconds }
                };
                // AnalyticsService.Instance.CustomData("playNovelFirstConfirmation", parameters);
                //TODO: Add position of confirmation. Maybe user thinks he/she has to click on the symbol...    
            }
        }

        private void SendPlayerChoice(int id)
        {
            if (ApplicationModeManager.Instance().IsOfflineModeActive())
            {
                return;
            }

            if (_hasBeenInitialized)
            {
                var text = GetTextByChoiceId(id);
                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    { "novelID", _idOfCurrentNovel },
                    { "question", _lastQuestionForChoice },
                    { "text", text },
                    { "indexOfChoice", id },
                    { "lengthOfChoice", text.Length }
                };
                // AnalyticsService.Instance.CustomData("playerChoice", parameters);
                _choiceId = -10; // means no choice selected
                _addedAllEntriesToChoiceList = false;
                _choiceList.Clear();
            }
        }

        public void SendPlayerFeeling(int id)
        {
            if (ApplicationModeManager.Instance().IsOfflineModeActive())
            {
                return;
            }

            if (_hasBeenInitialized)
            {
                string text;
                if (_feelingList.Count <= id)
                {
                    text = "Skip";
                }
                else
                {
                    text = GetTextByFeelingId(id);
                }

                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    { "novelID", _idOfCurrentNovel },
                    { "text", text },
                    { "indexOfFeeling", id },
                    { "chooseableFeelings", GetSelectableFeelings() }
                };
                // AnalyticsService.Instance.CustomData("playerFeeling", parameters);
                _feelingList.Clear();
            }
        }

        public void SendNovelPlayTime()
        {
            if (ApplicationModeManager.Instance().IsOfflineModeActive())
            {
                return;
            }

            if (_hasBeenInitialized)
            {
                StopStopwatch();
                if (_stopwatch != null)
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        { "playTime", _stopwatch.ElapsedMilliseconds }
                    };
                    // AnalyticsService.Instance.CustomData("novelPlayTime", parameters);
                }
            }
        }

        public void SendWaitedForAIFeedback()
        {
            if (ApplicationModeManager.Instance().IsOfflineModeActive())
            {
                return;
            }

            if (_hasBeenInitialized)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    { "waitForAIFeedback", _waitForAIFeedback }
                };
                // AnalyticsService.Instance.CustomData("waitForAIFeedback", parameters);
                _waitForAIFeedback = false;
            }
        }

        private void SendSessionStatistics()
        {
            if (ApplicationModeManager.Instance().IsOfflineModeActive())
            {
                return;
            }

            if (_hasBeenInitialized)
            {
                _stopwatchSession.Stop();
                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    { "sessionTime", _stopwatchSession.ElapsedMilliseconds }
                };
                // AnalyticsService.Instance.CustomData("sessionStatistics", parameters);
            }
        }
    }
}