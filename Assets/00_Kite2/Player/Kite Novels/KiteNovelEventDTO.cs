public class KiteNovelEventDTO
{
    private VisualNovelEventType eventType;
    private Location location;
    private Character character;
    private CharacterExpression emotionOfCharacter;
    private string dialogMessage;
    private KiteSound sound;
    private KiteAnimation animation;
    private string question;
    private string variableName;
    private string prompt;
    private CompletionHandler completionHandler;
    private string key;
    private string value;
    private DiscriminationBias relevantBias;

    public KiteNovelEventDTO()
    {
        EventType = VisualNovelEventType.NONE;
        Ort = Location.NONE;
        Character = Character.NONE;
        EmotionOfCharacter = CharacterExpression.NONE;
        DialogMessage = "";
        Sound = KiteSound.NONE;
        Animation = KiteAnimation.NONE;
        Question = "";
        VariableName = "";
        Prompt = "";
        CompletionHandler = CompletionHandler.NONE;
        Key = "";
        Value = "";
        RelevantBias = DiscriminationBias.NONE;
    }

    public VisualNovelEventType EventType
    {
        get { return eventType; }
        set { eventType = value; }
    }

    public Location Ort
    {
        get { return location; }
        set { location = value; }
    }

    public Character Character
    {
        get { return character; }
        set { character = value; }
    }

    public CharacterExpression EmotionOfCharacter
    {
        get { return emotionOfCharacter; }
        set { emotionOfCharacter = value; }
    }

    public string DialogMessage
    {
        get { return dialogMessage; }
        set { dialogMessage = value; }
    }

    public KiteSound Sound
    {
        get { return sound; }
        set { sound = value; }
    }

    public KiteAnimation Animation
    {
        get { return animation; }
        set { animation = value; }
    }

    public string Question
    {
        get { return question; }
        set { question = value; }
    }

    public string VariableName
    {
        get { return variableName; }
        set { variableName = value; }
    }

    public string Prompt
    {
        get { return prompt; }
        set { prompt = value; }
    }

    public CompletionHandler CompletionHandler
    {
        get { return completionHandler; }
        set { completionHandler = value; }
    }

    public string Key
    {
        get { return key; }
        set { key = value; }
    }

    public string Value
    {
        get { return value; }
        set { this.value = value; }
    }

    public DiscriminationBias RelevantBias
    {
        get { return relevantBias; }
        set { relevantBias = value; }
    }
}
