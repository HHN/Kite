using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundersBubbleSceneController : SceneController
{
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.FOUNDERS_BUBBLE_SCENE);
    }
}
