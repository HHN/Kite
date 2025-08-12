using System;

namespace Assets._Scripts.Novel
{
    /// <summary>
    /// Represents a single event or step within a visual novel's narrative.
    /// This class holds all data relevant to a specific moment in the story,
    /// such as character actions, dialogue, background changes, and interactive elements.
    /// Marked <see cref="Serializable"/> to allow for easy saving and loading of novel data.
    /// </summary>
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
        public string backgroundSprite;
        public int character;
        public string text;
        public int animationType;
        public int expressionType;
        public int xPosition;
        public int yPosition;
        public int opinionChoiceNumber;
        public string audioClipToPlay;
        public string animationToPlay;
        public bool show = true;
        public string questionForFreeTextInput;
        public string variablesName;
        public string gptPrompt;
        public string variablesNameForGptPrompt;
        public string gptCompletionHandler;
        public string key;
        public string value;
        public string relevantBias;

        /// <summary>
        /// Creates a deep copy of the current <see cref="VisualNovelEvent"/> instance.
        /// All fields are copied by value, as they are either primitive types or strings (which are immutable).
        /// </summary>
        /// <returns>A new <see cref="VisualNovelEvent"/> instance that is a deep copy of the original.</returns>
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
            newEvent.gptCompletionHandler = gptCompletionHandler;
            newEvent.key = key;
            newEvent.value = value;
            newEvent.relevantBias = relevantBias;

            return newEvent;
        }
    }
}