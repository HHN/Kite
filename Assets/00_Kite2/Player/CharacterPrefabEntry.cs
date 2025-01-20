namespace _00_Kite2.Player
{
    using System;
    using UnityEngine;

    [Serializable]
    public class CharacterPrefabEntry
    {
        public string novelId;              // Der eindeutige Schlüssel (z. B. "Bank")
        public GameObject[] characterPrefabs;  // Das zugehörige Prefab
    }
}