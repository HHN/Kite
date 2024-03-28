using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPrefsSceneController : MonoBehaviour
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
    }

    private void playerNameConfirmButtonListener()
    {
        PlayerDataManager.Instance().SavePlayerData("PlayerName", playerNameInputField.text);
    }
    private void companyNameConfirmButtonListener()
    {
        PlayerDataManager.Instance().SavePlayerData("CompanyName", companyNameInputField.text);
    }
    private void elevatorPitchConfirmButtonListener()
    {
        PlayerDataManager.Instance().SavePlayerData("ElevatorPitch", elevatorPitchInputField.text);
    }
    private void preverencesConfirmButtonListener()
    {
        PlayerDataManager.Instance().SavePlayerData("Preverences", preverencesInputField.text);
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

    private void LoadPlayerPrefs()
    {
        playerNameInputField.text = PlayerDataManager.Instance().ReadPlayerData("PlayerName");
        companyNameInputField.text = PlayerDataManager.Instance().ReadPlayerData("CompanyName");
        elevatorPitchInputField.text = PlayerDataManager.Instance().ReadPlayerData("ElevatorPitch");
        preverencesInputField.text = PlayerDataManager.Instance().ReadPlayerData("Preverences");
        preverencesAnswerInputField.text = PlayerDataManager.Instance().ReadPlayerData("GPTAnswerForPreverences");
    }
}
