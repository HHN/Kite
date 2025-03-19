mergeInto(LibraryManager.library, {
    TTS_Initialize: function() {
        if (typeof speechSynthesis === 'undefined') {
            console.log("Web Speech API nicht verfügbar oder wird vom Browser nicht unterstützt.");
        } else {
            console.log("Web Speech API erkannt: TTS kann verwendet werden.");
        }
    },

    TTS_Speak: function(messagePtr) {
        if (typeof speechSynthesis === 'undefined') {
            console.log("Web Speech API nicht verfügbar. Kann nicht sprechen.");
            return;
        }
        var message = UTF8ToString(messagePtr);

        // Neues Utterance-Objekt erstellen
        window._ttsUtterance = new SpeechSynthesisUtterance(message);

        // Event, wenn das Sprechen beendet ist
        window._ttsUtterance.onend = function(event) {
            window._ttsIsSpeaking = false;
        };

        // Flag setzen und sprechen
        window._ttsIsSpeaking = true;
        speechSynthesis.speak(window._ttsUtterance);
    },

    TTS_Stop: function() {
        if (typeof speechSynthesis !== 'undefined') {
            speechSynthesis.cancel();
            window._ttsIsSpeaking = false;
        }
    },

    TTS_IsSpeaking: function() {
        if (typeof window._ttsIsSpeaking === 'undefined') {
            window._ttsIsSpeaking = false;
        }
        return window._ttsIsSpeaking ? 1 : 0;
    }
});
