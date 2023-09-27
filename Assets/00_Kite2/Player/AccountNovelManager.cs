using System.Collections.Generic;

public class AccountNovelManager
{
    private static AccountNovelManager instance;

    private List<VisualNovel> novels = new List<VisualNovel>();

    private AccountNovelManager()
    {
        novels = new List<VisualNovel>
        {
            new ConversationWithAcquaintancesNovel() { id = 0},
            new BankAppointmentNovel(),
            new FeeNegotiationNovel() { id = 0},
        };
    }

    public static AccountNovelManager Instance()
    {
        if (instance == null)
        {
            instance = new AccountNovelManager();
        }
        return instance;
    }

    public List<VisualNovel> GetAllAccountNovels()
    {
        return novels;
    }
}
