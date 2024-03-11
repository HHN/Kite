using System.Collections.Generic;
using UnityEngine;

public class KiteNovelManager : MonoBehaviour
{
    private static Dictionary<long, VisualNovel> novels = new Dictionary<long, VisualNovel>();

    static KiteNovelManager()
    {
        //already modified novels
        novels[2] = new CallWithParentsNovel();
        novels[3] = new PressTalkNovel();
        novels[4] = new CallWithNotaryNovel();
        novels[7] = new InitialInterviewForGrantApplicationNovel();
        novels[6] = new RentingAnOfficeNovel();
        novels[5] = new BankAccountOpeningNovel();

        // not modified novels
        novels[1] = new BankTalkNovel();
        novels[8] = new StartUpGrantNovel();
        novels[9] = new ConversationWithAcquaintancesNovel();
        novels[10] = new BankAppointmentNovel();
        novels[11] = new FeeNegotiationNovel();
        novels[12] = new FreeTextAndGptDemo();
        novels[13] = new GPTInterview();
    }

    public static VisualNovel GetKiteNovelById(long id)
    {
        if (novels.ContainsKey(id))
        {
            return novels[id];
        }
        return null;
    }

    public static List<VisualNovel> GetAllKiteNovels()
    {
        return new List<VisualNovel>(novels.Values);
    }
}
