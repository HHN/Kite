using System.Collections;
using System.Collections.Generic;
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
