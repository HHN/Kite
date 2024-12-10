using System;
using System.Collections.Generic;
using UnityEngine;

namespace _00_Kite2.Test
{
    public class Tester : MonoBehaviour
    {
        // Pfad zur Twee-Datei (relativ oder absolut)
        [SerializeField] private string filePath = "Assets/YourTweeFile.twee";

        private void Start()
        {
            TweePathCalculator calculator = new TweePathCalculator();

            // try
            // {
                // Dateiinhalt lesen und parsen
                string tweeContent = calculator.ReadTweeFile(filePath);
                calculator.ParseTweeFile(tweeContent);
                calculator.PrintGraph();

                // Alle Pfade ab "Anfang" berechnen
                List<List<string>> allPaths = calculator.GetAllPaths("Anfang");

                // Pfade ausgeben
                calculator.PrintPaths(allPaths);
            
                Debug.Log("Fertig");
            // }
            // catch (Exception ex)
            // {
            //     Debug.Log($"Fehler: {ex.Message}");
            // }
        
            // // Beispiel in deinem Hauptskript
            // GameObject visualizerObject = new GameObject("GraphVisualizer");
            // GraphVisualizer visualizer = visualizerObject.AddComponent<GraphVisualizer>();
            // visualizer.SetGraph(calculator.Graph); // Übergib den Graphen
        }
    }
}
