using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

// Analytics
using Unity.Services.Core;
using Unity.Services.Analytics;
using System.Diagnostics;

public class NovelExplorerSceneController : SceneController, OnSuccessHandler
{
    public GameObject getNovelsServerCall;
    public List<VisualNovel> userNovels = GeneratedNovelManager.Instance().GetUserNovels();
    public Dictionary<long, VisualNovel> userNovelsMap = new Dictionary<long, VisualNovel>();
    public int tabIndex;
    public ExplorerButtons explorerButtons;
    public RadioButtonHandler radioButtonHandler;
    public TMP_InputField searchInputField;
    public VisualNovelGallery gallery;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.NOVEL_EXPLORER_SCENE);
        InitMemory();
    }

    public void OnSuccess(Response response)
    {
        if (response.novels != null && response.novels.Count != 0)
        {
            userNovels = response.novels;
            MapNovels(userNovels);
        }
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

    public void InitMemory()
    {
        NovelExplorerSceneMemory memory = SceneMemoryManager.Instance().GetMemoryOfNovelExplorerScene();
        if (memory == null)
        {
            explorerButtons.OnKiteNovelsButton();

            return; // Server Call is not performed, because its a GET-call and has a body. Since iOS 13 GET-calls are not allowed to have a body.

            //GetNovelsServerCall call = Instantiate(getNovelsServerCall).GetComponent<GetNovelsServerCall>();
            //call.sceneController = this;
            //call.onSuccessHandler = this;
            //call.SendRequest();
            //DontDestroyOnLoad(call.gameObject);
            //return;
        }

        userNovels = memory.GetUserNovels();
        searchInputField.text = memory.GetSearchPhrase();
        radioButtonHandler.SetIndex(memory.GetRadioButtonIndex());
        explorerButtons.kiteGaleryPosition = memory.GetScrollPositionOfKiteGallery();
        explorerButtons.userGaleryPosition = memory.GetScrollPositionOfUserGallery();
        explorerButtons.accountGaleryPosition = memory.GetScrollPositionOfAccountGallery();
        explorerButtons.favoritesGaleryPosition = memory.GetScrollPositionOfFavoritesGallery();
        explorerButtons.filterGaleryPosition = memory.GetScrollPositionOfFilterGallery();
        explorerButtons.ActivateTabByIndex(memory.GetTabIndex());
    }

    public override void OnStop()
    {
        base.OnStop();
        
        NovelExplorerSceneMemory memory = new NovelExplorerSceneMemory();
        memory.SetTabIndex(tabIndex);
        memory.SetSearchPhrase(searchInputField.text);
        explorerButtons.SaveCurrentPosition();
        memory.SetScrollPositionOfKiteGallery(explorerButtons.kiteGaleryPosition);
        memory.SetScrollPositionOfUserGallery(explorerButtons.userGaleryPosition);
        memory.SetScrollPositionOfAccountGallery(explorerButtons.accountGaleryPosition);
        memory.SetScrollPositionOfFavoritesGallery(explorerButtons.favoritesGaleryPosition);
        memory.SetScrollPositionOfFilterGallery(explorerButtons.filterGaleryPosition);
        memory.SetRadioButtonIndex(radioButtonHandler.GetIndex());
        memory.SetUserNovels(userNovels);
        SceneMemoryManager.Instance().SetMemoryOfNovelExplorerScene(memory);
    }
}
