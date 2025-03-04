using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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
            
                // Öffne den Link im Browser
                OpenURL(url);
            }
        }

        private void OpenURL(string url)
        {
#if UNITY_WEBGL
            Application.OpenURL(url);
#else
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
#endif
        }
    }
}