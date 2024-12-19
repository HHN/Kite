using _00_Kite2.Common.UI.UI_Elements.DropDown;
using TMPro;
using UnityEngine;

namespace _00_Kite2.UserFeedback
{
    public class AiReviewGuiElement : MonoBehaviour
    {
        [SerializeField] private DropDownMenu dropdownContainer;
        [SerializeField] private DropDownMenu dropDownPrompt;
        [SerializeField] private DropDownMenu dropDownCompletion;
        [SerializeField] private DropDownMenu dropDownReviewText;
        [SerializeField] private TextMeshProUGUI headButtonText;
        [SerializeField] private TextMeshProUGUI nameOfNovel;
        [SerializeField] private TextMeshProUGUI promptText;
        [SerializeField] private TextMeshProUGUI completionText;
        [SerializeField] private TextMeshProUGUI reviewText;
        [SerializeField] private AiReview aiReview;

        public void InitializeReview(AiReview aiReview)
        {
            this.aiReview = aiReview;
            headButtonText.text = "Bewertung (ID:" + aiReview.GetId() + ")";
            nameOfNovel.text = "Gespielte Novel: " + aiReview.GetNovelName();
            reviewText.text = aiReview.GetReviewText();
            promptText.text = aiReview.GetPrompt();
            completionText.text = aiReview.GetAiFeedback();
        }

        public string GetNovelName()
        {
            string novelName = aiReview?.GetNovelName();

            if (novelName == null)
            {
                return "";
            }

            return novelName;
        }

        public void CloseAll()
        {
            dropdownContainer.SetMenuOpen(false);
            dropDownPrompt.SetMenuOpen(false);
            dropDownCompletion.SetMenuOpen(false);
            dropDownReviewText.SetMenuOpen(false);
        }
    }
}