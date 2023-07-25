using System.Collections.Generic;

public class BankAccountOpeningNovel : VisualNovel
{
    public BankAccountOpeningNovel()
    {
        title = "Bank Kontoeröffnung";
        description = "Du hast eine Einladung zu einem Bankgespräch erhalten, um mehr Informationen über die Eröffnung eines Bankkontos zu erhalten und dieses darauf zu beantragen.";
        image = 0;
        nameOfMainCharacter = "Lea";
        feedback = "";
        novelEvents = new List<VisualNovelEvent>()
        {
        };
    }
}