using Assets._Scripts.Controller.SceneControllers;
using UnityEngine;

namespace Assets._Scripts.Player
{
    /// <summary>
    /// A <see cref="StateMachineBehaviour"/> script designed to be attached to Animator States.
    /// It triggers an 'Animation Finished' callback on the <see cref="PlayNovelSceneController"/>
    /// when the animation state is entered.
    /// </summary>
    public class FinishAnimation : StateMachineBehaviour
    {
        /// <summary>
        /// Called when the Animator enters a state that has this behaviour attached.
        /// This method finds the <see cref="PlayNovelSceneController"/> in the scene
        /// and notifies it that an animation has finished.
        /// </summary>
        /// <param name="animator">The <see cref="Animator"/> component that is controlling this behaviour.</param>
        /// <param name="stateInfo">Information about the current animation state.</param>
        /// <param name="layerIndex">The index of the current Animator layer.</param>
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // Find the GameObject named "Controller" in the scene.
            GameObject controller = GameObject.Find("Controller");

            // If the controller GameObject is not found, exit early.
            if (controller == null)
            {
                Debug.LogWarning("FinishAnimation: 'Controller' GameObject not found in scene.");
                return;
            }

            // Get the PlayNovelSceneController component from the found GameObject.
            PlayNovelSceneController novelSceneController = controller.GetComponent<PlayNovelSceneController>();

            // If the controller component is not found, exit early.
            if (novelSceneController == null)
            {
                Debug.LogWarning("FinishAnimation: PlayNovelSceneController component not found on 'Controller' GameObject.");
                return;
            }

            // Notify the scene controller that the animation has finished.
            novelSceneController.AnimationFinished();
        }
    }
}