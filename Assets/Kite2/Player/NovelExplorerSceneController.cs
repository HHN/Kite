using System.Collections.Generic;
using UnityEngine;

public class NovelExplorerSceneController : SceneController, OnSuccessHandler
{
    public GameObject getNovelsServerCall;
    public List<VisualNovel> userNovels = new List<VisualNovel>();
    public Dictionary<long, VisualNovel> userNovelsMap = new Dictionary<long, VisualNovel>();

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.NOVEL_EXPLORER_SCENE);

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
            userNovels = response.novels;
            MapNovels(userNovels);
        }
        this.messageObject.CloseMessageBox();
    }

    private void MapNovels(List<VisualNovel> novels)
    {
        foreach (VisualNovel novel in novels)
        {
            userNovelsMap[novel.id] = novel;
        }
    }

    public VisualNovel GetUserNovelById(long id)
    {
        if (userNovelsMap.ContainsKey(id))
        {
            return userNovelsMap[id];
        }
        return null;
    }
}
