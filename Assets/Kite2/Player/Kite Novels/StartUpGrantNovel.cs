using System.Collections.Generic;

public class StartUpGrantNovel : VisualNovel
{
    public StartUpGrantNovel()
    {
        title = "Gründerzuschuss";
        description = "Du bist heute bei deinem örtlichen Arbeitsamt, um einen Gründerzuschuss zu beantragen.";
        image = 6;
        nameOfMainCharacter = "Lea";
        feedback = "";
        novelEvents = new List<VisualNovelEvent>()
        {
        };
    }
}