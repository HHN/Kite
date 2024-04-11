using System.Collections.Generic;
using UnityEngine;

public class KiteNovelConverter
{
    public static int counterForNamingPurpose = 1;

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

    public static KiteNovelEventList ConvertTextDocumentIntoEventList(string tweeFile, KiteNovelMetaData kiteNovelMetaData)
    {
        KiteNovelEventList kiteNovelEventList = new KiteNovelEventList();
        string startEventLabel = TweeProcessor.GetStartLabelFromTweeFile(tweeFile);
        InitializeKiteNovelEventList(kiteNovelMetaData, kiteNovelEventList, startEventLabel);
        List<TweePassage> passages = TweeProcessor.ProcessTweeFile(tweeFile);

        foreach (TweePassage passage in passages)
        {
            VisualNovelEvent createdEvent = CreateVisualNovelEvent(passage, kiteNovelMetaData, kiteNovelEventList);
            HandleLoop(createdEvent, startEventLabel, kiteNovelEventList);
            HandleDialogueOptionEvent(passage, kiteNovelEventList.NovelEvents, createdEvent);
        }
        return kiteNovelEventList;
    }

    public static void HandleLoop(VisualNovelEvent lastEvent, string labelOfStartEvent, KiteNovelEventList kiteNovelEventList)
    {
        if (lastEvent == null || string.IsNullOrEmpty(lastEvent.nextId)
            || string.IsNullOrEmpty(labelOfStartEvent) || kiteNovelEventList == null)
        {
            return;
        }
        if (lastEvent.nextId == labelOfStartEvent)
        {
            string newLabel = "RandomEndNovelString" + counterForNamingPurpose;
            counterForNamingPurpose++;
            HandleEndNovelEvent(newLabel, kiteNovelEventList.NovelEvents);
            lastEvent.nextId = newLabel;
        }
    }

    public static void InitializeKiteNovelEventList(KiteNovelMetaData kiteNovelMetaData,
        KiteNovelEventList kiteNovelEventList, string startLabel)
    {
        if (!kiteNovelMetaData.IsWithStartValues)
        {
            return;
        }
        string connectionLabel = "initalCharakterJoinsEvent001";
        string id = "initalLocationEvent001";
        string nextId = connectionLabel;
        Location location = LocationHelper.ValueOf(kiteNovelMetaData.StartLocation);

        if (location == Location.NONE)
        {
            Debug.LogWarning("While loading " + kiteNovelMetaData?.TitleOfNovel + ": Initial Location not found!");
        }
        VisualNovelEvent initalLocationEvent = KiteNovelEventFactory.GetSetBackgroundEvent(id, nextId, location);
        kiteNovelEventList.NovelEvents.Add(initalLocationEvent);

        id = connectionLabel;
        nextId = startLabel;
        Character chracter = CharacterTypeHelper.ValueOf(kiteNovelMetaData.TalkingPartner01);
        if (chracter == Character.NONE)
        {
            Debug.LogWarning("While loading " + kiteNovelMetaData?.TitleOfNovel + ": Initial Character not found!");
        }
        CharacterExpression expression = CharacterExpressionHelper.ValueOf(kiteNovelMetaData.StartTalkingPartnerEmotion);
        if (expression == CharacterExpression.NONE)
        {
            Debug.LogWarning("While loading " + kiteNovelMetaData?.TitleOfNovel + ": Initial Character-Expression not found!");
        }

        VisualNovelEvent initalCharacterJoinsEvent = KiteNovelEventFactory.GetCharacterJoinsEvent(id, nextId, chracter, expression);
        kiteNovelEventList.NovelEvents.Add(initalCharacterJoinsEvent);


    }

    public static VisualNovelEvent HandleLocationEvent(TweePassage twee, Location location, List<VisualNovelEvent> list)
    {
        string id = twee?.Label;
        string nextId = twee?.Links?[0]?.Target;
        VisualNovelEvent novelEvent = KiteNovelEventFactory.GetSetBackgroundEvent(id, nextId, location);
        list.Add(novelEvent);
        return novelEvent;
    }

    public static VisualNovelEvent HandleCharacterTalksEvent(TweePassage twee, Character chracter, string dialogMessage,
        CharacterExpression expression, List<VisualNovelEvent> list)
    {
        string id = twee?.Label;
        string nextId = twee?.Links?[0]?.Target;
        VisualNovelEvent novelEvent = KiteNovelEventFactory.GetCharacterTalksEvent(id, nextId, chracter, dialogMessage, expression);
        list.Add(novelEvent);
        return novelEvent;
    }

    public static VisualNovelEvent HandleCharacterComesEvent(TweePassage twee, Character chracter,
        CharacterExpression expressionType, List<VisualNovelEvent> list)
    {
        string id = twee?.Label;
        string nextId = twee?.Links?[0]?.Target;
        VisualNovelEvent novelEvent = KiteNovelEventFactory.GetCharacterJoinsEvent(id, nextId, chracter, expressionType);
        list.Add(novelEvent);
        return novelEvent;
    }

    public static VisualNovelEvent HandleBiasEvent(TweePassage twee, DiscriminationBias RelevantBias, List<VisualNovelEvent> list)
    {
        string id = twee?.Label;
        string nextId = twee?.Links?[0]?.Target;
        VisualNovelEvent novelEvent = KiteNovelEventFactory.GetBiasEvent(id, nextId, RelevantBias);
        list.Add(novelEvent);
        return novelEvent;
    }

    public static void HandleEndNovelEvent(string label, List<VisualNovelEvent> list)
    {
        string label01 = label;
        string label02 = label01 + "RandomString0012003";
        string label03 = label02 + "RandomRandom";
        KiteSound leaveSceneSound = KiteSound.LEAVE_SCENE;

        VisualNovelEvent soundEvent = KiteNovelEventFactory.GetPlaySoundEvent(label01, label02, leaveSceneSound);
        VisualNovelEvent exitEvent = KiteNovelEventFactory.GetCharacterExitsEvent(label02, label03);
        VisualNovelEvent endEvent = KiteNovelEventFactory.GetEndNovelEvent(label03);

        list.Add(soundEvent);
        list.Add(exitEvent);
        list.Add(endEvent);
    }

    public static void HandleDialogueOptionEvent(TweePassage twee, List<VisualNovelEvent> list, VisualNovelEvent lastEvent)
    {
        if (twee == null || twee.Links == null || twee.Links.Count <= 1)
        {
            return;
        }

        string label = "OptionsLabel" + counterForNamingPurpose;
        counterForNamingPurpose++;

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

    public static VisualNovelEvent HandlePlaySoundEvent(TweePassage twee, KiteSound audioClipToPlay, List<VisualNovelEvent> list)
    {
        string id = twee?.Label;
        string nextId = twee?.Links?[0]?.Target;
        VisualNovelEvent novelEvent = KiteNovelEventFactory.GetPlaySoundEvent(id, nextId, audioClipToPlay);
        list.Add(novelEvent);
        return novelEvent;
    }

    public static VisualNovelEvent HandlePlayAnimationEvent(TweePassage twee, KiteAnimation animationToPlay, List<VisualNovelEvent> list)
    {
        string id = twee?.Label;
        string nextId = twee?.Links?[0]?.Target;
        VisualNovelEvent novelEvent = KiteNovelEventFactory.GetPlayAnimationEvent(id, nextId, animationToPlay);
        list.Add(novelEvent);
        return novelEvent;
    }

    public static VisualNovelEvent HandleFreeTextInputEvent(TweePassage twee, string message, List<VisualNovelEvent> list)
    {
        string[] parts = message.Split(new[] { ':' }, 2);

        if (parts.Length != 2)
        {
            Debug.LogWarning("While creating Visual Novels: Free Text Input Event could not be created.");
            return null;
        }
        string id = twee?.Label;
        string nextId = twee?.Links?[0]?.Target;
        string question = parts[1]?.Trim();
        string variablesName = parts[0]?.Trim();
        VisualNovelEvent novelEvent = KiteNovelEventFactory.GetFreeTextInputEvent(id, nextId, question, variablesName);
        list.Add(novelEvent);
        return novelEvent;
    }

    public static VisualNovelEvent HandleGptRequestEvent(TweePassage twee, string message,
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
        string prompt = parts[1]?.Trim();
        string variablesName = parts[0]?.Trim();
        VisualNovelEvent novelEvent = KiteNovelEventFactory.GetGptEvent(id, nextId, prompt, variablesName, completionHandlerId);
        list.Add(novelEvent);
        return novelEvent;
    }

    public static VisualNovelEvent HandleSaveDataEvent(TweePassage twee, string message, List<VisualNovelEvent> list)
    {
        string[] parts = message.Split(new[] { ':' }, 2);

        if (parts.Length != 2)
        {
            Debug.LogWarning("While creating Visual Novels: Save Persistent Event could not be created.");
            return null;
        }
        string id = twee?.Label;
        string nextId = twee?.Links?[0]?.Target;
        string key = parts[0]?.Trim();
        string value = parts[1]?.Trim();
        VisualNovelEvent novelEvent = KiteNovelEventFactory.GetSavePersistentEvent(id, nextId, key, value);
        list.Add(novelEvent);
        return novelEvent;
    }

    public static VisualNovelEvent CreateVisualNovelEvent(TweePassage passage, KiteNovelMetaData kiteNovelMetaData, KiteNovelEventList kiteNovelEventList)
    {
        string message = TweeProcessor.ExtractMessageOutOfTweePassage(passage.Passage);
        VisualNovelEvent visualNovelEvent = ConvertListOfDataObjectsIntoKiteNovelEvent(passage, message, kiteNovelMetaData, kiteNovelEventList);
        return visualNovelEvent;
    }

    private static VisualNovelEvent ConvertListOfDataObjectsIntoKiteNovelEvent(TweePassage passage, string message,
        KiteNovelMetaData kiteNovelMetaData, KiteNovelEventList kiteNovelEventList)
    {
        if (string.IsNullOrEmpty(passage?.Passage))
        {
            return null;
        }
        Character charakter01 = CharacterTypeHelper.ValueOf(kiteNovelMetaData.TalkingPartner01);
        Character charakter02 = CharacterTypeHelper.ValueOf(kiteNovelMetaData.TalkingPartner02);
        Character charakter03 = CharacterTypeHelper.ValueOf(kiteNovelMetaData.TalkingPartner03);

        string recognizedKeyWord = FindFirstKeyWordInText(passage?.Passage);

        switch (recognizedKeyWord)
        {
            case (NovelKeyWordValue.SZENE_BUERO):
                {
                    return HandleLocationEvent(passage, Location.OFFICE, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_01):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.RELAXED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_RELAXED):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.RELAXED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_ASTONISHED):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.ASTONISHED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_REFUSING):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.REFUSING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_SMILING):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SMILING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_FRIENDLY):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.FRIENDLY, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_LAUGHING):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.LAUGHING, kiteNovelEventList.NovelEvents);

                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_CRITICAL):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.CRITICAL, kiteNovelEventList.NovelEvents);

                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_DECISION_NO):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.DECISION_NO, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_HAPPY):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.HAPPY, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_PROUD):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.PROUD, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_SCARED):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.SCARED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_QUESTIONING):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.QUESTIONING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_DEFEATED):
                {
                    return HandleCharacterComesEvent(passage, charakter01, CharacterExpression.DEFEATED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.RELAXED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_RELAXED):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.RELAXED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_ASTONISHED):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.ASTONISHED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_REFUSING):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.REFUSING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_SMILING):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SMILING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_FRIENDLY):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.FRIENDLY, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_LAUGHING):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.LAUGHING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_CRITICAL):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.CRITICAL, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_DECISION_NO):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.DECISION_NO, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_HAPPY):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.HAPPY, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_PROUD):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.PROUD, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_SCARED):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.SCARED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_QUESTIONING):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.QUESTIONING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_DEFEATED):
                {
                    return HandleCharacterTalksEvent(passage, charakter01, message, CharacterExpression.DEFEATED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_02):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.RELAXED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_RELAXED):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.RELAXED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_ASTONISHED):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.ASTONISHED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_REFUSING):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.REFUSING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_SMILING):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SMILING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_FRIENDLY):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.FRIENDLY, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_LAUGHING):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.LAUGHING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_CRITICAL):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.CRITICAL, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_DECISION_NO):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.DECISION_NO, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_HAPPY):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.HAPPY, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_PROUD):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.PROUD, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_SCARED):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.SCARED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_QUESTIONING):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.QUESTIONING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_DEFEATED):
                {
                    return HandleCharacterComesEvent(passage, charakter02, CharacterExpression.DEFEATED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.RELAXED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_RELAXED):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.RELAXED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_ASTONISHED):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.ASTONISHED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_REFUSING):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.REFUSING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_SMILING):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SMILING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_FRIENDLY):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.FRIENDLY, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_LAUGHING):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.LAUGHING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_CRITICAL):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.CRITICAL, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_DECISION_NO):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.DECISION_NO, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_HAPPY):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.HAPPY, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_PROUD):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.PROUD, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_SCARED):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.SCARED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_QUESTIONING):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.QUESTIONING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_DEFEATED):
                {
                    return HandleCharacterTalksEvent(passage, charakter02, message, CharacterExpression.DEFEATED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_03):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.RELAXED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_RELAXED):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.RELAXED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_ASTONISHED):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.ASTONISHED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_REFUSING):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.REFUSING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_SMILING):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SMILING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_FRIENDLY):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.FRIENDLY, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_LAUGHING):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.LAUGHING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_CRITICAL):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.CRITICAL, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_DECISION_NO):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.DECISION_NO, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_HAPPY):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.HAPPY, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_PROUD):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.PROUD, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_SCARED):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.SCARED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_QUESTIONING):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.QUESTIONING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_DEFEATED):
                {
                    return HandleCharacterComesEvent(passage, charakter03, CharacterExpression.DEFEATED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.RELAXED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_RELAXED):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.RELAXED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_ASTONISHED):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.ASTONISHED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_REFUSING):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.REFUSING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_SMILING):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SMILING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_FRIENDLY):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.FRIENDLY, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_LAUGHING):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.LAUGHING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_CRITICAL):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.CRITICAL, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_DECISION_NO):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.DECISION_NO, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_HAPPY):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.HAPPY, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_PROUD):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.PROUD, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_SCARED):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.SCARED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_QUESTIONING):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.QUESTIONING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_DEFEATED):
                {
                    return HandleCharacterTalksEvent(passage, charakter03, message, CharacterExpression.DEFEATED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.SPIELER_CHARAKTER_SPRICHT):
                {
                    return HandleCharacterTalksEvent(passage, Character.PLAYER, message, CharacterExpression.RELAXED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.INFO_NACHRICHT_WIRD_ANGEZEIGT):
                {
                    return HandleCharacterTalksEvent(passage, Character.INFO, message, CharacterExpression.RELAXED, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.SOUND_ABSPIELEN_WATER_POURING):
                {
                    return HandlePlaySoundEvent(passage, KiteSound.WATER_POURING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.SOUND_ABSPIELEN_LEAVE_SCENE):
                {
                    return HandlePlaySoundEvent(passage, KiteSound.LEAVE_SCENE, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.SOUND_ABSPIELEN_TELEPHONE_CALL):
                {
                    return HandlePlaySoundEvent(passage, KiteSound.TELEPHONE_CALL, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.SOUND_ABSPIELEN_PAPER_SOUND):
                {
                    return HandlePlaySoundEvent(passage, KiteSound.PAPER_SOUND, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.SOUND_ABSPIELEN_MAN_LAUGHING):
                {
                    return HandlePlaySoundEvent(passage, KiteSound.MAN_LAUGHING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.ANIMATION_ABSPIELEN_WATER_POURING):
                {
                    HandlePlayAnimationEvent(passage, KiteAnimation.WATER_POURING, kiteNovelEventList.NovelEvents);
                    return null;
                }
            case (NovelKeyWordValue.ENDE):
                {
                    HandleEndNovelEvent(passage.Label, kiteNovelEventList.NovelEvents);
                    return null;
                }
            case (NovelKeyWordValue.FREITEXT_EINGABE):
                {
                    return HandleFreeTextInputEvent(passage, message, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.GPT_PROMPT_MIT_DEFAULT_COMPLETION_HANDLER):
                {
                    return HandleGptRequestEvent(passage, message, CompletionHandler.DEFAULT_COMPLETION_HANDLER, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.PERSISTENTES_SPEICHERN):
                {
                    return HandleSaveDataEvent(passage, message, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_FINANZIERUNGSZUGANG):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.ACCESS_TO_FUNDING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_GENDER_PAY_GAP):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.GENDER_PAY_GAP, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_UNTERBEWERTUNG_WEIBLICH_GEFUEHRTER_UNTERNEHMEN):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.UNDERVALUATION_OF_WOMEN_LED_BUSINESSES, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_RISK_AVERSION_BIAS):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.RISK_AVERSION_BIAS, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_BESTAETIGUNGSVERZERRUNG):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.CONFIRMATION_BIAS, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_TOKENISM):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.TOKENISM, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_BIAS_IN_DER_WAHRNEHMUNG_VON_FUEHRUNGSFAEHIGKEITEN):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.IN_PERCEPTION_OF_LEADERSHIP_ABILITIES, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_RASSISTISCHE_UND_ETHNISCHE_BIASES):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.RACIST_AND_ETHNIC_BIASES, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_SOZIOOEKONOMISCHE_BIASES):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.SOCIOECONOMIC_BIASES, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_ALTER_UND_GENERATIONEN_BIASES):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.AGE_AND_GENERATIONAL_BIASES, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_SEXUALITAETSBEZOGENE_BIASES):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.SEXUALITY_RELATED_BIASES, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_BIASES_GEGENUEBER_FRAUEN_MIT_BEHINDERUNGEN):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.AGAINST_WOMEN_WITH_DISABILITIES, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_STEREOTYPE_GEGENUEBER_FRAUEN_IN_NICHT_TRADITIONELLEN_BRANCHEN):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.STEREOTYPES_AGAINST_WOMEN_IN_NON_TRADITIONAL_INDUSTRIES, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_KULTURELLE_UND_RELIGIOESE_BIASES):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.CULTURAL_AND_RELIGIOUS_BIASES, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_MATERNAL_BIAS):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.MATERNAL_BIAS, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_BIASES_GEGENUEBER_FRAUEN_MIT_KINDERN):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.AGAINST_WOMEN_WITH_CHILDREN, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_ERWARTUNGSHALTUNG_BEZUEGLICH_FAMILIENPLANUNG):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.EXPECTATIONS_REGARDING_FAMILY_PLANNING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_WORK_LIFE_BALANCE_ERWARTUNGEN):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.WORK_LIFE_BALANCE_EXPECTATIONS, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_GESCHLECHTSSPEZIFISCHE_STEREOTYPEN):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.GENDER_SPECIFIC_STEREOTYPES, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_TIGHTROPE_BIAS):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.TIGHTROPE_BIAS, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_MIKROAGGRESSIONEN):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.MICROAGGRESSIONS, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_LEISTUNGSATTRIBUTIONS_BIAS):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.PERFORMANCE_ATTRIBUTION_BIAS, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_BIAS_IN_MEDIEN_UND_WERBUNG):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.IN_MEDIA_AND_ADVERTISING, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_UNBEWUSSTE_BIAS_IN_DER_KOMMUNIKATION):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.UNCONSCIOUS_BIAS_IN_COMMUNICATION, kiteNovelEventList.NovelEvents);
                }
            case (NovelKeyWordValue.RELEVANTER_BIAS_PROVE_IT_AGAIN_BIAS):
                {
                    return HandleBiasEvent(passage, DiscriminationBias.PROVE_IT_AGAIN_BIAS, kiteNovelEventList.NovelEvents);
                }
        }
        return null;
    }

    public static string FindFirstKeyWordInText(string text)
    {
        foreach (string keyWord in NovelKeyWordValue.ALL_KEY_WORDS)
        {
            if (text.Contains(keyWord))
            {
                return keyWord;
            }
        }
        return "";
    }
}