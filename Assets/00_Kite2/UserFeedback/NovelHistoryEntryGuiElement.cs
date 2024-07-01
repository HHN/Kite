using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NovelHistoryEntryGuiElement : MonoBehaviour
{
    [SerializeField] private DropDownMenu dropdownContainer;
    [SerializeField] private DropDownMenu dropDownDialog;
    [SerializeField] private DropDownMenu dropDownAiFeedback;
    [SerializeField] private TextMeshProUGUI headButtonText;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private TextMeshProUGUI aiFeedbackText;
    [SerializeField] private DialogHistoryEntry dialogHistoryEntry;

    public void InitializeEntry(DialogHistoryEntry dialogHistoryEntry)
    {
        this.dialogHistoryEntry = dialogHistoryEntry;
        headButtonText.text = dialogHistoryEntry.GetDateAndTime();
        dialogText.text = dialogHistoryEntry.GetDialog();
        aiFeedbackText.text = dialogHistoryEntry.GetCompletion();
    }

    public void CloseAll()
    {
        dropdownContainer.SetMenuOpen(false);
        dropDownDialog.SetMenuOpen(false);
        dropDownAiFeedback.SetMenuOpen(false);
    }

    public void AddLayoutToUpdateOnChange(RectTransform rectTransform)
    {
        dropdownContainer.AddLayoutToUpdateOnChange(rectTransform);
        dropDownDialog.AddLayoutToUpdateOnChange(rectTransform);
        dropDownAiFeedback.AddLayoutToUpdateOnChange(rectTransform);
    }

    public void SetNovelHistorySceneController(NovelHistorySceneController novelHistorySceneController)
    {
        dropdownContainer.SetNovelHistorySceneController(novelHistorySceneController);
        dropDownDialog.SetNovelHistorySceneController(novelHistorySceneController);
        dropDownAiFeedback.SetNovelHistorySceneController(novelHistorySceneController);
    }

    public void RebuildLayout()
    {
        dropdownContainer.RebuildLayout();
        dropDownDialog.RebuildLayout();
        dropDownAiFeedback.RebuildLayout();

        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
