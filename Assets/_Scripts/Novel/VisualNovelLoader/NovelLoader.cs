using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets._Scripts.Managers;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets._Scripts.Novel.VisualNovelLoader
{
    // NovelLoader ist ein MonoBehaviour, das daf�r verantwortlich ist, die Novellen (Visual Novels)
    // aus einer JSON-Datei zu laden und an den KiteNovelManager zu �bergeben.
    public class NovelLoader : MonoBehaviour
    {
        // Konstante, die den Dateinamen der JSON-Datei definiert, in der alle Novellen gespeichert sind.
        private const string NOVELS_PATH = "novels.json";

        // Start() wird beim Starten des GameObjects aufgerufen.
        // Hier wird der Ladevorgang der Novellen initialisiert.
        private void Start()
        {
            // Starte die Coroutine, die alle Novellen aus der JSON-Datei l�dt.
            StartCoroutine(LoadAllNovelsFromJson());
            
            Destroy(gameObject, 5f);
        }

        /// <summary>
        /// L�dt alle Novellen aus der JSON-Datei, sofern sie noch nicht im KiteNovelManager geladen sind.
        /// </summary>
        /// <returns>IEnumerator f�r die Coroutine</returns>
        private IEnumerator LoadAllNovelsFromJson()
        {
            // �berpr�fe, ob die Novellen bereits geladen wurden. Falls ja, wird die Coroutine beendet.
            if (KiteNovelManager.Instance().AreNovelsLoaded())
            {
                yield break;
            }

            // Erstelle den vollst�ndigen Pfad zur JSON-Datei, indem Application.streamingAssetsPath
            // (der Ordner f�r Streaming Assets) mit dem NOVELS_PATH kombiniert wird.
            string fullPath = Path.Combine(Application.streamingAssetsPath, NOVELS_PATH);

            // Starte die Coroutine LoadNovels, um die Novellen aus der JSON-Datei zu laden.
            // Das Ergebnis (eine Liste von VisualNovel-Objekten) wird �ber den Callback verarbeitet.
            yield return StartCoroutine(LoadNovels(fullPath, listOfAllNovel =>
            {
                // Falls die geladene Liste null oder leer ist, wird eine Warnung im Log ausgegeben.
                if (listOfAllNovel != null && listOfAllNovel.Count != 0)
                {
                    // Ansonsten wird die geladene Liste der Novellen an den KiteNovelManager �bergeben,
                    // der die Novellen im weiteren Verlauf verwaltet.
                    KiteNovelManager.Instance().SetAllKiteNovels(listOfAllNovel);
                }
                else
                {
                    Debug.LogWarning("Loading Novels failed: No Novels found! Path: " + fullPath);
                }
            }));
        }

        /// <summary>
        /// L�dt die Novellen aus einer JSON-Datei und deserialisiert sie in eine Liste von VisualNovel-Objekten.
        /// </summary>
        /// <param name="path">Der vollst�ndige Pfad zur JSON-Datei</param>
        /// <param name="callback">Callback, der die deserialisierte Liste zur�ckgibt</param>
        /// <returns>IEnumerator f�r die Coroutine</returns>
        private IEnumerator LoadNovels(string path, System.Action<List<VisualNovel>> callback)
        {
            // Starte die Coroutine LoadFileContent, um den Inhalt der Datei asynchron zu laden.
            yield return StartCoroutine(LoadFileContent(path, jsonString =>
            {
                // Wenn der geladene JSON-String leer oder null ist, rufe den Callback mit null auf.
                if (string.IsNullOrEmpty(jsonString))
                {
                    callback(null);
                }
                else
                {
                    // Deserialisiere den JSON-String in ein NovelListWrapper-Objekt.
                    NovelListWrapper kiteNovelList = JsonConvert.DeserializeObject<NovelListWrapper>(jsonString);
                    // �bergibt die Liste der VisualNovels, die in NovelListWrapper gespeichert sind, an den Callback.
                    callback(kiteNovelList?.VisualNovels);
                }
            }));
        }

        /// <summary>
        /// L�dt den Inhalt einer Datei asynchron.
        /// - Auf iOS (iPhonePlayer) wird die Datei synchron �ber File.ReadAllText geladen.
        /// - Auf anderen Plattformen wird UnityWebRequest verwendet, um den Inhalt asynchron zu laden.
        /// </summary>
        /// <param name="path">Der Pfad zur Datei</param>
        /// <param name="callback">Callback, das den geladenen Text zur�ckgibt</param>
        /// <returns>IEnumerator f�r die Coroutine</returns>
        private IEnumerator LoadFileContent(string path, System.Action<string> callback)
        {
            // Falls die Plattform iOS ist, wird die Datei direkt aus dem Dateisystem gelesen.
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                string jsonString = File.ReadAllText(path);
                callback(jsonString);
            }
            else
            {
                // F�r andere Plattformen wird UnityWebRequest verwendet, um den Dateiinhalt asynchron zu laden.
                using (UnityWebRequest www = UnityWebRequest.Get(path))
                {
                    // Warte, bis die Anfrage abgeschlossen ist.
                    yield return www.SendWebRequest();

                    // �berpr�fe, ob ein Verbindungs- oder Protokollfehler aufgetreten ist.
                    if ((www.result == UnityWebRequest.Result.ConnectionError) ||
                        (www.result == UnityWebRequest.Result.ProtocolError))
                    {
                        // Logge den Fehler und rufe den Callback mit null auf.
                        Debug.LogError($"Error loading file at {path}: {www.error}");
                        callback(null);
                    }
                    else
                    {
                        // Wenn kein Fehler aufgetreten ist, �bergebe den geladenen Text an den Callback.
                        callback(www.downloadHandler.text);
                    }
                }
            }
        }
    }
}