using TMPro;
using UnityEngine;

public class NovelHistoryEntryGuiElement : MonoBehaviour
{
    [SerializeField] private DropDownMenu dropdownContainer;
    [SerializeField] private DropDownMenu dropDownDialog;
    [SerializeField] private DropDownMenu dropDownAiFeedback;
    [SerializeField] private TextMeshProUGUI headButtonText;
    [SerializeField] private TextMeshProUGUI nameOfNovel;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private TextMeshProUGUI aiFeedbackText;
    [SerializeField] private DialogHistoryEntry dialogHistoryEntry;

    public void InitializeEntry(DialogHistoryEntry dialogHistoryEntry)
    {
        this.dialogHistoryEntry = dialogHistoryEntry;
        headButtonText.text = "Gespielte Novel";
        nameOfNovel.text = "Gespielte Novel: " + dialogHistoryEntry.GetNovelId();
        dialogText.text = dialogHistoryEntry.GetDialog();
        aiFeedbackText.text = dialogHistoryEntry.GetCompletion();
    }

    public void CloseAll()
    {
        dropdownContainer.SetMenuOpen(false);
        dropDownDialog.SetMenuOpen(false);
        dropDownAiFeedback.SetMenuOpen(false);
    }
}
