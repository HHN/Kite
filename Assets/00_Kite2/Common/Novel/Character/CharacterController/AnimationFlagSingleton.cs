using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFlagSingleton
{
    // Singleton instance
    private static AnimationFlagSingleton instance;

    // Flag indicating the state of the animation
    private bool animationFlag;

    // Object used for thread safety when creating the instance
    private static readonly object lockObject = new object();

    // Returns the singleton instance, creates it if it doesn't exist
    public static AnimationFlagSingleton Instance()
    {
        // Ensure thread-safe initialization with double-checked locking
        if (instance == null)
        {
            lock (lockObject)
            {
                instance ??= new AnimationFlagSingleton();
            }
        }
        return instance;
    }

    // Returns the current value of the animation flag
    public bool GetFlag()
    {
        return animationFlag;
    }

    // Sets the animation flag
    public void SetFlag(bool flag)
    {
        animationFlag = flag;
    }
}
