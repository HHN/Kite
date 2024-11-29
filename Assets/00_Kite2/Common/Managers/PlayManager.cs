using UnityEngine;

namespace _00_Kite2.Common.Managers
{
    public class PlayManager
    {
        private static PlayManager _instance;
        private VisualNovel _novelToPlay;
        private Color _backgroundColorForNovel;
        private Color _foregroundColorForNovel;
        private string _displayName;

        private PlayManager()
        {
        }

        public static PlayManager Instance()
        {
            if (_instance == null)
            {
                _instance = new PlayManager();
            }

            return _instance;
        }

        public void SetVisualNovelToPlay(VisualNovel novelToPlay)
        {
            this._novelToPlay = novelToPlay;
        }

        public VisualNovel GetVisualNovelToPlay()
        {
            if (_novelToPlay == null)
            {
                return null;
            }

            return _novelToPlay;
        }

        public void SetBackgroundColorOfVisualNovelToPlay(Color colorOfNovel)
        {
            this._backgroundColorForNovel = colorOfNovel;
        }

        public Color GetBackgroundColorOfVisualNovelToPlay()
        {
            if (_backgroundColorForNovel == null)
            {
                return new Color(0, 0, 0);
            }

            return _backgroundColorForNovel;
        }

        public void SetForegroundColorOfVisualNovelToPlay(Color colorOfNovel)
        {
            this._foregroundColorForNovel = colorOfNovel;
        }

        public Color GetForegroundColorOfVisualNovelToPlay()
        {
            if (_foregroundColorForNovel == null)
            {
                return new Color(0, 0, 0);
            }

            return _foregroundColorForNovel;
        }

        public void SetDisplayNameOfNovelToPlay(string v)
        {
            this._displayName = v;
        }

        public string GetDisplayNameOfNovelToPlay()
        {
            return this._displayName;
        }
    }
}