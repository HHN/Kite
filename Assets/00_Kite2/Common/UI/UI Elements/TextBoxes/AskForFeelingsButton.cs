using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskForFeelingsButton : MonoBehaviour
{
    public PlayNovelSceneController controller;

    private void Start()
    {
        GetComponent<Animator>().SetBool("AskForOpinionPulsation", true);
    }

    public void OnButtonClick()
    {
        this.gameObject.SetActive(false);
        controller.confirmArea.gameObject.SetActive(true);
        controller.SetFeelingsPanelActive(true);
    }
}
