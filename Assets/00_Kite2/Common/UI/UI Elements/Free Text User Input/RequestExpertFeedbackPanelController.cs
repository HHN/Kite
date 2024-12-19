using _00_Kite2.Common.Managers;
using _00_Kite2.Common.Novel;
using _00_Kite2.Player;
using _00_Kite2.Server_Communication;
using _00_Kite2.Server_Communication.Server_Calls;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Common.UI.UI_Elements.Free_Text_User_Input
{
    public class RequestExpertFeedbackPanelController : MonoBehaviour, IOnSuccessHandler
    {
        [SerializeField] private GameObject wrapper;
        [SerializeField] private GameObject requestExpertFeedbackServerCall;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button cancelButton;
        [SerializeField] private Button submitButton;
        [SerializeField] private SceneController controller;
        [SerializeField] private bool sent;

        private void Start()
        {
            cancelButton.onClick.AddListener(OnCancelButton);
            submitButton.onClick.AddListener(OnSubmitButton);
            controller = GameObject.Find("Controller").GetComponent<SceneController>();
        }

        private void OnSubmitButton()
        {
            if (sent)
            {
                return;
            }

            sent = true;
            string question = inputField.text;

            AddExpertFeedbackQuestionServerCall call = Instantiate(requestExpertFeedbackServerCall)
                .GetComponent<AddExpertFeedbackQuestionServerCall>();
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

        private void OnCancelButton()
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
}