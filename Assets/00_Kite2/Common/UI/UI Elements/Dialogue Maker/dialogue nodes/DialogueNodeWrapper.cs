using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueNodeWrapper : MonoBehaviour
{
    [SerializeField] private RectTransform dialogueNodeWrapperRectTransform;
    [SerializeField] private RectTransform dialogueNodeRectTransform;
    [SerializeField] private RectTransform contextMenuRectTransform;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button addNodeButton;
    [SerializeField] private Button editNodeButton;
    [SerializeField] private Button connectNodeButton;
    [SerializeField] private bool isContextMenuVisible;
    [SerializeField] private GameObject contextMenu;

    void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(dialogueNodeWrapperRectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(dialogueNodeRectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(contextMenuRectTransform);

        optionsButton.onClick.AddListener(delegate { OnOptionsButton(); });
        addNodeButton.onClick.AddListener(delegate { OnAddNodeButton(); });
        editNodeButton.onClick.AddListener(delegate { OnEditNodeButton(); });
        connectNodeButton.onClick.AddListener(delegate { OnConnectNodesButton(); });
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
        Debug.Log("Add Node Button");
    }

    public void OnEditNodeButton()
    {
        Debug.Log("Edit Node Button");
    }

    public void OnConnectNodesButton()
    {
        Debug.Log("Connect Node Button");
    }
}
