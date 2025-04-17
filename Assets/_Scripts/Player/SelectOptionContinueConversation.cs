using Assets._Scripts.Controller.SceneControllers;
using UnityEngine;

namespace Assets._Scripts.Player
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