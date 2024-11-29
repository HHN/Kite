namespace _00_Kite2.Common.Managers
{
    public class InfoTextManager
    {
        private string _textHead;
        private string _textBody;

        private static InfoTextManager _instance;

        private InfoTextManager()
        {
        }

        public static InfoTextManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InfoTextManager();
                }

                return _instance;
            }
        }

        public void SetTextHead(string textHead)
        {
            this._textHead = textHead;
        }

        public string GetTextHead()
        {
            return _textHead;
        }

        public void SetTextBody(string textBody)
        {
            this._textBody = textBody;
        }

        public string GetTextBody()
        {
            return _textBody;
        }
    }
}