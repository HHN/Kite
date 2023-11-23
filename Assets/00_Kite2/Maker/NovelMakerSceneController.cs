using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class NovelMakerSceneController : SceneController
{
    [SerializeField] private Button helpButton;
    [SerializeField] private Button characterExplorerButton;
    [SerializeField] private Button environmentExplorerButton;
    [SerializeField] private Button previewNovelButton;
    [SerializeField] private Button finishNovelButton;
    [SerializeField] private Animator detailsPanelAnimator;
    [SerializeField] private DetailsPanelController detailsPanelController;
    [SerializeField] private RectTransform detailsPanel;
    [SerializeField] private ScrollRect scollView;
    [SerializeField] private GameObject dialogueNodePrefab;
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private GameObject nodesContainer;
    [SerializeField] private GameObject linesContainer;

    [SerializeField] private List<DialogueNodeWrapper> allNodes;
    [SerializeField] private List<LineController> allLines;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.NOVEL_MAKER_SCENE);

        helpButton.onClick.AddListener(delegate { OnHelpButton(); });
        characterExplorerButton.onClick.AddListener(delegate { OnCharacterExplorerButton(); });
        environmentExplorerButton.onClick.AddListener(delegate { OnEnvironmentExplorerButton(); });
        previewNovelButton.onClick.AddListener(delegate { OnPreviewButton(); });
        finishNovelButton.onClick.AddListener(delegate { OnFinishNovelButton(); });
        allNodes = new List<DialogueNodeWrapper>();
        allLines = new List<LineController>();

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
        detailsPanelController.OpenDetailsPanel();
    }

    public void OpenDetailsPanelForNode(DialogueNode node, DialogueNodeWrapper wrapper)
    {
        detailsPanelController.Initialize(node, wrapper);
        detailsPanelController.OpenDetailsPanel();
    }

    public void CloseDetailsPanel()
    {
        detailsPanelController.CloseDetailsPanel();
    }

    public void DeactivateScrollView()
    {
        scollView.enabled = false;
    }

    public void ActivateScrollView()
    {
        scollView.enabled = true;
    }

    public void AddNewDialogueNode(Vector2 position, DialogueNodeWrapper originalNode = null)
    {
        GameObject dialogueNode = Instantiate(dialogueNodePrefab, nodesContainer.transform);
        dialogueNode.transform.localPosition = position;
        allNodes.Add(dialogueNode.GetComponent<DialogueNodeWrapper>());

        if (originalNode == null) { return; }

        AddNewLine(originalNode.transform, dialogueNode.transform, originalNode.GetDialogueNode().GetId(), 
            dialogueNode.GetComponent<DialogueNodeWrapper>().GetDialogueNode().GetId());
    }

    public void AddNewLine(Transform positionOne, Transform positionTwo, int from, int to)
    {
        GameObject line = Instantiate(linePrefab, linesContainer.transform);
        LineController lineController = line.GetComponent<LineController>();
        allLines.Add(lineController);
        lineController.SetUpLine(new Transform[] {positionOne, positionTwo}, from, to);
    }
}
