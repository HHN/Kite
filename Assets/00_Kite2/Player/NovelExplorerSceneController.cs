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
    [SerializeField] private GameObject getNovelsServerCall;
    [SerializeField] private List<VisualNovel> userNovels;
    [SerializeField] private Dictionary<long, VisualNovel> userNovelsMap = new Dictionary<long, VisualNovel>();
    [SerializeField] private TMP_InputField searchInputField;
    [SerializeField] private VisualNovelGallery gallery;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.NOVEL_EXPLORER_SCENE);
        userNovels = GeneratedNovelManager.Instance().GetUserNovels();
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
        List<VisualNovel> visualNovels = KiteNovelManager.GetAllKiteNovels();
        gallery.RemoveAll();
        gallery.AddNovelsToGallery(visualNovels);
        searchInputField.onValueChanged.AddListener(delegate {SearchAfterValueChanged();});
        if (memory == null)
        {
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
        StartCoroutine(gallery.EnsureCorrectScrollPosition(memory.GetScrollPositionOfKiteGallery()));
    }

    public override void OnStop()
    {
        base.OnStop();
        NovelExplorerSceneMemory memory = new NovelExplorerSceneMemory();
        memory.SetSearchPhrase(searchInputField.text);
        memory.SetScrollPositionOfKiteGallery(gallery.GetCurrentScrollPosition());
        memory.SetUserNovels(userNovels);
        SceneMemoryManager.Instance().SetMemoryOfNovelExplorerScene(memory);
    }

    private void SearchAfterValueChanged()
    {
        string searchText = searchInputField.text.Trim();
        List<VisualNovel> dataset = new List<VisualNovel>();
        dataset = KiteNovelManager.GetAllKiteNovels();
        List<VisualNovel> results = SubStringSearch(dataset, searchText);
        gallery.RemoveAll();
        gallery.AddNovelsToGallery(results);
    }

    private List<VisualNovel> SubStringSearch(List<VisualNovel> dataset, string subString)
    {
        List<VisualNovel> matches = new List<VisualNovel>();

        foreach (VisualNovel entry in dataset)
        {
            if (entry.title.IndexOf(subString, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                matches.Add(entry);
            }
        }

        return matches;
    }
}
