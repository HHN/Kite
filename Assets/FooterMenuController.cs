using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FooterMenuController : MonoBehaviour
{

    [SerializeField] private Button homeButton;
    [SerializeField] private Button archivButton;
    [SerializeField] private Button bookmarkButton;
    [SerializeField] private Button linksButton;
    [SerializeField] private Button profilButton;

    [SerializeField] private Sprite homeButtonActiveImage;
    [SerializeField] private Sprite homeButtonInactiveImage;

    [SerializeField] private Sprite archivButtonActiveImage;
    [SerializeField] private Sprite archivButtonInactiveImage;

    [SerializeField] private Sprite bookmarkButtonActiveImage;
    [SerializeField] private Sprite bookmarkButtonInactiveImage;

    [SerializeField] private Sprite linksButtonActiveImage;
    [SerializeField] private Sprite linksButtonInactiveImage;

    [SerializeField] private Sprite profilButtonActiveImage;
    [SerializeField] private Sprite profilButtonInactiveImage;


    void Start()
    {
        homeButton.onClick.AddListener(delegate { OnHomeButton(); });
        archivButton.onClick.AddListener(delegate { OnArchivButton(); });
        bookmarkButton.onClick.AddListener(delegate { OnBookmarkButton(); });
        linksButton.onClick.AddListener(delegate { OnLinksButton(); });
        profilButton.onClick.AddListener(delegate { OnProfilButton(); });
        SetButtonImagesInaktiv();
        SetImageForActivButton();
    }

    private void SetImageForActivButton()
    {
        string imageName = SceneManager.GetActiveScene().name;
        switch (imageName)
        {
            case SceneNames.FOUNDERS_BUBBLE_SCENE:
                SetHomeButtonImage();
                break;
            case SceneNames.NOVEL_HISTORY_SCENE:
                SetArchivButtonImage();
                break;
            case SceneNames.GEMERKTE_NOVELS_SCENE:
                SetBookmarkButtonImage();
                break;
            case SceneNames.RESSOURCEN_SCENE:
                SetLinksButtonImage();
                break;
            case SceneNames.FOUNDERS_WELL_2_SCENE:
                SetProfilButtonImage();
                break;
        }
    }

    private void OnHomeButton()
    {
        SceneLoader.LoadFoundersBubbleScene();
    }

    private void OnArchivButton()
    {
        SceneLoader.LoadNovelHistoryScene();
    }

    private void OnBookmarkButton()
    {
        SceneLoader.LoadGemerkteNovelsScene();
    }

    private void OnLinksButton()
    {
        SceneLoader.LoadRessourcenScene();
    }

    private void OnProfilButton()
    {
        SceneLoader.LoadFoundersWell2Scene();
    }

    private void SetButtonImagesInaktiv()
    {
        homeButton.image.sprite = homeButtonInactiveImage;
        archivButton.image.sprite = archivButtonInactiveImage;
        bookmarkButton.image.sprite = bookmarkButtonInactiveImage;
        linksButton.image.sprite = linksButtonInactiveImage;
        profilButton.image.sprite = profilButtonInactiveImage;
    }

    private void SetHomeButtonImage()
    {
        homeButton.image.sprite = homeButtonActiveImage;
    }

    private void SetArchivButtonImage()
    {
        archivButton.image.sprite = archivButtonActiveImage;
    }

    private void SetBookmarkButtonImage()
    {
        bookmarkButton.image.sprite = bookmarkButtonActiveImage;
    }

    private void SetLinksButtonImage()
    {
        linksButton.image.sprite = linksButtonActiveImage;
    }

    private void SetProfilButtonImage()
    {
        profilButton.image.sprite = profilButtonActiveImage;
    }

}
