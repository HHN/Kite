using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisualNovelRepresentation : MonoBehaviour
{
    public VisualNovel visualNovel;
    public Button button;
    public TextMeshProUGUI novelHeadline;
    public SelectNovelSceneController sceneController;

    void Start()
    {
        button.onClick.AddListener(delegate { OnClick(); });
    }

    public void SetHeadline(string headline)
    {
        novelHeadline.text = headline;
    }

    public void SetButtonImage(Sprite sprite)
    {
        button.image.sprite = sprite;
    }

    public void OnClick()
    {
        sceneController.ShowDetailsViewWithNovel(visualNovel);
    }
}
