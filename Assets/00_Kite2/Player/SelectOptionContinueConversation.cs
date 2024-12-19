using UnityEngine;

namespace _00_Kite2.Player
{
    public class SelectOptionContinueConversation : MonoBehaviour
    {
        public bool alreadyPlayedNextEvent;
        public PlayNovelSceneController controller;

        public void OnMessageShowed()
        {
            if (alreadyPlayedNextEvent)
            {
            }
            //controller.PlayNextEvent();
        }

        public void OnTypewritingStart()
        {
            controller = GameObject.Find("Controller").GetComponent<PlayNovelSceneController>();
            controller.selectOptionContinueConversation = this;
        }
    }
}