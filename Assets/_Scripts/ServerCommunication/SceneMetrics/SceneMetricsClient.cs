// Assets/_Scripts/ServerCommunication/SceneMetrics/SceneMetricsClient.cs
using System;
using System.Collections;
using System.Text;
using Assets._Scripts.ServerCommunication.SceneMetrics;
using Assets._Scripts.Utilities; // für BypassCertificate (wie in deinem ServerCall)
using UnityEngine;
using UnityEngine.Networking;

namespace Assets._Scripts.ServerCommunication
{
    [Serializable]
    public class SceneHitResult    { public string scene; public long count; }
    [Serializable]
    public class SceneCountResult  { public string scene; public long count; }
    [Serializable]
    public class PlaythroughsResult { public string metric; public long count; }

    public static class SceneMetricsClient
    {
        /// <summary>
        /// POST /metrics/scenes/{scene}/hit  (fire-and-forget möglich)
        /// </summary>
        public static IEnumerator Hit(SceneType scene,
                                      Action<SceneHitResult> onSuccess = null,
                                      Action<string> onError = null)
        {
            string url = ConnectionLink.SCENE_HIT(scene.ToWireName());
            using (var req = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST))
            {
                // Leerer Body ist ok; UploadHandler setzen, damit POST sauber rausgeht
                req.uploadHandler = new UploadHandlerRaw(Array.Empty<byte>());
                req.downloadHandler = new DownloadHandlerBuffer();
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8");

                // Zertifikats-Bypass analog zu deinem ServerCall
                req.certificateHandler = new BypassCertificate();

                yield return req.SendWebRequest();

                if (req.result == UnityWebRequest.Result.Success)
                {
                    if (onSuccess != null)
                    {
                        try
                        {
                            var data = JsonUtility.FromJson<SceneHitResult>(req.downloadHandler.text);
                            onSuccess(data);
                        }
                        catch (Exception ex)
                        {
                            onError?.Invoke($"Parsefehler: {ex.Message}");
                        }
                    }
                }
                else
                {
                    onError?.Invoke(req.error);
                }
            }
        }

        /// <summary>
        /// GET /metrics/scenes/{scene}  -> { "scene": "...", "count": N }
        /// </summary>
        public static IEnumerator GetCount(SceneType scene,
                                           Action<SceneCountResult> onSuccess,
                                           Action<string> onError = null)
        {
            string url = ConnectionLink.SCENE_COUNT(scene.ToWireName());
            using (var req = UnityWebRequest.Get(url))
            {
                req.certificateHandler = new BypassCertificate();
                yield return req.SendWebRequest();

                if (req.result == UnityWebRequest.Result.Success)
                {
                    try
                    {
                        var data = JsonUtility.FromJson<SceneCountResult>(req.downloadHandler.text);
                        onSuccess?.Invoke(data);
                    }
                    catch (Exception ex)
                    {
                        onError?.Invoke($"Parsefehler: {ex.Message}");
                    }
                }
                else
                {
                    onError?.Invoke(req.error);
                }
            }
        }

        /// <summary>
        /// POST /metrics/playthroughs/hit
        /// Erhöht den globalen Playthrough-Zähler (Durchlauf abgeschlossen).
        /// </summary>
        public static IEnumerator HitPlaythrough(Action<PlaythroughsResult> onSuccess = null,
                                                 Action<string> onError = null)
        {
            using (var req = new UnityWebRequest(ConnectionLink.PLAYTHROUGHS_HIT, UnityWebRequest.kHttpVerbPOST))
            {
                req.uploadHandler   = new UploadHandlerRaw(Array.Empty<byte>()); // leerer Body
                req.downloadHandler = new DownloadHandlerBuffer();
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8");
                req.certificateHandler = new BypassCertificate();

                yield return req.SendWebRequest();

                if (req.result == UnityWebRequest.Result.Success)
                {
                    if (onSuccess != null)
                    {
                        try
                        {
                            var data = JsonUtility.FromJson<PlaythroughsResult>(req.downloadHandler.text);
                            onSuccess(data);
                        }
                        catch (Exception ex)
                        {
                            onError?.Invoke($"Parsefehler: {ex.Message}");
                        }
                    }
                }
                else
                {
                    onError?.Invoke(req.error);
                }
            }
        }
    }
}
