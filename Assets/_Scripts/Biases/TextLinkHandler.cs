using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;

namespace Assets._Scripts.Biases
{
    public class TextLinkHandler : MonoBehaviour, IPointerClickHandler
    {
        private TextMeshProUGUI _textMeshPro;

        private void Awake()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // Ermittle den index des geklickten Links
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(_textMeshPro, eventData.position, null);

            if (linkIndex != -1)
            {
                TMP_LinkInfo linkInfo = _textMeshPro.textInfo.linkInfo[linkIndex];
                string url = linkInfo.GetLinkID();
                
                Debug.Log(url);

                // Öffne den Link im Browser
                Application.OpenURL(url);
            }
        }
    }
}