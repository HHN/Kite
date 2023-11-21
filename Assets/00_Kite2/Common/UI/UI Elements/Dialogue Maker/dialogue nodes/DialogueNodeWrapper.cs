using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueNodeWrapper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nodeName;
    [SerializeField] private RectTransform dialogueNodeWrapperRectTransform;
    [SerializeField] private RectTransform dialogueNodeRectTransform;
    [SerializeField] private RectTransform contextMenuRectTransform;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button addNodeButton;
    [SerializeField] private Button editNodeButton;
    [SerializeField] private Button connectNodeButton;
    [SerializeField] private bool isContextMenuVisible;
    [SerializeField] private GameObject contextMenu;
    [SerializeField] private DialogueNode dialogueNode;

    [SerializeField] private Image nodeImage;
    [SerializeField] private Sprite changeLocationSprite;
    [SerializeField] private Sprite charactercomesOrGoesSprite;
    [SerializeField] private Sprite characterTalkesSprite;

    void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(dialogueNodeWrapperRectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(dialogueNodeRectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(contextMenuRectTransform);

        optionsButton.onClick.AddListener(delegate { OnOptionsButton(); });
        addNodeButton.onClick.AddListener(delegate { OnAddNodeButton(); });
        editNodeButton.onClick.AddListener(delegate { OnEditNodeButton(); });
        connectNodeButton.onClick.AddListener(delegate { OnConnectNodesButton(); });

        dialogueNode = new DialogueNode();
    }

    public void OnOptionsButton()
    {
        if (isContextMenuVisible)
        {
            isContextMenuVisible = false;
            contextMenu.SetActive(false);
            return;
        }
        isContextMenuVisible = true;
        contextMenu.SetActive(true);
    }

    public void OnAddNodeButton()
    {
        isContextMenuVisible = false;
        contextMenu.SetActive(false);
    }

    public void OnEditNodeButton()
    {
        isContextMenuVisible = false;
        contextMenu.SetActive(false);
        NovelMakerSceneController controller = GameObject.Find("Controller").GetComponent<NovelMakerSceneController>();
        controller.OpenDetailsPanelForNode(dialogueNode, this);
    }

    public void OnConnectNodesButton()
    {
        isContextMenuVisible = false;
        contextMenu.SetActive(false);
    }

    public void SetDialogueNode(DialogueNode node)
    {
        this.dialogueNode = node;
    }

    public void OnFinishEdit()
    {
        nodeName.text = dialogueNode.GetNodeName();

        switch (dialogueNode.GetNodeType())
        {
            case DialogueNodeType.NONE:
                {
                    nodeImage.sprite = null; 
                    nodeImage.gameObject.SetActive(false);
                    return;
                }
            case DialogueNodeType.CHANGE_LOCATION:
                {
                    nodeImage.sprite = changeLocationSprite;
                    nodeImage.gameObject.SetActive(true);
                    return;
                }
            case DialogueNodeType.CHARACTER_COMES_OR_GOES:
                {
                    nodeImage.sprite = charactercomesOrGoesSprite;
                    nodeImage.gameObject.SetActive(true); 
                    return;
                }
            case DialogueNodeType.CHARACTER_SPEAKS:
                {
                    nodeImage.sprite = characterTalkesSprite;
                    nodeImage.gameObject.SetActive(true); 
                    return;
                }
            default: return;
        }
    }
}
