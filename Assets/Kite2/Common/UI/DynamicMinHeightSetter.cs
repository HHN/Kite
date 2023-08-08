using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutElement))]
public class DynamicMinHeight : MonoBehaviour
{
    private LayoutElement layoutElement;

    void Awake()
    {
        layoutElement = GetComponent<LayoutElement>();
    }

    void Start()
    {
        layoutElement.minHeight = GetComponent<RectTransform>().rect.height;
    }
}
