using System;
using System.Collections.Generic;
using Assets._Scripts.Utilities;
using UnityEngine;

namespace Assets._Scripts.Novel.VisualNovelLoader
{
    /// <summary>
    /// A wrapper class that encapsulates a list of VisualNovel objects.
    /// This class is primarily used for serialization and deserialization
    /// of visual novel data when saving to or loading from a JSON file.
    /// </summary>
    [Serializable]
    public class NovelListWrapper
    {
        [SerializeField] private List<VisualNovel> visualNovels;

        /// <summary>
        /// A wrapper class that encapsulates a list of VisualNovel objects.
        /// This class is primarily used for serialization and deserialization
        /// of visual novel data when saving to or loading from a JSON file.
        /// </summary>
        public NovelListWrapper(List<VisualNovel> visualNovels)
        {
            this.visualNovels = visualNovels;
        }

        /// <summary>
        /// A wrapper class that encapsulates a list of VisualNovel objects.
        /// This class is primarily used for serialization and deserialization
        /// of visual novel data when saving to or loading from a JSON file.
        /// </summary>
        public NovelListWrapper()
        {
            visualNovels = new List<VisualNovel>();
        }

        /// <summary>
        /// Gets or sets the list of VisualNovel objects encapsulated in the wrapper.
        /// This property is used to access or modify the visual novel data,
        /// often during serialization and deserialization processes.
        /// </summary>
        public List<VisualNovel> VisualNovels
        {
            get => visualNovels;
            set => visualNovels = value;
        }
        
        /// <summary>
        /// Outputs all entries in the list to the console.
        /// </summary>
        public void DebugLogAllNovels()
        {
            if (visualNovels == null)
            {
                LogManager.Warning("NovelListWrapper.DebugLogAllNovels: visualNovels == null");
                return;
            }

            if (visualNovels.Count == 0)
            {
                return;
            }

            for (int i = 0; i < visualNovels.Count; i++)
            {
                VisualNovel vn = visualNovels[i];

                if (vn == null)
                {
                    LogManager.Warning($"  [{i}] Eintrag ist NULL.");
                    continue;
                }
            }
        }

    }
}