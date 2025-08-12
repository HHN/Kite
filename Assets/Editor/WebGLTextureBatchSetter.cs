using UnityEditor;

namespace Editor
{
    public static class WebGLTextureBatchSetter
    {
        [MenuItem("Tools/Set WebGL Texture Overrides")]
        public static void ApplyWebGLOverrides()
        {
            // Alle Texture-Assets im Projekt finden
            string[] guids = AssetDatabase.FindAssets("t:Texture", new[] { "Assets" });
            int count = 0;

            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var importer = AssetImporter.GetAtPath(path) as TextureImporter;
                if (importer == null) 
                    continue;

                // WebGL-Override einschalten
                var settings = importer.GetPlatformTextureSettings("WebGL");
                settings.overridden = true;
                settings.format = TextureImporterFormat.ETC2_RGBA8;   // ETC2 8-Bit
                settings.maxTextureSize = 1024;                       // Max Size 1024
                settings.compressionQuality = 50;                     // optional: 0–100
                importer.SetPlatformTextureSettings(settings);

                // Crunch-Compression aktivieren
                importer.crunchedCompression = true;
                importer.compressionQuality = 50;

                // Änderungen speichern und Asset neu importieren
                importer.SaveAndReimport();
                count++;
            }

            EditorUtility.DisplayDialog(
                "WebGL Overrides gesetzt",
                $"Auf {count} Texturen in WebGL: ETC2_RGBA4, MaxSize=1024, Crunch aktiviert.",
                "OK"
            );
        }
    }
}