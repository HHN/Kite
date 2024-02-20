using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSceneController : SceneController
{
    [SerializeField] private RectTransform sublayout01;
    [SerializeField] private RectTransform sublayout02;
    [SerializeField] private RectTransform sublayout03;
    [SerializeField] private RectTransform sublayout04;
    [SerializeField] private RectTransform sublayout05;
    [SerializeField] private RectTransform sublayout06;
    [SerializeField] private RectTransform layout;

    [SerializeField] private Button aboutKiteButton;
    [SerializeField] private Button aboutKiteInfoButton;
    [SerializeField] private Button toggleDataCollectionButton;
    [SerializeField] private Button toggleDataCollectionInfoButton;
    [SerializeField] private Button deleteCollectedDataButton;
    [SerializeField] private Button deleteCollectedDataInfoButton;

    [SerializeField] private Button termsOfUseButton;
    [SerializeField] private Button termsOfUseInfoButton;
    [SerializeField] private Button dataPrivacyButton;
    [SerializeField] private Button dataPrivacyInfoButton;
    [SerializeField] private Button imprintButton;
    [SerializeField] private Button imprintInfoButton;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.SETTINGS_SCENE);

        aboutKiteButton.onClick.AddListener(delegate { OnAboutKiteButton(); });
        aboutKiteInfoButton.onClick.AddListener(delegate { OnAboutKiteInfoButton(); });
        toggleDataCollectionButton.onClick.AddListener(delegate { OnToggleDataCollectionButton(); });
        toggleDataCollectionInfoButton.onClick.AddListener(delegate { OnToggleDataCollectionInfoButton(); });
        deleteCollectedDataButton.onClick.AddListener(delegate { OnDeleteCollectedDataButton(); });
        deleteCollectedDataInfoButton.onClick.AddListener(delegate { OnDeleteCollectedDataInfoButton(); });

        termsOfUseButton.onClick.AddListener(delegate { OnTermsOfUseButton(); });
        termsOfUseInfoButton.onClick.AddListener(delegate { OnTermsOfUseInfoButton(); });
        dataPrivacyButton.onClick.AddListener(delegate { OnDataPrivacyButton(); });
        dataPrivacyInfoButton.onClick.AddListener(delegate { OnDataPrivacyInfoButton(); });
        imprintButton.onClick.AddListener(delegate { OnImprintButton(); });
        imprintInfoButton.onClick.AddListener(delegate { OnImprintInfoButton(); });

        InitializeToggleDataCollectionButton();
        LayoutRebuilder.ForceRebuildLayoutImmediate(sublayout01);
        LayoutRebuilder.ForceRebuildLayoutImmediate(sublayout02);
        LayoutRebuilder.ForceRebuildLayoutImmediate(sublayout03);
        LayoutRebuilder.ForceRebuildLayoutImmediate(sublayout04);
        LayoutRebuilder.ForceRebuildLayoutImmediate(sublayout05);
        LayoutRebuilder.ForceRebuildLayoutImmediate(sublayout06);
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

    public void OnTermsOfUseButton()
    {
        SceneLoader.LoadTermsOfUseScene();
    }

    public void OnTermsOfUseInfoButton()
    {
        DisplayInfoMessage(InfoMessages.EXPLANATION_TERMS_OF_USE_BUTTON);
    }

    public void OnDataPrivacyButton()
    {
        SceneLoader.LoadPrivacyPolicyScene();
    }

    public void OnDataPrivacyInfoButton()
    {
        DisplayInfoMessage(InfoMessages.EXPLANATION_DATA_PRIVACY_BUTTON);
    }

    public void OnImprintButton()
    {
        SceneLoader.LoadImprintScene();
    }

    public void OnImprintInfoButton()
    {
        DisplayInfoMessage(InfoMessages.EXPLANATION_IMPRINT_BUTTON);
    }
}
