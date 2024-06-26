using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RessourcenSceneController : SceneController
{
    [SerializeField] private RectTransform layout;

    [SerializeField] private Button bgaButton;
    [SerializeField] private Button hhnButton;
    [SerializeField] private Button kite2Button;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.RESSOURCEN_SCENE);
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);

        bgaButton.onClick.AddListener(delegate { OnBgaButton(); });
        hhnButton.onClick.AddListener(delegate { OnHhnButton(); });
        kite2Button.onClick.AddListener(delegate { OnKite2Button(); });
    }


    public void OnBgaButton()
    {
        Application.OpenURL("https://www.existenzgruendungsportal.de/Navigation/DE/Netzwerke/Gruenderinnen/gruenderinnen-im-fokus.html");
    }

    public void OnHhnButton()
    {
        Application.OpenURL("https://www.hs-heilbronn.de/de/lab-sozioinformatik");
    }

    public void OnKite2Button()
    {
        Application.OpenURL("https://kite2.de/");
    }
}
