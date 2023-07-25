using System.Collections.Generic;

public class CallWithParentsNovel : VisualNovel
{
    public CallWithParentsNovel()
    {
        title = "Telefonat mit den Eltern";
        description = "Du beschließt, deine Eltern anzurufen, und ihnen von deinem Gründungsvorhaben zu berichten.";
        image = 1;
        nameOfMainCharacter = "Lea";
        feedback = "";
        novelEvents = new List<VisualNovelEvent>()
        {
        };
    }
}
