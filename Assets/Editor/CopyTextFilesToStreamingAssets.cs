using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class CopyTextFilesToStreamingAssets : AssetPostprocessor
    {
        // Wird immer dann aufgerufen, wenn ein Build erstellt wird.
        static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            Debug.Log("OnPostprocessBuild called for target: " + target);
            if (target == BuildTarget.WebGL || target == BuildTarget.StandaloneWindows || target == BuildTarget.StandaloneLinux || target == BuildTarget.StandaloneOSX)
            {
                string sourceDirectory = "Assets/_Scripts/_Mappings"; // Quellordner der .txt-Dateien
                string destinationDirectory = Path.Combine(Application.streamingAssetsPath); // Zielordner (StreamingAssets)

                // �berpr�fe, ob der Zielordner existiert, und erstelle ihn, falls n�tig
                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }

                // Kopiere die .txt-Dateien aus dem Quellordner in den StreamingAssets-Ordner
                string[] txtFiles = Directory.GetFiles(sourceDirectory, "*.txt");
                foreach (string file in txtFiles)
                {
                    string fileName = Path.GetFileName(file); // Nur den Dateinamen holen
                    string destinationFile = Path.Combine(destinationDirectory, fileName);

                    // Kopiere die Datei, falls sie noch nicht vorhanden ist oder �berschreibe sie
                    File.Copy(file, destinationFile, true);
                    Debug.Log("Copied " + fileName + " to " + destinationFile);
                }
            }
        }
    }
}
