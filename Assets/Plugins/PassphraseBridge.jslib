mergeInto(LibraryManager.library, {
  GetPassphraseFromBrowser: function () {
    // Greift auf das globale window-Objekt des Browsers zu
    var returnStr = window.KITE_CONFIG ? window.KITE_CONFIG.passphrase : "";
    
    // Speicher im Unity-System reservieren und String kopieren
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  }
});