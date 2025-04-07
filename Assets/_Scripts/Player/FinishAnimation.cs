using Assets._Scripts.Controller.SceneControllers;
using UnityEngine;

namespace Assets._Scripts.Player
{
    public class FinishAnimation : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GameObject controller = GameObject.Find("Controller");

            if (controller == null)
            {
                return;
            }

            PlayNovelSceneController novelSceneController = controller.GetComponent<PlayNovelSceneController>();

            if (novelSceneController == null)
            {
                return;
            }

            novelSceneController.AnimationFinished();
        }
    }
}