using _00_Kite2;
using _00_Kite2.Player;
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
        
        Debug.Log("OnStartTyping");
        
        if (GameManager.Instance.calledFromReload)
        {
            Debug.Log("OnStartTyping called from Reload");
            typewriterCore.SkipTypewriter();
        }
        else
        {
            controller.StartTalking();
        }
        


    }

    public void OnStopTyping()
    {
        controller.StopTalking();
    }
}
