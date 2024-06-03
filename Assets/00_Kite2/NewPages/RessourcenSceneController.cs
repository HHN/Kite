using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourcenSceneController : SceneController
{
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.RESSOURCEN_SCENE);
    }
}
