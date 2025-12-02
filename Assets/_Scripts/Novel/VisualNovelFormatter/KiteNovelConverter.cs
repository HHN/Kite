﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assets._Scripts._Mappings;
using Assets._Scripts.Utilities;
using UnityEngine;

namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    /// <summary>
    /// Converter class that creates VisualNovel objects from processed novel folders
    /// and converts the Twee text document into a structured event list.
    /// Instead of a huge switch-case, it now uses the NovelKeywordParser to generate a NovelKeywordModel from the passage text.
    /// All values (role, expression, etc.) are handled as strings.
    /// </summary>
    public abstract class KiteNovelConverter
    {
        private static int _counterForNamingPurpose = 1;

        /// <summary>
        /// Converts processed novel folders into a list of VisualNovel objects.
        /// </summary>
        public static List<VisualNovel> ConvertFilesToNovels(List<KiteNovelFolder> folders)
        {
            List<VisualNovel> novels = new List<VisualNovel>();

            foreach (KiteNovelFolder folder in folders)
            {
                List<string> characters = new List<string>();
                AddCharacterIfNotNullOrWhitespace(characters, folder.NovelMetaData.TalkingPartner01);
                AddCharacterIfNotNullOrWhitespace(characters, folder.NovelMetaData.TalkingPartner02);
                AddCharacterIfNotNullOrWhitespace(characters, folder.NovelMetaData.TalkingPartner03);

                Color novelColor = Color.black;
                if (!string.IsNullOrEmpty(folder.NovelMetaData.NovelColor))
                {
                    ColorUtility.TryParseHtmlString(folder.NovelMetaData.NovelColor, out novelColor);
                }

                Color novelFrameColor = Color.black;
                if (!string.IsNullOrEmpty(folder.NovelMetaData.NovelFrameColor))
                {
                    ColorUtility.TryParseHtmlString(folder.NovelMetaData.NovelFrameColor, out novelFrameColor);
                }

                VisualNovel novel = new VisualNovel
                {
                    id = folder.NovelMetaData.IdNumberOfNovel,
                    title = folder.NovelMetaData.TitleOfNovel,
                    description = folder.NovelMetaData.DescriptionOfNovel,
                    context = folder.NovelMetaData.ContextForPrompt,
                    novelEvents = folder.NovelEventList,
                    characters = characters,
                    isKiteNovel = folder.NovelMetaData.IsKiteNovel,
                    novelColor = novelColor,
                    novelFrameColor = novelFrameColor
                };

                novels.Add(novel);
            }

            return novels;
        }

        /// <summary>
        /// Adds a character name to the provided list if the character name is not null, empty, or composed only of whitespace.
        /// </summary>
        /// <param name="list">The list to which the character name will be added.</param>
        /// <param name="characterName">The name of the character to be added.</param>
        private static void AddCharacterIfNotNullOrWhitespace(List<string> list, string characterName)
        {
            if (!string.IsNullOrWhiteSpace(characterName))
            {
                list.Add(characterName);
            }
        }

        /// <summary>
        /// Converts the content of a Twee text document into a structured event list.
        /// For each passage, the message (keyword) is extracted and converted into a NovelKeywordModel.
        /// Based on the fields set in the model, the corresponding event is created.
        /// </summary>
        public static List<VisualNovelEvent> ConvertTextDocumentIntoEventList(string tweeFile, KiteNovelMetaData metaData)
        {
            List<VisualNovelEvent> eventList = new List<VisualNovelEvent>();

            // Get the label of the start passage (usually "Start"/"Anfang"/"Willkommen", etc.).
            string startLabel = TweeProcessor.GetStartLabelFromTweeFile(tweeFile);

            // Add the initial "character joins" event.
            InitializeKiteNovelEventList(metaData, eventList, startLabel);

            List<TweePassage> passages = TweeProcessor.ProcessTweeFile(tweeFile);

            if (passages == null || passages.Count == 0)
            {
                LogManager.Warning($"ConvertTextDocumentIntoEventList: no passages found for novel '{metaData?.TitleOfNovel}'.");
                return eventList;
            }

            foreach (TweePassage passage in passages)
            {
                if (passage == null)
                    continue;

                // Skip meta passages that should not create events.
                if (string.Equals(passage.Label, "StoryTitle", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(passage.Label, "StoryData", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                try
                {
                    List<string> messages = TweeProcessor.ExtractMessageOutOfTweePassage(passage.Passage)
                                            ?? new List<string>();
                    List<string> keywords = TweeProcessor.ExtractKeywordOutOfTweePassage(passage.Passage)
                                            ?? new List<string>();
                    List<NovelKeywordModel> keywordModels = NovelKeywordParser.ParseKeywordsFromFile(keywords, metaData)
                                                            ?? new List<NovelKeywordModel>();

                    if (keywordModels.Count == 0)
                    {
                        // Nothing to create for this passage.
                        continue;
                    }

                    string baseId = passage.Label;
                    string firstLinkTarget = GetFirstLinkTarget(passage);

                    bool hasEndKeyword = keywordModels.Any(m => m.End.HasValue && m.End.Value);
                    bool hasNonEndKeyword = keywordModels.Any(m => !m.End.GetValueOrDefault());

                    // CASE 1: Passage contains only End keyword(s), e.g. ":: Spielstart >>End<<"
                    // This must still produce an event with id == passage label,
                    // otherwise links [[...->Spielstart]] will crash with KeyNotFoundException.
                    if (hasEndKeyword && !hasNonEndKeyword)
                    {
                        // First event in the end chain uses the passage label as id.
                        HandleEndNovelEvent(baseId, eventList, useLabelAsFirstId: true);
                        continue;
                    }

                    // Non-End models are the ones that actually create "normal" events (character, bias, sound, ...).
                    List<NovelKeywordModel> nonEndModels = keywordModels
                        .Where(m => !m.End.GetValueOrDefault())
                        .ToList();

                    if (nonEndModels.Count == 0)
                    {
                        // Defensive: should not happen because End-only case is handled above.
                        continue;
                    }

                    // CASE 2: Exactly one non-End keyword and no End keyword -> keep simple behavior.
                    if (nonEndModels.Count == 1 && !hasEndKeyword)
                    {
                        string text = messages.Count == 0 ? string.Empty : messages[0];
                        VisualNovelEvent createdEvent = CreateVisualNovelEventFromKeyword(
                            passage,
                            text,
                            nonEndModels[0],
                            eventList
                        );

                        HandleLoop(createdEvent, startLabel, eventList);
                        HandleDialogueOptionEvent(passage, eventList, createdEvent);
                        continue;
                    }

                    // CASE 3: Multiple non-End keywords and/or a mixture with End.
                    // We create a chain of events from the non-End keywords,
                    // then optionally append an end chain if an End keyword exists.
                    int messageIndex = 0;
                    int nonEndIndex = 0;
                    VisualNovelEvent lastCreatedEvent = null;
                    VisualNovelEvent previousCreatedEvent = null;

                    foreach (var model in nonEndModels)
                    {
                        string currentMessage;

                        if (messages.Count == 0)
                        {
                            currentMessage = string.Empty;
                        }
                        else if (messageIndex >= messages.Count)
                        {
                            // If we run out of messages, reuse the last one.
                            currentMessage = messages[messages.Count - 1];
                        }
                        else
                        {
                            currentMessage = messages[messageIndex];
                        }

                        VisualNovelEvent createdEvent = CreateVisualNovelEventFromKeyword(
                            passage,
                            currentMessage,
                            model,
                            eventList
                        );

                        if (createdEvent == null)
                        {
                            continue;
                        }

                        // Assign a stable id for each event originating in this passage.
                        // First event keeps the passage label, following events get an indexed suffix.
                        createdEvent.id = (nonEndIndex == 0)
                            ? baseId
                            : $"{baseId}_{nonEndIndex}";

                        // Link previous event in the same passage chain to the newly created event.
                        if (previousCreatedEvent != null)
                        {
                            previousCreatedEvent.nextId = createdEvent.id;
                        }

                        previousCreatedEvent = createdEvent;
                        lastCreatedEvent = createdEvent;

                        // Advance the message index only for ShowMessage events
                        // (the numeric value 4 is mapped to VisualNovelEventType.ShowMessageEvent).
                        if (createdEvent.eventType == 4)
                        {
                            messageIndex++;
                        }

                        nonEndIndex++;
                    }

                    if (lastCreatedEvent == null)
                    {
                        // Nothing usable got created.
                        continue;
                    }

                    // If we have an End keyword in this passage, append a dedicated end chain
                    // and connect the last "normal" event to the beginning of that chain.
                    if (hasEndKeyword)
                    {
                        // We already used baseId for the first dialog event in this passage,
                        // so the end chain must get distinct ids to avoid collisions.
                        string endChainFirstId = HandleEndNovelEvent(baseId, eventList, useLabelAsFirstId: false);
                        lastCreatedEvent.nextId = endChainFirstId;
                    }
                    else
                    {
                        // No End keyword -> follow the normal link to the next passage.
                        lastCreatedEvent.nextId = firstLinkTarget;
                    }

                    // Handle potential loops back to the start passage.
                    HandleLoop(lastCreatedEvent, startLabel, eventList);
                    // Finally, create choice events if the passage has multiple links.
                    HandleDialogueOptionEvent(passage, eventList, lastCreatedEvent);
                }
                catch (Exception ex)
                {
                    // Very important for debugging: one broken passage should not kill the whole novel.
                    LogManager.Error($"Error while converting passage '{passage.Label}': {ex.Message}");
                }
            }

            return eventList;
        }

        /// <summary>
        /// Safely returns the target of the first link of a passage, or an empty string if no links exist.
        /// </summary>
        private static string GetFirstLinkTarget(TweePassage passage)
        {
            if (passage?.Links == null || passage.Links.Count == 0)
                return string.Empty;

            TweeLink first = passage.Links[0];
            return first?.Target ?? string.Empty;
        }

        /// <summary>
        /// Creates a VisualNovelEvent based on the NovelKeywordModel.
        /// Depending on the fields set in the model (Bias, Sound, or Character event),
        /// the corresponding event is created.
        /// End keywords are handled separately in ConvertTextDocumentIntoEventList.
        /// </summary>
        private static VisualNovelEvent CreateVisualNovelEventFromKeyword(
            TweePassage passage,
            string originalMessage,
            NovelKeywordModel model,
            List<VisualNovelEvent> eventList)
        {
            if (model == null) return null;

            // End keywords are handled at passage level (ConvertTextDocumentIntoEventList),
            // because they sometimes need to be chained after other events.
            if (model.End.HasValue && model.End.Value)
            {
                return null;
            }

            // Bias event.
            if (!string.IsNullOrEmpty(model.Bias))
            {
                return HandleBiasEvent(passage, model.Bias, eventList);
            }

            // Sound event.
            if (!string.IsNullOrEmpty(model.Sound))
            {
                return HandlePlaySoundEvent(passage, model.Sound, eventList);
            }

            // Character talks event.
            int character = model.CharacterIndex;
            int expression = model.FaceExpression;

            return HandleCharacterTalksEvent(passage, character, originalMessage, expression, eventList);
        }

        /// <summary>
        /// Checks if the last created event refers to the start of the novel.
        /// If so, creates a new end event and adjusts the linking to avoid loops.
        /// </summary>
        private static void HandleLoop(VisualNovelEvent lastEvent, string startLabel, List<VisualNovelEvent> eventList)
        {
            if (lastEvent == null || string.IsNullOrEmpty(lastEvent.nextId) || string.IsNullOrEmpty(startLabel) || eventList == null)
            {
                return;
            }

            if (lastEvent.nextId == startLabel)
            {
                string newLabel = "RandomEndNovelString" + _counterForNamingPurpose;
                _counterForNamingPurpose++;
                HandleEndNovelEvent(newLabel, eventList);
                lastEvent.nextId = newLabel;
            }
        }

        /// <summary>
        /// Initializes the event list with start values (e.g., initial location and character join events)
        /// if defined in the metadata.
        /// </summary>
        private static void InitializeKiteNovelEventList(KiteNovelMetaData metaData, List<VisualNovelEvent> eventList, string startLabel)
        {
            string connectionLabel = "InitialCharacterJoinsEvent001";

            int character = MappingManager.MapCharacter(metaData.TalkingPartner01);
            if (character == -1)
            {
                LogManager.Warning("While loading " + metaData.TitleOfNovel + ": Initial character role not found!");
            }

            int expression = MappingManager.MapFaceExpressions(metaData.StartTalkingPartnerExpression);
            if (expression == -1)
            {
                LogManager.Warning("While loading " + metaData.TitleOfNovel + ": Initial character expression " + metaData.StartTalkingPartnerExpression + " not found!");
            }

            VisualNovelEvent initialCharacterJoinsEvent = KiteNovelEventFactory.GetCharacterJoinsEvent(connectionLabel, startLabel, character, expression);
            eventList.Add(initialCharacterJoinsEvent);
        }

        #region Specific Event Handlers

        /// <summary>
        /// Creates a VisualNovelEvent representing a character talking action
        /// and adds it to the provided list of events.
        /// </summary>
        /// <param name="passage">The TweePassage containing metadata such as the event ID and links to the next event.</param>
        /// <param name="character">The integer identifier of the character involved in the talking action.</param>
        /// <param name="dialogMessage">The dialog text associated with the character's talking action.</param>
        /// <param name="expression">The integer identifier for the character's facial expression during the event.</param>
        /// <param name="list">The list to which the created VisualNovelEvent will be added.</param>
        /// <returns>A VisualNovelEvent object representing the character's action.</returns>
        private static VisualNovelEvent HandleCharacterTalksEvent(
            TweePassage passage,
            int character,
            string dialogMessage,
            int expression,
            List<VisualNovelEvent> list)
        {
            string id = passage?.Label;

            string nextId = passage?.Links?.FirstOrDefault()?.Target ?? "";
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetCharacterTalksEvent(id, nextId, character, dialogMessage, expression);
            list.Add(novelEvent);
            return novelEvent;
        }

        /// <summary>
        /// Handles the creation of a bias-related VisualNovelEvent from a given TweePassage, bias string, and list of events.
        /// </summary>
        /// <param name="passage">The TweePassage containing the label and any associated links.</param>
        /// <param name="bias">The bias string relevant to the event being created.</param>
        /// <param name="list">The list to which the created VisualNovelEvent will be added.</param>
        /// <returns>The created VisualNovelEvent associated with the given bias.</returns>
        private static VisualNovelEvent HandleBiasEvent(TweePassage passage, string bias, List<VisualNovelEvent> list)
        {
            string id = passage?.Label;

            string nextId = (passage?.Links != null && passage.Links.Count > 0)
                ? passage.Links[0]?.Target ?? ""
                : "";

            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetBiasEvent(id, nextId, bias);
            list.Add(novelEvent);
            return novelEvent;
        }

        /// <summary>
        /// Creates a chain of end-of-novel events (sound -> character exit -> end marker),
        /// adds them to the list and returns the id of the first event in that chain.
        /// </summary>
        /// <param name="label">Base label used for naming the end events.</param>
        /// <param name="list">The list of VisualNovelEvent objects to which the generated events will be added.</param>
        /// <param name="useLabelAsFirstId">
        /// If true, the first end event will use <paramref name="label"/> as its id.
        /// This is needed for passages that are directly targeted by links (e.g. [[...->Spielstart]]).
        /// </param>
        private static string HandleEndNovelEvent(string label, List<VisualNovelEvent> list, bool useLabelAsFirstId)
        {
            string soundId = useLabelAsFirstId ? label : label + "_End_Sound";
            string exitId = soundId + "_Exit";
            string endId = soundId + "_Final";

            const string leaveSceneSound = "LeaveScene";

            VisualNovelEvent soundEvent = KiteNovelEventFactory.GetPlaySoundEvent(soundId, exitId, leaveSceneSound);
            VisualNovelEvent exitEvent = KiteNovelEventFactory.GetCharacterExitsEvent(exitId, endId);
            VisualNovelEvent endEvent = KiteNovelEventFactory.GetEndNovelEvent(endId);

            list.Add(soundEvent);
            list.Add(exitEvent);
            list.Add(endEvent);

            return soundId;
        }

        /// <summary>
        /// Convenience overload that creates an end chain using the label
        /// as the id of the first end event.
        /// Used primarily for loop handling.
        /// </summary>
        private static void HandleEndNovelEvent(string label, List<VisualNovelEvent> list)
        {
            HandleEndNovelEvent(label, list, useLabelAsFirstId: true);
        }

        /// <summary>
        /// Handles dialogue options within a passage and creates appropriate VisualNovelEvent(s)
        /// for adding and displaying choices.
        /// </summary>
        /// <param name="passage">The TweePassage object containing dialogue options.</param>
        /// <param name="list">The list of VisualNovelEvent objects where the generated events will be added.</param>
        /// <param name="lastEvent">The last VisualNovelEvent in the sequence to link dialogue options to.</param>
        private static void HandleDialogueOptionEvent(TweePassage passage, List<VisualNovelEvent> list, VisualNovelEvent lastEvent)
        {
            if (passage == null || passage.Links == null || passage.Links.Count <= 1)
            {
                return;
            }

            string label = "OptionsLabel" + _counterForNamingPurpose;
            _counterForNamingPurpose++;

            if (lastEvent != null)
            {
                lastEvent.nextId = label;
            }
            else
            {
                label = passage.Label;
            }

            foreach (TweeLink link in passage.Links)
            {
                string id = label;
                label += label;
                string nextId = label;
                string optionText = link.Text;
                string onChoice = link.Target;
                bool showAfterSelection = link.ShowAfterSelection;
                VisualNovelEvent visualNovelEvent = KiteNovelEventFactory.GetAddChoiceEvent(id, nextId, optionText, onChoice, showAfterSelection);
                list.Add(visualNovelEvent);
            }

            VisualNovelEvent showChoicesEvent = KiteNovelEventFactory.GetShowChoicesEvent(label);
            list.Add(showChoicesEvent);
        }

        /// <summary>
        /// Handles the creation of a play sound event from the given passage, sound, and event list.
        /// </summary>
        /// <param name="passage">The TweePassage containing the relevant label and links for the event.</param>
        /// <param name="sound">The sound file or identifier to be associated with the event.</param>
        /// <param name="list">The list of VisualNovelEvent objects to which the new event will be added.</param>
        /// <returns>The created VisualNovelEvent representing the play sound action.</returns>
        private static VisualNovelEvent HandlePlaySoundEvent(TweePassage passage, string sound, List<VisualNovelEvent> list)
        {
            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetPlaySoundEvent(id, nextId, sound);
            list.Add(novelEvent);
            return novelEvent;
        }

        #endregion
    }
}
