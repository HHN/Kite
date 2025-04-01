using Assets._Scripts.SceneControllers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._Scripts.Player
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