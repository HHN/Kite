using System;
using UnityEngine;

namespace Assets._Scripts.UIElements.FoundersBubble
{
    public class Positioner : MonoBehaviour
    {
        [Tooltip("Das RectTransform des visuellen Inhalts innerhalb des Buttons, das verschoben werden soll.")]
        [SerializeField] private RectTransform visualContentRect;

        // Diese Werte werden vom InfinityScroll-Skript gesetzt
        [HideInInspector] public int itemIndex; // Wird von außen gesetzt
        [HideInInspector] public float yOffsetAmount; // Wird von außen gesetzt (z.B. 30f)

        private RectTransform buttonRootRect; // Das RectTransform des Button-Roots
        private int totalNovelCount; 

        void Awake()
        {
            buttonRootRect = GetComponent<RectTransform>();

            if (visualContentRect == null && transform.childCount > 0)
            {
                visualContentRect = transform.GetChild(0).GetComponent<RectTransform>();
            }
            if (visualContentRect == null)
            {
                Debug.LogError($"Positioner ({gameObject.name}): visualContentRect not assigned and not found as first child. Please assign it in Inspector.", this);
            }
        }

        void OnEnable()
        {
            if (visualContentRect != null && totalNovelCount > 0) // Prüfen, ob schon initialisiert
            {
                ApplyZigzagYPosition();
            }
        }

        // Diese Methode wird von deinem InfinityScroll-Skript aufgerufen,
        // um den Index und den Offset zu übergeben und die Positionierung zu triggern.
        public void SetupButton(int newIndex, float offset, int novelCount)
        {
            itemIndex = newIndex;
            yOffsetAmount = offset;
            totalNovelCount = novelCount;
            ApplyZigzagYPosition();
        }

        private void ApplyZigzagYPosition()
        {
            if (visualContentRect == null || totalNovelCount == 0) // Prüfen, ob totalNovelCount gesetzt ist
            {
                return;
            }

            float targetYOffset = 0f;
            
            if (itemIndex % 2 == 0) // Gerade logische Indizes der Novels
            {
                targetYOffset = yOffsetAmount;
            }
            else // Ungerade logische Indizes der Novels
            {
                targetYOffset = -yOffsetAmount;
            }

            // Wichtig: Wir setzen die Y-Position des Child-Elements (visualContentRect)
            // Die X-Position bleibt unverändert, da die Horizontal Layout Group den Parent-Button verwaltet.
            visualContentRect.anchoredPosition = new Vector2(visualContentRect.anchoredPosition.x, targetYOffset);
        }
    }
}