using Assets._Scripts.Utilities;
using UnityEngine;

namespace Assets._Scripts.Player
{
    public class FoundersBubbleMetaInformation : MonoBehaviour
    {
        public static int numberOfNovelsToDisplay = 8;

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
                VisualNovelNames.EinstiegsNovel => true,
                _ => false
            };
        }

        internal static Color GetColorOfNovel(VisualNovelNames value)
        {
            return value switch
            {
                VisualNovelNames.ElternNovel => NovelColours.GetNovelColour(NovelColourPicker.Green),
                VisualNovelNames.PresseNovel => NovelColours.GetNovelColour(NovelColourPicker.Violet),
                VisualNovelNames.NotariatNovel => NovelColours.GetNovelColour(NovelColourPicker.Turquoise),
                VisualNovelNames.VermieterNovel => NovelColours.GetNovelColour(NovelColourPicker.DarkBlue),
                VisualNovelNames.InvestorNovel => NovelColours.GetNovelColour(NovelColourPicker.GreenBrown),
                VisualNovelNames.BankKreditNovel => NovelColours.GetNovelColour(NovelColourPicker.DarkBlue),
                VisualNovelNames.HonorarNovel => NovelColours.GetNovelColour(NovelColourPicker.Turquoise2),
                VisualNovelNames.EinstiegsNovel => NovelColours.GetNovelColour(NovelColourPicker.Violet),
                VisualNovelNames.None => NovelColours.GetNovelColour(NovelColourPicker.Default),
                _ => NovelColours.GetNovelColour(NovelColourPicker.Default)
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
                _ => -1
            };
        }
    }
}