using UnityEngine;
using UnityEngine.UI;

public class UploadButton : MonoBehaviour
{
    public void Start()
    {
        return;
        //this.gameObject.GetComponent<Button>().onClick.AddListener(delegate { OnClick(); });
        //Init();
    }

    public void Init()
    {
        VisualNovel novel = PlayManager.Instance().GetVisualNovelToPlay();
        if (novel == null)
        {
            return;
        }
        this.gameObject.SetActive(novel.id == 0);
        this.gameObject.GetComponent<Button>().interactable = (GameManager.Instance().applicationMode == ApplicationModes.LOGGED_IN_USER_MODE);
    }

    public void OnClick()
    {
    }
}
