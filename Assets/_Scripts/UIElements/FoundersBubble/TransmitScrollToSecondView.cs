using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.Founders_Bubble
{
    public class TransmitScrollToSecondView : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private ScrollRect secondScrollToTransmitTo;

        public void OnDrag(PointerEventData data)
        {
            secondScrollToTransmitTo.OnDrag(data);
        }

        public void OnBeginDrag(PointerEventData data)
        {
            secondScrollToTransmitTo.OnBeginDrag(data);
        }

        public void OnEndDrag(PointerEventData data)
        {
            secondScrollToTransmitTo.OnEndDrag(data);
        }
    }
}