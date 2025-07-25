using Assets._Scripts.Controller.SceneControllers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._Scripts.Player
{
    /// <summary>
    /// This component allows a UI element (specifically intended for a ScrollView)
    /// to pass click events through to an underlying controller, even when the ScrollView
    /// itself might normally consume them. It implements the <see cref="IPointerClickHandler"/> interface.
    /// </summary>
    public class ClickThroughScrollView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private FounderBubbleSceneControllerNew controller;

        /// <summary>
        /// Called when a pointer click occurs on this UI element.
        /// This method forwards the click event to the associated controller's background button handler.
        /// </summary>
        /// <param name="eventData">The <see cref="PointerEventData"/> containing information about the pointer click.</param>
        public void OnPointerClick(PointerEventData eventData)
        {
            controller.OnBackgroundButton();
        }
    }
}