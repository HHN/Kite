using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NutzungsbedingungenSceneController : SceneController
{
    [SerializeField] private RectTransform layout;
    [SerializeField] private RectTransform layout02;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.NUTZUNGSBEDINGUNGEN_SCENE);
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout02);
        FontSizeManager.Instance().UpdateAllTextComponents();
    }
}
