using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackRoleManagementSceneController : SceneController, OnSuccessHandler
{
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button confirmCodeButton;
    [SerializeField] private Button subscribeButton;
    [SerializeField] private Button novelReviewsButton;
    [SerializeField] private Button aiReviewsButton;
    [SerializeField] private Button observerButton;
    [SerializeField] private GameObject buttonsToHide;
    [SerializeField] private RectTransform layout;
    [SerializeField] private GameObject getUserRoleByCodeServerCallPrefab;

    // Start is called before the first frame update
    void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);

        BackStackManager.Instance().Push(SceneNames.FEEDBACK_ROKE_MANAGEMENT_SCENE);

        infoText.text = "Aktuelle Rolle: " + FeedbackRoleManager.Instance.GetFeedbackRoleName();
        int role = FeedbackRoleManager.Instance.GetFeedbackRole();

        if (role == 4 || role == 5)
        {
            buttonsToHide.SetActive(true);
        } else
        {
            buttonsToHide.SetActive(false);
        }

        confirmCodeButton.onClick.AddListener(delegate { OnConfirmButton(); });
        subscribeButton.onClick.AddListener(delegate { OnSubscribeButton(); });
        novelReviewsButton.onClick.AddListener(delegate { OnNovelReviewButton(); });
        aiReviewsButton.onClick.AddListener(delegate { OnKiReviewButton(); });
        observerButton.onClick.AddListener(delegate { OnObserverButton(); });
    }

    public void OnSubscribeButton()
    {
        SceneLoader.LoadAddObserverScene();
    }

    public void OnConfirmButton()
    {
        string input = inputField.text.Trim();
        inputField.text = "";
        GetUserRoleByCodeServerCall call = Instantiate(getUserRoleByCodeServerCallPrefab).GetComponent<GetUserRoleByCodeServerCall>();
        call.sceneController = this;
        call.onSuccessHandler = this;
        call.code = input;
        call.SendRequest();
        DontDestroyOnLoad(call.gameObject);
    }

    public void OnNovelReviewButton()
    {
        SceneLoader.LoadNovelReviewExplorerScene();
    }

    public void OnKiReviewButton()
    {
        SceneLoader.LoadAiReviewExplorerScene();
    }

    public void OnObserverButton()
    {
        SceneLoader.LoadReviewObserverExplorerScene();
    }

    public void OnSuccess(Response response)
    {
        string result = FeedbackRoleManager.Instance.SubmitCode(response.GetUserRole());
        this.DisplayInfoMessage(result);

        infoText.text = "Aktuelle Rolle: " + FeedbackRoleManager.Instance.GetFeedbackRoleName();
        int role = FeedbackRoleManager.Instance.GetFeedbackRole();

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
