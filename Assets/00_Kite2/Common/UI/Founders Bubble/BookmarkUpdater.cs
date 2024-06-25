using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookmarkUpdater : MonoBehaviour
{
    [SerializeField] private VisualNovelNames visualNovel;

    void Update()
    {
        this.gameObject.GetComponent<Image>().enabled =FavoritesManager.Instance().IsFavorite(VisualNovelNamesHelper.ToInt(visualNovel));
    }
}
