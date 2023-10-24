using System.Collections.Generic;

public class AccountNovelManager
{
    private static AccountNovelManager instance;

    private List<VisualNovel> novels = new List<VisualNovel>();

    private AccountNovelManager()
    {
        novels = new List<VisualNovel>();
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
