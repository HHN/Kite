using Assets._Scripts.Novel;
using UnityEngine;

namespace Assets._Scripts.Player
{
    public class FoundersBubbleMetaInformation : MonoBehaviour
    {
        public const int NumberOfNovelsToDisplay = 8;

        public static bool IsHighInGui(VisualNovelNames value)
        {
            return value switch
            {
                VisualNovelNames.ElternNovel => false,
                VisualNovelNames.PresseNovel => false,
                VisualNovelNames.NotariatNovel => true,
                VisualNovelNames.VermieterNovel => true,
                VisualNovelNames.InvestorNovel => false,
                VisualNovelNames.BankKreditNovel => true,
                VisualNovelNames.HonorarNovel => false,
                VisualNovelNames.EinstiegsNovel => false,
                _ => false
            };
        }

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
                VisualNovelNames.PresseNovel => 4,
                VisualNovelNames.NotariatNovel => 3,
                VisualNovelNames.VermieterNovel => 5,
                VisualNovelNames.InvestorNovel => 6,
                VisualNovelNames.BankKreditNovel => 1,
                VisualNovelNames.HonorarNovel => 8,
                VisualNovelNames.EinstiegsNovel => 7,
                VisualNovelNames.VertriebNovel => 9,
                _ => -1
            };
        }
    }
}