using System.Collections.Generic;

public class FeeNegotiationNovel : VisualNovel
{
    public FeeNegotiationNovel()
    {
        title = "Honrarverhandlung mit Kundin";
        description = "Du bist in Kontakt mit einem Kunden gekommen. Nachdem geklärt wurde, dass deine angebotenen Leistungen zu den Anforderungen deines Gegenübers passen, geht es nun um die Honorarverhandlung.";
        image = 6;
        nameOfMainCharacter = "Lea";
        feedback = "";
        novelEvents = new List<VisualNovelEvent>()
        {
        };
    }
}