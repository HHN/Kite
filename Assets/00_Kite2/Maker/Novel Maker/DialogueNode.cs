using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNode
{
    private int id;
    private string nodeName;
    private DialogueNodeType nodeType;

    private LocationEnum goToEnvironment;

    private CharacterEnum characterComes;
    private DirectionEnum characterComesDirection;
    private EmotionEnum emotionWhileComming;

    private CharacterEnum characterTalkes;
    private string textToTalk;
    private EmotionEnum emotionWhileTalking;

    private void Start()
    {
        this.nodeName = string.Empty;
        this.nodeType = DialogueNodeType.NONE;
        this.goToEnvironment = LocationEnum.NONE;
        this.characterComes = CharacterEnum.NONE;
        this.characterComesDirection = DirectionEnum.NONE;
        this.emotionWhileComming = EmotionEnum.NONE;
        this.characterTalkes = CharacterEnum.NONE;
        this.textToTalk = string.Empty;
        this.emotionWhileTalking = EmotionEnum.NONE;
    }

    public void SetNodeName(string nodeName)
    {
        this.nodeName = nodeName;
    }

    public string GetNodeName() 
    { 
        return this.nodeName; 
    }

    public void SetNodeType(DialogueNodeType nodeType)
    {
        this.nodeType = nodeType;
    }

    public DialogueNodeType GetNodeType()
    {
        return this.nodeType;
    }

    public void SetGoToEnvironment(LocationEnum goToEnvironment)
    {
        this.goToEnvironment = goToEnvironment;
    }

    public LocationEnum GetGoToEnvironment()
    {
        return this.goToEnvironment;
    }

    public void SetCharacterComes(CharacterEnum characterComes)
    {
        this.characterComes = characterComes;
    }

    public CharacterEnum GetCharacterComes()
    {
        return this.characterComes;
    }

    public void SetCharacterComesDirection(DirectionEnum characterComesDirection)
    {
        this.characterComesDirection = characterComesDirection;
    }

    public DirectionEnum GetCharacterComesDirection()
    {
        return this.characterComesDirection;
    }

    public void SetEmotionWhileComming(EmotionEnum emotionWhileComming)
    {
        this.emotionWhileComming = emotionWhileComming;
    }

    public EmotionEnum GetEmotionWhileComming()
    {
        return this.emotionWhileComming;
    }

    public void SetCharacterTalkes(CharacterEnum characterTalkes)
    {
        this.characterTalkes = characterTalkes;
    }

    public CharacterEnum GetCharacterTalkes()
    {
        return this.characterTalkes;
    }

    public void SetTextToTalk(string textToTalk)
    {
        this.textToTalk = textToTalk;
    }

    public string GetTextToTalk()
    {
        return this.textToTalk;
    }

    public void SetEmotionWhileTalking(EmotionEnum emotionWhileTalking)
    {
        this.emotionWhileTalking = emotionWhileTalking;
    }

    public EmotionEnum GetEmotionWhileTalking()
    {
        return this.emotionWhileTalking;
    }

    public void SetID(int id)
    {
        this.id = id;
    }

    public int GetId()
    {
        return this.id;
    }
}
