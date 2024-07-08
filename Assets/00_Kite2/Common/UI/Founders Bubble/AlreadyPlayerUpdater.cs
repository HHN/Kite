using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlreadyPlayerUpdater : MonoBehaviour
{
    [SerializeField] private VisualNovelNames visualNovel;

    void Update()
    {
        this.gameObject.GetComponent<Image>().enabled = (PlayRecordManager.Instance().GetNumberOfPlaysForNovel(visualNovel) > 0);
    }
}
