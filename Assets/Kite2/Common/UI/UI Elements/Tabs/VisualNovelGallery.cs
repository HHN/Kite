using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualNovelGallery : MonoBehaviour
{
    public GameObject visualNovelRepresentationPrefab;
    public GameObject content;
    public GameObject galleryRowPrefab;
    public SelectNovelSceneController sceneController;
    public bool isKiteNovelGallery = false;
    public bool isUserNovelGallery = false;

    private GameObject galleryRow = null;

    private void Start()
    {
        List<VisualNovel> novels;
        if (isKiteNovelGallery)
        {
            novels = sceneController.GetKiteNovels();
        } else
        {
            //novels = sceneController.GetUserNovels();
            novels = sceneController.GetKiteNovels();
        }
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
        Sprite sprite = sceneController.FindSmalSpriteById(novel.image);
        representation.SetButtonImage(sprite);
        representation.sceneController = sceneController;
    }

    private void CleanUp()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
