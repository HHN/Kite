using UnityEngine;
using UnityEngine.EventSystems;

namespace _00_Kite2.Player
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