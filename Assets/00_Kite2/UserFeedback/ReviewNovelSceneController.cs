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
    public class ReviewNovelSceneController : SceneController, IOnSuccessHandler
    {
        [SerializeField] private Button skipButton;
        [SerializeField] private Button confirmButton;
        [SerializeField] private TextMeshProUGUI headline;
        [SerializeField] private TextMeshProUGUI instruction;
        [SerializeField] private string novelName;
        [SerializeField] private long novelID;
        [SerializeField] private long rating;
        [SerializeField] private string reviewText;

        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button star01;
        [SerializeField] private Button star02;
        [SerializeField] private Button star03;
        [SerializeField] private Button star04;
        [SerializeField] private Button star05;

        [SerializeField] private Sprite starFullSprite;
        [SerializeField] private Sprite starEmptySprite;

        [SerializeField] private GameObject addNovelReviewServerCallPrefab;
        [SerializeField] private RectTransform layout;

        private void Start()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
            BackStackManager.Instance().Push(SceneNames.REVIEW_NOVEL_SCENE);
            skipButton.onClick.AddListener(OnSkipButton);
            confirmButton.onClick.AddListener(OnConfirmButton);
            star01.onClick.AddListener(OnStar01Button);
            star02.onClick.AddListener(OnStar02Button);
            star03.onClick.AddListener(OnStar03Button);
            star04.onClick.AddListener(OnStar04Button);
            star05.onClick.AddListener(OnStar05Button);

            VisualNovel novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();

            if (novelToPlay == null)
            {
                return;
            }

            novelName = novelToPlay.title;
            novelID = novelToPlay.id;
            InitializeHeadline();

            rating = 1;
            star01.image.sprite = starFullSprite;
            star02.image.sprite = starEmptySprite;
            star03.image.sprite = starEmptySprite;
            star04.image.sprite = starEmptySprite;
            star05.image.sprite = starEmptySprite;
        }

        private void InitializeHeadline()
        {
            headline.text = "Gespielte Novel: " + novelName;
            instruction.text = "Bitte teile uns deine Meinung zu der eben gespielten Novel mit!";
        }

        private void OnSkipButton()
        {
            SceneLoader.LoadFeedbackScene();
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
            SceneLoader.LoadFeedbackScene();
        }

        private void SendReviewToServer()
        {
            AddNovelReviewServerCall call = Instantiate(addNovelReviewServerCallPrefab)
                .GetComponent<AddNovelReviewServerCall>();
            call.sceneController = this;
            call.OnSuccessHandler = this;
            call.novelId = novelID;
            call.novelName = novelName;
            call.rating = rating;
            call.reviewText = reviewText;
            call.SendRequest();
            DontDestroyOnLoad(call.gameObject);
        }

        private void OnStar01Button()
        {
            rating = 1;
            star01.image.sprite = starFullSprite;
            star02.image.sprite = starEmptySprite;
            star03.image.sprite = starEmptySprite;
            star04.image.sprite = starEmptySprite;
            star05.image.sprite = starEmptySprite;
        }

        private void OnStar02Button()
        {
            rating = 2;
            star01.image.sprite = starFullSprite;
            star02.image.sprite = starFullSprite;
            star03.image.sprite = starEmptySprite;
            star04.image.sprite = starEmptySprite;
            star05.image.sprite = starEmptySprite;
        }

        private void OnStar03Button()
        {
            rating = 3;
            star01.image.sprite = starFullSprite;
            star02.image.sprite = starFullSprite;
            star03.image.sprite = starFullSprite;
            star04.image.sprite = starEmptySprite;
            star05.image.sprite = starEmptySprite;
        }

        private void OnStar04Button()
        {
            rating = 4;
            star01.image.sprite = starFullSprite;
            star02.image.sprite = starFullSprite;
            star03.image.sprite = starFullSprite;
            star04.image.sprite = starFullSprite;
            star05.image.sprite = starEmptySprite;
        }

        private void OnStar05Button()
        {
            rating = 5;
            star01.image.sprite = starFullSprite;
            star02.image.sprite = starFullSprite;
            star03.image.sprite = starFullSprite;
            star04.image.sprite = starFullSprite;
            star05.image.sprite = starFullSprite;
        }

        public void OnSuccess(Response response)
        {
            // nothing to do here
        }
    }
}