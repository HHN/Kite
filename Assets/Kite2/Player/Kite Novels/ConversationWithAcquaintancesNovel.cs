using System.Collections.Generic;

public class ConversationWithAcquaintancesNovel : VisualNovel
{
    public ConversationWithAcquaintancesNovel()
    {
        title = "Gespräch mit Bekannten";
        description = "Du triffst dich mit einem*einer Bekannten, den*die du seit ein paar Jahren nicht mehr gesehen hast, in einem kleinen Café. Er*Sie hat dir soeben erzählt, wo er*sie zurzeit beruflich steht. Nun bist du an der Reihe.";
        image = 7;
        nameOfMainCharacter = "Lea";
        feedback = "";
        novelEvents = new List<VisualNovelEvent>()
        {
        };
    }
}