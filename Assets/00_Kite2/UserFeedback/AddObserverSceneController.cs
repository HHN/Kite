using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObserverSceneController : SceneController
{
    // Start is called before the first frame update
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.ADD_OBSERVER_SCENE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
