using System.Reflection;
using Assets._Scripts.Common.Managers;
using Assets._Scripts.Common.Novel;
using Assets._Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Common.UI_Elements.FreeTextUserInput
{
    public class FreeTextInputController : MonoBehaviour
    {
        [SerializeField] private GameObject wrapper;
        [SerializeField] private TextMeshProUGUI question;
        [SerializeField] private string questionString;
        [SerializeField] private string variablesName;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button skipButton;
        [SerializeField] private Button submitButton;
        [SerializeField] private PlayNovelSceneController controller;

        private void Start()
        {
            skipButton.onClick.AddListener(OnSkipButton);
            submitButton.onClick.AddListener(OnSubmitButton);
            controller = GameObject.Find("Controller").GetComponent<PlayNovelSceneController>();
        }

        private void SetCarretVisible(int pos)
        {
            inputField.caretPosition = pos; // desired cursor position

            inputField.GetType().GetField("m_AllowInput", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(inputField, true);
            inputField.GetType().InvokeMember("SetCaretVisible",
                BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance, null, inputField, null);
        }

        public void Initialize(string question, string variablesName)
        {
            SetQuestion(question);
            SetVariablesName(variablesName);
        }

        private void SetQuestion(string question)
        {
            this.questionString = question;
            this.question.text = questionString;
        }

        private void SetVariablesName(string variablesName)
        {
            Debug.Log(variablesName);
            this.variablesName = variablesName;
        }

        private void OnSubmitButton()
        {
            string input = inputField.text;
            input = input.Trim();
            VisualNovel novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();

            if (novelToPlay == null)
            {
                Debug.LogError("Unexpected Error!");
                return;
            }

            if (novelToPlay.IsVariableExistent(variablesName))
            {
                novelToPlay.RemoveGlobalVariable(variablesName);
            }

            novelToPlay.AddGlobalVariable(variablesName, input);
            novelToPlay.SetGlobalVariable(variablesName, input);

            wrapper.SetActive(false);
            Destroy(wrapper);
            this.controller.SetWaitingForConfirmation(true);
            this.controller.OnConfirm();
        }

        private void OnSkipButton()
        {
            wrapper.SetActive(false);
            Destroy(wrapper);
            this.controller.SetWaitingForConfirmation(true);
            this.controller.OnConfirm();
        }

        /*inputField.GetType().InvokeMember("SetCaretVisible",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
                null, inputField, null);*/
    }
}