using System;
using UnityEngine.Serialization;

namespace _00_Kite2.Common.Novel
{
    [Serializable]
    public class VisualNovelEvent
    {
        public string id;
        public string nextId;
        public string onChoice;
        public int eventType;
        public bool waitForUserConfirmation;
        public int skinSpriteId;
        public int clotheSpriteId;
        public int hairSpriteId;
        public int faceSpriteId;
        public int backgroundSpriteId;
        public int character;
        public string text;
        public int animationType;
        public int expressionType;
        public int xPosition;
        public int yPosition;
        public int opinionChoiceNumber; // 1 -> Nervous; 2 -> Fearful; 3 -> Encouraged; 4 -> Annoyed;
        public int audioClipToPlay;
        public int animationToPlay;
        public bool show = true;
        public string questionForFreeTextInput;
        public string variablesName;
        public string gptPrompt;
        public string variablesNameForGptPrompt;
        public int gptCompletionHandlerId;
        public string key;
        public string value;
        public int relevantBias;

        public VisualNovelEvent DeepCopy()
        {
            VisualNovelEvent newEvent = new VisualNovelEvent();

            newEvent.id = id;
            newEvent.nextId = nextId;
            newEvent.onChoice = onChoice;
            newEvent.eventType = eventType;
            newEvent.waitForUserConfirmation = waitForUserConfirmation;
            newEvent.skinSpriteId = skinSpriteId;
            newEvent.clotheSpriteId = clotheSpriteId;
            newEvent.hairSpriteId = hairSpriteId;
            newEvent.faceSpriteId = faceSpriteId;
            newEvent.backgroundSpriteId = backgroundSpriteId;
            newEvent.character = character;
            newEvent.text = text;
            newEvent.animationType = animationType;
            newEvent.expressionType = expressionType;
            newEvent.xPosition = xPosition;
            newEvent.yPosition = yPosition;
            newEvent.opinionChoiceNumber = opinionChoiceNumber;
            newEvent.audioClipToPlay = audioClipToPlay;
            newEvent.animationToPlay = animationToPlay;
            newEvent.show = show;
            newEvent.questionForFreeTextInput = questionForFreeTextInput;
            newEvent.variablesName = variablesName;
            newEvent.gptPrompt = gptPrompt;
            newEvent.variablesNameForGptPrompt = variablesNameForGptPrompt;
            newEvent.gptCompletionHandlerId = gptCompletionHandlerId;
            newEvent.key = key;
            newEvent.value = value;
            newEvent.relevantBias = relevantBias;

            return newEvent;
        }
    }
}
