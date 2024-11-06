using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        // Die Mindestzeit (in Sekunden), die der Button gedrückt gehalten werden muss
        public float holdTime = 2f;

        // Interne Variable, um die gedrückte Zeit zu verfolgen
        private float timer = 0f;

        // Flag, um anzuzeigen, ob der Button gerade gedrückt wird
        private bool isHeld = false;

        // Referenz auf den Button (optional, falls Sie den Button beeinflussen möchten)
        private Button button;

        private void Start()
        {
            // Button-Komponente abrufen (falls benötigt)
            button = GetComponent<Button>();
        }

        private void Update()
        {
            // Wenn der Button gedrückt wird
            if (isHeld)
            {
                // Inkrementieren Sie den Timer um die vergangene Zeit seit dem letzten Frame
                timer += Time.deltaTime;

                // Überprüfen, ob die Haltezeit erreicht wurde
                if (timer >= holdTime)
                {
                    // Aktion ausführen
                    OnLongClick();

                    // Timer und Flag zurücksetzen, um Mehrfachaufrufe zu verhindern
                    timer = 0f;
                    isHeld = false;
                }
            }
        }

        // Methode, die aufgerufen wird, wenn der Button lange genug gedrückt wurde
        private void OnLongClick()
        {
            Debug.Log("Button wurde für " + holdTime + " Sekunden gedrückt gehalten.");

            GetComponent<Animator>().SetTrigger("isBlueMessagePrefabWithTrigger");
    }

    // Wird aufgerufen, wenn der Benutzer den Button drückt
    public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Button gedrückt.");
            isHeld = true;
            timer = 0f;
        }

        // Wird aufgerufen, wenn der Benutzer den Button loslässt
        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("Button losgelassen.");

            // Zurücksetzen der Variablen
            isHeld = false;
            timer = 0f;
        }
    }
