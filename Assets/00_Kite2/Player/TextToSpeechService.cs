using System.Threading;
using System.Threading.Tasks;
using _00_Kite2.Common;
using UnityEngine;

namespace _00_Kite2.Player
{
    public class TextToSpeechService
    {
        private static TextToSpeechService _instance;
        private string _choicesForTextToSpeech;
        private AudioSource _audioSource;
        private string _novelTitle;
        private int _optionCounter;
        private CancellationTokenSource _cancellationTokenSource;

        public static TextToSpeechService Instance()
        {
            if (_instance == null)
            {
                _instance = new TextToSpeechService();
            }

            return _instance;
        }

        public void SetAudioSource(AudioSource audioSource)
        {
            this._audioSource = audioSource;
        }

        public void SetNovelTitle(string novelTitle)
        {
            this._novelTitle = novelTitle;
        }

        public void TextToSpeech(string titleAndId, bool readAnyway = false)
        {
            if (TextToSpeechManager.Instance.IsTextToSpeechActivated() || readAnyway)
            {
                _audioSource.clip = Resources.Load<AudioClip>(ReturnNameOfAudioFileFromTitelAndId(titleAndId));
                if (_audioSource.clip != null)
                {
                    _audioSource.Play();
                }
                else
                {
                    Debug.LogError("AudioClip couldn't be found. Check the path.");
                }
            }

            _choicesForTextToSpeech = "";
        }

        private string ReturnNameOfAudioFileFromTitelAndId(string titleAndId)
        {
            string returnString =
                titleAndId.Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue").Replace("ß", "ss");
            return returnString;
        }

        public void AddChoiceToChoiceCollectionForTextToSpeech(string choice)
        {
            _optionCounter++;
            switch (_optionCounter)
            {
                case 1:
                    _choicesForTextToSpeech += "Erste Option: ";
                    break;
                case 2:
                    _choicesForTextToSpeech += "Zweite Option: ";
                    break;
                case 3:
                    _choicesForTextToSpeech += "Dritte Option: ";
                    break;
                case 4:
                    _choicesForTextToSpeech += "Vierte Option: ";
                    break;
                case 5:
                    _choicesForTextToSpeech += "Fünfte Option: ";
                    break;
            }

            _choicesForTextToSpeech += choice;
        }

        public string ReturnChoicesForTextToSpeech()
        {
            return _choicesForTextToSpeech;
        }

        public async Task TextToSpeechReadLive(string text, bool readAnyway = false)
        {
            //if (audioSource != null)
            //{
            //    if (TextToSpeechManager.Instance().IsTextToSpeechActivated() || readAnyway)
            //    {
            //        //Debug.Log(text);
            //        cancellationTokenSource = new CancellationTokenSource();
            //        try
            //        {
            //            AudioClip audioClip = await engine.Speak(text, TTSVoiceNative.LoadVoiceFromResources("de-de-thorsten-high"));
            //            if (audioSource != null && !cancellationTokenSource.IsCancellationRequested)
            //            {
            //                audioSource.clip = audioClip;
            //                audioSource.Play();
            //            }
            //        }
            //        catch (TaskCanceledException)
            //        {
            //            Debug.Log("Text-to-Speech operation was cancelled.");
            //        }
            //    }
            //}
        }

        public void CancelSpeechAndAudio()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }

            if (_audioSource != null && _audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
        }
    }
}