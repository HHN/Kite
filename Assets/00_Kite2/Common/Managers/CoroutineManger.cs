using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManger : MonoBehaviour
{

    private static CoroutineManger instance;

    private Coroutine speakingCoroutine;

    public static CoroutineManger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CoroutineManger>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("CoroutineManger");
                    instance = obj.AddComponent<CoroutineManger>();
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }

    public void SetSpeakingCoroutine(Coroutine coroutine)
    {
        speakingCoroutine = coroutine;
    }

    public Coroutine GetSpeakingCoroutine()
    {
        return speakingCoroutine;
    }

    public void StopSpeakingCoroutine()
    {
        StopCoroutine(speakingCoroutine);
    }
}
