using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisualNovelRepresentation : MonoBehaviour
{
    public VisualNovel visualNovel;
    public Button button;
    public TextMeshProUGUI novelHeadline;

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
}
