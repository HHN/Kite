using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Editor
{
    public static class ScrollViewSensitivityBatchSetter
    {
        [MenuItem("Tools/Set ScrollView Sensitivity to 10")]
        public static void ApplySensitivity()
        {
            int totalModified = 0;

            // 1) Prefabs bearbeiten
            string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets" });
            foreach (var guid in prefabGuids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var prefabRoot = PrefabUtility.LoadPrefabContents(path);
                bool changed = false;

                foreach (var scroll in prefabRoot.GetComponentsInChildren<ScrollRect>(true))
                {
                    if (Mathf.Approximately(scroll.scrollSensitivity, 10f) == false)
                    {
                        scroll.scrollSensitivity = 10f;
                        changed = true;
                        totalModified++;
                    }
                }

                if (changed)
                {
                    PrefabUtility.SaveAsPrefabAsset(prefabRoot, path);
                }
                PrefabUtility.UnloadPrefabContents(prefabRoot);
            }

            // 2) Szenen bearbeiten
            string[] sceneGuids = AssetDatabase.FindAssets("t:Scene", new[] { "Assets" });
            foreach (var guid in sceneGuids)
            {
                string scenePath = AssetDatabase.GUIDToAssetPath(guid);
                var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
                bool sceneChanged = false;

                foreach (var scroll in Object.FindObjectsOfType<ScrollRect>(true))
                {
                    if (Mathf.Approximately(scroll.scrollSensitivity, 10f) == false)
                    {
                        scroll.scrollSensitivity = 10f;
                        EditorUtility.SetDirty(scroll);
                        sceneChanged = true;
                        totalModified++;
                    }
                }

                if (sceneChanged)
                {
                    EditorSceneManager.SaveScene(scene);
                }
            }

            EditorUtility.DisplayDialog(
                "Fertig",
                $"ScrollSensitivity wurde auf 10 gesetzt in {totalModified} Komponenten.",
                "OK"
            );
        }
    }
}