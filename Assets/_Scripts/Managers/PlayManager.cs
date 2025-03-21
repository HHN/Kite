using Assets._Scripts.Novel;
using UnityEngine;

namespace Assets._Scripts.Managers
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
            _novelToPlay = novelToPlay;
        }

        public VisualNovel GetVisualNovelToPlay()
        {
            return _novelToPlay;
        }

        public void SetColorOfVisualNovelToPlay(Color colorOfNovel)
        {
            _backgroundColorForNovel = colorOfNovel;
        }

        public Color GetColorOfVisualNovelToPlay()
        {
            return _backgroundColorForNovel;
        }

        public void SetDisplayNameOfNovelToPlay(string v)
        {
            _displayName = v;
        }

        public string GetDisplayNameOfNovelToPlay()
        {
            return _displayName;
        }
    }
}