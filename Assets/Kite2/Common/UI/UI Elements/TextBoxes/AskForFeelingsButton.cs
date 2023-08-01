using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskForFeelingsButton : MonoBehaviour
{
    public PlayNovelSceneController controller;

    private void Start()
    {
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 131);
    }

    public void OnButtonClick()
    {
        this.gameObject.SetActive(false);
        controller.SetFeelingsPanelActive(true);
    }
}
