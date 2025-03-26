using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Assets._Scripts.NovelData;
using Assets._Scripts.Player.KiteNovels.VisualNovelFormatter;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets._Scripts.Utilities
{
    public class VisualNovelImporterWindow : EditorWindow
    {
        // // File Data
        [Tooltip("Name for json file")]
        private string _novelAlias;
        private string _eventListFilePath;
        // private string _fileContent = "No file loaded yet.";
        //
        // // Novel Data
        // private long _id;
        // private string _titleOfNovel;
        // private string _descriptionOfNovel; // große textfelder möglich?
        // private string _nameOfMainCharacter;
        // private string _contextForPrompt; // große textfelder möglich?
        //
        // // Dialog Partners
        // private FaceExpressions _startTalkingPartnerEmotion;
        private int _numberOfTalkingPartners = 1; // Standardmäßig 1
        // private string[] _talkingPartners = new string[1]; // Mindestens 1 Element
        private bool _isFinished = false;
        
        private static NovelMetaData _novelMetaData;

        [MenuItem("Tools/Novel Importer")]
        public static void ShowWindow()
        {
            GetWindow<VisualNovelImporterWindow>("Visual Novel Importer");
            
            _novelMetaData = new NovelMetaData();
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();
            GUILayout.Label("📂 File-Import", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField("Novel Alias", _novelAlias);
            EditorGUILayout.TextField("Event List File", _eventListFilePath);

            if (GUILayout.Button("Select File", GUILayout.Width(120)))
            {
                string path = EditorUtility.OpenFilePanel("Select Event File", "", "txt");
                if (!string.IsNullOrEmpty(path))
                {
                    _eventListFilePath = path;
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            GUILayout.Label("📖 Novel Information", EditorStyles.boldLabel);

            _novelMetaData.TitleOfNovel = EditorGUILayout.TextField("Title", _novelMetaData.TitleOfNovel);

            EditorGUILayout.LabelField("Description");
            _novelMetaData.DescriptionOfNovel = EditorGUILayout.TextField(_novelMetaData.DescriptionOfNovel, GUILayout.Height(50));

            EditorGUILayout.LabelField("Context For Prompt");
            _novelMetaData.ContextForPrompt = EditorGUILayout.TextArea(_novelMetaData.ContextForPrompt, GUILayout.Height(60));

            EditorGUILayout.Space();
            GUILayout.Label("💬 Dialog Partners", EditorStyles.boldLabel);

            _novelMetaData.NameOfMainCharacter = EditorGUILayout.TextField("Name Of Main Character", _novelMetaData.NameOfMainCharacter);
            _novelMetaData.StartTalkingPartnerEmotion = (FaceExpressions)EditorGUILayout.EnumPopup("Start Partner Emotion", FaceExpressions.NeutralRelaxed);

            _numberOfTalkingPartners = EditorGUILayout.IntSlider("Number of Partners", _numberOfTalkingPartners, 1, 3);
            Array.Resize(ref _novelMetaData.TalkingPartners, _numberOfTalkingPartners);

            // Talking Partners Felder anzeigen und bearbeiten
            for (int i = 0; i < _novelMetaData.TalkingPartners.Length; i++)
            {
                _novelMetaData.TalkingPartners[i] = EditorGUILayout.TextField($"Talking Partner {i + 1}", _novelMetaData.TalkingPartners[i]);
            }

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("📥 Import Novel", GUILayout.Height(30)))
            {
                ImportNovel();
            }

            if (GUILayout.Button("❌ Reset", GUILayout.Width(100), GUILayout.Height(30)))
            {
                ResetFields();
            }

            EditorGUILayout.EndHorizontal();
        }

        // "idNumberOfNovel": wird dynamisch vergeben

        private void ImportNovel()
        {
            if (!string.IsNullOrEmpty(_eventListFilePath) && File.Exists(_eventListFilePath))
            {
                Debug.Log("✅ File successfully loaded: " + _eventListFilePath);

                ProcessNovelAndSelectiveOverrideOldNovels(_eventListFilePath);
                SaveToJson();
            }
            else
            {
                Debug.LogError("❌ Invalid file path: " + _eventListFilePath);
                EditorUtility.DisplayDialog("Error", "Please select a valid file!", "OK");
            }
        }

        private void ResetFields()
        {
            _novelAlias = "";
            _eventListFilePath = "";
            _novelMetaData.TitleOfNovel = "";
            _novelMetaData.DescriptionOfNovel = "";
            _novelMetaData.ContextForPrompt = "";
            _novelMetaData.NameOfMainCharacter = "";
            _numberOfTalkingPartners = 1;
            _novelMetaData.TalkingPartners = new string[1];
        }

        private void ProcessNovelAndSelectiveOverrideOldNovels(string eventListFilePath)
        {
            List<KiteNovelFolder> allFolders = new List<KiteNovelFolder>();
            
            string jsonStringOfEventList = LoadFileContent(eventListFilePath);
            if (string.IsNullOrEmpty(jsonStringOfEventList))
            {
                Debug.LogWarning("Kite Novel Event List could not be loaded: " + eventListFilePath);
                // continue;
            }
            
            KiteNovelEventList kiteNovelEventList = KiteNovelConverter.ConvertTextDocumentIntoEventList(jsonStringOfEventList, _novelMetaData);
            
            allFolders.Add(new KiteNovelFolder(_novelMetaData, kiteNovelEventList));
            
            
            // List<VisualNovel> visualNovels = KiteNovelConverter.ConvertFilesToNovels(allFolders);
            //
            // List<VisualNovel> oldNovels = KiteNovelManager.Instance().GetAllKiteNovels();
            //
            // List<VisualNovel> modifiedListOfNovels = new List<VisualNovel>();
            //
            // foreach (VisualNovel visualNovel in oldNovels)
            // {
            //     VisualNovel novel = visualNovel;
            //
            //     foreach (VisualNovel newNovel in visualNovels)
            //     {
            //         if (newNovel.id == novel.id)
            //         {
            //             novel = newNovel;
            //             Debug.Log("Override Novel : " + newNovel.title);
            //             break;
            //         }
            //     }
            //
            //     modifiedListOfNovels.Add(novel);
            // }
            //
            // SaveToJson(new NovelListWrapper(modifiedListOfNovels));
        }
        
        private string LoadFileContent(string path)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return File.ReadAllText(path);
            }
        
            using (UnityWebRequest www = UnityWebRequest.Get(path))
            {
                www.SendWebRequest();
                if ((www.result == UnityWebRequest.Result.ConnectionError) || (www.result == UnityWebRequest.Result.ProtocolError))
                {
                    Debug.LogError($"Error loading file at {path}: {www.error}");
                    return null;
                }
        
                return www.downloadHandler.text;
            }
        }
        
        private void SaveToJson(/*NovelListWrapper novelListWrapper*/)
        {
            // Serialize the dictionary and save it in pretty format
            // string json = JsonUtility.ToJson(novelListWrapper, true);
            string json = JsonConvert.SerializeObject(_novelMetaData, Formatting.Indented);

            string fileName = _novelAlias.Replace(" ", "");

            string directoryPath = Application.persistentDataPath + "/Novels/";

            // Prüfen, ob das Verzeichnis existiert, falls nicht -> erstellen
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = directoryPath + fileName + ".json";

            File.WriteAllText(filePath, json, Encoding.UTF8);
            Debug.Log("Visual Novel has been successfully converted to JSON format and saved under the following path: " + filePath);
            _isFinished = true;
        }
    }
}