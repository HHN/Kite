using System.Collections.Generic;

public class AccountNovelManager
{
    private static List<VisualNovel> novels = new List<VisualNovel>();

    static AccountNovelManager()
    {
        novels = new List<VisualNovel>
        {
            new ConversationWithAcquaintancesNovel() { id = 0},
            new BankAppointmentNovel(),
            new FeeNegotiationNovel() { id = 0},
        };
    }

    public static List<VisualNovel> GetAllAccountNovels()
    {
        return novels;
    }
}
