using System.Collections.Generic;

public class BankAppointmentNovel : VisualNovel
{
    public BankAppointmentNovel()
    {
        title = "Banktermin zur Kreditvergabe";
        description = "Du hast eine Einladung zu einem Bankgespräch erhalten, um mehr Informationen über einen Kredit zu erhalten und diesen darauf zu beantragen.";
        image = 0;
        nameOfMainCharacter = "Lea";
        feedback = "";
        novelEvents = new List<VisualNovelEvent>()
        {
        };
    }
}