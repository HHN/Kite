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
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator ScrollToBottom()
        {
            yield return null;
            Canvas.ForceUpdateCanvases();
            GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 0);
        }
    }
}