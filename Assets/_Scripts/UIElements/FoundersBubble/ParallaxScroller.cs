using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.FoundersBubble
{
    /// <summary>
    /// Implements a parallax scrolling effect for UI elements.
    /// It creates multiple layers (background, midground, foreground) that scroll at different speeds
    /// based on the horizontal scroll position of a <see cref="ScrollRect"/>.
    /// </summary>
    public class ParallaxScroller : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;
        
        [SerializeField] private RectTransform[] backgroundItemList;
        [SerializeField] private RectTransform[] midgroundItemList;

        [Header("Parallax-Layer")] 
        [SerializeField] private RectTransform backgroundParent;
        [SerializeField] private RectTransform midgroundParent;
        [SerializeField] private RectTransform foreground;

        [Range(0f, 1f)] [SerializeField] private float backgroundSpeed = 0.3f;
        [Range(0f, 1f)] [SerializeField] private float midgroundSpeed = 0.6f;

        private float _width;

        private Vector2 _bgStartPos;
        private Vector2 _mgStartPos;
        
        private HorizontalLayoutGroup _bgLayoutGroup;
        private HorizontalLayoutGroup _mgLayoutGroup;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Initializes layout groups, instantiates parallax items, calculates scrollable width,
        /// and sets up the scroll event listener and initial scroll position.
        /// </summary>
        private void Start()
        {
            _bgLayoutGroup = backgroundParent.GetComponent<HorizontalLayoutGroup>();
            _mgLayoutGroup = midgroundParent.GetComponent<HorizontalLayoutGroup>();

            if (!_bgLayoutGroup || !_mgLayoutGroup)
            {
                Debug.LogError("HorizontalLayoutGroup missing on backgroundParent or midgroundParent! Please add it in the Inspector.");
                return;
            }
            
            for (int i = 0; i < backgroundItemList.Length; i++)
            {
                Instantiate(backgroundItemList[i], backgroundParent);
            }
            
            for (int i = 0; i < backgroundItemList.Length; i++)
            {
                int itemIndex = backgroundItemList.Length - 1 - i;
                RectTransform rt = Instantiate(backgroundItemList[itemIndex], backgroundParent);
                rt.SetAsFirstSibling();
            }
            
            for (int i = 0; i < midgroundItemList.Length; i++)
            {
                Instantiate(midgroundItemList[i], midgroundParent);
            }
            
            for (int i = 0; i < midgroundItemList.Length; i++)
            {
                int itemIndex = midgroundItemList.Length - 1 - i;
                RectTransform rt = Instantiate(midgroundItemList[itemIndex], midgroundParent);
                rt.SetAsFirstSibling();
            }
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundParent);
            LayoutRebuilder.ForceRebuildLayoutImmediate(midgroundParent);
            
            _width = scrollRect.content.rect.width - scrollRect.viewport.rect.width;

            scrollRect.onValueChanged.AddListener(OnScroll);

            // StartCoroutine(SetInitialScrollDelayed());
        }

        /// <summary>
        /// Callback function for when the ScrollRect's value changes.
        /// Calculates the new anchored positions for the background and midground layers
        /// based on the scroll position and their respective speeds, creating the parallax effect.
        /// </summary>
        /// <param name="scrollPos">The normalized scroll position (x and y components).</param>
        private void OnScroll(Vector2 scrollPos)
        {
            float scrollX = scrollPos.x;
            float baseScroll = scrollX * _width;

            backgroundParent.anchoredPosition = new Vector2(_bgStartPos.x - baseScroll * backgroundSpeed, _bgStartPos.y);
            midgroundParent.anchoredPosition = new Vector2(_mgStartPos.x - baseScroll * midgroundSpeed, _mgStartPos.y);
        }
    }
}