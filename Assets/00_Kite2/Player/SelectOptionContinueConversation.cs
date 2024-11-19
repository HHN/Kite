using _00_Kite2.Player;
using UnityEngine;

public class SelectOptionContinueConversation : MonoBehaviour
{
    public bool alreadyPlayedNextEvent = false;
    public PlayNovelSceneController controller;

    public void OnMessageShowed()
    {
        if (alreadyPlayedNextEvent)
        {
            return;
        }
        //controller.PlayNextEvent();
    }

    public void OnTypwritingStart()
    {
        controller = GameObject.Find("Controller").GetComponent<PlayNovelSceneController>();
        controller.selectOptionContinueConversation = this;
    }
}
