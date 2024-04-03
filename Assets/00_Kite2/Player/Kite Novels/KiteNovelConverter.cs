using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
            KiteNovelEventDTO dto = ExtractAndConvertToInformatonList(passage.passage);

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

    public static KiteNovelEventDTO ExtractAndConvertToInformatonList(string input)
    {
        KiteNovelEventDTO dto = ConvertListOfDataObjectsIntoKiteNovelEventDto(input);

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

    private static KiteNovelEventDTO ConvertListOfDataObjectsIntoKiteNovelEventDto(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return null;
        }
        KiteNovelEventDTO kiteNovelEventDTO = new KiteNovelEventDTO();

        if (input.Contains(">>go_to_office<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "ort",
                ort = "office"
            };
        }
        if (input.Contains(">>intro_mayer<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = "mayer"
            };
        }
        if (input.Contains(">>intro_mayer_relaxed<<", StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = "mayer",
                emotion_des_charakters = "relaxed"
            };
        }
        if (input.Contains(">>intro_mayer_astonished<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = "mayer",
                emotion_des_charakters = "astonished"
            };
        }
        if (input.Contains(">>intro_mayer_refusing<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = "mayer",
                emotion_des_charakters = "refusing"
            };
        }
        if (input.Contains(">>intro_mayer_smiling<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = "mayer",
                emotion_des_charakters = "smiling"
            };
        }
        if (input.Contains(">>intro_mayer_friendly<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = "mayer",
                emotion_des_charakters = "friendly"
            };
        }
        if (input.Contains(">>intro_mayer_laughing<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = "mayer",
                emotion_des_charakters = "laughing"
            };
        }
        if (input.Contains(">>intro_mayer_critical<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = "mayer",
                emotion_des_charakters = "critical"
            };
        }
        if (input.Contains(">>intro_mayer_decision_no<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = "mayer",
                emotion_des_charakters = "decision_no"
            };
        }
        if (input.Contains(">>intro_mayer_happy<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = "mayer",
                emotion_des_charakters = "happy"
            };
        }
        if (input.Contains(">>intro_mayer_proud<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = "mayer",
                emotion_des_charakters = "proud"
            };
        }
        if (input.Contains(">>intro_mayer_scared<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = "mayer",
                emotion_des_charakters = "scared"
            };
        }
        if (input.Contains(">>intro_mayer_questioning<<", StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = "mayer",
                emotion_des_charakters = "questioning"
            };
        }
        if (input.Contains(">>intro_mayer_defeated<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "kom",
                name_des_charakters = "mayer",
                emotion_des_charakters = "defeate"
            };
        }
        if (input.Contains(">>mayer<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = "mayer"
            };
        }
        if (input.Contains(">>mayer_relaxed<<", StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = "mayer",
                emotion_des_charakters = "relaxed"
            };
        }
        if (input.Contains(">>mayer_astonished<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = "mayer",
                emotion_des_charakters = "astonished"
            };
        }
        if (input.Contains(">>mayer_refusing<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = "mayer",
                emotion_des_charakters = "refusing"
            };
        }
        if (input.Contains(">>mayer_smiling<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = "mayer",
                emotion_des_charakters = "smiling"
            };
        }
        if (input.Contains(">>mayer_friendly<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = "mayer",
                emotion_des_charakters = "friendly"
            };
        }
        if (input.Contains(">>mayer_laughing<<", StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = "mayer",
                emotion_des_charakters = "laughing"
            };
        }
        if (input.Contains(">>mayer_critical<<", StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = "mayer",
                emotion_des_charakters = "critical"
            };
        }
        if (input.Contains(">>mayer_decision_no<<", StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = "mayer",
                emotion_des_charakters = "decision_no"
            };
        }
        if (input.Contains(">>mayer_happy<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = "mayer",
                emotion_des_charakters = "happy"
            };
        }
        if (input.Contains(">>mayer_proud<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = "mayer",
                emotion_des_charakters = "proud"
            };
        }
        if (input.Contains(">>mayer_scared<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = "mayer",
                emotion_des_charakters = "scared"
            };
        }
        if (input.Contains(">>mayer_questioning<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = "mayer",
                emotion_des_charakters = "questioning"
            };
        }
        if (input.Contains(">>mayer_defeated<<", StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = "mayer",
                emotion_des_charakters = "defeated"
            };
        }
        if (input.Contains(">>lea<<", StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = "lea"
            };
        }
        if (input.Contains(">>info<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "spricht",
                name_des_charakters = "info"
            };
        }
        if (input.Contains(">>sound_water_pouring<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "sound",
                audio_die_abgespielt_werden_soll = "water_pouring"
        };
        }
        if (input.Contains(">>sound_leave_scene<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "sound",
                audio_die_abgespielt_werden_soll = "leave_scene"
            };
        }
        if (input.Contains(">>sound_telephone_call<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "sound",
                audio_die_abgespielt_werden_soll = "telephone_call"
            };
        }
        if (input.Contains(">>sound_paper<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "sound",
                audio_die_abgespielt_werden_soll = "paper_sound"
            };
        }
        if (input.Contains(">>sound_man_laughing<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "sound",
                audio_die_abgespielt_werden_soll = "man_laughing"
            };
        }
        if (input.Contains(">>animation_water_pouring<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "animation",
                animation_die_abgespielt_werden_soll = "water_pouring"
            };
        }
        if (input.Contains(">>options<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "optionen"
            };
        }
        if (input.Contains(">>end<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "end"
            };
        }
        if (input.Contains(">>user_input<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "freetext"
            };
        }
        if (input.Contains(">>ask_gpt_01<<", StringComparison.OrdinalIgnoreCase)) 
        {
                return new KiteNovelEventDTO()
                {
                    event_art = "gpt",
                    id_nummer_des_completion_handlers = "DefaultCompletionHandler"
                };
            }
        if (input.Contains(">>save<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "save"
            };
        }
        if (input.Contains(">>bias_finanzierungszugang<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_finanzierungszugang",
                value = "true"
            };
        }
        if (input.Contains(">>bias_gender_pay_gap<<", StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_gender_pay_gap",
                value = "true"
            };
        }
        if (input.Contains(">>bias_unterbewertung_weiblich_gefuehrter_unternehmen<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_unterbewertung_weiblich_gefuehrter_unternehmen",
                value = "true"
            };
        }
        if (input.Contains(">>bias_risk_aversion_bias<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_risk_aversion_bias",
                value = "true"
            };
        }
        if (input.Contains(">>bias_bestaetigungsverzerrung<<", StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_bestaetigungsverzerrung",
                value = "true"
            };
        }
        if (input.Contains(">>bias_tokenism<<", StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_tokenism",
                value = "true"
            };
        }
        if (input.Contains(">>bias_bias_in_der_wahrnehmung_von_fuehrungsfaehigkeiten<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_bias_in_der_wahrnehmung_von_fuehrungsfaehigkeiten",
                value = "true"
            };
        }
        if (input.Contains(">>bias_rassistische_und_ethnische_biases<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_rassistische_und_ethnische_biases",
                value = "true"
            };
        }
        if (input.Contains(">>bias_soziooekonomische_biases<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_soziooekonomische_biases",
                value = "true"
            };
        }
        if (input.Contains(">>bias_alter_und_generationen_biases<<", StringComparison.OrdinalIgnoreCase)) 
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_alter_und_generationen_biases",
                value = "true"
            };
        }
        if (input.Contains(">>bias_sexualitaetsbezogene_biases<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_sexualitaetsbezogene_biases",
                value = "true"
            };
        }
        if (input.Contains(">>bias_biases_gegenueber_frauen_mit_behinderungen<<", StringComparison.OrdinalIgnoreCase)) {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_biases_gegenueber_frauen_mit_behinderungen",
                value = "true"
            };
        }
        if (input.Contains(">>bias_stereotype_gegenueber_frauen_in_nicht_traditionellen_branchen<<", StringComparison.OrdinalIgnoreCase)) {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_stereotype_gegenueber_frauen_in_nicht_traditionellen_branchen",
                value = "true"
            };
        }
        if (input.Contains(">>bias_kulturelle_und_religioese_biases<<", StringComparison.OrdinalIgnoreCase)) {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_kulturelle_und_religioese_biases",
                value = "true"
            };
        }
        if (input.Contains(">>bias_maternal_bias<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_maternal_bias",
                value = "true"
            };
        }
        if (input.Contains(">>bias_biases_gegenueber_frauen_mit_kindern<<", StringComparison.OrdinalIgnoreCase)) {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_biases_gegenueber_frauen_mit_kindern",
                value = "true"
            };
        }
        if (input.Contains(">>bias_erwartungshaltung_bezueglich_familienplanung<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_erwartungshaltung_bezueglich_familienplanung",
                value = "true"
            };
        }
        if (input.Contains(">>bias_work_life_balance_erwartungen<<", StringComparison.OrdinalIgnoreCase)) {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_work_life_balance_erwartungen",
                value = "true"
            };
        }
        if (input.Contains(">>bias_geschlechtsspezifische_stereotypen<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_geschlechtsspezifische_stereotypen",
                value = "true"
            };
        }
        if (input.Contains(">>bias_tightrope_bias<<", StringComparison.OrdinalIgnoreCase)) {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_tightrope_bias",
                value = "true"
            };
        }
        if (input.Contains(">>bias_mikroaggressionen<<", StringComparison.OrdinalIgnoreCase)) {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_mikroaggressionen",
                value = "true"
            };
        }
        if (input.Contains(">>bias_leistungsattributions_bias<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_leistungsattributions_bias",
                value = "true"
            };
        }
        if (input.Contains(">>bias_bias_in_medien_und_werbung<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_bias_in_medien_und_werbung",
                value = "true"
            };
        }
        if (input.Contains(">>bias_unbewusste_bias_in_der_kommunikation<<", StringComparison.OrdinalIgnoreCase))
        {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_unbewusste_bias_in_der_kommunikation",
                value = "true"
            };
        }
        if (input.Contains(">>bias_prove_it_again_bias<<", StringComparison.OrdinalIgnoreCase)) {
            return new KiteNovelEventDTO()
            {
                event_art = "bias",
                key = "bias_prove_it_again_bias",
                value = "true"
            };
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
