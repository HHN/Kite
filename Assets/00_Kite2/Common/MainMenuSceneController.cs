using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class MainMenuSceneController : SceneController, OnSuccessHandler
{
    [SerializeField] private Button novelPlayerButton;
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

    private void Start()
    { 
        InitializeScene();
        SetupButtonListeners();
        HandleTermsAndConditions();

        var privacyManager = PrivacyAndConditionManager.Instance();

        //Debug.Log("privacyManager.IsConditionsAccepted(): " + privacyManager.IsConditionsAccepted());
        //Debug.Log("privacyManager.IsPriavcyTermsAccepted(): " + privacyManager.IsPriavcyTermsAccepted());

        if (privacyManager.IsConditionsAccepted() && privacyManager.IsPriavcyTermsAccepted())
        {
            SceneLoader.LoadIntroNovelScene();
        }
    }

    private void InitializeScene()
    {
        DontDestroyOnLoad(novelLoader);
        AnalyticsServiceHandler.Instance().StartAnalytics();
        PlayerDataManager.Instance().LoadAllPlayerPrefs();
        BackStackManager.Instance().Clear();
        SceneMemoryManager.Instance().ClearMemory();
    }

    private void SetupButtonListeners()
    {
        novelPlayerButton.onClick.AddListener(OnNovelPlayerButton);
        continuetermsAndConditionsButton.onClick.AddListener(OnContinueTermsAndConditionsButton);
    }

    private void HandleTermsAndConditions()
    {
        var privacyManager = PrivacyAndConditionManager.Instance();
        Debug.Log("Checking terms and conditions...");

        if (privacyManager.IsConditionsAccepted() && privacyManager.IsPriavcyTermsAccepted())
        {
            Debug.Log("Terms already accepted.");
            termsAndConditionPanel.SetActive(false);
            kiteAudioLogo.Play();

            if (privacyManager.IsDataCollectionAccepted())
            {
                AnalyticsServiceHandler.Instance().CollectData();
            }

            if (!ApplicationModeManager.Instance().IsOfflineModeActive())
            {
                StartVersionCheck();
            }
        }
    }

    private void StartVersionCheck()
    {
        var call = Instantiate(getVersionServerCallPrefab).GetComponent<GetVersionServerCall>();
        call.sceneController = this;
        call.onSuccessHandler = this;
        call.SendRequest();
        DontDestroyOnLoad(call.gameObject);
    }

    public void OnNovelPlayerButton()
    {
        var analytics = AnalyticsServiceHandler.Instance();
        analytics.SendMainMenuStatistics();
        analytics.SetFromWhereIsNovelSelected("KITE NOVELS");

        // Instantiate the sound prefab and assign it to a variable
        GameObject buttonSound = Instantiate(buttonSoundPrefab);
        DontDestroyOnLoad(buttonSound);  // Correct usage of 

        // Remove or comment out any additional scene loading calls that might override the PlayNovelScene
        // SceneLoader.LoadFoundersBubbleScene(); // Comment this out
    }

    public void OnSettingsButton()
    {
        SceneLoader.LoadSettingsScene();
    }

    public void OnContinueTermsAndConditionsButton()
    {
        UpdateTermsAcceptance();
        ValidateTermsAndLoadScene();
    }

    private void UpdateTermsAcceptance()
    {
        var privacyManager = PrivacyAndConditionManager.Instance();

        UpdateAcceptance(termsOfUseToggle.IsClicked(),
                         privacyManager.AcceptConditionsOfUssage,
                         privacyManager.UnacceptConditionsOfUssage);

        UpdateAcceptance(dataPrivacyToggle.IsClicked(),
                         privacyManager.AcceptTermsOfPrivacy,
                         privacyManager.UnacceptTermsOfPrivacy);

        UpdateAcceptance(collectDataToggle.IsClicked(),
                         privacyManager.AcceptDataCollection,
                         privacyManager.UnacceptDataCollection);
    }

    private void UpdateAcceptance(bool isAccepted, System.Action acceptAction, System.Action unacceptAction)
    {
        if (isAccepted)
        {
            acceptAction();
        }
        else
        {
            unacceptAction();
        }
    }

    private void ValidateTermsAndLoadScene()
    {
        bool acceptedTermsOfUse = termsOfUseToggle.IsClicked();
        bool acceptedDataPrivacyTerms = dataPrivacyToggle.IsClicked();

        if (acceptedTermsOfUse && acceptedDataPrivacyTerms)
        {
            //SceneLoader.LoadMainMenuScene();
            Debug.Log("Both terms accepted, starting intro novel.");
            StartCoroutine(StartIntroNovel());
        }
        else
        {
            infoTextTermsAndConditions.gameObject.SetActive(true);
        }
    }

    public void OnSuccess(Response response)
    {
        if (response == null) return;

        if (response.GetVersion() != COMPATIBLE_SERVER_VERSION_NUMBER)
        {
            DisplayInfoMessage(InfoMessages.UPDATE_AVAILABLE);
        }
    }

    private IEnumerator StartIntroNovel()
    {
        yield return new WaitForSeconds(2.5f);

        Debug.Log("What's here?");

        List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();

        Debug.Log(allNovels.Count);

        foreach (var novel in allNovels)
        {
            if (novel.title == "Einstiegsdialog")
            {
                Debug.Log("Check");

                VisualNovelNames novelNames = VisualNovelNamesHelper.ValueOf((int)novel.id);

                PlayManager.Instance().SetVisualNovelToPlay(novel);
                PlayManager.Instance().SetForegroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetForegrundColorOfNovel(novelNames));
                PlayManager.Instance().SetBackgroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(novelNames));
                PlayManager.Instance().SetDiplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(novelNames));

                if (ShowPlayInstructionManager.Instance().ShowInstruction())
                {
                    Debug.Log("Check 2");
                    // Load the PlayNovelScene directly
                    SceneLoader.LoadPlayNovelScene();
                    //return; // Exit the loop after loading
                }
            }
        }
    }
}
