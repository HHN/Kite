using System.Collections.Generic;
using _00_Kite2.Common.Novel;

namespace _00_Kite2.Common.Managers
{
    public class KiteNovelManager
    {
        private static KiteNovelManager _instance;
        private List<VisualNovel> _kiteNovels;

        private KiteNovelManager()
        {
            this._kiteNovels = new List<VisualNovel>();
        }

        public static KiteNovelManager Instance()
        {
            if (_instance == null)
            {
                _instance = new KiteNovelManager();
            }

            return _instance;
        }

        public List<VisualNovel> GetAllKiteNovels()
        {
            return _kiteNovels;
        }

        public void SetAllKiteNovels(List<VisualNovel> kiteNovels)
        {
            this._kiteNovels = kiteNovels;
        }

        public bool AreNovelsLoaded()
        {
            return (_kiteNovels != null && _kiteNovels.Count > 0);
        }
    }
}