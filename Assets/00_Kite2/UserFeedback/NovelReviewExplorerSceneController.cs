using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovelReviewExplorerSceneController : SceneController
{
    // Start is called before the first frame update
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.NOVEL_REVIEW_EXPLORER_SCENE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
