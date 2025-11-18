using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Player
{
    /// <summary>
    /// Manages the scrolling behavior of a chat view, specifically providing functionality
    /// to automatically scroll to the bottom of the scroll view.
    /// </summary>
    public class ChatScrollView : MonoBehaviour
    {
        /// <summary>
        /// Scrolls the content of the <c>ScrollRect</c> immediately to the bottom (normalized position Y=0).
        /// </summary>
        /// <remarks>
        /// This coroutine is necessary to ensure the <c>Canvas</c> layout is calculated correctly (via <c>ForceUpdateCanvases</c>) 
        /// before setting the final scroll position, preventing visual errors when dynamic content size changes.
        /// </remarks>
        /// <returns>An enumerator allowing Unity to execute the scrolling across frames.</returns>
        public IEnumerator ScrollToBottom()
        {
            yield return null;
            Canvas.ForceUpdateCanvases();
            GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 0);
        }
    }
}