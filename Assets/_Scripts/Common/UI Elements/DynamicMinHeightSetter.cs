using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Common.UI_Elements
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