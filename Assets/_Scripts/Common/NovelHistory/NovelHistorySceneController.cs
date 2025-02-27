using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Assets._Scripts.Common.Managers;
using Assets._Scripts.Common.SceneManagement;
using Assets._Scripts.Common.UI_Elements.DropDown;
using Assets._Scripts.Player;
using UnityEngine;

namespace Assets._Scripts.Common.NovelHistory
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

        [SerializeField] private List<GameObject> novelPlaceholder = new List<GameObject>();
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

            NovelHistoryEntryGuiElement dataObjectGuiElement = Instantiate(dataObjectPrefab, entryContainer.transform)
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
                case "Bekannten treffen":
                    return containerForBekanntenTreffenNovel;
                case "Bankkonto":
                    return containerForBankkontoNovel;
                case "Förderantrag":
                    return containerForFoerderantragNovel;
                case "Eltern":
                    return containerForElternNovel;
                case "Notarin":
                    return containerForNotarinNovel;
                case "Presse":
                    return containerForPresseNovel;
                case "Büro":
                    return containerForBueroNovel;
                case "Gründungs-zuschuss":
                    return containerForGruendungszuschussNovel;
                case "Honorar":
                    return containerForHonorarNovel;
                case "Lebens-partner*in":
                    return containerForLebenspartnerinNovel;
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
            noDataObjectsHint.SetActive(displayNoDataObjectsHint);
        }

        private List<DialogHistoryEntry> SortEntries(List<DialogHistoryEntry> entries)
        {
            // Liste zum Speichern der sortierten Datumswerte und der zugehörigen Original-Elemente
            List<KeyValuePair<DialogHistoryEntry, DateTime>> sortedEntries =
                new List<KeyValuePair<DialogHistoryEntry, DateTime>>();

            // Setze die deutsche Kultur für die Datum/Uhrzeit-Formatierung
            CultureInfo culture = new CultureInfo("de-DE");

            // Sortiere novelHistoryEntries basierend auf dem Datum in HeadButtonText.text
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
                    // Füge das Element und das Datum als Paar zur Liste hinzu
                    sortedEntries.Add(new KeyValuePair<DialogHistoryEntry, DateTime>(entry, parsedDate));
                }
                else
                {
                    // Falls das Parsen fehlschlägt, füge den Eintrag mit MinValue hinzu
                    sortedEntries.Add(new KeyValuePair<DialogHistoryEntry, DateTime>(entry, DateTime.MinValue));
                }
            }

            // Zuerst nach novelID sortieren, dann nach DateAndTime
            sortedEntries = sortedEntries
                .OrderBy(entry => entry.Key.GetNovelId()) // Zuerst nach novelID sortieren
                .ThenBy(entry => entry.Value) // Dann nach DateAndTime sortieren
                .Reverse() // Optional: Umkehren der Reihenfolge (falls erforderlich)
                .ToList();

            // Liste der sortierten DialogHistoryEntries basierend auf novelID und DateAndTime
            entries = sortedEntries
                .Select(entry => entry.Key) // Extrahiere das Original-Element
                .ToList();

            // Jetzt die sortierten Einträge in das Dictionary einsortieren
            foreach (var pair in sortedEntries)
            {
                long novelId = pair.Key.GetNovelId();

                if (!_novelHistoryEntriesDictionary.ContainsKey(novelId))
                {
                    _novelHistoryEntriesDictionary[novelId] = new List<DialogHistoryEntry>();
                }

                _novelHistoryEntriesDictionary[novelId].Add(pair.Key);
            }

            return entries;
        }
    }
}