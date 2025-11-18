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
            VisualNovelEvent newEvent = new VisualNovelEvent
            {
                id = id,
                nextId = nextId,
                onChoice = onChoice,
                eventType = eventType,
                waitForUserConfirmation = waitForUserConfirmation,
                skinSpriteId = skinSpriteId,
                clotheSpriteId = clotheSpriteId,
                hairSpriteId = hairSpriteId,
                faceSpriteId = faceSpriteId,
                character = character,
                text = text,
                animationType = animationType,
                expressionType = expressionType,
                xPosition = xPosition,
                yPosition = yPosition,
                opinionChoiceNumber = opinionChoiceNumber,
                audioClipToPlay = audioClipToPlay,
                animationToPlay = animationToPlay,
                show = show,
                questionForFreeTextInput = questionForFreeTextInput,
                variablesName = variablesName,
                gptPrompt = gptPrompt,
                variablesNameForGptPrompt = variablesNameForGptPrompt,
                gptCompletionHandler = gptCompletionHandler,
                key = key,
                value = value,
                relevantBias = relevantBias
            };

            return newEvent;
        }
    }
}