using UnityEngine;
using UnityEngine.UI;

public class RequestExpertFeedbackButton : MonoBehaviour
{
    [SerializeField] private Button requestExpertFeedbackButton;
    [SerializeField] private GameObject requestExpertFeedbackPanel;
    [SerializeField] private GameObject container;

    void Start()
    {
        requestExpertFeedbackButton.onClick.AddListener(delegate { OnRequestExpertFeedbackButton(); });

    }

    public void OnRequestExpertFeedbackButton()
    {
        Instantiate(requestExpertFeedbackPanel, container.transform);
    }
}