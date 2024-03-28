using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class KiteNovelConverter
{
    public static List<KiteNovelFolder> ConvertNovelsToFiles(List<VisualNovel> novels)
    {
        List<KiteNovelFolder> folders = new List<KiteNovelFolder>();

        foreach (VisualNovel novel in novels)
        {
            KiteNovelFolder folder = new KiteNovelFolder();
            KiteNovelMetaData kiteNovelMetaData = new KiteNovelMetaData();
            KiteNovelEventList kiteNovelEventList = new KiteNovelEventList();

            kiteNovelMetaData.idNumberOfNovel = novel.id;
            kiteNovelMetaData.titleOfNovel = novel.title;
            kiteNovelMetaData.descriptionOfNovel = novel.description;
            kiteNovelMetaData.idNumberOfRepresentationImage = novel.image;
            kiteNovelMetaData.nameOfMainCharacter = novel.nameOfMainCharacter;
            kiteNovelMetaData.contextForPrompt = novel.context;
            kiteNovelMetaData.isKite2Novel = novel.isKite2Novel;

            kiteNovelEventList.novelEvents = novel.novelEvents;

            folder.novelMetaData = kiteNovelMetaData;
            folder.novelEventList = kiteNovelEventList;
            folders.Add(folder);
        }

        return folders;
    }

    public static List<VisualNovel> ConvertFilesToNovels(List<KiteNovelFolder> folders)
    {
        List<VisualNovel> novels = new List<VisualNovel>();

        foreach (KiteNovelFolder folder in folders)
        {
            VisualNovel novel = new VisualNovel();
            KiteNovelMetaData kiteNovelMetaData = folder.novelMetaData;
            KiteNovelEventList kiteNovelEventList = folder.novelEventList;

            novel.id = kiteNovelMetaData.idNumberOfNovel;
            novel.title = kiteNovelMetaData.titleOfNovel;
            novel.description = kiteNovelMetaData.descriptionOfNovel;
            novel.image = kiteNovelMetaData.idNumberOfRepresentationImage;
            novel.nameOfMainCharacter = kiteNovelMetaData.nameOfMainCharacter;
            novel.context = kiteNovelMetaData.contextForPrompt;
            novel.isKite2Novel = kiteNovelMetaData.isKite2Novel;

            novel.novelEvents = kiteNovelEventList.novelEvents;

            novels.Add(novel);
        }

        return novels;
    }

    public static KiteNovelEventList ConvertTextDocumentIntoEventList(string tweeFile)
    {
        KiteNovelEventList kiteNovelEventList = new KiteNovelEventList();
        kiteNovelEventList.novelEvents = new List<VisualNovelEvent>();

        List<TweePassage> passages = TweeProcessor.ProcessTweeFile(tweeFile);

        foreach (TweePassage passage in passages)
        {
            KiteNovelEventDTO dto = ExtractAndConvertToJson(passage.passage);

            if ((dto == null) || (dto.event_art == null))
            {
                continue;
            }
            if (dto.event_art == null)
            {
                continue;
            }
            if (dto.event_art.Contains("ort", StringComparison.OrdinalIgnoreCase))
            {
                HandleLocationEvent(passage, dto, kiteNovelEventList.novelEvents);
                continue;
            }
            if (dto.event_art.Contains("kom", StringComparison.OrdinalIgnoreCase))
            {
                HandleCharacterComesEvent(passage, dto, kiteNovelEventList.novelEvents);
                continue;
            }
            if (dto.event_art.Contains("spr", StringComparison.OrdinalIgnoreCase))
            {
                HandleCharacterTalksEvent(passage, dto, kiteNovelEventList.novelEvents);
                continue;
            }
            if (dto.event_art.Contains("sou", StringComparison.OrdinalIgnoreCase))
            {
                HandlePlaySoundEvent(passage, dto, kiteNovelEventList.novelEvents);
                continue;
            }
            if (dto.event_art.Contains("ani", StringComparison.OrdinalIgnoreCase))
            {
                HandlePlayAnimationEvent(passage, dto, kiteNovelEventList.novelEvents);
                continue;
            }
            if (dto.event_art.Contains("fre", StringComparison.OrdinalIgnoreCase))
            {
                HandleFreeTextInputEvent(passage, dto, kiteNovelEventList.novelEvents);
                continue;
            }
            if (dto.event_art.Contains("gpt", StringComparison.OrdinalIgnoreCase))
            {
                HandleGptRequestEvent(passage, dto, kiteNovelEventList.novelEvents);
                continue;
            }
            if (dto.event_art.Contains("opt", StringComparison.OrdinalIgnoreCase))
            {
                HandleDialogueOptionEvent(passage, dto, kiteNovelEventList.novelEvents);
                continue;
            }
            if (dto.event_art.Contains("sav", StringComparison.OrdinalIgnoreCase))
            {
                HandleSaveDataEvent(passage, dto, kiteNovelEventList.novelEvents);
                continue;
            }
            if (dto.event_art.Contains("end", StringComparison.OrdinalIgnoreCase))
            {
                HandleEndNovelEvent(passage, dto, kiteNovelEventList.novelEvents);
                continue;
            }
        }

        return kiteNovelEventList;
    }

    public static void HandleLocationEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
    {
        VisualNovelEvent novelEvent = new VisualNovelEvent();
        novelEvent.id = twee.label;
        novelEvent.eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SET_BACKGROUND_EVENT);

        if (twee.links != null)
        {
            novelEvent.nextId = twee.links[0].target;
        }

        novelEvent.backgroundSpriteId = GetLocationIdOutOfString(dto.ort);
        list.Add(novelEvent);
    }

    public static void HandleCharacterTalksEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
    {
        VisualNovelEvent novelEvent = new VisualNovelEvent();
        novelEvent.id = twee.label;
        novelEvent.eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT);

        if (twee.links != null)
        {
            novelEvent.nextId = twee.links[0].target;
        }

        novelEvent.name = GetNameOutOfString(dto.name_des_charakters);
        novelEvent.text = dto.dialog_nachricht;
        novelEvent.expressionType = ConvertStringIntoExpressionType(dto.emotion_des_charakters);
        novelEvent.waitForUserConfirmation = true;
        list.Add(novelEvent);
    }

    public static void HandleCharacterComesEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
    {
        VisualNovelEvent novelEvent = new VisualNovelEvent();
        novelEvent.id = twee.label;
        novelEvent.eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_JOIN_EVENT);

        if (twee.links != null)
        {
            novelEvent.nextId = twee.links[0].target;
        }

        novelEvent.name = GetNameOutOfString(dto.name_des_charakters);
        novelEvent.expressionType = ConvertStringIntoExpressionType(dto.emotion_des_charakters);
        list.Add(novelEvent);
    }

    public static void HandleEndNovelEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
    {
        string label01 = twee.label;
        string label02 = label01 + "RandomString0012003";
        string label03 = label02 + "RandomRandom";

        VisualNovelEvent soundEvent = new VisualNovelEvent()
        {
            id = label01,
            nextId = label02,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT),
            waitForUserConfirmation = false,
            audioClipToPlay = SoundEnumHelper.ToInt(SoundsEnum.LEAVE_SCENE)
        };

        VisualNovelEvent exitEvent = new VisualNovelEvent()
        {
            id = label02,
            nextId = label03,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_EXIT_EVENT),
            waitForUserConfirmation = false,
        };

        VisualNovelEvent endEvent = new VisualNovelEvent()
        {
            id = label03,
            eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.END_NOVEL_EVENT),
            waitForUserConfirmation = false
        };

        list.Add(soundEvent);
        list.Add(exitEvent);
        list.Add(endEvent);
    }

    public static void HandleDialogueOptionEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
    {
        string label = twee.label;

        foreach (TweeLink link in twee.links)
        {
            VisualNovelEvent visualNovelEvent = new VisualNovelEvent();

            visualNovelEvent.eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT);
            visualNovelEvent.id = label;
            label = label + "RandomString001";
            visualNovelEvent.nextId = label;
            visualNovelEvent.text = link.text;
            visualNovelEvent.onChoice = link.target;
            visualNovelEvent.show = false;
            list.Add(visualNovelEvent);
        }

        VisualNovelEvent showChoicesEvent = new VisualNovelEvent();
        showChoicesEvent.eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT);
        showChoicesEvent.id = label;
        showChoicesEvent.waitForUserConfirmation = true;
        list.Add(showChoicesEvent);
    }

    public static void HandlePlaySoundEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
    {
        VisualNovelEvent novelEvent = new VisualNovelEvent();
        novelEvent.id = twee.label;
        novelEvent.eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_SOUND_EVENT);
        novelEvent.audioClipToPlay = GetSoundTypeOutOfString(dto.audio_die_abgespielt_werden_soll);

        if (twee.links != null)
        {
            novelEvent.nextId = twee.links[0].target;
        }
        list.Add(novelEvent);
    }

    public static void HandlePlayAnimationEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
    {
        VisualNovelEvent novelEvent = new VisualNovelEvent();
        novelEvent.id = twee.label;
        novelEvent.eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.PLAY_ANIMATION_EVENT);

        if (twee.links != null)
        {
            novelEvent.nextId = twee.links[0].target;
        }

        novelEvent.animationToPlay = GetAnimationTypeOutOfString(dto.animation_die_abgespielt_werden_soll);
        list.Add(novelEvent);
    }

    public static void HandleFreeTextInputEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
    {
        VisualNovelEvent novelEvent = new VisualNovelEvent();
        novelEvent.id = twee.label;
        novelEvent.eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.FREE_TEXT_INPUT_EVENT);

        if (twee.links != null)
        {
            novelEvent.nextId = twee.links[0].target;
        }

        novelEvent.questionForFreeTextInput = dto.frage_fuer_freitext;
        novelEvent.variablesName = dto.variablen_name_fuer_die_antwort;
        novelEvent.waitForUserConfirmation = true;
        list.Add(novelEvent);
    }

    public static void HandleGptRequestEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
    {
        VisualNovelEvent novelEvent = new VisualNovelEvent();
        novelEvent.id = twee.label;
        novelEvent.eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.GPT_PROMPT_EVENT);

        if (twee.links != null)
        {
            novelEvent.nextId = twee.links[0].target;
        }

        novelEvent.gptPrompt = dto.prompt_fuer_gpt;
        novelEvent.variablesNameForGptPromp = dto.variablen_name_fuer_die_antwort;
        novelEvent.gptCompletionHandlerId = GetCompletionHandlerIdOutOfString(dto.id_nummer_des_completion_handlers);
        novelEvent.waitForUserConfirmation = true;
        list.Add(novelEvent);
    }

    public static void HandleSaveDataEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
    {
        VisualNovelEvent novelEvent = new VisualNovelEvent();
        novelEvent.id = twee.label;
        novelEvent.eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.METHODE_CALL_EVENT);

        if (twee.links != null)
        {
            novelEvent.nextId = twee.links[0].target;
        }

        novelEvent.methodNameToCall = "WriteUserInputToFile";
        novelEvent.key = dto.key;
        novelEvent.value = dto.value;
        list.Add(novelEvent);
    }

    public static KiteNovelEventDTO ExtractAndConvertToJson(string input)
    {
        Regex jsonRegex = new Regex(@"\{[^{}]*\}");
        MatchCollection matches = jsonRegex.Matches(input);

        foreach (Match match in matches)
        {
            try
            {
                KiteNovelEventDTO dto = JsonConvert.DeserializeObject<KiteNovelEventDTO>(match.Value);
                
                if (dto.event_art != null)
                {
                    return dto;
                }
            }
            catch
            {
                return null;
            }
        }
        return null;
    }

    public static int ConvertStringIntoExpressionType(string expressionType)
    {
        if (expressionType == null)
        {
            return 0;
        }
        if (expressionType.Contains("REL", StringComparison.OrdinalIgnoreCase))
        {
            return ExpressionTypeHelper.ToInt(ExpressionType.RELAXED);
        }
        else if (expressionType.Contains("AST", StringComparison.OrdinalIgnoreCase))
        {
            return ExpressionTypeHelper.ToInt(ExpressionType.ASTONISHED);
        }
        else if (expressionType.Contains("REF", StringComparison.OrdinalIgnoreCase))
        {
            return ExpressionTypeHelper.ToInt(ExpressionType.REFUSING);
        }
        else if (expressionType.Contains("SMI", StringComparison.OrdinalIgnoreCase))
        {
            return ExpressionTypeHelper.ToInt(ExpressionType.SMILING);
        }
        else if (expressionType.Contains("FRI", StringComparison.OrdinalIgnoreCase))
        {
            return ExpressionTypeHelper.ToInt(ExpressionType.FRIENDLY);
        }
        else if (expressionType.Contains("LAU", StringComparison.OrdinalIgnoreCase))
        {
            return ExpressionTypeHelper.ToInt(ExpressionType.LAUGHING);
        }
        else if (expressionType.Contains("CRI", StringComparison.OrdinalIgnoreCase))
        {
            return ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL);
        }
        else if (expressionType.Contains("DEC", StringComparison.OrdinalIgnoreCase))
        {
            return ExpressionTypeHelper.ToInt(ExpressionType.DECISION_NO);
        }
        else if (expressionType.Contains("HAP", StringComparison.OrdinalIgnoreCase))
        {
            return ExpressionTypeHelper.ToInt(ExpressionType.HAPPY);
        }
        else if (expressionType.Contains("PRO", StringComparison.OrdinalIgnoreCase))
        {
            return ExpressionTypeHelper.ToInt(ExpressionType.PROUD);
        }
        else if (expressionType.Contains("SCA", StringComparison.OrdinalIgnoreCase))
        {
            return ExpressionTypeHelper.ToInt(ExpressionType.SCARED);
        }
        else if (expressionType.Contains("QUE", StringComparison.OrdinalIgnoreCase))
        {
            return ExpressionTypeHelper.ToInt(ExpressionType.QUESTIONING);
        }
        else if (expressionType.Contains("DEF", StringComparison.OrdinalIgnoreCase))
        {
            return ExpressionTypeHelper.ToInt(ExpressionType.DEFEATED);
        }
        return 0;
    }

    public static int GetCompletionHandlerIdOutOfString(string value)
    {
        if (value == null)
        {
            return 0;
        }
        if ((value.Contains("Def", StringComparison.OrdinalIgnoreCase)) && 
            (value.Contains("Com", StringComparison.OrdinalIgnoreCase)) && 
            (value.Contains("Han", StringComparison.OrdinalIgnoreCase)))
        {
            return 1; // Default Completion Handler
        }
        return 0;
    }

    public static int GetAnimationTypeOutOfString(string value)
    {
        if (value == null)
        {
            return 0;
        }
        if ((value.Contains("WAT", StringComparison.OrdinalIgnoreCase)) && (value.Contains("POU", StringComparison.OrdinalIgnoreCase)))
        {
            return AnimationsEnumHelper.ToInt(AnimationsEnum.WATER_POURING);
        }
        return 0;
    }

    public static int GetSoundTypeOutOfString(string value)
    {
        if (value == null)
        {
            return 0;
        }
        if (value.Contains("WAT", StringComparison.OrdinalIgnoreCase) && value.Contains("POU", StringComparison.OrdinalIgnoreCase))
        {
            return SoundEnumHelper.ToInt(SoundsEnum.WATER_POURING);
        }
        else if (value.Contains("LEA", StringComparison.OrdinalIgnoreCase) && value.Contains("SCE", StringComparison.OrdinalIgnoreCase))
        {
            return SoundEnumHelper.ToInt(SoundsEnum.LEAVE_SCENE);
        }
        else if (value.Contains("TEL", StringComparison.OrdinalIgnoreCase) && value.Contains("CAL", StringComparison.OrdinalIgnoreCase))
        {
            return SoundEnumHelper.ToInt(SoundsEnum.TELEPHONE_CALL);
        }
        else if (value.Contains("PAP", StringComparison.OrdinalIgnoreCase) && value.Contains("SOU", StringComparison.OrdinalIgnoreCase))
        {
            return SoundEnumHelper.ToInt(SoundsEnum.PAPER_SOUND);
        }
        else if (value.Contains("MAN", StringComparison.OrdinalIgnoreCase) && value.Contains("LAU", StringComparison.OrdinalIgnoreCase))
        {
            return SoundEnumHelper.ToInt(SoundsEnum.MAN_LAUGHING);
        }
        return 0;
    }

    public static int GetLocationIdOutOfString(string value)
    {
        if (value == null)
        {
            return 0;
        }
        if (value.Contains("off", StringComparison.OrdinalIgnoreCase))
        {
            return 0;
        }
        return 0;
    }

    public static string GetNameOutOfString(string name)
    {
        if (name == null)
        {
            return "";
        }
        if (name.Contains("May", StringComparison.OrdinalIgnoreCase))
        {
            return "Mayer";
        }
        if (name.Contains("Lea", StringComparison.OrdinalIgnoreCase))
        {
            return "Lea";
        }
        if (name.Contains("Intro", StringComparison.OrdinalIgnoreCase))
        {
            return "Intro";
        }
        if (name.Contains("Outro", StringComparison.OrdinalIgnoreCase))
        {
            return "Outro";
        }
        if (name.Contains("Info", StringComparison.OrdinalIgnoreCase))
        {
            return "Info";
        }
        return "";
    }
}
