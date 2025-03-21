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
        public static Color green = new(37 / 255f, 101 / 255f, 14 / 255f);
        public static Color turquoise = new(17 / 255f, 69 / 255f, 74 / 255f);
        public static Color turquoise2 = new(15 / 255f, 69 / 255f, 60 / 255f);
        public static Color darkBlue = new(12 / 255f, 26 / 255f, 46 / 255f);
        public static Color violet = new(83 / 255f, 32 / 255f, 83 / 255f);
        public static Color brown = new(46 / 255f, 37 / 255f, 12 / 255f);
        public static Color greenBrown = new(48 / 255f, 72 / 255f, 15 / 255f);
        public static Color @default = new(0 / 255f, 0 / 255f, 0 / 255f);

        public static Color GetNovelColour(NovelColourPicker novelColour)
        {
            switch (novelColour)
            {
                case NovelColourPicker.Green:
                    return green;
                case NovelColourPicker.Turquoise:
                    return turquoise;
                case NovelColourPicker.Turquoise2:
                    return turquoise2;
                case NovelColourPicker.DarkBlue:
                    return darkBlue;
                case NovelColourPicker.Violet:
                    return violet;
                case NovelColourPicker.Brown:
                    return brown;
                case NovelColourPicker.GreenBrown:
                    return greenBrown;
                case NovelColourPicker.Default:
                default:
                    return @default;
            }
        }
    }
}