using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlreadyPlayerUpdater : MonoBehaviour
{
    [SerializeField] private VisualNovelNames visualNovel;
    [SerializeField] private TextMeshProUGUI number;

    void Update()
    {
        int numberOfPlays = PlayRecordManager.Instance().GetNumberOfPlaysForNovel(visualNovel);
        this.gameObject.GetComponent<Image>().enabled = (PlayRecordManager.Instance().GetNumberOfPlaysForNovel(visualNovel) > 0);

        number.gameObject.SetActive(PlayRecordManager.Instance().GetNumberOfPlaysForNovel(visualNovel) > 0);
        number.text = numberOfPlays.ToString();
    }
}
