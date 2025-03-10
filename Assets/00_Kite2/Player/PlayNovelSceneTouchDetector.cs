using Assets._Scripts.Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _00_Kite2.Player
{

    public class PlayNovelSceneTouchDetector : MonoBehaviour, IPointerClickHandler
    {
        // Schwellwert in Pixeln, ab dem wir von einer Drag-Bewegung ausgehen
        [SerializeField] private float dragThreshold = 10f;

        public void OnPointerClick(PointerEventData eventData)
        {
            // Pr�fen, ob sich der Finger/Mauszeiger zwischen Dr�cken und Loslassen 
            // weiter als 'dragThreshold' bewegt hat. Wenn ja, war es ein Drag, kein Klick.
            float distance = Vector2.Distance(eventData.pressPosition, eventData.position);
            if (distance > dragThreshold)
            {
                Debug.Log("DRAG");
                // War eher ein Drag � nichts tun
                return;
            }

            // War ein echter Klick � nun die Methode im PlayNovelSceneController aufrufen
            if (PlayNovelSceneController.Instance != null)
            {
                PlayNovelSceneController.Instance.OnConfirm();
            }
            else
            {
                Debug.LogWarning("PlayNovelSceneController.Instance ist nicht gesetzt!");
            }
        }
    }
}