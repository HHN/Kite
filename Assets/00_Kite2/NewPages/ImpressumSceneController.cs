using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpressumSceneController : SceneController
{
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.IMPRESSUM_SCENE);
    }
}
