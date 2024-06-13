using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BigTextPreViewController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textObject;
    [SerializeField] private Button showMoreButton;
    [SerializeField] private string textString;
    [SerializeField] private bool previewDisplayed;
    [SerializeField] private RectTransform parentContainer;
    [SerializeField] private RectTransform parentParentContainer;
    [SerializeField] private RectTransform rootContainer;

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
            string truncatedString = textString.Substring(0, 100 - 17) + "... MEHR ANZEIGEN";
            textObject.text = truncatedString;
            showMoreButton.gameObject.SetActive(true);
            previewDisplayed = true;
        }
    }

    public void OnShowMoreButton()
    {
        if (!previewDisplayed)
        {
            return;
        }
        showMoreButton.gameObject.SetActive(false);
        textObject.text = textString;
        previewDisplayed = false;
        LayoutRebuilder.ForceRebuildLayoutImmediate(parentContainer);
        LayoutRebuilder.ForceRebuildLayoutImmediate(parentParentContainer);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rootContainer);
    }
}
