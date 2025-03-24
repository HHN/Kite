using Assets._Scripts.SceneControllers;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(VisualNovelImporter))]
    public class VisualNovelImporterEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            VisualNovelImporter importer = (VisualNovelImporter)target;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("📂 File-Import", EditorStyles.boldLabel);
            importer.SetEventListFilePath(EditorGUILayout.TextField("Event List File Path", importer.GetEventListPath()));

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("📖 Novel Informationen", EditorStyles.boldLabel);
            importer.SetTitleOfNovel(EditorGUILayout.TextField("Title Of Novel", importer.GetTitleOfNovel()));
            importer.SetDescriptionOfNovel(EditorGUILayout.TextField("Description Of Novel", importer.GetDescriptionOfNovel()));
            importer.SetContextForPrompt(EditorGUILayout.TextField("Context For Prompt", importer.GetContextForPrompt()));

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("💬 Dialog-Partner", EditorStyles.boldLabel);
            importer.SetNameOfMainCharacter(EditorGUILayout.TextField("Name Of Main Character", importer.GetNameOfMainCharacter()));
            importer.SetNumberOfTalkingPartners(EditorGUILayout.IntSlider("Number of Talking Partners", importer.GetNumberOfTalkingPartners(), 1, 3));

            DrawDefaultInspector();
            
            // VisualNovelImporter importer = (VisualNovelImporter)target;
            //
            // // Zeigt den aktuellen Dateipfad an
            // EditorGUILayout.LabelField("Event List File:", importer.eventListFilePath);
            //
            // // Button zum Auswählen der Datei
            // if (GUILayout.Button("Datei auswählen"))
            // {
            //     string path = EditorUtility.OpenFilePanel("Event Datei auswählen", "", "json,txt,csv,xml");
            //     if (!string.IsNullOrEmpty(path))
            //     {
            //         importer.eventListFilePath = path;
            //         EditorUtility.SetDirty(importer);
            //     }
            // }
            //
            // // Button zum Laden der Datei
            // if (GUILayout.Button("Datei laden"))
            // {
            //     importer.LoadFile();
            // }
            
            // // Standard Inspector anzeigen
            // DrawDefaultInspector();
            
            // Anzahl der Talking Partners auswählen (zwischen 1 und 3)
            int newNumber = EditorGUILayout.IntSlider("Anzahl der Talking Partners", importer.GetNumberOfTalkingPartners(), 1, 3);

            if (newNumber != importer.GetNumberOfTalkingPartners())
            {
                importer.SetNumberOfTalkingPartners(newNumber);
                EditorUtility.SetDirty(importer);
            }

            // Stelle sicher, dass mindestens 1 Element da ist
            if (importer.GetTalkingPartners().Length == 0)
            {
                importer.SetNumberOfTalkingPartners(1);
            }

            // Talking Partners Felder anzeigen und bearbeiten
            for (int i = 0; i < importer.GetTalkingPartners().Length; i++)
            {
                importer.GetTalkingPartners()[i] = EditorGUILayout.TextField($"Talking Partner {i + 1}", importer.GetTalkingPartners()[i]);
            }

            // Speichern der Änderungen
            if (GUI.changed)
            {
                importer.SetTalkingPartners(importer.GetTalkingPartners());
                EditorUtility.SetDirty(importer);
            }
        }
    }
}
