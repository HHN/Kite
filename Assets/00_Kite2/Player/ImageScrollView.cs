using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Player
{
    public class ImageScrollView : MonoBehaviour
    {
        public IEnumerator ScrollToPoint(float x, float y)
        {
            yield return null;
            Canvas.ForceUpdateCanvases();
            GetComponent<ScrollRect>().normalizedPosition = new Vector2(x, y);
        }
    }
}