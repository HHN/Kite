using _00_Kite2.Common.UI.UI_Elements.DropDown;
using _00_Kite2.ExpertFeedback;
using TMPro;
using UnityEngine;

namespace _00_Kite2.UserFeedback
{
    public class ExpertFeedbackGuiElementController : MonoBehaviour
    {
        [SerializeField] private DropDownMenu dropdownContainer;
        [SerializeField] private DropDownMenu dropDownDialog;
        [SerializeField] private DropDownMenu dropDownAiFeedback;
        [SerializeField] private DropDownMenu dropDownQuestion;
        [SerializeField] private DropDownMenu dropDownAnswer;
        [SerializeField] private TextMeshProUGUI headButtonText;
        [SerializeField] private TextMeshProUGUI nameOfNovel;
        [SerializeField] private TextMeshProUGUI dialogText;
        [SerializeField] private TextMeshProUGUI aiFeedbackText;
        [SerializeField] private TextMeshProUGUI questionText;
        [SerializeField] private TextMeshProUGUI answerText;
        [SerializeField] private ExpertFeedbackQuestion expertFeedbackQuestion;

        public void InitializeReview(ExpertFeedbackQuestion expertFeedbackQuestion)
        {
            this.expertFeedbackQuestion = expertFeedbackQuestion;
            headButtonText.text = "Experten-Frage (ID:" + expertFeedbackQuestion.GetId() + ")";
            nameOfNovel.text = "Gespielte Novel: " + expertFeedbackQuestion.GetNovelName();
            dialogText.text = expertFeedbackQuestion.GetDialogue();
            aiFeedbackText.text = expertFeedbackQuestion.GetAiFeedback();
            questionText.text = expertFeedbackQuestion.GetExpertFeedbackQuestion();
            answerText.text = expertFeedbackQuestion.GetExpertFeedbackAnswer()?.GetExpertFeedbackAnswer();

            if (string.IsNullOrEmpty(answerText.text))
            {
                answerText.text = "Antwort noch nicht verfügbar! Versuche es bitte später noch einmal.";
            }
        }

        public string GetNovelName()
        {
            string novelName = expertFeedbackQuestion?.GetNovelName();

            if (novelName == null)
            {
                return "";
            }

            return novelName;
        }

        public void CloseAll()
        {
            dropdownContainer.SetMenuOpen(false);
            dropDownDialog.SetMenuOpen(false);
            dropDownAiFeedback.SetMenuOpen(false);
            dropDownQuestion.SetMenuOpen(false);
            dropDownAnswer.SetMenuOpen(false);
        }
    }
}