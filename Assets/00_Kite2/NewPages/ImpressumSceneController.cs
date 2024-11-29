using System.Collections;
using System.Collections.Generic;
using _00_Kite2.Common.Managers;
using UnityEngine;
using UnityEngine.UI;

public class ImpressumSceneController : SceneController
{
    [SerializeField] private RectTransform layout;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.IMPRESSUM_SCENE);
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
        FontSizeManager.Instance().UpdateAllTextComponents();
    }
}
