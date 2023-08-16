using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisualNovelRepresentation : MonoBehaviour
{
    private VisualNovel visualNovel;
    public Button button;
    public TextMeshProUGUI novelHeadline;
    public GameObject privateHint;

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
        PlayManager.Instance().SetVisualNovelToPlay(visualNovel);
        SceneLoader.LoadDetailsViewScene();
    }

    public void SetVisaulNovel(VisualNovel visualNovel)
    {
        this.visualNovel = visualNovel;
        if (visualNovel.id == 0)
        {
            privateHint.SetActive(true);
        }
    }
}
