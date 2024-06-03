using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatenschutzSceneController : SceneController
{
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.DATENSCHUTZ_SCENE);
    }
}
