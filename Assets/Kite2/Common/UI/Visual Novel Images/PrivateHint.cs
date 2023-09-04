using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivateHint : MonoBehaviour
{
    public GameObject privateHint;

    void Start()
    {
        VisualNovel visualNovel = PlayManager.Instance().GetVisualNovelToPlay();
        privateHint.SetActive(visualNovel.id == 0);
    }
}
