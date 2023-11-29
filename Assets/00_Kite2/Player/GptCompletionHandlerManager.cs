using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GptCompletionHandlerManager
{
    private static GptCompletionHandlerManager instance;

    private GptCompletionHandlerManager()
    {
    }

    public static GptCompletionHandlerManager Instance()
    {
        if (instance == null)
        {
            instance = new GptCompletionHandlerManager();
        }
        return instance;
    }

    public GptCompletionHandler GetCompletionHandlerById(int id)
    {
        switch (id)
        {
            case 1: { return new DefaultGptCompletionHandler(); }

            default: { return new DefaultGptCompletionHandler(); }
        }
    }
}
