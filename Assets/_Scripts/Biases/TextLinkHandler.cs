using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._Scripts.Biases
{
    /// <summary>
    /// A handler class designed to detect and respond to clickable links within a TextMeshProUGUI component in Unity.
    /// </summary>
    public class TextLinkHandler : MonoBehaviour, IPointerClickHandler
    {
        private TextMeshProUGUI _textMeshPro;

        private void Awake()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        /// <summary>
        /// Handles click events on the TextMeshProUGUI component, detects clickable links, and executes appropriate actions.
        /// </summary>
        /// <param name="eventData">The PointerEventData associated with the pointer click event.</param>
        public void OnPointerClick(PointerEventData eventData)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(_textMeshPro, eventData.position, null);

            if (linkIndex != -1)
            {
                TMP_LinkInfo linkInfo = _textMeshPro.textInfo.linkInfo[linkIndex];
                string url = linkInfo.GetLinkID();

                if (!string.IsNullOrEmpty(url))
                {
                    Application.OpenURL(url);
                }
            }
        }
    }
}