using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierefreiheitSceneController : SceneController
{
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.BARRIEREFREIHEIT_SCENE);
    }
}
