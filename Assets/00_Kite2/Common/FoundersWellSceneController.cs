
using UnityEngine;

public class FoundersWellSceneController : SceneController
{
    [SerializeField] private RadialLayoutGroup radialLayoutGroup;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.FOUNDERS_WELL_SCENE);
        radialLayoutGroup.InitializeRadius();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            radialLayoutGroup.InitializeRadius();
        }
    }
}
