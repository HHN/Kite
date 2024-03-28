using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollPassThrough : MonoBehaviour, IScrollHandler
{
    [SerializeField] private GameObject scrollView;

    public void OnScroll(PointerEventData eventData)
    {
        Debug.Log("TEST");
        ExecuteEvents.ExecuteHierarchy(scrollView, eventData, ExecuteEvents.scrollHandler);
    }

    public void OnScroll2()
    {
        Debug.Log("TEST");
        // ExecuteEvents.ExecuteHierarchy(scrollView, eventData, ExecuteEvents.scrollHandler);
    }
}
