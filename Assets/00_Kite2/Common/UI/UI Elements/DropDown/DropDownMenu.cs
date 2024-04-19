using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuWrapper;
    [SerializeField] private Button menuButton;
    [SerializeField] private Image indicatorImage;
    [SerializeField] private Sprite spriteWhileOpen;
    [SerializeField] private Sprite spriteWhileClosed;
    [SerializeField] private bool isOpen;
    [SerializeField] private List<RectTransform> listOfObjectToUpdate;

    // Start is called before the first frame update
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

        if (listOfObjectToUpdate == null)
        {
            return;
        }

        foreach (RectTransform rect in listOfObjectToUpdate)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        }
    }

    private void OpenMenu()
    {
        indicatorImage.sprite = spriteWhileOpen;
        menuWrapper.SetActive(true);
        isOpen = true;
    }

    private void CloseMenu()
    {
        indicatorImage.sprite = spriteWhileClosed;
        menuWrapper.SetActive(false);
        isOpen = false;
    }
}
