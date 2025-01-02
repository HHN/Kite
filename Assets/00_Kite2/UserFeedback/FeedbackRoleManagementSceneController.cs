using _00_Kite2.Common;
using _00_Kite2.Common.Managers;
using _00_Kite2.Common.SceneManagement;
using _00_Kite2.Server_Communication;
using _00_Kite2.Server_Communication.Server_Calls;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.UserFeedback
{
    public class FeedbackRoleManagementSceneController : SceneController, IOnSuccessHandler
    {
        [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button confirmCodeButton;
        [SerializeField] private Button subscribeButton;
        [SerializeField] private Button novelReviewsButton;
        [SerializeField] private Button aiReviewsButton;
        [SerializeField] private Button promptsAndCompletionsButton;
        [SerializeField] private GameObject buttonsToHide;
        [SerializeField] private RectTransform layout;
        [SerializeField] private GameObject getUserRoleByCodeServerCallPrefab;

        // Start is called before the first frame update
        private void Start()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);

            BackStackManager.Instance().Push(SceneNames.FEEDBACK_ROKE_MANAGEMENT_SCENE);

            infoText.text = "Aktuelle Rolle: " + Common.Managers.FeedbackRoleManager.Instance.GetFeedbackRoleName();
            int role = Common.Managers.FeedbackRoleManager.Instance.GetFeedbackRole();

            if (role == 4 || role == 5)
            {
                buttonsToHide.SetActive(true);
            }
            else
            {
                buttonsToHide.SetActive(false);
            }

            confirmCodeButton.onClick.AddListener(OnConfirmButton);
            subscribeButton.onClick.AddListener(OnSubscribeButton);
            novelReviewsButton.onClick.AddListener(OnNovelReviewButton);
            aiReviewsButton.onClick.AddListener(OnKiReviewButton);
            promptsAndCompletionsButton.onClick.AddListener(OnPromptsAndCompletionsButton);
        }

        private void OnSubscribeButton()
        {
            SceneLoader.LoadAddObserverScene();
        }

        private void OnConfirmButton()
        {
            string input = inputField.text.Trim();
            inputField.text = "";
            GetUserRoleByCodeServerCall call = Instantiate(getUserRoleByCodeServerCallPrefab)
                .GetComponent<GetUserRoleByCodeServerCall>();
            call.sceneController = this;
            call.OnSuccessHandler = this;
            call.code = input;
            call.SendRequest();
            DontDestroyOnLoad(call.gameObject);
        }

        private void OnNovelReviewButton()
        {
            SceneLoader.LoadNovelReviewExplorerScene();
        }

        private void OnKiReviewButton()
        {
            SceneLoader.LoadAiReviewExplorerScene();
        }

        private void OnPromptsAndCompletionsButton()
        {
            SceneLoader.LoadPromptsAndCompletionsExplorerScene();
        }

        public void OnSuccess(Response response)
        {
            string result = Common.Managers.FeedbackRoleManager.Instance.SubmitCode(response.GetUserRole());
            this.DisplayInfoMessage(result);

            infoText.text = "Aktuelle Rolle: " + Common.Managers.FeedbackRoleManager.Instance.GetFeedbackRoleName();
            int role = Common.Managers.FeedbackRoleManager.Instance.GetFeedbackRole();

            if (role == 4 || role == 5)
            {
                buttonsToHide.SetActive(true);
            }
            else
            {
                buttonsToHide.SetActive(false);
            }
        }
    }
}