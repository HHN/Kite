using Assets._Scripts.Managers;
using Assets._Scripts.UI_Elements.FreeTextUserInput;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.SceneControllers
{
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

        [SerializeField] private GameObject changePlayerPrefPrefab;
        [SerializeField] private GameObject root;

        [SerializeField] private string userName;
        [SerializeField] private string firmenName;
        [SerializeField] private string elevatorPitch;
        [SerializeField] private string interessen;
        [SerializeField] private string vorgeschlageneInhalte;

        private void Start()
        {
            InitializeValues();
            InitializeButtons();
        }

        public void InitializeValues()
        {
            userName = PlayerDataManager.Instance().GetPlayerData("PlayerName");
            firmenName = PlayerDataManager.Instance().GetPlayerData("CompanyName");
            elevatorPitch = PlayerDataManager.Instance().GetPlayerData("ElevatorPitch");
            interessen = PlayerDataManager.Instance().GetPlayerData("Preferences");
            vorgeschlageneInhalte = PlayerDataManager.Instance().GetPlayerData("GPTAnswerForPreferences");

            if (string.IsNullOrEmpty(userName))
            {
                userName = "Es wurde noch kein Name gespeichert.";
            }

            if (string.IsNullOrEmpty(firmenName))
            {
                firmenName = "Es wurde noch kein Firmenname gespeichert.";
            }

            if (string.IsNullOrEmpty(elevatorPitch))
            {
                elevatorPitch = "Es wurde noch kein Elevator Pitch gespeichert.";
            }

            if (string.IsNullOrEmpty(interessen))
            {
                interessen = "Es wurden noch keine Interessen gespeichert.";
            }

            if (string.IsNullOrEmpty(vorgeschlageneInhalte))
            {
                vorgeschlageneInhalte = "Es wurden noch keine Vorschläge ermittelt.";
            }

            nameTextObject.text = userName;
            firmenNameTextObject.text = firmenName;
            elevatorPitchTextObject.text = elevatorPitch;
            interessenTextObject.text = interessen;
            vorgeschlageneInhalteTextObject.text = vorgeschlageneInhalte;
        }

        private void InitializeButtons()
        {
            editNameButton.onClick.AddListener(OnEditNameButton);
            editFirmenNameButton.onClick.AddListener(OnEditFirmenNameButton);
            editElevatorPitchButton.onClick.AddListener(OnEditElevatorPitchButton);
            editInteressenButton.onClick.AddListener(OnEditInteressenButton);
        }

        private void OnEditNameButton()
        {
            ChangePlayerPrefsController changePlayerPrefsController =
                Object.Instantiate(this.changePlayerPrefPrefab, root.transform)
                    .GetComponent<ChangePlayerPrefsController>();
            changePlayerPrefsController.Initialize("PlayerName", userName, "Namen ändern",
                "Wie möchtest du genannt werden?", this);
        }

        private void OnEditFirmenNameButton()
        {
            ChangePlayerPrefsController changePlayerPrefsController =
                Object.Instantiate(this.changePlayerPrefPrefab, root.transform)
                    .GetComponent<ChangePlayerPrefsController>();
            changePlayerPrefsController.Initialize("CompanyName", firmenName, "Firmennamen ändern",
                "Wie möchtest du deine Firma nennen?", this);
        }

        private void OnEditElevatorPitchButton()
        {
            ChangePlayerPrefsController changePlayerPrefsController =
                Object.Instantiate(this.changePlayerPrefPrefab, root.transform)
                    .GetComponent<ChangePlayerPrefsController>();
            changePlayerPrefsController.Initialize("ElevatorPitch", elevatorPitch, "Elevator Pitch ändern",
                "Was für ein Unternehmen führst du?", this);
        }

        private void OnEditInteressenButton()
        {
            ChangePlayerPrefsController changePlayerPrefsController =
                Object.Instantiate(this.changePlayerPrefPrefab, root.transform)
                    .GetComponent<ChangePlayerPrefsController>();
            changePlayerPrefsController.Initialize("Preferences", interessen, "Interessen ändern",
                "Was sind deine Interessen?", this);
        }
    }
}