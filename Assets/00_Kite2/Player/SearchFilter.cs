using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SearchFilter : MonoBehaviour
{
    public Button searchButton;
    public TMP_InputField inputField;
    public VisualNovelGallery gallery;
    public RadioButtonHandler radioButtonHandler;
    public NovelExplorerSceneController sceneController;
    public GameObject filterView;

    private void Start()
    {
        inputField.onValueChanged.AddListener(delegate {SearchAfterValueChanged();});
    }

    public void SearchAfterValueChanged()
    {
        string searchText = inputField.text.Trim();
        List<VisualNovel> dataset = new List<VisualNovel>();
        dataset = KiteNovelManager.Instance().GetAllKiteNovels();

        List<VisualNovel> results = FuzzySearch(dataset, searchText);
        gallery.RemoveAll();
        gallery.AddNovelsToGallery(results);
        //filterView.SetActive(false);
    }

    public List<VisualNovel> FuzzySearch(List<VisualNovel> dataset, string query)
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
