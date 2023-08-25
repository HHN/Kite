using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCommentSectionButton : MonoBehaviour
{
    public Button button;

    void Start()
    {
        button.onClick.AddListener(delegate { OnClick(); });
    }

    public void OnClick()
    {
        SceneLoader.LoadCommentSectionScene();
    }
}
