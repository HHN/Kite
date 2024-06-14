using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BigTextPreViewController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textObject;
    [SerializeField] private Button showMoreButton;
    [SerializeField] private string textString;
    [SerializeField] private string previewText;
    [SerializeField] private bool previewDisplayed;
    [SerializeField] private RectTransform parentContainer;
    [SerializeField] private RectTransform parentParentContainer;
    [SerializeField] private RectTransform rootContainer;
    [SerializeField] private bool initialized;

    void Start()
    {
        showMoreButton.onClick.AddListener(delegate { OnShowMoreButton(); });
    }

    public void OnValueEntered()
    {
        if (textObject == null) { return; }

        textString = textObject.text;

        if (textString.Length > 100)
        {
            previewText = textString.Substring(0, 100 - 17) + "... MEHR ANZEIGEN";
            textObject.text = previewText;
            previewDisplayed = true;
            initialized = true;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(parentContainer);
        LayoutRebuilder.ForceRebuildLayoutImmediate(parentParentContainer);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rootContainer);
    }

    public void OnShowMoreButton()
    {
        if (!initialized) { return; }

        if (previewDisplayed)
        {
            textObject.text = textString;
            previewDisplayed = false;
        }
        else
        {
            textObject.text = previewText;
            previewDisplayed = true;
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(parentContainer);
        LayoutRebuilder.ForceRebuildLayoutImmediate(parentParentContainer);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rootContainer);
    }
}
