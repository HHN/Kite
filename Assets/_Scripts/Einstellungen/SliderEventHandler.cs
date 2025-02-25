using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets._Scripts.Einstellungen
{
    public class SliderEventHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Slider slider;

        public event Action<float> OnSliderReleasedEvent; // Event, das den Slider-Wert ?bergibt

        private bool isDragging = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            isDragging = true; // Slider wird gehalten
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (isDragging)
            {
                isDragging = false; // Slider wird losgelassen
                OnSliderReleased(slider.value); // Event ausl?sen
            }
        }

        private void OnSliderReleased(float value)
        {
            OnSliderReleasedEvent?.Invoke(value); // Event ausl?sen, falls Abonnenten vorhanden
        }
    }
}
