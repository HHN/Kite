using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSceneController : SceneController
{
    [SerializeField] private RectTransform sublayout01;
    [SerializeField] private RectTransform sublayout02;
    [SerializeField] private RectTransform sublayout03;
    [SerializeField] private RectTransform layout;

    [SerializeField] private Button aboutKiteButton;
    [SerializeField] private Button aboutKiteInfoButton;
    [SerializeField] private Button toggleDataCollectionButton;
    [SerializeField] private Button toggleDataCollectionInfoButton;
    [SerializeField] private Button deleteCollectedDataButton;
    [SerializeField] private Button deleteCollectedDataInfoButton;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.SETTINGS_SCENE);

        aboutKiteButton.onClick.AddListener(delegate { OnAboutKiteButton(); });
        aboutKiteInfoButton.onClick.AddListener(delegate { OnAboutKiteInfoButton(); });
        toggleDataCollectionButton.onClick.AddListener(delegate { OnToggleDataCollectionButton(); });
        toggleDataCollectionInfoButton.onClick.AddListener(delegate { OnToggleDataCollectionInfoButton(); });
        deleteCollectedDataButton.onClick.AddListener(delegate { OnDeleteCollectedDataButton(); });
        deleteCollectedDataInfoButton.onClick.AddListener(delegate { OnDeleteCollectedDataInfoButton(); });

        InitializeToggleDataCollectionButton();
        LayoutRebuilder.ForceRebuildLayoutImmediate(sublayout01);
        LayoutRebuilder.ForceRebuildLayoutImmediate(sublayout02);
        LayoutRebuilder.ForceRebuildLayoutImmediate(sublayout03);
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
    }

    public void OnAboutKiteButton()
    {
        SceneLoader.LoadInfoScene();
    }

    public void OnAboutKiteInfoButton()
    {
        DisplayInfoMessage(InfoMessages.EXPLANATION_ABOUT_US_BUTTON);
    }

    public void OnToggleDataCollectionButton()
    {
        if (PrivacyAndConditionManager.Instance().IsDataCollectionAccepted())
        {
            PrivacyAndConditionManager.Instance().UnacceptDataCollection();
            DisplayInfoMessage(InfoMessages.STOPED_DATA_COLLECTION);
        }
        else
        {
            PrivacyAndConditionManager.Instance().AcceptDataCollection();
            DisplayInfoMessage(InfoMessages.STARTED_DATA_COLLECTION);
        }
        InitializeToggleDataCollectionButton();
    }

    public void OnToggleDataCollectionInfoButton()
    {
        if (PrivacyAndConditionManager.Instance().IsDataCollectionAccepted())
        {
            DisplayInfoMessage(InfoMessages.EXPLANATION_STOP_DATA_BUTTON);
        } 
        else
        {
            DisplayInfoMessage(InfoMessages.EXPLANATION_COLLECT_DATA_BUTTON);
        }
    }

    public void OnDeleteCollectedDataButton()
    {
        AnalyticsServiceHandler.Instance().DeleteCollectedData();
        DisplayInfoMessage(InfoMessages.DELETED_DATA);
    }

    public void OnDeleteCollectedDataInfoButton()
    {
        DisplayInfoMessage(InfoMessages.EXPLANATION_DELETE_DATA_BUTTON);
    }

    public void InitializeToggleDataCollectionButton()
    {
        if (PrivacyAndConditionManager.Instance().IsDataCollectionAccepted())
        {
            toggleDataCollectionButton.GetComponentInChildren<TextMeshProUGUI>().text = "DATEN ERFASSUNG STOPPEN";
        }
        else
        {
            toggleDataCollectionButton.GetComponentInChildren<TextMeshProUGUI>().text = "DATEN ERFASSEN";
        }
    }
}
