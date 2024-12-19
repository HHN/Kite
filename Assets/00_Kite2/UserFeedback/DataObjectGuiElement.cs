using _00_Kite2.Common.UI.UI_Elements.DropDown;
using TMPro;
using UnityEngine;

namespace _00_Kite2.UserFeedback
{
    public class DataObjectGuiElement : MonoBehaviour
    {
        [SerializeField] private DropDownMenu dropdownContainer;
        [SerializeField] private DropDownMenu dropDownPrompt;
        [SerializeField] private DropDownMenu dropDownCompletion;
        [SerializeField] private TextMeshProUGUI headButtonText;
        [SerializeField] private TextMeshProUGUI promptText;
        [SerializeField] private TextMeshProUGUI completionText;
        [SerializeField] private DataObject dataObject;

        public void InitializeDataObject(DataObject dataObject)
        {
            this.dataObject = dataObject;
            headButtonText.text = "Prompt und Completion (ID:" + dataObject.GetId() + ")";
            promptText.text = dataObject.GetPrompt();
            completionText.text = dataObject.GetCompletion();
        }
    }
}