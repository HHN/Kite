using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets._Scripts.Common.UI_Elements
{
    public class ScrollPassThrough : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private ScrollRect scrollRect;

        public void OnBeginDrag(PointerEventData eventData)
        {
            scrollRect.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            scrollRect.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            scrollRect.OnEndDrag(eventData);
        }
    }
}