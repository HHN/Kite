using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutzungsbedingungenSceneController : SceneController
{
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.NUTZUNGSBEDINGUNGEN_SCENE);
    }
}
