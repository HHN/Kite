using System;

[Serializable]
public class NovelPostingRequest
{
    public string title;
    public string description;
    public long environmentWallID;
    public long environmentFloorID;
    public long environmentWindowID;
    public long environmentFurnitureID;
    public long playerCharacterFaceID;
    public long playerCharacterHairID;
    public long playerCharacterBodyID;
    public long playerCharacterClothsID;
    public long opponentCharacterFaceID;
    public long opponentCharacterHairID;
    public long opponentCharacterBodyID;
    public long opponentCharacterClothsID;
    public string dialog;
}
