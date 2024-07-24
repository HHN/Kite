using UnityEngine;
using UnityEngine.UI;

public class NovelArchiveDropDownMenu : MonoBehaviour
{
    [SerializeField] private GameObject dialogButton;
    [SerializeField] private GameObject dialogText;
    [SerializeField] private GameObject feedbackButton;
    [SerializeField] private GameObject feedbackText;
    [SerializeField] private SimpleDropDownMenu dialogDropDownMenu;
    [SerializeField] private SimpleDropDownMenu feedbackDropDownMenu;
    [SerializeField] private Button menuButton;
    [SerializeField] private Image indicatorImage;
    [SerializeField] private Sprite spriteWhileOpen;
    [SerializeField] private Sprite spriteWhileClosed;
    [SerializeField] private bool isOpen;

    void Start()
    {
        menuButton.onClick.AddListener(delegate { OnMenuButton(); });
        InitiateMenu();
    }

    public bool IsOpen()
    {
        return isOpen;
    }

    public void InitiateMenu()
    {
        if (isOpen)
        {
            OpenMenu();
            return;
        }
        CloseMenu();
    }

    public void SetMenuOpen(bool setOpen)
    {
        if (setOpen)
        {
            OpenMenu();
        }
        else
        {
            CloseMenu();
        }
    }

    public void OnMenuButton()
    {
        if (isOpen)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();

        }
    }

    private void OpenMenu()
    {
        indicatorImage.sprite = spriteWhileOpen;
        dialogButton.SetActive(true);
        dialogText.SetActive(dialogDropDownMenu.IsOpen());
        feedbackButton.SetActive(true);
        feedbackText.SetActive(feedbackDropDownMenu.IsOpen());
        isOpen = true;
    }

    private void CloseMenu()
    {
        indicatorImage.sprite = spriteWhileClosed;
        dialogButton.SetActive(false);
        dialogText.SetActive(false);
        feedbackButton.SetActive(false);
        feedbackText.SetActive(false);
        isOpen = false;
    }
}
