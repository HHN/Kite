using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MockUpController : MonoBehaviour
{
    [SerializeField] private Sprite[] mockUpPages;
    [SerializeField] private Button continueButton;
    [SerializeField] private Image image;

    [SerializeField] private int currentIndex;


    void Start()
    {
        currentIndex = 0;
        continueButton.onClick.AddListener(delegate { Continue(); });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Back();
        }
    }

    public void Continue()
    {
        if (currentIndex >= mockUpPages.Length - 1)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex++;
        }
        image.sprite = mockUpPages[currentIndex];
    }

    public void Back()
    {
        if (currentIndex <= 0)
        {
            currentIndex = mockUpPages.Length - 1;
        }
        else
        {
            currentIndex--;
        }
        image.sprite = mockUpPages[currentIndex];
    }

}
