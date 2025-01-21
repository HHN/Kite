using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SliderEventHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Slider slider;

    public event Action<float> OnSliderReleasedEvent; // Event, das den Slider-Wert übergibt

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
            OnSliderReleased(slider.value); // Event auslösen
        }
    }

    private void OnSliderReleased(float value)
    {
        OnSliderReleasedEvent?.Invoke(value); // Event auslösen, falls Abonnenten vorhanden
    }
}
