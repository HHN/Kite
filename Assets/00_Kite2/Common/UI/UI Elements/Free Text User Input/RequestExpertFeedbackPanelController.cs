using _00_Kite2.Common.Managers;
using _00_Kite2.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequestExpertFeedbackPanelController : MonoBehaviour, OnSuccessHandler
{
    [SerializeField] private GameObject wrapper;
    [SerializeField] private GameObject requestExpertFeedbackServerCall;
    [SerializeField] private TMP_InputField inputfield;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button submitButton;
    [SerializeField] private SceneController controller;
    [SerializeField] private bool sent;

    void Start()
    {
        cancelButton.onClick.AddListener(delegate { OnCancleButton(); });
        submitButton.onClick.AddListener(delegate { OnSubmitButton(); });
        controller = GameObject.Find("Controller").GetComponent<SceneController>();
    }

    public void OnSubmitButton()
    {
        if (sent)
        {
            return;
        }
        sent = true;
        string question = inputfield.text;

        AddExpertFeedbackQuestionServerCall call = Instantiate(requestExpertFeedbackServerCall).GetComponent<AddExpertFeedbackQuestionServerCall>();
        call.sceneController = controller;
        call.OnSuccessHandler = this;

        VisualNovel novel = PlayManager.Instance().GetVisualNovelToPlay();

        if (novel == null)
        {
            Debug.LogError("Unexpected Error!");
            return;
        } 

        call.novelId = novel.id;
        call.novelName = novel.title;
        call.userUuid = ExpertFeedbackQuestionManager.Instance.GetUUID();
        call.prompt = PromptManager.Instance().GetPrompt(novel.context);
        call.aiFeedback = novel.feedback;
        call.dialogue = PromptManager.Instance().GetDialog();
        call.expertFeedbackQuestion = question.Trim();

        call.SendRequest();
        DontDestroyOnLoad(call.gameObject);

        FeedbackSceneController feedbackSceneController = (FeedbackSceneController)controller;
        feedbackSceneController.DeactivateAskButton();
    }

    public void OnCancleButton()
    {
        wrapper.SetActive(false);
        Destroy(wrapper);
    }

    public void OnSuccess(Response response)
    {
        wrapper.SetActive(false);
        Destroy(wrapper);
    }
}
