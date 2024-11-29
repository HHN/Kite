using System.Collections;
using System.Collections.Generic;
using _00_Kite2.Common.Managers;
using _00_Kite2.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenerateOfflineAiFeeedbackSceneController : SceneController
{
    private static readonly object _lockObject = new object();

    private const string PREPARE_GENERATION_TEXT = "Generierung von Offline Feedback vorbereiten";
    private const string START_GENERATION_TEXT = "Generierung von Offline Feedback starten";
    private const string CONTINUE_GENERATION_TEXT = "Generierung von Offline Feedback fortf�hren";
    public bool novelsLoaded;
    public bool feedbackLoaded;
    public Sprite positiveSprite;
    public OfflineFeedbackLoader offlineFeedbackLoader;
    public GameObject getCompletionServerCall;

    public Image bankKreditNovelAvailableIndicator;
    public Image bekannteTreffenNovelAvailableIndicator;
    public Image bankkontoNovelAvailableIndicator;
    public Image foerderantragNovelAvailableIndicator;
    public Image elternNovelAvailableIndicator;
    public Image notarinNovelAvailableIndicator;
    public Image presseNovelAvailableIndicator;
    public Image bueroNovelAvailableIndicator;
    public Image gruendungszuschussNovelAvailableIndicator;
    public Image honorarNovelAvailableIndicator;
    public Image lebenspartnerNovelAvailableIndicator;
    public Image introNovelAvailableIndicator;

    public Button bankKreditNovelAnalyseButton;
    public Button bekannteTreffenNovelAnalyseButton;
    public Button bankkontoNovelAnalyseButton;
    public Button foerderantragNovelAnalyseButton;
    public Button elternNovelAnalyseButton;
    public Button notarinNovelAnalyseButton;
    public Button presseNovelAnalyseButton;
    public Button bueroNovelAnalyseButton;
    public Button gruendungszuschussNovelAnalyseButton;
    public Button honorarNovelAnalyseButton;
    public Button lebenspartnerNovelAnalyseButton;
    public Button introNovelAnalyseButton;

    public TextMeshProUGUI bankKreditNovelAvailableIndicatorText;
    public TextMeshProUGUI bekannteTreffenNovelAvailableIndicatorText;
    public TextMeshProUGUI bankkontoNovelAvailableIndicatorText;
    public TextMeshProUGUI foerderantragNovelAvailableIndicatorText;
    public TextMeshProUGUI elternNovelAvailableIndicatorText;
    public TextMeshProUGUI notarinNovelAvailableIndicatorText;
    public TextMeshProUGUI presseNovelAvailableIndicatorText;
    public TextMeshProUGUI bueroNovelAvailableIndicatorText;
    public TextMeshProUGUI gruendungszuschussNovelAvailableIndicatorText;
    public TextMeshProUGUI honorarNovelAvailableIndicatorText;
    public TextMeshProUGUI lebenspartnerNovelAvailableIndicatorText;
    public TextMeshProUGUI introNovelAvailableIndicatorText;

    public TextMeshProUGUI bankKreditNovelNumberOfPaths;
    public TextMeshProUGUI bekannteTreffenNovelNumberOfPaths;
    public TextMeshProUGUI bankkontoNovelNumberOfPaths;
    public TextMeshProUGUI foerderantragNovelNumberOfPaths;
    public TextMeshProUGUI elternNovelNumberOfPaths;
    public TextMeshProUGUI notarinNovelNumberOfPaths;
    public TextMeshProUGUI presseNovelNumberOfPaths;
    public TextMeshProUGUI bueroNovelNumberOfPaths;
    public TextMeshProUGUI gruendungszuschussNovelNumberOfPaths;
    public TextMeshProUGUI honorarNovelNumberOfPaths;
    public TextMeshProUGUI lebenspartnerNovelNumberOfPaths;
    public TextMeshProUGUI introNovelNumberOfPaths;    

    public Dictionary<VisualNovelNames, FeedbackNodeList> generatedFeedbackNodes;

    public Color green = new Color((11f / 355f), (98f / 355f), (11f/355f));

    public bool analysed;

    private void Start()
    {
        generatedFeedbackNodes = new Dictionary<VisualNovelNames, FeedbackNodeList> ();

        bankKreditNovelAnalyseButton.onClick.AddListener(delegate { AnalyseBankkreditNovel(); });
        bekannteTreffenNovelAnalyseButton.onClick.AddListener(delegate { AnalyseBekannteTreffenNovel(); });
        bankkontoNovelAnalyseButton.onClick.AddListener(delegate { AnalyseBankkontoNovel(); });
        foerderantragNovelAnalyseButton.onClick.AddListener(delegate { AnalyseFoerderantragNovel(); });
        elternNovelAnalyseButton.onClick.AddListener(delegate { AnalyseElternNovel(); });
        notarinNovelAnalyseButton.onClick.AddListener(delegate { AnalyseNotariatNovel(); });
        presseNovelAnalyseButton.onClick.AddListener(delegate { AnalysePresseNovel(); });
        bueroNovelAnalyseButton.onClick.AddListener(delegate { AnalyseBueroNovel(); });
        gruendungszuschussNovelAnalyseButton.onClick.AddListener(delegate { AnalyseGruendungszuschussNovel(); });
        honorarNovelAnalyseButton.onClick.AddListener(delegate { AnalyseHonorarNovel(); });
        lebenspartnerNovelAnalyseButton.onClick.AddListener(delegate { AnalyseLebenspartnerNovel(); });
        introNovelAnalyseButton.onClick.AddListener(delegate { AnalyseIntroNovel(); });        
    }

    void Update()
    {
        if (analysed) 
        {
            return; 
        }

        if (KiteNovelManager.Instance().GetAllKiteNovels() != null && KiteNovelManager.Instance().GetAllKiteNovels().Count != 0)
        {
            analysed = true;
            AnalyseNovels();
        }
    }

    public IEnumerator AnalyseNovel(VisualNovel novel, TextMeshProUGUI numberOfPathsText, Button button)
    {
        NovelAnalyser novelAnalyser = new NovelAnalyser();
        
        yield return novelAnalyser.AnalyseNovel(novel);

        if (novelAnalyser.LoopDetected())
        {
            numberOfPathsText.text = "Schleife erkannt! Analyse abgebrochen!";
            yield break;
        }
        Debug.Log("Analysierte Novel: " + novel.title);
        Debug.Log("Anzahl m�glicher Pfade: " + novelAnalyser.GetNumberOfPossiblePaths());

        yield return offlineFeedbackLoader.LoadOfflineFeedbackForNovelFromJsonInEditMode(VisualNovelNamesHelper.ValueOf((int) novel.id));

        if (PreGeneratedOfflineFeedbackManager.Instance().IsFeedbackLoaded(VisualNovelNamesHelper.ValueOf((int)novel.id)) && 
            (PreGeneratedOfflineFeedbackManager.Instance().GetPreGeneratedOfflineFeedback(VisualNovelNamesHelper.ValueOf((int)novel.id)).Count > 0))
        {
            List<FeedbackNodeContainer> nodes = new List<FeedbackNodeContainer>(PreGeneratedOfflineFeedbackManager.Instance().GetPreGeneratedOfflineFeedback(VisualNovelNamesHelper.ValueOf((int)novel.id)).Values);

            int numberOfContainedFeedbacks = 0;

            foreach (FeedbackNodeContainer node in nodes)
            {
                if (!string.IsNullOrEmpty(node.completion))
                {
                    numberOfContainedFeedbacks++;
                }
            }

            FeedbackNodeList list = new FeedbackNodeList();
            list.feedbackNodes = nodes;
            generatedFeedbackNodes[VisualNovelNamesHelper.ValueOf((int)novel.id)] = list;

            numberOfPathsText.text = "Feedbacks generiert: " + numberOfContainedFeedbacks + " / " + nodes.Count;

            button.interactable = true;

            if (numberOfContainedFeedbacks == 0)
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = START_GENERATION_TEXT;
            } else
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = CONTINUE_GENERATION_TEXT;
            }
        }
        else
        {
            List<FeedbackNodeContainer> listOfContainer = novelAnalyser.GetAllPossiblePaths();
            FeedbackNodeList list = new FeedbackNodeList();
            list.feedbackNodes = listOfContainer;
            generatedFeedbackNodes[VisualNovelNamesHelper.ValueOf((int)novel.id)] = list;
            button.interactable = true;
            button.GetComponentInChildren<TextMeshProUGUI>().text = START_GENERATION_TEXT;
            numberOfPathsText.text = "Feedbacks generiert: " + 0 + " / " + listOfContainer.Count;

        }

        offlineFeedbackLoader.SaveOfflineFeedbackForNovelInEditMode(VisualNovelNamesHelper.ValueOf((int)novel.id), generatedFeedbackNodes[VisualNovelNamesHelper.ValueOf((int)novel.id)]);
    }

    public void SaveOfflineFeedbackForEdit(VisualNovelNames visualNovelName)
    {
        if (!generatedFeedbackNodes.ContainsKey(visualNovelName))
        {
            Debug.LogWarning("Save Offline Feedback for Edit did not work! Feedback not found!");
            return;
        }
        FeedbackNodeList list = generatedFeedbackNodes[visualNovelName];
        offlineFeedbackLoader.SaveOfflineFeedbackForNovelInEditMode(visualNovelName, list);
    }

    void AnalyseNovels()
    {
        List<VisualNovel> novels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in novels)
        {
            switch (VisualNovelNamesHelper.ValueOf((int) novel.id))
            {
                case VisualNovelNames.ELTERN_NOVEL:
                    {
                        elternNovelAvailableIndicator.sprite = positiveSprite;
                        elternNovelAvailableIndicator.color = green;
                        elternNovelAvailableIndicatorText.text = "Verf�gbar!";
                        elternNovelAnalyseButton.interactable = true;
                        break;
                    }
                case VisualNovelNames.PRESSE_NOVEL:
                    {
                        presseNovelAvailableIndicator.sprite = positiveSprite;
                        presseNovelAvailableIndicator.color = green;
                        presseNovelAvailableIndicatorText.text = "Verf�gbar!";
                        presseNovelAnalyseButton.interactable = true;
                        break;
                    }
                case VisualNovelNames.NOTARIAT_NOVEL:
                    {
                        notarinNovelAvailableIndicator.sprite = positiveSprite;
                        notarinNovelAvailableIndicator.color = green;
                        notarinNovelAvailableIndicatorText.text = "Verf�gbar!";
                        notarinNovelAnalyseButton.interactable = true;
                        break;
                    }
                case VisualNovelNames.BANK_KONTO_NOVEL:
                    {
                        bankkontoNovelAvailableIndicator.sprite = positiveSprite;
                        bankkontoNovelAvailableIndicator.color = green;
                        bankkontoNovelAvailableIndicatorText.text = "Verf�gbar!";
                        bankkontoNovelAnalyseButton.interactable = true;
                        break;
                    }
                case VisualNovelNames.BUERO_NOVEL:
                    {
                        bueroNovelAvailableIndicator.sprite = positiveSprite;
                        bueroNovelAvailableIndicator.color = green;
                        bueroNovelAvailableIndicatorText.text = "Verf�gbar!";
                        bueroNovelAnalyseButton.interactable = true; 
                        break;
                    }
                case VisualNovelNames.FOERDERANTRAG_NOVEL:
                    {
                        foerderantragNovelAvailableIndicator.sprite = positiveSprite;
                        foerderantragNovelAvailableIndicator.color = green;
                        foerderantragNovelAvailableIndicatorText.text = "Verf�gbar!";
                        foerderantragNovelAnalyseButton.interactable = true;
                        break;
                    }
                case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                    {
                        gruendungszuschussNovelAvailableIndicator.sprite = positiveSprite;
                        gruendungszuschussNovelAvailableIndicator.color = green;
                        gruendungszuschussNovelAvailableIndicatorText.text = "Verf�gbar!";
                        gruendungszuschussNovelAnalyseButton.interactable = true;
                        break;
                    }
                case VisualNovelNames.BEKANNTE_TREFFEN_NOVEL:
                    {
                        bekannteTreffenNovelAvailableIndicator.sprite = positiveSprite;
                        bekannteTreffenNovelAvailableIndicator.color = green;
                        bekannteTreffenNovelAvailableIndicatorText.text = "Verf�gbar!";
                        bekannteTreffenNovelAnalyseButton.interactable = true;
                        break;
                    }
                case VisualNovelNames.BANK_KREDIT_NOVEL:
                    {
                        bankKreditNovelAvailableIndicator.sprite = positiveSprite;
                        bankKreditNovelAvailableIndicator.color = green;
                        bankKreditNovelAvailableIndicatorText.text = "Verf�gbar!";
                        bankKreditNovelAnalyseButton.interactable = true;
                        break;
                    }
                case VisualNovelNames.HONORAR_NOVEL:
                    {
                        honorarNovelAvailableIndicator.sprite = positiveSprite;
                        honorarNovelAvailableIndicator.color = green;
                        honorarNovelAvailableIndicatorText.text = "Verf�gbar!";
                        honorarNovelAnalyseButton.interactable = true;
                        break;
                    }
                case VisualNovelNames.INTRO_NOVEL:
                    {
                        introNovelAvailableIndicator.sprite = positiveSprite;
                        introNovelAvailableIndicator.color = green;
                        introNovelAvailableIndicatorText.text = "Verf�gbar!";
                        introNovelAnalyseButton.interactable = true;
                        break;
                    }
                case VisualNovelNames.LEBENSPARTNER_NOVEL:
                    {
                        lebenspartnerNovelAvailableIndicator.sprite = positiveSprite;
                        lebenspartnerNovelAvailableIndicator.color = green;
                        lebenspartnerNovelAvailableIndicatorText.text = "Verf�gbar!";
                        lebenspartnerNovelAnalyseButton.interactable = true;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }

    public IEnumerator StartGeneratingAiFeedback(VisualNovelNames visualNovelName, FeedbackNodeList feedbackNodeList, TextMeshProUGUI updatePossibility)
    {
        int done = 0;
        foreach (FeedbackNodeContainer item in feedbackNodeList.feedbackNodes)
        {
            if (!string.IsNullOrEmpty(item.completion))
            {
                done++;
            }
        }
        updatePossibility.text = "Feedbacks generiert: " + done + " / " + feedbackNodeList.feedbackNodes.Count;


        foreach (FeedbackNodeContainer item in feedbackNodeList.feedbackNodes)
        {
            if (!string.IsNullOrEmpty(item.completion))
            {
                continue;
            }
            HandleCompletionRequestResult handler = new HandleCompletionRequestResult(visualNovelName, this, updatePossibility, item);
            GetCompletionServerCall call = Instantiate(getCompletionServerCall).GetComponent<GetCompletionServerCall>();
            call.sceneController = this;
            call.onSuccessHandler = handler;
            call.onErrorHandler = handler;
            call.prompt = item.prompt;
            call.SendRequest();
            yield return new WaitForSeconds(5f);
        }
    }

    public void AnalyseElternNovel()
    {
        elternNovelAnalyseButton.interactable = false;

        if (generatedFeedbackNodes.ContainsKey(VisualNovelNames.ELTERN_NOVEL))
        {
            StartCoroutine(StartGeneratingAiFeedback(VisualNovelNames.ELTERN_NOVEL, generatedFeedbackNodes[VisualNovelNames.ELTERN_NOVEL], elternNovelNumberOfPaths));
            return;
        }

        List<VisualNovel> novels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in novels)
        {
            switch (VisualNovelNamesHelper.ValueOf((int)novel.id))
            {
                case VisualNovelNames.ELTERN_NOVEL:
                    {
                        StartCoroutine(AnalyseNovel(novel, elternNovelNumberOfPaths, elternNovelAnalyseButton));
                        break;
                    }
                default:
                    { break; }
            }
        }
    }

    public void AnalysePresseNovel()
    {
        presseNovelAnalyseButton.interactable = false;

        if (generatedFeedbackNodes.ContainsKey(VisualNovelNames.PRESSE_NOVEL))
        {
            StartCoroutine(StartGeneratingAiFeedback(VisualNovelNames.PRESSE_NOVEL, generatedFeedbackNodes[VisualNovelNames.PRESSE_NOVEL], presseNovelNumberOfPaths));
            return;
        }

        List<VisualNovel> novels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in novels)
        {
            switch (VisualNovelNamesHelper.ValueOf((int)novel.id))
            {
                case VisualNovelNames.PRESSE_NOVEL:
                    {
                        StartCoroutine(AnalyseNovel(novel, presseNovelNumberOfPaths, presseNovelAnalyseButton));
                        break;
                    }
                default:
                    { break; }
            }
        }
    }

    public void AnalyseNotariatNovel()
    {
        notarinNovelAnalyseButton.interactable = false;

        if (generatedFeedbackNodes.ContainsKey(VisualNovelNames.NOTARIAT_NOVEL))
        {
            StartCoroutine(StartGeneratingAiFeedback(VisualNovelNames.NOTARIAT_NOVEL, generatedFeedbackNodes[VisualNovelNames.NOTARIAT_NOVEL], notarinNovelNumberOfPaths));
            return;
        }

        List<VisualNovel> novels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in novels)
        {
            switch (VisualNovelNamesHelper.ValueOf((int)novel.id))
            {
                case VisualNovelNames.NOTARIAT_NOVEL:
                    {
                        StartCoroutine(AnalyseNovel(novel, notarinNovelNumberOfPaths, notarinNovelAnalyseButton));
                        break;
                    }
                default:
                    { break; }
            }
        }
    }

    public void AnalyseBankkontoNovel()
    {
        bankkontoNovelAnalyseButton.interactable = false;

        if (generatedFeedbackNodes.ContainsKey(VisualNovelNames.BANK_KONTO_NOVEL))
        {
            StartCoroutine(StartGeneratingAiFeedback(VisualNovelNames.BANK_KONTO_NOVEL, generatedFeedbackNodes[VisualNovelNames.BANK_KONTO_NOVEL], bankkontoNovelNumberOfPaths));
            return;
        }

        List<VisualNovel> novels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in novels)
        {
            switch (VisualNovelNamesHelper.ValueOf((int)novel.id))
            {
                case VisualNovelNames.BANK_KONTO_NOVEL:
                    {
                        StartCoroutine(AnalyseNovel(novel, bankkontoNovelNumberOfPaths, bankkontoNovelAnalyseButton));
                        break;
                    }
                default:
                    { break; }
            }
        }
    }

    public void AnalyseBueroNovel()
    {
        bueroNovelAnalyseButton.interactable = false;

        if (generatedFeedbackNodes.ContainsKey(VisualNovelNames.BUERO_NOVEL))
        {
            StartCoroutine(StartGeneratingAiFeedback(VisualNovelNames.BUERO_NOVEL, generatedFeedbackNodes[VisualNovelNames.BUERO_NOVEL], bueroNovelNumberOfPaths));
            return;
        }

        List<VisualNovel> novels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in novels)
        {
            switch (VisualNovelNamesHelper.ValueOf((int)novel.id))
            {
                case VisualNovelNames.BUERO_NOVEL:
                    {
                        StartCoroutine(AnalyseNovel(novel, bueroNovelNumberOfPaths, bueroNovelAnalyseButton));
                        break;
                    }
                default:
                    { break; }
            }
        }
    }

    public void AnalyseFoerderantragNovel()
    {
        foerderantragNovelAnalyseButton.interactable = false;

        if (generatedFeedbackNodes.ContainsKey(VisualNovelNames.FOERDERANTRAG_NOVEL))
        {
            StartCoroutine(StartGeneratingAiFeedback(VisualNovelNames.FOERDERANTRAG_NOVEL, generatedFeedbackNodes[VisualNovelNames.FOERDERANTRAG_NOVEL], foerderantragNovelNumberOfPaths));
            return;
        }

        List<VisualNovel> novels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in novels)
        {
            switch (VisualNovelNamesHelper.ValueOf((int)novel.id))
            {
                case VisualNovelNames.FOERDERANTRAG_NOVEL:
                    {
                        StartCoroutine(AnalyseNovel(novel, foerderantragNovelNumberOfPaths, foerderantragNovelAnalyseButton));
                        break;
                    }
                default:
                    { break; }
            }
        }
    }

    public void AnalyseGruendungszuschussNovel()
    {
        gruendungszuschussNovelAnalyseButton.interactable= false;

        if (generatedFeedbackNodes.ContainsKey(VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL))
        {
            StartCoroutine(StartGeneratingAiFeedback(VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL, generatedFeedbackNodes[VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL], gruendungszuschussNovelNumberOfPaths));
            return;
        }

        List<VisualNovel> novels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in novels)
        {
            switch (VisualNovelNamesHelper.ValueOf((int)novel.id))
            {
                case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                    {
                        StartCoroutine(AnalyseNovel(novel, gruendungszuschussNovelNumberOfPaths, gruendungszuschussNovelAnalyseButton));
                        break;
                    }
                default:
                    { break; }
            }
        }
    }

    public void AnalyseBekannteTreffenNovel()
    {
        bekannteTreffenNovelAnalyseButton.interactable = false;

        if (generatedFeedbackNodes.ContainsKey(VisualNovelNames.BEKANNTE_TREFFEN_NOVEL))
        {
            StartCoroutine(StartGeneratingAiFeedback(VisualNovelNames.BEKANNTE_TREFFEN_NOVEL, generatedFeedbackNodes[VisualNovelNames.BEKANNTE_TREFFEN_NOVEL], elternNovelNumberOfPaths));
            return;
        }

        List<VisualNovel> novels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in novels)
        {
            switch (VisualNovelNamesHelper.ValueOf((int)novel.id))
            {
                case VisualNovelNames.BEKANNTE_TREFFEN_NOVEL:
                    {
                        StartCoroutine(AnalyseNovel(novel, bekannteTreffenNovelNumberOfPaths, bekannteTreffenNovelAnalyseButton));
                        break;
                    }
                default:
                    { break; }
            }
        }
    }

    public void AnalyseBankkreditNovel()
    {
        bankKreditNovelAnalyseButton.interactable= false;

        if (generatedFeedbackNodes.ContainsKey(VisualNovelNames.BANK_KREDIT_NOVEL))
        {
            StartCoroutine(StartGeneratingAiFeedback(VisualNovelNames.BANK_KREDIT_NOVEL, generatedFeedbackNodes[VisualNovelNames.BANK_KREDIT_NOVEL], bankKreditNovelNumberOfPaths));
            return;
        }

        List<VisualNovel> novels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in novels)
        {
            switch (VisualNovelNamesHelper.ValueOf((int)novel.id))
            {
                case VisualNovelNames.BANK_KREDIT_NOVEL:
                    {
                        StartCoroutine(AnalyseNovel(novel, bankKreditNovelNumberOfPaths, bankKreditNovelAnalyseButton));
                        break;
                    }
                default:
                    { break; }
            }
        }
    }

    public void AnalyseHonorarNovel()
    {
        honorarNovelAnalyseButton.interactable = false;

        if (generatedFeedbackNodes.ContainsKey(VisualNovelNames.HONORAR_NOVEL))
        {
            StartCoroutine(StartGeneratingAiFeedback(VisualNovelNames.HONORAR_NOVEL, generatedFeedbackNodes[VisualNovelNames.HONORAR_NOVEL], honorarNovelNumberOfPaths));
            return;
        }

        List<VisualNovel> novels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in novels)
        {
            switch (VisualNovelNamesHelper.ValueOf((int)novel.id))
            {
                case VisualNovelNames.HONORAR_NOVEL:
                    {
                        StartCoroutine(AnalyseNovel(novel, honorarNovelNumberOfPaths, honorarNovelAnalyseButton));
                        break;
                    }
                default:
                    { break; }
            }
        }
    }

    public void AnalyseIntroNovel()
    {
        introNovelAnalyseButton.interactable = false;

        if (generatedFeedbackNodes.ContainsKey(VisualNovelNames.INTRO_NOVEL))
        {
            StartCoroutine(StartGeneratingAiFeedback(VisualNovelNames.INTRO_NOVEL, generatedFeedbackNodes[VisualNovelNames.INTRO_NOVEL], introNovelNumberOfPaths));
            return;
        }

        List<VisualNovel> novels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in novels)
        {
            switch (VisualNovelNamesHelper.ValueOf((int)novel.id))
            {
                case VisualNovelNames.INTRO_NOVEL:
                    {
                        StartCoroutine(AnalyseNovel(novel, introNovelNumberOfPaths, introNovelAnalyseButton));
                        break;
                    }
                default:
                    { break; }
            }
        }
    }

    public void AnalyseLebenspartnerNovel()
    {
        lebenspartnerNovelAnalyseButton.interactable = false;

        if (generatedFeedbackNodes.ContainsKey(VisualNovelNames.LEBENSPARTNER_NOVEL))
        {
            StartCoroutine(StartGeneratingAiFeedback(VisualNovelNames.LEBENSPARTNER_NOVEL, generatedFeedbackNodes[VisualNovelNames.LEBENSPARTNER_NOVEL], lebenspartnerNovelNumberOfPaths));
            return;
        }

        List<VisualNovel> novels = KiteNovelManager.Instance().GetAllKiteNovels();

        foreach (VisualNovel novel in novels)
        {
            switch (VisualNovelNamesHelper.ValueOf((int)novel.id))
            {
                case VisualNovelNames.LEBENSPARTNER_NOVEL:
                    {
                        StartCoroutine(AnalyseNovel(novel, lebenspartnerNovelNumberOfPaths, lebenspartnerNovelAnalyseButton));
                        break;
                    }
                default:
                    { break; }
            }
        }
    }

    public void AddNewFeedback(VisualNovelNames visualNovelName, string path, string completion, TextMeshProUGUI updatePossibility)
    {
        lock (_lockObject)
        {
            FeedbackNodeList list = generatedFeedbackNodes[visualNovelName];

            foreach (FeedbackNodeContainer item in list.feedbackNodes)
            {
                if (path == item.path)
                {
                    item.completion = completion;
                    break;
                }
            }
            offlineFeedbackLoader.SaveOfflineFeedbackForNovelInEditMode(visualNovelName, list);

            int numberOfContainedFeedbacks = 0;

            foreach (FeedbackNodeContainer node in list.feedbackNodes)
            {
                if (!string.IsNullOrEmpty(node.completion))
                {
                    numberOfContainedFeedbacks++;
                }
            }
            updatePossibility.text = "Feedbacks generiert: " + numberOfContainedFeedbacks + " / " + list.feedbackNodes.Count;
        }
    }
}

public class HandleCompletionRequestResult : OnSuccessHandler, OnErrorHandler
{
    VisualNovelNames visualNovelNames;
    GenerateOfflineAiFeeedbackSceneController controller;
    TextMeshProUGUI updatePossibility;
    FeedbackNodeContainer item;

    public HandleCompletionRequestResult(VisualNovelNames visualNovelName, GenerateOfflineAiFeeedbackSceneController controller, TextMeshProUGUI updatePossibility, FeedbackNodeContainer item)
    {
        this.visualNovelNames = visualNovelName;
        this.controller = controller;
        this.updatePossibility = updatePossibility;
        this.item = item;
    }
    public void OnSuccess(Response response)
    {
        controller.AddNewFeedback(visualNovelNames, item.path, response.GetCompletion(), updatePossibility);
    }

    public void OnError(Response response)
    {
        Debug.LogWarning("Error while generating AI Offline Feedback");
    }
}