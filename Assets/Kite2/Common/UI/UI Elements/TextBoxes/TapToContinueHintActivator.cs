using Febucci.UI.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToContinueHintActivator : MonoBehaviour
{
    private PlayNovelSceneController controller;

    private void Start()
    {
        controller = GameObject.Find("Controller").GetComponent<PlayNovelSceneController>();
    }

    public void OnStartTyping()
    {
        controller.SetTypeToContinueAnimationActive(false);
    }

    public void OnStopTyping()
    {
        controller.SetTypeToContinueAnimationActive(true);
    }
}
