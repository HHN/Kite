using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class NovelJsonTools
{
    /// <summary>
    /// Entfernt alle Novels aus novels.json, deren Titel in der Title-Liste-Datei stehen.
    /// Die Struktur von novels.json bleibt ansonsten unverändert.
    /// </summary>
    /// <param name="titlesListPath">
    /// Pfad zu list_of_novels_to_remove_from_json.txt
    /// (JSON mit { "visualNovels": [ "Titel1", "Titel2", ... ] }).
    /// </param>
    /// <param name="novelsJsonPath">Pfad zur novels.json in StreamingAssets.</param>
    public static void RemoveNovelsByTitle(string titlesListPath, string novelsJsonPath)
    {
        if (!File.Exists(titlesListPath))
        {
            Debug.LogError($"[NovelJsonTools] Title list not found: {titlesListPath}");
            return;
        }

        if (!File.Exists(novelsJsonPath))
        {
            Debug.LogError($"[NovelJsonTools] novels.json not found: {novelsJsonPath}");
            return;
        }

        // 1) Titel-Liste mit Unity JsonUtility einlesen (wie bisher)
        string listJson = File.ReadAllText(titlesListPath, Encoding.UTF8);
        var titlesWrapper = JsonUtility.FromJson<VisualNovelTitlesToRemove>(listJson);

        if (titlesWrapper == null || titlesWrapper.visualNovels == null)
        {
            Debug.LogError("[NovelJsonTools] Could not parse titles list.");
            return;
        }

        var titlesToRemove = new HashSet<string>();
        foreach (var rawTitle in titlesWrapper.visualNovels)
        {
            if (string.IsNullOrWhiteSpace(rawTitle))
                continue;
            titlesToRemove.Add(rawTitle.Trim());
        }

        if (titlesToRemove.Count == 0)
        {
            Debug.LogWarning("[NovelJsonTools] Title list is empty, nothing to remove.");
            return;
        }

        Debug.Log("[NovelJsonTools] Removing from novels.json: " +
                  string.Join(", ", titlesToRemove));

        // 2) novels.json roh laden
        string novelsJsonRaw = File.ReadAllText(novelsJsonPath, Encoding.UTF8);

        // BOM entfernen
        if (!string.IsNullOrEmpty(novelsJsonRaw) && novelsJsonRaw[0] == '\uFEFF')
        {
            novelsJsonRaw = novelsJsonRaw.Substring(1);
        }

        JObject root;
        try
        {
            root = JObject.Parse(novelsJsonRaw);
        }
        catch (JsonException ex)
        {
            Debug.LogError($"[NovelJsonTools] Could not parse novels.json: {ex.Message}");
            return;
        }

        var vnToken = root["visualNovels"];
        if (vnToken is not JArray vnArray)
        {
            Debug.LogError("[NovelJsonTools] 'visualNovels' is missing or not an array.");
            return;
        }

        int beforeCount = vnArray.Count;

        // 3) Von hinten nach vorne durchgehen und Titel entfernen
        for (int i = vnArray.Count - 1; i >= 0; i--)
        {
            if (vnArray[i] is not JObject item)
                continue;

            string title = item["title"]?.ToString();
            if (!string.IsNullOrEmpty(title) &&
                titlesToRemove.Contains(title.Trim()))
            {
                Debug.Log($"[NovelJsonTools] Removing novel '{title}' (index {i}).");
                vnArray.RemoveAt(i);
            }
        }

        int afterCount = vnArray.Count;
        int removed = beforeCount - afterCount;

        Debug.Log($"[NovelJsonTools] Before: {beforeCount}, after: {afterCount}, removed: {removed}");

        // 4) Datei zurückschreiben (Struktur bleibt identisch, nur Einträge fehlen)
        string updatedJson = root.ToString(Formatting.Indented);
        File.WriteAllText(novelsJsonPath, updatedJson, Encoding.UTF8);
    }
    
    /// <summary>
    /// Wrapper class for the list of novels to remove.
    /// File format:
    /// {
    ///   "visualNovels": [
    ///     "Einstieg",
    ///     "Eltern",
    ///     "Honorar"
    ///   ]
    /// }
    /// </summary>
    public class VisualNovelTitlesToRemove
    {
        public List<string> visualNovels;
    }
}
