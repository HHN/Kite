using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualNovelGallery : MonoBehaviour
{
    public GameObject visualNovelRepresentationPrefab;
    public GameObject content;
    public GameObject galleryRowPrefab;
    public NovelProvider novelProvider;
    public bool isKiteNovelGallery = false;
    public bool isUserNovelGallery = false;
    public bool isAccountNovelGallery = false;

    private List<VisualNovel> novelsInGallery;

    private GameObject galleryRow = null;

    private void Start()
    {
        List<VisualNovel> novels = new List<VisualNovel>();
        if (isKiteNovelGallery)
        {
            novels = novelProvider.GetKiteNovels();
        } 
        else if (isUserNovelGallery)
        {
            novels = novelProvider.GetUserNovels();
        } 
        else if (isAccountNovelGallery)
        {
            novels = novelProvider.GetAccountNovels();
        }
        this.novelsInGallery = novels;
        ShowNovels(novels);
    }

    public void ShowNovels(List<VisualNovel> novels)
    {
        CleanUp();

        for (int i = 0; i < novels.Count; i++)
        {
            VisualNovel novelToAdd = novels[i];
            AddNovelToGallery(novelToAdd);
        }
    }

    public void AddNovelToGallery(VisualNovel novel)
    {
        if (novel == null)
        {
            return;
        }
        bool rowFullAfterAddingNovel = true;

        if (galleryRow == null)
        {
            galleryRow = Instantiate(galleryRowPrefab, content.transform);
            rowFullAfterAddingNovel = false;
        }
        GameObject novelRepresentation = Instantiate(visualNovelRepresentationPrefab, galleryRow.transform);

        if (rowFullAfterAddingNovel)
        {
            galleryRow = null;
        }

        VisualNovelRepresentation representation = novelRepresentation.GetComponent<VisualNovelRepresentation>();
        representation.visualNovel = novel;
        representation.SetHeadline(novel.title);
        Sprite sprite = novelProvider.FindSmalSpriteById(novel.image);
        representation.SetButtonImage(sprite);
        representation.novelProvider = novelProvider;
    }

    private void CleanUp()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        galleryRow = null;
    }

    public List<VisualNovel> GetVisualNovels()
    {
        return novelsInGallery;
    }

    public void Reload()
    {
        this.ShowNovels(novelsInGallery);
    }
}
