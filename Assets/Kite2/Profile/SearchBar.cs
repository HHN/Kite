using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SearchBar : MonoBehaviour
{
    public Button searchButton;
    public TMP_InputField inputField;
    public VisualNovelGallery gallery;
    public List<VisualNovel> dataSet;

    private void Start()
    {
        searchButton.onClick.AddListener(delegate { OnSearchButton(); });
    }

    public void OnSearchButton()
    {
        string searchText = inputField.text.Trim();

        if (string.IsNullOrEmpty(searchText))
        {
            gallery.Reload();
            return;
        }

        dataSet = gallery.GetVisualNovels();
        if (dataSet.Count == 0)
        {
            return;
        }

        List<VisualNovel> results = FuzzySearch(dataSet, searchText, 2);

        gallery.ShowNovels(results);
    }

    public int LevenshteinDistance(string a, string b)
    {
        int m = a.Length;
        int n = b.Length;
        int[,] dp = new int[m + 1, n + 1];

        // Initialize first row and column.
        for (int i = 0; i <= m; i++)
        {
            dp[i, 0] = i;
        }
        for (int j = 0; j <= n; j++)
        {
            dp[0, j] = j;
        }

        // fill matrix
        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                if (a[i - 1] == b[j - 1])
                {
                    dp[i, j] = dp[i - 1, j - 1];
                }
                else
                {
                    dp[i, j] = Math.Min(dp[i - 1, j - 1], Math.Min(dp[i - 1, j], dp[i, j - 1])) + 1;
                }
            }
        }

        return dp[m, n];
    }

    public List<VisualNovel> FuzzySearch(List<VisualNovel> dataset, string query, int threshold)
    {
        List<VisualNovel> matches = new List<VisualNovel>();

        foreach (VisualNovel entry in dataset)
        {
            int distance = LevenshteinDistance(query, entry.title);
            if (distance <= threshold)
            {
                matches.Add(entry);
            } else
            {
                long id;
                if (long.TryParse(query, out id))
                {
                    if (id == entry.id)
                    {
                        matches.Add(entry);
                    }
                }
            }
        }

        return matches;
    }

}