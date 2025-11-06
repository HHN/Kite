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
                    designation = folder.NovelMetaData.DesignationOfNovel,
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
            string startLabel = TweeProcessor.GetStartLabelFromTweeFile(tweeFile);
            InitializeKiteNovelEventList(metaData, eventList, startLabel);
            List<TweePassage> passages = TweeProcessor.ProcessTweeFile(tweeFile);

            foreach (TweePassage passage in passages)
            {
                List<string> message = TweeProcessor.ExtractMessageOutOfTweePassage(passage.Passage);
                List<string> keywords = TweeProcessor.ExtractKeywordOutOfTweePassage(passage.Passage);
                List<NovelKeywordModel> keywordModels = NovelKeywordParser.ParseKeywordsFromFile(keywords, metaData);
                
                string id = passage.Label;

                switch (keywordModels.Count)
                {
                    case > 1:
                    {
                        string targetString = "";
                        int messageIndex = 0;

                        for (int i = 0; i < keywordModels.Count; i++)
                        {
                            string currentMessage;
                            if (message.Count == 0)
                            {
                                currentMessage = "";
                            }
                            else if (message.Count <= messageIndex)
                            {
                                currentMessage = message[^1];
                            }
                            else
                            {
                                currentMessage = message[messageIndex];
                            }

                            VisualNovelEvent createdEvent = CreateVisualNovelEventFromKeyword(passage, currentMessage, keywordModels[i], eventList);

                            targetString = targetString switch
                            {
                                "" when passage.Links.Any() => passage.Label, // If there is a link to the next event, we use the label bc it is unique
                                "" when !passage.Links.Any() => passage.Label + "newTarget", // If it's the last event (no next event after this)
                                _ => targetString
                            };
                            
                            // Not the last element
                             if (i != keywordModels.Count - 1)
                            {
                                createdEvent.id = id; 
                                var nextId = id + "" + i;
                                createdEvent.nextId = nextId;
                                id = createdEvent.nextId;
                            }
                            // Last element
                            else
                            {
                                if (createdEvent == null)
                                {
                                    eventList[^3].id = id;
                                }
                                else
                                {
                                    createdEvent.id = id;
                                    createdEvent.nextId = passage.Links[0].Target;
                                }
                                
                                // If dialogue options are present, process them.
                                HandleDialogueOptionEvent(passage, eventList, createdEvent);
                            }

                            // If the event is a dialogue option (eventType 4), increase the message index.
                            if (createdEvent != null && createdEvent.eventType == 4)
                            {
                                messageIndex++;
                            }

                            // Check if the event creates a loop and adjust if necessary.
                            HandleLoop(createdEvent, startLabel, eventList);
                        }

                        break;
                    }
                    case 1:
                    {
                        var createdEvent = CreateVisualNovelEventFromKeyword(passage, message.Count == 0 ? "" : message[0], keywordModels[0], eventList);

                        // Check if the event creates a loop and adjust if necessary.
                        HandleLoop(createdEvent, startLabel, eventList);

                        // If dialogue options are present, process them.
                        HandleDialogueOptionEvent(passage, eventList, createdEvent);
                        break;
                    }
                }
            }

            return eventList;
        }

        /// <summary>
        /// Creates a VisualNovelEvent based on the NovelKeywordModel.
        /// Depending on the fields set in the model (End, Bias, Sound, or Character event),
        /// the corresponding event is created.
        /// </summary>
        private static VisualNovelEvent CreateVisualNovelEventFromKeyword(TweePassage passage, string originalMessage, NovelKeywordModel model, List<VisualNovelEvent> eventList)
        {
            if (model == null) return null;

            // If the keyword signals the end.
            if (model.End.HasValue && model.End.Value)
            {
                HandleEndNovelEvent(passage.Label, eventList);
                return null;
            }

            // If a bias is defined.
            if (!string.IsNullOrEmpty(model.Bias))
            {
                return HandleBiasEvent(passage, model.Bias, eventList);
            }

            // If a sound is defined.
            if (!string.IsNullOrEmpty(model.Sound))
            {
                return HandlePlaySoundEvent(passage, model.Sound, eventList);
            }

            // If it's a character event.
            int character = model.CharacterIndex; //GetCharacterRoleFromIndex(model.CharacterIndex, metaData);
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
                LogManager.Warning("While loading " + metaData.TitleOfNovel + ": Initial character expression " + metaData.StartTalkingPartnerExpression + "not found!");
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
        private static VisualNovelEvent HandleCharacterTalksEvent(TweePassage passage, int character, string dialogMessage, int expression, List<VisualNovelEvent> list)
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
        /// Handles the creation of specific end-of-novel events, including playing a sound,
        /// making characters exit, and marking the end of the novel.
        /// Adds the generated events to the provided list of VisualNovelEvent objects.
        /// </summary>
        /// <param name="label">The unique identifier used for labeling the events.</param>
        /// <param name="list">The list of VisualNovelEvent objects to which the generated events will be added.</param>
        private static void HandleEndNovelEvent(string label, List<VisualNovelEvent> list)
        {
            string label01 = label + "RandomString0012003";
            string label02 = label01 + "RandomRandom";
            // Use the sound string directly.
            string leaveSceneSound = "LeaveScene";

            VisualNovelEvent soundEvent = KiteNovelEventFactory.GetPlaySoundEvent(label, label01, leaveSceneSound);
            VisualNovelEvent exitEvent = KiteNovelEventFactory.GetCharacterExitsEvent(label01, label02);
            VisualNovelEvent endEvent = KiteNovelEventFactory.GetEndNovelEvent(label02);

            list.Add(soundEvent);
            list.Add(exitEvent);
            list.Add(endEvent);
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
