using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static UnityEngine.UI.Image;

public class KiteNovelConverter
{
    public static int counterForNamingPurpose = 1;
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

    public static KiteNovelEventList ConvertTextDocumentIntoEventList(string tweeFile, bool isWithStartValues, 
        string startLocation, string startTalkingPartner, string startTalkingPartnerEmotion, string charakter01,
        string charakter02, string charakter03)
    {
        KiteNovelEventList kiteNovelEventList = new KiteNovelEventList();
        kiteNovelEventList.novelEvents = new List<VisualNovelEvent>();

        if (isWithStartValues)
        {
            VisualNovelEvent initalLocationEvent = new VisualNovelEvent();
            initalLocationEvent.id = "initalLocationEvent001";
            initalLocationEvent.eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SET_BACKGROUND_EVENT);
            initalLocationEvent.nextId = "initalCharakterJoinsEvent001";
            initalLocationEvent.backgroundSpriteId = GetLocationIdOutOfString(startLocation);
            kiteNovelEventList.novelEvents.Add(initalLocationEvent);

            VisualNovelEvent initalCharacterJoinsEvent = new VisualNovelEvent();
            initalCharacterJoinsEvent.id = "initalCharakterJoinsEvent001";
            initalCharacterJoinsEvent.eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_JOIN_EVENT);
            initalCharacterJoinsEvent.nextId = TweeProcessor.GetStartLabelFromTweeFile(tweeFile);
            initalCharacterJoinsEvent.name = GetNameOutOfString(startTalkingPartner);
            initalCharacterJoinsEvent.expressionType = ConvertStringIntoExpressionType(startTalkingPartnerEmotion);
            kiteNovelEventList.novelEvents.Add(initalCharacterJoinsEvent);
        }

        List<TweePassage> passages = TweeProcessor.ProcessTweeFile(tweeFile);

        foreach (TweePassage passage in passages)
        {
            KiteNovelEventDTO dto = ExtractAndConvertToInformatonList(passage.passage, charakter01, charakter02, charakter03);
            VisualNovelEvent lastEventOfCurrentLoop = null;

            if ((dto == null) || (dto.event_art == null))
            {
                // do nothing here
            }
            else if (dto.event_art.Contains("ort", StringComparison.OrdinalIgnoreCase))
            {
                lastEventOfCurrentLoop = HandleLocationEvent(passage, dto, kiteNovelEventList.novelEvents);
            }
            else if (dto.event_art.Contains("kom", StringComparison.OrdinalIgnoreCase))
            {
                lastEventOfCurrentLoop = HandleCharacterComesEvent(passage, dto, kiteNovelEventList.novelEvents);
            }
            else if (dto.event_art.Contains("spr", StringComparison.OrdinalIgnoreCase))
            {
                lastEventOfCurrentLoop = HandleCharacterTalksEvent(passage, dto, kiteNovelEventList.novelEvents);
            }
            else if (dto.event_art.Contains("sou", StringComparison.OrdinalIgnoreCase))
            {
                lastEventOfCurrentLoop = HandlePlaySoundEvent(passage, dto, kiteNovelEventList.novelEvents);
            }
            else if (dto.event_art.Contains("ani", StringComparison.OrdinalIgnoreCase))
            {
                lastEventOfCurrentLoop = HandlePlayAnimationEvent(passage, dto, kiteNovelEventList.novelEvents);
            }
            else if (dto.event_art.Contains("fre", StringComparison.OrdinalIgnoreCase))
            {
                lastEventOfCurrentLoop = HandleFreeTextInputEvent(passage, dto, kiteNovelEventList.novelEvents);
            }
            else if (dto.event_art.Contains("gpt", StringComparison.OrdinalIgnoreCase))
            {
                lastEventOfCurrentLoop = HandleGptRequestEvent(passage, dto, kiteNovelEventList.novelEvents);
            }
            else if (dto.event_art.Contains("sav", StringComparison.OrdinalIgnoreCase))
            {
                lastEventOfCurrentLoop = HandleSaveDataEvent(passage, dto, kiteNovelEventList.novelEvents);
            }
            else if (dto.event_art.Contains("end", StringComparison.OrdinalIgnoreCase))
            {
                HandleEndNovelEvent(passage, dto, kiteNovelEventList.novelEvents);
            }
            else if (dto.event_art.Contains("bia", StringComparison.OrdinalIgnoreCase))
            {
                lastEventOfCurrentLoop = HandleBiasEvent(passage, dto, kiteNovelEventList.novelEvents);
            }

            if (passage.links == null || passage.links.Count <= 1)
            {
                continue;
            }
            HandleDialogueOptionEvent(passage, dto, kiteNovelEventList.novelEvents, lastEventOfCurrentLoop);
        }

        return kiteNovelEventList;
    }

    public static VisualNovelEvent HandleLocationEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
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
        return novelEvent;
    }

    public static VisualNovelEvent HandleCharacterTalksEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
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
        return novelEvent;
    }

    public static VisualNovelEvent HandleCharacterComesEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
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
        return novelEvent;
    }

    public static VisualNovelEvent HandleBiasEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
    {
        VisualNovelEvent novelEvent = new VisualNovelEvent();
        novelEvent.id = twee.label;
        novelEvent.eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.MARK_BIAS);

        if (twee.links != null)
        {
            novelEvent.nextId = twee.links[0].target;
        }

        novelEvent.relevantBias = dto.relevantBias;
        list.Add(novelEvent);
        return novelEvent;
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

    public static void HandleDialogueOptionEvent(TweePassage twee, KiteNovelEventDTO dto, 
        List<VisualNovelEvent> list, VisualNovelEvent lastEvent)
    {
        string label = "OptionsLabel" + counterForNamingPurpose;
        counterForNamingPurpose++;

        if (lastEvent != null)
        {
            lastEvent.nextId = label;
        } else
        {
            label = twee.label;
        }

        foreach (TweeLink link in twee.links)
        {
            VisualNovelEvent visualNovelEvent = new VisualNovelEvent();
            visualNovelEvent.eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT);
            visualNovelEvent.id = label;
            label = label + label;
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

    public static VisualNovelEvent HandlePlaySoundEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
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
        return novelEvent;
    }

    public static VisualNovelEvent HandlePlayAnimationEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
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
        return novelEvent;
    }

    public static VisualNovelEvent HandleFreeTextInputEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
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
        return novelEvent;
    }

    public static VisualNovelEvent HandleGptRequestEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
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
        return novelEvent;
    }

    public static VisualNovelEvent HandleSaveDataEvent(TweePassage twee, KiteNovelEventDTO dto, List<VisualNovelEvent> list)
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
        return novelEvent;
    }

    public static KiteNovelEventDTO ExtractAndConvertToInformatonList(string input, string charakter01, string charakter02, string charakter03)
    {
        KiteNovelEventDTO dto = ConvertListOfDataObjectsIntoKiteNovelEventDto(input, charakter01, charakter02, charakter03);

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

        if (dto.event_art.Contains("spricht", StringComparison.OrdinalIgnoreCase))
        {
            dto.dialog_nachricht = input;
        }
        else if (dto.event_art.Contains("freetext", StringComparison.OrdinalIgnoreCase))
        {
            string[] parts = input.Split(new[] { ':' }, 2);

            if (parts.Length == 2)
            {
                string key = parts[0];
                string value = parts[1];

                dto.variablen_name_fuer_die_antwort = key.Trim();
                dto.frage_fuer_freitext = value.Trim();
            }
        }
        else if (dto.event_art.Contains("gpt", StringComparison.OrdinalIgnoreCase))
        {
            string[] parts = input.Split(new[] { ':' }, 2);

            if (parts.Length == 2)
            {
                string key = parts[0];
                string value = parts[1];

                dto.variablen_name_fuer_die_antwort = key.Trim();
                dto.prompt_fuer_gpt = value.Trim();
            }
        }
        else if (dto.event_art.Contains("save", StringComparison.OrdinalIgnoreCase))
        {
            string[] parts = input.Split(new[] { ':' }, 2);

            if (parts.Length == 2)
            {
                string key = parts[0];
                string value = parts[1];

                dto.key = key.Trim();
                dto.value = value.Trim();
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
        foreach (string keyWord in NovelKeyWord.ALL_KEY_WORDS)
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

    private static KiteNovelEventDTO ConvertListOfDataObjectsIntoKiteNovelEventDto(string input, string charakter01, string charakter02, string charakter03)
    {
        if (string.IsNullOrEmpty(input))
        {
            return null;
        }
        KiteNovelEventDTO kiteNovelEventDTO = new KiteNovelEventDTO();

        if (input.Contains(NovelKeyWord.SZENE_BUERO, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "ort",
                ort = "office"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_01, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter01
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_RELAXED, StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter01,
                emotion_des_charakters = "relaxed"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_ASTONISHED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter01,
                emotion_des_charakters = "astonished"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_REFUSING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter01,
                emotion_des_charakters = "refusing"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_SMILING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter01,
                emotion_des_charakters = "smiling"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_FRIENDLY, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter01,
                emotion_des_charakters = "friendly"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_LAUGHING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter01,
                emotion_des_charakters = "laughing"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_CRITICAL, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter01,
                emotion_des_charakters = "critical"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_DECISION_NO, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter01,
                emotion_des_charakters = "decision_no"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_HAPPY, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter01,
                emotion_des_charakters = "happy"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_PROUD, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter01,
                emotion_des_charakters = "proud"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_SCARED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter01,
                emotion_des_charakters = "scared"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_QUESTIONING, StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter01,
                emotion_des_charakters = "questioning"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_01_GESICHTSAUSDRUCK_DEFEATED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter01,
                emotion_des_charakters = "defeate"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter01
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_RELAXED, StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter01,
                emotion_des_charakters = "relaxed"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_ASTONISHED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter01,
                emotion_des_charakters = "astonished"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_REFUSING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter01,
                emotion_des_charakters = "refusing"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_SMILING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter01,
                emotion_des_charakters = "smiling"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_FRIENDLY, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter01,
                emotion_des_charakters = "friendly"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_LAUGHING, StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter01,
                emotion_des_charakters = "laughing"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_CRITICAL, StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter01,
                emotion_des_charakters = "critical"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_DECISION_NO, StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter01,
                emotion_des_charakters = "decision_no"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_HAPPY, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter01,
                emotion_des_charakters = "happy"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_PROUD, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter01,
                emotion_des_charakters = "proud"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_SCARED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter01,
                emotion_des_charakters = "scared"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_QUESTIONING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter01,
                emotion_des_charakters = "questioning"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_01_GESICHTSAUSDRUCK_DEFEATED, StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter01,
                emotion_des_charakters = "defeated"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_02, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter02
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_RELAXED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter02,
                emotion_des_charakters = "relaxed"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_ASTONISHED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter02,
                emotion_des_charakters = "astonished"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_REFUSING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter02,
                emotion_des_charakters = "refusing"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_SMILING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter02,
                emotion_des_charakters = "smiling"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_FRIENDLY, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter02,
                emotion_des_charakters = "friendly"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_LAUGHING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter02,
                emotion_des_charakters = "laughing"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_CRITICAL, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter02,
                emotion_des_charakters = "critical"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_DECISION_NO, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter02,
                emotion_des_charakters = "decision_no"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_HAPPY, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter02,
                emotion_des_charakters = "happy"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_PROUD, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter02,
                emotion_des_charakters = "proud"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_SCARED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter02,
                emotion_des_charakters = "scared"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_QUESTIONING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter02,
                emotion_des_charakters = "questioning"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_02_GESICHTSAUSDRUCK_DEFEATED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter02,
                emotion_des_charakters = "defeate"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter02
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_RELAXED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter02,
                emotion_des_charakters = "relaxed"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_ASTONISHED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter02,
                emotion_des_charakters = "astonished"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_REFUSING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter02,
                emotion_des_charakters = "refusing"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_SMILING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter02,
                emotion_des_charakters = "smiling"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_FRIENDLY, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter02,
                emotion_des_charakters = "friendly"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_LAUGHING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter02,
                emotion_des_charakters = "laughing"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_CRITICAL, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter02,
                emotion_des_charakters = "critical"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_DECISION_NO, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter02,
                emotion_des_charakters = "decision_no"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_HAPPY, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter02,
                emotion_des_charakters = "happy"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_PROUD, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter02,
                emotion_des_charakters = "proud"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_SCARED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter02,
                emotion_des_charakters = "scared"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_QUESTIONING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter02,
                emotion_des_charakters = "questioning"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_02_GESICHTSAUSDRUCK_DEFEATED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter02,
                emotion_des_charakters = "defeated"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_03, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter03
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_RELAXED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter03,
                emotion_des_charakters = "relaxed"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_ASTONISHED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter03,
                emotion_des_charakters = "astonished"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_REFUSING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter03,
                emotion_des_charakters = "refusing"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_SMILING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter03,
                emotion_des_charakters = "smiling"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_FRIENDLY, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter03,
                emotion_des_charakters = "friendly"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_LAUGHING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter03,
                emotion_des_charakters = "laughing"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_CRITICAL, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter03,
                emotion_des_charakters = "critical"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_DECISION_NO, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter03,
                emotion_des_charakters = "decision_no"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_HAPPY, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter03,
                emotion_des_charakters = "happy"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_PROUD, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter03,
                emotion_des_charakters = "proud"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_SCARED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter03,
                emotion_des_charakters = "scared"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_QUESTIONING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter03,
                emotion_des_charakters = "questioning"
            };
        }
        if (input.Contains(NovelKeyWord.EINTRITT_CHARAKTER_03_GESICHTSAUSDRUCK_DEFEATED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = charakter03,
                emotion_des_charakters = "defeate"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter03
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_RELAXED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter03,
                emotion_des_charakters = "relaxed"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_ASTONISHED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter03,
                emotion_des_charakters = "astonished"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_REFUSING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter03,
                emotion_des_charakters = "refusing"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_SMILING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter03,
                emotion_des_charakters = "smiling"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_FRIENDLY, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter03,
                emotion_des_charakters = "friendly"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_LAUGHING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter03,
                emotion_des_charakters = "laughing"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_CRITICAL, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter03,
                emotion_des_charakters = "critical"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_DECISION_NO, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter03,
                emotion_des_charakters = "decision_no"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_HAPPY, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter03,
                emotion_des_charakters = "happy"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_PROUD, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter03,
                emotion_des_charakters = "proud"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_SCARED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter03,
                emotion_des_charakters = "scared"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_QUESTIONING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter03,
                emotion_des_charakters = "questioning"
            };
        }
        if (input.Contains(NovelKeyWord.CHARAKTER_SPRICHT_CHARAKTER_03_GESICHTSAUSDRUCK_DEFEATED, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = charakter03,
                emotion_des_charakters = "defeated"
            };
        }
        if (input.Contains(NovelKeyWord.SPIELER_CHARAKTER_SPRICHT, StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = "lea"
            };
        }
        if (input.Contains(NovelKeyWord.INFO_NACHRICHT_WIRD_ANGEZEIGT, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = "info"
            };
        }
        if (input.Contains(NovelKeyWord.SOUND_ABSPIELEN_WATER_POURING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "sound",
                audio_die_abgespielt_werden_soll = "water_pouring"
        };
        }
        if (input.Contains(NovelKeyWord.SOUND_ABSPIELEN_LEAVE_SCENE, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "sound",
                audio_die_abgespielt_werden_soll = "leave_scene"
            };
        }
        if (input.Contains(NovelKeyWord.SOUND_ABSPIELEN_TELEPHONE_CALL, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "sound",
                audio_die_abgespielt_werden_soll = "telephone_call"
            };
        }
        if (input.Contains(NovelKeyWord.SOUND_ABSPIELEN_PAPER_SOUND, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "sound",
                audio_die_abgespielt_werden_soll = "paper_sound"
            };
        }
        if (input.Contains(NovelKeyWord.SOUND_ABSPIELEN_MAN_LAUGHING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "sound",
                audio_die_abgespielt_werden_soll = "man_laughing"
            };
        }
        if (input.Contains(NovelKeyWord.ANIMATION_ABSPIELEN_WATER_POURING, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "animation",
                animation_die_abgespielt_werden_soll = "water_pouring"
            };
        }
        if (input.Contains(NovelKeyWord.ENDE, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "end"
            };
        }
        if (input.Contains(NovelKeyWord.FREITEXT_EINGABE, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "freetext"
            };
        }
        if (input.Contains(NovelKeyWord.GPT_PROMPT_MIT_DEFAULT_COMPLETION_HANDLER, StringComparison.OrdinalIgnoreCase)) 
        {
                return new KiteNovelEventDTO()
                {
                    event_art = "gpt",
                    id_nummer_des_completion_handlers = "DefaultCompletionHandler"
                };
            }
        if (input.Contains(NovelKeyWord.PERSISTENTES_SPEICHERN, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "save"
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_FINANZIERUNGSZUGANG, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.ACCESS_TO_FUNDING
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_GENDER_PAY_GAP, StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.GENDER_PAY_GAP
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_UNTERBEWERTUNG_WEIBLICH_GEFUEHRTER_UNTERNEHMEN, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.UNDERVALUATION_OF_WOMEN_LED_BUSINESSES
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_RISK_AVERSION_BIAS, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.RISK_AVERSION_BIAS
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_BESTAETIGUNGSVERZERRUNG, StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.CONFIRMATION_BIAS
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_TOKENISM, StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.TOKENISM
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_BIAS_IN_DER_WAHRNEHMUNG_VON_FUEHRUNGSFAEHIGKEITEN, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.IN_PERCEPTION_OF_LEADERSHIP_ABILITIES
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_RASSISTISCHE_UND_ETHNISCHE_BIASES, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.RACIST_AND_ETHNIC_BIASES
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_SOZIOOEKONOMISCHE_BIASES, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.SOCIOECONOMIC_BIASES
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_ALTER_UND_GENERATIONEN_BIASES, StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.AGE_AND_GENERATIONAL_BIASES
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_SEXUALITAETSBEZOGENE_BIASES, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.SEXUALITY_RELATED_BIASES
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_BIASES_GEGENUEBER_FRAUEN_MIT_BEHINDERUNGEN, StringComparison.OrdinalIgnoreCase)) {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.AGAINST_WOMEN_WITH_DISABILITIES
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_STEREOTYPE_GEGENUEBER_FRAUEN_IN_NICHT_TRADITIONELLEN_BRANCHEN, StringComparison.OrdinalIgnoreCase)) {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.STEREOTYPES_AGAINST_WOMEN_IN_NON_TRADITIONAL_INDUSTRIES
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_KULTURELLE_UND_RELIGIOESE_BIASES, StringComparison.OrdinalIgnoreCase)) {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.CULTURAL_AND_RELIGIOUS_BIASES
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_MATERNAL_BIAS, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.MATERNAL_BIAS
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_BIASES_GEGENUEBER_FRAUEN_MIT_KINDERN, StringComparison.OrdinalIgnoreCase)) {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.AGAINST_WOMEN_WITH_CHILDREN
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_ERWARTUNGSHALTUNG_BEZUEGLICH_FAMILIENPLANUNG, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.EXPECTATIONS_REGARDING_FAMILY_PLANNING
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_WORK_LIFE_BALANCE_ERWARTUNGEN, StringComparison.OrdinalIgnoreCase)) {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.WORK_LIFE_BALANCE_EXPECTATIONS
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_GESCHLECHTSSPEZIFISCHE_STEREOTYPEN, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.GENDER_SPECIFIC_STEREOTYPES
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_TIGHTROPE_BIAS, StringComparison.OrdinalIgnoreCase)) {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.TIGHTROPE_BIAS
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_MIKROAGGRESSIONEN, StringComparison.OrdinalIgnoreCase)) {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.MICROAGGRESSIONS
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_LEISTUNGSATTRIBUTIONS_BIAS, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.PERFORMANCE_ATTRIBUTION_BIAS
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_BIAS_IN_MEDIEN_UND_WERBUNG, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.IN_MEDIA_AND_ADVERTISING
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_UNBEWUSSTE_BIAS_IN_DER_KOMMUNIKATION, StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.UNCONSCIOUS_BIAS_IN_COMMUNICATION
            };
        }
        if (input.Contains(NovelKeyWord.RELEVANTER_BIAS_PROVE_IT_AGAIN_BIAS, StringComparison.OrdinalIgnoreCase)) {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                relevantBias = BiasName.PROVE_IT_AGAIN_BIAS
            };
        }

        if (kiteNovelEventDTO.event_art == null)
        {
            return null;
        }

        return kiteNovelEventDTO;
    }

    public static int ConvertStringIntoExpressionType(string expressionType)
    {
        if (expressionType == null)
        {
            return ExpressionTypeHelper.ToInt(ExpressionType.RELAXED);
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
        return ExpressionTypeHelper.ToInt(ExpressionType.RELAXED);
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
