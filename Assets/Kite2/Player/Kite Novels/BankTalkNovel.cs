using System.Collections.Generic;

public class BankTalkNovel : VisualNovel
{
    public BankTalkNovel()
    {
        title = "Bankgespräch";
        description = "Du hast eine Einladung zu einem Bankgespräch erhalten, um mehr Informationen über einen Kredit zu erhalten und diesen darauf zu beantragen.";
        image = 0;
        nameOfMainCharacter = "Lea";
        feedback = "";
        novelEvents = new List<VisualNovelEvent>()
        {
        };
    }
}