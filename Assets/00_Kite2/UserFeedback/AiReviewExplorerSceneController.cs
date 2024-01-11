using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiReviewExplorerSceneController : SceneController
{
    // Start is called before the first frame update
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.AI_REVIEW_EXPLORER_SCENE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
