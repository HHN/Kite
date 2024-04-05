using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPrefsSceneController : SceneController
{

    [SerializeField] private Transform listViewParent;
    [SerializeField] private Button playerNameConfirmButton;
    [SerializeField] private Button companyNameConfirmButton;
    [SerializeField] private Button elevatorPitchConfirmButton;
    [SerializeField] private Button preverencesConfirmButton;
    [SerializeField] private Button playerNameCancleButton;
    [SerializeField] private Button companyNameCancleButton;
    [SerializeField] private Button elevatorPitchCancleButton;
    [SerializeField] private Button preverencesCancleButton;
    [SerializeField] private TMP_InputField playerNameInputField; 
    [SerializeField] private TMP_InputField companyNameInputField;
    [SerializeField] private TMP_InputField elevatorPitchInputField;
    [SerializeField] private TMP_InputField preverencesInputField;
    [SerializeField] private TMP_InputField preverencesAnswerInputField;
    
    private string originalTextPlayerName; 
    private string originalTextCompanyName; 
    private string originalTextElevatorPitch; 
    private string originalTextPreverences; 

    private void Start()
    {
        BackStackManager.Instance().Push(SceneNames.PLAYER_PREFS_SCENE);
        AddButtonListener();
        LoadPlayerPrefs();
    }

    private void AddButtonListener()
    {
        playerNameConfirmButton.onClick.AddListener(delegate { playerNameConfirmButtonListener(); });
        companyNameConfirmButton.onClick.AddListener(delegate { companyNameConfirmButtonListener(); });
        elevatorPitchConfirmButton.onClick.AddListener(delegate { elevatorPitchConfirmButtonListener(); });
        preverencesConfirmButton.onClick.AddListener(delegate { preverencesConfirmButtonListener(); });
        playerNameCancleButton.onClick.AddListener(delegate { playerNameCancleButtonListener(); });
        companyNameCancleButton.onClick.AddListener(delegate { companyNameCancleButtonListener(); });
        elevatorPitchCancleButton.onClick.AddListener(delegate { elevatorPitchCancleButtonListener(); });
        preverencesCancleButton.onClick.AddListener(delegate { preverencesCancleButtonListener(); });
        playerNameInputField.onValueChanged.AddListener(delegate { OnInputFieldChangedPlayerName(); });
        companyNameInputField.onValueChanged.AddListener(delegate { OnInputFieldChangedCompanyName(); });
        elevatorPitchInputField.onValueChanged.AddListener(delegate { OnInputFieldChangedElevatorPitch(); });
        preverencesInputField.onValueChanged.AddListener(delegate { OnInputFieldChangedPreverences(); });
    }

    private void playerNameConfirmButtonListener()
    {
        PlayerDataManager.Instance().SavePlayerData("PlayerName", playerNameInputField.text);
        DisplayInfoMessage(InfoMessages.SAVED_SUCCESFULLY);
    }
    private void companyNameConfirmButtonListener()
    {
        PlayerDataManager.Instance().SavePlayerData("CompanyName", companyNameInputField.text);
        DisplayInfoMessage(InfoMessages.SAVED_SUCCESFULLY);
    }
    private void elevatorPitchConfirmButtonListener()
    {
        PlayerDataManager.Instance().SavePlayerData("ElevatorPitch", elevatorPitchInputField.text);
        DisplayInfoMessage(InfoMessages.SAVED_SUCCESFULLY);
    }
    private void preverencesConfirmButtonListener()
    {
        PlayerDataManager.Instance().SavePlayerData("Preverences", preverencesInputField.text);
        DisplayInfoMessage(InfoMessages.SAVED_SUCCESFULLY);
    }
    private void playerNameCancleButtonListener()
    {
        playerNameInputField.text = PlayerDataManager.Instance().ReadPlayerData("PlayerName");
    }
    private void companyNameCancleButtonListener()
    {
        companyNameInputField.text = PlayerDataManager.Instance().ReadPlayerData("CompanyName");
    }
    private void elevatorPitchCancleButtonListener()
    {
        elevatorPitchInputField.text = PlayerDataManager.Instance().ReadPlayerData("ElevatorPitch");
    }
    private void preverencesCancleButtonListener()
    {
        preverencesInputField.text = PlayerDataManager.Instance().ReadPlayerData("Preverences");
    }

    void OnInputFieldChangedPlayerName()
    {
        bool textHasChanged = playerNameInputField.text != originalTextPlayerName;

        playerNameConfirmButton.interactable = textHasChanged;
        playerNameCancleButton.interactable = textHasChanged;
    }

    void OnInputFieldChangedCompanyName()
    {
        bool textHasChanged = companyNameInputField.text != originalTextCompanyName;

        companyNameConfirmButton.interactable = textHasChanged;
        companyNameCancleButton.interactable = textHasChanged;
    }

    void OnInputFieldChangedElevatorPitch()
    {
        bool textHasChanged = elevatorPitchInputField.text != originalTextElevatorPitch;

        elevatorPitchConfirmButton.interactable = textHasChanged;
        elevatorPitchCancleButton.interactable = textHasChanged;
    }

    void OnInputFieldChangedPreverences()
    {
        bool textHasChanged = preverencesInputField.text != originalTextPreverences;

        preverencesConfirmButton.interactable = textHasChanged;
        preverencesCancleButton.interactable = textHasChanged;
    }

    private void LoadPlayerPrefs()
    {
        originalTextPlayerName = PlayerDataManager.Instance().ReadPlayerData("PlayerName");
        originalTextCompanyName = PlayerDataManager.Instance().ReadPlayerData("CompanyName"); 
        originalTextElevatorPitch = PlayerDataManager.Instance().ReadPlayerData("ElevatorPitch"); 
        originalTextPreverences = PlayerDataManager.Instance().ReadPlayerData("Preverences"); 
        playerNameInputField.text = originalTextPlayerName;
        companyNameInputField.text = originalTextCompanyName;
        elevatorPitchInputField.text = originalTextElevatorPitch;
        preverencesInputField.text = originalTextPreverences;
        preverencesAnswerInputField.text = PlayerDataManager.Instance().ReadPlayerData("GPTAnswerForPreverences");
    }
}
