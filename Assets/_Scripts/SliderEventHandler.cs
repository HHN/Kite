using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets._Scripts
{
    /// <summary>
    /// This component extends a Unity UI Slider to provide an event that fires specifically
    /// when the user releases the slider handle after dragging it.
    /// It implements <see cref="IPointerDownHandler"/> and <see cref="IPointerUpHandler"/>
    /// to detect the start and end of a drag operation.
    /// </summary>
    [RequireComponent(typeof(Slider))] // Ensures a Slider component is present on the GameObject.
    public class SliderEventHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Slider slider;

        public event Action<float> OnSliderReleasedEvent;

        private bool _isDragging;

        /// <summary>
        /// Called when a pointer (e.g., mouse click, touch) is pressed down on the GameObject.
        /// This method sets the <see cref="_isDragging"/> flag to true, indicating a drag operation has begun.
        /// </summary>
        /// <param name="eventData">Current event data, containing info about the pointer event.</param>
        public void OnPointerDown(PointerEventData eventData)
        {
            _isDragging = true;
        }

        /// <summary>
        /// Called when a pointer that was previously pressed down on the GameObject is released.
        /// If the slider was being dragged (<see cref="_isDragging"/> is true), this method
        /// triggers the <see cref="OnSliderReleased"/> event with the slider's current value.
        /// </summary>
        /// <param name="eventData">Current event data, containing info about the pointer event.</param>
        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isDragging) return;
            
            _isDragging = false;
            OnSliderReleased(slider.value);
        }

        /// <summary>
        /// A private helper method to safely invoke the <see cref="OnSliderReleasedEvent"/>.
        /// It checks if there are any subscribers before invoking to prevent NullReferenceExceptions.
        /// </summary>
        /// <param name="value">The value of the slider at the moment it was released.</param>
        private void OnSliderReleased(float value)
        {
            OnSliderReleasedEvent?.Invoke(value);
        }
    }
}
