namespace Assets._Scripts.ServerCommunication
{
    /// <summary>
    /// Einfacher In-Memory Token-Store. Kein PlayerPrefs, keine Persistenz.
    /// </summary>
    public static class TokenManager
    {
        private static string _token;

        public static bool HasToken()
        {
            return !string.IsNullOrEmpty(_token);
        }

        public static string Token => _token;

        public static void SetToken(string token)
        {
            _token = token;
        }

        public static void ClearToken()
        {
            _token = null;
        }
    }
}