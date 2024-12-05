using _00_Kite2.Common;
using _00_Kite2.Common.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayerPrefsController : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private PlayerPrefsSceneController controller;

    [SerializeField] private TextMeshProUGUI headlineObject;
    [SerializeField] private TextMeshProUGUI questionObject;

    [SerializeField] private Button cancleButton;
    [SerializeField] private Button confirmButton;

    [SerializeField] private TMP_InputField inputfield;
    [SerializeField] private string playerPrefsKey;

    void Start()
    {
        cancleButton.onClick.AddListener(delegate { OnCancleButton(); });
        confirmButton.onClick.AddListener(delegate { OnConfirmButton(); });
    }

    public void Initialize(string playerPrefsKey, string currentValue, string headline, string question, PlayerPrefsSceneController controller)
    {
        this.headlineObject.text = headline;
        this.questionObject.text = question;
        this.playerPrefsKey = playerPrefsKey;
        this.inputfield.text = currentValue;
        this.controller = controller;
    }

    public void OnCancleButton()
    {
        root.SetActive(false);
        Destroy(root);
    }

    public void OnConfirmButton()
    {
        PlayerDataManager.Instance().SavePlayerData(playerPrefsKey, inputfield.text.Trim());
        controller.DisplayInfoMessage(InfoMessages.SAVED_SUCCESFULLY);
        controller.InitializeValues();
        root.SetActive(false);
        Destroy(root);
    }
}
