using System.Collections.Generic;

public class RentingAnOfficeNovel : VisualNovel
{
    public RentingAnOfficeNovel()
    {
        title = "Anmietung eines Büros";
        description = "Du hast heute einen Termin für die Besichtigung von Büroräumen.";
        image = 4;
        nameOfMainCharacter = "Lea";
        feedback = "";
        novelEvents = new List<VisualNovelEvent>()
        {
        };
    }
}