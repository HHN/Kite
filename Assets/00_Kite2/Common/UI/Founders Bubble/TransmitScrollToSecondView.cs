using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TransmitScrollToSecondView : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private ScrollRect SecondScrollToTransmitTo;

    public void OnDrag(PointerEventData data)
    {
        SecondScrollToTransmitTo.OnDrag(data);
    }
    public void OnBeginDrag(PointerEventData data)
    {
        SecondScrollToTransmitTo.OnBeginDrag(data);
    }

    public void OnEndDrag(PointerEventData data)
    {
        SecondScrollToTransmitTo.OnEndDrag(data);
    }
}