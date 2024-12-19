using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Common.UI
{
    public class DynamicMinHeightSetter : MonoBehaviour
    {
        private LayoutElement _layoutElement;

        private void Awake()
        {
            _layoutElement = GetComponent<LayoutElement>();
        }

        private void Update()
        {
            _layoutElement.minHeight = GetComponent<RectTransform>().rect.height;
        }
    }
}