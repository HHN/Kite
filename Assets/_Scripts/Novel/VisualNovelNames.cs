namespace Assets._Scripts.Novel
{
    /// <summary>
    /// Defines a list of specific visual novel names.
    /// This enum provides a strongly-typed way to refer to different novel scenarios.
    /// </summary>
    public enum VisualNovelNames
    {
        None,
        BankKreditNovel,
        InvestorNovel,
        ElternNovel,
        NotariatNovel,
        PresseNovel,
        VermieterNovel,
        HonorarNovel,
        EinstiegsNovel,
        VertriebNovel,
    }

    /// <summary>
    /// Provides helper methods for converting between <see cref="VisualNovelNames"/> enum values,
    /// their corresponding integer IDs, and their string names.
    /// Useful for mapping novel types for storage, display, or logical flow.
    /// </summary>
    public class VisualNovelNamesHelper
    {
        /// <summary>
        /// Converts an integer value to its corresponding <see cref="VisualNovelNames"/> enum member.
        /// </summary>
        /// <param name="value">The integer ID of the visual novel.</param>
        /// <returns>The <see cref="VisualNovelNames"/> enum member that matches the integer ID, or <see cref="VisualNovelNames.None"/> if no match is found.</returns>
        public static VisualNovelNames ValueOf(int value)
        {
            return value switch
            {
                2 => VisualNovelNames.ElternNovel,
                3 => VisualNovelNames.PresseNovel,
                4 => VisualNovelNames.NotariatNovel,
                6 => VisualNovelNames.VermieterNovel,
                9 => VisualNovelNames.InvestorNovel,
                10 => VisualNovelNames.BankKreditNovel,
                11 => VisualNovelNames.HonorarNovel,
                13 => VisualNovelNames.EinstiegsNovel,
                14 => VisualNovelNames.VertriebNovel,
                _ => VisualNovelNames.None
            };
        }

        /// <summary>
        /// Retrieves the localized string name for a given visual novel ID.
        /// </summary>
        /// <param name="value">The integer ID of the visual novel.</param>
        /// <returns>The string representation of the novel's name, or an empty string if the ID is not mapped.</returns>
        public static string GetName(long value)
        {
            return value switch
            {
                2 => "Eltern",
                3 => "Presse",
                4 => "Notarin",
                6 => "Vermieter",
                9 => "Investor",
                10 => "Bankkredit",
                11 => "Honorar",
                13 => "Einstieg",
                14 => "Vertrieb",
                _ => ""
            };
        }
        
        /// <summary>
        /// Converts a string name to its corresponding <see cref="VisualNovelNames"/> enum member.
        /// </summary>
        /// <param name="name">The string name of the visual novel (e.g., "Eltern", "Presse").</param>
        /// <returns>The <see cref="VisualNovelNames"/> enum member that matches the string name, or <see cref="VisualNovelNames.None"/> if no match is found.</returns>
        public static VisualNovelNames ValueByString(string name)
        {
            switch (name)
            {
                case "Eltern":
                    return VisualNovelNames.ElternNovel;
                case "Presse":
                    return VisualNovelNames.PresseNovel;
                case "Notarin":
                    return VisualNovelNames.NotariatNovel;
                case "Vermieter":
                    return VisualNovelNames.VermieterNovel;
                case "Investor":
                    return VisualNovelNames.InvestorNovel;
                case "Bankkredit":
                    return VisualNovelNames.BankKreditNovel;
                case "Honorar":
                    return VisualNovelNames.HonorarNovel;
                case "Einstieg":
                    return VisualNovelNames.EinstiegsNovel;
                case "Vertrieb":
                    return VisualNovelNames.VertriebNovel;
                default:
                    return VisualNovelNames.None;
            }
        }

        /// <summary>
        /// Converts a <see cref="VisualNovelNames"/> enum member to its corresponding integer ID.
        /// </summary>
        /// <param name="value">The <see cref="VisualNovelNames"/> enum member to convert.</param>
        /// <returns>The integer ID associated with the novel name, or 0 if the enum member is not explicitly mapped (e.g., <see cref="VisualNovelNames.None"/>).</returns>
        public static int ToInt(VisualNovelNames value)
        {
            return value switch
            {
                VisualNovelNames.ElternNovel => 2,
                VisualNovelNames.PresseNovel => 3,
                VisualNovelNames.NotariatNovel => 4,
                VisualNovelNames.VermieterNovel => 6,
                VisualNovelNames.InvestorNovel => 9,
                VisualNovelNames.BankKreditNovel => 10,
                VisualNovelNames.HonorarNovel => 11,
                VisualNovelNames.EinstiegsNovel => 13,
                VisualNovelNames.VertriebNovel => 14,
                _ => 0
            };
        }
    }
}