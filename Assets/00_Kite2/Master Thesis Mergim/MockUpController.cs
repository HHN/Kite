using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MockUpController : MonoBehaviour
{
    [SerializeField] private Sprite[] mockUpPages;
    [SerializeField] private Button continueButton;
    [SerializeField] private Image image;

    private int currentIndex;


    void Start()
    {
        currentIndex = 1;
        continueButton.onClick.AddListener(delegate { Continue(); });
    }

    public void Continue()
    {
        if (currentIndex >= mockUpPages.Length)
        {
            currentIndex = 0;
        }
        image.sprite = mockUpPages[currentIndex];
        currentIndex++;
    }
}
