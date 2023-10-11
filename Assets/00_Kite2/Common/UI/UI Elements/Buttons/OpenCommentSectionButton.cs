using UnityEngine;
using UnityEngine.UI;

public class OpenCommentSectionButton : MonoBehaviour
{
    public Button button;

    void Start()
    {
        return;
        //button.onClick.AddListener(delegate { OnClick(); });
        //Init();
    }

    public void Init()
    {
        VisualNovel novel = PlayManager.Instance().GetVisualNovelToPlay();
        if (novel == null)
        {
            return;
        }
        this.gameObject.SetActive(novel.id != 0);
    }

    public void OnClick()
    {
        SceneLoader.LoadCommentSectionScene();
    }
}
