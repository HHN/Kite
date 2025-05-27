using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollWheelDebugger : MonoBehaviour, IScrollHandler
{
    public void OnScroll(PointerEventData eventData)
    {
        Debug.Log($"[SCROLL DEBUG] Delta = {eventData.scrollDelta.y}");
    }
}