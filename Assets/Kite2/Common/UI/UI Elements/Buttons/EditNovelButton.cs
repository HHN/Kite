using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditNovelButton : MonoBehaviour
{
    public void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(delegate { OnClick(); });
        Init();
    }

    public void Init()
    {
        VisualNovel novel = PlayManager.Instance().GetVisualNovelToPlay();
        if (novel == null)
        {
            return;
        }
        this.gameObject.SetActive(novel.id == 0);
    }

    public void OnClick()
    {
    }
}
