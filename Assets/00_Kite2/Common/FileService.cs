using System.IO;
using UnityEngine;

public class FileService : MonoBehaviour
{
    private static FileService instance;
    private string filePath = "./UserSaveFiles";

    public static FileService Instance()
    {
        if (instance == null)
        {
            instance = new FileService();
        }
        return instance;
    }

    void Start()
    {
        // Pfad zum Speichern der Datei festlegen
        // Application.persistentDataPath ist ein spezieller Pfad, der zum Speichern und Lesen von Dateien auf dem Gerät verwendet wird
        filePath = Path.Combine(Application.persistentDataPath, "UserInput.txt");
    }

    // Methode zum Speichern von Text in einer Datei
    public void WriteToFile(string inputType, string input)
    {
        // Überprüfen, ob die Datei existiert
        if (!File.Exists(filePath))
        {
            Debug.LogError("Die Datei existiert nicht: " + filePath);
            return;
        }
        lines = ReadFromFile();

        // Durch alle Zeilen iterieren
        for (int i = 0; i < lines.Length; i++)
        {
            // Überprüfen, ob die Zeile mit dem gesuchten Text beginnt
            if (lines[i].StartsWith(lineStartsWith))
            {
                // Regex verwenden, um den Text in den eckigen Klammern zu finden und zu ersetzen
                lines[i] = Regex.Replace(lines[i], @"\[.*?\]", $"[{newText}]");
                break; // Annahme, dass es nur eine Zeile gibt, die ersetzt werden muss
            }
        }

        // Die geänderten Zeilen in die Datei zurückschreiben
        File.WriteAllLines(filePath, lines);
    }

    // Methode zum Lesen des Textes aus der Datei
    public string[] ReadFromFile()
    {
        // Überprüft, ob die Datei existiert, und liest den Text, falls vorhanden
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            return lines;
        }
        else
        {
            Debug.LogError("Die Datei existiert nicht.");
            return "";
        }
    }
}
