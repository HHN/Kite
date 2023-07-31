using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualNovelGallery : MonoBehaviour
{
    public GameObject visualNovelRepresentationPrefab;
    public GameObject content;
    public SelectNovelSceneController sceneController;
    public bool isKiteNovelGallery = false;
    public bool isUserNovelGallery = false;

    private void Start()
    {
        List<VisualNovel> novels;
        if (isKiteNovelGallery)
        {
            novels = sceneController.GetKiteNovels();
        } else
        {
            novels = sceneController.GetUserNovels();
        }
        ShowNovels(novels);
    }

    public void ShowNovels(List<VisualNovel> novels)
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }

        if ((novels.Count % 2) == 0)
        {
            content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, novels.Count / 2 * 600);
        }
        else
        {
            content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ((novels.Count / 2) + 1) * 600);
        }

        for (int i = 0; i < novels.Count; i++)
        {
            GameObject novelRepresentation = Instantiate(visualNovelRepresentationPrefab, content.transform);

            float x;
            float y;

            if (i % 2 == 0)
            {
                x = 295;
            }
            else
            {
                x = 785;
            }
            int o = i / 2;
            y = -300 - (o * 600);
            novelRepresentation.transform.localPosition = new Vector2(x, y);

            VisualNovelRepresentation representation = novelRepresentation.GetComponent<VisualNovelRepresentation>();
            representation.visualNovel = novels[i];
            representation.SetHeadline(novels[i].title);
            Sprite sprite = sceneController.FindSmalSpriteById(novels[i].image);
            representation.SetButtonImage(sprite);
            representation.sceneController = sceneController;
        }
    }
}
