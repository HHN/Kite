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

        public static BackStackManager Instance => _instance ??= new BackStackManager();

        /// <summary>
        /// Manages a back stack for scene navigation, allowing tracking of scene history
        /// and facilitating navigation operations such as pushing, popping, and clearing scenes.
        /// Provides a singleton instance for global access.
        /// </summary>
        private BackStackManager()
        {
            _backStack = new Stack<string>();
        }

        private string Current => _backStack.Count == 0 ? "" : _backStack.Peek();

        /// <summary>
        /// Pushes a new scene name onto the back stack if it is not already the current scene.
        /// Ensures the back stack accurately represents the navigation history of the application.
        /// </summary>
        /// <param name="sceneName">The name of the scene to be added to the back stack.</param>
        public void Push(string sceneName)
        {
            if (Current == sceneName) return;
            _backStack.Push(sceneName);
        }

        /// <summary>
        /// Removes and returns the most recent scene from the back stack.
        /// If the back stack is empty, it returns an empty string.
        /// </summary>
        /// <returns>The name of the most recent scene if available; otherwise, an empty string.</returns>
        public string Pop()
        {
            if (_backStack.Count == 0) return "";
            string sceneName = _backStack.Pop();
            return sceneName;
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

        /// <summary>
        /// Returns a string representation of the current back stack, showing the sequence
        /// of scene names in the order they were pushed onto the stack.
        /// This is useful for debugging or visualizing the navigation history.
        /// </summary>
        /// <returns>A string containing the scene names in the back stack, separated by " -> ".</returns>
        public override string ToString()
        {
            return string.Join(" -> ", _backStack.ToArray());
        }
    }
}