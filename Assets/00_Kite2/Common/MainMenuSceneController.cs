using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuSceneController : SceneController, OnSuccessHandler
{ 
    [SerializeField] private Button novelPlayerButton;
    [SerializeField] private Button kiteLogo;
    [SerializeField] private GameObject buttonSoundPrefab;
    [SerializeField] private GameObject termsAndConditionPanel;
    [SerializeField] private Button continuetermsAndConditionsButton;
    [SerializeField] private CustomToggle termsOfUseToggle;
    [SerializeField] private CustomToggle dataPrivacyToggle;
    [SerializeField] private CustomToggle collectDataToggle;
    [SerializeField] private TextMeshProUGUI infoTextTermsAndConditions;
    [SerializeField] private AudioSource kiteAudioLogo;
    [SerializeField] private GameObject getVersionServerCallPrefab;
    [SerializeField] private static int COMPATIBLE_SERVER_VERSION_NUMBER = 5;
    [SerializeField] private GameObject novelLoader;

    void Start()
    {
        DontDestroyOnLoad(novelLoader);

        // Analytics 
        var analytics = AnalyticsServiceHandler.Instance();
        analytics.StartAnalytics();

        BackStackManager.Instance().Clear();
        SceneMemoryManager.Instance().ClearMemory();

        novelPlayerButton.onClick.AddListener(delegate { OnNovelPlayerButton(); });
        kiteLogo.onClick.AddListener(delegate { OnKiteLogoButton(); });
        continuetermsAndConditionsButton.onClick.AddListener(delegate { OnContinueTermsAndConditionsButton(); });

        if (PrivacyAndConditionManager.Instance().IsConditionsAccepted() && PrivacyAndConditionManager.Instance().IsPriavcyTermsAccepted())
        {
            termsAndConditionPanel.SetActive(false);
            kiteAudioLogo.Play();

            if (PrivacyAndConditionManager.Instance().IsDataCollectionAccepted())
            {
                AnalyticsServiceHandler.Instance().CollectData();
            }

            if (ApplicationModeManager.Instance().IsOfflineModeActive())
            {
                return;
            }
            GetVersionServerCall call = Instantiate(getVersionServerCallPrefab).GetComponent<GetVersionServerCall>();
            call.sceneController = this;
            call.onSuccessHandler = this;
            call.SendRequest();
            DontDestroyOnLoad(call.gameObject);
        }
    }

    public void OnNovelPlayerButton()
    {
        AnalyticsServiceHandler.Instance().SendMainMenuStatistics();
        AnalyticsServiceHandler.Instance().SetFromWhereIsNovelSelected("KITE NOVELS");

        GameObject buttonSound = Instantiate(buttonSoundPrefab);
        DontDestroyOnLoad(buttonSound);

        SceneLoader.LoadFoundersBubbleScene();
    }

    public void OnSettingsButton()
    {
        SceneLoader.LoadSettingsScene();
    }

    public void OnKiteLogoButton()
    {
        SceneLoader.LoadInfoScene();
    }

    public void OnContinueTermsAndConditionsButton()
    {
        bool acceptedTermsOfUse = termsOfUseToggle.IsClicked();
        bool acceptedDataPrivacyTerms = dataPrivacyToggle.IsClicked();
        bool acceptedDataCollection = collectDataToggle.IsClicked();

        if (acceptedTermsOfUse)
        {
            PrivacyAndConditionManager.Instance().AcceptConditionsOfUssage();
        } 
        else
        {
            PrivacyAndConditionManager.Instance().UnacceptConditionsOfUssage();
        }
        if (acceptedDataPrivacyTerms)
        {
            PrivacyAndConditionManager.Instance().AcceptTermsOfPrivacy();
        }
        else
        {
            PrivacyAndConditionManager.Instance().UnacceptTermsOfPrivacy();
        }
        if (acceptedDataCollection)
        {
            PrivacyAndConditionManager.Instance().AcceptDataCollection();
        }
        else
        {
            PrivacyAndConditionManager.Instance().UnacceptDataCollection();
        }

        if (acceptedTermsOfUse && acceptedDataPrivacyTerms)
        {
            SceneLoader.LoadMainMenuScene();
        } 
        else
        {
            infoTextTermsAndConditions.gameObject.SetActive(true);
        }
    }

    public void OnSuccess(Response response)
    {
        if (response == null) 
        { 
            return; 
        }
        if (response.GetVersion() != COMPATIBLE_SERVER_VERSION_NUMBER)
        {
            DisplayInfoMessage(InfoMessages.UPDATE_AVAILABLE);
        }
    }
}
