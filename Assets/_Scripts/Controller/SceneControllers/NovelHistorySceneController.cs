using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.Player;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.UIElements.DropDown;
using Assets._Scripts.UIElements.FoundersBubble;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace Assets._Scripts.Controller.SceneControllers
{
    public class NovelHistorySceneController : SceneController
    {
        [SerializeField] private GameObject containerPrefab;
        [SerializeField] private GameObject spacingPrefab;
        [SerializeField] private Transform containerParent;

        [SerializeField] private GameObject entryPrefab;

        [SerializeField] private GameObject container;
        [SerializeField] private GameObject noDataObjectsHint;

        [SerializeField] private bool displayNoDataObjectsHint;

        [SerializeField] private List<NovelHistoryEntryGuiElement> novelHistoryEntries;

        [SerializeField] private GameObject copyNotificationContainer;

        private Dictionary<long, List<DialogHistoryEntry>> _novelHistoryEntriesDictionary = new();
        private Dictionary<long, GameObject> _novelContainers = new();
        private Dictionary<long, DropDownMenu> _novelDropDownMenus = new();
        private Dictionary<DateTime, long> _dateAndTimeToNovelIdDictionary = new();

        // Setze die deutsche Kultur für die Datum/Uhrzeit-Formatierung
        private readonly CultureInfo _culture = new("de-DE");

        private void Start()
        {
            if (copyNotificationContainer != null) InitCopyNotification();

            BackStackManager.Instance().Push(SceneNames.NovelHistoryScene);

            novelHistoryEntries = new List<NovelHistoryEntryGuiElement>();
            InitializeScene();

            StartCoroutine(RebuildLayout());
        }

        private void InitCopyNotification()
        {
            copyNotificationContainer.SetActive(false);
            GameObjectManager.Instance().SetCopyNotification(copyNotificationContainer);
        }

        private void InitializeScene()
        {
            List<DialogHistoryEntry> entries = DialogHistoryManager.Instance().GetEntries();

            if (entries == null || entries.Count == 0)
            {
                noDataObjectsHint.SetActive(true);
                return;
            }

            noDataObjectsHint.SetActive(false);
            SortEntriesAndCreateDictionary(entries);
            AddNovelContainer(entries);
        }

        private void AddNovelContainer(List<DialogHistoryEntry> entries)
        {
            foreach (DialogHistoryEntry entry in entries)
            {
                long novelId = entry.GetNovelId();

                if (!_novelHistoryEntriesDictionary.ContainsKey(novelId))
                    CreateNovelContainer(novelId);

                _novelHistoryEntriesDictionary[novelId].Add(entry);
                AddEntryToContainer(entry, _novelContainers[novelId]);
            }
        }

        private void CreateNovelContainer(long novelId)
        {
            GameObject novelContainer = Instantiate(containerPrefab, containerParent);
            novelContainer.name = VisualNovelNamesHelper.GetName(novelId);

            _novelContainers[novelId] = novelContainer;
            Instantiate(spacingPrefab, containerParent);
        }

        private void AddEntryToContainer(DialogHistoryEntry entry, GameObject novelContainer)
        {
            GameObject reviewContainer = novelContainer.transform.Find("Review Container").gameObject;
            GameObject reviewButton = novelContainer.transform.Find("Review Button").gameObject;

            var visualNovel = VisualNovelNamesHelper.ValueOf((int)entry.GetNovelId());

            reviewButton.GetComponent<Image>().color = FoundersBubbleMetaInformation.GetColorOfNovel(visualNovel);
            reviewButton.GetComponentInChildren<TextMeshProUGUI>().text = VisualNovelNamesHelper.GetName(entry.GetNovelId());
            reviewButton.GetComponentInChildren<AlreadyPlayedUpdater>().VisualNovel = visualNovel;

            RectTransform containerTransform = container.GetComponent<RectTransform>();
            reviewButton.GetComponent<DropDownMenu>().AddLayoutToUpdateOnChange(containerTransform);

            var dataObjectGuiElement = Instantiate(entryPrefab, reviewContainer.transform).GetComponent<NovelHistoryEntryGuiElement>();
            dataObjectGuiElement.InitializeEntry(entry);
            dataObjectGuiElement.SetVisualNovelColor(visualNovel);

            dataObjectGuiElement.AddLayoutToUpdateOnChange(reviewContainer.GetComponent<RectTransform>());
            dataObjectGuiElement.AddLayoutToUpdateOnChange(novelContainer.GetComponent<RectTransform>());
            dataObjectGuiElement.AddLayoutToUpdateOnChange(containerTransform);

            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        private IEnumerator RebuildLayout()
        {
            foreach (var novelDropDownMenu in _novelDropDownMenus)
            {
                novelDropDownMenu.Value.RebuildLayout();
            }

            yield break;
        }

        private void SortEntriesAndCreateDictionary(List<DialogHistoryEntry> entries)
        {
            // Erstelle eine Liste zum Speichern der Sortierung
            List<KeyValuePair<DateTime, long>> sortedEntries = new List<KeyValuePair<DateTime, long>>();
            List<KeyValuePair<DialogHistoryEntry, DateTime>> sortedEntriesWithOriginal = new List<KeyValuePair<DialogHistoryEntry, DateTime>>();

            foreach (var entry in entries)
            {
                // Zerlege den HeadButtonText.text anhand von " | " in Teile
                string[] parts = entry.GetDateAndTime().Split('|');

                if (parts.Length < 3)
                    continue; // Falls das Format fehlerhaft ist, überspringe den Eintrag

                string datePart = parts[1].Trim(); // "dd.MM.yyyy"
                string timePart = parts[2].Trim(); // "HH:mm"

                // Datum + Zeit zusammenfügen und in DateTime umwandeln
                if (DateTime.TryParseExact($"{datePart} {timePart}", "dd.MM.yyyy HH:mm", _culture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    // Füge das Datum und die NovelId zur Liste hinzu (für das Dictionary)
                    sortedEntries.Add(new KeyValuePair<DateTime, long>(parsedDate, entry.GetNovelId()));

                    // Füge auch das Original-Element und das Datum zur Liste hinzu (für das sortierte Ergebnis)
                    sortedEntriesWithOriginal.Add(new KeyValuePair<DialogHistoryEntry, DateTime>(entry, parsedDate));
                }
                else
                {
                    // Falls das Parsen fehlschlägt, füge das Element mit DateTime.MinValue hinzu
                    sortedEntriesWithOriginal.Add(new KeyValuePair<DialogHistoryEntry, DateTime>(entry, DateTime.MinValue));
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
    }
}