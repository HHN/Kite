using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundersWellSceneController : SceneController
{
    // Start is called before the first frame update
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.FOUNDERS_WELL_SCENE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
