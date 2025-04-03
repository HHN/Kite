using System;
using System.Collections.Generic;
using Assets._Scripts.Novel;
using Assets._Scripts.Novel.VisualNovelFormatter;
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

        public static KiteNovelEventList ConvertTextDocumentIntoEventList(string tweeFile, KiteNovelMetaData kiteNovelMetaData)
        {
            KiteNovelEventList kiteNovelEventList = new KiteNovelEventList();
            string startEventLabel = TweeProcessor.GetStartLabelFromTweeFile(tweeFile);
            InitializeKiteNovelEventList(kiteNovelMetaData, kiteNovelEventList, startEventLabel);
            List<TweePassage> passages = TweeProcessor.ProcessTweeFile(tweeFile);

            foreach (TweePassage passage in passages)
            {
                VisualNovelEvent lastCreatedEvent = CreateVisualNovelEvents(passage, kiteNovelMetaData, kiteNovelEventList);
                HandleLoop(lastCreatedEvent, startEventLabel, kiteNovelEventList);
                HandleDialogueOptionEvent(passage, kiteNovelEventList.NovelEvents, lastCreatedEvent);
            }

            return kiteNovelEventList;
        }

        private static void HandleLoop(VisualNovelEvent lastEvent, string labelOfStartEvent, KiteNovelEventList kiteNovelEventList)
        {
            if (lastEvent == null || string.IsNullOrEmpty(lastEvent.nextId) || string.IsNullOrEmpty(labelOfStartEvent) || kiteNovelEventList == null)
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
            string connectionLabel = "initalCharakterJoinsEvent001";
            string id = "initialLocationEvent001";
            string nextId = connectionLabel;

            VisualNovelEvent initialLocationEvent = KiteNovelEventFactory.GetSetBackgroundEvent(id, nextId);
            kiteNovelEventList.NovelEvents.Add(initialLocationEvent);

            id = connectionLabel;
            nextId = startLabel;
            CharacterRole character = CharacterTypeHelper.ValueOf(kiteNovelMetaData.TalkingPartner01);
            if (character == CharacterRole.None)
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
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetSetBackgroundEvent(id, nextId);
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
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetGptEvent(id, nextId, prompt, variablesName, completionHandlerId);
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
                Debug.LogWarning("While creating Visual Novels: Calculate Variable from boolean expression Event could not be created.");
                return null;
            }

            string id = twee?.Label;
            string nextId = twee?.Links?[0]?.Target;
            string key = parts[0].Trim();
            string value = parts[1].Trim();
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetCalculateVariableFromBooleanExpressionEvent(id, nextId, key, value);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleAddFeedbackUnderConditionEvent(TweePassage twee, string message,
            List<VisualNovelEvent> list)
        {
            string[] parts = message.Split(new[] { ':' }, 2);

            if (parts.Length != 2)
            {
                Debug.LogWarning("While creating Visual Novels: Add Feedback Under Condition Event could not be created.");
                return null;
            }

            string id = twee?.Label;
            string nextId = twee?.Links?[0]?.Target;
            string key = parts[0].Trim();
            string value = parts[1].Trim();
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetAddFeedbackUnderConditionEvent(id, nextId, key, value);
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

            return recognizedKeyWord switch
            {
                NovelKeyWord.SZENE_BUERO => HandleLocationEvent(passage, Location.OFFICE, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_01 => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautNeutralEntspannt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_ERSCHROCKEN => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautErschrocken, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_GENERVT => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautGenervt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_UNZUFRIEDEN => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautUnzufrieden, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_ABLEHNEND => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautAblehnend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_ERSTAUNT => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautErstaunt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_FRAGEND => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautErstaunt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_KRITISCH => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautKritisch, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_LAECHELN_GROSS => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautLaechelnGross, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_LACHEND => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautLachend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_LAECHELN => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautLaecheln, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautNeutralEntspannt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_NEUTRAL => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautNeutral, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_STOLZ => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautStolz, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SCHAUT => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautNeutralEntspannt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_ERSCHROCKEN => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautErschrocken, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_GENERVT => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautGenervt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_UNZUFRIEDEN => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautUnzufrieden, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_ABLEHNEND => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautAblehnend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_ERSTAUNT => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautErstaunt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_FRAGEND => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautErstaunt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_KRITISCH => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautKritisch, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_LAECHELN_GROSS => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautLaechelnGross, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_LACHEND => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautLachend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_LAECHELN => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautLaecheln, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautNeutralEntspannt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautNeutral, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SCHAUT_GESICHTSAUSDRUCK_STOLZ => HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautStolz, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SPRICHT => HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtNeutralEntspannt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_ERSCHROCKEN => HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtErschrocken, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_GENERVT => HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtGenervt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_UNZUFRIEDEN => HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtUnzufrieden, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_ABLEHNEND => HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtAblehnend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_ERSTAUNT => HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtErstaunt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_FRAGEND => HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtFragend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_KRITISCH => HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtKritisch, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_LAECHELN_GROSS => HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtLaechelnGross, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_LACHEND => HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtLachend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_LAECHELN => HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtLaecheln, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT => HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtNeutralEntspannt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL => HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtNeutral, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_01_SPRICHT_GESICHTSAUSDRUCK_STOLZ => HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtStolz, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_02 => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautNeutralEntspannt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_ERSCHROCKEN => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautErschrocken, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_GENERVT => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautGenervt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_UNZUFRIEDEN => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautUnzufrieden, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_ABLEHNEND => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautAblehnend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_ERSTAUNT => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautErstaunt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_FRAGEND => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautFragend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_KRITISCH => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautKritisch, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_LAECHELN_GROSS => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautLaechelnGross, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_LACHEND => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautLachend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_LAECHELN => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautLaecheln, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautNeutralEntspannt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_NEUTRAL => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautNeutral, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_STOLZ => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautStolz, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SCHAUT => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautNeutralEntspannt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_ERSCHROCKEN => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautErschrocken, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_GENERVT => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautGenervt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_UNZUFRIEDEN => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautUnzufrieden, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_ABLEHNEND => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautAblehnend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_ERSTAUNT => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautErstaunt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_FRAGEND => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautErstaunt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_KRITISCH => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautKritisch, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_LAECHELN_GROSS => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautLaechelnGross, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_LACHEND => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautLachend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_LAECHELN => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautLaecheln, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautNeutralEntspannt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautNeutral, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SCHAUT_GESICHTSAUSDRUCK_STOLZ => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautStolz, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SPRICHT => HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtNeutralEntspannt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_ERSCHROCKEN => HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtErschrocken, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_GENERVT => HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtGenervt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_UNZUFRIEDEN => HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtUnzufrieden, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_ABLEHNEND => HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtAblehnend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_ERSTAUNT => HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtErstaunt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_FRAGEND => HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtFragend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_KRITISCH => HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtKritisch, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_LAECHELN_GROSS => HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtLaechelnGross, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_LACHEND => HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtLachend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_LAECHELN => HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtLaecheln, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT => HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtNeutralEntspannt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL => HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtNeutral, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_02_SPRICHT_GESICHTSAUSDRUCK_STOLZ => HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtStolz, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_03 => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautNeutralEntspannt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_ERSCHROCKEN => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautErschrocken, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_GENERVT => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautGenervt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_UNZUFRIEDEN => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautUnzufrieden, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_ABLEHNEND => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautAblehnend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_ERSTAUNT => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautErstaunt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_FRAGEND => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautFragend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_KRITISCH => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautKritisch, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_LAECHELN_GROSS => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautLaechelnGross, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_LACHEND => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautLachend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_LAECHELN => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautLaecheln, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautNeutralEntspannt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_NEUTRAL => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautNeutral, kiteNovelEventList.NovelEvents),
                NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_STOLZ => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautStolz, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SCHAUT => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautNeutralEntspannt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_ERSCHROCKEN => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautErschrocken, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_GENERVT => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautGenervt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_UNZUFRIEDEN => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautUnzufrieden, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_ABLEHNEND => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautAblehnend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_ERSTAUNT => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautErstaunt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_FRAGEND => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautErstaunt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_KRITISCH => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautKritisch, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_LAECHELN_GROSS => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautLaechelnGross, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_LACHEND => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautLachend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_LAECHELN => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautLaecheln, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT => HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautNeutralEntspannt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_NEUTRAL => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautNeutral, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SCHAUT_GESICHTSAUSDRUCK_STOLZ => HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautStolz, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SPRICHT => HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtNeutralEntspannt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_ERSCHROCKEN => HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtErschrocken, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_GENERVT => HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtGenervt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_UNZUFRIEDEN => HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtUnzufrieden, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_ABLEHNEND => HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtAblehnend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_ERSTAUNT => HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtErstaunt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_FRAGEND => HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtFragend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_KRITISCH => HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtKritisch, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_LAECHELN_GROSS => HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtLaechelnGross, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_LACHEND => HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtLachend, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_LAECHELN => HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtLaecheln, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL_ENTSPANNT => HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtNeutralEntspannt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_NEUTRAL => HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtNeutral, kiteNovelEventList.NovelEvents),
                NovelKeyWord.CHARAKTER_03_SPRICHT_GESICHTSAUSDRUCK_STOLZ => HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtStolz, kiteNovelEventList.NovelEvents),
                NovelKeyWord.SPIELER_CHARAKTER_SPRICHT => HandleCharacterTalksEvent(passage, CharacterRole.Player, message, CharacterExpression.SchautNeutralEntspannt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.INFO_NACHRICHT_WIRD_ANGEZEIGT => HandleCharacterTalksEvent(passage, CharacterRole.Info, message, CharacterExpression.SchautNeutralEntspannt, kiteNovelEventList.NovelEvents),
                NovelKeyWord.SOUND_ABSPIELEN_WATER_POURING => HandlePlaySoundEvent(passage, KiteSound.WaterPouring, kiteNovelEventList.NovelEvents),
                NovelKeyWord.SOUND_ABSPIELEN_LEAVE_SCENE => HandlePlaySoundEvent(passage, KiteSound.LeaveScene, kiteNovelEventList.NovelEvents),
                NovelKeyWord.SOUND_ABSPIELEN_TELEPHONE_CALL => HandlePlaySoundEvent(passage, KiteSound.TelephoneCall, kiteNovelEventList.NovelEvents),
                NovelKeyWord.SOUND_ABSPIELEN_PAPER_SOUND => HandlePlaySoundEvent(passage, KiteSound.PaperSound, kiteNovelEventList.NovelEvents),
                NovelKeyWord.SOUND_ABSPIELEN_MAN_LAUGHING => HandlePlaySoundEvent(passage, KiteSound.ManLaughing, kiteNovelEventList.NovelEvents),
                NovelKeyWord.ANIMATION_ABSPIELEN_WATER_POURING => HandlePlayAnimationEvent(passage, KiteAnimation.WaterPouring, kiteNovelEventList.NovelEvents),
                NovelKeyWord.ENDE => HandleEndNovelEvent(passage.Label, kiteNovelEventList.NovelEvents),
                NovelKeyWord.GPT_PROMPT_MIT_DEFAULT_COMPLETION_HANDLER => HandleGptRequestEvent(passage, message, CompletionHandler.DefaultCompletionHandler, kiteNovelEventList.NovelEvents),
                NovelKeyWord.PERSISTENTES_SPEICHERN => HandleSaveDataEvent(passage, message, kiteNovelEventList.NovelEvents),
                NovelKeyWord.VARIABLE_SETZEN => HandleSetVariableEvent(passage, message, kiteNovelEventList.NovelEvents),
                NovelKeyWord.VARIABLE_AUS_BOOLSCHEM_AUSDRUCK_BESTIMMEN => HandleSetVariableFromBooleanExpressionEvent(passage, message, kiteNovelEventList.NovelEvents),
                NovelKeyWord.FEEDBACK_HINZUFUEGEN => HandleAddFeedbackEvent(passage, message, kiteNovelEventList.NovelEvents),
                NovelKeyWord.FEEDBACK_UNTER_BEDINGUNG_HINZUFUEGEN => HandleAddFeedbackUnderConditionEvent(passage, message, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_FINANZIERUNGSZUGANG => HandleBiasEvent(passage, DiscriminationBias.AccessToFunding, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_GENDER_PAY_GAP => HandleBiasEvent(passage, DiscriminationBias.GenderPayGap, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_UNTERBEWERTUNG_WEIBLICH_GEFUEHRTER_UNTERNEHMEN => HandleBiasEvent(passage, DiscriminationBias.UndervaluationOfWomenLedBusinesses, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_RISK_AVERSION_BIAS => HandleBiasEvent(passage, DiscriminationBias.RiskAversionBias, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_BESTAETIGUNGSVERZERRUNG => HandleBiasEvent(passage, DiscriminationBias.ConfirmationBias, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_TOKENISM => HandleBiasEvent(passage, DiscriminationBias.Tokenism, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_BIAS_IN_DER_WAHRNEHMUNG_VON_FUEHRUNGSFAEHIGKEITEN => HandleBiasEvent(passage, DiscriminationBias.InPerceptionOfLeadershipAbilities, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_RASSISTISCHE_UND_ETHNISCHE_BIASES => HandleBiasEvent(passage, DiscriminationBias.RacistAndEthnicBiases, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_SOZIOOEKONOMISCHE_BIASES => HandleBiasEvent(passage, DiscriminationBias.SocioeconomicBiases, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_ALTER_UND_GENERATIONEN_BIASES => HandleBiasEvent(passage, DiscriminationBias.AgeAndGenerationalBiases, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_SEXUALITAETSBEZOGENE_BIASES => HandleBiasEvent(passage, DiscriminationBias.SexualityRelatedBiases, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_BIASES_GEGENUEBER_FRAUEN_MIT_BEHINDERUNGEN => HandleBiasEvent(passage, DiscriminationBias.AgainstWomenWithDisabilities, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_STEREOTYPE_GEGENUEBER_FRAUEN_IN_NICHT_TRADITIONELLEN_BRANCHEN => HandleBiasEvent(passage, DiscriminationBias.StereotypesAgainstWomenInNonTraditionalIndustries, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_KULTURELLE_UND_RELIGIOESE_BIASES => HandleBiasEvent(passage, DiscriminationBias.CulturalAndReligiousBiases, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_MATERNAL_BIAS => HandleBiasEvent(passage, DiscriminationBias.MaternalBias, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_BIASES_GEGENUEBER_FRAUEN_MIT_KINDERN => HandleBiasEvent(passage, DiscriminationBias.AgainstWomenWithChildren, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_ERWARTUNGSHALTUNG_BEZUEGLICH_FAMILIENPLANUNG => HandleBiasEvent(passage, DiscriminationBias.ExpectationsRegardingFamilyPlanning, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_WORK_LIFE_BALANCE_ERWARTUNGEN => HandleBiasEvent(passage, DiscriminationBias.WorkLifeBalanceExpectations, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_GESCHLECHTSSPEZIFISCHE_STEREOTYPEN => HandleBiasEvent(passage, DiscriminationBias.GenderSpecificStereotypes, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_TIGHTROPE_BIAS => HandleBiasEvent(passage, DiscriminationBias.TightropeBias, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_MIKROAGGRESSIONEN => HandleBiasEvent(passage, DiscriminationBias.Microaggressions, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_LEISTUNGSATTRIBUTIONS_BIAS => HandleBiasEvent(passage, DiscriminationBias.PerformanceAttributionBias, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_BIAS_IN_MEDIEN_UND_WERBUNG => HandleBiasEvent(passage, DiscriminationBias.InMediaAndAdvertising, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_UNBEWUSSTE_BIAS_IN_DER_KOMMUNIKATION => HandleBiasEvent(passage, DiscriminationBias.UnconsciousBiasInCommunication, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_PROVE_IT_AGAIN_BIAS => HandleBiasEvent(passage, DiscriminationBias.ProveItAgainBias, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_HETERONORMATIVITAET_BIAS => HandleBiasEvent(passage, DiscriminationBias.HeteronormativitaetBias, kiteNovelEventList.NovelEvents),
                NovelKeyWord.RELEVANTER_BIAS_BENEVOLENTER_SEXISMUS_BIAS => HandleBiasEvent(passage, DiscriminationBias.BenevolenterSexismusBias, kiteNovelEventList.NovelEvents),
                _ => null
            };
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