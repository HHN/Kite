using Assets._Scripts.Novel;
using UnityEngine;

namespace Assets._Scripts.Player
{
    /// <summary>
    /// Provides utility methods for retrieving meta-information related to visual novels,
    /// primarily for displaying user-friendly names based on internal enum values.
    /// </summary>
    public class FoundersBubbleMetaInformation : MonoBehaviour
    {
        /// <summary>
        /// Retrieves the display name (a user-friendly string) for a given visual novel enum.
        /// This method maps <see cref="VisualNovelNames"/> enum values to their corresponding
        /// German string representations.
        /// </summary>
        /// <param name="value">The <see cref="VisualNovelNames"/> enum value for which to get the display name.</param>
        /// <returns>The string displaying the name of the novel, or an empty string if the value is not mapped.</returns>
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
                _ => "" // Default case for unmapped values.
            };
        }
    }
}