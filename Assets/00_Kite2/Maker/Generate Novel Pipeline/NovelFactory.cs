using System.Collections.Generic;

public class NovelFactory
{
    public static VisualNovel GenerateNovel(NovelInformationWrapper wrapper)
    {
        VisualNovel novel = new VisualNovel();

        novel.id = -100;
        novel.title = wrapper.title;
        novel.description = wrapper.description;
        novel.image = wrapper.image;
        novel.nameOfMainCharacter = wrapper.nameOfMainCharacter;
        novel.context = wrapper.context;

        novel.novelEvents = new List<VisualNovelEvent>()
        {
            new VisualNovelEvent()
            {
                id = 1,
                nextId = 2,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SET_BACKGROUND_EVENT),
                waitForUserConfirmation = false,
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                xPosition = 0,
                yPosition = 0,
                backgroundSpriteId = wrapper.backgroundSprite
            },

            new VisualNovelEvent()
            {
                id = 2,
                nextId = 3,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_JOIN_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.nameOfTalkingPartner,
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = wrapper.expressionTypeWhileJoining,
                xPosition = 0,
                yPosition = -6,
                skinSpriteId = 0,
                clotheSpriteId = 0,
                hairSpriteId = 0,
                faceSpriteId = 0
            },

            new VisualNovelEvent()
            {
                id = 3,
                nextId = 4,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker01,
                text = wrapper.message01,
                expressionType = wrapper.expressionType01
            },

            new VisualNovelEvent()
            {
                id = 4,
                nextId = 5,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker02,
                text = wrapper.message02,
                expressionType = wrapper.expressionType02
            },

            new VisualNovelEvent()
            {
                id = 5,
                nextId = 6,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name =  wrapper.speaker03,
                text =  wrapper.message03,
                expressionType = wrapper.expressionType03
            },

            new VisualNovelEvent()
            {
                id = 6,
                nextId = 7,
                onChoice = 9,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfMainCharacter,
                text = wrapper.message04_Option01
            },

            new VisualNovelEvent()
            {
                id = 7,
                nextId = 7001,
                onChoice = 9,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfMainCharacter,
                text = wrapper.message04_Option02
            },

            new VisualNovelEvent()
            {
                id = 7001,
                nextId = 7002,
                onChoice = 9,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfMainCharacter,
                text = wrapper.message04_Option03
            },

            new VisualNovelEvent()
            {
                id = 7002,
                nextId = 8,
                onChoice = 9,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfMainCharacter,
                text = wrapper.message04_Option04
            },

            new VisualNovelEvent()
            {
                id = 8,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 9,
                nextId = 10,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker05,
                text = wrapper.message05,
                expressionType = wrapper.expressionType05
            },

            new VisualNovelEvent()
            {
                id = 10,
                nextId = 11,
                onChoice = 13,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfMainCharacter,
                text = wrapper.message06_Option01
            },

            new VisualNovelEvent()
            {
                id = 11,
                nextId = 1101,
                onChoice = 13,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfMainCharacter,
                text = wrapper.message06_Option02
            },

            new VisualNovelEvent()
            {
                id = 1101,
                nextId = 1102,
                onChoice = 13,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfMainCharacter,
                text = wrapper.message06_Option03
            },

            new VisualNovelEvent()
            {
                id = 1102,
                nextId = 12,
                onChoice = 13,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfMainCharacter,
                text = wrapper.message06_Option04
            },

            new VisualNovelEvent()
            {
                id = 12,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 13,
                nextId = 15,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker07,
                text = wrapper.message07,
                expressionType = wrapper.expressionType07
            },

            new VisualNovelEvent()
            {
                id = 15,
                nextId = 16,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker08,
                text = wrapper.message08,
                expressionType = wrapper.expressionType08
            },

            new VisualNovelEvent()
            {
                id = 16,
                nextId = 17,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker09,
                text = wrapper.message09,
                expressionType = wrapper.expressionType09
            },

            new VisualNovelEvent()
            {
                id = 17,
                nextId = 18,
                onChoice = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfMainCharacter,
                text = wrapper.message10_Option01
            },

            new VisualNovelEvent()
            {
                id = 18,
                nextId = 1801,
                onChoice = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfMainCharacter,
                text = wrapper.message10_Option02
            },

            new VisualNovelEvent()
            {
                id = 1801,
                nextId = 1802,
                onChoice = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfMainCharacter,
                text = wrapper.message10_Option03
            },

            new VisualNovelEvent()
            {
                id = 1802,
                nextId = 19,
                onChoice = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfMainCharacter,
                text = wrapper.message10_Option04
            },

            new VisualNovelEvent()
            {
                id = 19,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 20,
                nextId = 21,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker11,
                text = wrapper.message11,
                expressionType = wrapper.expressionType11
            },

            new VisualNovelEvent()
            {
                id = 21,
                nextId = 22,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker12,
                text = wrapper.message12,
                expressionType = wrapper.expressionType12
            },

            new VisualNovelEvent()
            {
                id = 22,
                nextId = 23,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker13,
                text = wrapper.message13,
                expressionType = wrapper.expressionType13
            },

            new VisualNovelEvent()
            {
                id = 23,
                nextId = 24,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker14,
                text = wrapper.message14,
                expressionType = wrapper.expressionType14
            },

            new VisualNovelEvent()
            {
                id = 24,
                nextId = 25,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker15,
                text = wrapper.message15,
                expressionType = wrapper.expressionType15
            },

            new VisualNovelEvent()
            {
                id = 25,
                nextId = 26,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker16,
                text = wrapper.message16,
                expressionType = wrapper.expressionType16
            },

            new VisualNovelEvent()
            {
                id = 26,
                nextId = 27,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker17,
                text = wrapper.message17,
                expressionType = wrapper.expressionType17
            },

            new VisualNovelEvent()
            {
                id = 27,
                nextId = 28,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker18,
                text = wrapper.message18,
                expressionType = wrapper.expressionType18
            },

            new VisualNovelEvent()
            {
                id = 28,
                nextId = 29,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker19,
                text = wrapper.message19,
                expressionType = wrapper.expressionType19
            },

            new VisualNovelEvent()
            {
                id = 29,
                nextId = 36,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker20,
                text = wrapper.message20,
                expressionType = wrapper.expressionType20
            },

            new VisualNovelEvent()
            {
                id = 36,
                nextId = 37,
                onChoice = 41,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfMainCharacter,
                text = wrapper.message21_Option01
            },

            new VisualNovelEvent()
            {
                id = 37,
                nextId = 38,
                onChoice = 41,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfMainCharacter,
                text = wrapper.message21_Option02
            },

            new VisualNovelEvent()
            {
                id = 38,
                nextId = 39,
                onChoice = 41,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfMainCharacter,
                text = wrapper.message21_Option03
            },

            new VisualNovelEvent()
            {
                id = 39,
                nextId = 40,
                onChoice = 41,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfMainCharacter,
                text = wrapper.message21_Option04
            },

            new VisualNovelEvent()
            {
                id = 40,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 41,
                nextId = 42,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker22,
                text = wrapper.message22,
                expressionType = wrapper.expressionType22
            },

            new VisualNovelEvent()
            {
                id = 42,
                nextId = 43,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker23,
                text = wrapper.message23,
                expressionType = wrapper.expressionType23
            },

            new VisualNovelEvent()
            {
                id = 43,
                nextId = 44,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker24,
                text = wrapper.message24,
                expressionType = wrapper.expressionType24
            },

            new VisualNovelEvent()
            {
                id = 44,
                nextId = 45,
                onChoice = 48,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfMainCharacter,
                text = wrapper.message25_Option01
            },

            new VisualNovelEvent()
            {
                id = 45,
                nextId = 46,
                onChoice = 63,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfMainCharacter,
                text = wrapper.message25_Option02
            },

            new VisualNovelEvent()
            {
                id = 46,
                nextId = 4601,
                onChoice = 68,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfMainCharacter,
                text = wrapper.message25_Option03
            },

            new VisualNovelEvent()
            {
                id = 4601,
                nextId = 47,
                onChoice = 73,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfMainCharacter,
                text = wrapper.message25_Option04
            },

            new VisualNovelEvent()
            {
                id = 47,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            // Alternative 01 start

            new VisualNovelEvent()
            {
                id = 48,
                nextId = 49,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker26_Alternative01,
                text = wrapper.message26_Alternative01,
                expressionType = wrapper.expressionType26_Alternative01
            },

            new VisualNovelEvent()
            {
                id = 49,
                nextId = 50,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker27_Alternative01,
                text = wrapper.message27_Alternative01,
                expressionType = wrapper.expressionType27_Alternative01
            },

            new VisualNovelEvent()
            {
                id = 50,
                nextId = 51,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker28_Alternative01,
                text = wrapper.message28_Alternative01,
                expressionType = wrapper.expressionType28_Alternative01
            },

            new VisualNovelEvent()
            {
                id = 51,
                nextId = 52,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker29_Alternative01,
                text = wrapper.message29_Alternative01,
                expressionType = wrapper.expressionType29_Alternative01
            },

            new VisualNovelEvent()
            {
                id = 52,
                nextId = 61,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker30_Alternative01,
                text = wrapper.message30_Alternative01,
                expressionType = wrapper.expressionType30_Alternative01
            },

            //ALternative01 ende

            //Alternative02 Start

            new VisualNovelEvent()
            {
                id = 63,
                nextId = 64,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker26_Alternative02,
                text = wrapper.message26_Alternative02,
                expressionType = wrapper.expressionType26_Alternative02
            },

            new VisualNovelEvent()
            {
                id = 64,
                nextId = 65,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker27_Alternative02,
                text = wrapper.message27_Alternative02,
                expressionType = wrapper.expressionType27_Alternative02
            },

            new VisualNovelEvent()
            {
                id = 65,
                nextId = 66,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker28_Alternative02,
                text = wrapper.message28_Alternative02,
                expressionType = wrapper.expressionType28_Alternative02
            },

            new VisualNovelEvent()
            {
                id = 66,
                nextId = 67,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker29_Alternative02,
                text = wrapper.message29_Alternative02,
                expressionType = wrapper.expressionType29_Alternative02
            },

            new VisualNovelEvent()
            {
                id = 67,
                nextId = 61,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker30_Alternative02,
                text = wrapper.message30_Alternative02,
                expressionType = wrapper.expressionType30_Alternative02
            },

            //Alternative 02 Ende

            //Alternative03 Start

            new VisualNovelEvent()
            {
                id = 68,
                nextId = 69,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker26_Alternative03,
                text = wrapper.message26_Alternative03,
                expressionType = wrapper.expressionType26_Alternative03
            },

            new VisualNovelEvent()
            {
                id = 69,
                nextId = 70,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker27_Alternative03,
                text = wrapper.message27_Alternative03,
                expressionType = wrapper.expressionType27_Alternative03
            },

            new VisualNovelEvent()
            {
                id = 70,
                nextId = 71,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker28_Alternative03,
                text = wrapper.message28_Alternative03,
                expressionType = wrapper.expressionType28_Alternative03
            },

            new VisualNovelEvent()
            {
                id = 71,
                nextId = 72,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker29_Alternative03,
                text = wrapper.message29_Alternative03,
                expressionType = wrapper.expressionType29_Alternative03
            },

            new VisualNovelEvent()
            {
                id = 72,
                nextId = 61,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker30_Alternative03,
                text = wrapper.message30_Alternative03,
                expressionType = wrapper.expressionType30_Alternative03
            },

            //Alternative 03 Ende

            //Alternative 04 Start

            new VisualNovelEvent()
            {
                id = 73,
                nextId = 74,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker26_Alternative04,
                text = wrapper.message26_Alternative04,
                expressionType = wrapper.expressionType26_Alternative04
            },

            new VisualNovelEvent()
            {
                id = 74,
                nextId = 75,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker27_Alternative04,
                text = wrapper.message27_Alternative04,
                expressionType = wrapper.expressionType27_Alternative04
            },

            new VisualNovelEvent()
            {
                id = 75,
                nextId = 76,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker28_Alternative04,
                text = wrapper.message28_Alternative04,
                expressionType = wrapper.expressionType28_Alternative04
            },

            new VisualNovelEvent()
            {
                id = 76,
                nextId = 77,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker29_Alternative04,
                text = wrapper.message29_Alternative04,
                expressionType = wrapper.expressionType29_Alternative04
            },

            new VisualNovelEvent()
            {
                id = 77,
                nextId = 61,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = wrapper.speaker30_Alternative04,
                text = wrapper.message30_Alternative04,
                expressionType = wrapper.expressionType30_Alternative04
            },

            //Alternative 04 Ende

            new VisualNovelEvent()
            {
                id = 61,
                nextId = 62,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_EXIT_EVENT),
                waitForUserConfirmation = false,
                name = wrapper.nameOfTalkingPartner,
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = wrapper.expressionTypeWhileLeaving
            },

            new VisualNovelEvent()
            {
                id = 62,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.END_NOVEL_EVENT),
                waitForUserConfirmation = false
            }
        };

        return novel;
    }

    public class NovelInformationWrapper
    {
        public string title;
        public string description;
        public long image;
        public string nameOfMainCharacter;
        public string nameOfTalkingPartner;
        public string context;

        public int backgroundSprite;
        public int expressionTypeWhileJoining;

        public string speaker01;
        public string message01;
        public int expressionType01;

        public string speaker02;
        public string message02;
        public int expressionType02;

        public string speaker03;
        public string message03;
        public int expressionType03;

        public string message04_Option01;
        public string message04_Option02;
        public string message04_Option03;
        public string message04_Option04;

        public string speaker05;
        public string message05;
        public int expressionType05;

        public string message06_Option01;
        public string message06_Option02;
        public string message06_Option03;
        public string message06_Option04;

        public string speaker07;
        public string message07;
        public int expressionType07;

        public string speaker08;
        public string message08;
        public int expressionType08;

        public string speaker09;
        public string message09;
        public int expressionType09;

        public string message10_Option01;
        public string message10_Option02;
        public string message10_Option03;
        public string message10_Option04;

        public string speaker11;
        public string message11;
        public int expressionType11;

        public string speaker12;
        public string message12;
        public int expressionType12;

        public string speaker13;
        public string message13;
        public int expressionType13;

        public string speaker14;
        public string message14;
        public int expressionType14;

        public string speaker15;
        public string message15;
        public int expressionType15;

        public string speaker16;
        public string message16;
        public int expressionType16;

        public string speaker17;
        public string message17;
        public int expressionType17;

        public string speaker18;
        public string message18;
        public int expressionType18;

        public string speaker19;
        public string message19;
        public int expressionType19;

        public string speaker20;
        public string message20;
        public int expressionType20;

        public string message21_Option01;
        public string message21_Option02;
        public string message21_Option03;
        public string message21_Option04;

        public string speaker22;
        public string message22;
        public int expressionType22;

        public string speaker23;
        public string message23;
        public int expressionType23;

        public string speaker24;
        public string message24;
        public int expressionType24;

        public string message25_Option01;
        public string message25_Option02;
        public string message25_Option03;
        public string message25_Option04;

        public string speaker26_Alternative01;
        public string message26_Alternative01;
        public int expressionType26_Alternative01;

        public string speaker27_Alternative01;
        public string message27_Alternative01;
        public int expressionType27_Alternative01;

        public string speaker28_Alternative01;
        public string message28_Alternative01;
        public int expressionType28_Alternative01;

        public string speaker29_Alternative01;
        public string message29_Alternative01;
        public int expressionType29_Alternative01;

        public string speaker30_Alternative01;
        public string message30_Alternative01;
        public int expressionType30_Alternative01;

        public string speaker26_Alternative02;
        public string message26_Alternative02;
        public int expressionType26_Alternative02;

        public string speaker27_Alternative02;
        public string message27_Alternative02;
        public int expressionType27_Alternative02;

        public string speaker28_Alternative02;
        public string message28_Alternative02;
        public int expressionType28_Alternative02;

        public string speaker29_Alternative02;
        public string message29_Alternative02;
        public int expressionType29_Alternative02;

        public string speaker30_Alternative02;
        public string message30_Alternative02;
        public int expressionType30_Alternative02;

        public string speaker26_Alternative03;
        public string message26_Alternative03;
        public int expressionType26_Alternative03;

        public string speaker27_Alternative03;
        public string message27_Alternative03;
        public int expressionType27_Alternative03;

        public string speaker28_Alternative03;
        public string message28_Alternative03;
        public int expressionType28_Alternative03;

        public string speaker29_Alternative03;
        public string message29_Alternative03;
        public int expressionType29_Alternative03;

        public string speaker30_Alternative03;
        public string message30_Alternative03;
        public int expressionType30_Alternative03;

        public string speaker26_Alternative04;
        public string message26_Alternative04;
        public int expressionType26_Alternative04;

        public string speaker27_Alternative04;
        public string message27_Alternative04;
        public int expressionType27_Alternative04;

        public string speaker28_Alternative04;
        public string message28_Alternative04;
        public int expressionType28_Alternative04;

        public string speaker29_Alternative04;
        public string message29_Alternative04;
        public int expressionType29_Alternative04;

        public string speaker30_Alternative04;
        public string message30_Alternative04;
        public int expressionType30_Alternative04;

        public int expressionTypeWhileLeaving;
    }
}
