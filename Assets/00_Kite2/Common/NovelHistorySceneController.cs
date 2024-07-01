using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NovelHistorySceneController : SceneController
{
    [SerializeField] private GameObject dataObjectPrefab;
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject noDataObjectsHint;

    [SerializeField] private GameObject containerForBankkreditNovel;
    [SerializeField] private GameObject containerForBekannteTreffenNovel;
    [SerializeField] private GameObject containerForBankkontoNovel;
    [SerializeField] private GameObject containerForFoerderantragNovel;
    [SerializeField] private GameObject containerForElternNovel;
    [SerializeField] private GameObject containerForNotarinNovel;
    [SerializeField] private GameObject containerForPresseeNovel;
    [SerializeField] private GameObject containerForBueroNovel;
    [SerializeField] private GameObject containerForGruendungszuschussNovel;
    [SerializeField] private GameObject containerForHonorarNovel;
    [SerializeField] private GameObject containerForLebenspartnerinNovel;
    [SerializeField] private GameObject containerForIntroNovel;        
    
    [SerializeField] private GameObject spacingForBankkreditNovel;
    [SerializeField] private GameObject spacingForBekannteTreffenNovel;
    [SerializeField] private GameObject spacingForBankkontoNovel;
    [SerializeField] private GameObject spacingForFoerderantragNovel;
    [SerializeField] private GameObject spacingForElternNovel;
    [SerializeField] private GameObject spacingForNotarinNovel;
    [SerializeField] private GameObject spacingForPresseeNovel;
    [SerializeField] private GameObject spacingForBueroNovel;
    [SerializeField] private GameObject spacingForGruendungszuschussNovel;
    [SerializeField] private GameObject spacingForHonorarNovel;
    [SerializeField] private GameObject spacingForLebenspartnerinNovel;
    
    [SerializeField] private GameObject entryContainerForBankkreditNovel;
    [SerializeField] private GameObject entryContainerForBekannteTreffenNovel;
    [SerializeField] private GameObject entryContainerForBankkontoNovel;
    [SerializeField] private GameObject entryContainerForFoerderantragNovel;
    [SerializeField] private GameObject entryContainerForElternNovel;
    [SerializeField] private GameObject entryContainerForNotarinNovel;
    [SerializeField] private GameObject entryContainerForPresseeNovel;
    [SerializeField] private GameObject entryContainerForBueroNovel;
    [SerializeField] private GameObject entryContainerForGruendungszuschussNovel;
    [SerializeField] private GameObject entryContainerForHonorarNovel;
    [SerializeField] private GameObject entryContainerForLebenspartnerinNovel;
    [SerializeField] private GameObject entryContainerForIntroNovel;

    [SerializeField] private bool displayContainerForBankkreditNovel;
    [SerializeField] private bool displayContainerForBekannteTreffenNovel;
    [SerializeField] private bool displayContainerForBankkontoNovel;
    [SerializeField] private bool displayContainerForFoerderantragNovel;
    [SerializeField] private bool displayContainerForElternNovel;
    [SerializeField] private bool displayContainerForNotarinNovel;
    [SerializeField] private bool displayContainerForPresseeNovel;
    [SerializeField] private bool displayContainerForBueroNovel;
    [SerializeField] private bool displayContainerForGruendungszuschussNovel;
    [SerializeField] private bool displayContainerForHonorarNovel;
    [SerializeField] private bool displayContainerForLebenspartnerinNovel;
    [SerializeField] private bool displayContainerForIntroNovel;
    [SerializeField] private bool displayNoDataObjectsHint;

    [SerializeField] private List<NovelHistoryEntryGuiElement> novelHistoryEntries;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.NOVEL_HISTORY_SCENE);
        novelHistoryEntries = new List<NovelHistoryEntryGuiElement>();

        InitialiizeBooleans();

        List<DialogHistoryEntry> entries = DialogHistoryManager.Instance().GetEntries();

        if (entries == null)
        {
            SetVisiibilityOfUiElements();
            return;
        }

        if (entries.Count == 0 )
        {
            SetVisiibilityOfUiElements();
            return;
        }
        foreach (DialogHistoryEntry dataObject in entries)
        {
            AddEntry(dataObject);
        }

        SetVisiibilityOfUiElements();
        RebuildLayout();
    }

    public void RebuildLayout()
    {
        foreach (NovelHistoryEntryGuiElement entry in novelHistoryEntries)
        {
            entry.RebuildLayout();
        }
        if (displayContainerForBankkreditNovel) 
        { LayoutRebuilder.ForceRebuildLayoutImmediate(entryContainerForBankkreditNovel.GetComponent<RectTransform>()); 
        }
        if (displayContainerForBekannteTreffenNovel)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(entryContainerForBekannteTreffenNovel.GetComponent<RectTransform>());
        }
        if (displayContainerForBankkontoNovel)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(entryContainerForBankkontoNovel.GetComponent<RectTransform>());
        }
        if (displayContainerForFoerderantragNovel)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(entryContainerForFoerderantragNovel.GetComponent<RectTransform>());
        }
        if (displayContainerForElternNovel)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(entryContainerForElternNovel.GetComponent<RectTransform>());
        }
        if (displayContainerForNotarinNovel)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(entryContainerForNotarinNovel.GetComponent<RectTransform>());
        }
        if (displayContainerForPresseeNovel)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(entryContainerForPresseeNovel.GetComponent<RectTransform>());
        }
        if (displayContainerForBueroNovel)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(entryContainerForBueroNovel.GetComponent<RectTransform>());
        }
        if (displayContainerForGruendungszuschussNovel)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(entryContainerForGruendungszuschussNovel.GetComponent<RectTransform>());
        }
        if (displayContainerForHonorarNovel)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(entryContainerForHonorarNovel.GetComponent<RectTransform>());
        }
        if (displayContainerForLebenspartnerinNovel)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(entryContainerForLebenspartnerinNovel.GetComponent<RectTransform>());
        }
        if (displayContainerForIntroNovel)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(entryContainerForIntroNovel.GetComponent<RectTransform>());
        }

        RectTransform rectTransform = container.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }

    private void AddEntry(DialogHistoryEntry entry)
    {
        GameObject container = GetEntyContainerById(entry.GetNovelId());

        if (container == null) { return; }

        displayNoDataObjectsHint = false;

        NovelHistoryEntryGuiElement dataObjectGuiElement =
    Instantiate(dataObjectPrefab, container.transform)
    .GetComponent<NovelHistoryEntryGuiElement>();

        dataObjectGuiElement.InitializeEntry(entry);
        dataObjectGuiElement.SetNovelHistorySceneController(this);
        novelHistoryEntries.Add(dataObjectGuiElement);
    }

    private GameObject GetEntyContainerById(long novelId)
    {
        VisualNovelNames novelNames = VisualNovelNamesHelper.ValueOf((int) novelId);

        switch (novelNames)
        {
            case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    displayContainerForBankkreditNovel = true;
                    return entryContainerForBankkreditNovel;
                }
            case VisualNovelNames.BEKANNTE_TREFFEN_NOVEL:
                {
                    displayContainerForBekannteTreffenNovel = true;
                    return entryContainerForBekannteTreffenNovel;
                }
            case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    displayContainerForBankkontoNovel = true;
                    return entryContainerForBankkontoNovel;
                }           
            case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    displayContainerForFoerderantragNovel = true;
                    return entryContainerForFoerderantragNovel;
                }
            case VisualNovelNames.ELTERN_NOVEL:
                {
                    displayContainerForElternNovel = true;
                    return entryContainerForElternNovel;
                }
            case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    displayContainerForNotarinNovel = true;
                    return entryContainerForNotarinNovel;
                }
            case VisualNovelNames.PRESSE_NOVEL:
                {
                    displayContainerForPresseeNovel = true;
                    return entryContainerForPresseeNovel;
                }
            case VisualNovelNames.BUERO_NOVEL:
                {
                    displayContainerForBueroNovel = true;
                    return entryContainerForBueroNovel;
                }
            case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    displayContainerForGruendungszuschussNovel = true;
                    return entryContainerForGruendungszuschussNovel;
                }
            case VisualNovelNames.HONORAR_NOVEL:
                {
                    displayContainerForHonorarNovel = true;
                    return entryContainerForHonorarNovel;
                }
            case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    displayContainerForLebenspartnerinNovel = true;
                    return entryContainerForLebenspartnerinNovel;
                }
            case VisualNovelNames.INTRO_NOVEL:
                {
                    displayContainerForIntroNovel = true;
                    return entryContainerForIntroNovel;
                }
            default:
                {
                    return null;
                }
        }
    }

    private void InitialiizeBooleans()
    {
        displayContainerForBankkreditNovel = false;
        displayContainerForBekannteTreffenNovel = false;
        displayContainerForBankkontoNovel = false;
        displayContainerForFoerderantragNovel = false;
        displayContainerForElternNovel = false;
        displayContainerForNotarinNovel = false;
        displayContainerForPresseeNovel = false;
        displayContainerForBueroNovel = false;
        displayContainerForGruendungszuschussNovel = false;
        displayContainerForHonorarNovel = false;
        displayContainerForLebenspartnerinNovel = false;
        displayContainerForIntroNovel = false;
        displayNoDataObjectsHint = true;
    }

    private void SetVisiibilityOfUiElements()
    {
        containerForBankkreditNovel.SetActive(displayContainerForBankkreditNovel);
        spacingForBankkreditNovel.SetActive(displayContainerForBankkreditNovel);
        containerForBekannteTreffenNovel.SetActive(displayContainerForBekannteTreffenNovel);
        spacingForBekannteTreffenNovel.SetActive(displayContainerForBekannteTreffenNovel);
        containerForBankkontoNovel.SetActive(displayContainerForBankkontoNovel);
        spacingForBankkontoNovel.SetActive(displayContainerForBankkontoNovel);
        containerForFoerderantragNovel.SetActive(displayContainerForFoerderantragNovel);
        spacingForFoerderantragNovel.SetActive(displayContainerForFoerderantragNovel);
        containerForElternNovel.SetActive(displayContainerForElternNovel);
        spacingForElternNovel.SetActive(displayContainerForElternNovel);
        containerForNotarinNovel.SetActive(displayContainerForNotarinNovel);
        spacingForNotarinNovel.SetActive(displayContainerForNotarinNovel);
        containerForPresseeNovel.SetActive(displayContainerForPresseeNovel);
        spacingForPresseeNovel.SetActive(displayContainerForPresseeNovel);
        containerForBueroNovel.SetActive(displayContainerForBueroNovel);
        spacingForBueroNovel.SetActive(displayContainerForBueroNovel);
        containerForGruendungszuschussNovel.SetActive(displayContainerForGruendungszuschussNovel);
        spacingForGruendungszuschussNovel.SetActive(displayContainerForGruendungszuschussNovel);
        containerForHonorarNovel.SetActive(displayContainerForHonorarNovel);
        spacingForHonorarNovel.SetActive(displayContainerForHonorarNovel);
        containerForLebenspartnerinNovel.SetActive(displayContainerForLebenspartnerinNovel);
        spacingForLebenspartnerinNovel.SetActive(displayContainerForLebenspartnerinNovel);
        containerForIntroNovel.SetActive(displayContainerForIntroNovel);
        noDataObjectsHint.SetActive(displayNoDataObjectsHint);
    }
}
