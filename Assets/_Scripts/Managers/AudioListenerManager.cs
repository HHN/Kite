using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Scripts.Managers
{
    public class AudioListenerManager : MonoBehaviour
    {
        // Hilfsklasse zum Speichern, welche Listener zu welcher Szene gehören
        class SceneListeners
        {
            public Scene scene;
            public List<AudioListener> listeners;
        }

        // Stack der Szenen-Listener: [0] = Hauptszene, [1...] = additive Szenen in Lade-Reihenfolge
        private readonly List<SceneListeners> _sceneStack = new List<SceneListeners>();

        void Awake()
        {
            // 1) Dieses Objekt soll beim Szenenwechsel bestehen bleiben
            DontDestroyOnLoad(gameObject);

            // 2) Anfangs: alle Listener in der aktiven (Haupt-)Szene sammeln
            var initialScene = SceneManager.GetActiveScene();
            var initialListeners = FindObjectsOfType<AudioListener>().ToList();
            _sceneStack.Add(new SceneListeners {
                scene = initialScene,
                listeners = initialListeners
            });

            // 3) Auflade-/Entlade-Events registrieren
            SceneManager.sceneLoaded   += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        // Wird aufgerufen, wenn eine Szene additiv geladen wird
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (mode != LoadSceneMode.Additive)
                return;

            // 1) Alle bisherigen Listener abschalten
            foreach (var entry in _sceneStack)
            foreach (var al in entry.listeners)
                if (al != null) al.enabled = false;

            // 2) Alle Listener der neu geladenen Szene finden und aktivieren
            var newRoots      = scene.GetRootGameObjects();
            var newListeners  = newRoots
                .SelectMany(go => go.GetComponentsInChildren<AudioListener>(true))
                .ToList();
            foreach (var al in newListeners)
                al.enabled = true;

            // 3) Szene + ihre Listener oben auf den Stack legen
            _sceneStack.Add(new SceneListeners {
                scene     = scene,
                listeners = newListeners
            });
        }

        // Wird aufgerufen, wenn eine Szene entladen wird
        private void OnSceneUnloaded(Scene scene)
        {
            // 1) Entferne den Eintrag für die entladene Szene
            var entry = _sceneStack.FirstOrDefault(e => e.scene == scene);
            if (entry != null)
                _sceneStack.Remove(entry);

            // 2) Aktiviere wieder die Listener der zuletzt verbliebenen Szene (Stack-Top)
            var top = _sceneStack.LastOrDefault();
            if (top == null) return;
            foreach (var al in top.listeners)
                if (al != null)
                    al.enabled = true;
        }

        void OnDestroy()
        {
            // Sauber abmelden
            SceneManager.sceneLoaded   -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }
    }
}
