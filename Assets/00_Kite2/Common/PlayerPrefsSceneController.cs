using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPrefsSceneController : SceneController
{
    [SerializeField] private TextMeshProUGUI nameTextObject;
    [SerializeField] private TextMeshProUGUI firmenNameTextObject;
    [SerializeField] private TextMeshProUGUI elevatorPitchTextObject;
    [SerializeField] private TextMeshProUGUI interessenTextObject;
    [SerializeField] private TextMeshProUGUI vorgeschlageneInhalteTextObject;

    [SerializeField] private Button editNameButton;
    [SerializeField] private Button editFirmenNameButton;
    [SerializeField] private Button editElevatorPitchButton;
    [SerializeField] private Button editInteressenButton;

    [SerializeField] private BigTextPreViewController namePreviewController;
    [SerializeField] private BigTextPreViewController firmenNamePreviewController;
    [SerializeField] private BigTextPreViewController elevatorPitchPreviewController;
    [SerializeField] private BigTextPreViewController interessenPreviewController;
    [SerializeField] private BigTextPreViewController vorgeschlageneInhaltePreviewController;

    [SerializeField] private GameObject changePlayerPrefPrefab;
    [SerializeField] private GameObject root;

    [SerializeField] private string userName;
    [SerializeField] private string firmenName;
    [SerializeField] private string elevatorPitch;
    [SerializeField] private string interessen;
    [SerializeField] private string vorgeschlageneInhalte;

    void Start()
    {
        InitializeValues();
        InitializeButtons();
    }

    public void InitializeValues()
    {
        userName = PlayerDataManager.Instance().GetPlayerData("PlayerName");
        firmenName = PlayerDataManager.Instance().GetPlayerData("CompanyName");
        elevatorPitch = PlayerDataManager.Instance().GetPlayerData("ElevatorPitch");
        interessen = PlayerDataManager.Instance().GetPlayerData("Preverences");
        vorgeschlageneInhalte = PlayerDataManager.Instance().GetPlayerData("GPTAnswerForPreverences");

        if (string.IsNullOrEmpty(userName)) { userName = "Es wurde noch kein Name gespeichert."; }
        if (string.IsNullOrEmpty(firmenName)) { firmenName = "Es wurde noch kein Firmenname gespeichert."; }
        if (string.IsNullOrEmpty(elevatorPitch)) { elevatorPitch = "Es wurde noch kein Elevatorpitch gespeichert."; }
        if (string.IsNullOrEmpty(interessen)) { interessen = "Es wurden noch keine Interessen gespeichert."; }
        if (string.IsNullOrEmpty(vorgeschlageneInhalte)) { vorgeschlageneInhalte = "Es wurden noch keine Vorschläge ermittelt"; }

        nameTextObject.text = userName;
        firmenNameTextObject.text = firmenName;
        elevatorPitchTextObject.text = elevatorPitch;
        interessenTextObject.text = interessen;
        vorgeschlageneInhalteTextObject.text = vorgeschlageneInhalte;

        namePreviewController.OnValueEntered();
        firmenNamePreviewController.OnValueEntered();
        elevatorPitchPreviewController.OnValueEntered();
        interessenPreviewController.OnValueEntered();
        vorgeschlageneInhaltePreviewController.OnValueEntered();
    }

    public void InitializeButtons()
    {
        editNameButton.onClick.AddListener(delegate { OnEditNameButton(); });
        editFirmenNameButton.onClick.AddListener(delegate { OnEditFirmenNameButton(); });
        editElevatorPitchButton.onClick.AddListener(delegate { OnEditElevatorPitchButton(); });
        editInteressenButton.onClick.AddListener(delegate { OnEditInteressenButton(); });
    }

    public void OnEditNameButton()
    {
        ChangePlayerPrefsController changePlayerPrefsController = Instantiate(this.changePlayerPrefPrefab, root.transform)
        .GetComponent<ChangePlayerPrefsController>();
        changePlayerPrefsController.Initialize("PlayerName", userName, "Namen ändern", "Wie möchtest du genannt werden?", this);
    }

    public void OnEditFirmenNameButton()
    {
        ChangePlayerPrefsController changePlayerPrefsController = Instantiate(this.changePlayerPrefPrefab, root.transform)
        .GetComponent<ChangePlayerPrefsController>();
        changePlayerPrefsController.Initialize("CompanyName", firmenName, "Firmennamen ändern", "Wie möchtest du deine Firma nennen?", this);
    }

    public void OnEditElevatorPitchButton()
    {
        ChangePlayerPrefsController changePlayerPrefsController = Instantiate(this.changePlayerPrefPrefab, root.transform)
        .GetComponent<ChangePlayerPrefsController>();
        changePlayerPrefsController.Initialize("ElevatorPitch", elevatorPitch, "Elevatorpitch ändern", "Was für ein Unternehmen führst du?", this);
    }

    public void OnEditInteressenButton()
    {
        ChangePlayerPrefsController changePlayerPrefsController = Instantiate(this.changePlayerPrefPrefab, root.transform)
        .GetComponent<ChangePlayerPrefsController>();
        changePlayerPrefsController.Initialize("Preverences", interessen, "Interessen ändern", "Was sind deine Interessen?", this);
    }
}
