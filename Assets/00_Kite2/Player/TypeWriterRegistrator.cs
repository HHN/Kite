using Febucci.UI.Core;
using UnityEngine;

public class TypeWriterRegistrator : MonoBehaviour
{
    private PlayNovelSceneController controller;

    private void Start()
    {
        controller = GameObject.Find("Controller").GetComponent<PlayNovelSceneController>();
    }

    public void OnStartTyping()
    {
        TypewriterCore typewriterCore = GetComponent<TypewriterCore>();
        controller.currentTypeWriter = typewriterCore;
        controller.StartTalking();
    }

    public void OnStopTyping()
    {
        controller.StopTalking();
    }
}
