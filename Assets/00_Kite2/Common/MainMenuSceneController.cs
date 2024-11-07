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
    [SerializeField] private static int COMPATIBLE_SERVER_VERSION_NUMBER = 7;
    [SerializeField] private GameObject novelLoader;

    private void Start()
    {
        // Initialisiere die Szene und setze grundlegende Einstellungen
        InitializeScene();

        // Richte die Button-Listener ein, um auf Benutzereingaben zu reagieren
        SetupButtonListeners();

        // Behandle die Nutzungsbedingungen und Datenschutzrichtlinien
        HandleTermsAndConditions();

        // Hole die Instanz des PrivacyManagers, um den aktuellen Status der Datenschutzakzeptanz zu überprüfen
        var privacyManager = PrivacyAndConditionManager.Instance();

        // Überprüfe, ob die Nutzungsbedingungen und Datenschutzrichtlinien akzeptiert wurden
        if (privacyManager.IsConditionsAccepted() && privacyManager.IsPriavcyTermsAccepted())
        {
            // Starte eine Coroutine, die darauf wartet, dass die Novels geladen sind
            StartCoroutine(WaitForNovelsToLoad());
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
        //novelPlayerButton.onClick.AddListener(OnNovelPlayerButton);
        continuetermsAndConditionsButton.onClick.AddListener(OnContinueTermsAndConditionsButton);
    }

    private void HandleTermsAndConditions()
    {
        var privacyManager = PrivacyAndConditionManager.Instance();

        if (privacyManager.IsConditionsAccepted() && privacyManager.IsPriavcyTermsAccepted())
        {
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
            StartCoroutine(WaitForNovelsToLoad());
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

    // Coroutine that waits for the novels to load
    private IEnumerator WaitForNovelsToLoad()
    {
        // Initialize an empty list for the visual novels
        List<VisualNovel> allNovels = null;

        // Wait for the novel data to be loaded
        while (allNovels == null || allNovels.Count == 0)
        {
            // Try to get the list of all available novels
            allNovels = KiteNovelManager.Instance().GetAllKiteNovels();
            yield return new WaitForSeconds(0.5f); // Wait half a second and check again
        }

        // Call the method to start the introductory novel and pass the loaded novel list
        StartIntroNovel(allNovels);
    }

    // Method to start the introductory novel
    private void StartIntroNovel(List<VisualNovel> allNovels)
    {
        // Iterate through the list of all loaded novels
        foreach (var novel in allNovels)
        {
            // Check if the current novel has the title "Einstiegsdialog" (introductory dialogue)
            if (novel.title == "Einstiegsdialog")
            {
                // Convert the novel ID to the corresponding enum
                VisualNovelNames novelNames = VisualNovelNamesHelper.ValueOf((int)novel.id);

                PlayManager.Instance().SetVisualNovelToPlay(novel); // Set the novel to be played in the PlayManager          
                PlayManager.Instance().SetForegroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetForegrundColorOfNovel(novelNames)); // Set the foreground color for the novel
                PlayManager.Instance().SetBackgroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(novelNames)); // Set the background color for the novel
                PlayManager.Instance().SetDiplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(novelNames)); // Set the display name for the novel

                // Load the PlayNovelScene
                SceneLoader.LoadPlayNovelScene();
            }
        }
    }
}
