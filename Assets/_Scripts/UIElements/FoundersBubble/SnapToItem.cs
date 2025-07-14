using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.FoundersBubble
{
    public class SnapToItem : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private RectTransform contentPanel;
        [SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;
        [SerializeField] private RectTransform targetItem; // Das Element, zu dem gescrollt werden soll
        [SerializeField] private float scrollSpeed = 10f; // Geschwindigkeit des Scrollens

        private int _currentTarget;

        public ScrollRect ScrollRect
        {
            get => scrollRect;
            set => scrollRect = value;
        }

        public RectTransform ContentPanel
        {
            get => contentPanel;
            set => contentPanel = value;
        }

        // Diese Methode wird von jedem Button aufgerufen, wenn er geklickt wird
        public void CenterOnItem(RectTransform target)
        {
            if (!target || !scrollRect || !scrollRect.content || !scrollRect.viewport)
                return;

            Canvas.ForceUpdateCanvases(); // wichtig!

            // RectTransform content = scrollRect.content;
            // RectTransform viewport = scrollRect.viewport;
            //
            // // 1. Welt-Position des Ziels (z.B. Button)
            // Vector3[] targetWorldCorners = new Vector3[4];
            // target.GetWorldCorners(targetWorldCorners);
            //
            // // 2. Mittelpunkt X des Buttons
            // float targetCenterX = (targetWorldCorners[0].x + targetWorldCorners[3].x) * 0.5f;
            //
            // // 3. Welt-Position des Contents
            // Vector3[] contentWorldCorners = new Vector3[4];
            // content.GetWorldCorners(contentWorldCorners);
            //
            // float contentLeftX = contentWorldCorners[0].x;
            //
            // // 4. Zielposition relativ zum Content
            // float relativePos = targetCenterX - contentLeftX;
            //
            // // 5. Berechnung normalizedScrollValue (zwischen 0..1)
            // float scrollArea = content.rect.width - viewport.rect.width;
            // if (scrollArea <= 0) return; // nicht scrollbar
            //
            // float targetScroll = (relativePos - (viewport.rect.width / 2f)) / scrollArea;
            // targetScroll = Mathf.Clamp01(targetScroll);
            //
            // scrollRect.horizontalNormalizedPosition = targetScroll;
            //
            // Debug.Log($"[Snap] Centering '{target.name}' → Normalized: {targetScroll:F2}");


        }

        private IEnumerator CenterAfterFrame(RectTransform target)
        {
            yield return new WaitForEndOfFrame(); // oder yield return null;
            
            Canvas.ForceUpdateCanvases(); // Wichtig: aktualisiert Layout-Daten

            Vector2 childLocalPos = (Vector2)contentPanel.InverseTransformPoint(target.position);
            float newX = childLocalPos.x - scrollRect.viewport.rect.width / 2 + target.rect.width / 2;
            contentPanel.anchoredPosition = new Vector2(newX, contentPanel.anchoredPosition.y);

            Debug.Log($"Scrolled to X: {newX}");
        }

        private IEnumerator SnapToItemCoroutine(RectTransform targetItem)
        {
            RectTransform viewPortRect = scrollRect.viewport != null ? scrollRect.viewport : (RectTransform)scrollRect.transform;

            float normalizedX = 0f;

            float contentPanelWidth = contentPanel.rect.width;
            float viewPortWidth = viewPortRect.rect.width;

            // Sicherstellen, dass der Content breiter als der Viewport ist, sonst kein Scrollen notwendig
            if (contentPanelWidth <= viewPortWidth)
            {
                normalizedX = 0.5f; // Inhalt zentrieren, falls er komplett in den Viewport passt
            }
            else
            {
                // Mittelpunkt des Viewports (lokal zum ScrollRect)
                float viewPortCenterX = viewPortRect.rect.width / 2f;

                // Mittelpunkt des targetItem (lokal zum Content-Panel)
                // targetItem.localPosition.x ist der X-Offset des Pivots des targetItem vom Pivot des Content-Panels.
                // Wenn das targetItem.pivot.x = 0.5 ist, dann ist targetItem.localPosition.x bereits der Mittelpunkt.
                // Wenn targetItem.pivot.x = 0 ist (links), dann ist targetItem.localPosition.x die linke Kante.
                // Gehen wir davon aus, dass targetItem.pivot.x auf 0 gesetzt ist (Standard für Layout Groups):
                float targetItemLocalCenterX = targetItem.localPosition.x + targetItem.rect.width * targetItem.pivot.x;
                // oder wenn der Pivot immer 0.5 ist, dann einfach:
                // float targetItemLocalCenterX = targetItem.localPosition.x; // Wenn Pivot auf 0.5, dann ist localPosition der Mittelpunkt

                // Besser ist es, die position des elements im content (relativ zu content-panel) zu nutzen:
                // Offset des Mittelpunkts des targetItem vom linken Rand des Content-Panels (wenn Content-Pivot links ist)
                float targetItemCenterRelativeToContentLeft = targetItem.anchoredPosition.x + targetItem.rect.width * 0.5f;

                // Die X-Position, die der linke Rand des Content-Panels haben muss, damit der Mittelpunkt des targetItem
                // in der Mitte des Viewports liegt.
                // Der Viewport-Mittelpunkt ist viewPortWidth / 2.
                // Also muss der linke Rand des Content-Panels auf:
                // (viewPortWidth / 2) - targetItemCenterRelativeToContentLeft
                // gesetzt werden.
                float desiredContentLeftEdgeX = viewPortWidth / 2f - targetItemCenterRelativeToContentLeft;

                // Die Unity ScrollRect horizontalNormalizedPosition ist 0 am linken Anschlag (Content ist ganz links, localPosition.x = 0)
                // und 1 am rechten Anschlag (Content ist ganz rechts, localPosition.x = -(ContentWidth - ViewportWidth)).

                // Der 'Wert' des Content-Panels, der 0 entspricht (ganz links scrollen)
                float maxContentLocalX = 0f;
                // Der 'Wert' des Content-Panels, der 1 entspricht (ganz rechts scrollen)
                float minContentLocalX = -(contentPanelWidth - viewPortWidth);

                // Die desiredContentLeftEdgeX muss innerhalb dieses Bereichs geklammert werden.
                desiredContentLeftEdgeX = Mathf.Clamp(desiredContentLeftEdgeX, minContentLocalX, maxContentLocalX);

                // Jetzt normalisieren wir diese Position.
                // Wenn desiredContentLeftEdgeX = maxContentLocalX (0), dann soll normalizedX = 0 sein.
                // Wenn desiredContentLeftEdgeX = minContentLocalX, dann soll normalizedX = 1 sein.
                normalizedX = Mathf.InverseLerp(maxContentLocalX, minContentLocalX, desiredContentLeftEdgeX);
                normalizedX = Mathf.Clamp01(normalizedX); // Sicherstellen, dass der Wert zwischen 0 und 1 bleibt
            }

            Vector2 startNormalizedPosition = scrollRect.normalizedPosition;
            Vector2 targetNormalizedPosition = new Vector2(normalizedX, scrollRect.normalizedPosition.y); // Y unverändert lassen

            float startTime = Time.time;
            float journeyLength = Vector2.Distance(startNormalizedPosition, targetNormalizedPosition);

            // Nur scrollen, wenn eine Bewegung notwendig ist
            if (journeyLength > 0.001f) // Kleine Toleranz
            {
                while (Vector2.Distance(scrollRect.normalizedPosition, targetNormalizedPosition) > 0.0001f)
                {
                    float distCovered = (Time.time - startTime) * scrollSpeed;
                    float fractionOfJourney = Mathf.Min(1f, distCovered / journeyLength);
                    scrollRect.normalizedPosition = Vector2.Lerp(startNormalizedPosition, targetNormalizedPosition, fractionOfJourney);
                    yield return null;
                }
            }

            // Stelle sicher, dass die genaue Position erreicht wird
            scrollRect.normalizedPosition = targetNormalizedPosition;
        }
    }
}