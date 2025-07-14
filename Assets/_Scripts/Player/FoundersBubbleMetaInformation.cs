using Assets._Scripts.Novel;
using UnityEngine;

namespace Assets._Scripts.Player
{
    public class FoundersBubbleMetaInformation : MonoBehaviour
    {
        internal static string GetDisplayNameOfNovelToPlay(VisualNovelNames value)
        {
            return value switch
            {
                VisualNovelNames.ElternNovel => "Eltern",
                VisualNovelNames.PresseNovel => "Presse",
                VisualNovelNames.NotariatNovel => "Notarin",
                VisualNovelNames.VermieterNovel => "Vermieter",
                VisualNovelNames.InvestorNovel => "Investor",
                VisualNovelNames.BankKreditNovel => "Bankkredit",
                VisualNovelNames.HonorarNovel => "Honorar",
                VisualNovelNames.EinstiegsNovel => "Einstieg",
                VisualNovelNames.VertriebNovel => "Vertrieb",
                _ => ""
            };
        }

        internal static int GetIndexOfNovel(VisualNovelNames value)
        {
            return value switch
            {
                VisualNovelNames.ElternNovel => 2,
                VisualNovelNames.HonorarNovel => 3,
                VisualNovelNames.InvestorNovel => 4,
                VisualNovelNames.BankKreditNovel => 5,
                VisualNovelNames.NotariatNovel => 6,
                VisualNovelNames.PresseNovel => 7,
                VisualNovelNames.VermieterNovel => 8,
                VisualNovelNames.EinstiegsNovel => 9,
                VisualNovelNames.VertriebNovel => 10,
                _ => -1
            };
        }
    }
}