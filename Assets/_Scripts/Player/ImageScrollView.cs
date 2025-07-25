using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Player
{
    /// <summary>
    /// Manages the scrolling behavior of an image view, providing functionality
    /// to scroll to a specific normalized position within the scroll view.
    /// </summary>
    public class ImageScrollView : MonoBehaviour
    {
        /// <summary>
        /// A coroutine that forces the <see cref="ScrollRect"/> to scroll to a specified normalized point (x, y).
        /// It waits for one frame to ensure UI elements are updated before forcing the scroll.
        /// </summary>
        /// <param name="x">The normalized horizontal position to scroll to (0 = left, 1 = right).</param>
        /// <param name="y">The normalized vertical position to scroll to (0 = bottom, 1 = top).</param>
        /// <returns>An <see cref="IEnumerator"/> for the coroutine execution.</returns>
        public IEnumerator ScrollToPoint(float x, float y)
        {
            yield return null;

            Canvas.ForceUpdateCanvases();

            GetComponent<ScrollRect>().normalizedPosition = new Vector2(x, y);
        }
    }
}