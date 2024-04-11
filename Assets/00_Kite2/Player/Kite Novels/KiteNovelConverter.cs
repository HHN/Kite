using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

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
            KiteNovelEventDTO dto = ExtractAndConvertToInformatonList(passage.Passage, 
                kiteNovelMetaData.TalkingPartner01, kiteNovelMetaData.TalkingPartner02, kiteNovelMetaData.TalkingPartner03);
            VisualNovelEvent lastEventOfCurrentLoop = null;

            switch (dto?.EventType)
            {
                case VisualNovelEventType.SET_BACKGROUND_EVENT:
                    {
                        lastEventOfCurrentLoop = HandleLocationEvent(passage, dto, kiteNovelEventList.NovelEvents);
                        break;
                    }
                case VisualNovelEventType.CHARAKTER_JOIN_EVENT:
                    {
                        lastEventOfCurrentLoop = HandleCharacterComesEvent(passage, dto, kiteNovelEventList.NovelEvents);
                        break;
                    }
                case VisualNovelEventType.SHOW_MESSAGE_EVENT:
                    {
                        lastEventOfCurrentLoop = HandleCharacterTalksEvent(passage, dto, kiteNovelEventList.NovelEvents);
                        break;
                    }
                case VisualNovelEventType.PLAY_SOUND_EVENT:
                    {
                        lastEventOfCurrentLoop = HandlePlaySoundEvent(passage, dto, kiteNovelEventList.NovelEvents);
                        break;
                    }
                case VisualNovelEventType.PLAY_ANIMATION_EVENT:
                    {
                        lastEventOfCurrentLoop = HandlePlayAnimationEvent(passage, dto, kiteNovelEventList.NovelEvents);
                        break;
                    }
                case VisualNovelEventType.FREE_TEXT_INPUT_EVENT:
                    {
                        lastEventOfCurrentLoop = HandleFreeTextInputEvent(passage, dto, kiteNovelEventList.NovelEvents);
                        break;
                    }
                case VisualNovelEventType.GPT_PROMPT_EVENT:
                    {
                        lastEventOfCurrentLoop = HandleGptRequestEvent(passage, dto, kiteNovelEventList.NovelEvents);
                        break;
                    }
                case VisualNovelEventType.SAVE_PERSISTENT_EVENT:
                    {
                        lastEventOfCurrentLoop = HandleSaveDataEvent(passage, dto, kiteNovelEventList.NovelEvents);
                        break;
                    }
                case VisualNovelEventType.END_NOVEL_EVENT:
                    {
                        HandleEndNovelEvent(passage.Label, kiteNovelEventList.NovelEvents);
                        break;
                    }
                case VisualNovelEventType.MARK_BIAS_EVENT:
                    {
                        lastEventOfCurrentLoop = HandleBiasEvent(passage, dto, kiteNovelEventList.NovelEvents);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            HandleLoop(lastEventOfCurrentLoop, startEventLabel, kiteNovelEventList);
            HandleDialogueOptionEvent(passage, kiteNovelEventList.NovelEvents, lastEventOfCurrentLoop);
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

    public static VisualNovelEvent HandleLocationEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
    {
        string id = twee?.Label;
        string nextId = twee?.Links?[0]?.Target;
        Location location = dto.Ort;
        VisualNovelEvent novelEvent = KiteNovelEventFactory.GetSetBackgroundEvent(id, nextId, location);
        list.Add(novelEvent);
        return novelEvent;
    }

    public static VisualNovelEvent HandleCharacterTalksEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
    {
        string id = twee?.Label;
        string nextId = twee?.Links?[0]?.Target;
        Character chracter = dto.Character; 
        string dialogMessage = dto.DialogMessage;
        CharacterExpression expression =dto.EmotionOfCharacter;
        VisualNovelEvent novelEvent = KiteNovelEventFactory.GetCharacterTalksEvent(id, nextId, chracter, dialogMessage, expression);
        list.Add(novelEvent);
        return novelEvent;
    }

    public static VisualNovelEvent HandleCharacterComesEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
    {
        string id = twee?.Label;
        string nextId = twee?.Links?[0]?.Target;
        Character chracter = dto.Character;
        CharacterExpression expressionType = dto.EmotionOfCharacter;
        VisualNovelEvent novelEvent = KiteNovelEventFactory.GetCharacterJoinsEvent(id, nextId, chracter, expressionType);
        list.Add(novelEvent);
        return novelEvent;
    }

    public static VisualNovelEvent HandleBiasEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
    {
        string id = twee?.Label;
        string nextId = twee?.Links?[0]?.Target;
        DiscriminationBias RelevantBias = dto.RelevantBias;
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
        } else
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

    public static VisualNovelEvent HandlePlaySoundEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
    {
        string id = twee?.Label;
        string nextId = twee?.Links?[0]?.Target;
        KiteSound audioClipToPlay = dto.Sound;
        VisualNovelEvent novelEvent = KiteNovelEventFactory.GetPlaySoundEvent(id, nextId, audioClipToPlay);
        list.Add(novelEvent);
        return novelEvent;
    }

    public static VisualNovelEvent HandlePlayAnimationEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
    {
        string id = twee?.Label;
        string nextId = twee?.Links?[0]?.Target;
        KiteAnimation animationToPlay = dto.Animation;
        VisualNovelEvent novelEvent = KiteNovelEventFactory.GetPlayAnimationEvent(id, nextId, animationToPlay);
        list.Add(novelEvent);
        return novelEvent;
    }

    public static VisualNovelEvent HandleFreeTextInputEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
    {
        string id = twee?.Label;
        string nextId = twee?.Links?[0]?.Target;
        string question = dto.Question;
        string variablesName = dto.VariableName;
        VisualNovelEvent novelEvent = KiteNovelEventFactory.GetFreeTextInputEvent(id, nextId, question, variablesName);
        list.Add(novelEvent);
        return novelEvent;
    }

    public static VisualNovelEvent HandleGptRequestEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
    {
        string id = twee?.Label;
        string nextId = twee?.Links?[0]?.Target;
        string prompt = dto.Prompt;
        string variablesName = dto.VariableName;
        CompletionHandler completionHandlerId = dto.CompletionHandler;
        VisualNovelEvent novelEvent = KiteNovelEventFactory.GetGptEvent(id, nextId, prompt, variablesName, completionHandlerId);
        list.Add(novelEvent);
        return novelEvent;
    }

    public static VisualNovelEvent HandleSaveDataEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
    {
        string id = twee?.Label;
        string nextId = twee?.Links?[0]?.Target;
        string key = dto.Key;
        string value = dto.Value;
        VisualNovelEvent novelEvent = KiteNovelEventFactory.GetSavePersistentEvent(id, nextId, key, value);
        list.Add(novelEvent);
        return novelEvent;
    }

    public static KiteNovelEventDTO ExtractAndConvertToInformatonList(string input, string charakter01String, string charakter02String, string charakter03String)
    {
        Character character01 = CharacterTypeHelper.ValueOf(charakter01String);
        Character character02 = CharacterTypeHelper.ValueOf(charakter02String);
        Character character03 = CharacterTypeHelper.ValueOf(charakter03String); 

        KiteNovelEventDTO dto = ConvertListOfDataObjectsIntoKiteNovelEventDto(input, character01, character02, character03);

        if (dto == null)
        {
            return dto;
        }

        input = RemoveTitleFromPassage(input);
        input = RemoveTextInDoubleBrackets(input);
        input = RemoveTextInCurlyBraces(input);
        input = RemoveTextInParentheses(input);
        input = RemoveTextInDoubleAngleBrackets(input);
        input = RemoveTextInDoubleAngleBracketsOtherDirection(input);
        input = RemoveSquareBrackets(input);
        input = RemoveKeyWords(input);
        input = NormalizeSpaces(input);

        if (dto.EventType == VisualNovelEventType.SHOW_MESSAGE_EVENT)
        {
            dto.DialogMessage = input;
        }
        else if (dto.EventType == VisualNovelEventType.FREE_TEXT_INPUT_EVENT)
        {
            string[] parts = input.Split(new[] { ':' }, 2);

            if (parts.Length == 2)
            {
                string key = parts[0];
                string value = parts[1];

                dto.VariableName = key.Trim();
                dto.Question = value.Trim();
            }
        }
        else if (dto.EventType == VisualNovelEventType.GPT_PROMPT_EVENT)
        {
            string[] parts = input.Split(new[] { ':' }, 2);

            if (parts.Length == 2)
            {
                string key = parts[0];
                string value = parts[1];

                dto.VariableName = key.Trim();
                dto.Prompt = value.Trim();
            }
        }
        else if (dto.EventType == VisualNovelEventType.SAVE_PERSISTENT_EVENT)
        {
            string[] parts = input.Split(new[] { ':' }, 2);

            if (parts.Length == 2)
            {
                string key = parts[0];
                string value = parts[1];

                dto.Key = key.Trim();
                dto.Value = value.Trim();
            }
        }

        return dto;
    }

    private static string RemoveKeyWords(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }
        foreach (string keyWord in NovelKeyWordValue.ALL_KEY_WORDS)
        {
            input = input.Replace(keyWord, "");
        }
        return input;
    }

    public static string RemoveTitleFromPassage(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }
        var titlePattern = @"^\:\: [^\n]+";

        var result = Regex.Replace(input, titlePattern, "").Trim();

        return result;
    }

    public static string RemoveTextInDoubleBrackets(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }
        var pattern = @"\[\[(.*?)\]\]";

        var result = Regex.Replace(input, pattern, "");

        return result;
    }

    public static string RemoveTextInCurlyBraces(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }
        var pattern = @"\{(.*?)\}";

        var result = Regex.Replace(input, pattern, "");

        return result;
    }
    public static string RemoveTextInParentheses(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }

        var pattern = @"\([^\)]*\)";

        var result = Regex.Replace(input, pattern, "");

        return result;
    }

    public static string RemoveTextInDoubleAngleBrackets(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }

        var pattern = @"\<\<.*?\>\>";

        var result = Regex.Replace(input, pattern, "");

        return result;
    }

    public static string RemoveTextInDoubleAngleBracketsOtherDirection(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }

        var pattern = @"\>\>.*?\<\<";

        var result = Regex.Replace(input, pattern, "");

        return result;
    }

    public static string RemoveSquareBrackets(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }
        var result = input.Replace("[", "").Replace("]", "");
        return result;
    }

    public static string NormalizeSpaces(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }
        var pattern = @"\s+";

        var result = Regex.Replace(input, pattern, " ");

        return result;
    }

    private static KiteNovelEventDTO ConvertListOfDataObjectsIntoKiteNovelEventDto(string input, Character charakter01, Character charakter02, Character charakter03)
    {
        if (string.IsNullOrEmpty(input))
        {
            return null;
        }
        KiteNovelEventDTO kiteNovelEventDTO = new KiteNovelEventDTO();

        if (input.Contains(NovelKeyWordValue.SZENE_BUERO))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SET_BACKGROUND_EVENT,
                Ort = Location.OFFICE
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_01))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter01
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_RELAXED)) 
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.RELAXED
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_ASTONISHED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.ASTONISHED
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_REFUSING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.REFUSING
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_SMILING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.SMILING
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_FRIENDLY))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.FRIENDLY
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_LAUGHING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.LAUGHING
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_CRITICAL))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.CRITICAL
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_DECISION_NO))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.DECISION_NO
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_HAPPY))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.HAPPY
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_PROUD))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.PROUD
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_SCARED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.SCARED
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_QUESTIONING)) 
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.QUESTIONING
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_DEFEATED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.DEFEATED
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter01
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_RELAXED)) 
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.RELAXED
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_ASTONISHED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.ASTONISHED
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_REFUSING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.REFUSING
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_SMILING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.SMILING
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_FRIENDLY))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.FRIENDLY
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_LAUGHING)) 
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.LAUGHING
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_CRITICAL)) 
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.CRITICAL
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_DECISION_NO)) 
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.DECISION_NO
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_HAPPY))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.HAPPY
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_PROUD))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.PROUD
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_SCARED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.SCARED
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_QUESTIONING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.QUESTIONING
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_DEFEATED)) 
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter01,
                EmotionOfCharacter = CharacterExpression.DEFEATED
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_02))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter02
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_RELAXED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.RELAXED
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_ASTONISHED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.ASTONISHED
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_REFUSING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.REFUSING
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_SMILING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.SMILING
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_FRIENDLY))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.FRIENDLY
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_LAUGHING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.LAUGHING
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_CRITICAL))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.CRITICAL
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_DECISION_NO))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.DECISION_NO
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_HAPPY))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.HAPPY
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_PROUD))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.PROUD
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_SCARED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.SCARED
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_QUESTIONING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.QUESTIONING
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_DEFEATED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.DEFEATED
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter02
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_RELAXED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.RELAXED
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_ASTONISHED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.ASTONISHED
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_REFUSING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.REFUSING
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_SMILING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.SMILING
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_FRIENDLY))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.FRIENDLY
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_LAUGHING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.LAUGHING
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_CRITICAL))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.CRITICAL
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_DECISION_NO))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.DECISION_NO
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_HAPPY))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.HAPPY
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_PROUD))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.PROUD
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_SCARED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.SCARED
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_QUESTIONING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.QUESTIONING
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_DEFEATED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter02,
                EmotionOfCharacter = CharacterExpression.DEFEATED
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_03))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter03
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_RELAXED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.RELAXED
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_ASTONISHED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.ASTONISHED
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_REFUSING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.REFUSING
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_SMILING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.SMILING
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_FRIENDLY))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.FRIENDLY
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_LAUGHING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.LAUGHING
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_CRITICAL))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.CRITICAL
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_DECISION_NO))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.DECISION_NO
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_HAPPY))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.HAPPY
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_PROUD))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.PROUD
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_SCARED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.SCARED
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_QUESTIONING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.QUESTIONING
            };
        }
        if (input.Contains(NovelKeyWordValue.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_DEFEATED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.CHARAKTER_JOIN_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.DEFEATED
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter03
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_RELAXED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.RELAXED
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_ASTONISHED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.ASTONISHED
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_REFUSING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.REFUSING
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_SMILING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.SMILING
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_FRIENDLY))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.FRIENDLY
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_LAUGHING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.LAUGHING
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_CRITICAL))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.CRITICAL
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_DECISION_NO))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.DECISION_NO
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_HAPPY))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.HAPPY
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_PROUD))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.PROUD
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_SCARED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.SCARED
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_QUESTIONING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.QUESTIONING
            };
        }
        if (input.Contains(NovelKeyWordValue.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_DEFEATED))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = charakter03,
                EmotionOfCharacter = CharacterExpression.DEFEATED
            };
        }
        if (input.Contains(NovelKeyWordValue.SPIELER_CHARAKTER_SPRICHT)) 
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = Character.PLAYER
            };
        }
        if (input.Contains(NovelKeyWordValue.INFO_NACHRICHT_WIRD_ANGEZEIGT))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SHOW_MESSAGE_EVENT,
                Character = Character.INFO
            };
        }
        if (input.Contains(NovelKeyWordValue.SOUND_ABSPIELEN_WATER_POURING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.PLAY_SOUND_EVENT,
                Sound = KiteSound.WATER_POURING
        };
        }
        if (input.Contains(NovelKeyWordValue.SOUND_ABSPIELEN_LEAVE_SCENE))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.PLAY_SOUND_EVENT,
                Sound = KiteSound.LEAVE_SCENE
            };
        }
        if (input.Contains(NovelKeyWordValue.SOUND_ABSPIELEN_TELEPHONE_CALL))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.PLAY_SOUND_EVENT,
                Sound = KiteSound.TELEPHONE_CALL
            };
        }
        if (input.Contains(NovelKeyWordValue.SOUND_ABSPIELEN_PAPER_SOUND))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.PLAY_SOUND_EVENT,
                Sound = KiteSound.PAPER_SOUND
            };
        }
        if (input.Contains(NovelKeyWordValue.SOUND_ABSPIELEN_MAN_LAUGHING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.PLAY_SOUND_EVENT,
                Sound = KiteSound.MAN_LAUGHING
            };
        }
        if (input.Contains(NovelKeyWordValue.ANIMATION_ABSPIELEN_WATER_POURING))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.PLAY_ANIMATION_EVENT,
                Animation = KiteAnimation.WATER_POURING
            };
        }
        if (input.Contains(NovelKeyWordValue.ENDE))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.END_NOVEL_EVENT
            };
        }
        if (input.Contains(NovelKeyWordValue.FREITEXT_EINGABE))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.FREE_TEXT_INPUT_EVENT
            };
        }
        if (input.Contains(NovelKeyWordValue.GPT_PROMPT_MIT_DEFAULT_COMPLETION_HANDLER)) 
        {
                return new KiteNovelEventDTO()
                {
                    EventType = VisualNovelEventType.GPT_PROMPT_EVENT,
                    CompletionHandler = CompletionHandler.DEFAULT_COMPLETION_HANDLER
                };
            }
        if (input.Contains(NovelKeyWordValue.PERSISTENTES_SPEICHERN))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.SAVE_PERSISTENT_EVENT
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_FINANZIERUNGSZUGANG))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.ACCESS_TO_FUNDING
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_GENDER_PAY_GAP)) 
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.GENDER_PAY_GAP
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_UNTERBEWERTUNG_WEIBLICH_GEFUEHRTER_UNTERNEHMEN))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.UNDERVALUATION_OF_WOMEN_LED_BUSINESSES
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_RISK_AVERSION_BIAS))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.RISK_AVERSION_BIAS
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_BESTAETIGUNGSVERZERRUNG)) 
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.CONFIRMATION_BIAS
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_TOKENISM)) 
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.TOKENISM
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_BIAS_IN_DER_WAHRNEHMUNG_VON_FUEHRUNGSFAEHIGKEITEN))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.IN_PERCEPTION_OF_LEADERSHIP_ABILITIES
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_RASSISTISCHE_UND_ETHNISCHE_BIASES))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.RACIST_AND_ETHNIC_BIASES
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_SOZIOOEKONOMISCHE_BIASES))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.SOCIOECONOMIC_BIASES
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_ALTER_UND_GENERATIONEN_BIASES)) 
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.AGE_AND_GENERATIONAL_BIASES
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_SEXUALITAETSBEZOGENE_BIASES))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.SEXUALITY_RELATED_BIASES
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_BIASES_GEGENUEBER_FRAUEN_MIT_BEHINDERUNGEN)) {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.AGAINST_WOMEN_WITH_DISABILITIES
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_STEREOTYPE_GEGENUEBER_FRAUEN_IN_NICHT_TRADITIONELLEN_BRANCHEN)) {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.STEREOTYPES_AGAINST_WOMEN_IN_NON_TRADITIONAL_INDUSTRIES
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_KULTURELLE_UND_RELIGIOESE_BIASES)) {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.CULTURAL_AND_RELIGIOUS_BIASES
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_MATERNAL_BIAS))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.MATERNAL_BIAS
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_BIASES_GEGENUEBER_FRAUEN_MIT_KINDERN)) {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.AGAINST_WOMEN_WITH_CHILDREN
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_ERWARTUNGSHALTUNG_BEZUEGLICH_FAMILIENPLANUNG))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.EXPECTATIONS_REGARDING_FAMILY_PLANNING
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_WORK_LIFE_BALANCE_ERWARTUNGEN)) {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.WORK_LIFE_BALANCE_EXPECTATIONS
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_GESCHLECHTSSPEZIFISCHE_STEREOTYPEN))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.GENDER_SPECIFIC_STEREOTYPES
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_TIGHTROPE_BIAS)) {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.TIGHTROPE_BIAS
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_MIKROAGGRESSIONEN)) {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.MICROAGGRESSIONS
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_LEISTUNGSATTRIBUTIONS_BIAS))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.PERFORMANCE_ATTRIBUTION_BIAS
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_BIAS_IN_MEDIEN_UND_WERBUNG))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.IN_MEDIA_AND_ADVERTISING
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_UNBEWUSSTE_BIAS_IN_DER_KOMMUNIKATION))
        {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.UNCONSCIOUS_BIAS_IN_COMMUNICATION
            };
        }
        if (input.Contains(NovelKeyWordValue.RELEVANTER_BIAS_PROVE_IT_AGAIN_BIAS)) {
            return new KiteNovelEventDTO()
            {
                EventType = VisualNovelEventType.MARK_BIAS_EVENT,
                RelevantBias = DiscriminationBias.PROVE_IT_AGAIN_BIAS
            };
        }

        if (kiteNovelEventDTO.EventType == VisualNovelEventType.NONE)
        {
            return null;
        }

        return kiteNovelEventDTO;
    }
}