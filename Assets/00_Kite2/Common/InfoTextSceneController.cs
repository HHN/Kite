using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoTextSceneController : SceneController
{
    [SerializeField] private TextMeshProUGUI textHead;
    [SerializeField] private TextMeshProUGUI textBody;

    // Start is called before the first frame update
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.INFO_TEXT_SCENE);
        textHead.text = InfoTextManager.Instance.GetTextHead();
        textBody.text = InfoTextManager.Instance.GetTextBody();
    }
}
