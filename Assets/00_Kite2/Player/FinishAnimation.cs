using UnityEngine;

namespace _00_Kite2.Player
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