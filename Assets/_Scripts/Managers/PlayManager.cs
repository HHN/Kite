using Assets._Scripts.Novel;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class PlayManager
    {
        private static PlayManager _instance;
        private VisualNovel _novelToPlay;
        private Color _colorForNovel;
        private Color _foregroundColorForNovel;
        private string _displayName;
        private string _designation;

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
            if (_novelToPlay == null)
            {
                return null;
            }

            return _novelToPlay;
        }

        public void SetColorOfVisualNovelToPlay(Color colorOfNovel)
        {
            _colorForNovel = colorOfNovel;
        }

        public Color GetColorOfVisualNovelToPlay()
        {
            return _colorForNovel;
        }

        public void SetDisplayNameOfNovelToPlay(string v)
        {
            _displayName = v;
        }

        public string GetDisplayNameOfNovelToPlay()
        {
            return _displayName;
        }
        
        public void SetDesignationOfNovelToPlay(string v)
        {
            _designation = v;
        }

        public string GetDesignationOfNovelToPlay()
        {
            return _designation;
        }
    }
}