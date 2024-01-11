using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewAiSceneController : SceneController
{
    // Start is called before the first frame update
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.REVIEW_AI_SCENE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
