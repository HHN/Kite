using System;
using System.Collections;
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
    //[SerializeField] private GameObject containerForIntroNovel;    
    
    [SerializeField] private DropDownMenu dropdownForBankkreditNovel;
    [SerializeField] private DropDownMenu dropdownForBekannteTreffenNovel;
    [SerializeField] private DropDownMenu dropdownForBankkontoNovel;
    [SerializeField] private DropDownMenu dropdownForFoerderantragNovel;
    [SerializeField] private DropDownMenu dropdownForElternNovel;
    [SerializeField] private DropDownMenu dropdownForNotarinNovel;
    [SerializeField] private DropDownMenu dropdownForPresseeNovel;
    [SerializeField] private DropDownMenu dropdownForBueroNovel;
    [SerializeField] private DropDownMenu dropdownForGruendungszuschussNovel;
    [SerializeField] private DropDownMenu dropdownForHonorarNovel;
    [SerializeField] private DropDownMenu dropdownForLebenspartnerinNovel;
    //[SerializeField] private DropDownMenu dropdownForIntroNovel;        
    
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
    //[SerializeField] private GameObject entryContainerForIntroNovel;

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
    //[SerializeField] private bool displayContainerForIntroNovel;
    [SerializeField] private bool displayNoDataObjectsHint;

    [SerializeField] private List<NovelHistoryEntryGuiElement> novelHistoryEntries;

    [SerializeField] private GameObject copyNotificationContainer;

    void Start()
    {
        if (copyNotificationContainer != null)
        {
            // Das GameObject wurde gefunden, deaktiviere es
            copyNotificationContainer.SetActive(false);
            GameObjectManager.Instance().SetCopyNotification(copyNotificationContainer);
        }
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

        StartCoroutine(RebuildLayout());
    }

    public IEnumerator RebuildLayout()
    {
        dropdownForBankkreditNovel.RebuildLayout();
        dropdownForBekannteTreffenNovel.RebuildLayout();
        dropdownForBankkontoNovel.RebuildLayout();
        dropdownForFoerderantragNovel.RebuildLayout();
        dropdownForElternNovel.RebuildLayout();
        dropdownForNotarinNovel.RebuildLayout();
        dropdownForPresseeNovel.RebuildLayout();
        dropdownForBueroNovel.RebuildLayout();
        dropdownForGruendungszuschussNovel.RebuildLayout();
        dropdownForHonorarNovel.RebuildLayout();
        dropdownForLebenspartnerinNovel.RebuildLayout();
        //dropdownForIntroNovel.RebuildLayout();

        yield break;
    }

    private void AddEntry(DialogHistoryEntry entry)
    {
        GameObject container = GetEntyContainerById(entry.GetNovelId());
        GameObject containerGameObject = GetContainerGameObjectById(entry.GetNovelId());
        DropDownMenu dropDownMenu = GetDropDownMenuById(entry.GetNovelId());

        if (container == null) { return; }

        displayNoDataObjectsHint = false;

        NovelHistoryEntryGuiElement dataObjectGuiElement =
    Instantiate(dataObjectPrefab, container.transform)
    .GetComponent<NovelHistoryEntryGuiElement>();

        dataObjectGuiElement.InitializeEntry(entry);
        novelHistoryEntries.Add(dataObjectGuiElement);
        
        foreach (DropDownMenu dropdown in dataObjectGuiElement.GetDropDownMenus())
        {
            dropDownMenu.AddChildMenu(dropdown);
        }

        dataObjectGuiElement.AddLayoutToUpdateOnChange(container.GetComponent<RectTransform>());
        dataObjectGuiElement.AddLayoutToUpdateOnChange(containerGameObject.GetComponent<RectTransform>());
        dataObjectGuiElement.AddLayoutToUpdateOnChange(this.container.GetComponent<RectTransform>());
        dataObjectGuiElement.SetVisualNovelColor(VisualNovelNamesHelper.ValueOf((int)entry.GetNovelId()));
        FontSizeManager.Instance().UpdateAllTextComponents();
    }

    private DropDownMenu GetDropDownMenuById(long novelId)
    {
        VisualNovelNames novelNames = VisualNovelNamesHelper.ValueOf((int)novelId);

        switch (novelNames)
        {
            case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    displayContainerForBankkreditNovel = true;
                    return dropdownForBankkreditNovel;
                }
            case VisualNovelNames.BEKANNTE_TREFFEN_NOVEL:
                {
                    displayContainerForBekannteTreffenNovel = true;
                    return dropdownForBekannteTreffenNovel;
                }
            case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    displayContainerForBankkontoNovel = true;
                    return dropdownForBankkontoNovel;
                }
            case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    displayContainerForFoerderantragNovel = true;
                    return dropdownForFoerderantragNovel;
                }
            case VisualNovelNames.ELTERN_NOVEL:
                {
                    displayContainerForElternNovel = true;
                    return dropdownForElternNovel;
                }
            case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    displayContainerForNotarinNovel = true;
                    return dropdownForNotarinNovel;
                }
            case VisualNovelNames.PRESSE_NOVEL:
                {
                    displayContainerForPresseeNovel = true;
                    return dropdownForPresseeNovel;
                }
            case VisualNovelNames.BUERO_NOVEL:
                {
                    displayContainerForBueroNovel = true;
                    return dropdownForBueroNovel;
                }
            case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    displayContainerForGruendungszuschussNovel = true;
                    return dropdownForGruendungszuschussNovel;
                }
            case VisualNovelNames.HONORAR_NOVEL:
                {
                    displayContainerForHonorarNovel = true;
                    return dropdownForHonorarNovel;
                }
            case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    displayContainerForLebenspartnerinNovel = true;
                    return dropdownForLebenspartnerinNovel;
                }
            //case VisualNovelNames.INTRO_NOVEL:
            //    {
            //        displayContainerForIntroNovel = true;
            //        return dropdownForIntroNovel;
            //    }
            default:
                {
                    return null;
                }
        }
    }

    private GameObject GetContainerGameObjectById(long novelId)
    {
        VisualNovelNames novelNames = VisualNovelNamesHelper.ValueOf((int)novelId);

        switch (novelNames)
        {
            case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    displayContainerForBankkreditNovel = true;
                    return containerForBankkreditNovel;
                }
            case VisualNovelNames.BEKANNTE_TREFFEN_NOVEL:
                {
                    displayContainerForBekannteTreffenNovel = true;
                    return containerForBekannteTreffenNovel;
                }
            case VisualNovelNames.BANK_KONTO_NOVEL:
                {
                    displayContainerForBankkontoNovel = true;
                    return containerForBankkontoNovel;
                }
            case VisualNovelNames.FOERDERANTRAG_NOVEL:
                {
                    displayContainerForFoerderantragNovel = true;
                    return containerForFoerderantragNovel;
                }
            case VisualNovelNames.ELTERN_NOVEL:
                {
                    displayContainerForElternNovel = true;
                    return containerForElternNovel;
                }
            case VisualNovelNames.NOTARIAT_NOVEL:
                {
                    displayContainerForNotarinNovel = true;
                    return containerForNotarinNovel;
                }
            case VisualNovelNames.PRESSE_NOVEL:
                {
                    displayContainerForPresseeNovel = true;
                    return containerForPresseeNovel;
                }
            case VisualNovelNames.BUERO_NOVEL:
                {
                    displayContainerForBueroNovel = true;
                    return containerForBueroNovel;
                }
            case VisualNovelNames.GRUENDER_ZUSCHUSS_NOVEL:
                {
                    displayContainerForGruendungszuschussNovel = true;
                    return containerForGruendungszuschussNovel;
                }
            case VisualNovelNames.HONORAR_NOVEL:
                {
                    displayContainerForHonorarNovel = true;
                    return containerForHonorarNovel;
                }
            case VisualNovelNames.LEBENSPARTNER_NOVEL:
                {
                    displayContainerForLebenspartnerinNovel = true;
                    return containerForLebenspartnerinNovel;
                }
            //case VisualNovelNames.INTRO_NOVEL:
            //    {
            //        displayContainerForIntroNovel = true;
            //        return containerForIntroNovel;
            //    }
            default:
                {
                    return null;
                }
        }
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
            //case VisualNovelNames.INTRO_NOVEL:
            //    {
            //        displayContainerForIntroNovel = true;
            //        return entryContainerForIntroNovel;
            //    }
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
        //displayContainerForIntroNovel = false;
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
        //containerForIntroNovel.SetActive(displayContainerForIntroNovel);
        noDataObjectsHint.SetActive(displayNoDataObjectsHint);
    }
}
