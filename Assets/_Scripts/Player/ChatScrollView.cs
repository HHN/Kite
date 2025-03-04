using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Player
{
    public class ChatScrollView : MonoBehaviour
    {
        public IEnumerator ScrollToBottom()
        {
            yield return null;
            Canvas.ForceUpdateCanvases();
            GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 0);
        }
    }
}