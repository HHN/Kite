using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisualNovelRepresentation : MonoBehaviour
{
    private VisualNovel visualNovel;
    public Button button;
    public TextMeshProUGUI novelHeadline;
    public GameObject privateHint;
    public GameObject modifiedHint; // Modified from KITE II Team
    public GameObject kiteOneHint;  // Novel from Kite I

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
        else if ((visualNovel.id == (-2)) || (visualNovel.id == (-3)) || (visualNovel.id == (-4)) || (visualNovel.id == (-7)) || (visualNovel.id == (-6)))
        {
            modifiedHint.SetActive(true);
        }
        else if (visualNovel.id < 0)
        {
            kiteOneHint.SetActive(true);
        }
    }
}
