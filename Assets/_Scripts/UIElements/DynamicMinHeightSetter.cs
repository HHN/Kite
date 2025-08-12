using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements
{
    /// <summary>
    /// This script dynamically sets the minimum height of a UI element's LayoutElement
    /// to match its current RectTransform's height. This can be useful in UI layouts
    /// where an element's intrinsic size needs to influence its minimum allowed size
    /// within a UI layout group (e.g., VerticalLayoutGroup).
    /// </summary>
    [RequireComponent(typeof(LayoutElement))]
    [RequireComponent(typeof(RectTransform))]
    public class DynamicMinHeightSetter : MonoBehaviour
    {
        private LayoutElement _layoutElement;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// It's used here to get a reference to the LayoutElement component
        /// on this GameObject.
        /// </summary>
        private void Awake()
        {
            _layoutElement = GetComponent<LayoutElement>();
        }

        /// <summary>
        /// Called once per frame.
        /// In each frame, it updates the 'minHeight' property of the LayoutElement
        /// to match the current height of the RectTransform. This makes the element's
        /// minimum height always equal to its rendered height.
        /// </summary>
        private void Update()
        {
            _layoutElement.minHeight = GetComponent<RectTransform>().rect.height;
        }
    }
}