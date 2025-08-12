using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// Manages a back stack for scene navigation, allowing tracking of scene history
    /// and facilitating navigation operations such as pushing, popping, and clearing scenes.
    /// Provides a singleton instance for global access.
    /// </summary>
    public class BackStackManager
    {
        private static BackStackManager _instance;
        private readonly Stack<string> _backStack;

        /// <summary>
        /// Manages a back stack for scene navigation, allowing tracking of scene history
        /// and facilitating navigation operations such as pushing, popping, and clearing scenes.
        /// Provides a singleton instance for global access.
        /// </summary>
        private BackStackManager()
        {
            _backStack = new Stack<string>();
        }

        /// <summary>
        /// Provides a singleton instance of the BackStackManager, ensuring a single global point of access
        /// for managing the back stack of scenes in the application.
        /// </summary>
        /// <returns>An instance of the BackStackManager.</returns>
        public static BackStackManager Instance()
        {
            if (_instance == null)
            {
                _instance = new BackStackManager();
            }

            return _instance;
        }

        /// <summary>
        /// Pushes a new scene name onto the back stack if it is not already the current scene.
        /// Ensures the back stack accurately represents the navigation history of the application.
        /// </summary>
        /// <param name="sceneName">The name of the scene to be added to the back stack.</param>
        public void Push(string sceneName)
        {
            if (Peek() == sceneName)
            {
                return;
            }

            _backStack.Push(sceneName);
        }

        /// <summary>
        /// Removes and returns the most recent scene from the back stack.
        /// If the back stack is empty, returns an empty string.
        /// </summary>
        /// <returns>The name of the most recent scene if available; otherwise, an empty string.</returns>
        public string Pop()
        {
            if (_backStack.Count == 0)
            {
                return "";
            }

            string sceneName = _backStack.Peek();
            _backStack.Pop();
            return sceneName;
        }

        /// <summary>
        /// Retrieves the name of the most recent scene from the back stack without removing it.
        /// Returns an empty string if the back stack is empty.
        /// </summary>
        /// <returns>The name of the most recent scene if available; otherwise, an empty string.</returns>
        private string Peek()
        {
            return _backStack.Count == 0 ? "" : _backStack.Peek();
        }

        /// <summary>
        /// Clears all entries from the back stack, effectively resetting the navigation history.
        /// This operation is often used to prevent returning to previous scenes after transitioning
        /// to a new root scene or context.
        /// </summary>
        public void Clear()
        {
            _backStack.Clear();
        }
    }
}