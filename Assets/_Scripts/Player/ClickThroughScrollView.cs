using Assets._Scripts.Controller.SceneControllers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._Scripts.Player
{
    public class ClickThroughScrollView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private FounderBubbleSceneControllerNew controller;

        public void OnPointerClick(PointerEventData eventData)
        {
            controller.OnBackgroundButton();
        }
    }
}