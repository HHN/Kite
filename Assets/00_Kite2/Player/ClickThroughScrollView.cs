using _00_Kite2.Player;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickThroughScrollView : MonoBehaviour, IPointerClickHandler
{
    public FoundersBubbleSceneController controller;

    public void OnPointerClick(PointerEventData eventData)
    {
        controller.OnBackgroundButton();
    }
}
