using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UserFeedback
{
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

        private void Start()
        {
            showMoreButton.onClick.AddListener(OnShowMoreButton);
        }

        public void OnValueEntered()
        {
            if (textObject == null)
            {
                return;
            }

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

        private void OnShowMoreButton()
        {
            if (!initialized)
            {
                return;
            }

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
}