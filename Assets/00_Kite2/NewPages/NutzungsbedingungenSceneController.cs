using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NutzungsbedingungenSceneController : SceneController
{
    [SerializeField] private RectTransform layout;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.NUTZUNGSBEDINGUNGEN_SCENE);
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
    }
}
