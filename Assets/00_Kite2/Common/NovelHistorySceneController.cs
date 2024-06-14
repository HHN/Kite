using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NovelHistorySceneController : SceneController
{
    [SerializeField] private GameObject dataObjectPrefab;
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject noDataObjectsHint;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.NOVEL_HISTORY_SCENE);

        List<DialogHistoryEntry> entries = DialogHistoryManager.Instance().GetEntries();

        if (entries == null)
        {
            return;
        }

        if (entries.Count == 0 )
        {
            return;
        }

        noDataObjectsHint.SetActive(false);

        foreach (DialogHistoryEntry dataObject in entries)
        {
            NovelHistoryEntryGuiElement dataObjectGuiElement =
                Instantiate(dataObjectPrefab, container.transform)
                .GetComponent<NovelHistoryEntryGuiElement>();

            dataObjectGuiElement.InitializeEntry(dataObject);
        }
        RectTransform rectTransform = container.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }
}
