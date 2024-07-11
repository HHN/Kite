using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class FreeTextInputController : MonoBehaviour
{
    [SerializeField] private GameObject wrapper;
    [SerializeField] private TextMeshProUGUI question;
    [SerializeField] private string questionString;
    [SerializeField] private string variablesName;
    [SerializeField] private TMP_InputField inputfield;
    [SerializeField] private Button skipButton;
    [SerializeField] private Button submitButton;
    [SerializeField] private PlayNovelSceneController controller;

    void Start()
    {
        skipButton.onClick.AddListener(delegate { OnSkipButton(); });
        submitButton.onClick.AddListener(delegate { OnSubmitButton(); });
        controller = GameObject.Find("Controller").GetComponent<PlayNovelSceneController>();
    }

    private void SetCarretVisible(int pos)
    {
        inputfield.caretPosition = pos; // desired cursor position

        inputfield.GetType().GetField("m_AllowInput", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(inputfield, true);
        inputfield.GetType().InvokeMember("SetCaretVisible", BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance, null, inputfield, null);

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

    public void OnSubmitButton()
    {
        string input = inputfield.text;
        input = input.Trim();
        VisualNovel novelToPlay = PlayManager.Instance().GetVisualNovelToPlay();

        if (novelToPlay == null) 
        {
            Debug.LogError("Unexpected Error!");
            return; 
        }
        if (novelToPlay.IsVariableExistend(variablesName))
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

    public void OnSkipButton()
    {
        wrapper.SetActive(false);
        Destroy(wrapper);
        this.controller.SetWaitingForConfirmation(true);
        this.controller.OnConfirm();
    }

    /*inputfield.GetType().InvokeMember("SetCaretVisible",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
                null, inputfield, null);*/
}
