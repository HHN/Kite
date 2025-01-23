using _00_Kite2.Common.Managers;
using _00_Kite2.Common.Messages;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Common.UI.UI_Elements.Free_Text_User_Input
{
    public class ChangePlayerPrefsController : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private PlayerPrefsSceneController controller;

        [SerializeField] private TextMeshProUGUI headlineObject;
        [SerializeField] private TextMeshProUGUI questionObject;

        [SerializeField] private Button cancelButton;
        [SerializeField] private Button confirmButton;

        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private string playerPrefsKey;

        private void Start()
        {
            cancelButton.onClick.AddListener(OnCancelButton);
            confirmButton.onClick.AddListener(OnConfirmButton);
        }

        public void Initialize(string key, string currentValue, string headline, string question,
            PlayerPrefsSceneController playerPrefsSceneController)
        {
            this.headlineObject.text = headline;
            this.questionObject.text = question;
            this.playerPrefsKey = key;
            this.inputField.text = currentValue;
            this.controller = playerPrefsSceneController;
        }

        private void OnCancelButton()
        {
            root.SetActive(false);
            Destroy(root);
        }

        private void OnConfirmButton()
        {
            PlayerDataManager.Instance().SavePlayerData(playerPrefsKey, inputField.text.Trim());
            controller.DisplayInfoMessage(InfoMessages.SAVED_SUCCESFULLY);
            controller.InitializeValues();
            root.SetActive(false);
            Destroy(root);
        }
    }
}