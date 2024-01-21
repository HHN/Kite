using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        menuButton.onClick.AddListener(delegate { OnMenuButton(); });
        CloseMenu();
    }

    public void OnMenuButton()
    {
        if (isOpen)
        {
            CloseMenu();
            return;
        }
        OpenMenu();
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
