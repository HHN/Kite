using UnityEngine.UI;
using UnityEngine;

public class NovelMakerSceneController : SceneController
{
    [SerializeField] private Button helpButton;
    [SerializeField] private Button characterExplorerButton;
    [SerializeField] private Button environmentExplorerButton;
    [SerializeField] private Button previewNovelButton;
    [SerializeField] private Button finishNovelButton;
    [SerializeField] private Animator detailsPanelAnimator;
    [SerializeField] private RectTransform detailsPanel;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.NOVEL_MAKER_SCENE);

        helpButton.onClick.AddListener(delegate { OnHelpButton(); });
        characterExplorerButton.onClick.AddListener(delegate { OnCharacterExplorerButton(); });
        environmentExplorerButton.onClick.AddListener(delegate { OnEnvironmentExplorerButton(); });
        previewNovelButton.onClick.AddListener(delegate { OnPreviewButton(); });
        finishNovelButton.onClick.AddListener(delegate { OnFinishNovelButton(); });

        LayoutRebuilder.ForceRebuildLayoutImmediate(detailsPanel);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            OpenDetailsPanel();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CloseDetailsPanel();
        }
    }

    public void OnHelpButton()
    {
        SceneLoader.LoadHelpForNovelMakerScene();
    }

    public void OnCharacterExplorerButton()
    {
        SceneLoader.LoadCharacterExplorerScene();
    }

    public void OnPreviewButton()
    {
        SceneLoader.LoadNovelPreviewScene();
    }

    public void OnEnvironmentExplorerButton()
    {
        SceneLoader.LoadEnvironmentExplorerScene();
    }

    public void OnFinishNovelButton()
    {
        SceneLoader.LoadFinishNovelScene();
    }

    public void OpenDetailsPanel()
    {
        detailsPanelAnimator.SetBool("isOpen", true);
    }

    public void CloseDetailsPanel()
    {
        detailsPanelAnimator.SetBool("isOpen", false);
    }
}
