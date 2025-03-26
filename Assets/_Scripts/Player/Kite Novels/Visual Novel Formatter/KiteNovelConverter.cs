using System;
using System.Collections.Generic;
using Assets._Scripts.Novel;
using UnityEngine;

namespace Assets._Scripts.Player.Kite_Novels.Visual_Novel_Formatter
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
                novel.image = folder.NovelMetaData.IdNumberOfRepresentationImage;
                novel.context = folder.NovelMetaData.ContextForPrompt;
                novel.isKite2Novel = folder.NovelMetaData.IsKite2Novel;
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
                Debug.LogWarning("While loading " + kiteNovelMetaData.TitleOfNovel +
                                 ": Initial CharacterRole not found!");
            }

            CharacterExpression expression =
                CharacterExpressionHelper.ValueOf(kiteNovelMetaData.StartTalkingPartnerEmotion);

            if (expression == CharacterExpression.None)
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
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetSetBackgroundEvent(id, nextId, location);
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
                case (NovelKeyWord.SzeneBuero):
                {
                    return HandleLocationEvent(passage, Location.OFFICE, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter01):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautNeutralEntspannt,
                        kiteNovelEventList.NovelEvents);
                    // return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.RELAXED,
                    //     kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter01GesichtsausdruckErschrocken):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautErschrocken,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter01GesichtsausdruckGenervt):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautGenervt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter01GesichtsausdruckUnzufrieden):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautUnzufrieden,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter01GesichtsausdruckAblehnend):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautAblehnend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter01GesichtsausdruckErstaunt):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautErstaunt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter01GesichtsausdruckFragend):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautErstaunt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter01GesichtsausdruckKritisch):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautKritisch,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter01GesichtsausdruckLaechelnGross):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautLaechelnGross,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter01GesichtsausdruckLachend):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautLachend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter01GesichtsausdruckLaecheln):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautLaecheln,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter01GesichtsausdruckNeutralEntspannt):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautNeutralEntspannt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter01GesichtsausdruckNeutral):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautNeutral,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter01GesichtsausdruckStolz):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautStolz,
                        kiteNovelEventList.NovelEvents);
                }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_RELAXED):
                // {
                //     return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.RELAXED,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_ASTONISHED):
                // {
                //     return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.ASTONISHED,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_REFUSING):
                // {
                //     return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.REFUSING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_SMILING):
                // {
                //     return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SMILING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_FRIENDLY):
                // {
                //     return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.FRIENDLY,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_LAUGHING):
                // {
                //     return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.LAUGHING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_CRITICAL):
                // {
                //     return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.CRITICAL,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_DECISION_NO):
                // {
                //     return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.DECISION_NO,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_HAPPY):
                // {
                //     return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.HAPPY,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_PROUD):
                // {
                //     return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.PROUD,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_SCARED):
                // {
                //     return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCARED,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_QUESTIONING):
                // {
                //     return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.QUESTIONING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_DEFEATED):
                // {
                //     return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.DEFEATED,
                //         kiteNovelEventList.NovelEvents);
                // }

                case (NovelKeyWord.Charakter01Schaut):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautNeutralEntspannt,
                        kiteNovelEventList.NovelEvents);
                    // return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.RELAXED,
                    //     kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SchautGesichtsausdruckErschrocken):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautErschrocken,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SchautGesichtsausdruckGenervt):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautGenervt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SchautGesichtsausdruckUnzufrieden):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautUnzufrieden,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SchautGesichtsausdruckAblehnend):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautAblehnend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SchautGesichtsausdruckErstaunt):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautErstaunt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SchautGesichtsausdruckFragend):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautErstaunt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SchautGesichtsausdruckKritisch):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautKritisch,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SchautGesichtsausdruckLaechelnGross):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautLaechelnGross,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SchautGesichtsausdruckLachend):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautLachend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SchautGesichtsausdruckLaecheln):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautLaecheln,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SchautGesichtsausdruckNeutralEntspannt):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautNeutralEntspannt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SchautGesichtsausdruckNeutral):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautNeutral,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SchautGesichtsausdruckStolz):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SchautStolz,
                        kiteNovelEventList.NovelEvents);
                }

                case (NovelKeyWord.Charakter01Spricht):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message,
                        CharacterExpression.SprichtNeutralEntspannt,
                        kiteNovelEventList.NovelEvents);
                    // return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.RELAXED,
                    //     kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SprichtGesichtsausdruckErschrocken):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtErschrocken,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SprichtGesichtsausdruckGenervt):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtGenervt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SprichtGesichtsausdruckUnzufrieden):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtUnzufrieden,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SprichtGesichtsausdruckAblehnend):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtAblehnend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SprichtGesichtsausdruckErstaunt):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtErstaunt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SprichtGesichtsausdruckFragend):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtFragend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SprichtGesichtsausdruckKritisch):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtKritisch,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SprichtGesichtsausdruckLaechelnGross):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtLaechelnGross,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SprichtGesichtsausdruckLachend):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtLachend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SprichtGesichtsausdruckLaecheln):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtLaecheln,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SprichtGesichtsausdruckNeutralEntspannt):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message,
                        CharacterExpression.SprichtNeutralEntspannt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SprichtGesichtsausdruckNeutral):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtNeutral,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter01SprichtGesichtsausdruckStolz):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SprichtStolz,
                        kiteNovelEventList.NovelEvents);
                }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_RELAXED):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.RELAXED,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_ASTONISHED):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.ASTONISHED,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_REFUSING):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.REFUSING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_SMILING):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SMILING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_FRIENDLY):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.FRIENDLY,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_LAUGHING):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.LAUGHING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_CRITICAL):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.CRITICAL,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_DECISION_NO):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.DECISION_NO,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_HAPPY):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.HAPPY,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_PROUD):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.PROUD,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_SCARED):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SCARED,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_QUESTIONING):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.QUESTIONING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_DEFEATED):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.DEFEATED,
                //         kiteNovelEventList.NovelEvents);
                // }

                case (NovelKeyWord.EintrittCharakter02):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautNeutralEntspannt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter02GesichtsausdruckErschrocken):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautErschrocken,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter02GesichtsausdruckGenervt):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautGenervt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter02GesichtsausdruckUnzufrieden):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautUnzufrieden,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter02GesichtsausdruckAblehnend):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautAblehnend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter02GesichtsausdruckErstaunt):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautErstaunt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter02GesichtsausdruckFragend):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautFragend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter02GesichtsausdruckKritisch):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautKritisch,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter02GesichtsausdruckLaechelnGross):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautLaechelnGross,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter02GesichtsausdruckLachend):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautLachend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter02GesichtsausdruckLaecheln):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautLaecheln,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter02GesichtsausdruckNeutralEntspannt):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautNeutralEntspannt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter02GesichtsausdruckNeutral):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautNeutral,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter02GesichtsausdruckStolz):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautStolz,
                        kiteNovelEventList.NovelEvents);
                }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_RELAXED):
                // {
                //     return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.RELAXED,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_ASTONISHED):
                // {
                //     return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.ASTONISHED,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_REFUSING):
                // {
                //     return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.REFUSING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_SMILING):
                // {
                //     return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SMILING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_FRIENDLY):
                // {
                //     return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.FRIENDLY,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_LAUGHING):
                // {
                //     return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.LAUGHING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_CRITICAL):
                // {
                //     return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.CRITICAL,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_DECISION_NO):
                // {
                //     return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.DECISION_NO,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_HAPPY):
                // {
                //     return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.HAPPY,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_PROUD):
                // {
                //     return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.PROUD,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_SCARED):
                // {
                //     return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCARED,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_QUESTIONING):
                // {
                //     return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.QUESTIONING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_DEFEATED):
                // {
                //     return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.DEFEATED,
                //         kiteNovelEventList.NovelEvents);
                // }

                case (NovelKeyWord.Charakter02Schaut):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautNeutralEntspannt,
                        kiteNovelEventList.NovelEvents);
                    // return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.RELAXED,
                    //     kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SchautGesichtsausdruckErschrocken):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautErschrocken,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SchautGesichtsausdruckGenervt):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautGenervt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SchautGesichtsausdruckUnzufrieden):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautUnzufrieden,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SchautGesichtsausdruckAblehnend):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautAblehnend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SchautGesichtsausdruckErstaunt):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautErstaunt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SchautGesichtsausdruckFragend):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautErstaunt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SchautGesichtsausdruckKritisch):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautKritisch,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SchautGesichtsausdruckLaechelnGross):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautLaechelnGross,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SchautGesichtsausdruckLachend):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautLachend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SchautGesichtsausdruckLaecheln):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautLaecheln,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SchautGesichtsausdruckNeutralEntspannt):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautNeutralEntspannt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SchautGesichtsausdruckNeutral):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautNeutral,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SchautGesichtsausdruckStolz):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautStolz,
                        kiteNovelEventList.NovelEvents);
                }

                case (NovelKeyWord.Charakter02Spricht):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message,
                        CharacterExpression.SprichtNeutralEntspannt,
                        kiteNovelEventList.NovelEvents);
                    // return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.RELAXED,
                    //     kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SprichtGesichtsausdruckErschrocken):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtErschrocken,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SprichtGesichtsausdruckGenervt):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtGenervt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SprichtGesichtsausdruckUnzufrieden):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtUnzufrieden,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SprichtGesichtsausdruckAblehnend):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtAblehnend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SprichtGesichtsausdruckErstaunt):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtErstaunt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SprichtGesichtsausdruckFragend):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtFragend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SprichtGesichtsausdruckKritisch):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtKritisch,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SprichtGesichtsausdruckLaechelnGross):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtLaechelnGross,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SprichtGesichtsausdruckLachend):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtLachend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SprichtGesichtsausdruckLaecheln):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtLaecheln,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SprichtGesichtsausdruckNeutralEntspannt):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message,
                        CharacterExpression.SprichtNeutralEntspannt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SprichtGesichtsausdruckNeutral):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtNeutral,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter02SprichtGesichtsausdruckStolz):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SprichtStolz,
                        kiteNovelEventList.NovelEvents);
                }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_RELAXED):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.RELAXED,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_ASTONISHED):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.ASTONISHED,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_REFUSING):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.REFUSING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_SMILING):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SMILING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_FRIENDLY):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.FRIENDLY,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_LAUGHING):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.LAUGHING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_CRITICAL):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.CRITICAL,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_DECISION_NO):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.DECISION_NO,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_HAPPY):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.HAPPY,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_PROUD):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.PROUD,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_SCARED):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SCARED,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_QUESTIONING):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.QUESTIONING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_DEFEATED):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.DEFEATED,
                //         kiteNovelEventList.NovelEvents);
                // }

                case (NovelKeyWord.EintrittCharakter03):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautNeutralEntspannt,
                        kiteNovelEventList.NovelEvents);
                    // return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.RELAXED,
                    //     kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter03GesichtsausdruckErschrocken):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautErschrocken,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter03GesichtsausdruckGenervt):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautGenervt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter03GesichtsausdruckUnzufrieden):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautUnzufrieden,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter03GesichtsausdruckAblehnend):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautAblehnend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter03GesichtsausdruckErstaunt):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautErstaunt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter03GesichtsausdruckFragend):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautFragend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter03GesichtsausdruckKritisch):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautKritisch,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter03GesichtsausdruckLaechelnGross):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautLaechelnGross,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter03GesichtsausdruckLachend):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautLachend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter03GesichtsausdruckLaecheln):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautLaecheln,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter03GesichtsausdruckNeutralEntspannt):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautNeutralEntspannt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter03GesichtsausdruckNeutral):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautNeutral,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.EintrittCharakter03GesichtsausdruckStolz):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautStolz,
                        kiteNovelEventList.NovelEvents);
                }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_RELAXED):
                // {
                //     return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.RELAXED,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_ASTONISHED):
                // {
                //     return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.ASTONISHED,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_REFUSING):
                // {
                //     return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.REFUSING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_SMILING):
                // {
                //     return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SMILING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_FRIENDLY):
                // {
                //     return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.FRIENDLY,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_LAUGHING):
                // {
                //     return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.LAUGHING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_CRITICAL):
                // {
                //     return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.CRITICAL,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_DECISION_NO):
                // {
                //     return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.DECISION_NO,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_HAPPY):
                // {
                //     return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.HAPPY,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_PROUD):
                // {
                //     return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.PROUD,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_SCARED):
                // {
                //     return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCARED,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_QUESTIONING):
                // {
                //     return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.QUESTIONING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_DEFEATED):
                // {
                //     return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.DEFEATED,
                //         kiteNovelEventList.NovelEvents);
                // }

                case (NovelKeyWord.Charakter03Schaut):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautNeutralEntspannt,
                        kiteNovelEventList.NovelEvents);
                    // return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.RELAXED,
                    //     kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SchautGesichtsausdruckErschrocken):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautErschrocken,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SchautGesichtsausdruckGenervt):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautGenervt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SchautGesichtsausdruckUnzufrieden):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautUnzufrieden,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SchautGesichtsausdruckAblehnend):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautAblehnend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SchautGesichtsausdruckErstaunt):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautErstaunt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SchautGesichtsausdruckFragend):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautErstaunt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SchautGesichtsausdruckKritisch):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautKritisch,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SchautGesichtsausdruckLaechelnGross):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautLaechelnGross,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SchautGesichtsausdruckLachend):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautLachend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SchautGesichtsausdruckLaecheln):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautLaecheln,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SchautGesichtsausdruckNeutralEntspannt):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SchautNeutralEntspannt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SchautGesichtsausdruckNeutral):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautNeutral,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SchautGesichtsausdruckStolz):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SchautStolz,
                        kiteNovelEventList.NovelEvents);
                }

                case (NovelKeyWord.Charakter03Spricht):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message,
                        CharacterExpression.SprichtNeutralEntspannt,
                        kiteNovelEventList.NovelEvents);
                    // return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.RELAXED,
                    //     kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SprichtGesichtsausdruckErschrocken):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtErschrocken,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SprichtGesichtsausdruckGenervt):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtGenervt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SprichtGesichtsausdruckUnzufrieden):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtUnzufrieden,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SprichtGesichtsausdruckAblehnend):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtAblehnend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SprichtGesichtsausdruckErstaunt):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtErstaunt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SprichtGesichtsausdruckFragend):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtFragend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SprichtGesichtsausdruckKritisch):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtKritisch,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SprichtGesichtsausdruckLaechelnGross):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtLaechelnGross,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SprichtGesichtsausdruckLachend):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtLachend,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SprichtGesichtsausdruckLaecheln):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtLaecheln,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SprichtGesichtsausdruckNeutralEntspannt):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message,
                        CharacterExpression.SprichtNeutralEntspannt,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SprichtGesichtsausdruckNeutral):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtNeutral,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Charakter03SprichtGesichtsausdruckStolz):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SprichtStolz,
                        kiteNovelEventList.NovelEvents);
                }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_RELAXED):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.RELAXED,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_ASTONISHED):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.ASTONISHED,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_REFUSING):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.REFUSING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_SMILING):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SMILING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_FRIENDLY):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.FRIENDLY,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_LAUGHING):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.LAUGHING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_CRITICAL):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.CRITICAL,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_DECISION_NO):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.DECISION_NO,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_HAPPY):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.HAPPY,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_PROUD):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.PROUD,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_SCARED):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SCARED,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_QUESTIONING):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.QUESTIONING,
                //         kiteNovelEventList.NovelEvents);
                // }
                // case (NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_DEFEATED):
                // {
                //     return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.DEFEATED,
                //         kiteNovelEventList.NovelEvents);
                // }

                case (NovelKeyWord.SpielerCharakterSpricht):
                {
                    return HandleCharacterTalksEvent(passage, CharacterRole.PLAYER, message,
                        CharacterExpression.SchautNeutralEntspannt, kiteNovelEventList.NovelEvents);
                }

                case (NovelKeyWord.InfoNachrichtWirdAngezeigt):
                {
                    return HandleCharacterTalksEvent(passage, CharacterRole.INFO, message,
                        CharacterExpression.SchautNeutralEntspannt,
                        kiteNovelEventList.NovelEvents);
                }

                case (NovelKeyWord.SoundAbspielenWaterPouring):
                {
                    return HandlePlaySoundEvent(passage, KiteSound.WaterPouring, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.SoundAbspielenLeaveScene):
                {
                    return HandlePlaySoundEvent(passage, KiteSound.LeaveScene, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.SoundAbspielenTelephoneCall):
                {
                    return HandlePlaySoundEvent(passage, KiteSound.TelephoneCall, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.SoundAbspielenPaperSound):
                {
                    return HandlePlaySoundEvent(passage, KiteSound.PaperSound, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.SoundAbspielenManLaughing):
                {
                    return HandlePlaySoundEvent(passage, KiteSound.ManLaughing, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.AnimationAbspielenWaterPouring):
                {
                    return HandlePlayAnimationEvent(passage, KiteAnimation.WATER_POURING,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.Ende):
                {
                    return HandleEndNovelEvent(passage.Label, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.FreitextEingabe):
                {
                    return HandleFreeTextInputEvent(passage, message, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.GptPromptMitDefaultCompletionHandler):
                {
                    return HandleGptRequestEvent(passage, message, CompletionHandler.DefaultCompletionHandler,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.PersistentesSpeichern):
                {
                    return HandleSaveDataEvent(passage, message, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.VariableSetzen):
                {
                    return HandleSetVariableEvent(passage, message, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.VariableAusBoolschemAusdruckBestimmen):
                {
                    return HandleSetVariableFromBooleanExpressionEvent(passage, message,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.FeedbackHinzufuegen):
                {
                    return HandleAddFeedbackEvent(passage, message, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.FeedbackUnterBedingungHinzufuegen):
                {
                    return HandleAddFeedbackUnderConditionEvent(passage, message, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasFinanzierungszugang):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.ACCESS_TO_FUNDING,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasGenderPayGap):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.GENDER_PAY_GAP, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasUnterbewertungWeiblichGefuehrterUnternehmen):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.UNDERVALUATION_OF_WOMEN_LED_BUSINESSES,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasRiskAversionBias):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.RISK_AVERSION_BIAS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasBestaetigungsverzerrung):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.CONFIRMATION_BIAS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasTokenism):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.TOKENISM, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasBiasInDerWahrnehmungVonFuehrungsfaehigkeiten):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.IN_PERCEPTION_OF_LEADERSHIP_ABILITIES,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasRassistischeUndEthnischeBiases):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.RACIST_AND_ETHNIC_BIASES,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasSoziooekonomischeBiases):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.SOCIOECONOMIC_BIASES,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasAlterUndGenerationenBiases):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.AGE_AND_GENERATIONAL_BIASES,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasSexualitaetsbezogeneBiases):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.SEXUALITY_RELATED_BIASES,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasBiasesGegenueberFrauenMitBehinderungen):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.AGAINST_WOMEN_WITH_DISABILITIES,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasStereotypeGegenueberFrauenInNichtTraditionellenBranchen):
                {
                    return HandleBiasEvent(passage,
                        DiscriminationBias.STEREOTYPES_AGAINST_WOMEN_IN_NON_TRADITIONAL_INDUSTRIES,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasKulturelleUndReligioeseBiases):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.CULTURAL_AND_RELIGIOUS_BIASES,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasMaternalBias):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.MATERNAL_BIAS, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasBiasesGegenueberFrauenMitKindern):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.AGAINST_WOMEN_WITH_CHILDREN,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasErwartungshaltungBezueglichFamilienplanung):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.EXPECTATIONS_REGARDING_FAMILY_PLANNING,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasWorkLifeBalanceErwartungen):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.WORK_LIFE_BALANCE_EXPECTATIONS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasGeschlechtsspezifischeStereotypen):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.GENDER_SPECIFIC_STEREOTYPES,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasTightropeBias):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.TIGHTROPE_BIAS, kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasMikroaggressionen):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.MICROAGGRESSIONS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasLeistungsattributionsBias):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.PERFORMANCE_ATTRIBUTION_BIAS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasBiasInMedienUndWerbung):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.IN_MEDIA_AND_ADVERTISING,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasUnbewussteBiasInDerKommunikation):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.UNCONSCIOUS_BIAS_IN_COMMUNICATION,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasProveItAgainBias):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.PROVE_IT_AGAIN_BIAS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasHeteronormativitaetBias):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.HETERONORMATIVITAET_BIAS,
                        kiteNovelEventList.NovelEvents);
                }
                case (NovelKeyWord.RelevanterBiasBenevolenterSexismusBias):
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

            return NovelKeyWord.None;
        }
    }
}