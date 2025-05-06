using System;
using System.Collections.Generic;
using Assets._Scripts.Novel;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    [Serializable]
    public class PlayRecordData
    {
        public Dictionary<VisualNovelNames, int> PlayCounts = new();
    }


    public class PlayRecordManager
    {
        private static PlayRecordManager _instance;
        private const string Key = "PlayRecordData";

        private PlayRecordData _data;

        private PlayRecordManager()
        {
        }

        public static PlayRecordManager Instance()
        {
            return _instance ??= new PlayRecordManager();
        }

        public void IncreasePlayCounterForNovel(VisualNovelNames playedNovel)
        {
            LoadDataIfNeeded();

            _data.PlayCounts.TryAdd(playedNovel, 0);

            _data.PlayCounts[playedNovel]++;
            Save();
        }
        
        public int GetNumberOfPlaysForNovel(VisualNovelNames novel)
        {
            LoadDataIfNeeded();
            return _data.PlayCounts.TryGetValue(novel, out int count) ? count : 0;
        }
        
        private void LoadDataIfNeeded()
        {
            if (_data != null) return;

            if (PlayerDataManager.Instance().HasKey(Key))
            {
                string json = PlayerDataManager.Instance().GetPlayerData(Key);
                _data = JsonUtility.FromJson<PlayRecordData>(json);
            }
            else
            {
                _data = new PlayRecordData();
            }
        }

        private void Save()
        {
            string json = JsonUtility.ToJson(_data);
            PlayerDataManager.Instance().SavePlayerData(Key, json);
        }

        public void ClearData()
        {
            _data = new PlayRecordData();
            Save();
        }
    }
}