using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Player
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