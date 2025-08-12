using UnityEngine;

namespace Assets._Scripts.UIElements.FoundersBubble
{
    /// <summary>
    /// Positions a visual content element within a button in a zigzag pattern (alternating Y-offset).
    /// This script is designed to be used by an Infinity Scroll system to dynamically
    /// adjust the vertical position of individual items within a scrolling list.
    /// </summary>
    public class Positioner : MonoBehaviour
    {
        [Tooltip("Das RectTransform des visuellen Inhalts innerhalb des Buttons, das verschoben werden soll.")]
        [SerializeField] private RectTransform visualContentRect;

        // These values are set externally by the InfinityScroll script.
        [HideInInspector] public int itemIndex;
        [HideInInspector] public float yOffsetAmount;

        private RectTransform _buttonRootRect; // The RectTransform of the button's root GameObject.
        private int _totalNovelCount;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Retrieves the RectTransform of the button's root and tries to find the visualContentRect
        /// if it's not already assigned in the Inspector.
        /// </summary>
        private void Awake()
        {
            _buttonRootRect = GetComponent<RectTransform>();

            if (visualContentRect == null && transform.childCount > 0)
            {
                visualContentRect = transform.GetChild(0).GetComponent<RectTransform>();
            }
            if (visualContentRect == null)
            {
                Debug.LogError($"Positioner ({gameObject.name}): visualContentRect not assigned and not found as first child. Please assign it in Inspector.", this);
            }
        }

        /// <summary>
        /// Called when the object becomes enabled and active.
        /// If the visual content and total novel count are set, it applies the zigzag positioning.
        /// This ensures correct positioning when objects are reused in an object pool.
        /// </summary>
        private void OnEnable()
        {
            if (visualContentRect != null && _totalNovelCount > 0) // Check if already initialized with data
            {
                ApplyZigzagYPosition();
            }
        }

        /// <summary>
        /// This method is called by the InfinityScroll script to pass the item's index,
        /// the Y-offset amount, and the total novel count, then triggers the positioning.
        /// </summary>
        /// <param name="newIndex">The logical index of the item in the scroll list.</param>
        /// <param name="offset">The size of the Y-offset for the zigzag effect.</param>
        /// <param name="novelCount">The total number of items (novels) in the scroll list.</param>
        public void SetupButton(int newIndex, float offset, int novelCount)
        {
            itemIndex = newIndex;
            yOffsetAmount = offset;
            _totalNovelCount = novelCount;
            ApplyZigzagYPosition();
        }

        /// <summary>
        /// Applies the zigzag Y-positioning to the <see cref="visualContentRect"/>.
        /// Items with an even logical index will be offset by <see cref="yOffsetAmount"/>,
        /// while items with an odd logical index will be offset by -<see cref="yOffsetAmount"/>.
        /// </summary>
        private void ApplyZigzagYPosition()
        {
            if (visualContentRect == null || _totalNovelCount == 0) // Ensure critical references and data are set.
            {
                return;
            }

            float targetYOffset;

            // Determine the Y-offset based on whether the itemIndex is even or odd.
            if (itemIndex % 2 == 0) // Even logical indices of novels
            {
                targetYOffset = yOffsetAmount;
            }
            else // Odd logical indices of novels
            {
                targetYOffset = -yOffsetAmount;
            }

            // Important: We set the Y-position of the child element (visualContentRect).
            // The X-position remains unchanged, as the Horizontal Layout Group manages the parent button's X-position.
            visualContentRect.anchoredPosition = new Vector2(visualContentRect.anchoredPosition.x, targetYOffset);
        }
    }
}