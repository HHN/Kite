using UnityEngine;

namespace Assets._Scripts.Common.Managers
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

        public void SetCanvasHeight(float height)
        {
            if (height > 0) // Überprüfung, ob die Höhe positiv ist
            {
                this._canvasHeight = height;
            }
        }

        // Getter für canvasHeight
        public float GetCanvasHeight()
        {
            return this._canvasHeight;
        }

        // Setter für canvasWidth
        public void SetCanvasWidth(float width)
        {
            if (width > 0) // Überprüfung, ob die Breite positiv ist
            {
                this._canvasWidth = width;
            }
        }

        // Getter für canvasWidth
        public float GetCanvasWidth()
        {
            return this._canvasWidth;
        }
    }
}