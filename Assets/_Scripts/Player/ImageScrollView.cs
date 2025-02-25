using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Player
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