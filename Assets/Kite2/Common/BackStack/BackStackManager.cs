using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackStackManager
{
    private static BackStackManager instance;
    private Stack<string> backStack;

    private BackStackManager() 
    { 
        backStack = new Stack<string>();
    }  

    public static BackStackManager Instance() 
    { 
        if (instance == null)
        {
            instance = new BackStackManager();
        }
        return instance; 
    }

    public void Push(string sceneName)
    {
        if (Peek() == sceneName)
        {
            return;
        }
        backStack.Push(sceneName);
    }

    public string Pop()
    {
        if (backStack.Count == 0)
        {
            return "";
        }
        return backStack.Pop();
    }

    public string Peek()
    {
        if (backStack.Count == 0)
        {
            return "";
        }
        return backStack.Peek();
    }

    public void Clear() 
    { 
        backStack.Clear(); 
    }
}
