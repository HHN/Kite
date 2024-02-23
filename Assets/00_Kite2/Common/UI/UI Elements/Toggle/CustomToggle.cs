using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomToggle : MonoBehaviour
{
    [SerializeField] private Button toggleButton;
    [SerializeField] private GameObject visualIndicator;
    [SerializeField] private bool isClicked;

    // Start is called before the first frame update
    void Start()
    {
        isClicked = false;
        visualIndicator.SetActive(false);
    }

    public void OnButtonPressed()
    {
        isClicked = !isClicked;
        visualIndicator.SetActive(isClicked);
    }

    public bool IsClicked()
    {
        return isClicked;
    }
}
