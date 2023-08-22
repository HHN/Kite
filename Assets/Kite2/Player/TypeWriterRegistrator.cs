using Febucci.UI.Core;
using UnityEngine;

public class TypeWriterRegistrator : MonoBehaviour
{
    public void OnStartTyping()
    {
        TypewriterCore typewriterCore = GetComponent<TypewriterCore>();
        PlayNovelSceneController controller = GameObject.Find("Controller").GetComponent<PlayNovelSceneController>();
        controller.currentTypeWriter = typewriterCore;
    }
}
