using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    /// <summary>
    /// This script tests the keyword reading and extraction.
    /// You can specify the file path (relative to Application.dataPath or an absolute path) in the Inspector.
    /// The script reads the file, processes each line using the NovelKeywordParser, and outputs
    /// the extracted keyword properties (such as End, CharacterIndex, Action, FaceExpression, Sound, Bias)
    /// to the console.
    /// </summary>
    public class KeywordTester : MonoBehaviour
    {
        // Public field to specify the folder path in the Inspector.
        // Example: "Assets/_novels_twee/Eltern" if all files are located in this folder or its subfolders.
        public string folderPath = "Assets/_novels_twee/";

        /// <summary>
        /// Called when the script starts.
        /// Initiates the process of reading all files named "visual_novel_event_list.txt" from the given folder.
        /// </summary>
        public void Start()
        {
            // Start the coroutine that loads and processes keywords from all matching files in the folder.
            StartCoroutine(ProcessKeywordFolder());
        }

        /// <summary>
        /// Coroutine that searches for all files named "visual_novel_event_list.txt"
        /// in the specified folder (including subdirectories), processes each file to extract the keyword data,
        /// and outputs the total count of valid keywords found to the Unity console.
        /// </summary>
        private IEnumerator ProcessKeywordFolder()
        {
            // Build the full folder path by combining Application.dataPath with the folderPath provided.
            string fullFolderPath = Path.Combine(Application.dataPath, folderPath);

            // Check if the folder exists.
            if (!Directory.Exists(fullFolderPath))
            {
                Debug.LogError("Keyword folder not found at: " + fullFolderPath);
                yield break;
            }

            // Get all files with the name "visual_novel_event_list.txt" in the folder and its subdirectories.
            string[] filePaths = Directory.GetFiles(fullFolderPath, "visual_novel_event_list.txt", SearchOption.AllDirectories);
            Debug.Log("Number of files found: " + filePaths.Length);

            // Liste zum Sammeln aller validen Keyword-Modelle aus allen Dateien.
            List<NovelKeywordModel> allModels = new List<NovelKeywordModel>();

            // Gehe alle gefundenen Dateien durch.
            foreach (string file in filePaths)
            {
                Debug.Log("Processing file: " + file);

                // Lese den Inhalt der Datei synchron.
                List<string> fileContent = new List<string>();
                fileContent.AddRange(File.ReadAllLines(file));

                // Rufe den Parser auf, der den gesamten Dateiinhalt verarbeitet.
                List<NovelKeywordModel> models = NovelKeywordParser.ParseKeywordsFromFile(fileContent);

                if (models != null)
                {
                    allModels.AddRange(models);
                }

                // Optional: Warte einen Frame, um große Dateien nicht blockierend zu verarbeiten.
                yield return null;
            }

            // Gib die Gesamtzahl der gefundenen validen Keywords in der Konsole aus.
            Debug.Log("Total valid keywords found across all files: " + allModels.Count);

            yield return null;
        }
    }
}