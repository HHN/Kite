using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Assets._Scripts.Managers
{
    public class EventSystemManager : MonoBehaviour
    {
        // Hilfsklasse, um die EventSystems pro Szene zu speichern
        class SceneEntry
        {
            public Scene scene;
            public List<EventSystem> systems;
        }

        // Stack aller Szenen mit ihren EventSystems (Index 0 = Hauptszene, dann additiv)
        private readonly List<SceneEntry> _sceneStack = new List<SceneEntry>();

        void Awake()
        {
            // Nie zerstören beim Szenenwechsel
            DontDestroyOnLoad(gameObject);

            // 1) Initial: alle EventSystems in der aktiven (Haupt-)Szene sammeln
            Scene initial = SceneManager.GetActiveScene();
            var initialSystems = FindEventSystemsInScene(initial);
            _sceneStack.Add(new SceneEntry { scene = initial, systems = initialSystems });

            // 2) Auflade- und Entlade-Events abonnieren
            SceneManager.sceneLoaded   += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        // Wenn eine Szene additiv geladen wird:
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (mode != LoadSceneMode.Additive)
                return;

            // 1) Deaktiviere alle bisherigen EventSystems
            foreach (var entry in _sceneStack)
            foreach (var es in entry.systems)
                if (es != null) es.enabled = false;

            // 2) Finde und aktiviere alle EventSystems der neuen Szene
            var newSystems = FindEventSystemsInScene(scene);
            foreach (var es in newSystems)
                es.enabled = true;

            // 3) Szene + ihre EventSystems oben auf den Stack legen
            _sceneStack.Add(new SceneEntry { scene = scene, systems = newSystems });
        }

        // Wenn eine Szene entladen wird:
        private void OnSceneUnloaded(Scene scene)
        {
            // 1) Entferne den entsprechenden Stack-Eintrag
            var entry = _sceneStack.FirstOrDefault(e => e.scene == scene);
            if (entry != null)
                _sceneStack.Remove(entry);

            // 2) Reaktiviere die EventSystems der letzten verbliebenen Szene
            var top = _sceneStack.LastOrDefault();
            if (top == null) return;
            foreach (var es in top.systems)
                if (es != null)
                    es.enabled = true;
        }

        // Hilfsmethode: alle EventSystems in einer Szene finden
        private List<EventSystem> FindEventSystemsInScene(Scene scene)
        {
            var roots = scene.GetRootGameObjects();
            return roots
                .SelectMany(go => go.GetComponentsInChildren<EventSystem>(true))
                .ToList();
        }

        void OnDestroy()
        {
            // Events sauber abmelden
            SceneManager.sceneLoaded   -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }
    }
}
