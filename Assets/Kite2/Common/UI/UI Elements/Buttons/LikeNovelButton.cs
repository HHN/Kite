using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LikeNovelButton : MonoBehaviour, OnSuccessHandler
{
    public Sprite[] sprites;
    private VisualNovel novel;
    public bool isLiked;
    public GameObject likeNovelServerCallPrefab;
    public GameObject unlikeNovelServerCallPrefab;
    public GameObject getNovelLikeServerCallPrefab;
    public Button button;
    public DetailsViewSceneController sceneController;
    public TextMeshProUGUI count;

    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(delegate { OnClick(); });
        Init();
    }

    public void Init()
    {
        novel = PlayManager.Instance().GetVisualNovelToPlay();
        if (novel == null)
        {
            return;
        }
        if (novel.id == 0)
        {
            this.gameObject.SetActive(false);
            return;
        }
        if (GameManager.Instance().applicationMode != ApplicationModes.LOGGED_IN_USER_MODE)
        {
            this.GetComponent<Button>().interactable = false;
        }
        this.gameObject.GetComponent<Button>().image.sprite = sprites[0];
        isLiked = false;
        RequestInformation();
    }

    public void RequestInformation()
    {
        sceneController.DisplayInfoMessage(InfoMessages.WAIT_FOR_LIKE_NOVEL);
        GetNovelLikeInformationServerCall call = Instantiate(getNovelLikeServerCallPrefab)
            .GetComponent<GetNovelLikeInformationServerCall>();
        call.sceneController = sceneController;
        call.onSuccessHandler = this;
        call.id = novel.id;
        call.SendRequest();
    }

    public void LikeNovel()
    {
        this.gameObject.GetComponent<Button>().image.sprite = sprites[1];
        isLiked = true;

    }

    public void UnlikeNovel()
    {
        this.gameObject.GetComponent<Button>().image.sprite = sprites[0];
        isLiked = false;

    }

    public void SendLikeRequest()
    {
        sceneController.DisplayInfoMessage(InfoMessages.WAIT_FOR_LIKE_NOVEL);
        LikeNovelServerCall call = Instantiate(likeNovelServerCallPrefab).GetComponent<LikeNovelServerCall>();
        call.sceneController = sceneController;
        call.onSuccessHandler = this;
        call.id = novel.id;
        call.SendRequest();
    }

    public void SendUnlikeRequest()
    {
        sceneController.DisplayInfoMessage(InfoMessages.WAIT_FOR_UNLIKE_NOVEL);
        UnlikeNovelServerCall call = Instantiate(unlikeNovelServerCallPrefab).GetComponent<UnlikeNovelServerCall>();
        call.sceneController = sceneController;
        call.onSuccessHandler = this;
        call.id = novel.id;
        call.SendRequest();
    }

    public void OnSuccess(Response response)
    {
        sceneController.messageObject.CloseMessageBox();
        count.text = response.numberOfNovelLikes.ToString();

        if (response.novelLikedByUser) 
        {
            LikeNovel();
        } 
        else
        {
            UnlikeNovel();
        }
    }

    public void OnClick()
    {
        if (isLiked)
        {
            SendUnlikeRequest();
        }
        else
        {
            SendLikeRequest();
        }
    }


}
