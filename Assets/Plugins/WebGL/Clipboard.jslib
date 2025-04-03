mergeInto(LibraryManager.library, {
    CopyTextToClipboard: function(textPtr) {
        // Den Text aus dem übergebenen Zeiger lesen
        var text = UTF8ToString(textPtr);
        if (navigator.clipboard) {
            navigator.clipboard.writeText(text)
                .then(function() { console.log('Text wurde in die Zwischenablage kopiert.') })
                .catch(function(err) { console.error('Kopieren fehlgeschlagen:', err); });
        } else {
            // Fallback: Ein Textfeld erstellen, Text selektieren und kopieren
            var textArea = document.createElement("textarea");
            textArea.value = text;
            document.body.appendChild(textArea);
            textArea.select();
            document.execCommand('copy');
            document.body.removeChild(textArea);
        }
    }
});
