using System;
using System.Collections.Generic;
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
    }
}