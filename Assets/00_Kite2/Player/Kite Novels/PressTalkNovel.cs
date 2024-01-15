using System.Collections.Generic;
using UnityEngine;

public class PressTalkNovel : VisualNovel
{
    public PressTalkNovel()
    {
        id = -3;
        title = "Pressegespräch";
        description = "Du befindest dich auf einer Veranstaltung, bei der Jungunternehmer*innen ihre Geschäftsidee vor einem Publikum präsentieren können, um Rückmeldung zu der Idee zu erhalten und zu networken. Nachdem du deine Geschäftsidee vor dem Publikum gepitcht hast, stellst du dich an einen Tisch mit anderen Gästen, um mit ihnen zu reden.";
        image = 2;
        context = "Es ist das Gespräch einer Gründerin, Lea, mit einer Journalistin. Die Journalistin hat die Gründerin angesprochen und möchte einen Artikel über sie und ihr Unternehmen schreiben. Die Gründerin freut sich über diese Gelegenheit und hofft auf einen schönen Artikel.";
        nameOfMainCharacter = "Lea";
        feedback = "";
        novelEvents = new List<VisualNovelEvent>()
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
                name = "Frau Mayer",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING),
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
                name = "Intro",
                text = "Du befindest dich auf einer Veranstaltung, bei der Jungunternehmer*innen ihre " +
                "Geschäftsidee vor einem Publikum präsentieren können, um Rückmeldung zu der Idee zu " +
                "erhalten und zu networken. Nachdem du deine Geschäftsidee vor dem Publikum gepitcht " +
                "hast, stellst du dich an einen Tisch mit anderen Gästen, um mit ihnen zu reden."
            },

            new VisualNovelEvent()
            {
                id = 4,
                nextId = 5,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Das war ein sehr interessanter Pitch. Ich bin von der lokalen Presse und es " +
                "würde mich freuen, wenn ich etwas über Ihre Geschäftsidee schreiben darf.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 5,
                nextId = 6,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Danke! Das wäre toll! Haben Sie eine spezielle Frage für Ihren Artikel?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 6,
                nextId = 7,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Ja. Wann kamen Sie auf die Idee für Ihr Unternehmen?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 7,
                nextId = 8,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Die Idee für mein Unternehmen kam mir während eines Sabbatjahres im Jahr 2021, " +
                "als ich die enorme Lücke in der Branche für nachhaltige, personalisierte Technologien " +
                "entdeckte.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 8,
                nextId = 15,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Das ist spannend. Eine Frau, die ein Unternehmen gründet, " +
                "das wird sicher viele Leser*innen interessieren.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            // new VisualNovelEvent()
            // {
            //     id = 9,
            //     nextId = 10,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_OPINION_FEEDBACK_EVENT),
            //     waitForUserConfirmation = false
            // },

            // new VisualNovelEvent()
            // {
            //     id = 10,
            //     nextId = 11,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 1,
            //     text = "Nervosität während eines Interviews mit jemandem von der Presse ist verständlich. " +
            //     "Es bringt Aufmerksamkeit für das eigene Unternehmen und auch mögliche Kund*innen."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 11,
            //     nextId = 12,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 2,
            //     text = "Ängstlich zu sein ist in Ordnung. Du weißt nicht, was dein Gegenüber in dem " +
            //     "Artikel schreiben wird und welche Auswirkungen der Artikel haben wird."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 12,
            //     nextId = 13,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 3,
            //     text = "Jemand von der Zeitung ist auf dich aufmerksam geworden und das spornt an. " +
            //     "Der Artikel kann dir mehr Beachtung und Kund*innen bringen."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 13,
            //     nextId = 14,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_OPINION_CHOICE_EVENT),
            //     waitForUserConfirmation = false,
            //     opinionChoiceNumber = 4,
            //     text = "An diesem Punkt des Gesprächs wirkt es, als würde dein Geschlecht für den Artikel " +
            //     "mehr zählen als deine Geschäftsidee. Dies kann durchaus verärgern."
            // },

            // new VisualNovelEvent()
            // {
            //     id = 14,
            //     nextId = 15,
            //     eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ASK_FOR_OPINION_EVENT),
            //     waitForUserConfirmation = true,
            // },
            
            new VisualNovelEvent()
            {
                id = 15,
                nextId = 16,
                onChoice = 18,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das wäre toll. Wann wird der Artikel erscheinen?"
            },

            new VisualNovelEvent()
            {
                id = 16,
                nextId = 17,
                onChoice = getRandomReactionFromReporter(0),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Mir ist es wichtig, dass meine Geschäftsidee in dem Artikel betont wird. " +
                "Ist das machbar?"
            },

            new VisualNovelEvent()
            {
                id = 17,
                nextId = 100,
                onChoice = getRandomReactionFromReporter(1),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Wäre es möglich, dass mein Geschlecht nicht allzu sehr in dem Artikel " +
                "hervorgehoben wird? Ich möchte für mein Unternehmen Aufmerksamkeit bekommen und " +
                "nicht weil ich eine Frau bin."
            },

            new VisualNovelEvent()
            {
                id = 18,
                nextId = 19,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Vielen Dank für die Beantwortung der Frage. " +
                "Der Artikel wird am Sonntag erscheinen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 19,
                nextId = 20,
                onChoice = 21,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte noch weiter ausführen, warum mir das wichtig ist."
            },

            new VisualNovelEvent()
            {
                id = 20,
                nextId = 100,
                onChoice = 24,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte fragen, wieso es Leser*innen eher ansprechen könnte, wenn der Fokus auf das Geschlecht gelegt wird."
            },

            new VisualNovelEvent()
            {
                id = 21,
                nextId = 22,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Es ist mir wichtig, dass der Fokus nicht auf meinem Geschlecht liegt, weil ich möchte, dass meine Gründung und meine Leistungen eigenständig betrachtet werden. " + 
                "Wenn der Artikel sich zu stark auf mein Geschlecht konzentriert, besteht die Gefahr, dass die Aufmerksamkeit von der eigentlichen Geschichte abgelenkt wird - " + 
                "nämlich meiner Geschäftsidee, ihrer Entwicklung und ihrem Einfluss."
            },

            new VisualNovelEvent()
            {
                id = 22,
                nextId = 23,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Ich werde versuchen sicherzustellen, dass wir uns im Artikel auf Ihre Geschäftsidee und Ihre Leistungen konzentrieren, wie von Ihnen gewünscht. " + 
                "Ich kann es Ihnen leider nicht versprechen, da es von der Redaktionsleitung abhängig ist.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 23,
                nextId = 99,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich verstehe, danke für Ihre Bemühung. Ich freue mich auf den Artikel."
            }, 

            new VisualNovelEvent()
            {
                id = 24,
                nextId = 25,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Wieso könnte es Leser*innen eher ansprechen, wenn der Fokus auf das Geschlecht gelegt wird? Können Sie mir das erklären?"
            }, 

            new VisualNovelEvent()
            {
                id = 25,
                nextId = 26,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Geschichten über Frauen, die Unternehmen gründen, werden oft als inspirierend angesehen, " + 
                "weil sie Geschlechterstereotypen herausfordern und Frauen in unterrepräsentierten Branchen oder Positionen hervorheben. " + 
                "Dies kann Leser*innen ansprechen, die nach Vorbildern suchen. Es geht darum, Geschichten zu erzählen, die Menschen ermutigen und motivieren, Geschlechterbarrieren zu überwinden.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 26,
                nextId = 27,
                onChoice = 28,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das verstehe ich und finde das toll."
            }, 

            new VisualNovelEvent()
            {
                id = 27,
                nextId = 100,
                onChoice = 30,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Dennoch möchte ich betonen, dass mein Hauptfokus auf meiner Geschäftsidee und meinen Leistungen liegt."
            }, 

            new VisualNovelEvent()
            {
                id = 28,
                nextId = 29,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Ich denke auch, dass Sie für viele ein Vorbild sein werden und andere Frauen dazu inspirieren werden auch zu gründen!",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 29,
                nextId = 99,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Danke, ich freue mich den Artikel zu lesen."
            }, 

            new VisualNovelEvent()
            {
                id = 30,
                nextId = 29,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Wir werden versuchen, einen ausgewogenen Artikel zu schreiben, der sowohl Ihre Geschäftsidee als auch Ihre Reise als Unternehmerin berücksichtigt.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 31,
                nextId = 19,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Aber natürlich! Ich verstehe, dass die Geschäftsidee im Vordergrund stehen soll. Vielen Dank für den Hinweis. Der Artikel wird nächsten Sonntag erscheinen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 32,
                nextId = 33,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Da kann ich leider nichts versprechen. Ich kann zwar beim Schreiben darauf achten, aber am Ende entscheidet die Redaktionsleitung, " + 
                "wie die Story erscheinen wird. Soll ich etwa nicht erwähnen, dass Sie eine Frau sind?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 33,
                nextId = 34,
                onChoice = 35,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte nur sichergehen, dass die Betonung nicht auf meinem Geschlecht, sondern auf meiner Geschäftsidee liegt."
            }, 

            new VisualNovelEvent()
            {
                id = 34,
                nextId = 100,
                onChoice = 55,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte nicht, dass meine Gründung aufgrund meines Geschlechts sensationell dargestellt wird."
            }, 

            new VisualNovelEvent()
            {
                id = 35,
                nextId = 36,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Wir werden uns auf die wichtigen Aspekte Ihrer Gründungsgeschichte konzentrieren.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 36,
                nextId = 37,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich hoffe, wir können beides so darstellen, dass die Leser*innen einen umfassenden Einblick in meine Unternehmensreise bekommen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 37,
                nextId = 38,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Wie gesagt, ich kann es Ihnen nicht versprechen, aber werde mich darum bemühen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 38,
                nextId = 39,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Dann..."
            }, 

            new VisualNovelEvent()
            {
                id = 39,
                nextId = 40,
                onChoice = 41,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "... bedanke ich mich für Ihre Mühe. Ich freue mich darauf ihn zu lesen."
            }, 

            new VisualNovelEvent()
            {
                id = 40,
                nextId = 100,
                onChoice = 51,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "... wäre es mir doch lieber, wenn Sie ihn nicht schreiben."
            }, 

            new VisualNovelEvent()
            {
                id = 41,
                nextId = 99,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Schön. Vielen Dank für die Beantwortung der Frage. Der Artikel wird am Sonntag erscheinen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 42,
                nextId = 43,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Oh, warum denn nicht?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.QUESTIONING)
            },

            new VisualNovelEvent()
            {
                id = 43,
                nextId = 44,
                onChoice = 45,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte die Gründe nicht nennen."
            }, 

            new VisualNovelEvent()
            {
                id = 44,
                nextId = 100,
                onChoice = 52,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte meine Gründe erklären."
            },

            new VisualNovelEvent()
            {
                id = 45,
                nextId = 46,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich bevorzuge es, keine weiteren Einzelheiten zu nennen oder zu diskutieren."
            },

            new VisualNovelEvent()
            {
                id = 46,
                nextId = 47,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = false,
                name = "Frau Mayer",
                text = "Ach, wie schade! Und ich dachte, Ihre Geschichte könnte interessant sein. " + 
                "Aber wenn Sie sich nicht für die Aufmerksamkeit interessieren, dann ist das eben so.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 47,
                nextId = 48,
                onChoice = 49,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte daraufhin etwas erwidern."
            }, 

            new VisualNovelEvent()
            {
                id = 48,
                nextId = 100,
                onChoice = 54,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich ignoriere das und verabschiede mich."
            }, 

            new VisualNovelEvent()
            {
                id = 49,
                nextId = 50,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich schätze das Interesse an meiner Geschichte. Es geht darum, dass ich sicherstellen möchte, " + 
                "dass meine Gründung und meine Arbeit im richtigen Licht dargestellt werden, ohne sensationalisiert zu werden. " + 
                "Da dies anscheinend bei Ihnen nicht möglich ist, möchte ich das Gespräch an dieser Stelle beenden. Auf Wiedersehen."
            },

            new VisualNovelEvent()
            {
                id = 50,
                nextId = 99,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea", // TODO: chnage to narrator when implemented
                text = "Du verlässt den Raum."
            },

            new VisualNovelEvent()
            {
                id = 51,
                nextId = 43,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Oh, warum denn nicht?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 52,
                nextId = 53,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich habe Bedenken, dass der Artikel in eine Richtung gehen könnte, die mir unangenehm ist. " + 
                "Es ist wichtig für mich, dass meine Geschäftsidee und meine Erfolge im Vordergrund stehen, und ich befürchte, " + 
                "dass der Artikel einen anderen Fokus haben könnte."
            },

            new VisualNovelEvent()
            {
                id = 53,
                nextId = 99,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Ach so. Ich verstehe, vielen Dank für das Gespräch. Dann verbleiben wir einfach so. " + 
                "Ich wünsche Ihnen noch einen schönen Tag. Auf Wiedersehen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.NONE)
            },

            new VisualNovelEvent()
            {
                id = 54,
                nextId = 99,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Vielen Dank für Ihr Verständnis. Ich habe meine Entscheidung getroffen. Auf Wiedersehen."
            },

            new VisualNovelEvent()
            {
                id = 55,
                nextId = 56,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Die Tatsache, dass Sie eine Frau sind, macht Ihre Geschichte doch viel interessanter. Die Leser*innen lieben solche Geschichten!",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.ASTONISHED)
            },

            new VisualNovelEvent()
            {
                id = 56,
                nextId = 57,
                onChoice = 58,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das kann ich nachvollziehen, möchte aber ein Gleichgewicht finden.",
                show = false
            }, 

            new VisualNovelEvent()
            {
                id = 57,
                nextId = 100,
                onChoice = 61,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja, solche Geschichten erregen oft Aufmerksamkeit. Es geht mir aber mehr um das, was ich aufgebaut habe."
            }, 

            new VisualNovelEvent()
            {
                id = 58,
                nextId = Random.Range(59, 61),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich verstehe, dass solche Geschichten oft ansprechend sind, aber mein Hauptanliegen ist immer noch meine Geschäftsidee. " + 
                "Wenn wir das Gleichgewicht finden können, wäre das großartig."
            },

            new VisualNovelEvent()
            {
                id = 59,
                nextId = 19,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Vielen Dank für Ihr Verständnis. Ich schätze Ihre Geschäftsidee wirklich und werde mein Bestes tun, " + 
                "um eine ausgewogene Darstellung zu bieten, die sowohl Ihre Gründungsgeschichte als auch Ihre Geschäftsidee hervorhebt.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.FRIENDLY)
            },

            new VisualNovelEvent()
            {
                id = 60,
                nextId = 19,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Nun ja, ich denke, wir werden sehen, wie es läuft. Ich werde versuchen, einen Kompromiss zu finden, " + 
                "aber seien Sie nicht überrascht, wenn das Geschlecht doch im Mittelpunkt steht.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 61,
                nextId = Random.Range(62, 64),
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ja, ich weiß, dass Geschichten über Frauen, die Unternehmen gründen, Aufmerksamkeit erregen. " + 
                "Aber das bedeutet nicht, dass es mir gefällt, wenn der Fokus auf mich als Frau liegt und das sensationell dargestellt wird. " + 
                "Es geht mir mehr um das, was ich aufgebaut habe."
            }, 

            new VisualNovelEvent()
            {
                id = 62,
                nextId = 19,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Vielen Dank, dass Sie Ihre Perspektive teilen. Es ist wichtig, Ihre Bedenken zu respektieren und ich werde das an die Redaktionsleitung weitergeben.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 63,
                nextId = 64,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Ach, nutzen Sie es doch aus und zeigen Sie ihren weiblichen Charme. So könnten Sie viel mehr Aufmerksamkeit erhalten!",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 64,
                nextId = 65,
                onChoice = 66,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte nicht weiter darauf eingehen."
            },

            new VisualNovelEvent()
            {
                id = 65,
                nextId = 100,
                onChoice = 69,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte erklären, warum ich das nicht möchte."
            },

            new VisualNovelEvent()
            {
                id = 66,
                nextId = 67,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Okay. Was den Artikel betrifft,..."
            }, 

            new VisualNovelEvent()
            {
                id = 67,
                nextId = 68,
                onChoice = 41,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "... freue ich mich darauf ihn zu lesen."
            },

            new VisualNovelEvent()
            {
                id = 68,
                nextId = 100,
                onChoice = 42,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "... wäre es mir doch lieber, wenn Sie ihn nicht schreiben."
            },

            new VisualNovelEvent()
            {
                id = 69,
                nextId = 66,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich bin der Meinung, dass meine Arbeit und meine Ideen ausreichen sollten, um Anerkennung zu erhalten. " + 
                "Ich möchte nicht, dass es in die Richtung geht, dass ich erfolgreich bin 'für eine Frau'."
            },

            new VisualNovelEvent()
            {
                id = 70,
                nextId = 71,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Das wird allerdings nicht so gut bei den Lesenden ankommen. Ich denke unter " +
                "diesen Umständen wird sich die Redaktion für eine andere Story entscheiden.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 71,
                nextId = 72,
                onChoice = 73,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich verstehe es, wenn die Redaktion andere Prioritäten hat."
            },

            new VisualNovelEvent()
            {
                id = 72,
                nextId = 100,
                onChoice = 88,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Das ist wirklich enttäuschend zu hören."
            },

            new VisualNovelEvent()
            {
                id = 73,
                nextId = 74,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Verstehe, wenn die Redaktion andere Prioritäten hat. Dennoch ist es mir wichtig, meine Geschäftsidee " + 
                "und meine Leistungen angemessen darzustellen. Wenn sich die Gelegenheit ergibt, hoffe ich, dass wir die " + 
                "Geschichte in der gewünschten Weise erzählen können."
            },

            new VisualNovelEvent()
            {
                id = 74,
                nextId = 75,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Sind Sie sich sicher, dass Sie auf einen Artikel verzichten möchten?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.REFUSING)
            },

            new VisualNovelEvent()
            {
                id = 75,
                nextId = 76,
                onChoice = 77,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ja, wenn der Fokus nicht auf meinem Unternehmen liegt, verzichte ich lieber."
            },

            new VisualNovelEvent()
            {
                id = 76,
                nextId = 100,
                onChoice = 80,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Hm, ich fände es doch besser, wenn ein Artikel erscheinen würde."
            },

            new VisualNovelEvent()
            {
                id = 77,
                nextId = 78,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Wie können Sie nur keinen Artikel wollen?",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.ASTONISHED)
            },

            new VisualNovelEvent()
            {
                id = 78,
                nextId = 79,
                onChoice = 45,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte keine weiteren Gründe nennen."
            },

            new VisualNovelEvent()
            {
                id = 79,
                nextId = 100,
                onChoice = 52,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte meine Gründe erklären."
            },

            new VisualNovelEvent()
            {
                id = 80,
                nextId = 81,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Schön, dass Sie sich umentschieden haben. Wir werden unser Bestes tun, um eine inspirierende Geschichte über Ihr Unternehmen zu erzählen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 81,
                nextId = 82,
                onChoice = 83,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte nochmal nachfragen, ob der Fokus nicht doch auf das Unternehmen an sich gelegt werden kann."
            },

            new VisualNovelEvent()
            {
                id = 82,
                nextId = 100,
                onChoice = 29,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte mich für den Artikel bedanken und das Gespräch beenden."
            },

            new VisualNovelEvent()
            {
                id = 83,
                nextId = 84,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Können Sie nicht trotzdem versuchen, darauf zu achten, dass der Fokus auf das Unternehmen an sich liegt?"
            },

            new VisualNovelEvent()
            {
                id = 84,
                nextId = 85,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Wie gesagt, das ist von der Redaktion abhängig, aber so würde es weniger Leser*innen ansprechen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 85,
                nextId = 86,
                onChoice = 24,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte fragen, wieso es Leser*innen eher ansprechen könnte, wenn der Fokus auf das Geschlecht gelegt wird."
            },

            new VisualNovelEvent()
            {
                id = 86,
                nextId = 100,
                onChoice = 87,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte nicht mehr weiter darauf eingehen und das Gespräch beenden."
            },

            new VisualNovelEvent()
            {
                id = 87,
                nextId = 41,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Okay, ich verstehe. Dann bin ich gespannt auf den Artikel."
            },

            new VisualNovelEvent()
            {
                id = 88,
                nextId = 89,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich verstehe Ihre Perspektive, aber ich bleibe bei meinem Standpunkt. Wenn die Redaktion eine andere Story bevorzugt, " + 
                "ist das schade, aber ich werde meine Prinzipien nicht ändern. Mein Fokus liegt weiterhin auf der angemessenen Darstellung " + 
                "meiner Gründung und meiner Arbeit."
            },

            new VisualNovelEvent()
            {
                id = 89,
                nextId = 90,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Ich finde es bedauerlich, dass Sie nicht bereit sind, in dieser Angelegenheit flexibler zu sein. " + 
                "Geschichten über Menschen, die in gewisser Weise herausragen, sind oft ansprechender für unsere Leser*innen.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 90,
                nextId = 91,
                onChoice = 24,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte fragen, wieso es Leser*innen eher ansprechen könnte, wenn der Fokus auf das Geschlecht gelegt wird."
            },

            new VisualNovelEvent()
            {
                id = 91,
                nextId = 100,
                onChoice = 92,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte deren Fokus auf mein Geschlecht kritisieren.",
                show = false
            },

            new VisualNovelEvent()
            {
                id = 92,
                nextId = 93,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Es ist bedauerlicher, dass Sie glauben, dass Geschichten über Menschen, die herausragen, " + 
                "sich zwangsläufig auf Stereotypen oder Sensationalismus stützen müssen. "
            },

            new VisualNovelEvent()
            {
                id = 93,
                nextId = 94,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Ihre Geschäftsidee ist sicherlich beeindruckend, aber lassen Sie uns ehrlich sein, " + 
                "die Leser*innen werden sich eher für Ihre äußere Erscheinung und Ihren Charme interessieren. Das ist nun mal die Realität.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 94,
                nextId = 95,
                onChoice = 49,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte das Gespräch beenden."
            },

            new VisualNovelEvent()
            {
                id = 95,
                nextId = 100,
                onChoice = 96,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.ADD_CHOICE_EVENT),
                waitForUserConfirmation = false,
                name = "Lea",
                text = "Ich möchte auf diesen Kommentar eingehen."
            },

            new VisualNovelEvent()
            {
                id = 96,
                nextId = 97,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Als Unternehmerin ist es meine Überzeugung, dass meine Arbeit und meine Ideen die Anerkennung verdienen sollten, " + 
                "und ich werde mich nicht damit abfinden, auf mein Äußeres oder meinen Charme reduziert zu werden. " + 
                "Außerdem sehe ich die Rolle von Journalist*innen darin, eine ausgewogene und sachliche Darstellung von Ereignissen und " + 
                "Geschichten zu bieten, ohne Stereotypen zu verstärken."
            },

            new VisualNovelEvent()
            {
                id = 97,
                nextId = 98,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Frau Mayer",
                text = "Sie wissen anscheinend nicht so viel über Journalismushaben. Ich habe aber nun nicht die Zeit, um Sie aufzuklären.",
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.CRITICAL)
            },

            new VisualNovelEvent()
            {
                id = 98,
                nextId = 50,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_MESSAGE_EVENT),
                waitForUserConfirmation = true,
                name = "Lea",
                text = "Ich sehe, dass wir in dieser Angelegenheit keine Übereinstimmung finden werden. " + 
                "Ich denke, es ist am besten, wenn wir das Gespräch hier beenden.Auf Wiedersehen."
            },

            new VisualNovelEvent()
            {
                id = 99,
                nextId = 101,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.CHARAKTER_EXIT_EVENT),
                waitForUserConfirmation = false,
                name = "Frau Mayer",
                animationType = AnimationTypeHelper.ToInt(AnimationType.FLY_IN_FROM_ABOVE),
                expressionType = ExpressionTypeHelper.ToInt(ExpressionType.SMILING)
            },

            new VisualNovelEvent()
            {
                id = 101,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.END_NOVEL_EVENT),
                waitForUserConfirmation = true
            },

            new VisualNovelEvent()
            {
                id = 100,
                eventType = VisualNovelEventTypeHelper.ToInt(VisualNovelEventType.SHOW_CHOICES_EVENT),
                waitForUserConfirmation = true
            }
        };
    }
    private int getRandomReactionFromReporter(int optionOneOrTwo){
            int value = 0;
            if(optionOneOrTwo == 0){
                value = Random.Range(0, 10);
                if(value < 5){
                    return 31;
                } else if(value > 8){
                    return 32;
                } else {
                    return 70;
                }
            } else {
                value = Random.Range(0, 10);
                if(value < 2){
                    return 31;
                } else if(value > 6){
                    return 32;
                } else {
                    return 70;
                }
            }
        }
}