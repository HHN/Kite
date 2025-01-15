using System;
using System.Collections.Generic;
using _00_Kite2.Player;
using _00_Kite2.SaveNovelData;
using UnityEngine;
using UnityEngine.Serialization;

namespace _00_Kite2
{
    [Serializable]
    public class NovelSaveStatus
    {
        public string novelId;
        public bool isSaved;
    }
    
    [Serializable]
    public class CharacterData
    {
        public int skinIndex;
        public int handIndex;
        public int clotheIndex;
        public int hairIndex;
    }

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private bool skipIntroNovel;

        [SerializeField] private bool isIntroNovelSaved;
        [SerializeField] private bool introNovelLoadedFromMainMenu = true;

        // Liste zur Anzeige im Inspector (nur für Debugging, nicht direkt genutzt)
        [SerializeField] private List<NovelSaveStatus> novelSaveStatusList = new();

        public bool calledFromReload = true;

        // Dictionary zur dynamischen Verwaltung des Speicherstatus jeder Novel
        private readonly Dictionary<string, bool> _novelSaveStatus = new();
        public static GameManager Instance { get; private set; }
        
        public static List<CharacterData> CharacterDataList = new List<CharacterData>();

        public bool SkipIntroNovel
        {
            get => skipIntroNovel;
            set => skipIntroNovel = value;
        }

        public bool IsIntroNovelLoadedFromMainMenu
        {
            get => introNovelLoadedFromMainMenu;
            set => introNovelLoadedFromMainMenu = value;
        }
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject); // Bleibt beim Szenenwechsel bestehen

            // Überprüfe und setze den Speicherstatus f�r alle Novels beim Start
            CheckAndSetAllNovelsStatus();
        }

        /// <summary>
        /// Überprüft und setzt den Speicherstatus für alle Novels.
        /// </summary>
        public void CheckAndSetAllNovelsStatus()
        {
            _novelSaveStatus.Clear();
            novelSaveStatusList.Clear();

            foreach (VisualNovelNames novelName in Enum.GetValues(typeof(VisualNovelNames)))
            {
                string novelId = VisualNovelNamesHelper.ToInt(novelName).ToString();

                bool isSaved = SaveLoadManager.Load(novelId) != null;
                _novelSaveStatus[novelId] = isSaved;

                // Aktualisiert die Liste f�r den Inspector
                novelSaveStatusList.Add(new NovelSaveStatus { novelId = novelId, isSaved = isSaved });
            }
        }

        /// <summary>
        /// Überprüft, ob ein gespeicherter Fortschritt für eine bestimmte Novel vorhanden ist.
        /// </summary>
        /// <param name="novelId">Die eindeutige ID der Novel.</param>
        /// <returns>Gibt <c>true</c> zurück, wenn ein Speicherstand vorhanden ist; andernfalls <c>false</c>.</returns>
        public bool HasSavedProgress(string novelId)
        {
            // Hier wird geprüft, ob der Speicherstand f�r die Novel im Dictionary vorhanden ist und `true` ist
            return _novelSaveStatus.ContainsKey(novelId) && _novelSaveStatus[novelId];
        }

        /// <summary>
        /// Aktualisiert den Speicherstatus einer Novel, z.B. nach einem Speichervorgang.
        /// </summary>
        /// <param name="novelId">Die eindeutige ID der Novel.</param>
        /// <param name="isSaved">Der neue Speicherstatus der Novel.</param>
        public void UpdateNovelSaveStatus(string novelId, bool isSaved)
        {
            if (_novelSaveStatus.ContainsKey(novelId))
            {
                _novelSaveStatus[novelId] = isSaved;
            }
        }
    }
}