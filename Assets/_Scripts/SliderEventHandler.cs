using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets._Scripts
{
    public class SliderEventHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Slider slider;

        public event Action<float> OnSliderReleasedEvent; // Event, das den Slider-Wert übergibt

        private bool _isDragging;

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDragging = true; // Slider wird gehalten
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isDragging) return;
            
            _isDragging = false; // Slider wird losgelassen
            OnSliderReleased(slider.value); // Event auslösen
        }

        private void OnSliderReleased(float value)
        {
            OnSliderReleasedEvent?.Invoke(value); // Event auslösen, falls Abonnenten vorhanden
        }
    }
}
