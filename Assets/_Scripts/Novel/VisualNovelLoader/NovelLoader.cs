using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets._Scripts.Managers;
using Assets._Scripts.Utilities;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Newtonsoft.Json.Linq;


namespace Assets._Scripts.Novel.VisualNovelLoader
{
    /// <summary>
    /// NovelLoader is a MonoBehaviour that is responsible for loading novels (Visual Novels)
    /// from a JSON file and passing them to the KiteNovelManager.
    /// </summary>
    public class NovelLoader : MonoBehaviour
    {
        private const string NovelsPath = "novels.json";

        /// <summary>
        /// Initiates the process to load all novels from the JSON file
        /// and schedules the destruction of the NovelLoader object after a set duration.
        /// </summary>
        private void Start()
        {
            StartCoroutine(LoadAllNovelsFromJson());

            Destroy(gameObject, 5f);
        }

        /// <summary>
        /// Loads all novels from the JSON file specified in the configuration
        /// and assigns them to the KiteNovelManager for further management.
        /// </summary>
        /// <returns>Returns an IEnumerator to support coroutine execution.</returns>
        private IEnumerator LoadAllNovelsFromJson()
        {
            if (KiteNovelManager.Instance().AreNovelsLoaded())
            {
                LogManager.Info("[NovelLoader] Novels are already loaded, skipping load.", this);
                yield break;
            }

            string fullPath = Path.Combine(Application.streamingAssetsPath, NovelsPath);
            LogManager.Info($"[NovelLoader] Loading novels from: {fullPath}", this);

            yield return StartCoroutine(LoadNovels(fullPath, listOfAllNovel =>
            {
                if (listOfAllNovel != null && listOfAllNovel.Count != 0)
                {
                    LogManager.Info($"[NovelLoader] LoadNovels callback called with {listOfAllNovel.Count} items.", this);
                    KiteNovelManager.Instance().SetAllKiteNovels(listOfAllNovel);
                }
                else
                {
                    LogManager.Warning("[NovelLoader] LoadNovels callback returned null or empty list.", this);
                }
            }));

            LogManager.Info("[NovelLoader] LoadAllNovelsFromJson finished.", this);
        }


        /// <summary>
        /// Loads novels from the given file path and invokes the callback function
        /// with a list of VisualNovel objects upon completion.
        /// </summary>
        /// <param name="path">The path to the file containing the novel JSON data.</param>
        /// <param name="callback">A callback function to handle the list of VisualNovel objects after loading.</param>
        /// <returns>An IEnumerator allowing the operation to be executed asynchronously.</returns>
        private IEnumerator LoadNovels(string path, System.Action<List<VisualNovel>> callback)
        {
            LogManager.Info($"[NovelLoader] Entering LoadNovels for path: {path}", this);

            yield return StartCoroutine(LoadFileContent(path, jsonString =>
            {
                if (string.IsNullOrEmpty(jsonString))
                {
                    LogManager.Warning("[NovelLoader] LoadFileContent returned null or empty JSON string.", this);
                    callback(null);
                    return;
                }

                string cleanedJson = CleanJson(jsonString);
                LogManager.Info($"[NovelLoader] JSON length after cleaning: {cleanedJson.Length}", this);

                try
                {
                    var settings = new JsonSerializerSettings();
                    settings.Converters.Add(new UnityColorJsonConverter());

                    NovelListWrapper kiteNovelList =
                        JsonConvert.DeserializeObject<NovelListWrapper>(cleanedJson, settings);

                    if (kiteNovelList?.VisualNovels != null)
                    {
                        LogManager.Info($"[NovelLoader] Successfully parsed {kiteNovelList.VisualNovels.Count} novels.", this);
                    }
                    else
                    {
                        LogManager.Warning("[NovelLoader] Parsed JSON but VisualNovels list is null.", this);
                    }

                    kiteNovelList?.DebugLogAllNovels();
                    callback(kiteNovelList?.VisualNovels);
                }
                catch (JsonException ex)
                {
                    LogManager.Error($"[NovelLoader] JSON parse error: {ex.Message}", this);
                    LogManager.Info(
                        $"[NovelLoader] First 200 chars of JSON:\n{cleanedJson.Substring(0, Mathf.Min(200, cleanedJson.Length))}",
                        this
                    );
                    callback(null);
                }
            }));

            LogManager.Info("[NovelLoader] Leaving LoadNovels (coroutine completed).", this);
        }



        /// <summary>
        /// Reads the content of a file from the specified path and invokes the provided callback
        /// with a string representing the file's content.
        /// </summary>
        /// <param name="path">The path to the file from which content is to be read. It can support platform-specific file systems.</param>
        /// <param name="callback">The callback function that handles the file content as a string once loading is complete.</param>
        /// <returns>An IEnumerator to facilitate asynchronous loading of the file content.</returns>
        private IEnumerator LoadFileContent(string path, System.Action<string> callback)
        {
#if UNITY_IOS
    try
    {
        LogManager.Info($"[NovelLoader] (iOS) Reading file directly: {path}", this);
        string jsonString = File.ReadAllText(path);
        callback(jsonString);
    }
    catch (System.Exception ex)
    {
        LogManager.Error($"[NovelLoader] Error loading file at {path}: {ex.Message}", this);
        callback(null);
    }
#else
            string uri = path;
            if (!uri.StartsWith("file://"))
            {
                uri = "file://" + uri;
            }

            LogManager.Info($"[NovelLoader] (UWR) Requesting URI: {uri}", this);

            using (UnityWebRequest www = UnityWebRequest.Get(uri))
            {
                yield return www.SendWebRequest();

                LogManager.Info($"[NovelLoader] (UWR) Finished request. Result={www.result}, Error='{www.error}', ResponseCode={www.responseCode}", this);

                if (www.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
                {
                    LogManager.Error($"[NovelLoader] Error loading file at {uri}: {www.error}", this);
                    callback(null);
                }
                else
                {
                    string text = www.downloadHandler.text;
                    LogManager.Info($"[NovelLoader] (UWR) Downloaded {text?.Length ?? 0} characters.", this);
                    callback(text);
                }
            }
#endif
        }


        /// <summary>
        /// Removes a UTF-8 BOM (if present) and trims leading/trailing whitespace.
        /// This avoids "Unexpected character" errors when JSON files start with a BOM.
        /// </summary>
        private static string CleanJson(string json)
        {
            if (string.IsNullOrEmpty(json))
                return json;

            // Remove BOM if present
            if (json.Length > 0 && json[0] == '\uFEFF')
            {
                json = json.Substring(1);
            }

            // Optional: trim whitespace/newlines around the JSON
            return json.Trim();
        }
    }
    
    /// <summary>
/// Json.NET converter for UnityEngine.Color that tolerates different JSON formats:
/// - Empty or null string -> default color
/// - "#RRGGBB"/"#RRGGBBAA" or HTML color names -> parsed via ColorUtility
/// - Object { "r": ..., "g": ..., "b": ..., "a": ... } -> direct RGBA
/// </summary>
public class UnityColorJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Color);
    }

    public override object ReadJson(
        JsonReader reader,
        Type objectType,
        object existingValue,
        JsonSerializer serializer)
    {
        // Null or undefined -> keep existing or use white
        if (reader.TokenType == JsonToken.Null || reader.TokenType == JsonToken.Undefined)
        {
            return GetDefaultColor(existingValue);
        }

        // String cases: "", "#RRGGBB", "#RRGGBBAA", "red", etc.
        if (reader.TokenType == JsonToken.String)
        {
            string s = (reader.Value as string)?.Trim();

            if (string.IsNullOrEmpty(s))
            {
                // This is exactly your current error case: novelColor = ""
                Debug.LogWarning("[UnityColorJsonConverter] Empty color string encountered. Using default color (white).");
                return GetDefaultColor(existingValue);
            }

            if (ColorUtility.TryParseHtmlString(s, out var htmlColor))
            {
                return htmlColor;
            }

            Debug.LogWarning($"[UnityColorJsonConverter] Unable to parse color from string '{s}'. Using default color.");
            return GetDefaultColor(existingValue);
        }

        // Object case: { "r": ..., "g": ..., "b": ..., "a": ... }
        if (reader.TokenType == JsonToken.StartObject)
        {
            try
            {
                JObject obj = JObject.Load(reader);

                float r = obj["r"] != null ? obj["r"].Value<float>() : 0f;
                float g = obj["g"] != null ? obj["g"].Value<float>() : 0f;
                float b = obj["b"] != null ? obj["b"].Value<float>() : 0f;
                float a = obj["a"] != null ? obj["a"].Value<float>() : 1f;

                return new Color(r, g, b, a);
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[UnityColorJsonConverter] Error parsing color object: {ex.Message}. Using default color.");
                return GetDefaultColor(existingValue);
            }
        }

        // Fallback for unexpected token types
        Debug.LogWarning($"[UnityColorJsonConverter] Unexpected token type {reader.TokenType}. Using default color.");
        return GetDefaultColor(existingValue);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is Color c)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("r");
            writer.WriteValue(c.r);
            writer.WritePropertyName("g");
            writer.WriteValue(c.g);
            writer.WritePropertyName("b");
            writer.WriteValue(c.b);
            writer.WritePropertyName("a");
            writer.WriteValue(c.a);
            writer.WriteEndObject();
        }
        else
        {
            writer.WriteNull();
        }
    }

    /// <summary>
    /// Returns the existing color if present, otherwise Color.white as a safe default.
    /// </summary>
    private static Color GetDefaultColor(object existingValue)
    {
        if (existingValue is Color existing)
        {
            return existing;
        }

        return Color.white;
    }
}

}
