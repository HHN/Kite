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
        Debug.Log("OnStartTyping");
        TypewriterCore typewriterCore = GetComponent<TypewriterCore>();
        
        // typewriterCore.SkipTypewriter();
        
        if (GameManager.Instance.calledFromReload)
        {
            Debug.Log("OnStartTyping called from Reload");
            typewriterCore.SkipTypewriter();
        }
        
        controller.currentTypeWriter = typewriterCore;
        controller.StartTalking();
    }

    public void OnStopTyping()
    {
        controller.StopTalking();
    }
}
