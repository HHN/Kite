using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.Player;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.UI_Elements.DropDown;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets._Scripts.Controller.SceneControllers
{
    public class NovelHistorySceneController : SceneController
    {
        [SerializeField] private GameObject dataObjectPrefab;
        [SerializeField] private GameObject container;
        [SerializeField] private GameObject noDataObjectsHint;

        [SerializeField] private GameObject containerForBankkreditNovel;
        [SerializeField] private GameObject containerForInvestorNovel;
        [SerializeField] private GameObject containerForElternNovel;
        [SerializeField] private GameObject containerForNotarinNovel;
        [SerializeField] private GameObject containerForPresseNovel;
        [SerializeField] private GameObject containerForBueroNovel;
        [SerializeField] private GameObject containerForHonorarNovel;

        [SerializeField] private List<GameObject> novelPlaceholder = new List<GameObject>();
        [SerializeField] private DropDownMenu dropdownForBankkreditNovel;
        [SerializeField] private DropDownMenu dropdownForInvestorNovel;
        [SerializeField] private DropDownMenu dropdownForElternNovel;
        [SerializeField] private DropDownMenu dropdownForNotarinNovel;
        [SerializeField] private DropDownMenu dropdownForPresseNovel;
        [SerializeField] private DropDownMenu dropdownForBueroNovel;
        [SerializeField] private DropDownMenu dropdownForHonorarNovel;

        [SerializeField] private GameObject spacingForBankkreditNovel;
        [SerializeField] private GameObject spacingForInvestorNovel;
        [SerializeField] private GameObject spacingForElternNovel;
        [SerializeField] private GameObject spacingForNotarinNovel;
        [SerializeField] private GameObject spacingForPresseNovel;
        [SerializeField] private GameObject spacingForBueroNovel;
        [SerializeField] private GameObject spacingForHonorarNovel;

        [SerializeField] private GameObject entryContainerForBankkreditNovel;
        [SerializeField] private GameObject entryContainerForInvestorNovel;
        [SerializeField] private GameObject entryContainerForElternNovel;
        [SerializeField] private GameObject entryContainerForNotarinNovel;
        [SerializeField] private GameObject entryContainerForPresseNovel;
        [SerializeField] private GameObject entryContainerForBueroNovel;
        [SerializeField] private GameObject entryContainerForHonorarNovel;

        [SerializeField] private bool displayContainerForBankkreditNovel;
        [SerializeField] private bool displayContainerForInvestorNovel;
        [SerializeField] private bool displayContainerForElternNovel;
        [SerializeField] private bool displayContainerForNotarinNovel;
        [SerializeField] private bool displayContainerForPresseNovel;
        [SerializeField] private bool displayContainerForBueroNovel;
        [SerializeField] private bool displayContainerForHonorarNovel;

        [SerializeField] private bool displayNoDataObjectsHint;

        [SerializeField] private List<NovelHistoryEntryGuiElement> novelHistoryEntries;

        [SerializeField] private GameObject copyNotificationContainer;

        private Dictionary<long, List<DialogHistoryEntry>> _novelHistoryEntriesDictionary = new();
        private Dictionary<DateTime, long> _dateAndTimeToNovelIdDictionary = new();
        private List<int> novelIdAtIndex = new();

        private void Start()
        {
            if (copyNotificationContainer != null)
            {
                // Das GameObject wurde gefunden, deaktiviere es
                copyNotificationContainer.SetActive(false);
                GameObjectManager.Instance().SetCopyNotification(copyNotificationContainer);
            }

            BackStackManager.Instance().Push(SceneNames.NovelHistoryScene);
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

            SortEntriesAndCreateDictionary(entries);
            
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
            dropdownForInvestorNovel.RebuildLayout();
            dropdownForElternNovel.RebuildLayout();
            dropdownForNotarinNovel.RebuildLayout();
            dropdownForPresseNovel.RebuildLayout();
            dropdownForBueroNovel.RebuildLayout();
            dropdownForHonorarNovel.RebuildLayout();

            yield break;
        }

        private void AddEntry(DialogHistoryEntry entry)
        {
            GameObject entryContainer = GetEntryContainerById(entry.GetNovelId());
            GameObject containerGameObject = GetContainerGameObjectById(entry.GetNovelId());
            DropDownMenu dropDownMenu = GetDropDownMenuById(entry.GetNovelId());

            if (entryContainer == null)
            {
                return;
            }

            displayNoDataObjectsHint = false;

            NovelHistoryEntryGuiElement dataObjectGuiElement = Object.Instantiate(dataObjectPrefab, entryContainer.transform)
                .GetComponent<NovelHistoryEntryGuiElement>();

            dataObjectGuiElement.InitializeEntry(entry);
            novelHistoryEntries.Add(dataObjectGuiElement);

            foreach (DropDownMenu dropdown in dataObjectGuiElement.GetDropDownMenus())
            {
                dropDownMenu.AddChildMenu(dropdown);
            }

            dataObjectGuiElement.AddLayoutToUpdateOnChange(entryContainer.GetComponent<RectTransform>());
            dataObjectGuiElement.AddLayoutToUpdateOnChange(containerGameObject.GetComponent<RectTransform>());
            dataObjectGuiElement.AddLayoutToUpdateOnChange(this.container.GetComponent<RectTransform>());
            dataObjectGuiElement.SetVisualNovelColor(VisualNovelNamesHelper.ValueOf((int)entry.GetNovelId()));
            FontSizeManager.Instance().UpdateAllTextComponents();

            if (!novelIdAtIndex.Contains((int)entry.GetNovelId()))
            {
                novelIdAtIndex.Add((int)entry.GetNovelId());
            }
            
            GetContainerByNovelId(entry.GetNovelId()).transform.SetSiblingIndex(novelIdAtIndex.IndexOf((int)entry.GetNovelId()) * 2);
            GameObject placeholder = novelPlaceholder.Find(obj => obj.name.Contains(VisualNovelNamesHelper.GetName(entry.GetNovelId())));
            placeholder.transform.SetSiblingIndex(novelIdAtIndex.IndexOf((int)entry.GetNovelId()) * 2 + 1);
        }
        
        private void SortEntriesAndCreateDictionary(List<DialogHistoryEntry> entries)
        {
            // Setze die deutsche Kultur für die Datum/Uhrzeit-Formatierung
            CultureInfo culture = new CultureInfo("de-DE");

            // Erstelle eine Liste zum Speichern der Sortierung
            List<KeyValuePair<DateTime, long>> sortedEntries = new List<KeyValuePair<DateTime, long>>();
            List<KeyValuePair<DialogHistoryEntry, DateTime>> sortedEntriesWithOriginal =
                new List<KeyValuePair<DialogHistoryEntry, DateTime>>();
            
            foreach (var entry in entries)
            {
                // Zerlege den HeadButtonText.text anhand von " | " in Teile
                string[] parts = entry.GetDateAndTime().Split('|');

                if (parts.Length < 3)
                    continue; // Falls das Format fehlerhaft ist, überspringe den Eintrag

                string datePart = parts[1].Trim(); // "dd.MM.yyyy"
                string timePart = parts[2].Trim(); // "HH:mm"

                // Datum + Zeit zusammenfügen und in DateTime umwandeln
                if (DateTime.TryParseExact($"{datePart} {timePart}", "dd.MM.yyyy HH:mm", culture,
                        DateTimeStyles.None, out DateTime parsedDate))
                {
                    // Füge das Datum und die NovelId zur Liste hinzu (für das Dictionary)
                    sortedEntries.Add(new KeyValuePair<DateTime, long>(parsedDate, entry.GetNovelId()));

                    // Füge auch das Original-Element und das Datum zur Liste hinzu (für das sortierte Ergebnis)
                    sortedEntriesWithOriginal.Add(new KeyValuePair<DialogHistoryEntry, DateTime>(entry, parsedDate));
                }
                else
                {
                    // Falls das Parsen fehlschlägt, füge das Element mit DateTime.MinValue hinzu
                    sortedEntriesWithOriginal.Add(
                        new KeyValuePair<DialogHistoryEntry, DateTime>(entry, DateTime.MinValue));
                }
            }

            // Sortiere die Einträge nach Datum und Zeit (neueste zuerst)
            sortedEntriesWithOriginal = sortedEntriesWithOriginal
                .OrderByDescending(entry => entry.Value) // Sortiere nach DateTime in absteigender Reihenfolge
                .ThenBy(entry => entry.Key.GetNovelId()) // Danach nach novelId sortieren
                .ToList();

            // Erstelle das Dictionary für die Zuordnung von DateTime zu NovelId
            _dateAndTimeToNovelIdDictionary.Clear(); // Leere das Dictionary, falls es vorher schon existiert

            foreach (var entry in sortedEntries)
            {
                // Füge den Wert ins Dictionary ein
                _dateAndTimeToNovelIdDictionary[entry.Key] = entry.Value;
            }

            // Die sortierten DialogHistoryEntry-Elemente in der richtigen Reihenfolge zurückgeben
            entries.Clear();
            entries.AddRange(sortedEntriesWithOriginal.Select(entry => entry.Key));
        }
        
        private GameObject GetContainerByNovelId(long novelId)
        {
            // Dies ist ein Beispiel, wie du den richtigen Container basierend auf der NovelId zurückgeben kannst.
            switch (VisualNovelNamesHelper.GetName(novelId))
            {
                case "Bankkredit":
                    return containerForBankkreditNovel;
                case "Investor":
                    return containerForInvestorNovel;
                case "Eltern":
                    return containerForElternNovel;
                case "Notarin":
                    return containerForNotarinNovel;
                case "Presse":
                    return containerForPresseNovel;
                case "Vermieter":
                    return containerForBueroNovel;
                case "Honorar":
                    return containerForHonorarNovel;
                default:
                    return null; // Falls keine passende NovelId gefunden wird
            }
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
                case VisualNovelNames.INVESTOR_NOVEL:
                {
                    displayContainerForInvestorNovel = true;
                    return dropdownForInvestorNovel;
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
                case VisualNovelNames.VERMIETER_NOVEL:
                {
                    displayContainerForBueroNovel = true;
                    return dropdownForBueroNovel;
                }
                case VisualNovelNames.HONORAR_NOVEL:
                {
                    displayContainerForHonorarNovel = true;
                    return dropdownForHonorarNovel;
                }
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
                case VisualNovelNames.INVESTOR_NOVEL:
                {
                    displayContainerForInvestorNovel = true;
                    return containerForInvestorNovel;
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
                case VisualNovelNames.VERMIETER_NOVEL:
                {
                    displayContainerForBueroNovel = true;
                    return containerForBueroNovel;
                }
                case VisualNovelNames.HONORAR_NOVEL:
                {
                    displayContainerForHonorarNovel = true;
                    return containerForHonorarNovel;
                }
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
                case VisualNovelNames.INVESTOR_NOVEL:
                {
                    displayContainerForInvestorNovel = true;
                    return entryContainerForInvestorNovel;
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
                case VisualNovelNames.VERMIETER_NOVEL:
                {
                    displayContainerForBueroNovel = true;
                    return entryContainerForBueroNovel;
                }
                case VisualNovelNames.HONORAR_NOVEL:
                {
                    displayContainerForHonorarNovel = true;
                    return entryContainerForHonorarNovel;
                }
                default:
                {
                    return null;
                }
            }
        }

        private void InitializeBooleans()
        {
            displayContainerForBankkreditNovel = false;
            displayContainerForInvestorNovel = false;
            displayContainerForElternNovel = false;
            displayContainerForNotarinNovel = false;
            displayContainerForPresseNovel = false;
            displayContainerForBueroNovel = false;
            displayContainerForHonorarNovel = false;
            displayNoDataObjectsHint = true;
        }

        private void SetVisibilityOfUiElements()
        {
            containerForBankkreditNovel.SetActive(displayContainerForBankkreditNovel);
            spacingForBankkreditNovel.SetActive(displayContainerForBankkreditNovel);
            containerForInvestorNovel.SetActive(displayContainerForInvestorNovel);
            spacingForInvestorNovel.SetActive(displayContainerForInvestorNovel);
            containerForElternNovel.SetActive(displayContainerForElternNovel);
            spacingForElternNovel.SetActive(displayContainerForElternNovel);
            containerForNotarinNovel.SetActive(displayContainerForNotarinNovel);
            spacingForNotarinNovel.SetActive(displayContainerForNotarinNovel);
            containerForPresseNovel.SetActive(displayContainerForPresseNovel);
            spacingForPresseNovel.SetActive(displayContainerForPresseNovel);
            containerForBueroNovel.SetActive(displayContainerForBueroNovel);
            spacingForBueroNovel.SetActive(displayContainerForBueroNovel);
            containerForHonorarNovel.SetActive(displayContainerForHonorarNovel);
            spacingForHonorarNovel.SetActive(displayContainerForHonorarNovel);
            noDataObjectsHint.SetActive(displayNoDataObjectsHint);
        }
    }
}