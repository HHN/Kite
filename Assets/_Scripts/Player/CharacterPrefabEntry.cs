using System;
using UnityEngine;

namespace Assets._Scripts.Player
{
    [Serializable]
    public class CharacterPrefabEntry
    {
        public string novelId;              // Der eindeutige Schlüssel (z. B. "Bank")
        public GameObject[] characterPrefabs;  // Das zugehörige Prefab
    }
}