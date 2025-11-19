using Assets._Scripts.Controller.SceneControllers;
using Assets._Scripts.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._Scripts.Player
{
    /// <summary>
    /// Detects touch or click events on the screen within the play novel scene.
    /// It distinguishes between simple clicks and drags based on a customizable threshold,
    /// and triggers a confirmation action in the scene controller for valid clicks.
    /// Implements <see cref="IPointerClickHandler"/> to receive pointer events.
    /// </summary>
    public class PlayNovelSceneTouchDetector : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private float dragThreshold = 10f;

        /// <summary>
        /// Called when a pointer click (touch or mouse click) occurs on this GameObject.
        /// It checks if the movement during the click was below the <see cref="dragThreshold"/>
        /// to ensure it's a genuine tap/click, then notifies the <see cref="PlayNovelSceneController"/>.
        /// </summary>
        /// <param name="eventData">The <see cref="PointerEventData"/> containing information about the pointer event.</param>
        public void OnPointerClick(PointerEventData eventData)
        {
            float distance = Vector2.Distance(eventData.pressPosition, eventData.position);
            if (distance > dragThreshold)
            {
                return;
            }

            if (PlayNovelSceneController.Instance != null)
            {
                PlayNovelSceneController.Instance.OnConfirm();
            }
            else
            {
                LogManager.Warning("PlayNovelSceneController.Instance ist nicht gesetzt!");
            }
        }
    }
}