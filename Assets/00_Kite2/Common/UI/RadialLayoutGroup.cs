using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.Common.UI
{
    public class RadialLayoutGroup : LayoutGroup
    {
        public enum RadialLayoutStart
        {
            Top,
            Left,
            Right,
            Bottom
        };

        public RadialLayoutStart startFrom;
        [SerializeField] private RectTransform content;
        [SerializeField] private RectTransform canvas;

        public float offset;

        public float radius = 0.5f;
        public float arc = 360.0f;

        public void InitializeRadius()
        {
            if (canvas.rect.width < 1000)
            {
                float scaleFactor = Mathf.Min(1080 / canvas.rect.width, 1920 / canvas.rect.height);
                float actualWidthOfCircle = 200 * scaleFactor;
                radius = ((canvas.rect.width / 2));
                Vector2 size = content.sizeDelta;
                size.y = (canvas.rect.height - (radius * 2)) - 400;
                content.sizeDelta = size;
            }
            else
            {
                float scaleFactor = Mathf.Min(1080 / Screen.width, 1920 / Screen.height);
                float actualWidthOfCircle = 200 * scaleFactor;
                radius = ((Screen.width / 2) - (actualWidthOfCircle / 2));
                Vector2 size = content.sizeDelta;
                size.y = (Screen.height - (radius * 2)) - 400;
                content.sizeDelta = size;
            }

            UpdateChildren();
        }

        public override void SetLayoutHorizontal()
        {
            UpdateChildren();
        }

        public override void SetLayoutVertical()
        {
            UpdateChildren();
        }

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            UpdateChildren();
        }

        public override void CalculateLayoutInputVertical()
        {
            UpdateChildren();
        }

        private void UpdateChildren()
        {
            int i = 0;
            float angleStep = arc / transform.childCount;
            Vector3 direction;


            if (startFrom == RadialLayoutStart.Bottom) direction = -transform.up;
            else if (startFrom == RadialLayoutStart.Right) direction = transform.right;
            else if (startFrom == RadialLayoutStart.Left) direction = -transform.right;
            else direction = transform.up;

            foreach (RectTransform t in transform)
            {
                t.position = transform.position + Quaternion.Euler(0, 0, offset + angleStep * i) * direction * radius;
                i++;
            }
        }

        public void OnDatenschutzButton()
        {
            StartCoroutine(SmoothlyUpdateOffset(0, 1f));
        }

        public void OnImpressumButton()
        {
            StartCoroutine(SmoothlyUpdateOffset(-60, 1f));
        }

        public void OnRessourcenButton()
        {
            StartCoroutine(SmoothlyUpdateOffset(180, 1f));
        }

        public void OnBarrierefreiheitButton()
        {
            StartCoroutine(SmoothlyUpdateOffset(120, 1f));
        }

        public void OnNutzungsbedingungenButton()
        {
            StartCoroutine(SmoothlyUpdateOffset(60, 1f));
        }

        private IEnumerator SmoothlyUpdateOffset(float target, float duration)
        {
            float startOffset = offset;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                offset = Mathf.Lerp(startOffset, target, elapsed / duration);
                UpdateChildren();
                elapsed += Time.deltaTime;
                yield return null;
            }

            offset = target;
            UpdateChildren();
        }
    }
}