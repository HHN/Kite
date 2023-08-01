using System;

[Serializable]
public class VisualNovelEvent
{
    public int id;
    public int nextId;
    public int onChoice;
    public int eventType;
    public bool waitForUserConfirmation;
    public int imageId;
    public string name;
    public string text;
    public int animationType;
    public int expressionType;
    public int xPosition;
    public int yPosition;
    public int opinionChoiceNumber; // 1 -> Nervous; 2 -> Fearfull; 3 -> Encouraged; 4 -> Annoyed;
}
