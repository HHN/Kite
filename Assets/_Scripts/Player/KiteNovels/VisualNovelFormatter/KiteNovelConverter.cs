using System;
using System.Collections.Generic;
using Assets._Scripts.Novel;
using Assets._Scripts.Player.Kite_Novels.Visual_Novel_Formatter;
using UnityEngine;

namespace Assets._Scripts.Player.KiteNovels.VisualNovelFormatter
{
    #region NovelKeywordModel and Parser

    /// <summary>
    /// Model for a keyword. All fields are optional.
    /// </summary>
    public class NovelKeywordModel
    {
        public int? CharacterIndex { get; set; }
        public string Action { get; set; }
        public string FaceExpression { get; set; }
        public string Bias { get; set; }
        public string Sound { get; set; }
        public bool? End { get; set; }
    }

    /// <summary>
    /// Parser that converts a keyword string (e.g. ">>Character1|Looks|Angry<<") into a NovelKeywordModel.
    /// Expected formats:
    ///   >>End<<                → sets End = true
    ///   >>Info<<               → sets CharacterIndex = 0
    ///   >>Player<<             → sets CharacterIndex = 1
    ///   >>Character1|Looks|Angry<<  → sets CharacterIndex = 1+1 = 2, Action = "Looks", FaceExpression = "Angry"
    ///   >>Sound|TestSound<<     → sets Sound = "TestSound"
    ///   >>Bias|ConfirmationBias<<  → sets Bias = "ConfirmationBias"
    /// </summary>
    public static class NovelKeywordParser
    {
        public static NovelKeywordModel ParseKeyword(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return null;

            // Trim leading and trailing spaces.
            keyword = keyword.Trim();

            // Remove the ">>" and "<<" markers.
            if (keyword.StartsWith(">>") && keyword.EndsWith("<<"))
            {
                keyword = keyword.Substring(2, keyword.Length - 4);
            }

            NovelKeywordModel model = new NovelKeywordModel();

            // Check if the keyword signals the end.
            if (string.Equals(keyword, "End", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(keyword, "Ende", StringComparison.OrdinalIgnoreCase))
            {
                model.End = true;
                return model;
            }
            // Check for the exact keywords "Info" and "Player".
            if (string.Equals(keyword, "Info", StringComparison.OrdinalIgnoreCase))
            {
                model.CharacterIndex = 0;
                return model;
            }
            if (string.Equals(keyword, "Player", StringComparison.OrdinalIgnoreCase))
            {
                model.CharacterIndex = 1;
                return model;
            }

            // Split the string using the '|' separator.
            string[] parts = keyword.Split('|');
            if (parts.Length > 0)
            {
                // If it is a Character keyword.
                if (parts[0].StartsWith("Character", StringComparison.OrdinalIgnoreCase))
                {
                    // For example: "Character1" → extract "1" and add 1 (since 0 = Info, 1 = Player).
                    string numberPart = parts[0].Substring("Character".Length);
                    if (int.TryParse(numberPart, out int num))
                    {
                        model.CharacterIndex = num + 1;
                    }
                    if (parts.Length > 1)
                    {
                        model.Action = parts[1];
                    }
                    if (parts.Length > 2)
                    {
                        model.FaceExpression = parts[2];
                    }
                    return model;
                }
                // If it is a Sound keyword.
                else if (parts[0].StartsWith("Sound", StringComparison.OrdinalIgnoreCase))
                {
                    if (parts.Length > 1)
                    {
                        model.Sound = parts[1];
                    }
                    return model;
                }
                // If it is a Bias keyword.
                else if (parts[0].StartsWith("Bias", StringComparison.OrdinalIgnoreCase))
                {
                    if (parts.Length > 1)
                    {
                        model.Bias = parts[1];
                    }
                    return model;
                }
            }

            return model;
        }
    }

    #endregion

    /// <summary>
    /// Converter class that creates VisualNovel objects from processed novel folders
    /// and converts the Twee text document into a structured event list.
    /// Instead of using a huge switch-case, it now uses the NovelKeywordParser to generate
    /// a NovelKeywordModel from the passage text and then selects the appropriate event handler.
    /// </summary>
    public abstract class KiteNovelConverter
    {
        private static int _counterForNamingPurpose = 1;
        private const string EventDefinitionSeparator = ">>--<<";

        /// <summary>
        /// Converts the processed novel folders into a list of VisualNovel objects.
        /// </summary>
        public static List<VisualNovel> ConvertFilesToNovels(List<KiteNovelFolder> folders)
        {
            List<VisualNovel> novels = new List<VisualNovel>();

            foreach (KiteNovelFolder folder in folders)
            {
                VisualNovel novel = new VisualNovel();

                novel.id = folder.NovelMetaData.IdNumberOfNovel;
                novel.title = folder.NovelMetaData.TitleOfNovel;
                novel.description = folder.NovelMetaData.DescriptionOfNovel;
                novel.image = folder.NovelMetaData.IdNumberOfRepresentationImage;
                novel.context = folder.NovelMetaData.ContextForPrompt;
                novel.isKite2Novel = folder.NovelMetaData.IsKite2Novel;
                novel.novelEvents = folder.NovelEventList.NovelEvents;

                novels.Add(novel);
            }

            return novels;
        }

        /// <summary>
        /// Converts the content of a Twee text document into a structured event list.
        /// For each passage, the message text (keyword) is extracted and converted into a NovelKeywordModel.
        /// Based on the fields set in the model, the corresponding event is created.
        /// </summary>
        public static KiteNovelEventList ConvertTextDocumentIntoEventList(string tweeFile, KiteNovelMetaData kiteNovelMetaData)
        {
            KiteNovelEventList kiteNovelEventList = new KiteNovelEventList();
            string startEventLabel = TweeProcessor.GetStartLabelFromTweeFile(tweeFile);
            InitializeKiteNovelEventList(kiteNovelMetaData, kiteNovelEventList, startEventLabel);
            List<TweePassage> passages = TweeProcessor.ProcessTweeFile(tweeFile);

            foreach (TweePassage passage in passages)
            {
                // Extract the message text (i.e. the keyword) from the passage.
                string message = TweeProcessor.ExtractMessageOutOfTweePassage(passage.Passage);

                // Generate a NovelKeywordModel from the message text.
                NovelKeywordModel keywordModel = NovelKeywordParser.ParseKeyword(message);

                // Create the corresponding VisualNovelEvent based on the model.
                VisualNovelEvent createdEvent = CreateVisualNovelEventFromKeyword(passage, message, keywordModel, kiteNovelMetaData, kiteNovelEventList);

                // Check if the event creates a loop, and adjust if necessary.
                HandleLoop(createdEvent, startEventLabel, kiteNovelEventList);

                // If dialogue options are present, process them.
                HandleDialogueOptionEvent(passage, kiteNovelEventList.NovelEvents, createdEvent);
            }

            return kiteNovelEventList;
        }

        /// <summary>
        /// Creates a VisualNovelEvent based on the NovelKeywordModel.
        /// Depending on the fields set in the model (End, Bias, Sound, or Character event),
        /// the corresponding event type is created.
        /// </summary>
        private static VisualNovelEvent CreateVisualNovelEventFromKeyword(TweePassage passage, string originalMessage, NovelKeywordModel model, KiteNovelMetaData metaData, KiteNovelEventList eventList)
        {
            if (model == null)
            {
                return null;
            }

            // If the keyword signals the end.
            if (model.End.HasValue && model.End.Value)
            {
                return HandleEndNovelEvent(passage.Label, eventList.NovelEvents);
            }
            // If a bias is defined.
            else if (!string.IsNullOrEmpty(model.Bias))
            {
                DiscriminationBias biasEnum = MapBiasStringToEnum(model.Bias);
                return HandleBiasEvent(passage, biasEnum, eventList.NovelEvents);
            }
            // If a sound is defined.
            else if (!string.IsNullOrEmpty(model.Sound))
            {
                KiteSound soundEnum = MapSoundStringToEnum(model.Sound);
                return HandlePlaySoundEvent(passage, soundEnum, eventList.NovelEvents);
            }
            // If it's a character event.
            else if (model.CharacterIndex.HasValue)
            {
                CharacterRole role = GetCharacterRoleFromIndex(model.CharacterIndex.Value, metaData);
                CharacterExpression expression = MapExpressionStringToEnum(model.FaceExpression);

                // Decide based on the Action field which event to create.
                if (!string.IsNullOrEmpty(model.Action))
                {
                    if (model.Action.Equals("Entry", StringComparison.OrdinalIgnoreCase))
                    {
                        return HandleCharacterJoinsEvent(passage, role, expression, eventList.NovelEvents);
                    }
                    else if (model.Action.Equals("Speaks", StringComparison.OrdinalIgnoreCase))
                    {
                        return HandleCharacterTalksEvent(passage, role, originalMessage, expression, eventList.NovelEvents);
                    }
                    else if (model.Action.Equals("Looks", StringComparison.OrdinalIgnoreCase))
                    {
                        // "Looks" is handled as a variant of the character joining event.
                        return HandleCharacterJoinsEvent(passage, role, expression, eventList.NovelEvents);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Maps a bias string to the corresponding DiscriminationBias enum.
        /// Add further mappings as needed.
        /// </summary>
        private static DiscriminationBias MapBiasStringToEnum(string bias)
        {
            if (bias.Equals("AccessToFunding", StringComparison.OrdinalIgnoreCase))
                return DiscriminationBias.AccessToFunding;
            if (bias.Equals("GenderPayGap", StringComparison.OrdinalIgnoreCase))
                return DiscriminationBias.GenderPayGap;
            // Add further mappings here...
            return DiscriminationBias.None;
        }

        /// <summary>
        /// Maps a sound string to the corresponding KiteSound enum.
        /// </summary>
        private static KiteSound MapSoundStringToEnum(string sound)
        {
            if (sound.Equals("WaterPouring", StringComparison.OrdinalIgnoreCase))
                return KiteSound.WaterPouring;
            if (sound.Equals("LeaveScene", StringComparison.OrdinalIgnoreCase))
                return KiteSound.LeaveScene;
            if (sound.Equals("TelephoneCall", StringComparison.OrdinalIgnoreCase))
                return KiteSound.TelephoneCall;
            if (sound.Equals("PaperSound", StringComparison.OrdinalIgnoreCase))
                return KiteSound.PaperSound;
            if (sound.Equals("ManLaughing", StringComparison.OrdinalIgnoreCase))
                return KiteSound.ManLaughing;
            return KiteSound.None;
        }

        /// <summary>
        /// Maps an expression string to the corresponding CharacterExpression enum.
        /// Add further mappings as needed.
        /// </summary>
        private static CharacterExpression MapExpressionStringToEnum(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return CharacterExpression.None;
            if (expression.Equals("Angry", StringComparison.OrdinalIgnoreCase))
                return CharacterExpression.LooksCritically; // Example mapping
            if (expression.Equals("Scared", StringComparison.OrdinalIgnoreCase))
                return CharacterExpression.LooksScared;
            if (expression.Equals("Neutral", StringComparison.OrdinalIgnoreCase))
                return CharacterExpression.LooksNeutral;
            // Add further mappings as needed...
            return CharacterExpression.None;
        }

        /// <summary>
        /// Determines the CharacterRole based on the CharacterIndex.
        /// 0: Info, 1: Player, 2: first talking partner, 3: second talking partner, 4: third talking partner.
        /// </summary>
        private static CharacterRole GetCharacterRoleFromIndex(int index, KiteNovelMetaData metaData)
        {
            if (index == 0)
                return CharacterRole.Info;
            else if (index == 1)
                return CharacterRole.Player;
            else if (index == 2)
                return CharacterTypeHelper.ValueOf(metaData.TalkingPartner01);
            else if (index == 3)
                return CharacterTypeHelper.ValueOf(metaData.TalkingPartner02);
            else if (index == 4)
                return CharacterTypeHelper.ValueOf(metaData.TalkingPartner03);
            return CharacterRole.None;
        }

        /// <summary>
        /// Checks whether the last created event refers to the start of the novel.
        /// If so, creates a new end event and adjusts the linking to avoid loops.
        /// </summary>
        private static void HandleLoop(VisualNovelEvent lastEvent, string startLabel, KiteNovelEventList eventList)
        {
            if (lastEvent == null || string.IsNullOrEmpty(lastEvent.nextId)
                || string.IsNullOrEmpty(startLabel) || eventList == null)
            {
                return;
            }
            if (lastEvent.nextId == startLabel)
            {
                string newLabel = "RandomEndNovelString" + _counterForNamingPurpose;
                _counterForNamingPurpose++;
                HandleEndNovelEvent(newLabel, eventList.NovelEvents);
                lastEvent.nextId = newLabel;
            }
        }

        /// <summary>
        /// Initializes the event list with start values (e.g. initial location and character join events)
        /// if defined in the metadata.
        /// </summary>
        private static void InitializeKiteNovelEventList(KiteNovelMetaData metaData, KiteNovelEventList eventList, string startLabel)
        {
            if (!metaData.IsWithStartValues)
            {
                return;
            }

            string connectionLabel = "initialCharacterJoinsEvent001";
            string id = "initialLocationEvent001";
            string nextId = connectionLabel;
            Location location = LocationHelper.ValueOf(metaData.StartLocation);

            if (location == Location.NONE)
            {
                Debug.LogWarning("While loading " + metaData.TitleOfNovel + ": Initial location not found!");
            }

            VisualNovelEvent initialLocationEvent = KiteNovelEventFactory.GetSetBackgroundEvent(id, nextId, location);
            eventList.NovelEvents.Add(initialLocationEvent);

            id = connectionLabel;
            nextId = startLabel;
            CharacterRole character = CharacterTypeHelper.ValueOf(metaData.TalkingPartner01);
            if (character == CharacterRole.None)
            {
                Debug.LogWarning("While loading " + metaData.TitleOfNovel + ": Initial character role not found!");
            }

            CharacterExpression expression = CharacterExpressionHelper.ValueOf(metaData.StartTalkingPartnerEmotion);
            if (expression == CharacterExpression.None)
            {
                Debug.LogWarning("While loading " + metaData.TitleOfNovel + ": Initial character expression not found!");
            }

            VisualNovelEvent initialCharacterJoinsEvent = KiteNovelEventFactory.GetCharacterJoinsEvent(id, nextId, character, expression);
            eventList.NovelEvents.Add(initialCharacterJoinsEvent);
        }

        #region Specific Event Handlers

        private static VisualNovelEvent HandleLocationEvent(TweePassage passage, Location location, List<VisualNovelEvent> list)
        {
            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetSetBackgroundEvent(id, nextId, location);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleCharacterTalksEvent(TweePassage passage, CharacterRole character, string dialogMessage, CharacterExpression expression, List<VisualNovelEvent> list)
        {
            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetCharacterTalksEvent(id, nextId, character, dialogMessage, expression);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleCharacterJoinsEvent(TweePassage passage, CharacterRole character, CharacterExpression expression, List<VisualNovelEvent> list)
        {
            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetCharacterJoinsEvent(id, nextId, character, expression);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleBiasEvent(TweePassage passage, DiscriminationBias bias, List<VisualNovelEvent> list)
        {
            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetBiasEvent(id, nextId, bias);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleEndNovelEvent(string label, List<VisualNovelEvent> list)
        {
            string label01 = label;
            string label02 = label01 + "RandomString0012003";
            string label03 = label02 + "RandomRandom";
            KiteSound leaveSceneSound = KiteSound.LeaveScene;

            VisualNovelEvent soundEvent = KiteNovelEventFactory.GetPlaySoundEvent(label01, label02, leaveSceneSound);
            VisualNovelEvent exitEvent = KiteNovelEventFactory.GetCharacterExitsEvent(label02, label03);
            VisualNovelEvent endEvent = KiteNovelEventFactory.GetEndNovelEvent(label03);

            list.Add(soundEvent);
            list.Add(exitEvent);
            list.Add(endEvent);

            return null;
        }

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
                label = label + label;
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

        private static VisualNovelEvent HandlePlaySoundEvent(TweePassage passage, KiteSound audioClipToPlay, List<VisualNovelEvent> list)
        {
            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetPlaySoundEvent(id, nextId, audioClipToPlay);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandlePlayAnimationEvent(TweePassage passage, KiteAnimation animationToPlay, List<VisualNovelEvent> list)
        {
            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetPlayAnimationEvent(id, nextId, animationToPlay);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleFreeTextInputEvent(TweePassage passage, string message, List<VisualNovelEvent> list)
        {
            string[] parts = message.Split(new[] { ':' }, 2);

            if (parts.Length != 2)
            {
                Debug.LogWarning("While creating Visual Novels: Free Text Input Event could not be created.");
                return null;
            }

            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            string question = parts[1].Trim();
            string variableName = parts[0].Trim();
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetFreeTextInputEvent(id, nextId, question, variableName);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleGptRequestEvent(TweePassage passage, string message, CompletionHandler completionHandlerId, List<VisualNovelEvent> list)
        {
            string[] parts = message.Split(new[] { ':' }, 2);

            if (parts.Length != 2)
            {
                Debug.LogWarning("While creating Visual Novels: GPT Prompt Event could not be created.");
                return null;
            }

            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            string prompt = parts[1].Trim();
            string variableName = parts[0].Trim();
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetGptEvent(id, nextId, prompt, variableName, completionHandlerId);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleSaveDataEvent(TweePassage passage, string message, List<VisualNovelEvent> list)
        {
            string[] parts = message.Split(new[] { ':' }, 2);

            if (parts.Length != 2)
            {
                Debug.LogWarning("While creating Visual Novels: Save Persistent Event could not be created.");
                return null;
            }

            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            string key = parts[0].Trim();
            string value = parts[1].Trim();
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetSavePersistentEvent(id, nextId, key, value);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleSetVariableEvent(TweePassage passage, string message, List<VisualNovelEvent> list)
        {
            string[] parts = message.Split(new[] { ':' }, 2);

            if (parts.Length != 2)
            {
                Debug.LogWarning("While creating Visual Novels: Set Variable Event could not be created.");
                return null;
            }

            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            string key = parts[0].Trim();
            string value = parts[1].Trim();
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetSaveVariableEvent(id, nextId, key, value);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleSetVariableFromBooleanExpressionEvent(TweePassage passage, string message, List<VisualNovelEvent> list)
        {
            string[] parts = message.Split(new[] { ':' }, 2);

            if (parts.Length != 2)
            {
                Debug.LogWarning("While creating Visual Novels: Calculate Variable from Boolean Expression Event could not be created.");
                return null;
            }

            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            string key = parts[0].Trim();
            string value = parts[1].Trim();
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetCalculateVariableFromBooleanExpressionEvent(id, nextId, key, value);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleAddFeedbackUnderConditionEvent(TweePassage passage, string message, List<VisualNovelEvent> list)
        {
            string[] parts = message.Split(new[] { ':' }, 2);

            if (parts.Length != 2)
            {
                Debug.LogWarning("While creating Visual Novels: Add Feedback Under Condition Event could not be created.");
                return null;
            }

            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            string key = parts[0].Trim();
            string value = parts[1].Trim();
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetAddFeedbackUnderConditionEvent(id, nextId, key, value);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleAddFeedbackEvent(TweePassage passage, string message, List<VisualNovelEvent> list)
        {
            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetAddFeedbackEvent(id, nextId, message);
            list.Add(novelEvent);
            return novelEvent;
        }

        #endregion
    }
}
