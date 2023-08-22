using UnityEngine;

public class SelectOptionContinueConversation : MonoBehaviour
{
    public void OnMessageShowed()
    {
        PlayNovelSceneController controller = GameObject.Find("Controller").GetComponent<PlayNovelSceneController>();
        controller.PlayNextEvent();
    }
}
