using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// Manages audio listeners within the Unity scene to ensure proper audio behavior.
    /// This manager is responsible for handling the activation and deactivation of audio listeners
    /// to avoid conflicts, commonly caused by having multiple audio listeners active simultaneously.
    /// </summary>
    public class AudioListenerManager : MonoBehaviour
    {
        /// <summary>
        /// Represents a collection of audio listeners associated with a specific scene.
        /// This class is used to manage audio listeners for scenes in Unity, particularly
        /// in scenarios involving additive scene loading or unloading.
        /// </summary>
        private class SceneListeners
        {
            public Scene Scene;
            public List<AudioListener> Listeners;
        }

        // Scene listeners stack: [0] = main scene, [1...] = additive scenes in loading order
        private readonly List<SceneListeners> _sceneStack = new List<SceneListeners>();

        /// <summary>
        /// Initializes the AudioListenerManager by setting it to persist across scene changes,
        /// collecting audio listeners from the currently active main scene, and subscribing
        /// to scene loading and unloading events.
        /// This method ensures that the AudioListenerManager remains active across scene transitions
        /// and initializes the internal state to manage audio listeners effectively.
        /// Additionally, it collects all audio listeners from the active scene upon initialization
        /// to prepare for proper management.
        /// </summary>
        private void Awake()
        {
            // 1) This object should persist across scene changes
            DontDestroyOnLoad(gameObject);

            // 2) Initially: collect all listeners in the active (main) scene
            var initialScene = SceneManager.GetActiveScene();
            var initialListeners = FindObjectsOfType<AudioListener>().ToList();
            _sceneStack.Add(new SceneListeners {
                Scene = initialScene,
                Listeners = initialListeners
            });

            // 3) Register scene load/unload events
            SceneManager.sceneLoaded   += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        /// <summary>
        /// Handles the actions to perform when a new scene is loaded in additive mode.
        /// This method ensures that audio listeners in other scenes are properly disabled,
        /// while activating the audio listeners of the newly loaded scene.
        /// It maintains a stack of scene listeners to manage the activation state of
        /// audio listeners across different scenes.
        /// </summary>
        /// <param name="scene">The newly loaded scene.</param>
        /// <param name="mode">The mode in which the scene was loaded, which should be additive.</param>
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (mode != LoadSceneMode.Additive)
                return;

            // 1) Disable all previous listeners
            foreach (var entry in _sceneStack)
            foreach (var al in entry.Listeners)
                if (al != null) al.enabled = false;

            // 2) Find and activate all listeners of the newly loaded scene
            var newRoots      = scene.GetRootGameObjects();
            var newListeners  = newRoots
                .SelectMany(go => go.GetComponentsInChildren<AudioListener>(true))
                .ToList();
            foreach (var al in newListeners)
                al.enabled = true;

            // 3) Add the scene and its listeners on top of the stack
            _sceneStack.Add(new SceneListeners {
                Scene     = scene,
                Listeners = newListeners
            });
        }

        /// <summary>
        /// Handles the unloading of a scene by removing its associated audio listeners
        /// from the internal management stack. This ensures proper management of
        /// active audio listeners across scenes. When a scene is unloaded, this
        /// method reactivates the audio listeners of the remaining scene at the top
        /// of the stack if any are present.
        /// </summary>
        /// <param name="scene">The scene that has been unloaded.</param>
        private void OnSceneUnloaded(Scene scene)
        {
            // 1) Remove entry for the unloaded scene
            var entry = _sceneStack.FirstOrDefault(e => e.Scene == scene);
            if (entry != null)
                _sceneStack.Remove(entry);

            // 2) Reactivate listeners of the last remaining scene (stack top)
            var top = _sceneStack.LastOrDefault();
            if (top == null) return;
            foreach (var al in top.Listeners)
                if (al != null)
                    al.enabled = true;
        }

        /// <summary>
        /// Cleans up event subscriptions and resources when the AudioListenerManager is destroyed.
        /// This method unsubscribes from the SceneManager's sceneLoaded and sceneUnloaded events
        /// to prevent memory leaks or unintended behavior when the object is no longer active.
        /// </summary>
        private void OnDestroy()
        {
            // Sauber abmelden
            SceneManager.sceneLoaded   -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }
    }
}
