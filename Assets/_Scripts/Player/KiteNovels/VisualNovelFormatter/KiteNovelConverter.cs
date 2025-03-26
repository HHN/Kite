using System;
using System.Collections.Generic;
using Assets._Scripts.Novel;
using Assets._Scripts.Player.Kite_Novels.Visual_Novel_Formatter;
using UnityEngine;

namespace Assets._Scripts.Player.KiteNovels.VisualNovelFormatter
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
                VisualNovel novel = new VisualNovel();

                novel.id = folder.NovelMetaData.IdNumberOfNovel;
                novel.title = folder.NovelMetaData.TitleOfNovel;
                novel.description = folder.NovelMetaData.DescriptionOfNovel;
                novel.context = folder.NovelMetaData.ContextForPrompt;
                novel.novelEvents = folder.NovelEventList.NovelEvents;

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
                VisualNovelEvent lastCreatedEvent =
                    CreateVisualNovelEvents(passage, kiteNovelMetaData, kiteNovelEventList);
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

        private static void InitializeKiteNovelEventList(KiteNovelMetaData kiteNovelMetaData,
            KiteNovelEventList kiteNovelEventList, string startLabel)
        {

            string connectionLabel = "initalCharakterJoinsEvent001";
            string id = "initialLocationEvent001";
            string nextId = connectionLabel;

            VisualNovelEvent initialLocationEvent = KiteNovelEventFactory.GetSetBackgroundEvent(id, nextId);
            kiteNovelEventList.NovelEvents.Add(initialLocationEvent);

            id = connectionLabel;
            nextId = startLabel;
            CharacterRole character = CharacterTypeHelper.ValueOf(kiteNovelMetaData.TalkingPartner01);
            if (character == CharacterRole.NONE)
            {
                Debug.LogWarning("While loading " + kiteNovelMetaData.TitleOfNovel +
                                 ": Initial CharacterRole not found!");
            }

            CharacterExpression expression =
                CharacterExpressionHelper.ValueOf(kiteNovelMetaData.StartTalkingPartnerEmotion);

            if (expression == CharacterExpression.NONE)
            {
                Debug.LogWarning("While loading " + kiteNovelMetaData.TitleOfNovel +
                                 ": Initial CharacterRole-Expression not found!");
            }

            VisualNovelEvent initialCharacterJoinsEvent =
                KiteNovelEventFactory.GetCharacterJoinsEvent(id, nextId, character, expression);
            kiteNovelEventList.NovelEvents.Add(initialCharacterJoinsEvent);
        }

        private static VisualNovelEvent HandleLocationEvent(TweePassage twee, Location location,
            List<VisualNovelEvent> list)
        {
            string id = twee?.Label;
            string nextId = twee?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetSetBackgroundEvent(id, nextId);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleCharacterTalksEvent(TweePassage twee, CharacterRole character,
            string dialogMessage, CharacterExpression expression, List<VisualNovelEvent> list)
        {
            string id = twee?.Label;
            string nextId = twee?.Links?[0]?.Target;
            VisualNovelEvent novelEvent =
                KiteNovelEventFactory.GetCharacterTalksEvent(id, nextId, character, dialogMessage, expression);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleCharacterComesEvent(TweePassage twee, CharacterRole character,
            CharacterExpression expressionType, List<VisualNovelEvent> list)
        {
            string id = twee?.Label;
            string nextId = twee?.Links?[0]?.Target;
            VisualNovelEvent novelEvent =
                KiteNovelEventFactory.GetCharacterJoinsEvent(id, nextId, character, expressionType);
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

        private static VisualNovelEvent HandleFreeTextInputEvent(TweePassage twee, string message,
            List<VisualNovelEvent> list)
        {
            string[] parts = message.Split(new[] { ':' }, 2);

            if (parts.Length != 2)
            {
                Debug.LogWarning("While creating Visual Novels: Free Text Input Event could not be created.");
                return null;
            }

            string id = twee?.Label;
            string nextId = twee?.Links?[0]?.Target;
            string question = parts[1].Trim();
            string variablesName = parts[0].Trim();
            VisualNovelEvent novelEvent =
                KiteNovelEventFactory.GetFreeTextInputEvent(id, nextId, question, variablesName);
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
            VisualNovelEvent visualNovelEvent =
                ConvertListOfDataObjectsIntoKiteNovelEvent(passage, message, kiteNovelMetaData, kiteNovelEventList);
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

            CharacterRole charakter01 = CharacterTypeHelper.ValueOf(kiteNovelMetaData.TalkingPartner01);
            CharacterRole charakter02 = CharacterTypeHelper.ValueOf(kiteNovelMetaData.TalkingPartner02);
            CharacterRole charakter03 = CharacterTypeHelper.ValueOf(kiteNovelMetaData.TalkingPartner03);

            NovelKeyWord recognizedKeyWord = FindFirstKeyWordInText(passage.Passage);

            switch (recognizedKeyWord)
            {
                case (NovelKeyWord.SZENE_BUERO):
                {
                    return HandleLocationEvent(passage, Location.OFFICE, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_01):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_NEUTRAL_ENTSPANNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_ERSCHROCKEN):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_ERSCHROCKEN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_GENERVT):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_GENERVT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_UNZUFRIEDEN):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_UNZUFRIEDEN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_ABLEHNEND):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_ABLEHNEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_ERSTAUNT):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_ERSTAUNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_FRAGEND):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_ERSTAUNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_KRITISCH):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_KRITISCH,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_LAECHELN_GROSS):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_LAECHELN_GROSS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_LACHEND):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_LACHEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_LAECHELN):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_LAECHELN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_NEUTRAL_ENTSPANNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_NEUTRAL):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_NEUTRAL,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_STOLZ):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_STOLZ,
                        kiteNovelEventList.NovelEvents);
                }

                case (NovelKeyWord.CHARAKTER_01_SCHAUT):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_NEUTRAL_ENTSPANNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_ERSCHROCKEN):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_ERSCHROCKEN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_GENERVT):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_GENERVT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_UNZUFRIEDEN):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_UNZUFRIEDEN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_ABLEHNEND):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_ABLEHNEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_ERSTAUNT):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_ERSTAUNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_FRAGEND):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_ERSTAUNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_KRITISCH):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_KRITISCH,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_LAECHELN_GROSS):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_LAECHELN_GROSS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_LACHEND):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_LACHEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_LAECHELN):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_LAECHELN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_NEUTRAL_ENTSPANNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_NEUTRAL,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_STOLZ):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCHAUT_STOLZ,
                        kiteNovelEventList.NovelEvents);
                }

                case (NovelKeyWord.CHARAKTER_01_SPRICHT):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message,
                        CharacterExpression.SPRICHT_NEUTRAL_ENTSPANNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_ERSCHROCKEN):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SPRICHT_ERSCHROCKEN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_GENERVT):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SPRICHT_GENERVT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_UNZUFRIEDEN):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SPRICHT_UNZUFRIEDEN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_ABLEHNEND):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SPRICHT_ABLEHNEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_ERSTAUNT):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SPRICHT_ERSTAUNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_FRAGEND):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SPRICHT_FRAGEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_KRITISCH):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SPRICHT_KRITISCH,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_LAECHELN_GROSS):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SPRICHT_LAECHELN_GROSS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_LACHEND):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SPRICHT_LACHEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_LAECHELN):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SPRICHT_LAECHELN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message,
                        CharacterExpression.SPRICHT_NEUTRAL_ENTSPANNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SPRICHT_NEUTRAL,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_STOLZ):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SPRICHT_STOLZ,
                        kiteNovelEventList.NovelEvents);
                }

                case (NovelKeyWord.EINTRITT_CHARAKTER_02):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_NEUTRAL_ENTSPANNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_ERSCHROCKEN):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_ERSCHROCKEN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_GENERVT):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_GENERVT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_UNZUFRIEDEN):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_UNZUFRIEDEN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_ABLEHNEND):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_ABLEHNEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_ERSTAUNT):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_ERSTAUNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_FRAGEND):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_FRAGEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_KRITISCH):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_KRITISCH,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_LAECHELN_GROSS):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_LAECHELN_GROSS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_LACHEND):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_LACHEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_LAECHELN):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_LAECHELN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_NEUTRAL_ENTSPANNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_NEUTRAL):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_NEUTRAL,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_STOLZ):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_STOLZ,
                        kiteNovelEventList.NovelEvents);
                }

                case (NovelKeyWord.CHARAKTER_02_SCHAUT):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_NEUTRAL_ENTSPANNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_ERSCHROCKEN):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_ERSCHROCKEN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_GENERVT):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_GENERVT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_UNZUFRIEDEN):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_UNZUFRIEDEN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_ABLEHNEND):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_ABLEHNEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_ERSTAUNT):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_ERSTAUNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_FRAGEND):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_ERSTAUNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_KRITISCH):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_KRITISCH,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_LAECHELN_GROSS):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_LAECHELN_GROSS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_LACHEND):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_LACHEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_LAECHELN):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_LAECHELN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_NEUTRAL_ENTSPANNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_NEUTRAL,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_STOLZ):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_STOLZ,
                        kiteNovelEventList.NovelEvents);
                }

                case (NovelKeyWord.CHARAKTER_02_SPRICHT):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message,
                        CharacterExpression.SPRICHT_NEUTRAL_ENTSPANNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_ERSCHROCKEN):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SPRICHT_ERSCHROCKEN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_GENERVT):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SPRICHT_GENERVT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_UNZUFRIEDEN):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SPRICHT_UNZUFRIEDEN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_ABLEHNEND):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SPRICHT_ABLEHNEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_ERSTAUNT):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SPRICHT_ERSTAUNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_FRAGEND):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SPRICHT_FRAGEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_KRITISCH):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SPRICHT_KRITISCH,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_LAECHELN_GROSS):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SPRICHT_LAECHELN_GROSS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_LACHEND):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SPRICHT_LACHEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_LAECHELN):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SPRICHT_LAECHELN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message,
                        CharacterExpression.SPRICHT_NEUTRAL_ENTSPANNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SPRICHT_NEUTRAL,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_STOLZ):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SPRICHT_STOLZ,
                        kiteNovelEventList.NovelEvents);
                }

                case (NovelKeyWord.EINTRITT_CHARAKTER_03):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_NEUTRAL_ENTSPANNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_ERSCHROCKEN):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_ERSCHROCKEN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_GENERVT):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_GENERVT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_UNZUFRIEDEN):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_UNZUFRIEDEN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_ABLEHNEND):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_ABLEHNEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_ERSTAUNT):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_ERSTAUNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_FRAGEND):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_FRAGEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_KRITISCH):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_KRITISCH,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_LAECHELN_GROSS):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_LAECHELN_GROSS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_LACHEND):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_LACHEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_LAECHELN):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_LAECHELN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_NEUTRAL_ENTSPANNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_NEUTRAL):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_NEUTRAL,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_STOLZ):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_STOLZ,
                        kiteNovelEventList.NovelEvents);
                }

                case (NovelKeyWord.CHARAKTER_03_SCHAUT):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_NEUTRAL_ENTSPANNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_ERSCHROCKEN):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_ERSCHROCKEN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_GENERVT):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_GENERVT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_UNZUFRIEDEN):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_UNZUFRIEDEN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_ABLEHNEND):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_ABLEHNEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_ERSTAUNT):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_ERSTAUNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_FRAGEND):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_ERSTAUNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_KRITISCH):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_KRITISCH,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_LAECHELN_GROSS):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_LAECHELN_GROSS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_LACHEND):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_LACHEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_LAECHELN):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_LAECHELN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCHAUT_NEUTRAL_ENTSPANNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_NEUTRAL,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_STOLZ):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCHAUT_STOLZ,
                        kiteNovelEventList.NovelEvents);
                }

                case (NovelKeyWord.CHARAKTER_03_SPRICHT):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message,
                        CharacterExpression.SPRICHT_NEUTRAL_ENTSPANNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_ERSCHROCKEN):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SPRICHT_ERSCHROCKEN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_GENERVT):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SPRICHT_GENERVT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_UNZUFRIEDEN):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SPRICHT_UNZUFRIEDEN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_ABLEHNEND):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SPRICHT_ABLEHNEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_ERSTAUNT):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SPRICHT_ERSTAUNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_FRAGEND):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SPRICHT_FRAGEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_KRITISCH):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SPRICHT_KRITISCH,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_LAECHELN_GROSS):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SPRICHT_LAECHELN_GROSS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_LACHEND):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SPRICHT_LACHEND,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_LAECHELN):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SPRICHT_LAECHELN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message,
                        CharacterExpression.SPRICHT_NEUTRAL_ENTSPANNT,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SPRICHT_NEUTRAL,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_STOLZ):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SPRICHT_STOLZ,
                        kiteNovelEventList.NovelEvents);
                }

                case (NovelKeyWord.SPIELER_CHARAKTER_SPRICHT):
                {
                    return HandleCharacterTalksEvent(passage, CharacterRole.PLAYER, message,
                        CharacterExpression.SCHAUT_NEUTRAL_ENTSPANNT, kiteNovelEventList.NovelEvents);
                }

                case (NovelKeyWord.INFO_NACHRICHT_WIRD_ANGEZEIGT):
                {
                    return HandleCharacterTalksEvent(passage, CharacterRole.INFO, message,
                        CharacterExpression.SCHAUT_NEUTRAL_ENTSPANNT,
                        kiteNovelEventList.NovelEvents);
                }

                case (NovelKeyWord.SOUND_ABSPIELEN_WATER_POURING):
                {
                    return HandlePlaySoundEvent(passage, KiteSound.WaterPouring, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.SOUND_ABSPIELEN_LEAVE_SCENE):
                {
                    return HandlePlaySoundEvent(passage, KiteSound.LeaveScene, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.SOUND_ABSPIELEN_TELEPHONE_CALL):
                {
                    return HandlePlaySoundEvent(passage, KiteSound.TelephoneCall, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.SOUND_ABSPIELEN_PAPER_SOUND):
                {
                    return HandlePlaySoundEvent(passage, KiteSound.PaperSound, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.SOUND_ABSPIELEN_MAN_LAUGHING):
                {
                    return HandlePlaySoundEvent(passage, KiteSound.ManLaughing, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.ANIMATION_ABSPIELEN_WATER_POURING):
                {
                    return HandlePlayAnimationEvent(passage, KiteAnimation.WATER_POURING,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.ENDE):
                {
                    return HandleEndNovelEvent(passage.Label, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.FREITEXT_EINGABE):
                {
                    return HandleFreeTextInputEvent(passage, message, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.GPT_PROMPT_MIT_DEFAULT_COMPLETION_HANDLER):
                {
                    return HandleGptRequestEvent(passage, message, CompletionHandler.DEFAULT_COMPLETION_HANDLER,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.PERSISTENTES_SPEICHERN):
                {
                    return HandleSaveDataEvent(passage, message, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.VARIABLE_SETZEN):
                {
                    return HandleSetVariableEvent(passage, message, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.VARIABLE_AUS_BOOLSCHEM_AUSDRUCK_BESTIMMEN):
                {
                    return HandleSetVariableFromBooleanExpressionEvent(passage, message,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.FEEDBACK_HINZUFUEGEN):
                {
                    return HandleAddFeedbackEvent(passage, message, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.FEEDBACK_UNTER_BEDINGUNG_HINZUFUEGEN):
                {
                    return HandleAddFeedbackUnderConditionEvent(passage, message, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_FINANZIERUNGSZUGANG):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.ACCESS_TO_FUNDING,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_GENDER_PAY_GAP):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.GENDER_PAY_GAP, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_UNTERBEWERTUNG_WEIBLICH_GEFUEHRTER_UNTERNEHMEN):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.UNDERVALUATION_OF_WOMEN_LED_BUSINESSES,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_RISK_AVERSION_BIAS):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.RISK_AVERSION_BIAS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_BESTAETIGUNGSVERZERRUNG):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.CONFIRMATION_BIAS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_TOKENISM):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.TOKENISM, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_BIAS_IN_DER_WAHRNEHMUNG_VON_FUEHRUNGSFAEHIGKEITEN):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.IN_PERCEPTION_OF_LEADERSHIP_ABILITIES,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_RASSISTISCHE_UND_ETHNISCHE_BIASES):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.RACIST_AND_ETHNIC_BIASES,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_SOZIOOEKONOMISCHE_BIASES):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.SOCIOECONOMIC_BIASES,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_ALTER_UND_GENERATIONEN_BIASES):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.AGE_AND_GENERATIONAL_BIASES,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_SEXUALITAETSBEZOGENE_BIASES):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.SEXUALITY_RELATED_BIASES,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_BIASES_GEGENUEBER_FRAUEN_MIT_BEHINDERUNGEN):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.AGAINST_WOMEN_WITH_DISABILITIES,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_STEREOTYPE_GEGENUEBER_FRAUEN_IN_NICHT_TRADITIONELLEN_BRANCHEN):
                {
                    return HandleBiasEvent(passage,
                        DiscriminationBias.STEREOTYPES_AGAINST_WOMEN_IN_NON_TRADITIONAL_INDUSTRIES,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_KULTURELLE_UND_RELIGIOESE_BIASES):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.CULTURAL_AND_RELIGIOUS_BIASES,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_MATERNAL_BIAS):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.MATERNAL_BIAS, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_BIASES_GEGENUEBER_FRAUEN_MIT_KINDERN):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.AGAINST_WOMEN_WITH_CHILDREN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_ERWARTUNGSHALTUNG_BEZUEGLICH_FAMILIENPLANUNG):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.EXPECTATIONS_REGARDING_FAMILY_PLANNING,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_WORK_LIFE_BALANCE_ERWARTUNGEN):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.WORK_LIFE_BALANCE_EXPECTATIONS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_GESCHLECHTSSPEZIFISCHE_STEREOTYPEN):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.GENDER_SPECIFIC_STEREOTYPES,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_TIGHTROPE_BIAS):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.TIGHTROPE_BIAS, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_MIKROAGGRESSIONEN):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.MICROAGGRESSIONS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_LEISTUNGSATTRIBUTIONS_BIAS):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.PERFORMANCE_ATTRIBUTION_BIAS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_BIAS_IN_MEDIEN_UND_WERBUNG):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.IN_MEDIA_AND_ADVERTISING,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_UNBEWUSSTE_BIAS_IN_DER_KOMMUNIKATION):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.UNCONSCIOUS_BIAS_IN_COMMUNICATION,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_PROVE_IT_AGAIN_BIAS):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.PROVE_IT_AGAIN_BIAS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_HETERONORMATIVITAET_BIAS):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.HETERONORMATIVITAET_BIAS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RELEVANTER_BIAS_BENEVOLENTER_SEXISMUS_BIAS):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.BENEVOLENTER_SEXISMUS_BIAS,
                        kiteNovelEventList.NovelEvents);
                }
            }

            return null;
        }

        private static NovelKeyWord FindFirstKeyWordInText(string text)
        {
            foreach (string keyWord in NovelKeyWordValue.ALL_KEY_WORDS)
            {
                if (text.Contains(keyWord))
                {
                    return NovelKeyWordHelper.GetKeyWordOutOfValue(keyWord);
                }
            }

            return NovelKeyWord.NONE;
        }
    }
}