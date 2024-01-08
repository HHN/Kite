using System.Collections.Generic;

public class NovelFactory
{
    public static VisualNovel GenerateNovel(Dictionary<string, string> memory, Dictionary<int, string> idToKey, Dictionary<int, string> idToValue)
    {
        VisualNovel novel = new VisualNovel();

        novel.id = -100;
        novel.title = memory[GenerateNovelPipeline.GENERATED_TITLE];
        novel.description = memory[GenerateNovelPipeline.GENERATED_DESCRIPTION];
        novel.image = long.Parse(memory[GenerateNovelPipeline.CHOSEN_DISPLAY_IMAGE]);
        novel.nameOfMainCharacter = "Lea";
        novel.feedback = "";
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
                backgroundSpriteId = 0
            },

            new VisualNovelEvent()
            {
                id = 2,
                nextId = 3,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_JOIN_EVENT),
                waitForUserConfirmation = true,
                name = "Herr Mayer",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED),
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
                name = idToKey[3],
                text = idToValue[3]
            },

            new VisualNovelEvent()
            {
                id = 4,
                nextId = 5,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[4],
                text = idToValue[4],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 5,
                nextId = 6,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[5],
                text = idToValue[5],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 6,
                nextId = 7,
                onChoice = 9,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = idToKey[6],
                text = idToValue[6]
            },

            new VisualNovelEvent()
            {
                id = 7,
                nextId = 7001,
                onChoice = 9,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = idToKey[6],
                text = memory[GenerateNovelPipeline.ALTERNATIVE_ANSWER_01_01]
            },

            new VisualNovelEvent()
            {
                id = 7001,
                nextId = 7002,
                onChoice = 9,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = idToKey[6],
                text = memory[GenerateNovelPipeline.ALTERNATIVE_ANSWER_01_02]
            },

            new VisualNovelEvent()
            {
                id = 7002,
                nextId = 8,
                onChoice = 9,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = idToKey[6],
                text = memory[GenerateNovelPipeline.ALTERNATIVE_ANSWER_01_03]
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
                name = idToKey[7],
                text = idToValue[7],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 10,
                nextId = 11,
                onChoice = 13,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = idToKey[8],
                text = idToValue[8]
            },

            new VisualNovelEvent()
            {
                id = 11,
                nextId = 1101,
                onChoice = 13,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = idToKey[8],
                text = memory[GenerateNovelPipeline.ALTERNATIVE_ANSWER_02_01]
            },

            new VisualNovelEvent()
            {
                id = 1101,
                nextId = 1102,
                onChoice = 13,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = idToKey[8],
                text = memory[GenerateNovelPipeline.ALTERNATIVE_ANSWER_02_02]
            },

            new VisualNovelEvent()
            {
                id = 1102,
                nextId = 12,
                onChoice = 13,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = idToKey[8],
                text = memory[GenerateNovelPipeline.ALTERNATIVE_ANSWER_02_03]
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
                name = idToKey[9],
                text = idToValue[9],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 15,
                nextId = 16,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[10],
                text = idToValue[10],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 16,
                nextId = 17,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[11],
                text = idToValue[11],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 17,
                nextId = 18,
                onChoice = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = idToKey[12],
                text = idToValue[12]
            },

            new VisualNovelEvent()
            {
                id = 18,
                nextId = 1801,
                onChoice = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = idToKey[12],
                text = memory[GenerateNovelPipeline.ALTERNATIVE_ANSWER_03_01]
            },

            new VisualNovelEvent()
            {
                id = 1801,
                nextId = 1802,
                onChoice = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = idToKey[12],
                text = memory[GenerateNovelPipeline.ALTERNATIVE_ANSWER_03_02]
            },

            new VisualNovelEvent()
            {
                id = 1802,
                nextId = 19,
                onChoice = 20,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = idToKey[12],
                text = memory[GenerateNovelPipeline.ALTERNATIVE_ANSWER_03_03]
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
                name = idToKey[13],
                text = idToValue[13],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 21,
                nextId = 22,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[14],
                text = idToValue[14],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 22,
                nextId = 23,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[15],
                text = idToValue[15],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 23,
                nextId = 24,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[16],
                text = idToValue[16],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 24,
                nextId = 25,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[17],
                text = idToValue[17],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 25,
                nextId = 26,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[18],
                text = idToValue[18],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 26,
                nextId = 27,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[19],
                text = idToValue[19],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 27,
                nextId = 28,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[20],
                text = idToValue[20],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 28,
                nextId = 29,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[21],
                text = idToValue[21],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 29,
                nextId = 36,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[22],
                text = idToValue[22],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 36,
                nextId = 37,
                onChoice = 41,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = idToKey[23],
                text = idToValue[23]
            },

            new VisualNovelEvent()
            {
                id = 37,
                nextId = 38,
                onChoice = 41,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = idToKey[23],
                text = memory[GenerateNovelPipeline.ALTERNATIVE_ANSWER_04_01]
            },

            new VisualNovelEvent()
            {
                id = 38,
                nextId = 39,
                onChoice = 41,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = idToKey[23],
                text = memory[GenerateNovelPipeline.ALTERNATIVE_ANSWER_04_02]
            },

            new VisualNovelEvent()
            {
                id = 39,
                nextId = 40,
                onChoice = 41,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = idToKey[23],
                text = memory[GenerateNovelPipeline.ALTERNATIVE_ANSWER_04_03]
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
                name = idToKey[24],
                text = idToValue[24],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 42,
                nextId = 43,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[25],
                text = idToValue[25],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 43,
                nextId = 44,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[26],
                text = idToValue[26],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 44,
                nextId = 45,
                onChoice = 48,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = idToKey[27],
                text = memory[GenerateNovelPipeline.ALTERNATIVE_ANSWER_05_02]
            },

            new VisualNovelEvent()
            {
                id = 45,
                nextId = 46,
                onChoice = 48,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = idToKey[27],
                text = memory[GenerateNovelPipeline.ALTERNATIVE_ANSWER_05_01]
            },

            new VisualNovelEvent()
            {
                id = 46,
                nextId = 4601,
                onChoice = 48,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = idToKey[27],
                text = idToValue[27]
            },

            new VisualNovelEvent()
            {
                id = 4601,
                nextId = 47,
                onChoice = 48,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = idToKey[27],
                text = memory[GenerateNovelPipeline.ALTERNATIVE_ANSWER_05_03]
            },

            new VisualNovelEvent()
            {
                id = 47,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 48,
                nextId = 49,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[28],
                text = idToValue[28],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 49,
                nextId = 50,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[29],
                text = idToValue[29],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 50,
                nextId = 51,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[30],
                text = idToValue[30],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 51,
                nextId = 52,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[31],
                text = idToValue[31],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 52,
                nextId = 53,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[32],
                text = idToValue[32],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 53,
                nextId = 54,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[33],
                text = idToValue[33],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 54,
                nextId = 55,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[34],
                text = idToValue[34],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 55,
                nextId = 56,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[35],
                text = idToValue[35],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 56,
                nextId = 57,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[36],
                text = idToValue[36],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 57,
                nextId = 58,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[37],
                text = idToValue[37],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 58,
                nextId = 59,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[38],
                text = idToValue[38],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 59,
                nextId = 60,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[39],
                text = idToValue[39],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
             id = 60,
             nextId = 61,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = idToKey[40],
                text = idToValue[40],
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
            },

            new VisualNovelEvent()
            {
                id = 61,
                nextId = 62,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_EXIT_EVENT),
                waitForUserConfirmation = false,
                name = "Herr Mayer",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.RELAXED)
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
}
