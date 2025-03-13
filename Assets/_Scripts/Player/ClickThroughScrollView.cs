using Assets._Scripts.SceneControllers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._Scripts.Player
{
    public class ClickThroughScrollView : MonoBehaviour, IPointerClickHandler
    {
        public FoundersBubbleSceneController controller;

        public void OnPointerClick(PointerEventData eventData)
        {
            controller.OnBackgroundButton();
        }
    }
}