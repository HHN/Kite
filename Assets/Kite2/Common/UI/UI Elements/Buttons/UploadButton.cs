using UnityEngine;
using UnityEngine.UI;

public class UploadButton : MonoBehaviour
{
    private VisualNovel novel;

    public void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(delegate { OnClick(); });
        Init();
    }

    public void Init()
    {
        novel = PlayManager.Instance().GetVisualNovelToPlay();
        if (novel == null ) 
        { 
            return;  
        }
        if (novel.id != 0)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
            this.gameObject.GetComponent<Image>().enabled = false;
            return;
        }
        else
        {
            this.gameObject.GetComponent<Button>().interactable = true;
            this.gameObject.GetComponent<Image>().enabled = true;
        }
    }

    public void OnClick()
    {
    }
}
