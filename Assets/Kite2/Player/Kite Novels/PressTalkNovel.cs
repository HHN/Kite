using System.Collections.Generic;

public class PressTalkNovel : VisualNovel
{
    public PressTalkNovel()
    {
        title = "Pressegespräch";
        description = "Du befindest dich auf einer Veranstaltung, bei der Jungunternehmer*innen ihre Geschäftsidee vor einem Publikum präsentieren können, um Rückmeldung zu der Idee zu erhalten und zu networken. Nachdem du  deine Geschäftsidee vor dem Publikum gepitcht hast, stellst du dich an einen Tisch mit anderen Gästen, um mit ihnen zu reden.";
        image = 2;
        nameOfMainCharacter = "Lea";
        feedback = "";
        novelEvents = new List<VisualNovelEvent>()
        {
        };
    }
}