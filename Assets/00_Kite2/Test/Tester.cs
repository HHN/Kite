using System;
using System.Collections.Generic;
using UnityEngine;

namespace _00_Kite2.Test
{
    public class Tester : MonoBehaviour
    {
        // Pfad zur Twee-Datei (relativ oder absolut)
        [SerializeField] private string filePathNovel = "Assets/YourNovelFile.txt";
        [SerializeField] private string filePathMeta = "Assets/YourMetaFile.txt";

        private void Start()
        {
            TweePathCalculator calculator = new TweePathCalculator();

            try
            {
                string metaContent = calculator.ReadTweeFile(filePathMeta);
                string novelContent = calculator.ReadTweeFile(filePathNovel);

                calculator.ParseMetaTweeFile(metaContent);
                calculator.ParseTweeFile(novelContent);

                // Alle Pfade ab "Anfang" berechnen
                List<List<string>> allPaths = calculator.GetAllPaths("Anfang");

                // Pfade als Gespräch und reinen Pfad ausgeben
                calculator.PrintPathsAsConversationAndPath(allPaths);

                Debug.Log("Fertig");
            }
            catch (Exception ex)
            {
                Debug.Log($"Fehler: {ex.Message}");
            }
        }
    }
}
