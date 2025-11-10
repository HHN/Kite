// Assets/_Scripts/ServerCommunication/SceneMetrics/SceneType.cs
namespace Assets._Scripts.ServerCommunication.SceneMetrics
{
    public enum SceneType
    {
        StartScene,
        Einstiegsnovel,
        FoundersBubble,
        Feedback
    }

    public static class SceneTypeExtensions
    {
        public static string ToWireName(this SceneType t)
        {
            switch (t)
            {
                case SceneType.StartScene:       return "StartScene";
                case SceneType.Einstiegsnovel:   return "Einstiegsnovel";
                case SceneType.FoundersBubble:   return "FoundersBubble";
                case SceneType.Feedback:         return "Feedback";
                default:                         return t.ToString();
            }
        }
    }
}