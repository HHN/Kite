using Assets._Scripts.Controller.SceneControllers;
using UnityEngine;

namespace Assets._Scripts.Player
{
    /// <summary>
    /// A component responsible for handling the continuation of the conversation
    /// after an option has been selected or a message has finished displaying.
    /// It interacts with the <see cref="PlayNovelSceneController"/> to manage the narrative flow.
    /// </summary>
    public class SelectOptionContinueConversation : MonoBehaviour
    {
        public bool alreadyPlayedNextEvent;
        public PlayNovelSceneController controller;

        /// <summary>
        /// Called when the typewriting effect of a message starts.
        /// This method finds and establishes a reference to the <see cref="PlayNovelSceneController"/>
        /// and registers itself with the controller for callback purposes.
        /// </summary>
        public void OnTypewritingStart()
        {
            controller = GameObject.Find("PlayNovelSceneController").GetComponent<PlayNovelSceneController>();
            controller.selectOptionContinueConversation = this;
        }
    }
}