using System.Collections;
using System.Collections.Generic;
using _00_Kite2.Common.Managers;
using _00_Kite2.Common.SceneManagement;
using _00_Kite2.Common.UI.UI_Elements.DropDown;
using _00_Kite2.Player;
using _00_Kite2.UserFeedback;
using UnityEngine;
using UnityEngine.Serialization;

namespace _00_Kite2.Common
{
    public class NovelHistorySceneController : SceneController
    {
        [SerializeField] private GameObject dataObjectPrefab;
        [SerializeField] private GameObject container;
        [SerializeField] private GameObject noDataObjectsHint;

        [SerializeField] private GameObject containerForBankkreditNovel;
        [SerializeField] private GameObject containerForBekanntenTreffenNovel;
        [SerializeField] private GameObject containerForBankkontoNovel;
        [SerializeField] private GameObject containerForFoerderantragNovel;
        [SerializeField] private GameObject containerForElternNovel;
        [SerializeField] private GameObject containerForNotarinNovel;
        [SerializeField] private GameObject containerForPresseNovel;
        [SerializeField] private GameObject containerForBueroNovel;
        [SerializeField] private GameObject containerForGruendungszuschussNovel;
        [SerializeField] private GameObject containerForHonorarNovel;
        [SerializeField] private GameObject containerForLebenspartnerinNovel;
        //[SerializeField] private GameObject containerForIntroNovel;    

        [SerializeField] private DropDownMenu dropdownForBankkreditNovel;
        [SerializeField] private DropDownMenu dropdownForBekanntenTreffenNovel;
        [SerializeField] private DropDownMenu dropdownForBankkontoNovel;
        [SerializeField] private DropDownMenu dropdownForFoerderantragNovel;
        [SerializeField] private DropDownMenu dropdownForElternNovel;
        [SerializeField] private DropDownMenu dropdownForNotarinNovel;
        [SerializeField] private DropDownMenu dropdownForPresseNovel;
        [SerializeField] private DropDownMenu dropdownForBueroNovel;
        [SerializeField] private DropDownMenu dropdownForGruendungszuschussNovel;
        [SerializeField] private DropDownMenu dropdownForHonorarNovel;
        [SerializeField] private DropDownMenu dropdownForLebenspartnerinNovel;
        //[SerializeField] private DropDownMenu dropdownForIntroNovel;        

        [SerializeField] private GameObject spacingForBankkreditNovel;
        [SerializeField] private GameObject spacingForBekanntenTreffenNovel;
        [SerializeField] private GameObject spacingForBankkontoNovel;
        [SerializeField] private GameObject spacingForFoerderantragNovel;
        [SerializeField] private GameObject spacingForElternNovel;
        [SerializeField] private GameObject spacingForNotarinNovel;
        [SerializeField] private GameObject spacingForPresseNovel;
        [SerializeField] private GameObject spacingForBueroNovel;
        [SerializeField] private GameObject spacingForGruendungszuschussNovel;
        [SerializeField] private GameObject spacingForHonorarNovel;
        [SerializeField] private GameObject spacingForLebenspartnerinNovel;

        [SerializeField] private GameObject entryContainerForBankkreditNovel;
        [SerializeField] private GameObject entryContainerForBekanntenTreffenNovel;
        [SerializeField] private GameObject entryContainerForBankkontoNovel;
        [SerializeField] private GameObject entryContainerForFoerderantragNovel;
        [SerializeField] private GameObject entryContainerForElternNovel;
        [SerializeField] private GameObject entryContainerForNotarinNovel;
        [SerializeField] private GameObject entryContainerForPresseNovel;
        [SerializeField] private GameObject entryContainerForBueroNovel;
        [SerializeField] private GameObject entryContainerForGruendungszuschussNovel;
        [SerializeField] private GameObject entryContainerForHonorarNovel;
        [SerializeField] private GameObject entryContainerForLebenspartnerinNovel;
        //[SerializeField] private GameObject entryContainerForIntroNovel;

        [SerializeField] private bool displayContainerForBankkreditNovel;
        [SerializeField] private bool displayContainerForBekanntenTreffenNovel;
        [SerializeField] private bool displayContainerForBankkontoNovel;
        [SerializeField] private bool displayContainerForFoerderantragNovel;
        [SerializeField] private bool displayContainerForElternNovel;
        [SerializeField] private bool displayContainerForNotarinNovel;
        [SerializeField] private bool displayContainerForPresseNovel;
        [SerializeField] private bool displayContainerForBueroNovel;
        [SerializeField] private bool displayContainerForGruendungszuschussNovel;
        [SerializeField] private bool displayContainerForHonorarNovel;

        [SerializeField] private bool displayContainerForLebenspartnerinNovel;

        //[SerializeField] private bool displayContainerForIntroNovel;
        [SerializeField] private bool displayNoDataObjectsHint;

        [SerializeField] private List<NovelHistoryEntryGuiElement> novelHistoryEntries;

        [SerializeField] private GameObject copyNotificationContainer;

        private void Start()
        {
            if (copyNotificationContainer != null)
            {
                // Das GameObject wurde gefunden, deaktiviere es
                copyNotificationContainer.SetActive(false);
                GameObjectManager.Instance().SetCopyNotification(copyNotificationContainer);
            }

            BackStackManager.Instance().Push(SceneNames.NOVEL_HISTORY_SCENE);
            novelHistoryEntries = new List<NovelHistoryEntryGuiElement>();

            InitializeBooleans();

            List<DialogHistoryEntry> entries = DialogHistoryManager.Instance().GetEntries();

            if (entries == null)
            {
                SetVisibilityOfUiElements();
                return;
            }

            if (entries.Count == 0)
            {
                SetVisibilityOfUiElements();
                return;
            }

            foreach (DialogHistoryEntry dataObject in entries)
            {
                AddEntry(dataObject);
            }

            SetVisibilityOfUiElements();

            StartCoroutine(RebuildLayout());
        }

        private IEnumerator RebuildLayout()
        {
            dropdownForBankkreditNovel.RebuildLayout();
            dropdownForBekanntenTreffenNovel.RebuildLayout();
            dropdownForBankkontoNovel.RebuildLayout();
            dropdownForFoerderantragNovel.RebuildLayout();
            dropdownForElternNovel.RebuildLayout();
            dropdownForNotarinNovel.RebuildLayout();
            dropdownForPresseNovel.RebuildLayout();
            dropdownForBueroNovel.RebuildLayout();
            dropdownForGruendungszuschussNovel.RebuildLayout();
            dropdownForHonorarNovel.RebuildLayout();
            dropdownForLebenspartnerinNovel.RebuildLayout();
            //dropdownForIntroNovel.RebuildLayout();

            yield break;
        }

        private void AddEntry(DialogHistoryEntry entry)
        {
            GameObject container = GetEntryContainerById(entry.GetNovelId());
            GameObject containerGameObject = GetContainerGameObjectById(entry.GetNovelId());
            DropDownMenu dropDownMenu = GetDropDownMenuById(entry.GetNovelId());

            if (container == null)
            {
                return;
            }

            displayNoDataObjectsHint = false;

            NovelHistoryEntryGuiElement dataObjectGuiElement = Instantiate(dataObjectPrefab, container.transform).GetComponent<NovelHistoryEntryGuiElement>();

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
                case VisualNovelNames.BEKANNTEN_TREFFEN_NOVEL:
                {
                    displayContainerForBekanntenTreffenNovel = true;
                    return dropdownForBekanntenTreffenNovel;
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
                    displayContainerForPresseNovel = true;
                    return dropdownForPresseNovel;
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
                case VisualNovelNames.BEKANNTEN_TREFFEN_NOVEL:
                {
                    displayContainerForBekanntenTreffenNovel = true;
                    return containerForBekanntenTreffenNovel;
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
                    displayContainerForPresseNovel = true;
                    return containerForPresseNovel;
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

        private GameObject GetEntryContainerById(long novelId)
        {
            VisualNovelNames novelNames = VisualNovelNamesHelper.ValueOf((int)novelId);

            switch (novelNames)
            {
                case VisualNovelNames.BANK_KREDIT_NOVEL:
                {
                    displayContainerForBankkreditNovel = true;
                    return entryContainerForBankkreditNovel;
                }
                case VisualNovelNames.BEKANNTEN_TREFFEN_NOVEL:
                {
                    displayContainerForBekanntenTreffenNovel = true;
                    return entryContainerForBekanntenTreffenNovel;
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
                    displayContainerForPresseNovel = true;
                    return entryContainerForPresseNovel;
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

        private void InitializeBooleans()
        {
            displayContainerForBankkreditNovel = false;
            displayContainerForBekanntenTreffenNovel = false;
            displayContainerForBankkontoNovel = false;
            displayContainerForFoerderantragNovel = false;
            displayContainerForElternNovel = false;
            displayContainerForNotarinNovel = false;
            displayContainerForPresseNovel = false;
            displayContainerForBueroNovel = false;
            displayContainerForGruendungszuschussNovel = false;
            displayContainerForHonorarNovel = false;
            displayContainerForLebenspartnerinNovel = false;
            //displayContainerForIntroNovel = false;
            displayNoDataObjectsHint = true;
        }

        private void SetVisibilityOfUiElements()
        {
            containerForBankkreditNovel.SetActive(displayContainerForBankkreditNovel);
            spacingForBankkreditNovel.SetActive(displayContainerForBankkreditNovel);
            containerForBekanntenTreffenNovel.SetActive(displayContainerForBekanntenTreffenNovel);
            spacingForBekanntenTreffenNovel.SetActive(displayContainerForBekanntenTreffenNovel);
            containerForBankkontoNovel.SetActive(displayContainerForBankkontoNovel);
            spacingForBankkontoNovel.SetActive(displayContainerForBankkontoNovel);
            containerForFoerderantragNovel.SetActive(displayContainerForFoerderantragNovel);
            spacingForFoerderantragNovel.SetActive(displayContainerForFoerderantragNovel);
            containerForElternNovel.SetActive(displayContainerForElternNovel);
            spacingForElternNovel.SetActive(displayContainerForElternNovel);
            containerForNotarinNovel.SetActive(displayContainerForNotarinNovel);
            spacingForNotarinNovel.SetActive(displayContainerForNotarinNovel);
            containerForPresseNovel.SetActive(displayContainerForPresseNovel);
            spacingForPresseNovel.SetActive(displayContainerForPresseNovel);
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
}