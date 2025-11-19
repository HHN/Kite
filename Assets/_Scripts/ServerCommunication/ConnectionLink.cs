namespace Assets._Scripts.ServerCommunication
{
    /// <summary>
    /// Provides static constant strings for all server API endpoints used in the application.
    /// This centralizes the management of connection links, making it easier to configure
    /// and maintain server communication paths.
    /// </summary>
    public static class ConnectionLink
    {
        private static readonly string BASE_LINK = "https://kite2.site/"; // "https://localhost/"; //

        public static readonly string COMPLETION_LINK = BASE_LINK + "ai";
        public static readonly string NOVEL_REVIEW_LINK = BASE_LINK + "novelreview";
        public static readonly string AI_REVIEW_LINK = BASE_LINK + "aireview";
        public static readonly string OBSERVER_LINK = BASE_LINK + "reviewobserver";
        public static readonly string VERSION_LINK = BASE_LINK + "version";
        public static readonly string USER_ROLE_LINK = BASE_LINK + "role";
        public static readonly string DATA_LINK = BASE_LINK + "data";
        public static readonly string EXPERT_FEEDBACK_QUESTION = BASE_LINK + "expertfeedbackquestion";
        public static readonly string EXPERT_FEEDBACK_ANSWER = BASE_LINK + "expertfeedbackanswer";
        
        public static string METRICS_SCENES_BASE = BASE_LINK + "metrics/scenes";
        public static string SCENE_HIT(string sceneWire)   => $"{METRICS_SCENES_BASE}/{sceneWire}/hit";
        public static string SCENE_COUNT(string sceneWire) => $"{METRICS_SCENES_BASE}/{sceneWire}";
        public static string SCENES_ALL                    => METRICS_SCENES_BASE;
        
        public static readonly string PLAYTHROUGHS_BASE = BASE_LINK + "metrics/playthroughs";
        public static string PLAYTHROUGHS_HIT => PLAYTHROUGHS_BASE + "/hit";

    }
}