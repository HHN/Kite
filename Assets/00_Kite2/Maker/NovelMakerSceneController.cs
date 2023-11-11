using UnityEngine.UI;
using UnityEngine;

public class NovelMakerSceneController : SceneController
{
    [SerializeField] private Button infoButton;
    [SerializeField] private Animator detailsPanelAnimator;
    [SerializeField] private RectTransform detailsPanel;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.NOVEL_MAKER_SCENE);

        infoButton.onClick.AddListener(delegate { OnInfoButton(); });

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

    public void OnInfoButton()
    {
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
