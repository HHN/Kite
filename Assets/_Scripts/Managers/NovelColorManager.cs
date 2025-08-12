using UnityEngine;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// Manages a single instance of a color that can be accessed and modified globally.
    /// This is primarily used to ensure consistent color theming across various components
    /// in the application.
    /// </summary>
    public class NovelColorManager
    {
        private static NovelColorManager _instance;

        private Color _color;

        private float _canvasHeight;
        private float _canvasWidth;

        /// <summary>
        /// Retrieves the singleton instance of the NovelColorManager. If no instance exists,
        /// a new one is created and returned. This ensures there is always only one instance of the manager.
        /// </summary>
        /// <returns>The singleton instance of the NovelColorManager.</returns>
        public static NovelColorManager Instance()
        {
            return _instance ??= new NovelColorManager();
        }

        /// <summary>
        /// Sets the current color used by the NovelColorManager.
        /// </summary>
        /// <param name="color">The color to set in the NovelColorManager.</param>
        public void SetColor(Color color)
        {
            _color = color;
        }

        /// <summary>
        /// Retrieves the current color managed by the NovelColorManager instance.
        /// This color is typically used for ensuring consistent theming across the application.
        /// </summary>
        /// <returns>The currently managed color.</returns>
        public Color GetColor()
        {
            return _color;
        }
    }
}