using UnityEngine;

public class SelectNovelSceneController : SceneController, OnSuccessHandler
{
    public GameObject getNovelsServerCall;
    public NovelProvider novelProvider;

    void Start()
    {
        this.DisplayInfoMessage(InfoMessages.WAIT_FOR_LOAD_NOVEL);
        GetNovelsServerCall call = Instantiate(getNovelsServerCall).GetComponent<GetNovelsServerCall>();
        call.sceneController = this;
        call.onSuccessHandler = this;
        call.SendRequest();
    }

    public void OnSuccess(Response response)
    {
        if (response.novels != null && response.novels.Count != 0)
        {
            novelProvider.userNovels = response.novels;
        }
        this.messageObject.CloseMessageBox();
    }
}
