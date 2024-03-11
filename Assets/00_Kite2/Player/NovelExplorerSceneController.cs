using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class NovelExplorerSceneController : SceneController
{
    [SerializeField] private GameObject getNovelsServerCall;
    [SerializeField] private List<VisualNovel> userNovels;
    [SerializeField] private Dictionary<long, VisualNovel> userNovelsMap = new Dictionary<long, VisualNovel>();
    [SerializeField] private TMP_InputField searchInputField;
    [SerializeField] private VisualNovelGallery gallery;
    [SerializeField] private Button clearSearchButton;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.NOVEL_EXPLORER_SCENE);
        userNovels = GeneratedNovelManager.Instance().GetUserNovels();
        searchInputField.onValueChanged.AddListener(delegate {SearchAfterValueChanged();});
        clearSearchButton.onClick.AddListener(delegate {ClearSearch();});
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
        if (memory == null)
        {
            return;
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
        results.AddRange(IdSearch(dataset, searchText));
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

    private List<VisualNovel> IdSearch(List<VisualNovel> dataset, string id)
    {
        List<VisualNovel> matches = new List<VisualNovel>();

        foreach (VisualNovel entry in dataset)
        {
            float floatValue;
            if(float.TryParse(id, out floatValue))
            {
                if (entry.id == floatValue)
                    {
                        matches.Add(entry);
                    }
            }
        }

        return matches;
    }

    private void ClearSearch()
    {
        searchInputField.text = "";
    }
}
