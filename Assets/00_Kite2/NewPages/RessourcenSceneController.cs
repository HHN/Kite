using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RessourcenSceneController : SceneController
{
    [SerializeField] private RectTransform layout;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.RESSOURCEN_SCENE);
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
    }
}
