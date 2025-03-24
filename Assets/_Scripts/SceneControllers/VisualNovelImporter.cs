using System.IO;
using Assets._Scripts.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets._Scripts.SceneControllers
{
    public class VisualNovelImporter : MonoBehaviour
    {
        private string _eventListFilePath;
        private long _id;
        private string _titleOfNovel;
        private string _descriptionOfNovel; // große textfelder möglich?
        private string _nameOfMainCharacter;
        private string _contextForPrompt; // große textfelder möglich?
        private FaceExpressions _startTalkingPartnerEmotion;

        private int _numberOfTalkingPartners = 1;  // Standardmäßig 1
        private string[] _talkingPartners = new string[1];  // Mindestens 1 Element
        
        private string _fileContent = "Noch keine Datei geladen";

        public string GetEventListPath() => _eventListFilePath;
        public string GetTitleOfNovel() => _titleOfNovel;
        public string GetDescriptionOfNovel() => _descriptionOfNovel;
        public string GetNameOfMainCharacter() => _nameOfMainCharacter;
        public string GetContextForPrompt() => _contextForPrompt;
        public FaceExpressions GetStartTalkingPartnerEmotion() => _startTalkingPartnerEmotion;
        public int GetNumberOfTalkingPartners() => _numberOfTalkingPartners;
        public string[] GetTalkingPartners() => _talkingPartners;

        public void SetEventListFilePath(string path) => _eventListFilePath = path;
        public void SetTitleOfNovel(string title) => _titleOfNovel = title;
        public void SetDescriptionOfNovel(string description) => _descriptionOfNovel = description;
        public void SetNameOfMainCharacter(string characterName) => _nameOfMainCharacter = characterName;
        public void SetContextForPrompt(string context) => _contextForPrompt = context;
        public void SetStartTalkingPartnerEmotion(FaceExpressions expression) => _startTalkingPartnerEmotion = expression;
        public void SetTalkingPartners(string[] newTalkingPartners) => _talkingPartners = newTalkingPartners;

        public void SetNumberOfTalkingPartners(int newNumber)
        {
            _numberOfTalkingPartners = Mathf.Clamp(newNumber, 1, 3);
            System.Array.Resize(ref _talkingPartners, _numberOfTalkingPartners);
        }
        
        // "idNumberOfNovel": wird dynamisch vergeben
        
        public void LoadFile()
        {
            if (!string.IsNullOrEmpty(_eventListFilePath) && File.Exists(_eventListFilePath))
            {
                _fileContent = File.ReadAllText(_eventListFilePath);
                Debug.Log("Datei geladen: " + _eventListFilePath);
            }
            else
            {
                Debug.LogError("Ungültiger Dateipfad: " + _eventListFilePath);
            }
        }

        public string GetFileContent()
        {
            return _fileContent;
        }
    }
}
