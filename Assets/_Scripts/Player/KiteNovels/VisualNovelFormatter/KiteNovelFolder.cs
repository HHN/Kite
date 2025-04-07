using System;
using System.Collections.Generic;
using Assets._Scripts.Novel;


namespace Assets._Scripts.Player.Kite_Novels.Visual_Novel_Formatter
{
    public class KiteNovelFolder
    {
        public KiteNovelFolder(KiteNovelMetaData novelMetaData, List<VisualNovelEvent> novelEventList)
        {
            this.NovelMetaData = novelMetaData;
            this.NovelEventList = novelEventList;
        }

        public KiteNovelFolder()
        {
            this.NovelMetaData = null;
            this.NovelEventList = null;
        }

        public KiteNovelMetaData NovelMetaData { get; set; }

        public List<VisualNovelEvent> NovelEventList { get; set; }
    }
}