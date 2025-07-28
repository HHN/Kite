using System.Collections.Generic;
using System.Diagnostics;
using Assets._Scripts.Managers;
using Unity.Services.Analytics;
using Unity.Services.Core;

namespace Assets._Scripts
{
    /// <summary>
    /// Handles the integration with Unity Gaming Services (UGS) Analytics.
    /// This class follows a Singleton pattern to ensure only one instance exists.
    /// It's responsible for initializing analytics, managing data collection consent,
    /// tracking user interactions, timings, and custom events throughout the application,
    /// especially related to novel selection, play, choices, and session length.
    /// </summary>
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

        /// <summary>
        /// Private constructor to enforce the Singleton pattern.
        /// Initializes the _hasBeenInitialized flag to false.
        /// </summary>
        private AnalyticsServiceHandler()
        {
            _hasBeenInitialized = false;
        }

        /// <summary>
        /// Provides the static instance of the AnalyticsServiceHandler.
        /// Creates a new instance if one doesn't already exist.
        /// Ensures the GameObject this script is on is marked as DontDestroyOnLoad.
        /// </summary>
        /// <returns>The singleton instance of AnalyticsServiceHandler.</returns>
        public static AnalyticsServiceHandler Instance()
        {
            if (_instance == null)
            {
                _instance = new AnalyticsServiceHandler();
            }

            return _instance;
        }

        /// <summary>
        /// Initializes Unity Gaming Services (UGS) asynchronously and starts session tracking.
        /// This should be called early in the application's lifecycle (e.g., in a main menu script).
        /// </summary>
        public async void StartAnalytics()
        {
            await UnityServices.InitializeAsync();
            StartStopwatch();
            _stopwatchSession = new Stopwatch();
            _stopwatchSession.Start();
            _hasBeenInitialized = true;
        }

        /// <summary>
        /// Checks if the Analytics Service Handler has been initialized.
        /// </summary>
        /// <returns>True if initialized, false otherwise.</returns>
        public bool IsAnalyticsInitialized()
        {
            return _hasBeenInitialized;
        }

        /// <summary>
        /// Resumes data collection for UGS Analytics.
        /// This typically happens after user consent is granted.
        /// </summary>
        public void CollectData()
        {
            AnalyticsService.Instance.StartDataCollection();
        }

        /// <summary>
        /// Halts data collection for UGS Analytics.
        /// This typically happens when a user revokes consent.
        /// </summary>
        public void DoNotCollectData()
        {
            AnalyticsService.Instance.StopDataCollection();
        }

        /// <summary>
        /// Requests the deletion of all collected user data from UGS Analytics.
        /// Resets the initialization status locally.
        /// </summary>
        public void DeleteCollectedData()
        {
            _hasBeenInitialized = false;
            AnalyticsService.Instance.RequestDataDeletion();
        }

        /// <summary>
        /// MonoBehaviour lifecycle method called when the application is paused or resumed.
        /// Used to pause/resume the session stopwatch.
        /// </summary>
        /// <param name="pauseStatus">True if the application is paused, false if resumed.</param>
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

        /// <summary>
        /// MonoBehaviour lifecycle method called when the application is quitting.
        /// Used to send final session statistics.
        /// </summary>
        private void OnApplicationQuit()
        {
            SendSessionStatistics();
        }

        /// <summary>
        /// Starts or restarts the general-purpose stopwatch.
        /// </summary>
        public void StartStopwatch()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        /// <summary>
        /// Stops the general-purpose stopwatch if it's running.
        /// </summary>
        private void StopStopwatch()
        {
            if (_stopwatch != null)
            {
                _stopwatch.Stop();
            }
        }

        /// <summary>
        /// Sets the source from where a novel was selected (e.g., "Novel Explorer", "Favorites").
        /// This data is later used in analytics events.
        /// </summary>
        /// <param name="fromWhereIsNovelSelected">A string indicating the source of novel selection.</param>
        public void SetFromWhereIsNovelSelected(string fromWhereIsNovelSelected)
        {
            if (string.IsNullOrEmpty(fromWhereIsNovelSelected))
            {
                return;
            }

            _fromWhereIsNovelSelected = fromWhereIsNovelSelected;
        }

        /// <summary>
        /// Adds a player choice option to a list for later analytics.
        /// </summary>
        /// <param name="choice">The text of the choice option.</param>
        public void AddChoiceToList(string choice)
        {
            if (string.IsNullOrEmpty(choice))
            {
                return;
            }

            _choiceList.Add(choice);
        }

        /// <summary>
        /// Signals that all available choices for a player interaction have been added to the list.
        /// If a choice ID was set prematurely, it triggers the SendPlayerChoice event.
        /// </summary>
        public void AddedLastChoice()
        {
            _addedAllEntriesToChoiceList = true;
            if (_choiceId >= 0)
            {
                SendPlayerChoice(_choiceId);
            }
        }

        /// <summary>
        /// Sets the ID of the player's chosen option. If all choices were already added,
        /// it immediately sends the analytics event; otherwise, it stores the ID to be sent later.
        /// </summary>
        /// <param name="choiceId">The numerical ID of the chosen option.</param>
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

        /// <summary>
        /// Adds a player feeling option to a list for later analytics.
        /// </summary>
        /// <param name="feeling">The text of the feeling option.</param>
        public void AddFeelingToList(string feeling)
        {
            if (string.IsNullOrEmpty(feeling))
            {
                return;
            }

            _feelingList.Add(feeling);
        }

        /// <summary>
        /// Retrieves the index (ID) of a choice based on its text content.
        /// </summary>
        /// <param name="text">The text of the choice.</param>
        /// <returns>The index of the choice, or -1 if not found or text is empty/null.</returns>
        private int GetChoiceIdByText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return -1;
            }

            return _choiceList.IndexOf(text);
        }

        /// <summary>
        /// Retrieves the text content of a choice based on its index (ID).
        /// </summary>
        /// <param name="id">The index of the choice.</param>
        /// <returns>The text of the choice.</returns>
        private string GetTextByChoiceId(int id)
        {
            return _choiceList[id];
        }

        /// <summary>
        /// Retrieves the text content of a feeling based on its index (ID).
        /// </summary>
        /// <param name="id">The index of the feeling.</param>
        /// <returns>The text of the feeling.</returns>
        private string GetTextByFeelingId(int id)
        {
            return _feelingList[id];
        }

        /// <summary>
        /// Sets the last question posed to the player, which led to a choice.
        /// This is used in the analytics event for player choices.
        /// </summary>
        /// <param name="question">The text of the last question.</param>
        public void SetLastQuestionForChoice(string question)
        {
            if (string.IsNullOrEmpty(question))
            {
                return;
            }

            _lastQuestionForChoice = question;
        }

        /// <summary>
        /// Sets the ID of the novel currently being played.
        /// </summary>
        /// <param name="id">The ID of the current novel.</param>
        public void SetIdOfCurrentNovel(long id)
        {
            _idOfCurrentNovel = id;
        }

        /// <summary>
        /// Gets a comma-separated string of all selectable feelings.
        /// </summary>
        /// <returns>A string containing all selectable feelings, separated by commas.</returns>
        private string GetSelectableFeelings()
        {
            return string.Join(",", _feelingList);
        }

        /// <summary>
        /// Sets a flag to indicate that the user waited for AI feedback.
        /// </summary>
        public void SetWaitedForAiFeedbackTrue()
        {
            _waitForAIFeedback = true;
        }

        /// <summary>
        /// Sends analytics data for time spent in the Main Menu.
        /// </summary>
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

        /// <summary>
        /// Sends analytics data for time spent in the Novel Explorer.
        /// </summary>
        /// <param name="visualNovelID">The ID of the novel viewed in the explorer (if applicable).</param>
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

        /// <summary>
        /// Sends analytics data for time spent in the Novel Detail View.
        /// </summary>
        /// <param name="visualNovelID">The ID of the novel whose detail view was shown.</param>
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

        /// <summary>
        /// Sends an analytics event when the first confirmation to play a novel occurs.
        /// </summary>
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

        /// <summary>
        /// Sends analytics data for a player's choice within a novel.
        /// Clears the choice list and resets relevant flags after sending.
        /// </summary>
        /// <param name="id">The ID of the player's chosen option.</param>
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

        /// <summary>
        /// Sends analytics data for a player's selected feeling/feedback.
        /// Clears the feeling list after sending.
        /// </summary>
        /// <param name="id">The ID of the player's selected feeling.</param>
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

        /// <summary>
        /// Sends analytics data for the total play time of a novel.
        /// </summary>
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

        /// <summary>
        /// Sends an analytics event indicating whether the user waited for AI feedback.
        /// Resets the flag after sending.
        /// </summary>
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

        /// <summary>
        /// Sends analytics data for the total session duration when the application quits.
        /// </summary>
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