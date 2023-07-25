using System.Collections.Generic;

public class CallWithNotaryNovel : VisualNovel
{
    public CallWithNotaryNovel()
    {
        title = "Telefonat mit dem Notar";
        description = "Du hast ein Telefonat mit einem*einer Notar*in, um einen Termin für deine Gründung auszumachen.";
        image = 3;
        nameOfMainCharacter = "Lea";
        feedback = "";
        novelEvents = new List<VisualNovelEvent>()
        {
        };
    }
}