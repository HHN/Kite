using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviewNovelSceneController : SceneController
{
    [SerializeField] private Button skipButton;

    // Start is called before the first frame update
    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.REVIEW_NOVEL_SCENE);
        skipButton.onClick.AddListener(delegate { OnSkipButton(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSkipButton()
    {
        SceneLoader.LoadFeedbackScene();
    }
}
