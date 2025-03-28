using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Novels.Visual_Novel_Formatter
{
    public abstract class KiteNovelConverter
    {
        private static int _counterForNamingPurpose = 1;
        private const string EventDefinitionSeparator = ">>--<<";

        public static List<VisualNovel> ConvertFilesToNovels(List<KiteNovelFolder> folders)
        {
            List<VisualNovel> novels = new List<VisualNovel>();

            foreach (KiteNovelFolder folder in folders)
            {
                VisualNovel novel = new VisualNovel
                {
                    id = folder.NovelMetaData.IdNumberOfNovel,
                    title = folder.NovelMetaData.TitleOfNovel,
                    description = folder.NovelMetaData.DescriptionOfNovel,
                    image = folder.NovelMetaData.IdNumberOfRepresentationImage,
                    context = folder.NovelMetaData.ContextForPrompt,
                    isKite2Novel = folder.NovelMetaData.IsKite2Novel,
                    novelEvents = folder.NovelEventList.NovelEvents
                };

                novels.Add(novel);
            }

            return novels;
        }

        public static KiteNovelEventList ConvertTextDocumentIntoEventList(string tweeFile,
            KiteNovelMetaData kiteNovelMetaData)
        {
            KiteNovelEventList kiteNovelEventList = new KiteNovelEventList();
            string startEventLabel = TweeProcessor.GetStartLabelFromTweeFile(tweeFile);
            InitializeKiteNovelEventList(kiteNovelMetaData, kiteNovelEventList, startEventLabel);
            List<TweePassage> passages = TweeProcessor.ProcessTweeFile(tweeFile);

            foreach (TweePassage passage in passages)
            {
                VisualNovelEvent lastCreatedEvent = CreateVisualNovelEvents(passage, kiteNovelMetaData, kiteNovelEventList);
                if (passage.Label.Contains("Anfang"))
                {
                    Debug.Log($"lastCreatedEvent: {lastCreatedEvent.id}");
                }
                HandleLoop(lastCreatedEvent, startEventLabel, kiteNovelEventList);
                HandleDialogueOptionEvent(passage, kiteNovelEventList.NovelEvents, lastCreatedEvent);
            }

            return kiteNovelEventList;
        }

        private static void HandleLoop(VisualNovelEvent lastEvent, string labelOfStartEvent,
            KiteNovelEventList kiteNovelEventList)
        {
            if (lastEvent == null || string.IsNullOrEmpty(lastEvent.nextId)
                                  || string.IsNullOrEmpty(labelOfStartEvent) || kiteNovelEventList == null)
            {
                return;
            }

            if (lastEvent.nextId == labelOfStartEvent)
            {
                string newLabel = "RandomEndNovelString" + _counterForNamingPurpose;
                _counterForNamingPurpose++;
                HandleEndNovelEvent(newLabel, kiteNovelEventList.NovelEvents);
                lastEvent.nextId = newLabel;
            }
        }

        private static void InitializeKiteNovelEventList(KiteNovelMetaData kiteNovelMetaData, KiteNovelEventList kiteNovelEventList, string startLabel)
        {
            if (!kiteNovelMetaData.IsWithStartValues)
            {
                return;
            }

            string connectionLabel = "initalCharakterJoinsEvent001";
            string id = "initialLocationEvent001";
            string nextId = connectionLabel;
            Location location = LocationHelper.ValueOf(kiteNovelMetaData.StartLocation);

            if (location == Location.NONE)
            {
                Debug.LogWarning("While loading " + kiteNovelMetaData.TitleOfNovel + ": Initial Location not found!");
            }

            VisualNovelEvent initialLocationEvent = KiteNovelEventFactory.GetSetBackgroundEvent(id, nextId, location);
            kiteNovelEventList.NovelEvents.Add(initialLocationEvent);

            id = connectionLabel;
            nextId = startLabel;
            CharacterRole character = CharacterTypeHelper.ValueOf(kiteNovelMetaData.TalkingPartner01);
            if (character == CharacterRole.NONE)
            {
                Debug.LogWarning("While loading " + kiteNovelMetaData.TitleOfNovel + ": Initial CharacterRole not found!");
            }

            CharacterExpression expression =
                CharacterExpressionHelper.ValueOf(kiteNovelMetaData.StartTalkingPartnerEmotion);

            if (expression == CharacterExpression.None)
            {
                Debug.LogWarning("While loading " + kiteNovelMetaData.TitleOfNovel + ": Initial CharacterRole-Expression not found!");
            }

            VisualNovelEvent initialCharacterJoinsEvent = KiteNovelEventFactory.GetCharacterJoinsEvent(id, nextId, character, expression);
            kiteNovelEventList.NovelEvents.Add(initialCharacterJoinsEvent);
        }

        private static VisualNovelEvent HandleLocationEvent(TweePassage twee, Location location,
            List<VisualNovelEvent> list)
        {
            string id = twee?.Label;
            string nextId = twee?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetSetBackgroundEvent(id, nextId, location);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleCharacterTalksEvent(TweePassage twee, CharacterRole character,
            string dialogMessage, CharacterExpression expression, List<VisualNovelEvent> list)
        {
            string id = twee?.Label;
            string nextId = twee?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetCharacterTalksEvent(id, nextId, character, dialogMessage, expression);
            list.Add(novelEvent);
            
            if (twee.Label.Contains("Anfang"))
            {
                Debug.Log($"Anfang: {twee.Label}");
                Debug.Log($"novelEvent: {novelEvent.id}");
            }
            
            return novelEvent;
        }

        private static VisualNovelEvent HandleCharacterComesEvent(TweePassage twee, CharacterRole character,
            CharacterExpression expressionType, List<VisualNovelEvent> list)
        {
            string id = twee?.Label;
            string nextId = twee?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetCharacterJoinsEvent(id, nextId, character, expressionType);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleBiasEvent(TweePassage twee, DiscriminationBias relevantBias,
            List<VisualNovelEvent> list)
        {
            string id = twee?.Label;
            string nextId = twee?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetBiasEvent(id, nextId, relevantBias);
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

        private static void HandleDialogueOptionEvent(TweePassage twee, List<VisualNovelEvent> list,
            VisualNovelEvent lastEvent)
        {
            if (twee == null || twee.Links == null || twee.Links.Count <= 1)
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
                label = twee.Label;
            }

            foreach (TweeLink link in twee.Links)
            {
                string id = label;
                label = label + label;
                string nextId = label;
                string optionText = link.Text;
                string onChoice = link.Target;
                bool showAfterSelection = link.ShowAfterSelection;
                VisualNovelEvent visualNovelEvent =
                    KiteNovelEventFactory.GetAddChoiceEvent(id, nextId, optionText, onChoice, showAfterSelection);
                list.Add(visualNovelEvent);
            }

            VisualNovelEvent showChoicesEvent = KiteNovelEventFactory.GetShowChoicesEvent(label);
            list.Add(showChoicesEvent);
        }

        private static VisualNovelEvent HandlePlaySoundEvent(TweePassage twee, KiteSound audioClipToPlay,
            List<VisualNovelEvent> list)
        {
            string id = twee?.Label;
            string nextId = twee?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetPlaySoundEvent(id, nextId, audioClipToPlay);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandlePlayAnimationEvent(TweePassage twee, KiteAnimation animationToPlay,
            List<VisualNovelEvent> list)
        {
            string id = twee?.Label;
            string nextId = twee?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetPlayAnimationEvent(id, nextId, animationToPlay);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleGptRequestEvent(TweePassage twee, string message,
            CompletionHandler completionHandlerId, List<VisualNovelEvent> list)
        {
            string[] parts = message.Split(new[] { ':' }, 2);

            if (parts.Length != 2)
            {
                Debug.LogWarning("While creating Visual Novels: GPT Prompt Event could not be created.");
                return null;
            }

            string id = twee?.Label;
            string nextId = twee?.Links?[0]?.Target;
            string prompt = parts[1].Trim();
            string variablesName = parts[0].Trim();
            VisualNovelEvent novelEvent =
                KiteNovelEventFactory.GetGptEvent(id, nextId, prompt, variablesName, completionHandlerId);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleSaveDataEvent(TweePassage twee, string message,
            List<VisualNovelEvent> list)
        {
            string[] parts = message.Split(new[] { ':' }, 2);

            if (parts.Length != 2)
            {
                Debug.LogWarning("While creating Visual Novels: Save Persistent Event could not be created.");
                return null;
            }

            string id = twee?.Label;
            string nextId = twee?.Links?[0]?.Target;
            string key = parts[0].Trim();
            string value = parts[1].Trim();
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetSavePersistentEvent(id, nextId, key, value);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleSetVariableEvent(TweePassage twee, string message,
            List<VisualNovelEvent> list)
        {
            string[] parts = message.Split(new[] { ':' }, 2);

            if (parts.Length != 2)
            {
                Debug.LogWarning("While creating Visual Novels: Save Variable Event could not be created.");
                return null;
            }

            string id = twee?.Label;
            string nextId = twee?.Links?[0]?.Target;
            string key = parts[0].Trim();
            string value = parts[1].Trim();
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetSaveVariableEvent(id, nextId, key, value);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleSetVariableFromBooleanExpressionEvent(TweePassage twee, string message,
            List<VisualNovelEvent> list)
        {
            string[] parts = message.Split(new[] { ':' }, 2);

            if (parts.Length != 2)
            {
                Debug.LogWarning(
                    "While creating Visual Novels: Calculate Variable from boolean expression Event could not be created.");
                return null;
            }

            string id = twee?.Label;
            string nextId = twee?.Links?[0]?.Target;
            string key = parts[0].Trim();
            string value = parts[1].Trim();
            VisualNovelEvent novelEvent =
                KiteNovelEventFactory.GetCalculateVariableFromBooleanExpressionEvent(id, nextId, key, value);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleAddFeedbackUnderConditionEvent(TweePassage twee, string message,
            List<VisualNovelEvent> list)
        {
            string[] parts = message.Split(new[] { ':' }, 2);

            if (parts.Length != 2)
            {
                Debug.LogWarning(
                    "While creating Visual Novels: Add Feedback Under Condition Event could not be created.");
                return null;
            }

            string id = twee?.Label;
            string nextId = twee?.Links?[0]?.Target;
            string key = parts[0].Trim();
            string value = parts[1].Trim();
            VisualNovelEvent novelEvent =
                KiteNovelEventFactory.GetAddFeedbackUnderConditionEvent(id, nextId, key, value);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleAddFeedbackEvent(TweePassage twee, string message,
            List<VisualNovelEvent> list)
        {
            string id = twee?.Label;
            string nextId = twee?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetAddFeedbackEvent(id, nextId, message);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent CreateVisualNovelEvents(TweePassage passage,
            KiteNovelMetaData kiteNovelMetaData, KiteNovelEventList kiteNovelEventList)
        {
            if (passage.Passage.Contains(EventDefinitionSeparator))
            {
                return CreateMultipleVisualNovelEvent(passage, kiteNovelMetaData, kiteNovelEventList);
            }
            else
            {
                return CreateOneVisualNovelEvent(passage, kiteNovelMetaData, kiteNovelEventList);
            }
        }

        private static VisualNovelEvent CreateOneVisualNovelEvent(TweePassage passage,
            KiteNovelMetaData kiteNovelMetaData, KiteNovelEventList kiteNovelEventList)
        {
            string message = TweeProcessor.ExtractMessageOutOfTweePassage(passage.Passage);
            VisualNovelEvent visualNovelEvent = ConvertListOfDataObjectsIntoKiteNovelEvent(passage, message, kiteNovelMetaData, kiteNovelEventList);
            return visualNovelEvent;
        }

        private static VisualNovelEvent CreateMultipleVisualNovelEvent(TweePassage passage,
            KiteNovelMetaData kiteNovelMetaData, KiteNovelEventList kiteNovelEventList)
        {
            List<VisualNovelEvent> createdEvents = new List<VisualNovelEvent>();
            string[] eventDefinitions = passage?.Passage?.Split(new[] { EventDefinitionSeparator },
                StringSplitOptions.RemoveEmptyEntries);
            string label = passage.Label;

            foreach (string eventDefinition in eventDefinitions)
            {
                string nextLabel = label + "RandomSeperatorString1020304" + label;
                List<TweeLink> links = new List<TweeLink> { new TweeLink(nextLabel, nextLabel, false) };
                TweePassage newPassage = new TweePassage(label, eventDefinition, links);
                label = nextLabel;
                string message = TweeProcessor.ExtractMessageOutOfTweePassage(eventDefinition);
                VisualNovelEvent visualNovelEvent =
                    ConvertListOfDataObjectsIntoKiteNovelEvent(newPassage, message, kiteNovelMetaData,
                        kiteNovelEventList);
                
                if (passage.Label.Contains("Anfang"))
                {
                    Debug.Log($"visualNovelEvent: {visualNovelEvent.id}");
                }

                if (visualNovelEvent != null)
                {
                    createdEvents.Add(visualNovelEvent);
                }
            }

            if (createdEvents.Count == 0)
            {
                return null;
            }

            VisualNovelEvent firstEvent = createdEvents[0];
            if (firstEvent != null)
            {
                firstEvent.id = passage.Label;
            }

            VisualNovelEvent lastEvent = createdEvents[createdEvents.Count - 1];
            if (lastEvent != null && passage.Links?.Count != 0)
            {
                lastEvent.nextId = passage.Links?[0]?.Target;
            }

            return lastEvent;
        }

        private static VisualNovelEvent ConvertListOfDataObjectsIntoKiteNovelEvent(TweePassage passage, string message,
            KiteNovelMetaData kiteNovelMetaData, KiteNovelEventList kiteNovelEventList)
        {
            if (string.IsNullOrEmpty(passage?.Passage))
            {
                return null;
            }

            CharacterRole character01 = CharacterTypeHelper.ValueOf(kiteNovelMetaData.TalkingPartner01);
            CharacterRole character02 = CharacterTypeHelper.ValueOf(kiteNovelMetaData.TalkingPartner02);
            CharacterRole character03 = CharacterTypeHelper.ValueOf(kiteNovelMetaData.TalkingPartner03);

            NovelKeyWord recognizedKeyWord = FindFirstKeyWordInText(passage.Passage);

            switch (recognizedKeyWord)
            {
                case NovelKeyWord.SceneOffice:
                {
                    return HandleLocationEvent(passage, Location.OFFICE, kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter01:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksNeutralRelaxed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter01FacialExpressionScared:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksScared,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter01FacialExpressionDefeated:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksDefeated,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter01FacialExpressionDissatisfied:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksDissatisfied,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter01FacialExpressionRejecting:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksRejecting,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter01FacialExpressionAmazed:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksAmazed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter01FacialExpressionQuestioning:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksQuestioning,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter01FacialExpressionCritical:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksCritical,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter01FacialExpressionSmilingBig:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksSmilingBig,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter01FacialExpressionLaughing:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksLaughing,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter01FacialExpressionSmiling:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksSmiling,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter01FacialExpressionNeutralRelaxed:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksNeutralRelaxed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter01FacialExpressionNeutral:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksNeutral,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter01FacialExpressionProud:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksProud,
                        kiteNovelEventList.NovelEvents);
                }
                
                case NovelKeyWord.Character01Looks:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksNeutralRelaxed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01LooksFacialExpressionScared:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksScared,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01LooksFacialExpressionDefeated:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksDefeated,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01LooksFacialExpressionDissatisfied:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksDissatisfied,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01LooksFacialExpressionRejecting:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksRejecting,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01LooksFacialExpressionAmazed:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksAmazed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01LooksFacialExpressionQuestioning:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksQuestioning,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01LooksFacialExpressionCritical:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksCritical,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01LooksFacialExpressionSmilingBig:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksSmilingBig,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01LooksFacialExpressionLaughing:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksLaughing,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01LooksFacialExpressionSmiling:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksSmiling,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01LooksFacialExpressionNeutralRelaxed:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksNeutralRelaxed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01LooksFacialExpressionNeutral:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksNeutral,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01LooksFacialExpressionProud:
                {
                    return HandleCharacterComesEvent(passage, character01, CharacterExpression.LooksProud,
                        kiteNovelEventList.NovelEvents);
                }

                case NovelKeyWord.Character01Speaks:
                {
                    return HandleCharacterTalksEvent(passage, character01, message, CharacterExpression.SpeaksNeutralRelaxed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01SpeaksFacialExpressionScared:
                {
                    return HandleCharacterTalksEvent(passage, character01, message, CharacterExpression.SpeaksScared,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01SpeaksFacialExpressionDefeated:
                {
                    return HandleCharacterTalksEvent(passage, character01, message, CharacterExpression.SpeaksDefeated,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01SpeaksFacialExpressionDissatisfied:
                {
                    return HandleCharacterTalksEvent(passage, character01, message, CharacterExpression.SpeaksDissatisfied,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01SpeaksFacialExpressionRejecting:
                {
                    return HandleCharacterTalksEvent(passage, character01, message, CharacterExpression.SpeaksRejecting,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01SpeaksFacialExpressionAmazed:
                {
                    return HandleCharacterTalksEvent(passage, character01, message, CharacterExpression.SpeaksAmazed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01SpeaksFacialExpressionQuestioning:
                {
                    return HandleCharacterTalksEvent(passage, character01, message, CharacterExpression.SpeaksQuestioning,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01SpeaksFacialExpressionCritical:
                {
                    return HandleCharacterTalksEvent(passage, character01, message, CharacterExpression.SpeaksCritical,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01SpeaksFacialExpressionSmilingBig:
                {
                    return HandleCharacterTalksEvent(passage, character01, message, CharacterExpression.SpeaksSmilingBig,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01SpeaksFacialExpressionLaughing:
                {
                    return HandleCharacterTalksEvent(passage, character01, message, CharacterExpression.SpeaksLaughing,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01SpeaksFacialExpressionSmiling:
                {
                    return HandleCharacterTalksEvent(passage, character01, message, CharacterExpression.SpeaksSmiling,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01SpeaksFacialExpressionNeutralRelaxed:
                {
                    return HandleCharacterTalksEvent(passage, character01, message, CharacterExpression.SpeaksNeutralRelaxed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01SpeaksFacialExpressionNeutral:
                {
                    return HandleCharacterTalksEvent(passage, character01, message, CharacterExpression.SpeaksNeutral,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character01SpeaksFacialExpressionProud:
                {
                    return HandleCharacterTalksEvent(passage, character01, message, CharacterExpression.SpeaksProud,
                        kiteNovelEventList.NovelEvents);
                }

                case NovelKeyWord.EntryCharacter02:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksNeutralRelaxed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter02FacialExpressionScared:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksScared,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter02FacialExpressionDefeated:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksDefeated,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter02FacialExpressionDissatisfied:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksDissatisfied,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter02FacialExpressionRejecting:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksRejecting,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter02FacialExpressionAmazed:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksAmazed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter02FacialExpressionQuestioning:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksQuestioning,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter02FacialExpressionCritical:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksCritical,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter02FacialExpressionSmilingBig:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksSmilingBig,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter02FacialExpressionLaughing:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksLaughing,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter02FacialExpressionSmiling:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksSmiling,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter02FacialExpressionNeutralRelaxed:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksNeutralRelaxed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter02FacialExpressionNeutral:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksNeutral,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter02FacialExpressionProud:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksProud,
                        kiteNovelEventList.NovelEvents);
                }

                case NovelKeyWord.Character02Looks:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksNeutralRelaxed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02LooksFacialExpressionScared:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksScared,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02LooksFacialExpressionDefeated:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksDefeated,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02LooksFacialExpressionDissatisfied:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksDissatisfied,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02LooksFacialExpressionRejecting:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksRejecting,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02LooksFacialExpressionAmazed:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksAmazed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02LooksFacialExpressionQuestioning:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksQuestioning,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02LooksFacialExpressionCritical:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksCritical,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02LooksFacialExpressionSmilingBig:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksSmilingBig,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02LooksFacialExpressionLaughing:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksLaughing,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02LooksFacialExpressionSmiling:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksSmiling,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02LooksFacialExpressionNeutralRelaxed:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksNeutralRelaxed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02LooksFacialExpressionNeutral:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksNeutral,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02LooksFacialExpressionProud:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksProud,
                        kiteNovelEventList.NovelEvents);
                }

                case NovelKeyWord.Character02Speaks:
                {
                    return HandleCharacterTalksEvent(passage, character02, message, CharacterExpression.SpeaksNeutralRelaxed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02SpeaksFacialExpressionScared:
                {
                    return HandleCharacterTalksEvent(passage, character02, message, CharacterExpression.SpeaksScared,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02SpeaksFacialExpressionDefeated:
                {
                    return HandleCharacterTalksEvent(passage, character02, message, CharacterExpression.SpeaksDefeated,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02SpeaksFacialExpressionDissatisfied:
                {
                    return HandleCharacterTalksEvent(passage, character02, message, CharacterExpression.SpeaksDissatisfied,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02SpeaksFacialExpressionRejecting:
                {
                    return HandleCharacterTalksEvent(passage, character02, message, CharacterExpression.SpeaksRejecting,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02SpeaksFacialExpressionAmazed:
                {
                    return HandleCharacterTalksEvent(passage, character02, message, CharacterExpression.SpeaksAmazed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02SpeaksFacialExpressionQuestioning:
                {
                    return HandleCharacterTalksEvent(passage, character02, message, CharacterExpression.SpeaksQuestioning,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02SpeaksFacialExpressionCritical:
                {
                    return HandleCharacterTalksEvent(passage, character02, message, CharacterExpression.SpeaksCritical,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02SpeaksFacialExpressionSmilingBig:
                {
                    return HandleCharacterTalksEvent(passage, character02, message, CharacterExpression.SpeaksSmilingBig,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02SpeaksFacialExpressionLaughing:
                {
                    return HandleCharacterTalksEvent(passage, character02, message, CharacterExpression.SpeaksLaughing,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02SpeaksFacialExpressionSmiling:
                {
                    return HandleCharacterTalksEvent(passage, character02, message, CharacterExpression.SpeaksSmiling,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02SpeaksFacialExpressionNeutralRelaxed:
                {
                    return HandleCharacterTalksEvent(passage, character02, message, CharacterExpression.SpeaksNeutralRelaxed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02SpeaksFacialExpressionNeutral:
                {
                    return HandleCharacterTalksEvent(passage, character02, message, CharacterExpression.SpeaksNeutral,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character02SpeaksFacialExpressionProud:
                {
                    return HandleCharacterTalksEvent(passage, character02, message, CharacterExpression.SpeaksProud,
                        kiteNovelEventList.NovelEvents);
                }

                case NovelKeyWord.EntryCharacter03:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksNeutralRelaxed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter03FacialExpressionScared:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksScared,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter03FacialExpressionDefeated:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksDefeated,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter03FacialExpressionDissatisfied:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksDissatisfied,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter03FacialExpressionRejecting:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksRejecting,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter03FacialExpressionAmazed:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksAmazed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter03FacialExpressionQuestioning:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksQuestioning,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter03FacialExpressionCritical:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksCritical,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter03FacialExpressionSmilingBig:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksSmilingBig,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter03FacialExpressionLaughing:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksLaughing,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter03FacialExpressionSmiling:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksSmiling,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter03FacialExpressionNeutralRelaxed:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksNeutralRelaxed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter03FacialExpressionNeutral:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksNeutral,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.EntryCharacter03FacialExpressionProud:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksProud,
                        kiteNovelEventList.NovelEvents);
                }

                case NovelKeyWord.Character03Looks:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksNeutralRelaxed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03LooksFacialExpressionScared:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksScared,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03LooksFacialExpressionDefeated:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksDefeated,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03LooksFacialExpressionDissatisfied:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksDissatisfied,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03LooksFacialExpressionRejecting:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksRejecting,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03LooksFacialExpressionAmazed:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksAmazed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03LooksFacialExpressionQuestioning:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksQuestioning,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03LooksFacialExpressionCritical:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksCritical,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03LooksFacialExpressionSmilingBig:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksSmilingBig,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03LooksFacialExpressionLaughing:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksLaughing,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03LooksFacialExpressionSmiling:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksSmiling,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03LooksFacialExpressionNeutralRelaxed:
                {
                    return HandleCharacterComesEvent(passage, character02, CharacterExpression.LooksNeutralRelaxed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03LooksFacialExpressionNeutral:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksNeutral,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03LooksFacialExpressionProud:
                {
                    return HandleCharacterComesEvent(passage, character03, CharacterExpression.LooksProud,
                        kiteNovelEventList.NovelEvents);
                }

                case NovelKeyWord.Character03Speaks:
                {
                    return HandleCharacterTalksEvent(passage, character03, message, CharacterExpression.SpeaksNeutralRelaxed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03SpeaksFacialExpressionScared:
                {
                    return HandleCharacterTalksEvent(passage, character03, message, CharacterExpression.SpeaksScared,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03SpeaksFacialExpressionDefeated:
                {
                    return HandleCharacterTalksEvent(passage, character03, message, CharacterExpression.SpeaksDefeated,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03SpeaksFacialExpressionDissatisfied:
                {
                    return HandleCharacterTalksEvent(passage, character03, message, CharacterExpression.SpeaksDissatisfied,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03SpeaksFacialExpressionRejecting:
                {
                    return HandleCharacterTalksEvent(passage, character03, message, CharacterExpression.SpeaksRejecting,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03SpeaksFacialExpressionAmazed:
                {
                    return HandleCharacterTalksEvent(passage, character03, message, CharacterExpression.SpeaksAmazed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03SpeaksFacialExpressionQuestioning:
                {
                    return HandleCharacterTalksEvent(passage, character03, message, CharacterExpression.SpeaksQuestioning,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03SpeaksFacialExpressionCritical:
                {
                    return HandleCharacterTalksEvent(passage, character03, message, CharacterExpression.SpeaksCritical,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03SpeaksFacialExpressionSmilingBig:
                {
                    return HandleCharacterTalksEvent(passage, character03, message, CharacterExpression.SpeaksSmilingBig,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03SpeaksFacialExpressionLaughing:
                {
                    return HandleCharacterTalksEvent(passage, character03, message, CharacterExpression.SpeaksLaughing,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03SpeaksFacialExpressionSmiling:
                {
                    return HandleCharacterTalksEvent(passage, character03, message, CharacterExpression.SpeaksSmiling,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03SpeaksFacialExpressionNeutralRelaxed:
                {
                    return HandleCharacterTalksEvent(passage, character03, message, CharacterExpression.SpeaksNeutralRelaxed,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03SpeaksFacialExpressionNeutral:
                {
                    return HandleCharacterTalksEvent(passage, character03, message, CharacterExpression.SpeaksNeutral,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.Character03SpeaksFacialExpressionProud:
                {
                    return HandleCharacterTalksEvent(passage, character03, message, CharacterExpression.SpeaksProud,
                        kiteNovelEventList.NovelEvents);
                }

                case NovelKeyWord.PlayerCharacterSpeaks:
                {
                    return HandleCharacterTalksEvent(passage, CharacterRole.PLAYER, message, CharacterExpression.LooksNeutralRelaxed, 
                        kiteNovelEventList.NovelEvents);
                }

                case NovelKeyWord.InfoMessageIsDisplayed:
                {
                    return HandleCharacterTalksEvent(passage, CharacterRole.INFO, message, CharacterExpression.LooksNeutralRelaxed, kiteNovelEventList.NovelEvents);
                }

                case NovelKeyWord.PlaySoundWaterPouring:
                {
                    return HandlePlaySoundEvent(passage, KiteSound.WaterPouring, kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.PlaySoundLeaveScene:
                {
                    return HandlePlaySoundEvent(passage, KiteSound.LeaveScene, kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.PlaySoundTelephoneCall:
                {
                    return HandlePlaySoundEvent(passage, KiteSound.TelephoneCall, kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.PlaySoundPaperSound:
                {
                    return HandlePlaySoundEvent(passage, KiteSound.PaperSound, kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.PlaySoundManLaughing:
                {
                    return HandlePlaySoundEvent(passage, KiteSound.ManLaughing, kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.AnimationAbspielenWaterPouring:
                {
                    return HandlePlayAnimationEvent(passage, KiteAnimation.WATER_POURING,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.End:
                {
                    return HandleEndNovelEvent(passage.Label, kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.GptPromptMitDefaultCompletionHandler:
                {
                    return HandleGptRequestEvent(passage, message, CompletionHandler.DefaultCompletionHandler,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.PersistentSaving:
                {
                    return HandleSaveDataEvent(passage, message, kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.SetVariable:
                {
                    return HandleSetVariableEvent(passage, message, kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.DetermineVariableFromBooleanExpression:
                {
                    return HandleSetVariableFromBooleanExpressionEvent(passage, message,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.AddFeedback:
                {
                    return HandleAddFeedbackEvent(passage, message, kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.AddFeedbackUnderCondition:
                {
                    return HandleAddFeedbackUnderConditionEvent(passage, message, kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasAccessToFunding:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.AccessToFunding,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasGenderPayGap:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.GenderPayGap, kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasUndervaluationOfWomenLedBusinesses:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.UndervaluationOfWomenLedBusinesses,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasRiskAversionBias:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.RiskAversionBias,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasConfirmationBias:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.ConfirmationBias,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasTokenism:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.Tokenism, kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasBiasInThePerceptionOfLeadershipAbilities:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.InPerceptionOfLeadershipAbilities,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasRacialAndEthnicBiases:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.RacistAndEthnicBiases,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasSocioeconomicBiases:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.SocioeconomicBiases,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasAgeAndGenerationBiases:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.AgeAndGenerationalBiases,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasStereotypesAgainstWomenInNonTraditionalIndustries:
                {
                    return HandleBiasEvent(passage,
                        DiscriminationBias.StereotypesAgainstWomenInNonTraditionalIndustries,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasCulturalAndReligiousBiases:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.CulturalAndReligiousBiases,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasMaternalBias:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.MaternalBias, kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasBiasesAgainstWomenWithChildren:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.AgainstWomenWithChildren,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasExpectationsRegardingFamilyPlanning:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.ExpectationsRegardingFamilyPlanning,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasWorkLifeBalanceExpectations:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.WorkLifeBalanceExpectations,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasGenderSpecificStereotypes:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.GenderSpecificStereotypes,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasTightropeBias:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.TightropeBias, kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasMicroaggressions:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.Microaggressions,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasPerformanceAttributionBias:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.PerformanceAttributionBias,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasBiasInMediaAndAdvertising:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.InMediaAndAdvertising,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasUnconsciousBiasInCommunication:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.UnconsciousBiasInCommunication,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasProveItAgainBias:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.ProveItAgainBias,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasHeteronormativityBias:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.HeteronormativityBias,
                        kiteNovelEventList.NovelEvents);
                }
                case NovelKeyWord.RelevantBiasBenevolentSexismBias:
                {
                    return HandleBiasEvent(passage, DiscriminationBias.BenevolentSexismBias,
                        kiteNovelEventList.NovelEvents);
                }
            }

            return null;
        }

        private static NovelKeyWord FindFirstKeyWordInText(string text)
        {
            foreach (NovelKeyWord novelKeyWord in Enum.GetValues(typeof(NovelKeyWord)))
            {
                string keyWord = NovelKeyWordHelper.GetNovelKeywordString(novelKeyWord);
                if (text.Contains(keyWord))
                {
                    Debug.Log($"keyWord: {keyWord}");
                    return NovelKeyWordHelper.GetKeyWordOutOfValue(keyWord);
                }
            }

            return NovelKeyWord.None;
        }
    }
}