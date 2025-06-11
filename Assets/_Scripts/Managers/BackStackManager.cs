using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    // Used to hold the different scenes.
    public class BackStackManager
    {
        private static BackStackManager _instance;
        private readonly Stack<string> _backStack;

        private BackStackManager()
        {
            _backStack = new Stack<string>();
        }

        public static BackStackManager Instance()
        {
            if (_instance == null)
            {
                _instance = new BackStackManager();
            }

            return _instance;
        }

        public void Push(string sceneName)
        {
            if (Peek() == sceneName)
            {
                return;
            }

            _backStack.Push(sceneName);
        }

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

        private string Peek()
        {
            if (_backStack.Count == 0)
            {
                return "";
            }

            return _backStack.Peek();
        }

        public void Clear()
        {
            _backStack.Clear();
        }
    }
}