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
    [SerializeField] private int tabIndex;
    [SerializeField] private RadioButtonHandler radioButtonHandler;
    [SerializeField] private TMP_InputField searchInputField;
    [SerializeField] private VisualNovelGallery gallery;
    [SerializeField] private GalleryType openGallery = GalleryType.KITE_GALLERY;
    [SerializeField] private float kiteGaleryPosition = 1;

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
        openGallery = GalleryType.KITE_GALLERY;
        StartCoroutine(gallery.EnsureCorrectScrollPosition(kiteGaleryPosition));
    }

    public override void OnStop()
    {
        base.OnStop();
        List<VisualNovel> visualNovels = KiteNovelManager.GetAllKiteNovels();
        gallery.RemoveAll();
        gallery.AddNovelsToGallery(visualNovels);
        searchInputField.onValueChanged.AddListener(delegate {SearchAfterValueChanged();});
        NovelExplorerSceneMemory memory = new NovelExplorerSceneMemory();
        memory.SetTabIndex(tabIndex);
        memory.SetSearchPhrase(searchInputField.text);
        gallery.GetCurrentScrollPosition();
        openGallery = GalleryType.KITE_GALLERY;
        StartCoroutine(gallery.EnsureCorrectScrollPosition(kiteGaleryPosition));
        memory.SetUserNovels(userNovels);
        SceneMemoryManager.Instance().SetMemoryOfNovelExplorerScene(memory);
    }

    public void SaveCurrentPosition()
    {
        switch (openGallery)
        {
            case GalleryType.KITE_GALLERY:
                {
                    kiteGaleryPosition = gallery.GetCurrentScrollPosition();
                    return;
                }
            default:
                {
                    return;
                }
        }
    }

    private void SearchAfterValueChanged()
    {
        string searchText = searchInputField.text.Trim();
        List<VisualNovel> dataset = new List<VisualNovel>();
        dataset = KiteNovelManager.GetAllKiteNovels();

        List<VisualNovel> results = FuzzySearch(dataset, searchText);
        gallery.RemoveAll();
        gallery.AddNovelsToGallery(results);
    }

    private List<VisualNovel> FuzzySearch(List<VisualNovel> dataset, string query)
    {
        List<VisualNovel> matches = new List<VisualNovel>();

        foreach (VisualNovel entry in dataset)
        {
            if (entry.title.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                matches.Add(entry);
            }
        }

        return matches;
    }
}
