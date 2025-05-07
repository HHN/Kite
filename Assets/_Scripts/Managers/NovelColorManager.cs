using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class NovelColorManager
    {
        private static NovelColorManager _instance;

        private Color _color;

        private float _canvasHeight;
        private float _canvasWidth;

        public static NovelColorManager Instance()
        {
            if (_instance == null)
            {
                _instance = new NovelColorManager();
            }

            return _instance;
        }

        public void SetColor(Color color)
        {
            this._color = color;
        }

        public Color GetColor()
        {
            return _color;
        }
    }
}