using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Assets._Scripts.Managers
{
    public class EventSystemManager : MonoBehaviour
    {
        /// <summary>
        /// Represents an entry corresponding to a scene and its associated event systems.
        /// </summary>
        private class SceneEntry
        {
            public Scene scene;
            public List<EventSystem> systems;
        }

        // Stack of all scenes with their event systems (index 0 = main scene, then additive)
        private readonly List<SceneEntry> _sceneStack = new List<SceneEntry>();

        /// <summary>
        /// Initializes the EventSystemManager instance.
        /// Ensures the object is not destroyed on scene load, gathers all EventSystems
        /// in the initially active scene, and subscribes to scene load and unload events
        /// to handle EventSystem management across additive scenes.
        /// </summary>
        private void Awake()
        {
            // Never destroy on scene change
            DontDestroyOnLoad(gameObject);

            // 1) Initially: collect all EventSystems in the active (main) scene
            Scene initial = SceneManager.GetActiveScene();
            var initialSystems = FindEventSystemsInScene(initial);
            _sceneStack.Add(new SceneEntry { scene = initial, systems = initialSystems });

            // 2) Subscribe to scene load and unload events
            SceneManager.sceneLoaded   += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        /// <summary>
        /// Handles logic when a scene is loaded in additive mode.
        /// Disables all EventSystems in previously loaded scenes,
        /// activates all EventSystems in the newly loaded scene,
        /// and updates the scene stack with the new scene and its associated EventSystems.
        /// </summary>
        /// <param name="scene">The scene that was loaded.</param>
        /// <param name="mode">The load mode of the scene. This method only performs actions if the mode is additive.</param>
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (mode != LoadSceneMode.Additive)
                return;
        
            // 1) Disable all existing EventSystems
            foreach (var entry in _sceneStack)
            foreach (var es in entry.systems)
                if (es != null) es.enabled = false;
        
            // 2) Find and activate all EventSystems in the new scene
            var newSystems = FindEventSystemsInScene(scene);
            foreach (var es in newSystems)
                es.enabled = true;
        
            // 3) Add the scene and its EventSystems to top of the stack
            _sceneStack.Add(new SceneEntry { scene = scene, systems = newSystems });
        }

        /// <summary>
        /// Handles logic when a scene is unloaded.
        /// Removes the corresponding entry for the unloaded scene from the internal scene stack
        /// and reactivates the EventSystems of the last remaining scene in the stack, if any.
        /// </summary>
        /// <param name="scene">The scene that has been unloaded.</param>
        private void OnSceneUnloaded(Scene scene)
        {
            // 1) Remove the corresponding stack entry
            var entry = _sceneStack.FirstOrDefault(e => e.scene == scene);
            if (entry != null)
                _sceneStack.Remove(entry);

            // 2) Reactivate the EventSystems of the last remaining scene
            var top = _sceneStack.LastOrDefault();
            
            if (top == null) return;
            
            foreach (var es in top.systems)
                if (es != null)
                    es.enabled = true;
        }

        /// <summary>
        /// Retrieves all EventSystem components present in the specified scene.
        /// Searches through all root GameObjects and their children to gather active
        /// and inactive EventSystem components.
        /// </summary>
        /// <param name="scene">The scene to search for EventSystem components.</param>
        /// <returns>A list of EventSystem components found in the specified scene.</returns>
        private List<EventSystem> FindEventSystemsInScene(Scene scene)
        {
            var roots = scene.GetRootGameObjects();
            return roots
                .SelectMany(go => go.GetComponentsInChildren<EventSystem>(true))
                .ToList();
        }

        /// <summary>
        /// Unsubscribes from the scene load and unload events managed by the SceneManager.
        /// Ensures that no lingering references remain, preventing potential memory leaks or unintended behavior.
        /// </summary>
        private void OnDestroy()
        {
            SceneManager.sceneLoaded   -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }
    }
}
