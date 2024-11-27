using _00_Kite2.Player;
using UnityEngine;

public class FinishAnimation : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
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
