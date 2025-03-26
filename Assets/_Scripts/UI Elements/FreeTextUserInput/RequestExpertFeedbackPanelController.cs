using Assets._Scripts.Managers;
using Assets._Scripts.Novels;
using Assets._Scripts.Player;
using Assets._Scripts.SceneControllers;
using Assets._Scripts.Server_Communication;
using Assets._Scripts.Server_Communication.Server_Calls;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI_Elements.FreeTextUserInput
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