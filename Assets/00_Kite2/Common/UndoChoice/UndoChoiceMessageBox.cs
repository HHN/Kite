using _00_Kite2.Common.Managers;
using _00_Kite2.Player;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UndoChoiceMessageBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageBoxBody;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button confirmButton;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject backgroundLeave;
    [SerializeField] private GameObject textStay;
    [SerializeField] private GameObject person;

    private Color novelColor;
    private PlayNovelSceneController playNovelSceneController;

    void Start()
    {
        playNovelSceneController = FindAnyObjectByType<PlayNovelSceneController>();

        cancelButton.onClick.AddListener(OnCancelButton);
        confirmButton.onClick.AddListener(OnConfirmButton);

        InitUI();
        FontSizeManager.Instance().UpdateAllTextComponents();
    }

    private void InitUI()
    {
        // Retrieve the color once and assign it to all necessary elements
        novelColor = NovelColorManager.Instance().GetColor();

        ApplyColorToUI(background, novelColor);
        ApplyColorToUI(backgroundLeave, novelColor);
        textStay.GetComponent<TextMeshProUGUI>().color = novelColor;
    }

    // Method for applying color to reduce redundancy
    private void ApplyColorToUI(GameObject uiElement, Color color)
    {
        if (uiElement != null && uiElement.TryGetComponent<Image>(out Image image))
        {
            image.color = color;
        }
    }

    public void SetBody(string headline)
    {
        messageBoxBody.text = headline;
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    public void OnCancelButton()
    {
        this.CloseMessageBox();
    }

    public void OnConfirmButton()
    {
        CloseMessageBox();
        playNovelSceneController.RestoreChoice();
    }

    public void CloseMessageBox()
    {
        gameObject.SetActive(false); // Only deactivate instead of destroying
    }
}
