using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviewAiSceneController : SceneController
{
    [SerializeField] private Button skipButton;

    // Start is called before the first frame update
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.REVIEW_AI_SCENE);
        skipButton.onClick.AddListener(delegate { OnSkipButton(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSkipButton()
    {
        BackStackManager.Instance().Clear(); // we go back to the explorer and don't want
                                             // the back-button to bring us to the feedback scene aggain
        SceneLoader.LoadNovelExplorerScene();
    }
}
