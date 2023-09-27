using UnityEngine;
using UnityEngine.UI;

public class DynamicMinHeightSetter : MonoBehaviour
{
    private LayoutElement layoutElement;

    void Awake()
    {
        layoutElement = GetComponent<LayoutElement>();
    }

    void Update()
    {
        layoutElement.minHeight = GetComponent<RectTransform>().rect.height;
    }
}
