using _00_Kite2.Common;
using _00_Kite2.Common.Managers;
using _00_Kite2.Common.Novel;
using _00_Kite2.Common.SceneManagement;
using _00_Kite2.Server_Communication;
using _00_Kite2.Server_Communication.Server_Calls;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.UserFeedback
{
    public class ReviewAiSceneController : SceneController, IOnSuccessHandler
    {
        [SerializeField] private Button skipButton;
        [SerializeField] private Button confirmButton;
        [SerializeField] private TextMeshProUGUI aiFeedback;
        [SerializeField] private TextMeshProUGUI instruction;
        [SerializeField] private string novelName;
        [SerializeField] private long novelID;
        [SerializeField] private string reviewText;
        [SerializeField] public string prompt;
        [SerializeField] public string aiFeedbackString;

        [SerializeField] private TMP_InputField inputField;

        [SerializeField] private GameObject addAiReviewServerCallPrefab;
        [SerializeField] private RectTransform layout;

        // Start is called before the first frame update
        private void Start()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
            BackStackManager.Instance().Push(SceneNames.REVIEW_AI_SCENE);
            skipButton.onClick.AddListener(OnSkipButton);

            confirmButton.onClick.AddListener(OnConfirmButton);

            VisualNovel novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();

            if (novelToPlay == null)
            {
                return;
            }

            novelName = novelToPlay.title;
            novelID = novelToPlay.id;
            aiFeedbackString = novelToPlay.feedback;
            aiFeedback.text = aiFeedbackString;
            prompt = PromptManager.Instance().GetPrompt(novelToPlay.context);
        }

        private void OnConfirmButton()
        {
            VisualNovel novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();

            if (novelToPlay == null || string.IsNullOrEmpty(inputField.text))
            {
                return;
            }

            reviewText = inputField.text.Trim();
            inputField.text = "";
            SendReviewToServer();

            BackStackManager.Instance().Clear(); // we go back to the founders bubble and don't want
            // the back-button to bring us to the feedback scene again
            SceneLoader.LoadFoundersBubbleScene();
        }

        private void OnSkipButton()
        {
            BackStackManager.Instance().Clear(); // we go back to the founders bubble and don't want
            // the back-button to bring us to the feedback scene again
            SceneLoader.LoadFoundersBubbleScene();
        }

        private void SendReviewToServer()
        {
            AddAiReviewServerCall call = Instantiate(addAiReviewServerCallPrefab).GetComponent<AddAiReviewServerCall>();
            call.sceneController = this;
            call.OnSuccessHandler = this;
            call.novelId = novelID;
            call.novelName = novelName;
            call.reviewText = reviewText;
            call.prompt = prompt;
            call.aiFeedback = aiFeedbackString;
            call.SendRequest();
            DontDestroyOnLoad(call.gameObject);
        }

        public void OnSuccess(Response response)
        {
            //Nothing to do here
        }
    }
}