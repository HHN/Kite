using UnityEngine;

namespace Assets._Scripts.Utilities
{
    public enum NovelColourPicker
    {
        Green,
        Turquoise,
        Turquoise2,
        DarkBlue,
        Violet,
        Brown,
        GreenBrown,
        Default
    }
    
    public static class NovelColours
    {
        private static readonly Color Green = new(37 / 255f, 101 / 255f, 14 / 255f);
        private static readonly Color Turquoise = new(17 / 255f, 69 / 255f, 74 / 255f);
        private static readonly Color Turquoise2 = new(15 / 255f, 69 / 255f, 60 / 255f);
        private static readonly Color DarkBlue = new(12 / 255f, 26 / 255f, 46 / 255f);
        private static readonly Color Violet = new(83 / 255f, 32 / 255f, 83 / 255f);
        private static readonly Color Brown = new(46 / 255f, 37 / 255f, 12 / 255f);
        private static readonly Color GreenBrown = new(48 / 255f, 72 / 255f, 15 / 255f);
        private static readonly Color Default = new(0 / 255f, 0 / 255f, 0 / 255f);

        public static Color GetNovelColour(NovelColourPicker novelColour)
        {
            switch (novelColour)
            {
                case NovelColourPicker.Green:
                    return Green;
                case NovelColourPicker.Turquoise:
                    return Turquoise;
                case NovelColourPicker.Turquoise2:
                    return Turquoise2;
                case NovelColourPicker.DarkBlue:
                    return DarkBlue;
                case NovelColourPicker.Violet:
                    return Violet;
                case NovelColourPicker.Brown:
                    return Brown;
                case NovelColourPicker.GreenBrown:
                    return GreenBrown;
                case NovelColourPicker.Default:
                default:
                    return Default;
            }
        }
    }
}