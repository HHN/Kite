using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualNovelGallery : MonoBehaviour
{
    public GameObject visualNovelRepresentationPrefab;
    public GameObject content;
    public GameObject galleryRowPrefab;
    public GameObject placeHolder;
    public GameObject noNovelsInfo;
    private List<VisualNovel> novelsInGallery = new List<VisualNovel>();
    private GameObject currentGalleryRow = null;
    private GameObject currentPlaceHolder = null;
    private GameObject currentNoNovelsInfo = null;
    public Sprite[] smalNovelSprites;

    public void Awake()
    {
        currentGalleryRow = Instantiate(galleryRowPrefab, content.transform);
        currentNoNovelsInfo = Instantiate(noNovelsInfo, currentGalleryRow.transform);
        currentPlaceHolder = Instantiate(placeHolder, currentGalleryRow.transform);
        novelsInGallery = new List<VisualNovel>();
    }

    public IEnumerator EnsureCorrectScrollPosition(float value)
    {
        yield return null;
        Canvas.ForceUpdateCanvases();
        GetComponent<ScrollRect>().verticalNormalizedPosition = value;
    }

    public void AddNovelToGallery(VisualNovel novel)
    {
        if (novel == null || novelsInGallery.Contains(novel))
        {
            return;
        }
        GameObject novelRepresentation;

        if (novelsInGallery.Count == 0)
        {
            Destroy(currentNoNovelsInfo);
            Destroy(currentPlaceHolder);
            novelRepresentation = Instantiate(visualNovelRepresentationPrefab, currentGalleryRow.transform);
            currentPlaceHolder = Instantiate(placeHolder, currentGalleryRow.transform);
        }
        else if ((novelsInGallery.Count % 2) == 0)
        {
            currentGalleryRow = Instantiate(galleryRowPrefab, content.transform);
            novelRepresentation = Instantiate(visualNovelRepresentationPrefab, currentGalleryRow.transform);
            currentPlaceHolder = Instantiate(placeHolder, currentGalleryRow.transform);
        } 
        else
        {
            Destroy(currentPlaceHolder);
            novelRepresentation = Instantiate(visualNovelRepresentationPrefab, currentGalleryRow.transform);
        }
        novelsInGallery.Add(novel);
        VisualNovelRepresentation representation = novelRepresentation.GetComponent<VisualNovelRepresentation>();
        representation.SetVisaulNovel(novel);
        representation.SetHeadline(novel.title);
        representation.SetButtonImage(FindSmalSpriteById(novel.image));
    }

    public void AddNovelsToGallery(List<VisualNovel> novels)
    {
        foreach (VisualNovel novel in novels)
        {
            AddNovelToGallery(novel);
        }
    }

    public void RemoveNovelFromGallery(VisualNovel novel)
    {
        if (novel == null || !novelsInGallery.Contains(novel))
        {
            return;
        }
        List<VisualNovel> currentNovels = novelsInGallery;
        currentNovels.Remove(novel);
        RemoveAll();
        AddNovelsToGallery(currentNovels);
    }

    public void RemoveNovelsFromGallery(List<VisualNovel> novels)
    {
        foreach (VisualNovel novel in novels)
        {
            RemoveNovelFromGallery(novel);
        }
    }

    public void RemoveAll()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        currentGalleryRow = Instantiate(galleryRowPrefab, content.transform);
        currentNoNovelsInfo = Instantiate(noNovelsInfo, currentGalleryRow.transform);
        currentPlaceHolder = Instantiate(placeHolder, currentGalleryRow.transform);
        novelsInGallery = new List<VisualNovel>();
    }

    private Sprite FindSmalSpriteById(long id)
    {
        if (smalNovelSprites.Length <= id)
        {
            return null;
        }
        return smalNovelSprites[id];
    }

    public float GetCurrentScrollPosition()
    {
        return GetComponent<ScrollRect>().verticalNormalizedPosition;
    }
}