using System;

namespace _00_Kite2.Common
{
    [Serializable]
    public class TextToSpeechWrapper
    {
        public bool activatedTextToSpeech;
        // public int speakerId; add this if functionality is desired
        // public int mode; add this if functionality is desired. Can be used to toggle where text to speech is activated. For example: "everywhere", "only in menu" or "only in novel".
    }
}