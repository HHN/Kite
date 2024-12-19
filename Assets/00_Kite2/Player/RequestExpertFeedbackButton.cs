using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Player
{
    public class RequestExpertFeedbackButton : MonoBehaviour
    {
        [SerializeField] private Button requestExpertFeedbackButton;
        [SerializeField] private GameObject requestExpertFeedbackPanel;
        [SerializeField] private GameObject container;

        private void Start()
        {
            requestExpertFeedbackButton.onClick.AddListener(OnRequestExpertFeedbackButton);
        }

        private void OnRequestExpertFeedbackButton()
        {
            Instantiate(requestExpertFeedbackPanel, container.transform);
        }
    }
}