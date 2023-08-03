using System;
using System.Collections.Generic;

[Serializable]
public class VisualNovel
{
    public string title;
    public string description;
    public long image;
    public string nameOfMainCharacter;
    public string feedback;
    public List<VisualNovelEvent> novelEvents;
    public long id;
}
