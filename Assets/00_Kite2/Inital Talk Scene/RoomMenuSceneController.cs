using UnityEngine;
using UnityEngine.UI;
public class RoomMenuSceneController : SceneController
{
    [SerializeField] private Button leftChairButton1;
    [SerializeField] private Button leftChairButton2;
    [SerializeField] private Button bookButton;
    [SerializeField] private Button flowerPotButton;
    [SerializeField] private Button waterCarafeButton;
    [SerializeField] private Button novelPlayerButton;
    [SerializeField] private Button settingsButton;

    void Start()
    {
        
        BackStackManager.Instance().Clear();
        SceneMemoryManager.Instance().ClearMemory();
        leftChairButton1.onClick.AddListener(delegate {OnNovelPlayerButton();});
        leftChairButton2.onClick.AddListener(delegate {OnNovelPlayerButton();});
        bookButton.onClick.AddListener(delegate {OnSettingsButton();});
    }

    public void OnNovelPlayerButton()
    {
        SceneLoader.LoadNovelExplorerScene();
    }

    public void OnSettingsButton()
    {
        SceneLoader.LoadSettingsScene();
    }
}
